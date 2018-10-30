using DryIoc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ToneWell.Models;

namespace ToneWell.Services
{
    public class PlayerService : INotifyPropertyChanged
    {
        private static volatile PlayerService instance;
        private static object syncRoot = new Object();

        private bool repeatTracks;
        private bool shuffleTrakcs;

        private IFileService fileService;
        private IMyMediaPlayer mediaPlayer;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsPlaying => mediaPlayer.IsPlaying;

        public int Duration => mediaPlayer.Duration;

        public int CurrentPosition => mediaPlayer.CurrentPosition;


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

        public bool RepeatTracks
        {
            get { return repeatTracks; }
            set { repeatTracks = value;
                OnPropertyChanged("repeatTraks");
            }
        }

        public bool ShuffleTracks
        {
            get { return shuffleTrakcs; }
            set {
                shuffleTrakcs = value;
                OnPropertyChanged("shuffleTraks");
            }
        }

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

                    if (index == 0 && repeatTracks)
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

            var tracks = filePaths.ConvertAll(file => new Track
            {
                Title = file.Split('/').Last().Split('.').First(),
                Artist = file.Split('/').Last().Split('.').First().Split('_').FirstOrDefault(),
                FilePath = file,
                ImagePath = file,
            });

            Tracks = tracks;

            return tracks;
        }

        public void OnPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
