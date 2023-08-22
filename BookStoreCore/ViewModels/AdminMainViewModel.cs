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

namespace BookStoreCore.ViewModels
{
    public class AdminMainViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;

        public ObservableCollection<BookDetails> BookDetails { get; set; }

        public ICommand ShowAddBookView { get; }
        public ICommand UpdateBookView 
        {
            get => 
                new RelayCommand<BookDetails>(book => new NavigationService(this._navigationStore, () => UpdateBookViewModel(book.Book)).Navigate(), false);
        }

        public AdminMainViewModel(List<BookDetails> bookDetails, NavigationStore navigationStore)
        {
            this._navigationStore = navigationStore;

            this.ShowAddBookView = new NavigationCommand(new NavigationService(navigationStore, CreateBookViewModel));

            this.BookDetails = new ObservableCollection<BookDetails>();
            UpdateBookCollection(bookDetails);

            // Test
            this.BookDetails.Add(new BookDetails()
            {
                Id = 1,
                Count = 100,
                IsAvailable = true,
                Book = new Book()
                {
                    Name = "Test1",
                    Author = new Author() { Name = "Name1", LastName = "LastName1", Id = 1 },
                    Id = 1,
                    PageCount = 200,
                    Price = 20,
                    PublishYear = 2019,
                    Publisher = "Publisher1",
                    Value = 15,
                    Genres = new List<string>() { "Fantasy" },
                    Image = "https://images.ctfassets.net/usf1vwtuqyxm/24YWmI4UcyoMwj7wdKrEcL/374de1941927db12bd844fb197eab11f/English_Harry_Potter_3_Epub_9781781100233.jpg?w=914&q=70&fm=jpg",
                    Part = 1,
                }
            });
            this.BookDetails.Add(new BookDetails()
            {
                Id = 2,
                Count = 200,
                IsAvailable = false,
                Book = new Book()
                {
                    Name = "Test2",
                    Author = new Author() { Name = "Name2", LastName = "LastName2", Id = 2 },
                    Id = 2,
                    PageCount = 320,
                    Price = 25,
                    PublishYear = 2018,
                    Publisher = "Publisher2",
                    Value = 15,
                    Genres = new List<string>() { "Fantasy", "Detective" },
                    Image = "https://images.ctfassets.net/usf1vwtuqyxm/3d9kpFpwHyjACq8H3EU6ra/85673f9e660407e5e4481b1825968043/English_Harry_Potter_4_Epub_9781781105672.jpg?w=914&q=70&fm=jpg",
                    Part = 2,
                }
            });
            //////////////////////
        }

        private void UpdateBookCollection(List<BookDetails> bookDetails)
        {
            this.BookDetails.Clear();
            foreach(var bookDetail in bookDetails)
            {
                this.BookDetails.Add(bookDetail);
            }
        }

        private BookViewModel CreateBookViewModel()
        {
            return new BookViewModel(this.BookDetails.ToList(), new NavigationService(this._navigationStore, CreateAdminMainViewModel));
        }

        private BookViewModel UpdateBookViewModel(Book book)
        {
            return new BookViewModel(this.BookDetails.ToList(), new NavigationService(this._navigationStore, CreateAdminMainViewModel), book);
        }

        private AdminMainViewModel CreateAdminMainViewModel()
        {
            return new AdminMainViewModel(this.BookDetails.ToList(), this._navigationStore);
        }


    }
}
