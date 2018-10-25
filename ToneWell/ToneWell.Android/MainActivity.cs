using Android.App;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Prism;
using Prism.Ioc;
using ToneWell.Droid.Services;
using ToneWell.Services;

namespace ToneWell.Droid
{
    [Activity(Label = "ToneWell", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);


            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(false);

            LoadApplication(new App(new AndroidInitializer()));

        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IFileService, FileService>();
            containerRegistry.Register<IMyMediaPlayer, MyMediaPlayer>();
        }
    }
}

