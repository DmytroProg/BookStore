using BookStoreCore.Stores;
using BookStoreCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreCore.Services
{
    public class NavigationService
    {
        private readonly NavigationStore _navigationStore;
        private Func<ViewModelBase> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            this._navigationStore = navigationStore;
            this._createViewModel = createViewModel;
        }

        public void Navigate()
        {
            this._navigationStore.CurrectViewModel = _createViewModel();
        }
    }
}
