using System;
using System.Collections.Generic;
using System.Text;

namespace ToneWell.Services
{
    public interface IDbPath
    {
        string GetDatabasePath(string fileName);
    }
}
