using BookStoreCore.Commands;
using BookStoreCore.Services;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookStoreCore.ViewModels
{
    public class InfoBookViewModel : ViewModelBase
    {
        public BookDetailsService _bookDetailsService = null!;
        public Book CurrentBook { get; set; } = null!;

        public InfoBookViewModel(Book book, NavigationService navigationService)
        {
            this.GoBack = new NavigationCommand(navigationService);
            this._bookDetailsService = new BookDetailsService(
                ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString);
            this.CurrentBook = book;
            OnPropertyChanged(nameof(CurrentBook));
        }

        public InfoBookViewModel()
        {
        }

        public ICommand GoBack { get; }
        public ICommand SaveBook {
            get => new RelayCommand(() =>
            {
                this._bookDetailsService.BuyBook(this.CurrentBook, false);
            });
        }
    }
}
