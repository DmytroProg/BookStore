using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Stores
{
    public class NavigationStore
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrectViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            this.CurrentViewModelChanged?.Invoke();
        }
    }
}
