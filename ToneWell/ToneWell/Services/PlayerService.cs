using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using ToneWell.Models;

namespace ToneWell.Services
{
    public class PlayerService
    {
        private static volatile PlayerService instance;
        private static object syncRoot = new Object();

        private bool repeatTracks;
        private bool shuffleTrakcs;

        private IFileService fileService;
        private IMyMediaPlayer mediaPlayer;

        public bool IsPlaying
        {
            get
            {
                return mediaPlayer.IsPlaying;
            }
        }

        public List<Track> Tracks { get; private set; }

        public Track CurrentTrack { get; private set; }

        private PlayerService()
        {
            Tracks = new List<Track>();
            CurrentTrack = new Track();

            fileService = App.Container.Resolve<IFileService>();
            mediaPlayer = App.Container.Resolve<IMyMediaPlayer>();

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

        public bool RepeatTraks
        {
            get { return repeatTracks; }
            set { repeatTracks = value; }
        }

        public bool ShuffleTraks
        {
            get { return shuffleTrakcs; }
            set { shuffleTrakcs = value; }
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
                mediaPlayer.Completion += delegate
                {
                    PlayNextTrack();
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

            if(++index >= Tracks.Count)
            {
                index = 0;
            }

            Play(Tracks[index]);
        }

        public void PlayPreviousTrack()
        {
            var index = Tracks.IndexOf(CurrentTrack);

            if (--index < 0)
            {
                index = Tracks.Count -1;
            }

            Play(Tracks[index]);
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
    }
}
