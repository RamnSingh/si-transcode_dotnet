using MediaToolkit.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcode.Core
{
    public class TranscodeAudio : TranscodeBase
    {
        private AudioFomatsSupported _audioFormatsSupported;
        private String _inputFilePath;
        private String _outputFilePath;
        private ConversionOptions _conversionOptions;
        internal TranscodeAudio()
        {

        }
        internal void Convert(String inputPath, String outputPath, AudioFomatsSupported audioFormat, ConversionOptions conversionOptions = null)
        {            
            List<string> formats = new List<string>();
            formats.AddRange(Enum.GetNames(typeof(AudioFomatsSupported)).ToList());
            formats.AddRange(Enum.GetNames(typeof(VideoFomatsSupported)).ToList());

            if (!formats.Any(vfs => vfs.Equals(System.IO.Path.GetExtension(inputPath).ToLower().Substring(1))))
                            throw new Exception("Format du fichier n'est pas supporté");

            _inputFilePath = inputPath;
            _outputFilePath = outputPath;
            _audioFormatsSupported = audioFormat;
            conversionOptions = conversionOptions ?? new ConversionOptions();
        }


        public override void Start()
        {
            base.Convert(_inputFilePath, _outputFilePath, _conversionOptions, true);
        }

        protected override string GetFormat(bool withPoint = false)
        {
            return (withPoint ? "." : "") + _audioFormatsSupported.ToString();
        }
    }
}
