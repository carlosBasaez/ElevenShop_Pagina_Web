using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Transbank.Common;
using Transbank.Patpass.PatpassComercio;

namespace transbanksdkdotnetrestexample.Controllers
{
    public class PatpassComercioController : Controller
    {
        public ActionResult Start()
        {
            var random = new Random();
            
            var url = "https://www.comercio.com/urlretorno";
            var name = "nombre";
            var f_lastname = "pApellido";
            var s_lastname = "sApellido";
            var rut = "14959787-6";
            var service_id = random.Next(999999999).ToString();
            var final_url = "https://www.comercio.com/urlrfinal";
            var commerce_code = "28785666";
            var max_amount = 0;
            var phone_number = random.Next(999999999).ToString();
            var mobile_number = random.Next(999999999).ToString();
            var patpass_name = "nombre del patpass";
            var client_email = "persona@persona.cl";
            var commerce_email = "comercio@comercio.cl";
            var address = "huerfanos 101";
            var city = "city";
            
            var urlHelper = new UrlHelper(ControllerContext.RequestContext);
            var returnUrl = urlHelper.Action("Start", "PatpassComercio", null, Request.Url.Scheme);

            var result = Inscription.Start(
                url: url,
                name: name,
                fLastname: f_lastname,
                sLastname: s_lastname,
                rut: rut,
                serviceId: service_id,
                finalUrl: final_url,
                maxAmount: max_amount,
                phoneNumber: phone_number,
                mobileNumber: mobile_number,
                patpassName: patpass_name,
                personEmail: client_email,
                commerceEmail: commerce_email,
                address: address,
                city: city);

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Action = result.Url;
            ViewBag.Token = result.Token;
            ViewBag.Result = result;
            
            ViewBag.Url = url;
            ViewBag.Name = name;
            ViewBag.F_Lastname = f_lastname;
            ViewBag.S_Lastname = s_lastname;
            ViewBag.Rut = rut;
            ViewBag.Service_id = service_id;
            ViewBag.Final_url = final_url;
            ViewBag.Commerce_code = commerce_code;
            ViewBag.Max_amount = max_amount;
            ViewBag.Phone_number = phone_number;
            ViewBag.Mobile_number = mobile_number;
            ViewBag.Patpass_name = patpass_name;
            ViewBag.Client_name = client_email;
            ViewBag.Commerce_email = commerce_email;
            ViewBag.Address = address;
            ViewBag.City = city;

            return View();
        }

        public ActionResult Status()
        {
            var token = Request.Form["token_ws"];
            var result = Inscription.Status(token);
            
            var urlHelper = new UrlHelper(ControllerContext.RequestContext);

            ViewBag.Action = urlHelper.Action("Status", "PatpassComercio", null, Request.Url.Scheme);
            ViewBag.Result = result;

            return View();
        }

    }
}