using FFImageLoading.Forms;
using FFImageLoading.Svg.Forms;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ToneWell.Controls
{
    public class ImageButton : ContentView
    {
        protected readonly CachedImage _image;

        public ImageButton()
        {
            _image = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FadeAnimationEnabled = false,
            };

            Padding = 0;
            GestureRecognizers.Clear();
            GestureRecognizers.Add(new TapGestureRecognizer());

            HeightRequest = 36d;
            WidthRequest = 36d;
            IconSize = 25d;

            Content = _image;
        }

        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(ImageButton), default(string));

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly BindableProperty IconWidthProperty = BindableProperty.Create(nameof(IconWidth), typeof(double), typeof(ImageButton), default(double));

        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(double), typeof(ImageButton), default(double));

        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        public static readonly BindableProperty IconHeightProperty = BindableProperty.Create(nameof(IconHeight), typeof(double), typeof(ImageButton), default(double));

        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ImageButton), default(ICommand));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ImageButton), default(object));

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IconProperty.PropertyName)
            {
                if (Icon != null && Icon.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
                    _image.Source = SvgImageSource.FromFile(Icon);
                else
                    _image.Source = Icon;
            }

            if (propertyName == IconWidthProperty.PropertyName
                     || propertyName == IconHeightProperty.PropertyName)
            {
                _image.WidthRequest = IconWidth;
                _image.HeightRequest = IconHeight;
                WidthRequest = Math.Max(IconWidth, 36d);
                HeightRequest = Math.Max(IconHeight, 36d);
            }

            if (propertyName == IconSizeProperty.PropertyName)
            {
                if ((object)IconWidth != IconWidthProperty.DefaultValue)
                {
                    _image.WidthRequest = IconSize;
                    WidthRequest = Math.Max(IconSize, 36d);
                }

                if ((object)IconHeight != IconHeightProperty.DefaultValue)
                {
                    _image.HeightRequest = IconSize;
                    HeightRequest = Math.Max(IconSize, 36d);
                }
            }

            if (propertyName == CommandProperty.PropertyName)
            {
                (GestureRecognizers.First() as TapGestureRecognizer).Command = Command;
            }

            if (propertyName == CommandParameterProperty.PropertyName)
            {
                (GestureRecognizers.First() as TapGestureRecognizer).CommandParameter = CommandParameter;
            }
        }
    }
}
