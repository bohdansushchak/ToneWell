using Prism.Navigation;

namespace ToneWell.ViewModels
{
    public class SongListPageViewModel : ViewModelBase
    {
        public SongListPageViewModel(INavigationService navigationService)
         : base(navigationService)
        {
            Title = "All Songs";

            

        }
    }
}
