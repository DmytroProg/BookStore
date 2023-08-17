using BookStoreCore.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreCore.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;

        public ViewModelBase CurrectViewModel { get => this._navigationStore.CurrectViewModel; }

        public MainViewModel(NavigationStore navigationStore)
        {
            this._navigationStore = navigationStore;

            this._navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrectViewModel));
        }
    }
}
