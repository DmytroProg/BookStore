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

namespace BookStoreCore.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        private BookDetailsService _bookDetailsService;

        private int _id;
        private string _title;
        private Author _author;
        private string _imagePath;
        private string _publisher;
        private int _pageCount;
        private int _publishYear;
        private decimal _value;
        private decimal _price;
        private int? _part;


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

        public List<Author> Authors { get; set; }
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
                    Author = new Author() { Name = this.Author.Name,
                        LastName = this.Author.LastName },
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

                this._bookDetailsService.Add(bookDetails);

                GoBackToAdminMain?.Execute(null);
                });
        }

        public BookViewModel(NavigationService navigationService)
        {
            this._id = -1;
            this._bookDetailsService = new BookDetailsService(
                ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString);
            GoBackToAdminMain = new NavigationCommand(navigationService);

            // Test
            this.Authors = new List<Author>() {
                new Author(){ Name = "Name1", LastName = "LastName1" },
                new Author(){ Name = "Test2", LastName = "LastName2" },
                new Author(){ Name = "Name3", LastName = "LastName3" },
                new Author(){ Name = "Name1", LastName = "LastName1" },
                new Author(){ Name = "Test2", LastName = "LastName2" },
                new Author(){ Name = "Name3", LastName = "LastName3" },
                new Author(){ Name = "Hello", LastName = "LastName1" },
                new Author(){ Name = "Test2", LastName = "LastName2" },
                new Author(){ Name = "Name3", LastName = "LastName3" },
            };

            this.Genres = new List<Genre>{
                new Genre(){ GenreName = "Horror", IsSelected = false },
                new Genre() { GenreName = "Fiction", IsSelected = false },
                new Genre() { GenreName = "Detective", IsSelected = false },
                new Genre() { GenreName = "Novel", IsSelected = false }
            };
            //////////////////////
        }

        public BookViewModel(List<BookDetails> bookDetails, NavigationService navigationService, 
            Book book) : this(navigationService)
        {
            this._id = book.Id;
            this.Title = book.Name;
            this.Publisher = book.Publisher;
            this.PublishYear = book.PublishYear;
            this.Author = book.Author;
            this.Value = book.Value;
            this.Price = book.Price;
            this.Part = book.Part;
            this.PageCount = book.PageCount;
            this.ImagePath = book.Image;

            this.Genres.Where(x => book.Genres.Select(x => x.GenreName).Contains(x.GenreName))
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
            if (Author is null || this.Authors.Find(x => x.Name == this.Author.Name && x.LastName ==
                this.Author.LastName) is null)
                throw new ArgumentException(nameof(this.Author));
            if (string.IsNullOrEmpty(this.ImagePath) || string.IsNullOrWhiteSpace(this.ImagePath))
                throw new ArgumentException("Image path");
            if(this.Genres.Count(x => x.IsSelected) == 0)
                throw new ArgumentException(nameof(Genre));
        }
    }
}
