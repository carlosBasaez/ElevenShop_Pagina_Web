using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transbank.Webpay.Common;
using Transbank.Webpay.WebpayPlus;

namespace transbanksdkdotnetrestexample.Controllers
{
    public class PayController
    {
        public static Transbank.Webpay.WebpayPlus.Responses.CreateResponse CrearPago(string buyOrder, int amount, string urlReturn)
        {

            System.Net.ServicePointManager.SecurityProtocol =
            System.Net.SecurityProtocolType.Tls12;

            var random = new Random();
            //var buyOrder = random.Next(999999999).ToString();
            var sessionId = random.Next(999999999).ToString();
            //var amount = random.Next(1000, 999999);
            var returnUrl = urlReturn;
            var result = Transaction.Create(buyOrder, sessionId, amount, returnUrl);

            return result;
        }

        public static string EstadoPago(string token)
        {
            System.Net.ServicePointManager.SecurityProtocol =
            System.Net.SecurityProtocolType.Tls12;

            var result = Transaction.Status(token);
            return result.Vci;
        }
    }
}