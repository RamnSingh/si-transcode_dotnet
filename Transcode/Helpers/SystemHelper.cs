using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Transcode.Helpers
{
    public class SystemHelper
    {
        private static string transcodeFolder = "C:\\Transcode";
        public static string GetTranscodeFolder()
        {
            createFolderIfNotExists();
            return transcodeFolder;
        }

        public static long GetStorageUsed(string userId)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(System.IO.Path.Combine(transcodeFolder, userId));

            if(di.Exists)
            {
                return di.GetFiles().Sum(f => f.Length);
            }
            return 0;
        }

        private static void createFolderIfNotExists()
        {
            if(!System.IO.Directory.Exists(transcodeFolder))
            {
                System.IO.Directory.CreateDirectory(transcodeFolder);
            }
        }
    }
}