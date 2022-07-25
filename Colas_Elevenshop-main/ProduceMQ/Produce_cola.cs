using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using Biblioteca;

namespace ProduceMQ
{
    class Produce_cola
    {
        static void Main(string[] args)
        {
            MessageQueue cola = new MessageQueue();
            cola.Path = @".\Private$\ColaCorreos";
            Message mensaje = new Message();
            Cliente c = new Cliente();
            c.Rut = "1";
            c.Id_pedido = 1;
            mensaje.Formatter = new XmlMessageFormatter(new Type[] { typeof(Cliente) });
            mensaje.Body = c;
            mensaje.Label = "Envio Correo";
            cola.Send(mensaje);
            cola.Close();
        }
    }
}
