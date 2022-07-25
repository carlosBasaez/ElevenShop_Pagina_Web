using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using ConexionOracle;
using ModeloDatos;

namespace NegocioElevenShop
{
    public class Negocio_Producto
    {
        public static bool insert(Producto producto)
        {
            ConexionBD bd = new ConexionBD();

            string query = $"INSERT INTO Producto (idproducto, nombre, reserva, stock, valor, caracteristicas) VALUES (" +
                $" {NewID()} , " +
                $" '{producto.Nombre}' , " +
                $" {producto.Reserva} , " +
                $" {producto.Stock} , " +
                $" {producto.Valor} , " +
                $" '{producto.Caracteristicas}' " +
                $")";

            return bd.NonQuery(query);
        }

        public static bool update(Producto producto)
        {
            ConexionBD bd = new ConexionBD();

            string query = $"UPDATE Producto SET " +
                $" nombre = '{producto.Nombre}' , " +
                $" reserva = {producto.Reserva} , " +
                $" stock = {producto.Stock} , " +
                $" valor = {producto.Valor} , " +
                $" caracteristicas = '{producto.Caracteristicas}' " +
                $" WHERE idproducto = {producto.Id_producto}";

            return bd.NonQuery(query);
        }

        public static int NewID()
        {
            ConexionBD bd = new ConexionBD();

            string query = "SELECT NVL(MAX(IdProducto),0) FROM Producto";

            List<object[]> select = bd.Query(query);

            if (select == null || select.Count == 0) return 1;

            object[] row = select[0];

            int id = int.Parse(row[0].ToString()) + 1;

            return id;
        }

        public static Producto GetProducto(int index)
        {
            ConexionBD bd = new ConexionBD();

            string query = "SELECT IdProducto, Nombre, Reserva, Stock, Caracteristicas, Valor FROM Producto WHERE IdProducto = " + index;

            List<object[]> select = bd.Query(query);

            if (select == null || select.Count == 0) return null;

            object[] row = select[0];

            Producto producto = new Producto();
            producto.Id_producto = int.Parse(row[0].ToString());
            producto.Nombre = (string)row[1];
            producto.Reserva = int.Parse(row[2].ToString());
            producto.Stock = int.Parse(row[3].ToString());
            producto.Caracteristicas = (string)row[4];
            producto.Valor = int.Parse(row[5].ToString());

            return producto;
        }

        public static List<Producto> ListarProductos()
        {
            ConexionBD bd = new ConexionBD();
            Producto producto;
            List<Producto> lista = new List<Producto>();

            string query = "SELECT IdProducto, Nombre, Reserva, Stock, Caracteristicas, Valor FROM Producto";

            List<object[]> select = bd.Query(query);

            if (select == null) return lista;

            foreach (var row in select)
            {
                producto = new Producto();
                producto.Id_producto = int.Parse(row[0].ToString());
                producto.Nombre = (string)row[1];
                producto.Reserva = int.Parse(row[2].ToString());
                producto.Stock = int.Parse(row[3].ToString());
                producto.Caracteristicas = (string)row[4];
                producto.Valor = int.Parse(row[5].ToString());
                lista.Add(producto);
            }

            return lista;
        }
    }
}
