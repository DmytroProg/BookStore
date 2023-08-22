using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [Table("ORDER")]
    public class OrderInfo
    {
        public int Id { get; set; }

        [Required]
        public virtual BookInfo Book { get; set; } = null!;

        [Required]
        public virtual UserInfo User { get; set; } = null!;

        [Required]
        [Column(TypeName = "BIT")]
        public bool IsPaid { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime OrderDate { get; set; } 
    }
}
