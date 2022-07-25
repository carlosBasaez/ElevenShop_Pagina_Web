using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos
{
    public class Pago
    {
        int idPedido;
        string token;

        public int IdPedido { get => idPedido; set => idPedido = value; }
        public string Token { get => token; set => token = value; }
    }
}
