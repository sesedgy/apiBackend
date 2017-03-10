using GeneralModel.ViewModel;

namespace GeneralModel.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
        }
    }
}
