using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeloDatos;
using ConexionOracle;

namespace NegocioElevenShop
{
    public class Negocio_Bodeguero
    {
        public static bool insert(Bodeguero bodeguero)
        {
            ConexionBD bd = new ConexionBD();
            string nonquery = $"INSERT INTO bodeguero (rutbodeguero) VALUES ('{bodeguero.Rut}')";
            return bd.NonQuery(nonquery);
        }

        public static bool delete(string rut)
        {
            ConexionBD bd = new ConexionBD();
            string nonquery = $"DELETE FROM bodeguero WHERE rutbodeguero = '{rut}'";
            return bd.NonQuery(nonquery);
        }

        public static Bodeguero get(string rut)
        {
            ConexionBD bd = new ConexionBD();
            string query = $"SELECT rutbodeguero FROM bodeguero WHERE rutbodeguero = '{rut}'";
            List<object[]> select = bd.Query(query);

            if (select == null || select.Count == 0) return null;

            object[] row = select[0];
            Bodeguero bodeguero = new Bodeguero();
            bodeguero.Rut = row[0].ToString();

            return bodeguero;
        }

        public static List<Bodeguero> list()
        {
            List<Bodeguero> lista = new List<Bodeguero>();
            ConexionBD bd = new ConexionBD();
            string query = $"SELECT rutbodeguero FROM bodeguero";
            List<object[]> select = bd.Query(query);

            if (select != null)
            {
                foreach (var row in select)
                {
                    Bodeguero bodeguero = new Bodeguero();
                    bodeguero.Rut = row[0].ToString();
                    lista.Add(bodeguero);
                }
            }
            return lista;
        }

        public static List<Usuario> listUsuariosBodeguero()
        {
            List<Usuario> lista = new List<Usuario>();
            ConexionBD bd = new ConexionBD();
            string query = $"SELECT nombrecompleto, correoelectronico, rut, telefono FROM bodeguero b join usuario u on (u.rut = b.rutbodeguero)";
            List<object[]> select = bd.Query(query);

            if (select != null)
            {
                foreach (var row in select)
                {
                    Usuario user = new Usuario();
                    user.nombrecompleto = row[0].ToString();
                    user.email = row[1].ToString();
                    user.rut = row[2].ToString();
                    user.telefono = int.Parse(row[3].ToString());
                    user.pass = "te quiero pipo";
                    lista.Add(user);
                }
            }
            return lista;
        }

        public static bool exist(string rut)
        {
            return get(rut) != null;
        }
    }
}
