using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class Movie_MoreInfo
    {
        public string MoviePersionName { get; set; }
        public string MovieEnglishName { get; set; }
        public string MovieTitle { get; set; }
        public string MovieReleaseDate { get; set; }
        public string MovieStatus { get; set; }
        public string MovieCountryProduct { get; set; }
        public string MovieAgeCategory { get; set; }
        public string MovieOriginalLanguage { get; set; }
        public string MovieIMDBScore { get; set; }
        public string MovieAuthor { get; set; }
        public string MovieDirector { get; set; }
        public string MovieDuration { get; set; }
        public string MovieSummary { get; set; }
        public string MovieAbout { get; set; }
        public string MovieBudget { get; set; }
        public int MovieLike { get; set; }
        public int MovieDislike { get; set; }
        public string CategoryTitle { get; set; }
        public List<string> GenreTitle { get; set; }
        public string CartPicPath { get; set; }
        public List<string> ActorPicPath { get; set; }
        public List<string> ActorName { get; set; }
        public List<string> GalleryPicPath { get; set; }
    }
}
