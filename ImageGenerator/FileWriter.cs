using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Drawing;

namespace ImageGenerator
{
    public static class FileWriter
    {
        private static string RootDir =>
            new DirectoryInfo(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.ToString();
        private static string OutputDir => Path.Combine(RootDir, "Images");
        private static string ImageFilePath(string fileName) => Path.Combine(OutputDir, fileName);

        public static void WriteImage(Canvas canvas, bool remux = true)
        {
            var filename = $"{GetTimeFormatted()}.ppm";
            // Directory.CreateDirectory(OutputDir);

            var path = ImageFilePath(filename);
            var ppm = PPMConverter.SerializeCanvas(canvas, 255);
            File.WriteAllText(path, ppm);
            Console.WriteLine($"File created or overwritten: {path}");

            if (remux) RemuxImage(path);
        }

        private static void RemuxImage(string path)
        {
            var origFileInfo = new FileInfo(path);
            var remuxedInfo = GetFileWithDiffExtension(origFileInfo, ".png");

            var proc = new ProcessStartInfo("ffmpeg") { UseShellExecute = true };
            proc.Arguments = $"-i {origFileInfo} {remuxedInfo}";

            Console.WriteLine($"Remuxing image: {origFileInfo} -> {remuxedInfo}");
            Process.Start(proc);
        }

        private static FileInfo GetFileWithDiffExtension(FileInfo info, string newExt)
        {
            var extLen = info.Extension.Length;
            var name = info.FullName.Substring(0, info.FullName.Length - extLen);
            return new FileInfo(name + newExt);
        }

        private static string GetTimeFormatted()
        {
            return DateTime.Now.ToString("yy-MM-dd_hh-mm-ss");
        }
    }
}