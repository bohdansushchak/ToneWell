using FFImageLoading.Forms;
using Syncfusion.ListView.XForms;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TagLib.Matroska;
using ToneWell.Helpers;
using Xamarin.Forms;

namespace ToneWell.Controls
{
    public class SoungListView : ContentView
    {
        protected SfListView listView;

        public SoungListView()
        {
            Padding = 0;

            listView = new SfListView()
            {
                Padding = 0,
                Margin = 0,
                AutoFitMode = AutoFitMode.Height,
                DragStartMode = DragStartMode.None,
                SelectionMode = SelectionMode.None,
            };

            listView.ItemTapped += ItemTapped;

            var itemTemplate = new DataTemplate(() =>
            {
                var _root = new Grid()
                {
                    Padding = 0,
                    Margin = new Thickness(10, 5, 10, 5),
                    ColumnDefinitions = new ColumnDefinitionCollection()
                    {
                        new ColumnDefinition() { Width = GridLength.Auto },
                        new ColumnDefinition() { Width = GridLength.Star },
                        new ColumnDefinition() { Width = GridLength.Auto },
                    },
                };

                var _trackImage = new CachedImage()
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    HeightRequest = 60d,
                    WidthRequest = 60d,
                    Aspect = Aspect.AspectFill,
                    ErrorPlaceholder = "thumbnail_small.png",
                    CacheType = FFImageLoading.Cache.CacheType.None,
                };

                _trackImage.SetBinding(CachedImage.SourceProperty, new Binding("FilePath", converter: Converters.Converters.GetImageFromTrackConverter,
                    converterParameter: "thumbnail_small.png"));

                var _labelRoot = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Vertical,
                    Spacing = 0,
                };

                var _labelTitle = new Label
                {
                    FontSize = 14,
                    TextColor = Colors.l_TextPrimary,
                    Margin = new Thickness(5, 0, 5, 0),
                };

                _labelTitle.SetBinding(Label.TextProperty, new Binding("Title"));

                var _labelSubTitle = new Label()
                {
                    FontSize = 12,
                    TextColor = Colors.l_TextSubItem,
                    Margin = new Thickness(5, 0, 5, 0),
                };

                _labelSubTitle.SetBinding(Label.TextProperty, new Binding("Artist"));

                var indicator = new CachedImage
                {
                    HeightRequest = 35d,
                    WidthRequest = 35d,
                    Source = "more.svg",
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Aspect = Aspect.Fill,
                };

                
                _labelRoot.Children.Add(_labelTitle);
                _labelRoot.Children.Add(_labelSubTitle);

                _root.Children.Add(_trackImage, 0, 0);
                _root.Children.Add(_labelRoot, 1, 0);
                _root.Children.Add(indicator, 2, 0);

                return _root;
            });

            listView.ItemTemplate = itemTemplate;

            Content = listView;
        }

        private void ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (TapItemCommand.CanExecute(e))
                TapItemCommand.Execute(e);
        }

        public static readonly BindableProperty TapItemCommandProperty = BindableProperty.Create(nameof(TapItemCommand), typeof(ICommand), typeof(SoungListView), default(ICommand));

        public ICommand TapItemCommand
        {
            get { return (ICommand)GetValue(TapItemCommandProperty); }
            set { SetValue(TapItemCommandProperty, value); }
        }

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(ICollection<Track>), typeof(SoungListView), default(ICollection<Track>));

        public ICollection<Track> Items
        {
            get { return (ICollection<Track>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName.Equals(ItemsProperty.PropertyName))
            {
                listView.ItemsSource = Items;
                this.InvalidateMeasure();
            }
        }
    }
}
