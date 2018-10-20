using Prism.Commands;
using Syncfusion.ListView.XForms;
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
            rootListView.ItemDragging += rootListView_ItemDragging;




            Content = rootListView;
        }

        public static readonly BindableProperty ReorderCommandProperty = BindableProperty.Create(nameof(ReorderCommand), typeof(DelegateCommand), typeof(TrackListView), default(DelegateCommand));

        public DelegateCommand ReorderCommand
        {
            get { return (DelegateCommand)GetValue(ReorderCommandProperty); }
            set { SetValue(ReorderCommandProperty, value); }
        }


        private void rootListView_ItemDragging(object sender, ItemDraggingEventArgs e)
        {
            if (e.Action == DragAction.Drop)
            {
                System.Diagnostics.Debug.WriteLine("Drop");
                if (ReorderCommand.CanExecute())
                    ReorderCommand.Execute();
            }
        }
    }
}
