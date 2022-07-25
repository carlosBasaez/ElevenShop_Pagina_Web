using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.OracleClient;
using ConexionOracle;

namespace ConexionOracle
{
    public class ConexionBD
    {
        public string string_connection = "DATA SOURCE = xe ;" +
             "PASSWORD = 1234 ;" + "USER ID = ELEVENSHOP";
        public OracleConnection conexion;

        public ConexionBD(string string_conexion)
        {
            this.string_connection = string_conexion;
            CrearConexion();
        }

        public ConexionBD()
        {
            CrearConexion();
        }

        void CrearConexion()
        {
            try
            {
                conexion = new OracleConnection(string_connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(">> ERROR: ConexionBD.CrearConexion. No se pudo crear la coneccion. StringConnection: " + string_connection + "\nMessage: " + ex.Message);
            }
        }

        public bool Open()
        {
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(">> ERROR: ConexionBD.Open. No se pudo abrir la coneccion. \nMessage: " + ex.Message);
                return false;
            }
            return true;
        }

        public bool Close()
        {
            try
            {
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(">> ERROR: ConexionBD.Close. No se pudo cerrar la coneccion. \nMessage: " + ex.Message);
                return false;
            }
            return true;
        }

        public bool NonQuery(string nonquery)
        {
            bool correcto = false;

            if (Open())
            {

                try
                {
                    OracleCommand command = new OracleCommand(nonquery, conexion);
                    int resultado = command.ExecuteNonQuery();

                    if (resultado > 0)
                        correcto = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">> ERROR: ConexionBD.NonQuery. No se pudo completar. NonQuery: " + nonquery + "\nMessage: " + ex.Message);
                    correcto = false;
                }

            }
            Close();

            return correcto;
        }

        public List<object[]> Query(string query)
        {
            List<object[]> list = new List<object[]>();
            object[] objs;

            if (Open())
            {

                try
                {
                    OracleCommand command = new OracleCommand(query, conexion);
                    OracleDataReader reader = command.ExecuteReader();
                    int columns = reader.FieldCount;

                    while (reader.Read())
                    {
                        objs = new object[columns];
                        for (int i = 0; i < columns; i++)
                        {
                            objs[i] = reader[i];
                        }
                        list.Add(objs);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">> ERROR: ConexionBD.Query. No se pudo completar. Query: " + query + "\nMessage: " + ex.Message);
                    list = new List<object[]>();
                }

            }
            Close();

            return list;
        }



        //public static bool test(string email, string pass) 
        //{
        //    bool indicador = false;
        //    using (OracleConnection ora = new OracleConnection(string_connection))
        //    {
        //        try
        //        {
        //            ora.Open();
        //            OracleCommand command = new OracleCommand("SELECT RUT,NOMBRECOMPLETO FROM USUARIO WHERE :EMAIL IN (RUT, correoelectronico) AND contrasena=:PASS", ora);
        //            command.Parameters.Add(":EMAIL", OracleType.VarChar).Value = email;
        //            command.Parameters.Add(":PASS", OracleType.VarChar).Value = pass;
        //            OracleDataReader lector = command.ExecuteReader();
        //            if (lector.Read())
        //            {
        //                indicador = true;
        //                Usuario.RutCurrent = lector.GetString(0);
        //                Usuario.NombreCurrent = lector.GetString(1);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(">>>>>>>" + ex.Message);
        //        }
        //        ora.Close();
        //    }
        //    return indicador;
        //}
    }
}
