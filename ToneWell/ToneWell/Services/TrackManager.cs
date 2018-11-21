using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToneWell.Database;
using ToneWell.Extensions;
using ToneWell.Models;

namespace ToneWell.Services
{
    public class TrackManager : ITrackManager
    {
        private DatabaseContext dbContext;
        private IFileService fileService;

        public TrackManager()
        {
            fileService = App.Container.Resolve<IFileService>();
            dbContext = new DatabaseContext();
        }

        public void SaveTracks(List<Track> tracks)
        {
            if (dbContext.Tracks.Count() != 0)
            {
                dbContext.Tracks.Clear();
            }

            var dbTracks = tracks.ToTracksDb();

            dbContext.Tracks.AddRangeAsync(dbTracks);
            dbContext.SaveChangesAsync();
        }

        public async Task<List<Track>> GetAllTracks()
        {
            var tracks = new List<Track>();

            if (dbContext.Tracks.Count() == 0)
            {
                tracks = await FindAllTracks();

                dbContext.Tracks.AddRange(tracks.ToTracksDb());
                dbContext.SaveChanges();

            }
            else
            {
                tracks = dbContext.Tracks.ToList().ToTracks();
            }

            return tracks;

        }

        private async Task<List<Track>> FindAllTracks()
        {
            var filePaths = fileService.FindAllMp3Files();

            var tracks = new List<Track>();

            foreach (var file in filePaths)
            {
                var track = new Track();

                try
                {
                    TagLib.File tagFile = TagLib.File.Create(file);

                    string artist = tagFile.Tag.FirstAlbumArtist;
                    string title = tagFile.Tag.Title;

                    if (string.IsNullOrEmpty(title))
                    {
                        title = file.Split('/').Last().Split('.').First();
                    }

                    track.Title = title;
                    track.Artist = artist;

                }
                catch (Exception e)
                {
                    track.Title = file.Split('/').Last().Split('.').First();
                }
                finally
                {
                    track.FilePath = file;
                }

                tracks.Add(track);
            }

            return tracks;
        }

    }
}
