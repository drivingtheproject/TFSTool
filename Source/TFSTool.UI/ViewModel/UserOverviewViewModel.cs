using AutoMapper;
using ExternalTfsService;
using ExternalTfsService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TFSTool.Services.Services;
using TFSTool.Services.Services.Interfaces;
using TFSTool.UI.Core;
using TFSTool.UI.Model;

namespace TFSTool.UI.ViewModel
{
    public class UserOverviewViewModel : ObservableItem
    {

        private readonly ITfsUserService _userService;
        private readonly IExportServiceFactory _exportServiceFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<UserOverviewViewModel> _logger;
        private ObservableCollection<UserModel> _users;
        private UserModel _selectedUser;
        private string _statusMessgae;
        private bool _isEnabled = true;
       
        public UserOverviewViewModel(ITfsUserService userService, IExportServiceFactory exportServiceFactory, IMapper mapper, ILogger<UserOverviewViewModel> logger)
        {
            _userService = userService;
            _exportServiceFactory = exportServiceFactory;
            _mapper = mapper;
            _logger = logger;

            GetUsersCommand = new RelayCommand(GetUsers);
            ExportUsersCommand = new RelayCommand(ExportUsers, HasData);
            DeleteUsersCommand = new RelayCommand(DeleteUsers, IsSelected);
            ReverseUsersCommand = new RelayCommand(ReverseUsers, HasData);
        }
       
        public RelayCommand GetUsersCommand { get; set; }

        public RelayCommand ExportUsersCommand { get; set; }

        public RelayCommand ReverseUsersCommand { get; set; }

        public RelayCommand DeleteUsersCommand { get; set; }

        public ObservableCollection<UserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        public string StatusMessage
        {
            get { return _statusMessgae; }
            set
            {
                _statusMessgae = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        private async void GetUsers(object obj)
        {
            try
            {
                LogInfo($"Getting users...");
                IsEnabled = false;
                Users = null;
                var result = await _userService.GetAllUsersAsync().ToListAsync();
                Users = new ObservableCollection<UserModel>(_mapper.Map<IEnumerable<TfsUser>, IEnumerable<UserModel>>(result));
                LogInfo($"{result.Count} user(s) retrived successfully.");
            }
            catch (Exception ex)
            {
                LogInfo($"Error while getting users.Please refer error logs for more details");
                _logger.LogError($"GetUsers Error: {ex.Message}");

            }
            finally
            {
                IsEnabled = true;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private void DeleteUsers(object obj)
        {
            try
            {
                LogInfo($"Deleting selected users...");
                var items = obj as IList;
                if (items != null && Users != null)
                {
                    var users = new List<UserModel>(items.Cast<UserModel>());
                    foreach (var user in users)
                    {
                        Users.Remove(user);
                    }
                    LogInfo($"{users.Count} user(s) deleted successfully.");
                }

                SelectedUser = null;
            }
            catch (Exception ex)
            {
                LogInfo($"Error while deleting users.Please refer error logs for details");
                _logger.LogError($"DeleteUsers Error: {ex.Message}");

            }
        }

        private async void ExportUsers(object obj)
        {
            if (_users?.Any() == true)
            {
                IsEnabled = false;
                LogInfo($"Exporting users...");
                var fileName = $"{AppContext.BaseDirectory}export_{DateTime.Now:dd-MM-yyyy}.xml";
                var service = _exportServiceFactory.GetService(ExportType.Xml);
                var result = await service.ExportAsync<UserModel>(fileName, _users);

                if (result)
                {
                    LogInfo($"File {fileName} exported successfully.");
                }
                else
                {
                    LogInfo($"Xml {fileName} not exported.Please refer error logs for more details.");
                }
                IsEnabled = true;
            }
        }

        private async void ReverseUsers(object obj)
        {
            try
            {
                IsEnabled = false;
                await Task.Factory.StartNew(() =>
                {
                    Users = new ObservableCollection<UserModel>(_users.Reverse());
                });
            }
            catch (Exception ex)
            {
                LogInfo($"Error while reversing users.Please refer error logs for more details");
                _logger.LogError($"ReverseUsers Error: {ex.Message}");

            }
            IsEnabled = true;
        }

        private bool HasData(object arg)
        {
            return _users?.Any() == true;
        }

        private bool IsSelected(object arg)
        {
            return _selectedUser != null;
        }

        private void LogInfo(string message)
        {
            StatusMessage = message;
            _logger.LogInformation(message);
        }

    }
}
