using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos
{
    public class Cliente
    {
        public string rut_cliente;
        public string direccion;
        public string ciudad;
        public int comuna;

        public string Rut_cliente { get => rut_cliente; set => rut_cliente = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Ciudad { get => ciudad; set => ciudad = value; }
        public int Comuna { get => comuna; set => comuna = value; }


    }
}
