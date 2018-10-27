using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public List<Track> Tracks { get; set; }

        private PlayerService()
        {            
            Tracks = new List<Track>();

            fileService = App.Container.Resolve<IFileService>();
            mediaPlayer = App.Container.Resolve<IMyMediaPlayer>();

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

        public List<Track> InitializeTracks()
        {
            var filePaths = fileService.FindAllMp3Files();

            var tracks = filePaths.ConvertAll(file => new Track
            {
                Title = file.Split('/').Last().Split('.').First(),
                FilePath = file,
                ImagePath = file,
            });

            return tracks;
        }
    }
}
