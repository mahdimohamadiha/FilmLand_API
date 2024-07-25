using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class Genre
    {
        public Guid GenreId { get; set; }
        public string GenreTitle { get; set; }
        public DateTime GenreCreateDate { get; set; }
        public DateTime GenreModifiedDate { get; set; }
        public bool GenreIsStatus { get; set; }
        public bool GenreIsDelete { get; set; }
    }
}
