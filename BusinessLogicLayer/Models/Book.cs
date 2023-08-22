using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Author Author { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public int PageCount { get; set; }
        public List<string> Genres { get; set; } = null!;
        public int PublishYear { get; set; }
        public decimal Value { get; set; }
        public decimal Price { get; set; }
        public int? Part { get; set; }
        public string Image { get; set; } = null!;
    }
}
