using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transcode.App_Start;
using Transcode.Helpers;

namespace Transcode.Services
{
    public class PaypalPaymentService
    {
        public enum Currency
        {
            eur, usd
        }
        public static Payment CreatePayment(string fileName, Currency currency, decimal price, int quantity, string fileId, string userId)
        {
            var apiContext = PaypalConfig.GetAPIContext();

            // Payment Resource
            var payment = new Payment()
            {
                intent = "Order",
                payer = new Payer() { payment_method = "paypal" },
                transactions = GetTransactionsList(fileName, currency, price.ToString(), quantity.ToString(), fileId, userId),
                redirect_urls = GetReturnUrls()
            };

            // Create a payment using a valid APIContext
            var createdPayment = payment.Create(apiContext);

            return createdPayment;
        }

        private static List<Transaction> GetTransactionsList(string fileName, Currency currency, string price, string quantity, string fileId, string userId)
        {
            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                invoice_number = fileId + "_" + userId,
                amount = new Amount()
                {
                    currency = currency.ToString().ToUpper(),
                    total = price,       // Total must be equal to sum of shipping, tax and subtotal.
                    
                },
                
                item_list = new ItemList()
                {
                    items = new List<Item>()
                    {
                        new Item()
                        {
                            name = fileName,
                            currency = currency.ToString().ToUpper(),
                            price = price,
                            quantity = quantity,
                            sku = fileId
                        }
                    }
                }
            });
            return transactionList;
        }


        private static RedirectUrls GetReturnUrls()
        {
            return new RedirectUrls()
            {
                cancel_url = ApplicationHelper.GetBaseUrl() + "/Paypal/PaymentCancelled",
                return_url = ApplicationHelper.GetBaseUrl() + "/Paypal/PaymentSuccessful"
            };
        }

        public static Payment ExecutePayment(string paymentId, string payerId)
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            var apiContext = PaypalConfig.GetAPIContext();

            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };

            // Execute the payment.
            var executedPayment = payment.Execute(apiContext, paymentExecution);

            return executedPayment;
        }
    }
}