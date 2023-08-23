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
    public class BookDetailsRepository : IRepository<BookDetailsInfo>
    {
        private BookStoreContext _bookDetailsContext = null!;

        public BookDetailsRepository(string connectionString)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder().UseSqlServer(connectionString);
            this._bookDetailsContext = new BookStoreContext(dbContextOptionsBuilder.Options);
        }

        public void Add(BookDetailsInfo value)
        {
            if(value is null)
                throw new ArgumentNullException(nameof(value));

            this._bookDetailsContext.Add(value);
            this._bookDetailsContext.SaveChanges();
        }

        public IEnumerable<BookDetailsInfo> GetAll()
        {
            return this._bookDetailsContext.BookDetails
                .Include(x => x.Book)
                .Include(x => x.Book.Author)
                .Include(x => x.Book.Genres);
        }

        public void Remove(BookDetailsInfo value)
        {
            throw new NotImplementedException();
        }

        public void Update(BookDetailsInfo value)
        {
            throw new NotImplementedException();
        }
    }
}
