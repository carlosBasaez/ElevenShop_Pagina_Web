using Biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ProduceMQ
{
    public class NegocioSendCola
    {
        public static bool SendReserva(Cliente c)
        {
            try
            {
                c.Subject = "Reserva realizada";
                c.Body = "Para Rut: " + c.Rut + "\nSe ha realizado una reserva. \nNº Pedido: " + c.Id_pedido;

                MessageQueue cola = new MessageQueue();
                cola.Path = @".\Private$\ColaCorreos";
                Message mensaje = new Message();
                mensaje.Formatter = new XmlMessageFormatter(new Type[] { typeof(Cliente) });
                mensaje.Body = c;
                mensaje.Label = "Envio Correo";
                cola.Send(mensaje);
                cola.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        public static bool SendPago(Cliente c)
        {
            try
            {
                c.Subject = "Pago realizado";
                c.Body = "Para Rut: " + c.Rut + "\nSe ha realizado el Pago. \nNº Pedido: " + c.Id_pedido;

                MessageQueue cola = new MessageQueue();
                cola.Path = @".\Private$\ColaCorreos";
                Message mensaje = new Message();
                mensaje.Formatter = new XmlMessageFormatter(new Type[] { typeof(Cliente) });
                mensaje.Body = c;
                mensaje.Label = "Envio Correo";
                cola.Send(mensaje);
                cola.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool SendUsuarioCreado(Cliente c)
        {
            try
            {
                c.Subject = "Usuario creado con exito";
                c.Body = "El usuario de Rut: " + c.Rut + "\nSe creado su cuenta en Elevenshop, en este correo se le notificaran todas sus compras \nGracias!";

                MessageQueue cola = new MessageQueue();
                cola.Path = @".\Private$\ColaCorreos";
                Message mensaje = new Message();
                mensaje.Formatter = new XmlMessageFormatter(new Type[] { typeof(Cliente) });
                mensaje.Body = c;
                mensaje.Label = "Envio correo";
                cola.Send(mensaje);
                cola.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool SendCambioContraseña(Cliente c)
        {
            try
            {
                c.Subject = "Contraseña actualizada con exito";
                c.Body = "El usuario de Rut: " + c.Rut + "\nActualizo su contraseña correctamente \nGracias!";

                MessageQueue cola = new MessageQueue();
                cola.Path = @".\Private$\ColaCorreos";
                Message mensaje = new Message();
                mensaje.Formatter = new XmlMessageFormatter(new Type[] { typeof(Cliente) });
                mensaje.Body = c;
                mensaje.Label = "Envio correo";
                cola.Send(mensaje);
                cola.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool SendCambioContraseña(Cliente c, string token)
        {
            try
            {
                c.Subject = "Cambio de contraseña";
                c.Body = "El usuario de Rut: " + c.Rut + $"\nCodigo de Verificación: {token}";

                MessageQueue cola = new MessageQueue();
                cola.Path = @".\Private$\ColaCorreos";
                Message mensaje = new Message();
                mensaje.Formatter = new XmlMessageFormatter(new Type[] { typeof(Cliente) });
                mensaje.Body = c;
                mensaje.Label = "Envio correo";
                cola.Send(mensaje);
                cola.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool SendActualizacionUsuario(Cliente c)
        {
            try
            {
                c.Subject = "Cliente editado con exito";
                c.Body = "El usuario de Rut: " + c.Rut + "\nEdito su perfil y fue guardado de forma exitosa";

                MessageQueue cola = new MessageQueue();
                cola.Path = @".\Private$\ColaCorreos";
                Message mensaje = new Message();
                mensaje.Formatter = new XmlMessageFormatter(new Type[] { typeof(Cliente) });
                mensaje.Body = c;
                mensaje.Label = "Envio correo";
                cola.Send(mensaje);
                cola.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool SendPreparadoDespacho(Cliente c)
        {
            try
            {
                c.Subject = "Pedido preparado para despacho";
                c.Body = "N° de pedido: " + c.Id_pedido + "\nEsta siendo preparado para despacho, se notificara la actualizacion de estado correspondiente";

                MessageQueue cola = new MessageQueue();
                cola.Path = @".\Private$\ColaCorreos";
                Message mensaje = new Message();
                mensaje.Formatter = new XmlMessageFormatter(new Type[] { typeof(Cliente) });
                mensaje.Body = c;
                mensaje.Label = "Envio correo";
                cola.Send(mensaje);
                cola.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool SendEntregado(Cliente c)
        {
            try
            {
                c.Subject = "Pedido fue entregado";
                c.Body = "N° de pedido: " + c.Id_pedido + "\nFue entregado \nGracias por confiar en ElevenShop \nDudas o consultas post venta al correo postventa@elevenshop.cl";

                MessageQueue cola = new MessageQueue();
                cola.Path = @".\Private$\ColaCorreos";
                Message mensaje = new Message();
                mensaje.Formatter = new XmlMessageFormatter(new Type[] { typeof(Cliente) });
                mensaje.Body = c;
                mensaje.Label = "Envio correo";
                cola.Send(mensaje);
                cola.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool SendEnTransito(Cliente c)
        {
            try
            {
                c.Subject = "Pedido en transito";
                c.Body = "N° de pedido: " + c.Id_pedido + "\nEn transito \nGracias por confiar en ElevenShop \nDudas o consultas post venta al correo postventa@elevenshop.cl";

                MessageQueue cola = new MessageQueue();
                cola.Path = @".\Private$\ColaCorreos";
                Message mensaje = new Message();
                mensaje.Formatter = new XmlMessageFormatter(new Type[] { typeof(Cliente) });
                mensaje.Body = c;
                mensaje.Label = "Envio correo";
                cola.Send(mensaje);
                cola.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool SendCancelado(Cliente c)
        {
            try
            {
                c.Subject = "Pedido cancelado";
                c.Body = "N° de pedido: " + c.Id_pedido + "\nCancelado \nGracias por confiar en ElevenShop \nDudas o consultas post venta al correo postventa@elevenshop.cl";

                MessageQueue cola = new MessageQueue();
                cola.Path = @".\Private$\ColaCorreos";
                Message mensaje = new Message();
                mensaje.Formatter = new XmlMessageFormatter(new Type[] { typeof(Cliente) });
                mensaje.Body = c;
                mensaje.Label = "Envio correo";
                cola.Send(mensaje);
                cola.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}