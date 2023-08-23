using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [Table("BOOK_DETAILS")]
    public class BookDetailsInfo
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }

        [Required]
        public virtual BookInfo Book { get; set; } = null!;

        [Required]
        public int Count { get; set; }

        [Required]
        [Column(TypeName ="BIT")]
        public bool IsAvailable { get; set; }
    }
}
