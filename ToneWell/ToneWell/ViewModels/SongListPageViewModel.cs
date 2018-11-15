using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ToneWell.Models;
using ToneWell.Services;

namespace ToneWell.ViewModels
{
    public class SongListPageViewModel : ViewModelBase
    {
        protected INavigationService navigationService;
        protected PlayerService playerService;

        public SongListPageViewModel(INavigationService navigationService)
         : base(navigationService)
        {
            Title = "All Songs";

            this.navigationService = navigationService;
            this.playerService = PlayerService.Instance;


            TapItemCommand = new DelegateCommand<Syncfusion.ListView.XForms.ItemTappedEventArgs>(tapItem);

            var trackse = playerService.initializeTracks();

            Tracks = new ObservableCollection<Track>(trackse);
        }

        private void tapItem(Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = (Track)e.ItemData;

            var parametr = new NavigationParameters();
            parametr.Add("item", item);

            navigationService.NavigateAsync("PlayerPage", parametr);
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

        public ICommand TapItemCommand { get; set; }
    }
}
