using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Transcode.ViewModel.Conversion
{
    public class ConversionForm
    {
        [Url(ErrorMessage="L'url fourni n'est pas valid")]
        public String Url { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }
        [Required]
        public String Format { get; set; }
    }
}