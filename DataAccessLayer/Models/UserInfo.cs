using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [Table("USER")]
    public class UserInfo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Login { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = null!;

        [Required]
        [Column(TypeName = "BIT")]
        public bool IsAdmin { get; set; }
    }
}
