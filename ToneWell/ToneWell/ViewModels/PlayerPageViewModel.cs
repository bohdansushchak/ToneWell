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
            Track = playerService.CurrentTrack;

            MoreCommand = new DelegateCommand(moreAction);
            LikeCommand = new DelegateCommand(likeAction);
            PlayOrPauseCommand = new DelegateCommand(playOrPauseAction);
            RepeatCommand = new DelegateCommand(repeatAction);
            ShuffleCommand = new DelegateCommand(shuffleAction);
            NextCommand = new DelegateCommand(nextTrackAction);
            PreviousCommand = new DelegateCommand(previousTrackAction);

        }

        private Track track;
        public Track Track
        {
            get { return track; }
            set
            {

                track = value;
                RaisePropertyChanged("Track");
            }
        }

        private void moreAction()
        {

        }

        private void likeAction()
        {

        }

        private void playOrPauseAction()
        {
            if (playerService.IsPlaying)
            {
                playerService.Pause();
            }
            else
            {
                playerService.Play(Track);
            }

        }

        private void repeatAction()
        {

        }

        private void shuffleAction()
        {

        }

        private void nextTrackAction()
        {
            playerService.PlayNextTrack();
            Track = playerService.CurrentTrack;
        }

        private void previousTrackAction()
        {
            playerService.PlayPreviousTrack();
            Track = playerService.CurrentTrack;
        }

        public ICommand MoreCommand { get; set; }
        public ICommand LikeCommand { get; set; }
        public ICommand PlayOrPauseCommand { get; set; }
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
