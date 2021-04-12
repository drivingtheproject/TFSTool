using System.Windows;
using System.Windows.Input;
using TFSTool.UI.ViewModel;

namespace TFSTool.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel model)
        {
            InitializeComponent();                     
            DataContext = model;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
