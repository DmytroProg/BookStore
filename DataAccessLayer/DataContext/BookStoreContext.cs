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

        public virtual DbSet<BookDetailsInfo> BookDetails { get; set; }
        public virtual DbSet<BookInfo> Books { get; set; }
        public virtual DbSet<AuthorInfo> Authors { get; set; }   
        public virtual DbSet<GenreInfo> Genres { get; set; }    
        public virtual DbSet<OrderInfo> Orders { get; set; }
        public virtual DbSet<UserInfo> Users { get; set; }
    }
}
