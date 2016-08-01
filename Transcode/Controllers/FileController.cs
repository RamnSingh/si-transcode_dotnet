using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Transcode.Helpers;
using System.IO;

namespace Transcode.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            decimal storageUsed = SystemHelper.GetStorageUsed(User.Identity.GetUserId());
            ViewBag.StorageUsed = storageUsed == 0 ? 0 : ((storageUsed / (1024)) / 1024);
            System.IO.DirectoryInfo di = new DirectoryInfo(Path.Combine(SystemHelper.GetTranscodeFolder(), User.Identity.GetUserId()));
            ViewBag.FilesInfo = di.GetFiles();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult Download(String fileId, String fileName)
        {
            string localFileName = System.IO.Path.Combine(SystemHelper.GetTranscodeFolder(), User.Identity.GetUserId(), fileId + "_" + fileName);
            if(System.IO.File.Exists(localFileName))
            {
                return File(localFileName, System.Net.Mime.MediaTypeNames.Application.Octet, System.IO.Path.GetFileName(localFileName));
            }
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string fileId, string fileName)
        {
            string localFileName = System.IO.Path.Combine(SystemHelper.GetTranscodeFolder(), User.Identity.GetUserId(), fileId + "_" + fileName);
            if (System.IO.File.Exists(localFileName))
            {
                System.IO.File.Delete(localFileName);
            }

            return RedirectToAction("index");
        }
    }
}