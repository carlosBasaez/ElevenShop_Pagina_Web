using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using ModeloDatos;
using ConexionOracle;

namespace NegocioElevenShop
{
    public class Negocio_Cliente
    {


        public static bool InsertarCliente (Cliente cliente)
        {
            ConexionBD bd = new ConexionBD();
            bool insert;
            string nonQuery = $"INSERT INTO Cliente VALUES (" +
                                $" '{cliente.rut_cliente}', " +
                                $" '{cliente.direccion}', " +
                                $" '{cliente.ciudad}', " +
                                $" {cliente.comuna}" +
                                $")";
            insert = bd.NonQuery(nonQuery);
            return insert;
        }

        public static bool updateCliente(Cliente cliente)
        {
            ConexionBD bd = new ConexionBD();
            string nonQuery = $"UPDATE Cliente SET " +
                                $" direccion = '{cliente.direccion}', " +
                                $" ciudad = '{cliente.ciudad}', " +
                                $" comuna = {cliente.comuna} " +
                                $" WEHRE rutcliente = '{cliente.rut_cliente}'";
            return bd.NonQuery(nonQuery);
        }

        public static bool deleteCliente(string rut)
        {
            ConexionBD bd = new ConexionBD();
            string nonQuery = $"DELETE FROM Cliente WHERE rutcliente = '{rut}'";
            return bd.NonQuery(nonQuery);
        }

        public static bool actualizarpass(Usuario usuario)
        {
            usuario.pass = Utilities.GetSHA256(usuario.rut + usuario.pass);
            ConexionBD bd = new ConexionBD();
            bool update;
            string nonQuery = $"UPDATE Usuario SET Contrasena = '{usuario.pass}' WHERE Rut = '{usuario.rut}'";
            update = bd.NonQuery(nonQuery);
            return update;
        }

        public static Cliente getCliente(string rut)
        {
            ConexionBD bd = new ConexionBD();
            string Query = "SELECT rutcliente, direccion, ciudad, comuna FROM Cliente WHERE Rutcliente = '" + rut + "'";
            var select = bd.Query(Query);

            if (select == null || select.Count == 0) return null;

            Cliente cliente = new Cliente();
            var row = select[0];

            cliente.rut_cliente = row[0].ToString();
            cliente.direccion = row[1].ToString();
            cliente.ciudad = row[2].ToString();
            cliente.comuna = int.Parse(row[3].ToString());

            return cliente;
        }

        public static bool existCliente(string rut)
        {
            return getCliente(rut) != null;
        }

    }

}

