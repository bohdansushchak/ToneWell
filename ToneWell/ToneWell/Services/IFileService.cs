using System;
using System.Collections.Generic;
using System.Text;

namespace ToneWell.Services
{
    public interface IFileService
    {
        List<string> FindAllMp3Files();
    }
}
