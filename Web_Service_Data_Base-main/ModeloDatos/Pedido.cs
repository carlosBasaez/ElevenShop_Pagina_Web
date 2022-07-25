using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos
{
    public class Pedido
    {
        int id_pedido;
        DateTime fecha;
        string email;
        string direccion;
        string rut_cliente;
        string rut_bodeguero;
        string rut_despachador;
        int id_carrito;
        int id_estado;

        public int Id_pedido { get => id_pedido; set => id_pedido = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Email { get => email; set => email = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Rut_cliente { get => rut_cliente; set => rut_cliente = value; }
        public int Id_carrito { get => id_carrito; set => id_carrito = value; }
        public int Id_estado { get => id_estado; set => id_estado = value; }
        public string Rut_bodeguero { get => rut_bodeguero; set => rut_bodeguero = value; }
        public string Rut_despachador { get => rut_despachador; set => rut_despachador = value; }
    }
}
