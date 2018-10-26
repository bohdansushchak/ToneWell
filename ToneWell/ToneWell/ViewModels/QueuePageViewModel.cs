using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using ToneWell.Models;
using ToneWell.Services;

namespace ToneWell.ViewModels
{
    public class QueuePageViewModel : ViewModelBase
    {
        protected INavigationService navigationService;
        protected PlayerService playerService;

        public QueuePageViewModel(INavigationService navigationService, IFileService fileService)
           : base(navigationService)
        {
            Title = "Queue";

            this.navigationService = navigationService;
            this.playerService = PlayerService.Instance;

            tapItemCommand = new DelegateCommand<Syncfusion.ListView.XForms.ItemTappedEventArgs>(tapItem);

            var files = fileService.FindAllMp3Files();

            Tracks = new ObservableCollection<Track>(files.ConvertAll(file => new Track
            {
                Title = file.Split('/').Last().Split('.').First(),
                FilePath = file,
                ImagePath = file,
            }));
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
