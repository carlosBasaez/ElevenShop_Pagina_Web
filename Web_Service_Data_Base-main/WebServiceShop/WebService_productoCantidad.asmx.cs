using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServiceShop
{
    /// <summary>
    /// Descripción breve de WebService_productoCantidad
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService_productoCantidad : System.Web.Services.WebService
    {

        [WebMethod]
        public string insertar_producto_cantidad(int id_carrito, int id_producto, int cantidad)
        {
            if (NegocioElevenShop.Negocio_ProductoCantidad.comprobarID(id_carrito, id_producto)==false)
            {
                ModeloDatos.Producto_Cantidad producto = new ModeloDatos.Producto_Cantidad();
                producto.Id_carrito = id_carrito;
                producto.Id_producto = id_producto;
                producto.Cantidad = cantidad;

                if (NegocioElevenShop.Negocio_ProductoCantidad.insertarProductoCantidad(producto))
                    return "Producto_Cantidad insertado";
                else
                    return "Producto_Cantidad no se ha insertado";
            }
            else
            {
                return "no se puede ingresar, la id carrito o id producto ya existe";
            }
            
        }

        [WebMethod]
        public string borrar_producto_cantidad (int id_carrito, int id_producto)
        {
            if (NegocioElevenShop.Negocio_ProductoCantidad.comprobarID(id_carrito, id_producto)==true)
            {
                NegocioElevenShop.Negocio_ProductoCantidad.borrarProductoCantidad(id_carrito, id_producto);

                return "datos borrados";
            }
            else
            {
                return "datos no borrados, el id no existe";
            }
        }

        [WebMethod]
        public bool borrar_producto_cantidad_carrito(int id_carrito)
        {
            return NegocioElevenShop.Negocio_ProductoCantidad.borrarProductoCantidad(id_carrito);
        }

        [WebMethod]
        public string update_producto_cantidad (int id_carrito, int id_producto, int cantidad)
        {
            if (NegocioElevenShop.Negocio_ProductoCantidad.comprobarID(id_carrito, id_producto)==true)
            {
                ModeloDatos.Producto_Cantidad producto = new ModeloDatos.Producto_Cantidad();
                producto.Id_carrito = id_carrito;
                producto.Id_producto = id_producto;
                producto.Cantidad = cantidad;

                NegocioElevenShop.Negocio_ProductoCantidad.actualizarProductoCantidad(producto);

                return "datos actualizados";
            }
            else
            {
                return "no se pudieron actualizar los datos, id incorrecto";
            }
            
        }

        [WebMethod]
        public ModeloDatos.Producto_Cantidad GetProducto_Cantidad(int id_carrito, int id_producto)
        {
            return NegocioElevenShop.Negocio_ProductoCantidad.GetProductoCantidad(id_carrito, id_producto); 
        }

        [WebMethod]
        public bool ExisteProductoCantidad(int id_carrito, int id_producto)
        {
            return NegocioElevenShop.Negocio_ProductoCantidad.GetProductoCantidad(id_carrito, id_producto) != null;
        }
    }
}
