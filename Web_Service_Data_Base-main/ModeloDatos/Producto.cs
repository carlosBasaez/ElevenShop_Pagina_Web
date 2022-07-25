using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos
{
    public class Producto
    {
        private int id_producto;
        private string nombre;
        private int stock;
        private string caracteristicas;
        private int reserva;
        private int valor;

        public int Id_producto { get => id_producto; set => id_producto = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Stock { get => stock; set => stock = value; }
        public string Caracteristicas { get => caracteristicas; set => caracteristicas = value; }
        public int Reserva { get => reserva; set => reserva = value; }
        public int Valor { get => valor; set => valor = value; }
    }
}
