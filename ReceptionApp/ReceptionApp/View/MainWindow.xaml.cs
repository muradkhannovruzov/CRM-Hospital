using ReceptionApp.ViewModel;
using System.Windows;

namespace ReceptionApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.container.GetInstance<MainViewModel>();
        }               
    }
}
