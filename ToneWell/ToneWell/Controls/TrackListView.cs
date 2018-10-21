using FFImageLoading.Forms;
using FFImageLoading.Svg.Forms;
using Syncfusion.ListView.XForms;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ToneWell.Models;
using Xamarin.Forms;

namespace ToneWell.Controls
{
    public class TrackListView : ContentView
    {
        protected SfListView rootListView;
        protected DataTemplate dataTemplate;

        public TrackListView()
        {
            rootListView = new SfListView();
            rootListView.SelectionMode = SelectionMode.None;
            rootListView.Padding = 0;
            rootListView.Margin = 0;
            rootListView.DragStartMode = DragStartMode.OnDragIndicator;
            rootListView.AutoFitMode = AutoFitMode.Height;
            rootListView.DragDropController.UpdateSource = true;
            rootListView.ItemTapped += ItemTapped;

            dataTemplate = new DataTemplate(() =>
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
                    LoadingPlaceholder = "thumbnail.png",
                    ErrorPlaceholder = "thumbnail.png",
                    Source = "http://loremflickr.com/600/600/nature?filename=simple.jpg"
                };

                //_trackImage.SetBinding(CachedImage.SourceProperty, new Binding("ImagePath"));

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
                    TextColor = Color.FromHex("#414344"),
                    Margin = new Thickness(5, 0, 5, 0),
                };

                _labelTitle.SetBinding(Label.TextProperty, new Binding("Title"));

                var _labelSubTitle = new Label()
                {
                    FontSize = 12,
                    TextColor = Color.FromHex("#AAAFB3"),
                    Margin = new Thickness(5, 0, 5, 0),
                };

                _labelSubTitle.SetBinding(Label.TextProperty, new Binding("Artist"));

                var dragIndicatorView = new DragIndicatorView()
                {
                    ListView = rootListView,
                    Margin = new Thickness(5, 5, 5, 5),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Content = new CachedImage
                    {
                        HeightRequest = 35d,
                        WidthRequest = 35d,
                        Source = SvgImageSource.FromFile("more.svg"),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        Aspect = Aspect.Fill,
                    },
                };

                _labelRoot.Children.Add(_labelTitle);
                _labelRoot.Children.Add(_labelSubTitle);

                _root.Children.Add(_trackImage, 0, 0);
                _root.Children.Add(_labelRoot, 1, 0);
                _root.Children.Add(dragIndicatorView, 2, 0);

                return _root;

            });

            rootListView.ItemTemplate = dataTemplate;

            Content = rootListView;
        }

        private void ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (TapItemCommand.CanExecute(e))
                TapItemCommand.Execute(e);
        }

        public static readonly BindableProperty TapItemCommandProperty = BindableProperty.Create(nameof(TapItemCommand), typeof(ICommand), typeof(TrackListView), default(ICommand));

        public ICommand TapItemCommand
        {
            get { return (ICommand)GetValue(TapItemCommandProperty); }
            set { SetValue(TapItemCommandProperty, value); }
        }

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(ICollection<Track>), typeof(TrackListView), default(ICollection<Track>));

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
                rootListView.ItemsSource = Items;
                this.InvalidateMeasure();
            }
        }

    }
}
