using BookStoreCore.Stores;
using BookStoreCore.Commands;
using BookStoreCore.ViewModels;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookStoreCore.Services;
using GalaSoft.MvvmLight.Command;
using BusinessLogicLayer.Services;
using System.Configuration;

namespace BookStoreCore.ViewModels
{
    public class AdminMainViewModel : ViewModelBase
    {
        private BookDetailsService _bookDetailsService;
        private NavigationStore _navigationStore;

        public ObservableCollection<BookDetails> BookDetails { get; set; }

        public ICommand ShowAddBookView { get; }
        public ICommand UpdateBookView 
        {
            get => 
                new RelayCommand<BookDetails>(book => new NavigationService(this._navigationStore, () => UpdateBookViewModel(book)).Navigate(), false);
        }

        public AdminMainViewModel(NavigationStore navigationStore)
        {
            this._navigationStore = navigationStore;
            this._bookDetailsService = new BookDetailsService(
                ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString);
            this.ShowAddBookView = new NavigationCommand(new NavigationService(navigationStore, CreateBookViewModel));

            this.BookDetails = new ObservableCollection<BookDetails>();
            UpdateBookCollection(this._bookDetailsService.GetAll());

           
        }

        private void UpdateBookCollection(IEnumerable<BookDetails> bookDetails)
        {
            this.BookDetails.Clear();
            foreach(var bookDetail in bookDetails)
            {
                this.BookDetails.Add(bookDetail);
            }
        }

        private BookViewModel CreateBookViewModel()
        {
            return new BookViewModel(new NavigationService(this._navigationStore, CreateAdminMainViewModel));
        }

        private BookViewModel UpdateBookViewModel(BookDetails book)
        {
            return new BookViewModel(this.BookDetails.ToList(), new NavigationService(this._navigationStore, CreateAdminMainViewModel), book);
        }

        private AdminMainViewModel CreateAdminMainViewModel()
        {
            return new AdminMainViewModel(this._navigationStore);
        }


    }
}
