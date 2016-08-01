using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Transcode.Attributes
{
    public class AcceptOnlyAttribute : ValidationAttribute
    {
        private string[] restricted;
        public AcceptOnlyAttribute(params string[] restricted)
        {
            this.restricted = restricted;
        }

        public override bool IsValid(object value)
        {
            return restricted.Contains(value) ? true : false;
        }
    }
}