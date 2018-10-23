using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ToneWell.Models;

namespace ToneWell.ViewModels
{
    public class QueuePageViewModel : ViewModelBase
    {
        protected INavigationService navigationService;

        public QueuePageViewModel(INavigationService navigationService)
           : base(navigationService)
        {

            this.navigationService = navigationService;

            Title = "Queue";

            tapItemCommand = new DelegateCommand<Syncfusion.ListView.XForms.ItemTappedEventArgs>(tapItem);

            Tracks = new ObservableCollection<Track>();

            for (int i = 0; i < 10; i++)
            {
                Tracks.Add(new Track
                {
                    Artist = "some artist " + i,
                    Title = "some title " + i,
                    ImagePath = "thumbnail.png",
                });
            }
        }

        private void tapItem(Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = (Track)e.ItemData;
            System.Diagnostics.Debug.WriteLine(item.Title);

            var p = new NavigationParameters();

            navigationService.NavigateAsync("PlayerPage", p);
        }

        private ICommand tapItemCommand;
        public ICommand TapItemCommand
        {
            get { return tapItemCommand; }
            set { tapItemCommand = value; }
        }

        private ObservableCollection<Track> tracks;
        public ObservableCollection<Track> Tracks
        {
            get { return tracks; }
            set { tracks = value; }
        }
    }
}
