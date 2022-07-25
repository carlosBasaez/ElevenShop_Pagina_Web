using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Service_Data_Base_ElevenShop
{
    public abstract class Usuario
    {
        public static string RutCurrent = "";
        public static string NombreCurrent = "";

        public string nombrecompleto { get; set; }

        public string email { get; set; }
        
        public string rut { get; set; }

        public int telefono { get; set; }

        public string pass { get; set; }
    }
}
