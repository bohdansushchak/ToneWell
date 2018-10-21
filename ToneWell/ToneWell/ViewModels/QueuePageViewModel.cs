using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ToneWell.Models;

namespace ToneWell.ViewModels
{
    public class QueuePageViewModel : ViewModelBase
    {
        public QueuePageViewModel(INavigationService navigationService)
           : base(navigationService)
        {
            Title = "Queue";

            reorderListCommand = new DelegateCommand(reorderList);


            Tracks = new ObservableCollection<Track>();

            for (int i = 0; i < 10; i++)
            {
                Tracks.Add(new Track
                {
                    Artist = "some artist " + i,
                    Title = "some title " + i,
                });
            }
        }

        private void reorderList()
        {
            foreach (var track in Tracks)
            {
                System.Diagnostics.Debug.WriteLine(track.Title);
            }

        }

        private ICommand reorderListCommand;
        public ICommand ReorderListCommand
        {
            get { return reorderListCommand; }
            set { reorderListCommand = value; }
        }

        private ObservableCollection<Track> tracks;
        public ObservableCollection<Track> Tracks
        {
            get { return tracks; }
            set { tracks = value; }
        }
    }
}
