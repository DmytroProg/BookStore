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
            throw new NotImplementedException();
        }

        public void Update(BookDetails value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            this._bookService.Update(value.Book);
            value.Book.Id = this._bookService.GetAll().Last().Id;
            this._bookDetailsRepository.Update(TranslateToBookDetailsInfo(value));
        }

        public BookDetails FindOne(int id)
        {
            return TranslateToBookDetailsModel(this._bookDetailsRepository.FindOne(id));
        }
    }
}
