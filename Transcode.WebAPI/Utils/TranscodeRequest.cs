using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transcode.WebAPI.Utils
{
    public class TranscodeRequest
    {
        public string UserId { get; private set; }
        public string  FileName { get; private set; }
        public string ConvertTo { get; private set; }
        public TranscodeRequest(string userId, long fileTime, string fileName, string convertTo)
        {
            UserId = userId;
            FileName = fileName;
            ConvertTo = convertTo;
        }
    }
}