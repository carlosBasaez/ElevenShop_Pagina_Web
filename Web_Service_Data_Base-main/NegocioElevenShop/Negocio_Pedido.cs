using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeloDatos;
using ConexionOracle;

namespace NegocioElevenShop
{
    public class Negocio_Pedido
    {
        public static bool insertarpedido(Pedido pedido)
        {
            ConexionBD bd = new ConexionBD();
            bool insert;
            string NonQuery = $"INSERT INTO Pedido VALUES (" +
                            $" {pedido.Id_pedido}, " +
                            $" {pedido.Id_carrito}, " +
                            $" TO_TIMESTAMP('{pedido.Fecha.ToString("MM/dd/yyyy HH:mm:ss")}','mm/dd/yyyy hh24:mi:ss'), " +
                            $" '{pedido.Rut_cliente}', " +
                            $" null, " +
                            $" null, " +
                            $" {pedido.Id_estado}" +
                            $")";
            insert = bd.NonQuery(NonQuery);
            return insert;
        }


        public static List<Pedido> GetPedido(string rut)
        {
            ConexionBD bd = new ConexionBD();
            List<Pedido> lista = new List<Pedido>();

            string query = "SELECT IdPedido, IdCarrito, TO_CHAR(Fecha, 'mm/dd/yyyy hh24:mi:ss'), RutCliente, RutBodeguero, RutDespachador, IdEstado FROM Pedido WHERE RutCliente = '" + rut +"' order by idEstado, Fecha desc";

            List<object[]> select = bd.Query(query);

            if (select == null || select.Count == 0) return null;

            foreach (var row in select)
            {
                Pedido pedido = new Pedido();
                pedido.Id_pedido = int.Parse(row[0].ToString());
                pedido.Id_carrito = int.Parse(row[1].ToString());
                
                pedido.Fecha = DateTime.ParseExact(row[2].ToString(), "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                pedido.Rut_cliente = (string)row[3];
                //pedido.Rut_bodeguero = (string)row[4];
                //pedido.Rut_despachador = (string)row[5];
                pedido.Id_estado = int.Parse(row[6].ToString());
                lista.Add(pedido);
            }

            return lista;
        }

        public static List<Pedido> GetPedidoEstado(string estados)
        {
            ConexionBD bd = new ConexionBD();
            List<Pedido> lista = new List<Pedido>();

            string query = "SELECT IdPedido, IdCarrito, TO_CHAR(Fecha, 'mm/dd/yyyy hh24:mi:ss'), RutCliente, RutBodeguero, RutDespachador, IdEstado FROM Pedido WHERE IdEstado in " + estados + " order by idEstado, Fecha asc";

            List<object[]> select = bd.Query(query);

            if (select == null || select.Count == 0) return null;

            foreach (var row in select)
            {
                Pedido pedido = new Pedido();
                pedido.Id_pedido = int.Parse(row[0].ToString());
                pedido.Id_carrito = int.Parse(row[1].ToString());

                pedido.Fecha = DateTime.ParseExact(row[2].ToString(), "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                pedido.Rut_cliente = (string)row[3];
                //pedido.Rut_bodeguero = (string)row[4];
                //pedido.Rut_despachador = (string)row[5];
                pedido.Id_estado = int.Parse(row[6].ToString());
                lista.Add(pedido);
            }

            return lista;
        }

        public static Pedido GetPedido_por_id(int id_pedido)
        {
            ConexionBD bd = new ConexionBD();

            string query = "SELECT IdPedido, IdCarrito, TO_CHAR(Fecha, 'mm/dd/yyyy hh24:mi:ss'), RutCliente, RutBodeguero, RutDespachador, IdEstado FROM Pedido WHERE IdPedido = '" + id_pedido + "'";

            List<object[]> select = bd.Query(query);

            if (select == null || select.Count == 0) return null;

            object[] row = select[0];

            Pedido pedido = new Pedido();
            pedido.Id_pedido = int.Parse(row[0].ToString());
            pedido.Id_carrito = int.Parse(row[1].ToString());
            pedido.Fecha = DateTime.ParseExact(row[2].ToString(), "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            pedido.Rut_cliente = (string)row[3];
            //pedido.Rut_bodeguero = (string)row[4];
            //pedido.Rut_despachador = (string)row[5];
            pedido.Id_estado = int.Parse(row[6].ToString());


            return pedido;
        }

        public static int UltimoPedidoID()
        {
            ConexionBD bd = new ConexionBD();

            string query = "SELECT NVL(MAX(IdPedido), 0) FROM Pedido";

            List<object[]> select = bd.Query(query);

            if (select == null || select.Count == 0) return 0;

            object[] row = select[0];

            int id = int.Parse(row[0].ToString());
            
            return id;
        }

        public static bool Update(Pedido pedido)
        {
            ConexionBD bd = new ConexionBD();

            string nonQuery = $"UPDATE pedido SET " +
                $" fecha = TO_TIMESTAMP('{pedido.Fecha.ToString("MM/dd/yyyy HH:mm:ss")}','mm/dd/yyyy hh24:mi:ss'), " +
                $" idestado = {pedido.Id_estado} " +
                $" WHERE idpedido = {pedido.Id_pedido}";

            return bd.NonQuery(nonQuery);
        }
    }
}
