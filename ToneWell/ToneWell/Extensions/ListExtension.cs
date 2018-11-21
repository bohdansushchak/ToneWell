using System.Collections.Generic;
using ToneWell.Database.Tables;
using ToneWell.Models;

namespace ToneWell.Extensions
{
    public static class ListExtension
    {
        public static List<TrackDb> ToTracksDb(this List<Track> tracks)
        {
            var tracksDb = tracks.ConvertAll(track => new TrackDb
            {
                Title = track.Title,
                Artist = track.Artist,
                FilePath = track.FilePath,
            });


            return tracksDb;
        }

        public static List<Track> ToTracks(this List<TrackDb> tracksDb)
        {
            var tracks = tracksDb.ConvertAll(track => new Track
            {
                Title = track.Title,
                Artist = track.Artist,
                FilePath = track.FilePath,
            });


            return tracks;
        }

    }
}
