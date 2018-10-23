using FFImageLoading.Forms;
using FFImageLoading.Svg.Forms;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ToneWell.Controls
{
    public class ToggleButton : ContentView
    {
        protected CachedImage Image;

        public ToggleButton()
        {
            Image = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FadeAnimationEnabled = false,
            };

            State = false;

            Padding = 0;
            GestureRecognizers.Clear();
            GestureRecognizers.Add(new TapGestureRecognizer());
            (GestureRecognizers.First() as TapGestureRecognizer).Tapped += tap;

            HeightRequest = 36d;
            WidthRequest = 36d;
            IconSize = 25d;

            Content = Image;
        }


        void tap(object sender, object e)
        {
            if (State)
            {
                SetValue(StateProperty, false);
                Image.Source = !string.IsNullOrEmpty(IconOff) ? SvgImageSource.FromFile(IconOff) : SvgImageSource.FromFile("more.svg");
            }
            else
            {
                SetValue(StateProperty, true);
                Image.Source = !string.IsNullOrEmpty(IconOn) ? SvgImageSource.FromFile(IconOn) : SvgImageSource.FromFile("more.svg");
            }
        }

        public static readonly BindableProperty IconOnProperty = BindableProperty.Create(nameof(IconOn), typeof(string), typeof(ToggleButton), default(string));

        public string IconOn
        {
            get { return (string)GetValue(IconOnProperty); }
            set { SetValue(IconOnProperty, value); }
        }

        public static readonly BindableProperty IconOffProperty = BindableProperty.Create(nameof(IconOff), typeof(string), typeof(ToggleButton), default(string));

        public string IconOff
        {
            get { return (string)GetValue(IconOffProperty); }
            set { SetValue(IconOffProperty, value); }
        }

        public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(double), typeof(ToggleButton), default(double));

        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        public static readonly BindableProperty StateProperty = BindableProperty.Create(nameof(State), typeof(bool), typeof(ToggleButton), default(bool));

        public bool State
        {
            get { return (bool)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ToggleButton), default(ICommand));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == StateProperty.PropertyName)
            {
                if (State)
                {
                    Image.Source = !string.IsNullOrEmpty(IconOn) ? SvgImageSource.FromFile(IconOn) : SvgImageSource.FromFile("more.svg");

                }
                else
                {
                    Image.Source = !string.IsNullOrEmpty(IconOff) ? SvgImageSource.FromFile(IconOff) : SvgImageSource.FromFile("more.svg");
                }
            }

            if(propertyName == IconOnProperty.PropertyName)
            {
                if(State)
                    Image.Source = !string.IsNullOrEmpty(IconOn) ? SvgImageSource.FromFile(IconOn) : SvgImageSource.FromFile("more.svg");
            }

            if (propertyName == IconOffProperty.PropertyName)
            {
                if (!State)
                    Image.Source = !string.IsNullOrEmpty(IconOff) ? SvgImageSource.FromFile(IconOff) : SvgImageSource.FromFile("more.svg");
            }

            if(propertyName == CommandProperty.PropertyName)
            {
                (GestureRecognizers.First() as TapGestureRecognizer).Command = Command;
            }

            if (propertyName == IconSizeProperty.PropertyName)
            {

                Image.WidthRequest = IconSize;
                WidthRequest = Math.Max(IconSize, 36d);

                Image.HeightRequest = IconSize;
                HeightRequest = Math.Max(IconSize, 36d);

            }
        }
    }
}
