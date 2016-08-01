using MediaToolkit.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcode.Core
{
    public class Transcode
    {
        private TranscodeVideo _transcodevideo;
        private TranscodeAudio _transcodeAudio;
        public TranscodeVideo ConvertToVideo(String inputPath, String outputFolder, VideoFomatsSupported videoFormat)
        {
            validArguments(inputPath, outputFolder);
            _transcodevideo = new TranscodeVideo();
            _transcodevideo.Convert(inputPath, outputFolder, videoFormat);
            return _transcodevideo;
        }

        public TranscodeAudio ConvertToAudio(String inputPath, String outputFolder, AudioFomatsSupported audioFormat)
        {
            validArguments(inputPath, outputFolder);
            _transcodeAudio = new TranscodeAudio();
            _transcodeAudio.Convert(inputPath, outputFolder, audioFormat);
            return _transcodeAudio;
        }

        private void validArguments(String inputPath, String outputFolder)
        {
            if (string.IsNullOrWhiteSpace(inputPath) || string.IsNullOrWhiteSpace(outputFolder))
                throw new ArgumentNullException();

            if (!Directory.Exists(outputFolder))
                throw new Exception("Output folder doesn't exist");
        }
    }
}
