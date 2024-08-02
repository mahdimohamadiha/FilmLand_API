using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models.DTO
{
    public class MovieDTO
    {
        public string MoviePersionName { get; set; }
        public string MovieEnglishName { get; set; }
        public string MovieTitle { get; set; }
        public string? MovieReleaseDate { get; set; }
        public string? MovieStatus { get; set; }
        public string? MovieCountryProduct { get; set; }
        public string? MovieAgeCategory { get; set; }
        public string? MovieOriginalLanguage { get; set; }
        public string? MovieIMDBScore { get; set; }
        public string? MovieAuthor { get; set; }
        public string? MovieDirector { get; set; }
        public string? MovieDuration { get; set; }
        public string? MovieSummary { get; set; }
        public string? MovieAbout { get; set; }
        public string? MovieBudget { get; set; }
        public Guid? CategoryId { get; set; }
        public List<Guid>? GenreIds { get; set; }
        public IFormFile? CartPicture { get; set; }
        public List<IFormFile>? GalleryPictures { get; set; }
        public List<Guid>? ActorIds { get; set; }

    }
}
