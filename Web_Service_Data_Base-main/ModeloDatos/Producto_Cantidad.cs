using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos
{
    public class Producto_Cantidad
    {
        private int id_carrito;
        private int id_producto;
        private int cantidad;

        public int Id_carrito { get => id_carrito; set => id_carrito = value; }
        public int Id_producto { get => id_producto; set => id_producto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
    }
}
