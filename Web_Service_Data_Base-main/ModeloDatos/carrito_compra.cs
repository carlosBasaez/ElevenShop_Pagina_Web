using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModeloDatos
{
    public class Carrito_Compra
    {
        private int id_carrito;
        private int valor_total;

        public int Id_carrito { get => id_carrito; set => id_carrito = value; }
        public int Valor_total { get => valor_total; set => valor_total = value; }
    }
}
