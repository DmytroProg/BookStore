using BookStoreCore.Commands;
using BookStoreCore.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookStoreCore.ViewModels
{
    public class UserLoginViewModel : ViewModelBase
    {

        private string _login;
        private string _password;

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
            LogToAccaunt = new NavigationCommand(navigationService);
        }

        public ICommand LogToAccaunt { get; }
    }
}
