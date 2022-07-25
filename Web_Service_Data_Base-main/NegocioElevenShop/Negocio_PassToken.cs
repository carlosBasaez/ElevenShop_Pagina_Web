using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeloDatos;
using ConexionOracle;

namespace NegocioElevenShop
{
    public class Negocio_PassToken
    {
        public static bool Create(PassToken passToken)
        {
            ConexionBD bd = new ConexionBD();
            string sql = $"INSERT INTO PassToken (rut, token) VALUES ('{passToken.Rut}', '{passToken.Token}')";
            return bd.NonQuery(sql);
        }

        public static bool Delete(string rut)
        {
            ConexionBD bd = new ConexionBD();
            string sql = $"DELETE FROM PassToken WHERE rut = '{rut}'";
            return bd.NonQuery(sql);
        }

        public static PassToken Get(string rut)
        {
            ConexionBD bd = new ConexionBD();
            string sql = $"SELECT rut, token FROM PassToken WHERE rut = '{rut}'";
            var select = bd.Query(sql);

            if (select == null || select.Count == 0) return null;

            PassToken passToken = new PassToken();
            var row = select[0];

            passToken.Rut = row[0].ToString();
            passToken.Token = row[1].ToString();

            return passToken;
        }

        public static bool Exist(string rut)
        {
            return Get(rut) != null;
        }

        public static PassToken GetSpecific(string rut, string token)
        {
            ConexionBD bd = new ConexionBD();
            string sql = $"SELECT rut, token FROM PassToken WHERE rut = '{rut}' and token = '{token}'";
            var select = bd.Query(sql);

            if (select == null || select.Count == 0) return null;

            PassToken passToken = new PassToken();
            var row = select[0];

            passToken.Rut = row[0].ToString();
            passToken.Token = row[1].ToString();

            return passToken;
        }

        public static bool ExistSpecific(string rut, string token)
        {
            return GetSpecific(rut, token) != null;
        }

        public static PassToken GetToken(string token)
        {
            ConexionBD bd = new ConexionBD();
            string sql = $"SELECT rut, token FROM PassToken WHERE token = '{token}'";
            var select = bd.Query(sql);

            if (select == null || select.Count == 0) return null;

            PassToken passToken = new PassToken();
            var row = select[0];

            passToken.Rut = row[0].ToString();
            passToken.Token = row[1].ToString();

            return passToken;
        }

        public static bool ExistToken(string token)
        {
            return GetToken(token) != null;
        }
    }
}
