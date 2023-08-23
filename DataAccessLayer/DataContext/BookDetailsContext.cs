using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataContext
{
    public class BookDetailsContext : DbContext
    {
        public BookDetailsContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }


        public virtual DbSet<BookDetailsInfo> BookDetails { get; set; }
        public virtual DbSet<BookInfo> Books { get; set; }
        public virtual DbSet<AuthorInfo> Author { get; set; }   
        public virtual DbSet<GenreInfo> Genres { get; set; }    
        public virtual DbSet<BookGenre> BookGenres { get; set; }
        public virtual DbSet<OrderInfo> Orders { get; set; }

    }
}
