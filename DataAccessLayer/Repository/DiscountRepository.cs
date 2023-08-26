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
    public class DiscountRepository : IRepository<DiscountInfo>
    {
        private BookStoreContext _bookStoreContext = null!;

        public DiscountRepository(string connectionString)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder().UseSqlServer(connectionString);
            this._bookStoreContext = new BookStoreContext(dbContextOptionsBuilder.Options);
        }

        public void Add(DiscountInfo value)
        {
            if(value is null)
                throw new ArgumentNullException(nameof(value));

            this._bookStoreContext.Discounts.Add(value);
            this._bookStoreContext.SaveChanges();
        }

        public DiscountInfo FindOne(int id)
        {
            return GetAll().First(x => x.Id == id);
        }

        public IEnumerable<DiscountInfo> GetAll()
        {
            return this._bookStoreContext.Discounts.Include(x => x.Books);
        }

        public void Remove(DiscountInfo value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            this._bookStoreContext.Discounts.Remove(value);
            this._bookStoreContext.SaveChanges();
        }

        public void Update(DiscountInfo value)
        {
            throw new NotImplementedException();
        }
    }
}
