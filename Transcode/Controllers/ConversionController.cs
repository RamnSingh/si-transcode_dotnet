using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Transcode.DAL;
using Transcode.Models;
using Transcode.ViewModel.Conversion;
using System.Net.Http.Headers;
using System.Text;
using Transcode.Helpers;
using Transcode.Services;
using System.Net.Mail;
using System.Configuration;

namespace Transcode.Controllers
{
    [Authorize]
    public class ConversionController : Controller
    {
        private IUnitOfWork unitOfWork;
        private HttpClient httpClient = new HttpClient();
        private readonly string apiBaseAddress = ConfigurationManager.AppSettings["apiAddress"];
        private ConversionApi conversionApi;
        private const decimal UNIT_PRICE = 1;
        private const long MAXIMUM_FILE_LENGTH = (1024 * 1024) * 2;
        private const long MAXIMUM_MEMORY_ATTRIBUTED = (1024 * 1024) * 10;
        public ConversionController(IUnitOfWork unitOfWork)
        {
            conversionApi = new ConversionApi();
            httpClient.BaseAddress = new Uri(apiBaseAddress);
            this.unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> Convert()
        {
            ViewBag.audioFormats = await conversionApi.GetAudioFormatsSupported();
            ViewBag.videoFormats = await conversionApi.GetVideoFormatsSupported();
            var cf = Session["conversionForm"] as ConversionForm;
            return View(cf);
        }
        // GET: Conversion
        [HttpPost]
        //Max 2GO
        public async Task <ActionResult> Convert(ConversionForm form)
        {
            if (ModelState.IsValid && isConversionFormValid(form))
            {
                if (!User.Identity.IsAuthenticated)
                {
                    Session.Remove("conversionForm");
                    Session.Add("conversionForm", form);
                    return RedirectToAction("Login", "Account", new { returnUrl = "/Conversion/Convert" });
                }



                try
                {
                    string fileName = Path.GetFileName(form.Url);
                    long fileTime = DateTime.Now.ToFileTime();
                    decimal price = 0;
                    long fileLength = 0;
                    if (form.Url != null)
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(form.Url);
                        HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                        fileLength = response.ContentLength / 1024;
                    }
                    else
                    {
                        fileLength = form.File.ContentLength / 1024;
                    }
                    

                    if((fileLength + SystemHelper.GetStorageUsed(User.Identity.GetUserId())) <= MAXIMUM_MEMORY_ATTRIBUTED)
                    {
                        Session.Add("conversionForm", form);
                        return proceedToPayment(fileName, getPrice(fileLength), 1, fileTime.ToString());
                    }
                    else
                    {
                        Session.Remove("conversionForm");
                        ModelState.AddModelError("", "Stockage insuffisant");
                    }

                    
                }
                catch (Exception)
                {
                    
                }
                ViewBag.audioFormats = await conversionApi.GetAudioFormatsSupported();
                ViewBag.videoFormats = await conversionApi.GetVideoFormatsSupported();
                return View(form);
                
            }
            TempData.Add("modelState", ModelState);
            return RedirectToAction("index", "home");
        }

        
        public async Task<ActionResult> ConvertAfterPayment()
        {
            ConversionForm form = Session["conversionForm"] as ConversionForm;

            string transcodeFolderPath = SystemHelper.GetTranscodeFolder();
            string userTranscodeFolderPath = System.IO.Path.Combine(transcodeFolderPath, User.Identity.GetUserId());
            string localFileName = string.Empty;
            if (!System.IO.Directory.Exists(userTranscodeFolderPath))
                System.IO.Directory.CreateDirectory(userTranscodeFolderPath);


            if (form.Url != null)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(form.Url);
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                const int maxLengthMb = (1024 * 1024) * 2;
                long fileLength = response.ContentLength / 1024;

                string fileName = Path.GetFileName(form.Url);
                decimal price = getPrice(fileLength);
                long fileTime = DateTime.Now.ToFileTime();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (maxLengthMb >= fileLength)
                    {
                        localFileName = fileTime + "_" + Path.GetFileName(form.Url);
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFile(new Uri(form.Url, UriKind.Absolute), Path.Combine(userTranscodeFolderPath, localFileName));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Fichier presente sur l'URL donné est supérieur que 2GO");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "L'URL fournie n'est pas accesible.");
                }

            }
            else
            {
                localFileName = DateTime.Now.ToFileTime() + "_" + form.File.FileName;
                form.File.SaveAs(Path.Combine(userTranscodeFolderPath, localFileName));
            }

            if (ModelState.IsValid)
            {
                String result = await conversionApi.Convert(localFileName, form.Format, User.Identity.GetUserId());

                if (String.IsNullOrWhiteSpace(result))
                {
                    ModelState.AddModelError("", "Problème rencontré lors de transcodage");
                }
                else
                {
                    await SendEmail();
                    return RedirectToAction("Index", "File");
                }
            }


            return View("Convert", form);
        }
        private decimal getPrice(long fileLengthMb)
        {
            return UNIT_PRICE * Math.Ceiling((decimal)(fileLengthMb / 1024)/1024);
        }

        private ActionResult proceedToPayment(string fileName, decimal price, int quantity, string fileId)
        {
            var payment = PaypalPaymentService.CreatePayment(fileName, PaypalPaymentService.Currency.eur, price, quantity,  fileId, User.Identity.GetUserId());

            return Redirect(payment.GetApprovalUrl());
        }

        private async Task SendEmail()
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "transcodeprojet@gmail.com",
                Password = "transcodeprj",
            };
            smtpClient.Send(new MailMessage("transcodeprojet@gmail.com", await UserHelper.GetConnectedUserEmail(User.Identity.GetUserId()))
            {
                Subject = "Transcodage réussi",
                Body = "Veuillez vous conneter sur le site pour pouvoir télécharger le fichier converti."
            });
        }

        private bool isConversionFormValid(ConversionForm form)
        {
            if((form.File == null && form.Url == null) || (form.File != null && form.Url != null))
            {
                ModelState.AddModelError("urlFileRequirement", "Veuillez séléctionner soit un Url ou un fichier");
                return false;
            }
            return true;
        }
    }
}