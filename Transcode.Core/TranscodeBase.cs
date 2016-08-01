using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcode.Core
{
    public enum VideoFomatsSupported
    {
        avi, mov, mpeg, mkv, mp4
    }
    public enum AudioFomatsSupported
    {
        mp3, wav, acc, wma
    }
    public delegate void ProcessCompletionEventHandler(Object sender, EventArgs args);
    public delegate void ProcessStartingEventHandler(Object sender, EventArgs args);
    public abstract class TranscodeBase : ITranscodeMedia
    {
        public event ProcessCompletionEventHandler processCompletionEventHandler;
        public event ProcessStartingEventHandler processStartingEventHandler;
        public Uri FileUri { get; private set; }
        public long FileSize { get; private set; }
        public string FileName { get; set; }
        public int TimeTaken { get; private set; }
        public DateTime ConversionDate { get; private set; }
        protected virtual void Convert(String inputFilepath,String outputFolder, ConversionOptions conversionOptions, bool convertToAudio)
        {
            if(String.IsNullOrWhiteSpace(FileName))
                FileName = Path.GetFileNameWithoutExtension(inputFilepath) + GetFormat(true);

            string outputFilePath = Path.Combine(outputFolder, FileName);
            MediaFile inputFile = new MediaFile(@inputFilepath);
            MediaFile outputFile = new MediaFile(outputFilePath);
            using (var engine = new Engine())
            {
                if (processStartingEventHandler != null)
                    processStartingEventHandler(this, EventArgs.Empty);
                engine.Convert(inputFile, outputFile);
                if (processCompletionEventHandler != null)
                    processCompletionEventHandler(this, EventArgs.Empty);

            }
            
        }
        protected abstract String GetFormat(bool withPoint = false);
        public abstract void Start();
    }
}
