using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos
{
    public class ProductoCantidadCarrito
    {
        int idCarrito;
        int idProducto;
        int cantidad;
        string nombre;
        int valor;
        public int IdCarrito { get => idCarrito; set => idCarrito = value; }
        public int IdProducto { get => idProducto; set => idProducto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Valor { get => valor; set => valor = value; }
    }
}
