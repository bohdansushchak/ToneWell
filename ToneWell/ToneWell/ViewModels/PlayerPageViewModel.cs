using Prism.Commands;
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
                LeftProgressSec = "2:04",
                Repeat = false,
                Shuffle = false,
            };

            MoreCommand = new DelegateCommand(moreAction);
            LikeCommand = new DelegateCommand(likeAction);
            PlayCommand = new DelegateCommand(playAction);
            RepeatCommand = new DelegateCommand(repeatAction);
            ShuffleCommand = new DelegateCommand(shuffleAction);
            NextCommand = new DelegateCommand(() => { });
            PreviousCommand = new DelegateCommand(() => { });
        }


        private Track track;
        public Track Track
        {
            get { return track; }
            set { track = value; }
        }


        private void moreAction()
        {

        } 

        private void likeAction()
        {

        }

        private void playAction()
        {

        }

        private void repeatAction()
        {

        }

        private void shuffleAction()
        {

        }

        public ICommand MoreCommand { get; set; }
        public ICommand LikeCommand { get; set; }
        public ICommand PlayCommand { get; set; }
        public ICommand RepeatCommand { get; set; }
        public ICommand ShuffleCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand PreviousCommand { get; set; }

    }
}
