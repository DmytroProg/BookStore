using BookStoreCore.Commands;
using BookStoreCore.Services;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookStoreCore.ViewModels
{
    public class InfoBookViewModel : ViewModelBase
    {
        public BookDetailsService _bookDetailsService = null!;
        public Book CurrentBook { get; set; } = null!;

        public bool CanBuy
        {
            get => this._bookDetailsService.GetAll()?
                    .FirstOrDefault(x => x.Book.Id == this.CurrentBook.Id)?.Count > 0;
        }

        public bool CanSave
        {
            get => !this._bookDetailsService.GetOrders()
                    .Any(x => x.Book.Id == this.CurrentBook.Id && !x.IsPaid);
        }

        public InfoBookViewModel(Book book, NavigationService navigationService)
        {
            this.GoBack = new NavigationCommand(navigationService);
            this._bookDetailsService = new BookDetailsService(
                ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString);
            this.CurrentBook = book;
            OnPropertyChanged(nameof(CurrentBook));
        }

        public InfoBookViewModel()
        {
        }

        private bool MakePurchase(bool isPaid)
        {
            try
            {
                this._bookDetailsService.BuyBook(this.CurrentBook, isPaid);
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot save this book. Try restart a program and delete it again",
                            "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        public ICommand GoBack { get; }
        public ICommand SaveBook {
            get => new RelayCommand(() =>
            {
                MakePurchase(false);
                OnPropertyChanged(nameof(CanSave));
            });
        }

        public ICommand BuyBook
        {
            get => new RelayCommand(() =>
            {
                try
                {
                    var bookDetails = this._bookDetailsService.GetAll()
                    .FirstOrDefault(x => x.Book.Id == this.CurrentBook.Id);
                    if (bookDetails != null)
                    {
                        bookDetails.Count--;
                        this._bookDetailsService.Update(bookDetails);
                    }
                    else
                    {
                        MessageBox.Show("Cannot find this book. Try restart a program and delete it again",
                                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot buy this book. Try restart a program and delete it again",
                                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                try { 
                    bool purchaseMade = MakePurchase(true);

                    if (purchaseMade)
                    {
                        MessageBox.Show($"Book \"{this.CurrentBook.Name}\" is bought!", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        GoBack.Execute(this);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot buy this book. Try restart a program and delete it again",
                                "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    var bookDetails = this._bookDetailsService.GetAll()
                    .FirstOrDefault(x => x.Book.Id == this.CurrentBook.Id);
                    bookDetails.Count++;
                    this._bookDetailsService.Update(bookDetails);
                }
            });
        }
    }
}
