using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using ModeloDatos;
using ConexionOracle;

namespace NegocioElevenShop
{
    public class Negocio_ProductoCantidad
    {
        public static bool insertarProductoCantidad (Producto_Cantidad producto_Cantidad)
        {
            ConexionBD bd = new ConexionBD();
            bool insert;
            string NonQuery = "INSERT INTO Producto_Cantidad (IdCarrito, IdProducto, Cantidad) VALUES  (" + producto_Cantidad.Id_carrito +
                ", " + producto_Cantidad.Id_producto +", " + producto_Cantidad.Cantidad +")";
            insert = bd.NonQuery(NonQuery);
            return insert;
        }

        public static bool borrarProductoCantidad (int id_carrito, int id_producto)
        {
            bool borrar;
            ConexionBD bd = new ConexionBD();
            string NonQuery = "DELETE FROM Producto_Cantidad WHERE IdCarrito = " + id_carrito + "AND IdProducto =" + id_producto;
            borrar = bd.NonQuery(NonQuery);
            return borrar;
        }

        public static bool borrarProductoCantidad(int id_carrito)
        {
            bool borrar;
            ConexionBD bd = new ConexionBD();
            string NonQuery = "DELETE FROM Producto_Cantidad WHERE IdCarrito = " + id_carrito;
            borrar = bd.NonQuery(NonQuery);
            return borrar;
        }

        public static bool actualizarProductoCantidad (Producto_Cantidad producto_Cantidad)
        {
            ConexionBD bd = new ConexionBD();
            bool update;
            string nonQuery = $"UPDATE Producto_Cantidad SET Cantidad = {producto_Cantidad.Cantidad} WHERE IdCarrito = {producto_Cantidad.Id_carrito} AND IdProducto = {producto_Cantidad.Id_producto}";
            update = bd.NonQuery(nonQuery);
            return update;
        }

        public static List<object[]> verProductoCandidad()
        {
            ConexionBD bd = new ConexionBD();
            string query = "SELECT IdCarrito, IdProducto, Cantidad FROM Producto_Cantidad";
            List<object[]> datos = bd.Query(query);
            return datos;
        }

        public static Producto_Cantidad GetProductoCantidad(int idCarrito, int idProducto)
        {
            ConexionBD bd = new ConexionBD();

            string query = $"SELECT IdCarrito, IdProducto, Cantidad FROM Producto_Cantidad WHERE IdCarrito = {idCarrito} and idProducto = {idProducto}";

            List<object[]> select = bd.Query(query);

            if (select == null || select.Count == 0) return null;

            object[] row = select[0];

            Producto_Cantidad producto_Cantidad = new Producto_Cantidad();
            producto_Cantidad.Id_carrito = int.Parse(row[0].ToString());
            producto_Cantidad.Id_producto = int.Parse(row[1].ToString());
            producto_Cantidad.Cantidad = int.Parse(row[2].ToString());


            return producto_Cantidad;

        }

        public static bool comprobarID(int id_carrito, int id_producto)
        {
            bool comprobar = false;
            ConexionBD bd = new ConexionBD();
            string Query = "SELECT * FROM Producto_Cantidad WHERE IdCarrito ="+id_carrito +" AND IdProducto ="+ id_producto+"";
            comprobar = bd.Query(Query).Count > 0;
            return comprobar;

        }
        public static bool comprobarIDproducto(int id_carrito, int id_producto)
        {
            bool comprobar = false;
            ConexionBD bd = new ConexionBD();
            string Query = "SELECT * FROM Producto_Cantidad WHERE IdProducto =" + id_producto + "";
            comprobar = bd.Query(Query).Count > 0;
            return comprobar;

        }
    }
}
