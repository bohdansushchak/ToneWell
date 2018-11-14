using System;
using System.ComponentModel;

namespace ToneWell.Models
{
    public class Track : INotifyPropertyChanged
    {
        private string title;
        private string artist;
        private string filePath;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
