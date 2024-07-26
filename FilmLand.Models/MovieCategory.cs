using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class GenreCategory
    {
        public Guid MovieGenreId { get; set; }
        public Guid MovieGenre_MovieRef { get; set; }
        public Guid MovieGenre_GenreRef { get; set; }
    }
}
