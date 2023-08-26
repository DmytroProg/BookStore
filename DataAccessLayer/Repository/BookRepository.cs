using DataAccessLayer.DataContext;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class BookRepository : IRepository<BookInfo>
    {
        private BookStoreContext _bookStoreContext = null!;

        public BookRepository(string connectionString)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder().UseSqlServer(connectionString);
            //dbContextOptionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            this._bookStoreContext = new BookStoreContext(dbContextOptionsBuilder.Options);
            
        }

        public void Add(BookInfo value)
        {
            if(value is null)
                throw new ArgumentNullException(nameof(value));

            var author = value.Author.Id;
            value.Author = this._bookStoreContext.Authors.First(x => x.Id == author);

            var genres = value.Genres.Select(x => x.Id);

            var tempGenres = this._bookStoreContext.Genres.Where(x => genres.Contains(x.Id));
            value.Genres = new List<GenreInfo>(tempGenres);

            this._bookStoreContext.Books.Add(value);
            this._bookStoreContext.SaveChanges();
        }

        public IEnumerable<BookInfo> GetAll()
        {
            return this._bookStoreContext.Books
                .Include(x => x.Author)
                .Include(x => x.Genres)
                .Include(x => x.Discount);
        }

        public void Remove(BookInfo value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var book = this._bookStoreContext.Books.FirstOrDefault(x => x.Id == value.Id);

            if(book is null)
                throw new ArgumentException(nameof(book));

            this._bookStoreContext.Books.Remove(book);
            this._bookStoreContext.SaveChanges();
        }

        public void Update(BookInfo value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var tempBook = this._bookStoreContext.Books
                .Include(x => x.Author)
                .Include(x => x.Genres)
                .First(x => x.Id == value.Id);

            if (tempBook is null)
                throw new ArgumentException(nameof(tempBook));

            tempBook.Name = value.Name;

            var author = value.Author.Id;
            tempBook.Author = this._bookStoreContext.Authors.First(x => x.Id == author);

            tempBook.AuthorId = value.AuthorId;
            tempBook.Value = value.Value;
            tempBook.Price = value.Price;

            //tempBook.Genres.Clear();
            var genres = value.Genres.Select(x => x.Id);
            tempBook.Genres = this._bookStoreContext.Genres.Where(x => genres.Contains(x.Id)).ToList();

            tempBook.PageCount = value.PageCount;
            tempBook.Part = value.Part;
            tempBook.Image = value.Image;
            tempBook.Publisher = value.Publisher;
            tempBook.PublishYear = value.PublishYear;

            if(value.Discount != null)
            {
                tempBook.Discount = new DiscountInfo()
                {
                    Id = value.Discount.Id,
                    Name = value.Discount.Name,
                    Percents = value.Discount.Percents
                };
            }

            this._bookStoreContext.Books.Update(tempBook);

            this._bookStoreContext.SaveChanges();
        }

        public IEnumerable<AuthorInfo> GetAuthors()
        {
            return this._bookStoreContext.Authors;
        }

        public IEnumerable<GenreInfo> GetGenres()
        {
            return this._bookStoreContext.Genres;
        }

        public void AddAuthor(AuthorInfo value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            this._bookStoreContext.Authors.Add(value);
            this._bookStoreContext.SaveChanges();
        }

        public BookInfo FindOne(int id)
        {
            return GetAll().First(x => x.Id == id);
        }
    }
}
