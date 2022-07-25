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
    public class Negocio_Estado
    {
        public static Estado GetEstado (int index)
        {
            ConexionBD bd = new ConexionBD();

            string query = "SELECT IdEstado, NombreEstado FROM Estado WHERE IdEstado = " + index;

            List<object[]> select = bd.Query(query);

            if (select == null || select.Count == 0) return null;

            object[] row = select[0];

            Estado estado = new Estado();
            estado.Id_estado = int.Parse(row[0].ToString());
            estado.Nombre_estado = (string)row[1];

            return estado;
              
        }
    }
}
