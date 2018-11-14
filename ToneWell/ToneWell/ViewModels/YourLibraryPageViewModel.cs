using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;
using ToneWell.Services;

namespace ToneWell.ViewModels
{
    public class YourLibraryPageViewModel : ViewModelBase
    {
        protected INavigationService navigationService;
        protected PlayerService playerService;

        public YourLibraryPageViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            Title = "Your Library";

            this.navigationService = navigationService;
            this.playerService = PlayerService.Instance;

            TapItemCommand = new DelegateCommand<string>(tapItem);
            SettingsCommand = new DelegateCommand(() =>
            {

            });

        }

        void tapItem(string param)
        {
            switch (param)
            {
                case "PlayList":
                    {
                        break;
                    }

                case "Songs":
                    {
                        navigationService.NavigateAsync("SongListPage");
                        break;
                    }

                case "Artists":
                    {
                        navigationService.NavigateAsync("QueuePage");
                        break;
                    }
            }

        }


        public ICommand TapItemCommand { get; set; }

        public ICommand SettingsCommand { get; set; }
    }
}
