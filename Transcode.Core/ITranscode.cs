using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transcode.Core
{
    public interface ITranscodeVideo
    {
        void Convert(VideoFomatsSupported videoFomatsSupported);
    }
    public interface ITranscodeAudio
    {
        void Convert(AudioFomatsSupported audioFomatsSupported);
    }
}
