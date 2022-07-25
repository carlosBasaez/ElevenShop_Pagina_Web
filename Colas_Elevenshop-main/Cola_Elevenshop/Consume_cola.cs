using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using Biblioteca;

namespace AppCorreos
{
    class Consume_cola
    {
        static void Main(string[] args)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.To.Add(new System.Net.Mail.MailAddress("carlitos.basaez.54@gmail.com"));
            msg.Subject = "Su compra en ElevenShop fue realizada";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = "Compra Realizada, gracias por su compra";
            msg.BodyEncoding = System.Text.Encoding.UTF8;

            msg.From = new System.Net.Mail.MailAddress("elevenshopnoreply@gmail.com");

            System.Net.Mail.SmtpClient correo = new System.Net.Mail.SmtpClient();


            correo.Credentials = new System.Net.NetworkCredential("elevenshopnoreply@gmail.com", "elevenshop.123");

            correo.Port = 587;

            correo.EnableSsl = true;

            correo.Host = "smtp.gmail.com";



            Message mensaje;
            int cant = 0;
            Console.WriteLine("En espera de correos...");
            while (true)
            {
                MessageQueue queue = new MessageQueue();
                queue.Path = @".\Private$\ColaCorreos";
                mensaje = queue.Receive();
                mensaje.Formatter = new XmlMessageFormatter(new Type[] { typeof(Cliente) });
                Cliente c = (Cliente)mensaje.Body;
                Console.WriteLine("Correo Numero: " + (++cant) + " Enviado");


                msg.To.Clear();
                msg.To.Add(new System.Net.Mail.MailAddress(c.Correo));
                msg.Subject = c.Subject;
                msg.Body = c.Body;

                correo.Send(msg);
            }
        }
    }
}
