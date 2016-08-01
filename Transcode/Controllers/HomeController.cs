using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Transcode.DAL;
using Transcode.Models;
using Transcode.Services;
using Transcode.ViewModel.Conversion;

namespace Transcode.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork unitOfWork;
        private ConversionApi conversionApi;
        public HomeController(IUnitOfWork unitOfWork)
        {
            this.conversionApi = new ConversionApi();
            this.unitOfWork = unitOfWork;
        }
        public async Task<ActionResult> Index()
        {
            var aa =  TempData["modelState"] as ModelStateDictionary;
            if(aa != null)
            {
                foreach (var a in aa)
                {
                    ModelState.Add(a.Key, a.Value);
                }
            }

            ViewBag.audioFormats = await conversionApi.GetAudioFormatsSupported();
            ViewBag.videoFormats = await conversionApi.GetVideoFormatsSupported();

            return View();
        }


        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public async Task<ActionResult> Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        

        
    }
}