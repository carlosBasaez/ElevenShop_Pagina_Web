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
    public class Negocio_CarritoCompra
    {

        public static bool insertarcarrito(Carrito_Compra carrito)
        {
            ConexionBD bd = new ConexionBD();
            bool insert;
            string NonQuery = "INSERT INTO Carrito_Compra VALUES ("+carrito.Id_carrito+","+carrito.Valor_total+")";
            insert = bd.NonQuery(NonQuery);
            return insert;
        }


        public static bool borrarCarrito(int id_carrito)
        {
            bool borrar;
            ConexionBD bd = new ConexionBD();
            string NonQuery = "DELETE FROM Carrito_Compra WHERE IdCarrito =" + id_carrito + "";
            borrar = bd.NonQuery(NonQuery);
            return borrar;

        }
        public static bool comprobarID(int id_carrito)
        {
            bool comprobar = false;
            ConexionBD bd = new ConexionBD();
            string Query = "SELECT IdCarrito FROM Carrito_Compra WHERE IdCarrito = "+id_carrito+"";
            comprobar = bd.Query(Query).Count > 0;
            return comprobar;


        }

        public static Carrito_Compra verCarrito(int id_carrito)
        {
            Carrito_Compra cc = new Carrito_Compra();
            ConexionBD bd = new ConexionBD();
            string query = "Select idcarrito, valortotal from carrito_compra where idcarrito = " + id_carrito;

            List<object[]> select = bd.Query(query);

            if (select.Count == 0) return null;

            try
            {
                object[] objs = select[0];
                cc.Id_carrito = int.Parse(objs[0].ToString());
                cc.Valor_total = int.Parse(objs[1].ToString());
            }
            catch (Exception ex)
            {
                return null;
            }

            return cc;
        }

        public static int ultimoIDCarrito()
        {
            
            ConexionBD bd = new ConexionBD();
            string query = "Select MAX(idcarrito) from carrito_compra";
            int id;

            List<object[]> select = bd.Query(query);

            if (select.Count == 0) return 0;

            try
            {
                object[] objs = select[0];
                id = int.Parse(objs[0].ToString());
            }
            catch (Exception ex)
            {
                return 0;
            }

            return id;
        }
    }
}
