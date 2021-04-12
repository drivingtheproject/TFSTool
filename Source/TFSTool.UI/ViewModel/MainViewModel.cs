using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TFSTool.UI.Core;

namespace TFSTool.UI.ViewModel
{
    public class MainViewModel : ObservableItem
    {
        private ObservableItem _selectedView;
        private int _selectedIndex;
        private readonly List<ObservableItem> _menuItemViews;
        private readonly ILogger<MainViewModel> _logger;

        public MainViewModel(ILogger<MainViewModel> logger, HomeViewModel homeView, UserOverviewViewModel userOverviewView)
        {
            _logger = logger;
            _menuItemViews = new List<ObservableItem> { homeView, userOverviewView };
            SelectedIndex = 0;
            CloseApplicationCommand = new RelayCommand(o =>
            {
                Environment.Exit(0);
            });
        }

        public RelayCommand CloseApplicationCommand { get; set; }

        public ObservableItem SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                OnPropertyChanged();
            }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                SelectedView = _menuItemViews[_selectedIndex];
                OnPropertyChanged();
            }
        }
    }
}
