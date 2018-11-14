using System.Runtime.CompilerServices;
using System.Windows.Input;
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

        public static readonly BindableProperty PlayOrPauseCommandProperty = BindableProperty.Create(nameof(PlayOrPauseCommand), typeof(ICommand), typeof(MiniPlayerView), default(ICommand));

        public ICommand PlayOrPauseCommand
        {
            get { return (ICommand)GetValue(PlayOrPauseCommandProperty); }
            set { SetValue(PlayOrPauseCommandProperty, value); }
        }

        public static readonly BindableProperty PreviousCommandProperty = BindableProperty.Create(nameof(PreviousCommand), typeof(ICommand), typeof(MiniPlayerView), default(ICommand));

        public ICommand PreviousCommand
        {
            get { return (ICommand)GetValue(PreviousCommandProperty); }
            set { SetValue(PreviousCommandProperty, value); }
        }

        public static readonly BindableProperty NextCommandProperty = BindableProperty.Create(nameof(NextCommand), typeof(ICommand), typeof(MiniPlayerView), default(ICommand));

        public ICommand NextCommand
        {
            get { return (ICommand)GetValue(NextCommandProperty); }
            set { SetValue(NextCommandProperty, value); }
        }


        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(double), typeof(MiniPlayerView), default(double));

        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(MiniPlayerView), default(string));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty ArtistProperty = BindableProperty.Create(nameof(Artist), typeof(string), typeof(MiniPlayerView), default(string));

        public string Artist
        {
            get { return (string)GetValue(ArtistProperty); }
            set { SetValue(ArtistProperty, value); }
        }

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(MiniPlayerView), default(string));

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly BindableProperty IsPlayingProperty = BindableProperty.Create(nameof(IsPlaying), typeof(bool), typeof(MiniPlayerView), default(bool));

        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            set { SetValue(IsPlayingProperty, value); }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName.Equals(ProgressProperty.PropertyName))
            {
                if(Progress >= 0)
                    progressBar.Progress = Progress;
            }

            if (propertyName.Equals(PlayOrPauseCommandProperty.PropertyName))
            {
                btnPlay.Command = PlayOrPauseCommand;
            }

            if (propertyName.Equals(PreviousCommandProperty.PropertyName))
            {
                btnPrevious.Command = PreviousCommand;
            }

            if (propertyName.Equals(NextCommandProperty.PropertyName))
            {
                btnNext.Command = NextCommand;
            }

            if (propertyName.Equals(IsPlayingProperty.PropertyName))
            {
                btnPlay.State = IsPlaying;
            }

            if (propertyName.Equals(TitleProperty.PropertyName))
            {
                lTitle.Text = Title;
            }

            if (propertyName.Equals(ArtistProperty.PropertyName))
            {
                lArtist.Text = Artist;
            }

            if (propertyName.Equals(ImageSourceProperty.PropertyName))
            {
                if(!string.IsNullOrEmpty(ImageSource))
                    image.Source = ImageSource;
            }

        }
    }
}
