using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Transcode.App_Start;
using Transcode.Services;

namespace Transcode.Controllers
{
    [Authorize]
    public class PaypalController : Controller
    {
        Payment payment;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreatePayment()
        {
            //var payment = PaypalPaymentService.CreatePayment("localFileName", PaypalPaymentService.Currency.eur, (float)10.0, 1, "65454545454545454554", User.Identity.GetUserId());

            //return Redirect(payment.GetApprovalUrl());

            return null;
        }

        public ActionResult PaymentCancelled()
        {
            // TODO: Handle cancelled payment
            return RedirectToAction("Error");
        }

        public ActionResult PaymentSuccessful(string paymentId, string token, string PayerID)
        {
            // Execute Payment
            var payment = PaypalPaymentService.ExecutePayment(paymentId, PayerID);

            return RedirectToAction("ConvertAfterPayment", "Conversion", null);
        }
    }
}