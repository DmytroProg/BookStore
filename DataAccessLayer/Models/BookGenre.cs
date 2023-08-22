using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class BookGenre
    {
        public int Id { get; set; }

        [Required]
        public virtual BookInfo Book { get; set; } = null!;

        [Required]
        public virtual GenreInfo Genre { get; set; } = null!;
    }
}
