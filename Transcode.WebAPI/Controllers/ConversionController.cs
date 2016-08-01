using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Transcode.WebAPI.Utils;

namespace Transcode.WebAPI.Controllers
{
    [RoutePrefix("api/Conversion")]
    public class ConversionController : ApiController
    {
        private static readonly string transcodePath = "C:\\Transcode";
        public readonly String[] audioFormatsSupported = Enum.GetNames(typeof(Transcode.Core.AudioFomatsSupported));
        public readonly String[] videoFormatsSupported = Enum.GetNames(typeof(Transcode.Core.VideoFomatsSupported));
        public bool isConverted = false;

        public string FilePath { get; private set; }
        public string OutputFolder { get; private set; }



        [Route("Convert")]
        [HttpPost]
        public String Convert([FromBody]TranscodeRequest request)
        {
            bool validatedFile = validateFile(request.UserId, request.FileName);
            
            if(validatedFile)
            {
                FilePath = System.IO.Path.Combine(transcodePath, request.UserId, request.FileName);
                OutputFolder = System.IO.Path.Combine(transcodePath, request.UserId);
                Core.Transcode transcode = new Core.Transcode();
                if (audioFormatsSupported.Contains<string>(request.ConvertTo.Trim()))
                {
                    return ConvertToAudio(transcode, FilePath, request.ConvertTo.Trim());
                }
                else if (videoFormatsSupported.Contains<string>(request.ConvertTo.Trim()))
                {
                    return ConvertToVideo(transcode, FilePath, request.ConvertTo);
                }
            }

            return null;
        }

        [Route("Formats/Audio")]
        [HttpGet]
        public IEnumerable<string> GetAudioFormatsSupported()
        {
            return audioFormatsSupported;
        }

        [Route("Formats/Video")]
        [HttpGet]
        public IEnumerable<string> GetVideoFormatsSupported()
        {
            return videoFormatsSupported;
        }

        private bool validateFile(string userId, string fileName)
        {
            return System.IO.File.Exists(System.IO.Path.Combine(transcodePath, userId, fileName));
        }

        private String ConvertToAudio(Core.Transcode transcode,String uri,  string convertTo)
        {
            Core.TranscodeAudio transcodeAudio = transcode.ConvertToAudio(uri, OutputFolder, GetAudioFormat(convertTo.ToLower()));
            transcodeAudio.processCompletionEventHandler += onCompletion;
            transcodeAudio.Start();
            return transcodeAudio.FileName;
        }

        private String ConvertToVideo(Core.Transcode transcode, String uri, string convertTo)
        {
            Core.TranscodeVideo transcodeVideo = transcode.ConvertToVideo(uri, OutputFolder, GetVideoFormat(convertTo.ToLower()));
            transcodeVideo.processCompletionEventHandler += onCompletion;
            transcodeVideo.Start();
            return transcodeVideo.FileName;
        }

        private void onCompletion(Object obj, EventArgs evt)
        {
            System.IO.File.Delete(FilePath);
        }

        private Transcode.Core.AudioFomatsSupported GetAudioFormat(String convertTo)
        {
            return (Core.AudioFomatsSupported)Enum.Parse(typeof(Core.AudioFomatsSupported), convertTo.ToLower());
        }
        private Transcode.Core.VideoFomatsSupported GetVideoFormat(String convertTo)
        {
            return (Core.VideoFomatsSupported)Enum.Parse(typeof(Core.VideoFomatsSupported), convertTo.ToLower());
        }
    }
}
