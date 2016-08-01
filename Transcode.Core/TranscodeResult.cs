using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcode.Core
{
    public class TranscodeResult
    {
        public Uri FileUri { get; set; }
        public int TimeTaken { get; set; }
        public String FileName { get; set; }
        public long FileSize { get; set; }

    }
}
