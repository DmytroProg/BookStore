using BookStore.Views;
using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Models;
using BookStore.Stores;
using System.Windows.Input;
using BookStore.Commands;

namespace BookStore.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        private string _title;
        private string _author;
        private string _imagePath;
        private string _publisher;
        private int _pageCount;
        private List<string> _genre;
        private int _publishYear;
        private decimal _value;
        private decimal _price;
        private int? _part;


        public string Title { 
            get => _title; 
            set { 
                _title = value; 
                OnPropertyChanged(nameof(this.Title)); 
            } 
        }
        public string Author { 
            get => _author; 
            set { 
                _author = value; 
                OnPropertyChanged(nameof(this.Author)); 
            } 
        }
        public string Publisher
        {
            get => _publisher;
            set
            {
                _title = _publisher;
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
        public string ImagePath { 
            get => _imagePath; 
            set { 
                _imagePath = value; 
                OnPropertyChanged(nameof(this.ImagePath)); 
            } 
        }

        public List<Author> Authors { get; set; }
        public List<Genre> Genres { get; set; }

        public ICommand GoBackToAdminMain { get; }

        public BookViewModel(NavigationStore navigationStore, Func<AdminMainViewModel> createAdminMainViewModel)
        {
            GoBackToAdminMain = new NavigationCommand(navigationStore, createAdminMainViewModel);

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
    }
}
