using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;

namespace ToneWell.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {

        public SettingsPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "Settings";

            UpdateTracks = new DelegateCommand(updateTracks);
        }

        private void updateTracks()
        {

        } 

        public ICommand UpdateTracks { get; set; }
    }
}
