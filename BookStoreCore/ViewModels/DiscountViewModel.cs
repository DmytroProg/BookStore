using BookStoreCore.Commands;
using BookStoreCore.Services;
using BusinessLogicLayer.Interfaces;
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
    public class DiscountViewModel : ViewModelBase
    {
        private IService<Discount> _discountService;
        private IService<BookDetails> _bookDetailsService;
        private IService<Book> _bookService;
        private NavigationService _navigationService;
        private int _percent;
        private string _name;
        private string _selectedGenre;

        public int Persent
        {
            get => _percent;
            set
            {
                _percent = value;
                OnPropertyChanged(nameof(Persent));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string SelectedGenre
        {
            get => _selectedGenre;
            set
            {
                _selectedGenre = value;
                OnPropertyChanged(nameof(SelectedGenre));
                if(value == "No specific genre")
                {
                    UpdateCollection();
                    return;
                }
                UpdateCollectionByGenre();
            }
        }

        private bool _isBooks;

        public bool IsBooksRadio
        {
            get => _isBooks;
            set
            {
                _isBooks = value;
                OnPropertyChanged(nameof(this.IsBooksRadio));
                OnPropertyChanged(nameof(this.IsBooksTable));
            }
        }

        public bool IsDiscountsRadio => !IsBooksRadio;

        public Visibility IsBooksTable
        {
            get => _isBooks? Visibility.Visible : Visibility.Hidden;
            set
            {
                _isBooks = value == Visibility.Visible;
                OnPropertyChanged(nameof(IsBooksTable));
                
            }
        }

        public Visibility IsDiscountTable => 
            IsBooksTable == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;

        private void UpdateCollectionByGenre()
        {
            this.BookDetails.Clear();
            foreach(var item in this._bookDetailsService.GetAll()
                .Where(x => x.IsAvailable && x.Count > 0 &&
            x.Book.Genres.Select(y => y.GenreName).Contains(_selectedGenre)))
            {
                this.BookDetails.Add(item);
            }
        }

        private void UpdateCollection()
        {
            this.BookDetails.Clear();
            foreach (var item in this._bookDetailsService.GetAll()
                .Where(x => x.IsAvailable && x.Count > 0))
            {
                this.BookDetails.Add(item);
            }
        }

        public ObservableCollection<BookDetails> BookDetails { get; set; }
        public ObservableCollection<Discount> Discounts { get; set; }
        public ObservableCollection<string> Genres { get; set; }

        public ICommand AddDiscount { get; }
        public ICommand GoBack { get; }
        public ICommand DeleteDiscount {
            get => new RelayCommand<Discount>(d =>
            {
                if(MessageBox.Show("Are you sure you want to delete this discount?", "Question",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        this._discountService.Remove(d);
                        this.Discounts.Remove(d);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Cannot remove a discount. Try restart a program and delete it again",
                                "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    finally
                    {
                        UpdateCollection();
                    }
                }
            });
        }

        public DiscountViewModel(NavigationService navigationService)
        {
            this.Persent = 10;
            string connectionString = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            this._discountService = new DiscountService(connectionString);
            this._bookDetailsService = new BookDetailsService(connectionString);
            this._bookService = new BookService(connectionString);  
            this.BookDetails = new ObservableCollection<BookDetails>(this._bookDetailsService.GetAll()
                .Where(x => x.IsAvailable && x.Count > 0));
            this.Discounts = new ObservableCollection<Discount>(this._discountService.GetAll());
            this.Genres = new ObservableCollection<string>((this._bookService as BookService).GetGenres()
                .Select(x => x.GenreName));
            this.Genres.Insert(0, "No specific genre");
            this._navigationService = navigationService;
            this.GoBack = new NavigationCommand(this._navigationService);
            this.AddDiscount = new RelayCommand(() => { 
                CreateDiscount();
                this.GoBack.Execute(this);
            });
        }

        public DiscountViewModel()
        { 
        }

        private void CreateDiscount()
        {
            var discount = new Discount()
            {
                Name = this.Name,
                Percents = this.Persent,
                Books = new List<Book>(this.BookDetails.Where(x => x.HasDiscount).Select(x => x.Book)),
            };
            try
            {
                this._discountService.Add(discount);
                discount.Id = this._discountService.GetAll().Last().Id;
                foreach (var item in this.BookDetails.Where(x => x.HasDiscount))
                {
                    item.Book.Discount = discount;
                    this._bookService.Update(item.Book);
                }
                MessageBox.Show("Discount added!", "Message",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot add a discount. Try restart a program and delete it again",
                                "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
