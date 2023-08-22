﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [Table("GENRE")]
    public class GenreInfo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string GenreName { get; set; } = null!;
    }
}
