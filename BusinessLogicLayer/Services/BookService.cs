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
    public class BookService : IService<Book>
    {
        private IRepository<BookInfo> _bookRepository = null!;

        public BookService(string connectionString)
        {
            this._bookRepository = new BookRepository(connectionString);
        }

        private Book TranslateToBookModel(BookInfo book)
        {
            return new Book()
            {
                Id = book.Id,
                Name = book.Name,
                Author = new Author()
                {
                    Id = book.Author.Id,
                    Name = book.Author.Name,
                    LastName = book.Author.LastName,
                    BornYear = book.Author.BornYear,
                },
                Genres = new List<Genre>(book.Genres.Select(x =>
                new Genre()
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
            };
        }

        private BookInfo TranslateToBookInfo(Book book)
        {
            return new BookInfo()
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
                Genres = new List<GenreInfo>(book.Genres.Where(x => x.IsSelected)
                    .Select(x => new GenreInfo()
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
            };
        }

        private Author TranslateToAuthorModel(AuthorInfo author)
        {
            var mapObject = new MapperConfiguration(map => map.CreateMap<AuthorInfo, Author>())
               .CreateMapper();
            return mapObject.Map<AuthorInfo, Author>(author);
        }

        private AuthorInfo TranslateToAuthorInfo(Author author)
        {
            var mapObject = new MapperConfiguration(map => map.CreateMap<Author, AuthorInfo>())
               .CreateMapper();
            return mapObject.Map<Author, AuthorInfo>(author);
        }

        private Genre TranslateToGenreModel(GenreInfo genre)
        {
            var mapObject = new MapperConfiguration(map => map.CreateMap<GenreInfo, Genre>())
               .CreateMapper();
            return mapObject.Map<GenreInfo, Genre>(genre);
        }

        public void Add(Book value)
        {
            if(value is null)
                throw new ArgumentNullException(nameof(value));

            this._bookRepository.Add(TranslateToBookInfo(value));
        }

        public IEnumerable<Book> GetAll()
        {
            return this._bookRepository.GetAll().Select(x => TranslateToBookModel(x));
        }

        public void Remove(Book value)
        {
            if(value is null)
                throw new ArgumentNullException(nameof(value));

            this._bookRepository.Remove(TranslateToBookInfo(value));
        }

        public void Update(Book value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            this._bookRepository.Update(TranslateToBookInfo(value));
        }

        public IEnumerable<Author> GetAuthors()
        {
            if (this._bookRepository is BookRepository bookRepository)
            {
                return bookRepository.GetAuthors().Select(x => TranslateToAuthorModel(x));
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public IEnumerable<Genre> GetGenres()
        {
            if (this._bookRepository is BookRepository bookRepository)
            {
                return bookRepository.GetGenres().Select(x => TranslateToGenreModel(x));
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public void AddAuthor(Author value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            if (this._bookRepository is BookRepository bookRepository)
            {
                bookRepository.AddAuthor(TranslateToAuthorInfo(value));
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public Book FindOne(int id)
        {
            return TranslateToBookModel(this._bookRepository.FindOne(id));
        }
    }
}
