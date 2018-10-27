using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToneWell.Services;

namespace ToneWell.Droid.Services
{
    public class FileService : IFileService
    {
        public List<string> FindAllMp3Files()
        {
            var path = "/storage/";

            List<string> directories = Directory.GetDirectories(path).ToList();
            List<string> files = Directory.GetFiles(path, "*.mp3").ToList();

            for (int i = 0; i < directories.Count; i++)
            {
                try
                {
                    var dir = directories[i] + "/";

                    var newDir = Directory.GetDirectories(dir);

                    if (newDir != null)
                        if (newDir.Count() > 0)
                            directories.AddRange(newDir);

                    var newFiles = Directory.GetFiles(dir, "*.mp3");

                    if (newFiles != null)
                        if (newFiles.Count() > 0)
                            files.AddRange(newFiles);
                }
                catch (UnauthorizedAccessException e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }

            foreach (var dir in files)
                System.Diagnostics.Debug.WriteLine(dir);

            return files;
        }
    }
}

