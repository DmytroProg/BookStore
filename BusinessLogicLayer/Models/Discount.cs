using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Discount
    {
        public Discount()
        {
            this.Books = new List<Book>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Percents { get; set; }

        public List<Book> Books { get; set; } = null!;
    }
}
