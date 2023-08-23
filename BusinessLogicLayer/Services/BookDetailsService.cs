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

        public BookDetailsService(string connectionString)
        {
            this._bookDetailsRepository = new BookDetailsRepository(connectionString);
        }

        private BookDetailsInfo TranslateToBookDetailsInfo(BookDetails bookDetails)
        {
            return new BookDetailsInfo()
            {
                Id = bookDetails.Id,
                BookId = bookDetails.Book.Id,
                Count = bookDetails.Count,
                IsAvailable = bookDetails.IsAvailable,
                Book = new BookInfo()
                {
                    Id = bookDetails.Book.Id,
                    Name = bookDetails.Book.Name,
                    AuthorId = bookDetails.Book.Author.Id,
                    Author = new AuthorInfo()
                    {
                        Id = bookDetails.Book.Author.Id,
                        Name = bookDetails.Book.Author.Name,
                        LastName = bookDetails.Book.Author.LastName,
                        BornYear = bookDetails.Book.Author.BornYear
                    },
                    Genres = new List<GenreInfo>(bookDetails.Book.Genres.Where(x => x.IsSelected)
                    .Select(x => new GenreInfo()
                    {
                        Id = x.Id,
                        GenreName = x.GenreName,
                    })),
                    Publisher = bookDetails.Book.Publisher,
                    PageCount = bookDetails.Book.PageCount,
                    PublishYear = bookDetails.Book.PublishYear,
                    Value = bookDetails.Book.Value,
                    Price = bookDetails.Book.Price,
                    Part = bookDetails.Book.Part,
                    Image = bookDetails.Book.Image,
                }
            };
        }

        private BookDetails TranslateToBookDetailsModel(BookDetailsInfo bookDetails)
        {
            return new BookDetails()
            {
                Id = bookDetails.Id,
                Count = bookDetails.Count,
                IsAvailable = bookDetails.IsAvailable,
                Book = new Book()
                {
                    Id = bookDetails.Book.Id,
                    Name = bookDetails.Book.Name,
                    Author = new Author()
                    {
                        Id = bookDetails.Book.Author.Id,
                        Name = bookDetails.Book.Author.Name,
                        LastName = bookDetails.Book.Author.LastName,
                        BornYear = bookDetails.Book.Author.BornYear,
                    },
                    Genres = new List<Genre>(bookDetails.Book.Genres.Select(x => 
                    new Genre(){
                        Id = x.Id,
                        GenreName = x.GenreName,
                    })),
                    Publisher = bookDetails.Book.Publisher,
                    PageCount = bookDetails.Book.PageCount,
                    PublishYear = bookDetails.Book.PublishYear,
                    Value = bookDetails.Book.Value,
                    Price = bookDetails.Book.Price,
                    Part = bookDetails.Book.Part,
                    Image = bookDetails.Book.Image,
                },
                
            };
        }

        public void Add(BookDetails value)
        {
            if(value is null)
                throw new ArgumentNullException(nameof(value));

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
            throw new NotImplementedException();
        }
    }
}
