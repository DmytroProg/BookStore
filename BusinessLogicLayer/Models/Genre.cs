using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Genre
    {
        public string GenreName { get; set; } = null!;
        public bool IsSelected { get; set; }
    }
}
