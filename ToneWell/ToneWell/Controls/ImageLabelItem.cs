using FFImageLoading.Forms;
using FFImageLoading.Svg.Forms;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ToneWell.Helpers;
using Xamarin.Forms;

namespace ToneWell.Controls
{
    public class ImageLabelItem : ContentView
    {
        protected CachedImage icon;
        protected Label title;
        protected Grid grid;

        public ImageLabelItem()
        {
            Padding = 0;

            GestureRecognizers.Clear();
            GestureRecognizers.Add(new TapGestureRecognizer());

            icon = new CachedImage
            {
                WidthRequest = 36,
                HeightRequest = 36,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };

            title = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 16,
                TextColor = Colors.l_TextPrimary,
                VerticalOptions = LayoutOptions.Center
            };


            grid = new Grid
            {
                ColumnSpacing = 10,
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            grid.Children.Add(icon, 0, 0);
            grid.Children.Add(title, 1, 0);

            Content = grid;
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ImageLabelItem), default(ICommand));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(string), typeof(ImageLabelItem), default(string));

        public string CommandParameter
        {
            get { return (string)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(double), typeof(ImageLabelItem), default(double));

        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        public static readonly BindableProperty TextSizeProperty = BindableProperty.Create(nameof(TextSize), typeof(double), typeof(ImageLabelItem), default(double));

        public double TextSize
        {
            get { return (double)GetValue(TextSizeProperty); }
            set { SetValue(TextSizeProperty, value); }
        }

        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(ImageLabelItem), default(string));

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(ImageLabelItem), default(string));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IconProperty.PropertyName)
            {
                if (Icon != null && Icon.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
                    icon.Source = SvgImageSource.FromFile(Icon);
                else
                    icon.Source = Icon;
            }

            if(propertyName == TitleProperty.PropertyName)
            {
                title.Text = Title;
            }

            if(propertyName == TextSizeProperty.PropertyName)
            {
                title.FontSize = TextSize;
            }

            if (propertyName == IconSizeProperty.PropertyName)
            {
                icon.HeightRequest = IconSize;
                icon.WidthRequest = IconSize;
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
