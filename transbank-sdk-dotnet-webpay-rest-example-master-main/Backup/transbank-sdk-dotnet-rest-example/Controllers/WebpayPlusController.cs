using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Transbank.Webpay.Common;
using Transbank.Webpay.WebpayPlus;

namespace transbanksdkdotnetrestexample.Controllers
{
    public class WebpayPlusController : Controller
    {
        #region Webpay Plus

        public ActionResult NormalCreate()
        {
            var random = new Random();
            var buyOrder = random.Next(999999999).ToString();
            var sessionId = random.Next(999999999).ToString();
            var amount = random.Next(1000, 999999);
            var urlHelper = new UrlHelper(ControllerContext.RequestContext);
            var returnUrl = urlHelper.Action("NormalReturn", "WebpayPlus", null, Request.Url.Scheme);
            var result = Transaction.Create(buyOrder, sessionId, amount, returnUrl);

            ViewBag.BuyOrder = buyOrder;
            ViewBag.SessionId = sessionId;
            ViewBag.Amount = amount;
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Result = result;
            ViewBag.Action = result.Url;
            ViewBag.Token = result.Token;
            return View();
        }

        [HttpPost]
        public ActionResult NormalReturn()
        {
            var token = Request.Form["token_ws"];
            var result = Transaction.Commit(token);

            var urlHelper = new UrlHelper(ControllerContext.RequestContext);

            ViewBag.Token = token;
            ViewBag.Action = urlHelper.Action("ExecuteRefund", "WebpayPlus", null, Request.Url.Scheme);
            ViewBag.Result = result;
            ViewBag.SaveToken = token;

            return View();
        }

        public ActionResult NormalRefund()
        {
            var urlHelper = new UrlHelper(ControllerContext.RequestContext);
            ViewBag.Action = urlHelper.Action("ExecuteRefund", "WebpayPlus", null, Request.Url.Scheme);
            return View();
        }

        [HttpPost]
        public ActionResult ExecuteRefund()
        {
            var token = Request.Form["token_ws"];
            var refundAmount = 500;
            var result = Transaction.Refund(token, refundAmount);

            var urlHelper = new UrlHelper(ControllerContext.RequestContext);
            ViewBag.Action = urlHelper.Action("ExecuteStatus", "WebpayPlus", null, Request.Url.Scheme);

            ViewBag.Token = token;
            ViewBag.Amount = refundAmount;
            ViewBag.Result = result;

            return View();
        }


        public ActionResult NormalStatus()
        {
            var urlHelper = new UrlHelper(ControllerContext.RequestContext);
            ViewBag.Action = urlHelper.Action("ExecuteStatus", "WebpayPlus", null, Request.Url.Scheme);
            return View();
        }

        [HttpPost]
        public ActionResult ExecuteStatus()
        {
            var token = Request.Form["token_ws"];
            var result = Transaction.Status(token);

            ViewBag.Result = result;

            return View();
        }

        #endregion Webpay Plus

        #region Webpay Plus Deferred

        public ActionResult DeferredCreate()
        {
            var random = new Random();
            var buyOrder = random.Next(999999999).ToString();
            var sessionId = random.Next(999999999).ToString();
            var amount = random.Next(1000, 999999);
            var urlHelper = new UrlHelper(ControllerContext.RequestContext);
            var returnUrl = urlHelper.Action("DeferredReturn", "WebpayPlus", null, Request.Url.Scheme);
            var result = DeferredTransaction.Create(buyOrder, sessionId, amount, returnUrl);

            ViewBag.BuyOrder = buyOrder;
            ViewBag.SessionId = sessionId;
            ViewBag.Amount = amount;
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Result = result;
            ViewBag.Action = result.Url;
            ViewBag.Token = result.Token;
            return View();
        }

        [HttpPost]
        public ActionResult DeferredReturn()
        {
            var token = Request.Form["token_ws"];
            var result = DeferredTransaction.Commit(token);

            var urlHelper = new UrlHelper(ControllerContext.RequestContext);

            ViewBag.Token = token;
            ViewBag.Action = urlHelper.Action("ExecuteDeferredCapture", "WebpayPlus", null, Request.Url.Scheme);
            ViewBag.Result = result;
            ViewBag.SaveToken = token;
            ViewBag.SaveAmount = result.Amount.ToString();
            ViewBag.SaveAuthorizationCode = result.AuthorizationCode;

            return View();
        }

        [HttpPost]
        public ActionResult ExecuteDeferredCapture()
        {
            var token = Request.Form["token_ws"];
            var buyOrder = Request.Form["buy_order"];
            var authorizationCode = Request.Form["authorization_code"];
            var captureAmount = decimal.Parse(Request.Form["capture_amount"]);
            var result = DeferredTransaction.Capture(token, buyOrder, authorizationCode, captureAmount);

            var urlHelper = new UrlHelper(ControllerContext.RequestContext);
            ViewBag.Action = urlHelper.Action("ExecuteDeferredRefund", "WebpayPlus", null, Request.Url.Scheme);
            ViewBag.Token = token;
            ViewBag.SaveToken = token;

            ViewBag.BuyOrder = buyOrder;
            ViewBag.AuthorizationCode = authorizationCode;
            ViewBag.CaptureAmount = captureAmount;
            ViewBag.Result = result;

            return View();
        }

        public ActionResult DeferredRefund()
        {
            var urlHelper = new UrlHelper(ControllerContext.RequestContext);
            ViewBag.Action = urlHelper.Action("ExecuteDeferredRefund", "WebpayPlus", null, Request.Url.Scheme);
            return View();
        }

        [HttpPost]
        public ActionResult ExecuteDeferredRefund()
        {
            var token = Request.Form["token_ws"];
            var amount = decimal.Parse(Request.Form["amount"]);
            var result = DeferredTransaction.Refund(token, amount);

            var urlHelper = new UrlHelper(ControllerContext.RequestContext);
            ViewBag.Action = urlHelper.Action("ExecuteDeferredStatus", "WebpayPlus", null, Request.Url.Scheme);
            ViewBag.Token = token;

            ViewBag.Result = result;

            return View();
        }

        public ActionResult DeferredStatus()
        {
            var urlHelper = new UrlHelper(ControllerContext.RequestContext);
            ViewBag.Action = urlHelper.Action("ExecuteDeferredStatus", "WebpayPlus", null, Request.Url.Scheme);
            return View();
        }

        [HttpPost]
        public ActionResult ExecuteDeferredStatus()
        {
            var token = Request.Form["token_ws"];
            var result = DeferredTransaction.Status(token);

            ViewBag.Result = result;

            return View();
        }

        #endregion Webpay Plus Deferred

        #region Webpay Plus Mall

        public ActionResult MallCreate()
        {
            var random = new Random();
            var buyOrder = random.Next(9999999).ToString();
            var sessionId = random.Next(9999999).ToString();
            var urlHelper = new UrlHelper(ControllerContext.RequestContext);
            var returnUrl = urlHelper.Action("MallReturn", "WebpayPlus", null, Request.Url.Scheme);

            var transactions = new List<TransactionDetail>();
            transactions.Add(new TransactionDetail(
                random.Next(9999999),
                "597055555536",
                random.Next(9999999).ToString()
            ));
            transactions.Add(new TransactionDetail(
                random.Next(9999999),
                "597055555537",
                random.Next(9999999).ToString()
            ));

            var result = MallTransaction.Create(buyOrder, sessionId, returnUrl, transactions);

            ViewBag.Result = result;
            ViewBag.BuyOrder = buyOrder;
            ViewBag.SessionId = sessionId;
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Transactions = transactions;
            ViewBag.Action = result.Url;
            ViewBag.Token = result.Token;

            return View();
        }

        [HttpPost]
        public ActionResult MallReturn()
        {
            var token = Request.Form["token_ws"];
            var result = MallTransaction.Commit(token);

            var urlHelper = new UrlHelper(ControllerContext.RequestContext);

            ViewBag.Token = token;
            ViewBag.Action = urlHelper.Action("ExecuteMallRefund", "WebpayPlus", null, Request.Url.Scheme);
            ViewBag.Result = result;
            ViewBag.SaveToken = token;
            ViewBag.SaveAmount = result.Details.First().Amount;
            ViewBag.SaveCommerceCode = result.Details.First().CommerceCode;
            ViewBag.SaveBuyOrder = result.Details.First().BuyOrder;

            ViewBag.Success = result.Details.All(detail => detail.ResponseCode == 0);
            return View();
        }

        public ActionResult MallRefund()
        {
            UrlHelper urlHelper = new UrlHelper(ControllerContext.RequestContext);
            ViewBag.Action = urlHelper.Action("ExecuteMallRefund", "WebpayPlus", null, Request.Url.Scheme);
            return View();
        }

        [HttpPost]
        public ActionResult ExecuteMallRefund()
        {
            var token = Request.Form["token_ws"];
            var buyOrder = Request.Form["buy_order"];
            var commerceCode = Request.Form["commerce_code"];
            var amount = decimal.Parse(Request.Form["amount"]);
            ViewBag.Token = token;
            UrlHelper urlHelper = new UrlHelper(ControllerContext.RequestContext);
            ViewBag.Action = urlHelper.Action("ExecuteMallStatus", "WebpayPlus", null, Request.Url.Scheme);

            ViewBag.SaveAmount = amount;
            ViewBag.SaveCommerceCode = commerceCode;
            ViewBag.SaveToken = token;
            ViewBag.SaveBuyOrder = buyOrder;

            var result = MallTransaction.Refund(token, buyOrder, commerceCode, amount);

            ViewBag.Result = result;

            return View();
        }

        public ActionResult MallStatus()
        {
            UrlHelper urlHelper = new UrlHelper(ControllerContext.RequestContext);
            ViewBag.Action = urlHelper.Action("ExecuteMallStatus", "WebpayPlus", null, Request.Url.Scheme);
            return View();
        }

        public ActionResult ExecuteMallStatus()
        {
            var token = Request.Form["token_ws"];

            var result = MallTransaction.Status(token);

            ViewBag.Result = result;
            ViewBag.SaveToke = token;

            return View();
        }

        #endregion Webpay Plus Mall

        public ActionResult MallDeferredCreate()
        {
            var random = new Random();
            var buyOrder = random.Next(9999999).ToString();
            var sessionId = random.Next(9999999).ToString();
            var urlHelper = new UrlHelper(ControllerContext.RequestContext);
            var returnUrl = urlHelper.Action("MallDeferredCommit", "WebpayPlus", null, Request.Url.Scheme);

            var transactions = new List<TransactionDetail>();
            transactions.Add(new TransactionDetail(
                random.Next(9999999),
                "597055555545",
                random.Next(9999999).ToString()
            ));
            transactions.Add(new TransactionDetail(
                random.Next(9999999),
                "597055555546",
                random.Next(9999999).ToString()
            ));
            
            var result = MallDeferredTransaction.Create(buyOrder, sessionId, returnUrl, transactions);

            ViewBag.Result = result;
            ViewBag.BuyOrder = buyOrder;
            ViewBag.SessionId = sessionId;
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Transactions = transactions;
            ViewBag.Action = result.Url;
            ViewBag.Token = result.Token;

            return View();
        }
        
        public ActionResult MallDeferredCommit()
        {
            var token = Request.Form["token_ws"];
            var result = MallDeferredTransaction.Commit(token);

            var urlHelper = new UrlHelper(ControllerContext.RequestContext);

            ViewBag.Token = token;
            ViewBag.Action = urlHelper.Action("ExecuteMallDeferredCapture", "WebpayPlus", null, Request.Url.Scheme);
            ViewBag.Result = result;
            ViewBag.ResponseCode = result.Details.First().ResponseCode;
            ViewBag.SaveToken = token;
            ViewBag.SaveAmount = result.Details.First().Amount;
            ViewBag.SaveCommerceCode = result.Details.First().CommerceCode;
            ViewBag.SaveBuyOrder = result.Details.First().BuyOrder;
            ViewBag.SaveAuthorizationCode = result.Details.First().AuthorizationCode;

            ViewBag.Success = result.Details.All(detail => detail.ResponseCode == 0);
            return View();
        }
        
        public ActionResult ExecuteMallDeferredCapture()
        {
            var token = Request.Form["token_ws"];
            var buyOrder = Request.Form["buy_order"];
            var authorizationCode = Request.Form["authorization_code"];
            var captureAmount = decimal.Parse(Request.Form["capture_amount"]);
            string commerceCode = Request.Form["commerce_code"];;
            var result = MallDeferredTransaction.Capture(token, commerceCode, buyOrder, authorizationCode, captureAmount);

            var urlHelper = new UrlHelper(ControllerContext.RequestContext);
            ViewBag.Action = urlHelper.Action("ExecuteMallDeferredRefund", "WebpayPlus", null, Request.Url.Scheme);
            ViewBag.Token = token;
            ViewBag.SaveToken = token;

            ViewBag.BuyOrder = buyOrder;
            ViewBag.AuthorizationCode = authorizationCode;
            ViewBag.CaptureAmount = captureAmount;
            ViewBag.CommerceCode = commerceCode;
            ViewBag.Result = result;

            return View();
        }

        public ActionResult ExecuteMallDeferredRefund()
        {
            var token = Request.Form["token_ws"];
            var buyOrder = Request.Form["buy_order"];
            var commerceCode = Request.Form["commerce_code"];
            var amount = decimal.Parse(Request.Form["amount"]);
            ViewBag.Token = token;
            UrlHelper urlHelper = new UrlHelper(ControllerContext.RequestContext);
            ViewBag.Action = urlHelper.Action("ExecuteMallDeferredStatus", "WebpayPlus", null, Request.Url.Scheme);

            ViewBag.SaveAmount = amount;
            ViewBag.SaveCommerceCode = commerceCode;
            ViewBag.SaveToken = token;
            ViewBag.SaveBuyOrder = buyOrder;

            var result = MallDeferredTransaction.Refund(token, buyOrder, commerceCode, amount);

            ViewBag.Result = result;

            return View();
        }

        public ActionResult ExecuteMallDeferredStatus()
        {
            var token = Request.Form["token_ws"];

            var result = MallDeferredTransaction.Status(token);

            ViewBag.Result = result;
            ViewBag.SaveToke = token;

            return View();
        }
    }
}