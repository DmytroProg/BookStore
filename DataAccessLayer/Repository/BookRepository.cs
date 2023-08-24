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
            this._bookStoreContext = new BookStoreContext(dbContextOptionsBuilder.Options);
        }

        public void Add(BookInfo value)
        {
            if(value is null)
                throw new ArgumentNullException(nameof(value));

            this._bookStoreContext.Books.Add(value);
        }

        public IEnumerable<BookInfo> GetAll()
        {
            return this._bookStoreContext.Books
                .Include(x => x.Author)
                .Include(x => x.Genres);
        }

        public void Remove(BookInfo value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            this._bookStoreContext.Books.Add(value);
        }

        public void Update(BookInfo value)
        {
            throw new NotImplementedException();
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
        }

        public BookInfo FindOne(int id)
        {
            return GetAll().First(x => x.Id == id);
        }
    }
}
