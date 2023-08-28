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
        private string _topFilter;
        private bool _isUpdatingCollection;
        private Func<IEnumerable<BookDetails>, IEnumerable<BookDetails>> FilterBookData = null!;

        public ObservableCollection<BookDetails> BookDetails { get; set; } = null!;
        public ObservableCollection<Order> SavedBooks { get; set; } = null!;
        public ObservableCollection<Order> MyBooks { get; set; } = null!;
        public List<string> Authors { get; set; } = null!;
        public List<string> Genres { get; set; } = null!;
        private Dictionary<string, Func<IEnumerable<BookDetails>, IEnumerable<BookDetails>>> _topFilters;
        public List<string> TopFilters { get; set; } = null!;

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

        public string TopFilterKey
        {
            get => _topFilter;
            set
            {
                _topFilter = value;
                OnPropertyChanged(nameof(TopFilterKey));
                if (_topFilter == "-")
                {
                    FilterBookData -= GetTopFilter;
                }
                else
                {
                    if (!FilterBookData.GetInvocationList().Contains(GetTopFilter))
                        FilterBookData += GetTopFilter;
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
            this.Authors = new List<string>(bookService.GetAuthors()
                .OrderByDescending(x => this._bookDetailsService.GetOrders()?
                .Where(y => y.Book.Author.Id == x.Id).Count())
                .Select(x => x.LastName));
            this.Authors.Insert(0, "-");
            this.Genres = new List<string>(bookService.GetGenres().Select(x => x.GenreName));
            this.Genres.Insert(0, "-");
            this.FilterBookData = (list) => GetAllBooks();
            SetupTopFilters();
            this.TopFilters = new List<string>(this._topFilters.Keys);
            this.TopFilterKey = "-";
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
            if (_isUpdatingCollection) return;
            _isUpdatingCollection = true;
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

            if(_topFilter != "-")
            {
                list = list.Take(5).ToList();
            }

            foreach(var item in list)
            {
                this.BookDetails.Add(item);
            }
            _isUpdatingCollection = false;
        }

        private IEnumerable<BookDetails> GetTopFilter(IEnumerable<BookDetails> books)
        {
            return this._topFilters[this.TopFilterKey].Invoke(books);
        }

        private void SetupTopFilters()
        {
            this._topFilters = new Dictionary<string, Func<IEnumerable<BookDetails>, 
                IEnumerable<BookDetails>>>(); 
            this._topFilters.Add("-", null);
            this._topFilters.Add("Top 5 new books", GetTopNewBooks);
            this._topFilters.Add("Top 5 best books", GetTopBestBooks);
            this._topFilters.Add("Top genre today", books => GetBestGenre(books, 1));
            this._topFilters.Add("Top genre this week", books => GetBestGenre(books, 7));
            this._topFilters.Add("Top genre this month", books => GetBestGenre(books, 30));
            this._topFilters.Add("Top genre this year", books => GetBestGenre(books, 365));
        }

        private IEnumerable<BookDetails> GetTopNewBooks(IEnumerable<BookDetails> books) =>
            books.OrderByDescending(x => x.Book.PublishYear);
        private IEnumerable<BookDetails> GetTopBestBooks(IEnumerable<BookDetails> books) =>
            books.OrderByDescending(x => this._bookDetailsService?.GetOrders()?
            .Where(y => y.Book.Id == x.Book.Id).Count());
        private IEnumerable<BookDetails> GetBestGenre(IEnumerable<BookDetails> books, int days)
        {
            string genre = this.Genres.GroupBy(g => g)
                .OrderByDescending(x => this._bookDetailsService?.GetOrders()?
                .Where(y => y.Book.Genres
                .Select(z => z.GenreName)
                .Contains(x.Key)
                && (DateTime.Now - y.OrderDate) <= new TimeSpan(days, 0, 0, 0))
                .Count()).First().Key;

            this.FilterGenre = genre;
            return GetBooksByGenre(books);        
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
