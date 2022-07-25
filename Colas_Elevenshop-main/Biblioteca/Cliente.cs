using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    public class Cliente
    {
        private string rut;
        private int id_pedido;
        private string correo;
        private string subject;
        private string body;

        public Cliente()
        {
            Init();
        }


        public void Init()
        {
            Rut = "";
            Id_pedido = 0;
            correo = "";
            subject = "";
            body = "";
        }

        public string Rut { get => rut; set => rut = value; }
        public int Id_pedido { get => id_pedido; set => id_pedido = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Subject { get => subject; set => subject = value; }
        public string Body { get => body; set => body = value; }
    }
}
