using System.Windows;

namespace WpfAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AsyncViewModel viewModel = new AsyncViewModel();
            this.DataContext = viewModel;
        }
    }
}