using FFImageLoading.Forms;
using FFImageLoading.Svg.Forms;
using Prism.Commands;
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
            
            dataTemplate = new DataTemplate(() =>
            {
                var _root = new Grid()
                {
                    Padding = 0,
                    ColumnDefinitions = new ColumnDefinitionCollection()
                    {
                        new ColumnDefinition() { Width = GridLength.Auto },
                        new ColumnDefinition() { Width = GridLength.Star },
                        new ColumnDefinition() { Width = GridLength.Auto },
                    },
                };

                var _trackImage = new CachedImage()
                {
                    HeightRequest = 50d,
                    WidthRequest = 50d,
                    Aspect = Aspect.AspectFill,

                };

                _trackImage.SetBinding(CachedImage.SourceProperty, new Binding("ImagePath"));

                var _labelRoot = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Vertical,
                };

                var _labelTitle = new Label
                {
                    FontSize = 14,
                    TextColor = Color.FromHex("#414344"),
                };
                _labelTitle.SetBinding(Label.TextProperty, new Binding("Title"));

                var _labelSubTitle = new Label()
                {
                    FontSize = 12,
                    TextColor = Color.FromHex("#AAAFB3"),
                };
                _labelSubTitle.SetBinding(Label.TextProperty, new Binding("Artist"));

                var dragIndicatorView = new DragIndicatorView()
                {
                    ListView = rootListView,
                    Content = new CachedImage
                    {
                        Source = SvgImageSource.FromFile("more.svg"),
                        VerticalOptions = LayoutOptions.FillAndExpand
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
