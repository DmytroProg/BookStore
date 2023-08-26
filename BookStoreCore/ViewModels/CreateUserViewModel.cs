using BookStoreCore.Services;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookStoreCore.ViewModels
{
    public class CreateUserViewModel : ViewModelBase
    {
        private string _login = null!;
        private string _password = null!;
        private string _name = null!;

        private NavigationService _userNavigationService;
        private UserService _userService { get; set; }

        public string Login
        {
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

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(this.Name));
            }
        }

        public CreateUserViewModel(NavigationService userNavigationService)
        {
            this._userNavigationService = userNavigationService;
            this._userService = new UserService(ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString);

        }

        private bool IsValid()
        {
            if (string.IsNullOrEmpty(this.Name) || string.IsNullOrWhiteSpace(this.Name))
                return false;
            if (string.IsNullOrEmpty(this.Login) || string.IsNullOrWhiteSpace(this.Login))
                return false;
            if (string.IsNullOrEmpty(this.Password) || string.IsNullOrWhiteSpace(this.Password))
                return false;
            if (this.Password.Length < 8)
                return false;
            return true;
        }

        public ICommand CreateNewAccount
        {
            get => new RelayCommand(() =>
            {
                if (!IsValid())
                {
                    MessageBox.Show("The input is incorrect. Fields must be filled and the password mush consist of at least 8 characters",
                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (this._userService.LoginExists(this.Login))
                {
                    MessageBox.Show("This login is already taken",
                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                User user = User.GetInstance();
                user.Name = this.Name;
                user.Login = this.Login;
                user.Password = this.Password;
                user.IsAdmin = false;

                this._userService.Add(user);

                this._userNavigationService.Navigate();
            });
        }
    }
}
