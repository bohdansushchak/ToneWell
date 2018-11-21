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

        private IMyMediaPlayer mediaPlayer;
        private ITrackManager trackManager;

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

            mediaPlayer = App.Container.Resolve<IMyMediaPlayer>();
            trackManager = App.Container.Resolve<ITrackManager>();

            RepeatTracks = false;
            ShuffleTracks = false;

            updateProgressThread = new Thread(updateProgres);
            updateProgressThread.IsBackground = true;
            updateProgressThread.Start();

            Tracks = trackManager.GetAllTracks().GetAwaiter().GetResult();
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
    }
}
