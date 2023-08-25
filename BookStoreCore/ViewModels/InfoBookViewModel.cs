using BookStoreCore.Commands;
using BookStoreCore.Services;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookStoreCore.ViewModels
{
    public class InfoBookViewModel : ViewModelBase
    {
        public Book CurrentBook { get; set; } = null!;

        public InfoBookViewModel(Book book, NavigationService navigationService)
        {
            this.GoBack = new NavigationCommand(navigationService);
            this.CurrentBook = book;
            OnPropertyChanged(nameof(CurrentBook));
        }

        public InfoBookViewModel()
        {
        }

        public ICommand GoBack { get; }
    }
}
