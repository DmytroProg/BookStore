using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace BookStoreCore.ViewModels
{
    public class UserMainViewModel : ViewModelBase
    {
        private BookDetailsService _bookDetailsService = null!;

        public ObservableCollection<BookDetails> BookDetails { get; set; } = null!;

        public UserMainViewModel()
        {
            this._bookDetailsService = new BookDetailsService(
                ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString);
            this.BookDetails = new ObservableCollection<BookDetails>(
                this._bookDetailsService.GetAll().Where(x => x.IsAvailable && x.Count > 0));

            this.BookDetails.Add(new BookDetails()
            {
                Count = 100,
                IsAvailable = true,
                Book = new Book()
                {
                    Name = "Harry Potter",
                    Price = 9.99m,
                    Image = "https://images.ctfassets.net/usf1vwtuqyxm/24YWmI4UcyoMwj7wdKrEcL/374de1941927db12bd844fb197eab11f/English_Harry_Potter_3_Epub_9781781100233.jpg?w=914&q=70&fm=jpg"
                }
            });
        }
    }
}
