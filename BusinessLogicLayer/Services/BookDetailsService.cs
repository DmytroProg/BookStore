using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class BookDetailsService : IService<BookDetails>
    {
        private IRepository<BookDetailsInfo> _bookDetailsRepository = null!;
        private BookService _bookService = null!;

        public BookDetailsService(string connectionString)
        {
            this._bookDetailsRepository = new BookDetailsRepository(connectionString);
            this._bookService = new BookService(connectionString);
        }

        private BookDetailsInfo TranslateToBookDetailsInfo(BookDetails bookDetails)
        {
            return new BookDetailsInfo()
            {
                Id = bookDetails.Id,
                BookId = bookDetails.Book.Id,
                Count = bookDetails.Count,
                IsAvailable = bookDetails.IsAvailable,
            };
        }

        private BookDetails TranslateToBookDetailsModel(BookDetailsInfo bookDetails)
        {
            return new BookDetails()
            {
                Id = bookDetails.Id,
                Count = bookDetails.Count,
                IsAvailable = bookDetails.IsAvailable,
                Book = this._bookService.FindOne(bookDetails.BookId)
            };
        }

        public void Add(BookDetails value)
        {
            if(value is null)
                throw new ArgumentNullException(nameof(value));

            this._bookService.Add(value.Book);
            value.Book.Id = this._bookService.GetAll().Last().Id;
            this._bookDetailsRepository.Add(TranslateToBookDetailsInfo(value));
        }

        public IEnumerable<BookDetails> GetAll()
        {
            return this._bookDetailsRepository.GetAll().Select(x => TranslateToBookDetailsModel(x));
        }

        public void Remove(BookDetails value)
        {
            if(value is null)
                throw new ArgumentNullException(nameof(value));

            var tempBook = this._bookService.FindOne(value.Book.Id); 
            this._bookService.Remove(tempBook);
            //this._bookDetailsRepository.Remove(TranslateToBookDetailsInfo(value));
        }

        public void Update(BookDetails value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            //this._bookService.Update(value.Book);
            //value.Book.Id = this._bookService.GetAll().Last().Id;
            this._bookDetailsRepository.Update(TranslateToBookDetailsInfo(value));
        }

        public BookDetails FindOne(int id)
        {
            return TranslateToBookDetailsModel(this._bookDetailsRepository.FindOne(id));
        }

        public void BuyBook(Book book, bool isPaid)
        {
            if (book is null)
                throw new ArgumentNullException(nameof(book));

            var user = User.GetInstance();
            OrderInfo order = new OrderInfo()
            {
                User = new UserInfo()
                {
                    Name = user.Name,
                    Login = user.Login,
                    Password = user.Password,
                    IsAdmin = user.IsAdmin,
                    Id = user.Id
                },
                Book = new BookInfo()
                {
                    Id = book.Id,
                    Name = book.Name,
                    AuthorId = book.Author.Id,
                    Author = new AuthorInfo()
                    {
                        Id = book.Author.Id,
                        Name = book.Author.Name,
                        LastName = book.Author.LastName,
                        BornYear = book.Author.BornYear
                    },
                    Genres = new List<GenreInfo>(book.Genres.Select(x => new GenreInfo()
                    {
                        Id = x.Id,
                        GenreName = x.GenreName,
                    })),
                    Publisher = book.Publisher,
                    PageCount = book.PageCount,
                    PublishYear = book.PublishYear,
                    Value = book.Value,
                    Price = book.Price,
                    Part = book.Part,
                    Image = book.Image,
                    Discount = book.Discount is null ? null : new DiscountInfo()
                    {
                        Id = book.Discount.Id,
                        Name = book.Discount.Name,
                        Percents = book.Discount.Percents
                    }
                },
                IsPaid = isPaid,
                OrderDate = DateTime.Now
            };

            (this._bookDetailsRepository as BookDetailsRepository)?.BuyBook(order);
        }

        public IEnumerable<Order>? GetOrders()
        {
            return (this._bookDetailsRepository as BookDetailsRepository)?.GetOrders()
                .Where(x => x.User.Login == User.GetInstance().Login)
                .Select(x => new Order
                {
                    Id = x.Id,
                    IsPaid = x.IsPaid,
                    OrderDate = x.OrderDate,
                    Book = this._bookService.FindOne(x.Book.Id)
                });
        }
    }
}
