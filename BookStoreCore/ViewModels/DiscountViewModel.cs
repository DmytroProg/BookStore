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
                    this.BookDetails.Clear();
                    foreach (var item in this._bookDetailsService.GetAll()
                        .Where(x => x.IsAvailable && x.Count > 0))
                    {
                        this.BookDetails.Add(item);
                    }
                    return;
                }
                UpdateCollection();
            }
        }

        private void UpdateCollection()
        {
            this.BookDetails.Clear();
            foreach(var item in this._bookDetailsService.GetAll()
                .Where(x => x.IsAvailable && x.Count > 0 &&
            x.Book.Genres.Select(y => y.GenreName).Contains(_selectedGenre)))
            {
                this.BookDetails.Add(item);
            }
        }

        public ObservableCollection<BookDetails> BookDetails { get; set; }
        public ObservableCollection<string> Genres { get; set; }

        public ICommand AddDiscount { get; }

        public DiscountViewModel(NavigationService navigationService)
        {
            this.Persent = 10;
            string connectionString = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            this._discountService = new DiscountService(connectionString);
            this._bookDetailsService = new BookDetailsService(connectionString);
            this.BookDetails = new ObservableCollection<BookDetails>(this._bookDetailsService.GetAll()
                .Where(x => x.IsAvailable && x.Count > 0));
            this.Genres = new ObservableCollection<string>(new BookService(connectionString).GetGenres()
                .Select(x => x.GenreName));
            this.Genres.Insert(0, "No specific genre");
            this.AddDiscount = new RelayCommand(() => { CreateDiscount(); new NavigationCommand(navigationService).Execute(this); });
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
            this._discountService.Add(discount);
            foreach(var item in this.BookDetails.Where(x => x.HasDiscount))
            {
                item.Book.Discount = discount;
                this._bookDetailsService.Update(item);
            }
            MessageBox.Show("Discount added!", "Message",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
