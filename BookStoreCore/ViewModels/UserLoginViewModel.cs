﻿using BookStoreCore.Commands;
using BookStoreCore.Services;
using BookStoreCore.Stores;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookStoreCore.ViewModels
{
    public class UserLoginViewModel : ViewModelBase
    {
        private string _login = null!;
        private string _password = null!;
        private string _name = null!;

        private NavigationService _adminNavigationService;
        private NavigationService _userNavigationService;
        private NavigationService _createUserNavigationService;
        private UserService _userService { get; set; }

        public string Login {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(this.Login));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(this.Password));
            }
        }

        public UserLoginViewModel(NavigationStore navigationStore)
        {
            this._adminNavigationService = new NavigationService(navigationStore, () => new AdminMainViewModel(navigationStore));
            this._userNavigationService = new NavigationService(navigationStore, () => new UserMainViewModel(navigationStore));
            this._createUserNavigationService = new NavigationService(navigationStore, () => new CreateUserViewModel(this._userNavigationService));
            this.CreateNewAccount = new NavigationCommand(this._createUserNavigationService);
            this._userService = new UserService(ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString);
            
        }

        private bool IsValid()
        {
            if (string.IsNullOrEmpty(this.Login) || string.IsNullOrWhiteSpace(this.Login))
                return false;
            if (string.IsNullOrEmpty(this.Password) || string.IsNullOrWhiteSpace(this.Password))
                return false;
            if (this.Password.Length < 8)
                return false;
            return true;
        }

        public ICommand CreateNewAccount { get; }

        public ICommand LogToAccaunt
        {
            get => new RelayCommand(() =>
            {
                if (!IsValid())
                {
                    MessageBox.Show("The input is incorrect. Fields must be filled and the password mush consist of at least 8 characters",
                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var tempUser = this._userService.GetAll()
                .FirstOrDefault(x => x.Login == this.Login && x.Password == this.Password);
                if (tempUser != null)
                {
                    User user = User.GetInstance();
                    user.Name = tempUser.Name;
                    user.Login = tempUser.Login;
                    user.Password = tempUser.Password;
                    user.IsAdmin = tempUser.IsAdmin;

                    if (user.IsAdmin)
                        this._adminNavigationService.Navigate();
                    else
                        this._userNavigationService.Navigate();
                }
                else
                {
                    MessageBox.Show("No user found with this login and password", "Warning",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            });
        }
    }
}
