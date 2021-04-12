using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using TFSTool.UI.Extensions;
using TFSTool.UI.ViewModel;

namespace TFSTool.UI.View
{
    /// <summary>
    /// Interaction logic for UserOverviewView.xaml
    /// </summary>
    public partial class UserOverviewView : UserControl
    {
        public UserOverviewView()
        {
            InitializeComponent();
            var serviceProvider = Application.Current.GetServiceProvider();
            DataContext = serviceProvider.GetRequiredService<UserOverviewViewModel>();
        }
    }
}
