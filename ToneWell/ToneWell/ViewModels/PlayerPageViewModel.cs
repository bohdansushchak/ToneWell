using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using ToneWell.Models;
using ToneWell.Services;

namespace ToneWell.ViewModels
{
    public class PlayerPageViewModel : ViewModelBase, INavigatedAware
    {
        protected PlayerService playerService;

        public PlayerPageViewModel(INavigationService navigationService)
           : base(navigationService)
        {
            Title = "Player Page";
            playerService = PlayerService.Instance;

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

            playerService.Play(Track);
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Track = parameters["item"] as Track;

            playerService.Play(Track);
        }

    }
}
