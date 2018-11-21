using System.Collections.Generic;
using System.Threading.Tasks;
using ToneWell.Models;

namespace ToneWell.Services
{
    public interface ITrackManager
    {
        void SaveTracks(List<Track> tracks);
        Task<List<Track>> GetAllTracks();
    }
}
