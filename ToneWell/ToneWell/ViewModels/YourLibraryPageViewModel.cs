using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;

namespace ToneWell.ViewModels
{
    public class YourLibraryPageViewModel : ViewModelBase
    {

        public YourLibraryPageViewModel(INavigationService navigationService)
          : base(navigationService)
        {
            Title = "YourLibrary";

            TapItemCommand = new DelegateCommand<string>(tapItem);
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
                        break;
                    }

                case "Artists":
                    {
                        break;
                    }
            }

        }


        public ICommand TapItemCommand { get; set; }
    }
}
