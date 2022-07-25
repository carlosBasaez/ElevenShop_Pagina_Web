using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using NegocioElevenShop;
using ModeloDatos;

namespace WebServiceShop
{
    /// <summary>
    /// Descripción breve de WebService_pedido
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService_pedido : System.Web.Services.WebService
    {

        [WebMethod]
        public List<ModeloDatos.Pedido> MostrarPedidos(string rut_cliente)
        {
            return NegocioElevenShop.Negocio_Pedido.GetPedido(rut_cliente);
        }

        [WebMethod]
        public List<ModeloDatos.Pedido> MostrarPedidosEstado(string estados)
        {
            return NegocioElevenShop.Negocio_Pedido.GetPedidoEstado(estados);
        }

        [WebMethod]
        public bool insertarPedido (int id_pedido, int id_carrito, DateTime dateTime, string rut_cliente, int id_estado)
        {
            ModeloDatos.Pedido pedido = new ModeloDatos.Pedido();
            pedido.Id_pedido = id_pedido;
            pedido.Id_carrito = id_carrito;
            pedido.Fecha = dateTime;
            pedido.Rut_cliente = rut_cliente;
            pedido.Id_estado = id_estado;

            string correo = NegocioElevenShop.Negocio_Usuario.getUsuario(pedido.Rut_cliente).email;

            ServiceCorreo.WebService_CorreoSoapClient servicio_correo = new ServiceCorreo.WebService_CorreoSoapClient();
            servicio_correo.SendReserva(pedido.Rut_cliente, pedido.Id_pedido, correo);

            return NegocioElevenShop.Negocio_Pedido.insertarpedido(pedido);  
        }

        [WebMethod]

        public ModeloDatos.Pedido pedido_por_id(int id_pedido)
        {
            return NegocioElevenShop.Negocio_Pedido.GetPedido_por_id(id_pedido);
        }

        [WebMethod]
        public int UltimoPedidoID()
        {
            return NegocioElevenShop.Negocio_Pedido.UltimoPedidoID();
        }

        [WebMethod]
        public bool Pagado(int id)
        {
            ModeloDatos.Pedido pedido = NegocioElevenShop.Negocio_Pedido.GetPedido_por_id(id);

            string correo = NegocioElevenShop.Negocio_Usuario.getUsuario(pedido.Rut_cliente).email;

            ServiceCorreo.WebService_CorreoSoapClient servicio_correo = new ServiceCorreo.WebService_CorreoSoapClient();
            servicio_correo.SendPago(pedido.Rut_cliente, pedido.Id_pedido,correo);

            pedido.Id_estado = 1;
            pedido.Fecha = DateTime.Now;
            return NegocioElevenShop.Negocio_Pedido.Update(pedido);
        }

        [WebMethod]
        public bool CambiarEstado(int id, int estado)
        {
            ModeloDatos.Pedido pedido = NegocioElevenShop.Negocio_Pedido.GetPedido_por_id(id);

            string correo = NegocioElevenShop.Negocio_Usuario.getUsuario(pedido.Rut_cliente).email;
            ServiceCorreo.WebService_CorreoSoapClient servicio_correo = new ServiceCorreo.WebService_CorreoSoapClient();

            switch (estado)
            {
                case 2: //Bodega listo para Despacho
                    servicio_correo.SendPedidoDespacho(pedido.Id_pedido, correo);
                    break;
                case 3: //Despacho enviando
                    servicio_correo.SendEnvio(pedido.Id_pedido, correo);
                    break;
                case 4: //Entregado
                    servicio_correo.SendEntregado(pedido.Id_pedido, correo);
                    break;
                case 5: //Cancelado
                    servicio_correo.SendCancelado(pedido.Id_pedido, correo);
                    break;
                default:
                    break;
            }
            

            pedido.Id_estado = estado;
            pedido.Fecha = DateTime.Now;
            return NegocioElevenShop.Negocio_Pedido.Update(pedido);
        }

        [WebMethod]
        public ServiceReference_PayTransbank.Pago CrearPago(int idPedido, string urlReturn)
        {
            var pedido = Negocio_Pedido.GetPedido_por_id(idPedido);
            var total = Negocio_ProductoCantidadCarrito.total_carrito(pedido.Id_carrito);

            var PayTransbank = new ServiceReference_PayTransbank.WebService_PayTransbankSoapClient();
            var pago = PayTransbank.CrearPago(idPedido.ToString(), total, urlReturn);

            if (Negocio_Pago.Exist(idPedido))
            {
                Negocio_Pago.Delete(idPedido);
            }
            var PagoSQL = new ModeloDatos.Pago();
            PagoSQL.IdPedido = idPedido;
            PagoSQL.Token = pago.Token;
            Negocio_Pago.Insert(PagoSQL);

            return pago;
        }

        [WebMethod]
        public bool RevisarPagoToken(string token)
        {
            Pago pago = Negocio_Pago.Find(token);
            if (pago == null) return false;

            Pedido pedido = Negocio_Pedido.GetPedido_por_id(pago.IdPedido);

            if (pedido.Id_estado >= 1) return true;

            var pay = new ServiceReference_PayTransbank.WebService_PayTransbankSoapClient();

            string estado_pago = pay.EstadoPago(token);

            if (estado_pago == "TSY")
            {
                string correo = NegocioElevenShop.Negocio_Usuario.getUsuario(pedido.Rut_cliente).email;

                ServiceCorreo.WebService_CorreoSoapClient servicio_correo = new ServiceCorreo.WebService_CorreoSoapClient();
                servicio_correo.SendPago(pedido.Rut_cliente, pedido.Id_pedido, correo);

                pedido.Id_estado = 1;
                pedido.Fecha = DateTime.Now;
                return NegocioElevenShop.Negocio_Pedido.Update(pedido);
            }

            return false;
        }

        [WebMethod]
        public bool RevisarPagoPedido(int idpedido)
        {
            Pago pago = Negocio_Pago.Get(idpedido);
            if (pago == null) return false;

            Pedido pedido = Negocio_Pedido.GetPedido_por_id(pago.IdPedido);

            if (pedido.Id_estado >= 1) return true;

            var pay = new ServiceReference_PayTransbank.WebService_PayTransbankSoapClient();

            string estado_pago = pay.EstadoPago(pago.Token);

            if (estado_pago == "TSY")
            {
                string correo = NegocioElevenShop.Negocio_Usuario.getUsuario(pedido.Rut_cliente).email;

                ServiceCorreo.WebService_CorreoSoapClient servicio_correo = new ServiceCorreo.WebService_CorreoSoapClient();
                servicio_correo.SendPago(pedido.Rut_cliente, pedido.Id_pedido, correo);

                pedido.Id_estado = 1;
                pedido.Fecha = DateTime.Now;
                return NegocioElevenShop.Negocio_Pedido.Update(pedido);
            }

            return false;
        }
    }
}
