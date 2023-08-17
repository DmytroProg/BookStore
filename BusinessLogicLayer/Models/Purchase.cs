using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
