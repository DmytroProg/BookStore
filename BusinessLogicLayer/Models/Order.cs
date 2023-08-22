using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Book Book { get; set; } = null!;
        public User User { get; set; } = null!;
        public bool IsPaid { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
