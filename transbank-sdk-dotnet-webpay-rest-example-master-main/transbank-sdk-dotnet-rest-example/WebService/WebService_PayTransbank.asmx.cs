using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace transbanksdkdotnetrestexample.WebService
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService_PayTransbank : System.Web.Services.WebService
    {

        public class Pago
        {
            private string token;
            private string url;

            public string Token { get => token; set => token = value; }
            public string Url { get => url; set => url = value; }
        }


        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }


        [WebMethod]
        public Pago CrearPago(string buyOrder, int amount, string urlReturn)
        {
            Transbank.Webpay.WebpayPlus.Responses.CreateResponse instancia = Controllers.PayController.CrearPago(buyOrder, amount, urlReturn);
            Pago pago = new Pago();
            pago.Token = instancia.Token;
            pago.Url = instancia.Url;
            return pago;

        }

        [WebMethod]
        public string EstadoPago(string token)
        {
            var estado = Controllers.PayController.EstadoPago(token);
            return estado;
        }
    }
}
