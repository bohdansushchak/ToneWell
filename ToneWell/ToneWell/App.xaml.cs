using DryIoc;
using Prism;
using Prism.Ioc;
using ToneWell.ViewModels;
using ToneWell.Views;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ToneWell
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */

        public static Container Container { get; private set; } = new Container();

        public App() : this(null)
        {}

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("CustomNavigationPage/QueuePage"); //NavigationPage/

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
        }
    }
}
