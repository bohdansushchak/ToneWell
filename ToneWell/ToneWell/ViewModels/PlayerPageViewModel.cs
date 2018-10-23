using Prism.Navigation;
using System.Windows.Input;
using ToneWell.Models;

namespace ToneWell.ViewModels
{
    public class PlayerPageViewModel : ViewModelBase
    {
        public PlayerPageViewModel(INavigationService navigationService)
           : base(navigationService)
        {
            Title = "Player Page";

            Track = new Track()
            {
                Title = "misof",
                Artist = "Artist",
                ImagePath = "http://loremflickr.com/600/600/nature?filename=simple.jpg",
                ProgressSec = "1:21",
                LeftProgressSec = "2:04"
            };

        }


        private Track track;
        public Track Track
        {
            get { return track; }
            set { track = value; }
        }

        public ICommand MoreCommand { get; set; }
    }
}
