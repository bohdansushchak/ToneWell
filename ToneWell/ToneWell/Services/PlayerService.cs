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

               
                //string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "*.mp3");

                string[] di = Directory.GetDirectories("/data/user/0");

                System.Diagnostics.Debug.WriteLine(di.ToString());
                System.Diagnostics.Debug.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));

                /*files.ToList().ConvertAll(file => new Track()
                {
                    FilePath = file,
                });
                */
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            /*
            for (int i = 0; i < 10; i++)
            {
                Tracks.Add(new Track
                {
                    Artist = "some artist " + i,
                    Title = "some title " + i,
                    ImagePath = "thumbnail.png",
                });
            }
            */
        }

        public List<Track> Tracks { get; set; }

    }
}
