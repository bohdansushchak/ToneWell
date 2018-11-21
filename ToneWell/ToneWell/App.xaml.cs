using DryIoc;
using Prism;
using Prism.Ioc;
using ToneWell.Services;
using ToneWell.ViewModels;
using ToneWell.Views;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ToneWell
{
    public partial class App
    {
        public static Container Container { get; private set; } = new Container();

        public App() : this(null)
        { }

        public App(IPlatformInitializer initializer) : base(initializer)
        { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("CustomNavigationPage/YourLibraryPage");

            //TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        /*
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            if (!e.Observed)
            {
                e.SetObserved();

                ShowCrashPage(e.Exception.Flatten().GetBaseException());
            }
        }

        private void ShowCrashPage(Exception exception)
        {
            Device.BeginInvokeOnMainThread(() => this.MainPage = new CrashPage(exception?.Message));
        }
        */
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CustomNavigationPage>();
            containerRegistry.RegisterForNavigation<QueuePage, QueuePageViewModel>();
            containerRegistry.RegisterForNavigation<PlayerPage, PlayerPageViewModel>();
            containerRegistry.RegisterForNavigation<YourLibraryPage, YourLibraryPageViewModel>();
            containerRegistry.RegisterForNavigation<SongListPage, SongListPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();

            Container.Register<ITrackManager, TrackManager>(Reuse.Singleton);
        }
    }
}
