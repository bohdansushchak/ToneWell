using System.IO;
using ToneWell.Services;

namespace ToneWell.Droid.Services
{
    public class FileService : IFileService
    {
        public string[] FindFilesMp3()
        {


            
            var path = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;

            //string[] files = Directory.GetFiles(path, "*.mp3");

            System.Diagnostics.Debug.WriteLine(path);

            string[] dire = Directory.GetDirectories(path);
            foreach (var dir in dire)
                System.Diagnostics.Debug.WriteLine(dir);

            return null;
        }
    }
}