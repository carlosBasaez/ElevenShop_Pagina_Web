using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeloDatos;
using ConexionOracle;

namespace NegocioElevenShop
{
    public class Negocio_Usuario
    {
        public static bool insertarUsuario(Usuario usuario)
        {
            usuario.pass = Utilities.GetSHA256(usuario.rut + usuario.pass);

            ConexionBD bd = new ConexionBD();
            bool insert;
            string NonQuery = $"INSERT INTO Usuario VALUES (" +
                            $" '{usuario.nombrecompleto}', " +
                            $" '{usuario.email}', " +
                            $" '{usuario.rut}', " +
                            $" '{usuario.pass}', " +
                            $" {usuario.telefono}" +
                            $")";
            insert = bd.NonQuery(NonQuery);
            return insert;
        }

        public static bool updateUsuario(Usuario usuario)
        {
            ConexionBD bd = new ConexionBD();
            string nonquery = $"UPDATE usuario SET " +
                $" nombrecompleto = '{usuario.nombrecompleto}', " +
                $" correoelectronico = '{usuario.email}', " +
                $" telefono = {usuario.telefono} " +
                $" WHERE rut = '{usuario.rut}'";
            return bd.NonQuery(nonquery);
        }

        public static bool deleteUsuario(string rut)
        {
            ConexionBD bd = new ConexionBD();
            string nonquery = $"DELETE FROM Usuario WHERE rut = '{rut}'";
            return bd.NonQuery(nonquery);
        }

        public static Usuario getUsuario(string rut)
        {
            Usuario user = null;

            ConexionBD bd = new ConexionBD();

            string query = $"select rut, correoelectronico, contrasena, nombrecompleto, telefono from usuario where rut = '{rut}'";

            List<object[]> select = bd.Query(query);

            if (select == null || select.Count == 0) return null;

            object[] row = select[0];

            user = new Usuario();
            user.rut = row[0].ToString();
            user.email = row[1].ToString();
            user.pass = "te quiero pipo";
            user.nombrecompleto = row[3].ToString();
            user.telefono = int.Parse(row[4].ToString());

            return user;
        }

        public static bool existUsuario(string rut)
        {
            return getUsuario(rut) != null;
        }

        public static Usuario getUsuario(string rut, string pass)
        {
            pass = Utilities.GetSHA256(rut + pass);

            Usuario user = null;

            ConexionBD bd = new ConexionBD();

            string query = $"select rut, correoelectronico, nombrecompleto, telefono, contrasena from usuario where rut = '{rut}' and contrasena = '{pass}'";

            List<object[]> select = bd.Query(query);

            if (select == null || select.Count == 0) return null;

            object[] row = select[0];

            user = new Usuario();
            user.rut = row[0].ToString();
            user.email = row[1].ToString();
            user.pass = "te quiero pipo";
            user.nombrecompleto = row[2].ToString();
            user.telefono = int.Parse(row[3].ToString());

            return user;
        }

        public static List<Usuario> list()
        {
            List<Usuario> lista = new List<Usuario>();
            ConexionBD bd = new ConexionBD();
            string query = $"SELECT nombrecompleto, correoelectronico, rut, telefono FROM usuario";
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

        public static bool existUsuario(string rut, string pass)
        {
            return getUsuario(rut, pass) != null;
        }
    }
}
