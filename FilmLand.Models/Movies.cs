using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class Movies
    {
        public Guid MovieId { get; set; }
        public string MoviePersionName { get; set; }
        public string MovieEnglishName { get; set; }
        public string MovieReleaseDate { get; set; }
        public string MovieCountryProduct { get; set; }
        public string MovieIMDBScore { get; set; }
        public string MovieSummary { get; set; }
        public string UploadFilePath { get; set; }

    }
}
