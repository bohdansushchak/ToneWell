using System;
using System.Collections.Generic;
using System.Text;
using ToneWell.Models;

namespace ToneWell.Services
{
    public class PlayerService
    {
        private static volatile PlayerService instance;
        private static object syncRoot = new Object();
        private bool repeatTracks;
        private bool shuffleTrakcs;

        private PlayerService()
        {
            Tracks = new List<Track>();

            FindAllMusicFiles();
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
        
        private void FindAllMusicFiles()
        {
            if (Tracks == null)
                Tracks = new List<Track>();

            for (int i = 0; i < 10; i++)
            {
                Tracks.Add(new Track
                {
                    Artist = "some artist " + i,
                    Title = "some title " + i,
                    ImagePath = "thumbnail.png",
                });
            }
        }

        public List<Track> Tracks { get; set; }
    }
}
