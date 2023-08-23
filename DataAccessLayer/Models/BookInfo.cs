using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [Table("BOOK")]
    public class BookInfo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }

        public virtual AuthorInfo Author { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Publisher { get; set; } = null!;

        public virtual ICollection<GenreInfo> Genres { get; set; } = null!;

        [Required]
        public int PageCount { get; set; }

        [Required]
        public int PublishYear { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal Value { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        public int? Part { get; set; }

        [Required]
        public string Image { get; set; } = null!;

        public virtual BookDetailsInfo BookDetails { get; set; } = null!;
    }
}
