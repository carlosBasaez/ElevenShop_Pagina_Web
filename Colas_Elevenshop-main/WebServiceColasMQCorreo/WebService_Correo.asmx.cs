using Biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServiceColasMQCorreo
{
    /// <summary>
    /// Descripción breve de WebService_Correo
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService_Correo : System.Web.Services.WebService
    {

        [WebMethod]
        public bool SendReserva(string rut, int id_pedido, string email)
        {
            Cliente c = new Cliente();
            c.Rut = rut;
            c.Id_pedido = id_pedido;
            c.Correo = email;

            return ProduceMQ.NegocioSendCola.SendReserva(c);
        }

        [WebMethod]
        public bool SendPago(string rut, int id_pedido, string email)
        {
            Cliente c = new Cliente();
            c.Rut = rut;
            c.Id_pedido = id_pedido;
            c.Correo = email;

            return ProduceMQ.NegocioSendCola.SendPago(c);
        }

        [WebMethod]
        public bool SendUsuarioNuevo(string rut, string email)
        {
            Cliente c = new Cliente();
            c.Rut = rut;
            c.Correo = email;

            return ProduceMQ.NegocioSendCola.SendUsuarioCreado(c);
        }

        [WebMethod]
        public bool SendCambioContraseña(string rut, string email)
        {
            Cliente c = new Cliente();
            c.Rut = rut;
            c.Correo = email;

            return ProduceMQ.NegocioSendCola.SendCambioContraseña(c);
        }

        [WebMethod]
        public bool SendNewCambioContraseña(string rut, string email, string token)
        {
            Cliente c = new Cliente();
            c.Rut = rut;
            c.Correo = email;

            return ProduceMQ.NegocioSendCola.SendCambioContraseña(c, token);
        }

        [WebMethod]
        public bool SendActualizacionUsuario(string rut, string email)
        {
            Cliente c = new Cliente();
            c.Rut = rut;
            c.Correo = email;

            return ProduceMQ.NegocioSendCola.SendActualizacionUsuario(c);
        }

        [WebMethod]
        public bool SendPedidoDespacho(int pedido_id, string email)
        {
            Cliente c = new Cliente();
            c.Id_pedido = pedido_id;
            c.Correo = email;

            return ProduceMQ.NegocioSendCola.SendPreparadoDespacho(c);
        }

        [WebMethod]
        public bool SendEntregado(int pedido_id, string email)
        {
            Cliente c = new Cliente();
            c.Id_pedido = pedido_id;
            c.Correo = email;

            return ProduceMQ.NegocioSendCola.SendEntregado(c);
        }

        [WebMethod]
        public bool SendEnvio(int pedido_id, string email)
        {
            Cliente c = new Cliente();
            c.Id_pedido = pedido_id;
            c.Correo = email;

            return ProduceMQ.NegocioSendCola.SendEnTransito(c);
        }

        [WebMethod]
        public bool SendCancelado(int pedido_id, string email)
        {
            Cliente c = new Cliente();
            c.Id_pedido = pedido_id;
            c.Correo = email;

            return ProduceMQ.NegocioSendCola.SendCancelado(c);
        }
    }
}
