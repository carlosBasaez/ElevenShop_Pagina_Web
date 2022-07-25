using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeloDatos;
using ConexionOracle;

namespace NegocioElevenShop
{
    public class Negocio_Pago
    {
        public static Pago Get(int idPedido) 
        {
            ConexionBD bd = new ConexionBD();
            string sql = $"SELECT IdPedido, Token FROM Pago WHERE IdPedido = {idPedido}";
            var select = bd.Query(sql);

            if (select == null || select.Count == 0) return null;

            var row = select[0];

            Pago pago = new Pago();
            pago.IdPedido = int.Parse(row[0].ToString());
            pago.Token = row[1].ToString();

            return pago;
        }

        public static Pago Find(string token)
        {
            ConexionBD bd = new ConexionBD();
            string sql = $"SELECT IdPedido, Token FROM Pago WHERE Token = '{token}'";
            var select = bd.Query(sql);

            if (select == null || select.Count == 0) return null;

            var row = select[0];

            Pago pago = new Pago();
            pago.IdPedido = int.Parse(row[0].ToString());
            pago.Token = row[1].ToString();

            return pago;
        }

        public static bool Exist(int idPedido)
        {
            return Get(idPedido) != null;
        }

        public static bool Delete(int idPedido)
        {
            ConexionBD bd = new ConexionBD();
            string sql = $"Delete FROM Pago WHERE idPedido = {idPedido}";
            return bd.NonQuery(sql);
        }

        public static bool Insert(Pago pago)
        {
            ConexionBD bd = new ConexionBD();
            string sql = $"INSERT INTO Pago (IdPedido, Token) VALUES ({pago.IdPedido}, '{pago.Token}')";
            return bd.NonQuery(sql);
        }
    }
}
