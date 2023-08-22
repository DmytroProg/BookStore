using BookStoreCore.Services;
using BookStoreCore.Stores;
using BookStoreCore.ViewModels;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookStoreCore.Commands
{
    public class NavigationCommand : ICommand
    {
        private readonly NavigationService _navigationService;

        public event EventHandler CanExecuteChanged;

        public NavigationCommand(NavigationService navigationService)
        {
            this._navigationService = navigationService;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            this._navigationService.Navigate();
        }
    }
}
