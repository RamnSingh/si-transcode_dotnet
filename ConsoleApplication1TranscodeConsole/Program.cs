using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transcode.Core;

namespace ConsoleApplication1TranscodeConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            String inputFilePath = "C:\\Users\\Ramneek SINGH\\Documents\\Visual Studio 2013\\Projects\\Transcode\\Transcode\\Content\\test.mp4";
            String outputFolder = "C:\\Users\\Ramneek SINGH\\Documents\\Visual Studio 2013\\Projects\\Transcode\\Transcode\\Content";
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //Thread t1 = new Thread(() => Convert1(inputFilePath, outputFolder));
            //Thread t2 = new Thread(() => Convert2(inputFilePath, outputFolder));
            //t1.Start();
            //t2.Start();
            //t1.Join();
            //t2.Join();
            Convert1(inputFilePath, outputFolder);
            Convert2(inputFilePath, outputFolder);
            //Parallel.Invoke(() => Convert1(inputFilePath, outputFolder), () => Convert2(inputFilePath, outputFolder));
            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());
            Console.WriteLine(sw.Elapsed.Minutes);

            Console.ReadKey();
            //String outputFolderAudio = "C:\\Users\\Ramneek SINGH\\Documents\\Visual Studio 2013\\Projects\\Transcode\\Transcode\\Content";
            //TranscodeAudio transcodeAudio = transcode.ConvertToAudio(inputFilePath, outputFolderAudio, AudioFomatsSupported._3gp);
            //transcodeAudio.processCompletionEventHandler += transcodeVideo_processCompletionEventHandler;
            //transcodeAudio.Start();
        }

        static void Convert1(string inputFilePath, String outputFolder)
        {
            Console.WriteLine("1");
            Process pr = new Process();

            pr.StartInfo.UseShellExecute = false;
            pr.StartInfo.RedirectStandardOutput = true;
            pr.StartInfo.FileName = "C:\\Users\\Ramneek SINGH\\Desktop\\FFmpeg.exe";
            pr.StartInfo.Arguments = "-i C:\\Users\\Ramneek SINGH\\Desktop\\est.mp4 C:\\Users\\Ramneek SINGH\\Desktop\\test.mov";
            pr.Start();
            pr.WaitForExit();
            //Transcode.Core.Transcode transcode = new Transcode.Core.Transcode();
            //TranscodeVideo transcodeVideo = transcode.ConvertToVideo(inputFilePath, outputFolder, VideoFomatsSupported.mkv);
            //transcodeVideo.processCompletionEventHandler += transcodeVideo_processCompletionEventHandler;
            //transcodeVideo.Start();
            Console.WriteLine("1");
        }

        static void Convert2(string inputFilePath, String outputFolder)
        {
            Console.WriteLine("2");
            Process pr = new Process();
            pr.StartInfo.UseShellExecute = false;
            pr.StartInfo.RedirectStandardOutput = true;
            pr.StartInfo.FileName = "C:\\Users\\Ramneek SINGH\\Desktop\\FFmpeg.exe";
            pr.StartInfo.Arguments = "-i C:\\Users\\Ramneek SINGH\\Desktop\\test.mp4 C:\\Users\\Ramneek SINGH\\Desktop\\test.mkv";
            pr.Start();
            pr.WaitForExit();
            //Transcode.Core.Transcode transcode = new Transcode.Core.Transcode();
            //TranscodeVideo transcodeVideo = transcode.ConvertToVideo(inputFilePath, outputFolder, VideoFomatsSupported.mov);
            //transcodeVideo.Start();
            Console.WriteLine("2");
        }
        static void transcodeVideo_processCompletionEventHandler(object sender, EventArgs args)
        {
            Console.WriteLine("Coucou");
        }
    }
}
