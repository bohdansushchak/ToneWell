using System;
using System.Collections.Generic;

using System.IO;
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


            //var fof = ((Prism.PrismApplicationBase)App.Current).Container.Resolve();

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

            try
            {

               

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

        }

        public List<Track> Tracks { get; set; }

    }
}
