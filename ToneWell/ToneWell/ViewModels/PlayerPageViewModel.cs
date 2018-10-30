using Prism.Commands;
using Prism.Navigation;
using System;
using System.Threading;
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

            CurrentPositionSec = "0:00";
            LeftProgressSec = "-0:00";
            ProgressDegree = 0;

            RepeatTracks = playerService.RepeatTracks;
            ShuffleTracks = playerService.ShuffleTracks;


            MoreCommand = new DelegateCommand(moreAction);
            LikeCommand = new DelegateCommand(likeAction);
            PlayOrPauseCommand = new DelegateCommand(playOrPauseAction);
            RepeatCommand = new DelegateCommand(repeatAction);
            ShuffleCommand = new DelegateCommand(shuffleAction);
            NextCommand = new DelegateCommand(nextTrackAction);
            PreviousCommand = new DelegateCommand(previousTrackAction);

            Thread thread = new Thread(updateProgres);
            thread.Start();
        }

        private void updateProgres()
        {
            while (true)
            {

                TimeSpan currTime = TimeSpan.FromMilliseconds(playerService.CurrentPosition);
                TimeSpan leftTime = TimeSpan.FromMilliseconds(playerService.Duration - playerService.CurrentPosition);

                CurrentPositionSec = currTime.ToString(@"m\:ss");
                LeftProgressSec = string.Format("-{0}", leftTime.ToString(@"m\:ss"));

                double currentPosition = playerService.CurrentPosition;
                double duration = playerService.Duration;

                ProgressDegree = currentPosition / duration;

                Thread.Sleep(1000);
            }
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
            playerService.RepeatTracks = !playerService.RepeatTracks;
            RepeatTracks = playerService.RepeatTracks;
        }

        private void shuffleAction()
        {
            playerService.ShuffleTracks = !playerService.ShuffleTracks;
            ShuffleTracks = playerService.ShuffleTracks;
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



        private bool repeatTracks;
        private bool shuffleTracks;
        private string currentPositionSec;
        private string leftProgressSec;
        private double progressDegree;

        public string CurrentPositionSec
        {
            get
            {
                return currentPositionSec;
            }
            set
            {
                currentPositionSec = value;
                RaisePropertyChanged("CurrentPositionSec");

            }
        }

        public bool RepeatTracks
        {
            get { return repeatTracks; }
            set
            {
                repeatTracks = value;
                RaisePropertyChanged("RepeatTracks");
            }
        }

        public bool ShuffleTracks
        {
            get { return shuffleTracks; }
            set
            {
                shuffleTracks = value;
                RaisePropertyChanged("ShuffleTracks");
            }
        }

        public string LeftProgressSec
        {
            get
            {
                return leftProgressSec;
            }
            set
            {
                leftProgressSec = value;
                RaisePropertyChanged("LeftProgressSec");

            }
        }

        public double ProgressDegree
        {
            get { return progressDegree; }
            set
            {
                progressDegree = value;
                RaisePropertyChanged("ProgressDegree");
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Track = parameters["item"] as Track;

            playerService.Play(Track);
        }

    }
}
