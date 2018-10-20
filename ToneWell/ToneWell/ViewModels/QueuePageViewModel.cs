using Prism.Commands;
using Prism.Navigation;
using Syncfusion.ListView.XForms;
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

            reorderListCommand = new DelegateCommand<ItemDraggingEventArgs>(reorderList);
        }


        private void reorderList(ItemDraggingEventArgs e)
        {

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
