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
    /// Descripción breve de WebServices_Producto
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServices_Producto : System.Web.Services.WebService
    {

        [WebMethod]
        public ModeloDatos.Producto MostrarProducto(int id_producto)
        {
            return NegocioElevenShop.Negocio_Producto.GetProducto(id_producto);
        }

        [WebMethod]
        public string Nombre(int id_producto)
        {
            return NegocioElevenShop.Negocio_Producto.GetProducto(1).Nombre;
            
        }

        [WebMethod]
        public List<Producto> Listar()
        {
            return Negocio_Producto.ListarProductos();
        }

        [WebMethod]
        public bool insert(string nombre, int stock, int valor, string caracteristicas)
        {
            Producto prod = new Producto();

            prod.Nombre = nombre;
            prod.Reserva = 0;
            prod.Stock = stock;
            prod.Valor = valor;
            prod.Caracteristicas = caracteristicas.Length <= 100 ? caracteristicas : caracteristicas.Substring(0, 100);

            return Negocio_Producto.insert(prod);
        }

        [WebMethod]
        public bool update(int id, string nombre, int stock, int valor, string caracteristicas)
        {
            Producto prod = Negocio_Producto.GetProducto(id);

            prod.Id_producto = id;
            prod.Nombre = nombre;
            prod.Stock = stock;
            prod.Valor = valor;
            prod.Caracteristicas = caracteristicas.Length <= 100 ? caracteristicas : caracteristicas.Substring(0, 100);

            return Negocio_Producto.update(prod);
        }

        [WebMethod]
        public bool updateStock(int id, int stock, int reserva)
        {
            Producto prod = Negocio_Producto.GetProducto(id);

            prod.Id_producto = id;
            
            prod.Stock = stock;
            prod.Reserva = reserva;

            return Negocio_Producto.update(prod);
        }
    }
}
