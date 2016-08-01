using MediaToolkit;
using MediaToolkit.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcode.Core
{
    public class TranscodeVideo : TranscodeBase
    {
        private VideoFomatsSupported _videoFormatsSupported;
        private String _inputFilePath;
        private String _outputFilePath;
        private ConversionOptions _conversionOptions;
        internal TranscodeVideo()
        {
        }
        internal void Convert(String inputPath, String outputPath, VideoFomatsSupported videoFormat, ConversionOptions conversionOptions = null)
        {
            if (!Enum.GetNames(typeof(VideoFomatsSupported)).Cast<String>()
                .Any(vfs => vfs.Equals(System.IO.Path.GetExtension(inputPath.ToLower()).Substring(1))))
                throw new Exception("Format du fichier n'est pas supporté");

            _inputFilePath = inputPath;
            _outputFilePath = outputPath;
            _videoFormatsSupported = videoFormat;
            conversionOptions = conversionOptions ?? new ConversionOptions();
        }

        public override void Start()
        {
            base.Convert(_inputFilePath, _outputFilePath, _conversionOptions, false);
        }


        protected override string GetFormat(bool withPoint = false)
        {
            return (withPoint ? "." : "") +  _videoFormatsSupported.ToString();
        }
    }
}
