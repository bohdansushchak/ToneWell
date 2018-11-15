using System;
using System.IO;
using ToneWell.Services;

namespace ToneWell.Droid.Services
{
    public class AndroidDbPath : IDbPath
    {
        public string GetDatabasePath(string fileName)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);
        }
    }
}