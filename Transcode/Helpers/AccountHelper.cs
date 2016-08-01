using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace Transcode.Helpers
{
    public class UserHelper
    {
        public async static Task<string> GetConnectedUserEmail(string userId)
        {
            return await HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().GetEmailAsync(userId);
        }

    }
}