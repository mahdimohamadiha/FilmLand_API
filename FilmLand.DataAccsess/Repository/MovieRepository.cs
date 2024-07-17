using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
using FilmLand.Logs;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ICustomLogger _customLogger;
        public MovieRepository(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }
        public string AddMovie(MovieDTO movieDTO)
        {
            Guid idMovie = Guid.NewGuid();
            string message = DapperEntities.ExecuteDatabase("INSERT INTO Movie (MovieId, MoviePersionName, MovieEnglishName, MovieTitle, MovieLike, MovieDislike, MovieCreateDate, MovieIsStatus, MovieIsDelete) VALUES (@MovieId, @MoviePersionName, @MovieEnglishName, @MovieTitle, 0, 0, GETDATE(), 1, 0);", Connection.FilmLand(), new { MovieId = idMovie, MoviePersionName = movieDTO.MoviePersionName, MovieEnglishName = movieDTO.MovieEnglishName, MovieTitle = movieDTO.MovieTitle });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return message;
        }
    }
}
