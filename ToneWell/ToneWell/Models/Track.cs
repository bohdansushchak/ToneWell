﻿using System;
using System.ComponentModel;

namespace ToneWell.Models
{
    public class Track : INotifyPropertyChanged
    {
        private String imagePath;
        private String title;
        private String artist;
        private int duration;
        private string progressSec;
        private string leftProgressSec;
        private bool repeat;
        private bool shuffle;
        private bool isLiked;



        public bool IsLiked
        {
            get { return isLiked; }
            set
            {
                isLiked = value;
                OnPropertyChanged("isLiked");
            }
        }

        public bool Repeat
        {
            get { return repeat; }
            set {
                repeat = value;
                OnPropertyChanged("repeat");
            }
        }

        public bool Shuffle
        {
            get { return shuffle; }
            set
            {
                shuffle = value;
                OnPropertyChanged("shuffle");
            }
        }

        public String ProgressSec
        {
            get { return progressSec; }
            set
            {
                progressSec = value;
                OnPropertyChanged("progressSec");
            }
        }

        public String LeftProgressSec
        {
            get { return leftProgressSec; }
            set
            {
                leftProgressSec = value;
                OnPropertyChanged("leftProgressSec");
            }
        }

        public String ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                OnPropertyChanged("imagePath");
            }
        }
        public String Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("title");
            }
        }

        public String Artist
        {
            get { return artist; }
            set
            {
                artist = value;
                OnPropertyChanged("artist");
            }
        }

        public int Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged("duration");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
