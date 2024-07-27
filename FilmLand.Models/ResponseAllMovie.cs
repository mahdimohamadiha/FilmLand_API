using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class ResponseAllMovie
    {
        public Guid MovieId { get; set; }
        public string MovieEnglishName { get; set; }
        public DateTime MovieCreateDate { get; set; }
        public DateTime MovieModifiedDate { get; set; }
        public bool MovieIsStatus { get; set; }
        public string CategoryTitle { get; set; }
        public List<string> GenreTitles { get; set; }
    }
}
