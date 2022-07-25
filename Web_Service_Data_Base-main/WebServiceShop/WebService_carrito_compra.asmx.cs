using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ModeloDatos;
using NegocioElevenShop;

namespace WebServiceShop
{
    /// <summary>
    /// Descripción breve de WebService_carrito_compra
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService_carrito_compra : System.Web.Services.WebService
    {

        [WebMethod]
        public string CrearCarrito(int id_carrito, int valor_total)
        {
            ModeloDatos.Carrito_Compra carrito = new ModeloDatos.Carrito_Compra();
            carrito.Id_carrito = id_carrito;
            carrito.Valor_total = valor_total;
            if (NegocioElevenShop.Negocio_CarritoCompra.comprobarID(id_carrito)==false)
            {
                if (NegocioElevenShop.Negocio_CarritoCompra.insertarcarrito(carrito))
                    return "Carrito insertado";
                else
                    return "Error en la insercion del carrito";
            }
            else
            {
                return "carrito no insertado, id ya existe"; 
            }
        }

        [WebMethod]

        public string borrarCarrito(int id_carrito)
        {
            if (NegocioElevenShop.Negocio_CarritoCompra.borrarCarrito(id_carrito) == true)
            {
                NegocioElevenShop.Negocio_CarritoCompra.borrarCarrito(id_carrito);

                return "carrito borrado";
            }
            else
            {
                return "carrito no borrado porque no existe";
            }
        }

        [WebMethod]
        public Carrito_Compra VerCarrito(int idcarrito)
        {
            return Negocio_CarritoCompra.verCarrito(idcarrito);
        }

        [WebMethod]
        public int GetNewID()
        {
            return Negocio_CarritoCompra.ultimoIDCarrito() + 1;
        }
    }
}
