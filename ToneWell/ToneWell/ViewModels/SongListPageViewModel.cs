using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using TagLib.Matroska;

namespace ToneWell.ViewModels
{
    public class SongListPageViewModel : ViewModelBase
    {
        protected INavigationService navigationService;

        public SongListPageViewModel(INavigationService navigationService)
         : base(navigationService)
        {
            Title = "All Songs";
            this.navigationService = navigationService;

            TapItemCommand = new DelegateCommand<Syncfusion.ListView.XForms.ItemTappedEventArgs>(tapItem);
        }

        private void tapItem(Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = (Track)e.ItemData;

            var parametr = new NavigationParameters();
            parametr.Add("item", item);

            navigationService.NavigateAsync("PlayerPage", parametr);
        }

        public ICommand TapItemCommand { get; set; }
    }
}
