using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Docker
            //ConexionBD.connection = "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = ORCLCDB.localdomain))); User Id =  SETHET; password = pipo;";
            //Normal
            //ConexionBD.connection = "DATA SOURCE = xe ;PASSWORD = pipo ;" + "USER ID = SETHET";
            //Console.WriteLine( ConexionBD.test("Seba@gmail.com", "seba.perez"));

            //Console.Read();
            var pass = Console.ReadLine();
            Console.WriteLine(NegocioElevenShop.Utilities.GetSHA256(pass));
            Console.WriteLine(NegocioElevenShop.Utilities.GetSHA256(pass).Length);

            Console.Read();
        }
    }
}
