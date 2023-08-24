using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreCore.Stores;
using System.Windows.Input;
using BookStoreCore.Commands;
using BookStoreCore.Services;
using GalaSoft.MvvmLight.Command;
using System.CodeDom;
using System.Windows;
using System.DirectoryServices.ActiveDirectory;
using BusinessLogicLayer.Services;
using System.Configuration;
using System.Collections.ObjectModel;

namespace BookStoreCore.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        private BookDetailsService _bookDetailsService = null!;
        private BookService _bookService = null!;

        private BookDetails? updateBook;
        private string _title;
        private Author _author;
        private string _imagePath;
        private string _publisher;
        private int _pageCount;
        private int _publishYear;
        private decimal _value;
        private decimal _price;
        private int? _part;
        private Author _newAuthor;

        public Author NewAuthor {
            get => _newAuthor;
            set
            {
                _newAuthor = value;
                OnPropertyChanged(nameof(NewAuthor));
            }
        }

        public ICommand AddNewAuthor
        {
            get => new RelayCommand(() =>
            {
                try
                {
                    if (IsAuthorValid())
                    {
                        this._bookService.AddAuthor(this.NewAuthor);
                        this.Authors.Add(this.NewAuthor);
                        MessageBox.Show("New author is added!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Incorrect data passed to the form", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch(ArgumentNullException)
                {
                    MessageBox.Show("Incorrect data passed to the form", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot create a new author right now. Check your input or contact us",
                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            });
        }

        private bool IsAuthorValid()
        {
            if (string.IsNullOrEmpty(this.NewAuthor.Name) || string.IsNullOrWhiteSpace(this.NewAuthor.Name))
                return false;
            if (string.IsNullOrEmpty(this.NewAuthor.LastName) || string.IsNullOrWhiteSpace(this.NewAuthor.LastName))
                return false;
            if (this.NewAuthor.BornYear <= 0)
                return false;
            return true;
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(this.Title));
            }
        }
        public Author Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged(nameof(this.Author));
            }
        }
        public string Publisher
        {
            get => _publisher;
            set
            {
                _publisher = value;
                OnPropertyChanged(nameof(this.Publisher));
            }
        }
        public int PageCount
        {
            get => _pageCount;
            set
            {
                _pageCount = value;
                OnPropertyChanged(nameof(this.PageCount));
            }
        }
        public int PublishYear
        {
            get => _publishYear;
            set
            {
                _publishYear = value;
                OnPropertyChanged(nameof(this.PublishYear));
            }
        }
        public decimal Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(this.Value));
            }
        }
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(this.Price));
            }
        }
        public int? Part
        {
            get => _part;
            set
            {
                _part = value;
                OnPropertyChanged(nameof(this.Part));
            }
        }
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChanged(nameof(this.ImagePath));
            }
        }

        public ObservableCollection<Author> Authors { get; set; }
        public List<Genre> Genres { get; set; }

        public ICommand GoBackToAdminMain { get; }

        public ICommand CreateBook
        {
            get => new RelayCommand(() => {
                if (!TryShowErrorMessage())
                {
                    return;
                }
                var book = new Book() {
                    Name = this.Title,
                    Author = new Author() { 
                        Id = this.Author.Id,
                        Name = this.Author.Name,
                        LastName = this.Author.LastName,
                        BornYear = this.Author.BornYear,
                    },
                    Genres = new List<Genre>(this.Genres),
                    PageCount = this.PageCount,
                    Price = this.Price,
                    PublishYear = this.PublishYear,
                    Publisher = this.Publisher,
                    Value = this.Value,
                    Part = this.Part,
                    Image = this.ImagePath
                };
                var bookDetails = new BookDetails()
                    {
                        Book = book,
                        Count = 0,
                        IsAvailable = false
                    };

                if (this.updateBook != null)
                {
                    bookDetails.Id = this.updateBook.Id;
                    bookDetails.Book.Id = this.updateBook.Book.Id;
                    bookDetails.Book.Author.Id = this.updateBook.Book.Author.Id;
                    this._bookDetailsService.Update(bookDetails);
                }
                else
                {
                    this._bookDetailsService.Add(bookDetails);
                }

                GoBackToAdminMain?.Execute(null);
                });
        }

        public BookViewModel(NavigationService navigationService)
        {
            this.updateBook = null;
            this.NewAuthor = new Author();
            string connectionString = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            this._bookDetailsService = new BookDetailsService(connectionString);
            this._bookService = new BookService(connectionString);
            GoBackToAdminMain = new NavigationCommand(navigationService);

            this.Authors = new ObservableCollection<Author>(this._bookService.GetAuthors());
            this.Genres = new List<Genre>(this._bookService.GetGenres());
        }

        public BookViewModel(List<BookDetails> bookDetails, NavigationService navigationService, 
            BookDetails book) : this(navigationService)
        {
            this.updateBook = book;
            this.Title = book.Book.Name;
            this.Publisher = book.Book.Publisher;
            this.PublishYear = book.Book.PublishYear;
            this.Author = book.Book.Author;
            this.Value = book.Book.Value;
            this.Price = book.Book.Price;
            this.Part = book.Book.Part;
            this.PageCount = book.Book.PageCount;
            this.ImagePath = book.Book.Image;

            this.Genres.Where(x => book.Book.Genres.Select(x => x.GenreName).Contains(x.GenreName))
                .ToList()
                .ForEach(x => x.IsSelected = true);

        }

        private bool TryShowErrorMessage()
        {
            try
            {
                Validate();
                return true;
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show($"{ex.Message} is incorrect", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show($"The input is incorrect. Please meke sure every required column is filled correctly ",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(this.Title) || string.IsNullOrWhiteSpace(this.Title))
                throw new ArgumentException(nameof(this.Title));
            if (string.IsNullOrEmpty(this.Publisher) || string.IsNullOrWhiteSpace(this.Publisher))
                throw new ArgumentException(nameof(this.Publisher));
            if(this.PageCount <= 0)
                throw new ArgumentException("Page count");
            if (this.Value <= 0)
                throw new ArgumentException(nameof(this.Value));
            if (this.Price <= 0)
                throw new ArgumentException(nameof(this.Price));
            if (this.Part.HasValue && this.Part <= 0)
                throw new ArgumentException(nameof(Part));
            if (this.PublishYear <= 0)
                throw new ArgumentException("Publish year");
            if (Author is null || this.Authors.ToList().Find(x => x.Name == this.Author.Name && x.LastName ==
                this.Author.LastName) is null)
                throw new ArgumentException(nameof(this.Author));
            if (string.IsNullOrEmpty(this.ImagePath) || string.IsNullOrWhiteSpace(this.ImagePath))
                throw new ArgumentException("Image path");
            if(this.Genres.Count(x => x.IsSelected) == 0)
                throw new ArgumentException(nameof(Genre));
        }
    }
}
