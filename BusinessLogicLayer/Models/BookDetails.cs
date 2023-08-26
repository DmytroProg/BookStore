using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class BookDetails
    {
        public int Id { get; set; }
        public Book Book { get; set; } = null!;
        public int Count { get; set; }
        public bool IsAvailable { get; set; }
        public bool HasDiscount { get; set; }
    }
}
