using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ToneWell.Models;
using ToneWell.Services;

namespace ToneWell.ViewModels
{
    public class QueuePageViewModel : ViewModelBase
    {
        protected INavigationService navigationService;
        protected PlayerService playerService;

        public QueuePageViewModel(INavigationService navigationService)
           : base(navigationService)
        {
            Title = "Queue";

            this.navigationService = navigationService;
            this.playerService = PlayerService.Instance;

            TapItemCommand = new DelegateCommand<Syncfusion.ListView.XForms.ItemTappedEventArgs>(tapItem);

            GoCommand = new DelegateCommand(() =>
            {
                var tracks = playerService.initializeTracks();

                Tracks = new ObservableCollection<Track>(tracks);
            });

        }

        private void tapItem(Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = (Track)e.ItemData;

            var parametr = new NavigationParameters();
            parametr.Add("item", item);

            navigationService.NavigateAsync("PlayerPage", parametr);
        }

        public ICommand TapItemCommand { get; set; }

        public ICommand GoCommand { get; set; }
    
        public ICommand PlayOrPauseCommand { get; set; }

        public ICommand NextCommand { get; set; }

        public ICommand PreviousCommand { get; set; }

        private void nextTrackAction()
        {
            playerService.PlayNextTrack();
            //Track = playerService.CurrentTrack;
        }

        private void previousTrackAction()
        {
            playerService.PlayPreviousTrack();
            //Track = playerService.CurrentTrack;
        }
        private void playOrPauseAction()
        {
            if (playerService.IsPlaying)
            {
                playerService.Pause();
            }
            else
            {
               // playerService.Play(Track);
            }

        }

        private ObservableCollection<Track> _tracks;
        public ObservableCollection<Track> Tracks
        {
            get { return _tracks; }
            set
            {
                _tracks = value;
                RaisePropertyChanged("Tracks");
            }
        }
    }
}
