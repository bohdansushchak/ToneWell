using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ToneWell.Helpers;
using ToneWell.Models;

namespace ToneWell.Services
{
    public class PlayerService
    {
        private static volatile PlayerService instance;
        private static object syncRoot = new Object();

        private IFileService fileService;
        private IMyMediaPlayer mediaPlayer;

        private Thread updateProgressThread;

        public bool IsPlaying => mediaPlayer.IsPlaying;

        public int Duration => mediaPlayer.Duration;

        public int CurrentPosition => mediaPlayer.CurrentPosition;

        public event EventHandler<PlayerArgs> UpdateProgress;

        public List<Track> Tracks { get; private set; }

        public Track CurrentTrack { get; private set; }

        private PlayerService()
        {
            Tracks = new List<Track>();
            CurrentTrack = new Track();

            fileService = App.Container.Resolve<IFileService>();
            mediaPlayer = App.Container.Resolve<IMyMediaPlayer>();

            RepeatTracks = false;
            ShuffleTracks = false;

            updateProgressThread = new Thread(updateProgres);
            updateProgressThread.IsBackground = true;
            updateProgressThread.Start();

            Tracks = initializeTracks();
        }

        public static PlayerService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new PlayerService();
                    }
                }

                return instance;

            }
        }

        private void updateProgres()
        {
            while (true)
            {

                TimeSpan currTime = TimeSpan.FromMilliseconds(CurrentPosition);
                TimeSpan leftTime = TimeSpan.FromMilliseconds(Duration - CurrentPosition);

                var CurrentPositionSec = currTime.ToString(@"m\:ss");
                var LeftProgressSec = string.Format("-{0}", leftTime.ToString(@"m\:ss"));

                double currentPosition = CurrentPosition;
                double duration = Duration;

                var args = new PlayerArgs
                {
                    CurrentPositionSec = currTime.ToString(@"m\:ss"),
                    LeftProgressSec = string.Format("-{0}", leftTime.ToString(@"m\:ss")),
                    ProgressDegree = currentPosition / duration
                };

                UpdateProgress?.Invoke(this, args);

                Thread.Sleep(1000);
            }
        }

        public bool RepeatTracks { get; set; }

        public bool ShuffleTracks { get; set; }

        public void Play(Track track)
        {
            if (CurrentTrack.Equals(track))
            {
                if (!mediaPlayer.IsPlaying)
                    mediaPlayer.Resume();
            }
            else
            {
                CurrentTrack = track;

                mediaPlayer.StartPlayer(track.FilePath);
                mediaPlayer.Resume();
                mediaPlayer.Completion += delegate
                {

                    var index = Tracks.IndexOf(CurrentTrack);

                    if (++index >= Tracks.Count)
                    {
                        index = 0;
                    }

                    if (index == 0 && RepeatTracks)
                    {
                        Play(Tracks[index]);
                    }
                    else if (index > 0)
                    {
                        Play(Tracks[index]);
                    }
                };
            }
        }

        public void Pause()
        {
            mediaPlayer.Pause();
        }

        public void PlayNextTrack()
        {
            var index = Tracks.IndexOf(CurrentTrack);

            if (++index >= Tracks.Count)
            {
                index = 0;
            }

            if (mediaPlayer.IsPlaying)
            {
                Play(Tracks[index]);
            }

            else
            {
                CurrentTrack = Tracks[index];
                mediaPlayer.StartPlayer(CurrentTrack.FilePath);
            }

        }

        public void PlayPreviousTrack()
        {
            var index = Tracks.IndexOf(CurrentTrack);

            if (--index < 0)
            {
                index = Tracks.Count - 1;
            }

            if (mediaPlayer.IsPlaying)
                Play(Tracks[index]);
            else
            {
                CurrentTrack = Tracks[index];
                mediaPlayer.StartPlayer(CurrentTrack.FilePath);
            }

        }

        public List<Track> initializeTracks()
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

                    /*
                    var mStream = new MemoryStream();
                    var firstPicture = tagFile.Tag.Pictures.FirstOrDefault();
                    if (firstPicture != null)
                    {
                        byte[] pData = firstPicture.Data.Data;
                        mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
                        mStream.Dispose();
                    }
                    */

                    if (string.IsNullOrEmpty(title))
                    {
                        title = file.Split('/').Last().Split('.').First();
                    }

                    track.Title = title;
                    track.Artist = artist;
                    
                }
                catch(Exception e)
                {
                    track.Title = file.Split('/').Last().Split('.').First();                    
                }
                finally
                {
                    track.FilePath = file;
                }

                tracks.Add(track);
            }

            Tracks = tracks;

            return tracks;
        }
    }
}
