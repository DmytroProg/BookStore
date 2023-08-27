using BookStoreCore.Services;
using BookStoreCore.Stores;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Net.WebRequestMethods;

namespace BookStoreCore.ViewModels
{
    public class UserMainViewModel : ViewModelBase
    {
        private BookDetailsService _bookDetailsService = null!;
        private NavigationStore _navigationStore = null!;

        public ObservableCollection<BookDetails> BookDetails { get; set; } = null!;
        public ObservableCollection<Order> SavedBooks { get; set; } = null!;

        public UserMainViewModel(NavigationStore navigationStore)
        {
            this._bookDetailsService = new BookDetailsService(
                ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString);
            this.BookDetails = new ObservableCollection<BookDetails>(
                this._bookDetailsService.GetAll().Where(x => x.IsAvailable && x.Count > 0));
            this._navigationStore = navigationStore;
            this.SavedBooks = new ObservableCollection<Order>(this._bookDetailsService.GetOrders()
                .Where(x => !x.IsPaid));
        }

        public ICommand ChooseBook
        {
            get => new RelayCommand<BookDetails>(book => { new NavigationService(this._navigationStore, () => CreateInfoBookViewModel(book.Book)).Navigate(); });
        }

        public ICommand ChooseSavedBook
        {
            get => new RelayCommand<Order>(order => { new NavigationService(this._navigationStore, () => CreateInfoBookViewModel(order.Book)).Navigate(); });
        }

        public InfoBookViewModel CreateInfoBookViewModel(Book book)
        {
            return new InfoBookViewModel(book, new NavigationService(this._navigationStore, CreateUserMainViewModel));
        }

        public UserMainViewModel CreateUserMainViewModel()
        {
            return new UserMainViewModel(this._navigationStore);
        }
    }
}
