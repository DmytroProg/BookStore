using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataContext
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
            
        }

        public DbSet<BookDetailsInfo> BookDetails { get; set; }
        public DbSet<BookInfo> Books { get; set; }
        public DbSet<AuthorInfo> Authors { get; set; }   
        public DbSet<GenreInfo> Genres { get; set; }    
        public DbSet<OrderInfo> Orders { get; set; }
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<DiscountInfo> Discounts { get; set; }
    }
}
