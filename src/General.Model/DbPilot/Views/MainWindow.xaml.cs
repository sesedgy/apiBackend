using System.Windows.Controls;
using GeneralModel.ViewModel;

namespace GeneralModel.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
            ScrollViewer viewer = new ScrollViewer();
            viewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        }
    }
}
