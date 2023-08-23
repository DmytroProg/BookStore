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
        private BookDetailsContext _bookDetailsContext = null!;

        public BookDetailsRepository(string connectionString)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder().UseSqlServer(connectionString);
            this._bookDetailsContext = new BookDetailsContext(dbContextOptionsBuilder.Options);
        }

        public void Add(BookDetailsInfo value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookDetailsInfo> GetAll()
        {
            throw new NotImplementedException();
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
