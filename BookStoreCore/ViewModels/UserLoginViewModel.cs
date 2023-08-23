using BookStoreCore.Commands;
using BookStoreCore.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
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

        public UserLoginViewModel(NavigationService navigationService)
        {
            LogToAccaunt = new RelayCommand(() =>
            {
                if (!IsValid())
                {
                    MessageBox.Show("The input is incorrect. Fields must be filled and the password mush consist of at least 8 characters", 
                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                navigationService.Navigate();
            });
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

        public ICommand LogToAccaunt { get; }
    }
}
