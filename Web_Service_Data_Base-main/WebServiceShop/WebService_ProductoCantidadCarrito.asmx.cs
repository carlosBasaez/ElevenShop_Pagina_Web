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
    /// Descripción breve de WebService_ProductoCantidadCarrito
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService_ProductoCantidadCarrito : System.Web.Services.WebService
    {

        [WebMethod]
        public List<ProductoCantidadCarrito> Listar(int id_carrito)
        {
            return Negocio_ProductoCantidadCarrito.Lista(id_carrito);
        }

        [WebMethod]
        public int ValorTotal(int id_carrito)
        {
            return Negocio_ProductoCantidadCarrito.total_carrito(id_carrito);
        }
    }
}
