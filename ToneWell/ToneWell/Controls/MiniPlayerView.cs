using System.Runtime.CompilerServices;
using FFImageLoading.Forms;
using ToneWell.Helpers;
using Xamarin.Forms;

namespace ToneWell.Controls
{
    public class MiniPlayerView : ContentView
    {
        protected CachedImage image;
        protected Label lTitle;
        protected Label lArtist;
        protected ImageButton btnPrevious;
        protected ToggleButton btnPlay;
        protected ImageButton btnNext;
        protected ProgressBar progressBar;

        protected Grid root;

        public MiniPlayerView()
        {
            BackgroundColor = Colors.player_Background;

            root = new Grid();

            root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

            image = new CachedImage
            {
                Margin = new Thickness(10,10,0,10),
                HeightRequest = 70,
                WidthRequest = 70,
                ErrorPlaceholder = "thumbnail_small.png",
                Source = "thumbnail_small.png",
                CacheType = FFImageLoading.Cache.CacheType.None,
            };

            var controllButonGrid = new Grid()
            {
                ColumnSpacing = 10,
                Margin = new Thickness(5,15,0,0),
                Padding = 0,
            };

            controllButonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            controllButonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            controllButonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
        
            btnPlay = new ToggleButton
            {
                IconOff = "btn_play_small.svg",
                IconOn = "btn_pause_small.svg",
                IconSize = 38d,
            };

            btnNext = new ImageButton
            {
                Icon = "btn_next_small.svg",
                IconSize = 38d,
            };

            btnPrevious = new ImageButton
            {
                Icon = "btn_previous_small.svg",
                IconSize = 38d,
            };

            controllButonGrid.Children.Add(btnPrevious, 0, 0);
            controllButonGrid.Children.Add(btnPlay, 1, 0);
            controllButonGrid.Children.Add(btnNext, 2, 0);

            var labelStack = new StackLayout
            {
                Spacing = 0,
                Padding = 0,
                Margin = 0,

            };

            lTitle = new Label
            {
                FontSize = 20,
                TextColor = Colors.l_TextPrimary,
                Text = "This Girl"
            };

            lArtist = new Label
            {
                FontSize = 14,
                TextColor = Colors.l_TextSubItem,
                Text = "Kungs",
            };

            labelStack.Children.Add(lTitle);
            labelStack.Children.Add(lArtist);

            var labelButtonsGrid = new Grid()
            {
                ColumnSpacing = 0,
                Margin = new Thickness(10,10,15,10),
                Padding = 0,
            };

            labelButtonsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            labelButtonsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

            labelButtonsGrid.Children.Add(labelStack, 0, 0);
            labelButtonsGrid.Children.Add(controllButonGrid, 1, 0);

            var stack = new StackLayout()
            {
                Spacing = 0,
                Padding = 0,
                Margin = 0,
            };

            progressBar = new ProgressBar
            {
                Margin = new Thickness(10, 10, 15, 10),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Progress = 0.5d,
                ProgressColor = Colors.pb_ProgressColor,
            };

            stack.Children.Add(labelButtonsGrid);
            stack.Children.Add(progressBar);

            root.Children.Add(image, 0, 0);
            root.Children.Add(stack, 1, 0);
            
            Content = root;
        }






        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);


        }
    }
}
