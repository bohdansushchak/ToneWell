using System;
using System.ComponentModel;

namespace ToneWell.Models
{
    public class Track : INotifyPropertyChanged
    {
        private string imagePath;
        private string title;
        private string artist;
        private string filePath;
        private int duration;
        private string progressSec;
        private string leftProgressSec;
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

        public string FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                OnPropertyChanged("filePath");
            }
        }

        public string ProgressSec
        {
            get { return progressSec; }
            set
            {
                progressSec = value;
                OnPropertyChanged("progressSec");
            }
        }

        public string LeftProgressSec
        {
            get { return leftProgressSec; }
            set
            {
                leftProgressSec = value;
                OnPropertyChanged("leftProgressSec");
            }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                OnPropertyChanged("imagePath");
            }
        }
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("title");
            }
        }

        public string Artist
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
