using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;

namespace ToneWell.ViewModels
{
    public class YourLibraryPageViewModel : ViewModelBase
    {
        protected INavigationService navigationService;

        public YourLibraryPageViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            Title = "YourLibrary";
            this.navigationService = navigationService;
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
                        break;
                    }
            }

        }


        public ICommand TapItemCommand { get; set; }

        public ICommand SettingsCommand { get; set; }
    }
}
