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
        private readonly NavigationStore _navigationStore;
        private Func<ViewModelBase> _createViewModel;


        public event EventHandler CanExecuteChanged;

        public NavigationCommand(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            this._navigationStore = navigationStore;
            this._createViewModel = createViewModel;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            this._navigationStore.CurrectViewModel = _createViewModel();
        }
    }
}
