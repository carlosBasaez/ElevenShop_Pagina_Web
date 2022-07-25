using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos
{
    public class PassToken
    {
        string rut;
        string token;

        public string Rut { get => rut; set => rut = value; }
        public string Token { get => token; set => token = value; }
    }
}
