using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionOracle;
using ModeloDatos;

namespace NegocioElevenShop
{
    public class Negocio_Administrador
    {
        public static bool insert(Administrador admin)
        {
            ConexionBD bd = new ConexionBD();
            string nonquery = $"INSERT INTO administrador (rutadmin) VALUES ('{admin.Rut}')";
            return bd.NonQuery(nonquery);
        }

        public static bool delete(string rut)
        {
            ConexionBD bd = new ConexionBD();
            string nonquery = $"DELETE FROM administrador WHERE rutadmin = '{rut}'";
            return bd.NonQuery(nonquery);
        }

        public static Administrador get(string rut)
        {
            ConexionBD bd = new ConexionBD();
            string query = $"SELECT rutadmin FROM administrador WHERE rutadmin = '{rut}'";
            List<object[]> select = bd.Query(query);

            if (select == null || select.Count == 0) return null;

            object[] row = select[0];
            Administrador admin = new Administrador();
            admin.Rut = row[0].ToString();

            return admin;
        }

        public static List<Administrador> list()
        {
            List<Administrador> lista = new List<Administrador>();
            ConexionBD bd = new ConexionBD();
            string query = $"SELECT rutadmin FROM administrador";
            List<object[]> select = bd.Query(query);

            if (select != null)
            {
                foreach (var row in select)
                {
                    Administrador admin = new Administrador();
                    admin.Rut = row[0].ToString();
                    lista.Add(admin);
                }
            }
            return lista;
        }

        public static List<Usuario> listUsuariosAdmin()
        {
            List<Usuario> lista = new List<Usuario>();
            ConexionBD bd = new ConexionBD();
            string query = $"SELECT nombrecompleto, correoelectronico, rut, telefono FROM administrador a join usuario u on (u.rut = a.rutadmin)";
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
