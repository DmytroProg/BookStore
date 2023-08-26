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
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace BookStoreCore.ViewModels
{
    public class AdminMainViewModel : ViewModelBase
    {
        private BookDetailsService _bookDetailsService;
        private NavigationStore _navigationStore;
        private string _searchText;

        public ObservableCollection<BookDetails> BookDetails { get; set; }

        public string SearchText { 
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                if (_searchText == string.Empty)
                {
                    UpdateBookCollection(this._bookDetailsService.GetAll());
                }
                else
                {
                    UpdateBookCollection(this._bookDetailsService.GetAll()
                        .Where(x => x.Book.Name.Contains(_searchText)));
                }
            }
        }

        public ICommand ShowAddBookView { get; }
        public ICommand UpdateBookView 
        {
            get => 
                new RelayCommand<BookDetails>(book => new NavigationService(this._navigationStore, () => UpdateBookViewModel(book)).Navigate(), false);
        }

        public ICommand DeleteBookView
        {
            get => 
                new RelayCommand<BookDetails>(book => {
                    if(book is null)
                    {
                        MessageBox.Show("Select a book before deleting", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var dialog = MessageBox.Show($"Are you sure you want to delete book \"{book.Book.Name}\"?",
                        "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (dialog == MessageBoxResult.Yes)
                    {
                        this._bookDetailsService.Remove(book);
                        UpdateBookCollection(this._bookDetailsService.GetAll());
                    }
                });
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

        public ICommand SaveDetails
        {
            get => new RelayCommand(() =>
            {
                foreach (var item in this.BookDetails)
                {
                    this._bookDetailsService.Update(item);
                }
                MessageBox.Show("Saved!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            });
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
