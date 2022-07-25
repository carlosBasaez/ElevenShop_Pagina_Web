using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeloDatos;
using ConexionOracle;

namespace NegocioElevenShop
{
    public class Negocio_Despachador
    {
        public static bool insert(Despachador despachador)
        {
            ConexionBD bd = new ConexionBD();
            string nonquery = $"INSERT INTO despachador (rutdespachador) VALUES ('{despachador.Rut}')";
            return bd.NonQuery(nonquery);
        }

        public static bool delete(string rut)
        {
            ConexionBD bd = new ConexionBD();
            string nonquery = $"DELETE FROM despachador WHERE rutdespachador = '{rut}'";
            return bd.NonQuery(nonquery);
        }

        public static Despachador get(string rut)
        {
            ConexionBD bd = new ConexionBD();
            string query = $"SELECT rutdespachador FROM despachador WHERE rutdespachador = '{rut}'";
            List<object[]> select = bd.Query(query);

            if (select == null || select.Count == 0) return null;

            object[] row = select[0];
            Despachador despachador = new Despachador();
            despachador.Rut = row[0].ToString();

            return despachador;
        }

        public static List<Despachador> list()
        {
            List<Despachador> lista = new List<Despachador>();
            ConexionBD bd = new ConexionBD();
            string query = $"SELECT rutdespachador FROM despachador";
            List<object[]> select = bd.Query(query);

            if (select != null)
            {
                foreach (var row in select)
                {
                    Despachador despachador = new Despachador();
                    despachador.Rut = row[0].ToString();
                    lista.Add(despachador);
                }
            }
            return lista;
        }

        public static List<Usuario> listUsuariosDespachador()
        {
            List<Usuario> lista = new List<Usuario>();
            ConexionBD bd = new ConexionBD();
            string query = $"SELECT nombrecompleto, correoelectronico, rut, telefono FROM despachador d join usuario u on (u.rut = d.rutdespachador)";
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
