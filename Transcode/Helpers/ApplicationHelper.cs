using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transcode.Helpers
{
    public class ApplicationHelper
    {
        public static string GetBaseUrl()
        {
            return HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.LocalPath, string.Empty);
        }
    }
}