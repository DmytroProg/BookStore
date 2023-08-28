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

namespace BookStoreCore.ViewModels
{
    public class UserMainViewModel : ViewModelBase
    {
        private BookDetailsService _bookDetailsService = null!;
        private NavigationStore _navigationStore = null!;
        private string _searchText;
        private string _filterAuthor;
        private string _filterGenre;
        private Func<IEnumerable<BookDetails>, IEnumerable<BookDetails>> FilterBookData;

        public ObservableCollection<BookDetails> BookDetails { get; set; } = null!;
        public ObservableCollection<Order> SavedBooks { get; set; } = null!;
        public ObservableCollection<Order> MyBooks { get; set; } = null!;
        public List<string> Authors { get; set; } = null!;
        public List<string> Genres { get; set; } = null!;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                if (_searchText == string.Empty)
                {
                    FilterBookData -= GetBooksByName;
                }
                else
                {
                    if(!FilterBookData.GetInvocationList().Contains(GetBooksByName))
                        FilterBookData += GetBooksByName;
                }
                UpdateCollection();
            }
        }

        public string FilterAuthor
        {
            get => _filterAuthor;
            set
            {
                _filterAuthor = value;
                OnPropertyChanged(nameof(FilterAuthor));
                if(_filterAuthor == "-")
                {
                    FilterBookData -= GetBooksByAuthor;
                }
                else
                {
                    if (!FilterBookData.GetInvocationList().Contains(GetBooksByAuthor))
                        FilterBookData += GetBooksByAuthor;
                }
                UpdateCollection();
            }
        }

        public string FilterGenre
        {
            get => _filterGenre;
            set
            {
                _filterGenre = value;
                OnPropertyChanged(nameof(FilterGenre));
                if (_filterGenre == "-")
                {
                    FilterBookData -= GetBooksByGenre;
                }
                else
                {
                    if (!FilterBookData.GetInvocationList().Contains(GetBooksByGenre))
                        FilterBookData += GetBooksByGenre;
                }
                UpdateCollection();
            }
        }

        public UserMainViewModel(NavigationStore navigationStore)
        {
            this._bookDetailsService = new BookDetailsService(
                ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString);
            this.BookDetails = new ObservableCollection<BookDetails>(GetAllBooks());
            this._navigationStore = navigationStore;
            this.SavedBooks = new ObservableCollection<Order>(this._bookDetailsService.GetOrders()
                .Where(x => !x.IsPaid));
            this.MyBooks = new ObservableCollection<Order>(this._bookDetailsService.GetOrders()
                .Where(x => x.IsPaid));
            var bookService = new BookService(ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString);
            this.Authors = new List<string>(bookService.GetAuthors().Select(x => x.LastName));
            this.Authors.Insert(0, "-");
            this.Genres = new List<string>(bookService.GetGenres().Select(x => x.GenreName));
            this.Genres.Insert(0, "-");
            this.FilterBookData = (list) => GetAllBooks();
        }

        private IEnumerable<BookDetails> GetAllBooks() => this._bookDetailsService.GetAll()
                        .Where(x => x.IsAvailable && x.Count > 0);
        private IEnumerable<BookDetails> GetBooksByName(IEnumerable<BookDetails> books) => 
                        books.Where(x => x.Book.Name.Contains(_searchText));
        private IEnumerable<BookDetails> GetBooksByAuthor(IEnumerable<BookDetails> books) => 
                        books.Where(x => x.Book.Author.LastName == _filterAuthor);
        private IEnumerable<BookDetails> GetBooksByGenre(IEnumerable<BookDetails> books) =>
                        books.Where(x => x.Book.Genres.Select(y => y.GenreName).Contains(_filterGenre));

        private void UpdateCollection()
        {
            this.BookDetails.Clear();
            var list = new List<BookDetails>(); 
            if (FilterBookData != null)
            {
                foreach(Func<IEnumerable<BookDetails>, IEnumerable<BookDetails>> filter 
                    in FilterBookData.GetInvocationList())
                {
                    list = filter(list).ToList();
                }
            }

            foreach(var item in list)
            {
                this.BookDetails.Add(item);
            }
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
