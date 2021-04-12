using Microsoft.Extensions.Logging;
using TFSTool.UI.Core;

namespace TFSTool.UI.ViewModel
{
    public class HomeViewModel : ObservableItem
    {
        private readonly ILogger<HomeViewModel> _logger;
        public HomeViewModel(ILogger<HomeViewModel> logger)
        {
            _logger = logger;
        }
    }
}
