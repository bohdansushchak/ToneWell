using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ToneWell.Controls;
using ToneWell.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomProgressBar), typeof(CustomProgressBarRenderer))]
namespace ToneWell.Droid.Renderers
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class CustomProgressBarRenderer : ProgressBarRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ProgressBar> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                var progressBar = e.NewElement as CustomProgressBar;

                
            }
        }

    }
#pragma warning restore CS0618 // Type or member is obsolete
}