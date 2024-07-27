using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
using FilmLand.Logs;
using FilmLand.Models;
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
            string message = DapperEntities.ExecuteDatabase("INSERT INTO Movie (MovieId, MoviePersionName, MovieEnglishName, MovieTitle, MovieReleaseDate , MovieStatus, MovieCountryProduct, MovieAgeCategory, MovieOriginalLanguage, MovieIMDBScore, MovieAuthor, MovieDirector, MovieDuration, MovieSummary, MovieAbout, MovieBudget, MovieLike, MovieDislike, MovieCreateDate, MovieIsStatus, MovieIsDelete) VALUES (@MovieId, @MoviePersionName, @MovieEnglishName, @MovieTitle, @MovieReleaseDate , @MovieStatus, @MovieCountryProduct, @MovieAgeCategory, @MovieOriginalLanguage, @MovieIMDBScore, @MovieAuthor, @MovieDirector, @MovieDuration, @MovieSummary, @MovieAbout, @MovieBudget, 0, 0, GETDATE(), 1, 0);", Connection.FilmLand(), new { MovieId = idMovie, MoviePersionName = movieDTO.MoviePersionName, MovieEnglishName = movieDTO.MovieEnglishName, MovieTitle = movieDTO.MovieTitle, MovieReleaseDate = movieDTO.MovieReleaseDate, MovieStatus = movieDTO.MovieStatus, MovieCountryProduct = movieDTO.MovieCountryProduct, MovieAgeCategory = movieDTO.MovieAgeCategory, MovieOriginalLanguage = movieDTO.MovieOriginalLanguage, MovieIMDBScore = movieDTO.MovieIMDBScore, MovieAuthor = movieDTO.MovieAuthor, MovieDirector = movieDTO.MovieDirector, MovieDuration = movieDTO.MovieDuration, MovieSummary = movieDTO.MovieSummary, MovieAbout = movieDTO.MovieAbout, MovieBudget = movieDTO.MovieBudget });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
                Guid idMovieCategory = Guid.NewGuid();
                string message2 = DapperEntities.ExecuteDatabase("INSERT INTO MovieCategory (MovieCategoryId, MovieCategory_MovieRef, MovieCategory_CategoryRef) VALUES (@MovieCategoryId, @MovieCategory_MovieRef, @MovieCategory_CategoryRef);", Connection.FilmLand(), new { MovieCategoryId = idMovieCategory, MovieCategory_MovieRef = idMovie, MovieCategory_CategoryRef = movieDTO.CategoryId});
                if (message2 == "Success")
                {
                    _customLogger.SuccessDatabase(message2);
                    foreach (Guid genreId in movieDTO.GenreIds)
                    {
                        Guid idMovieGenre = Guid.NewGuid();
                        string message3 = DapperEntities.ExecuteDatabase("INSERT INTO MovieGenre (MovieGenreId, MovieGenre_MovieRef, MovieGenre_GenreRef) VALUES (@MovieGenreId, @MovieGenre_MovieRef, @MovieGenre_GenreRef);", Connection.FilmLand(), new { MovieGenreId = idMovieGenre, MovieGenre_MovieRef = idMovie, MovieGenre_GenreRef = genreId });
                        if (message3 == "Success")
                        {
                            _customLogger.SuccessDatabase(message3);
                        }
                        else
                        {
                            _customLogger.ErrorDatabase(message3);
                        }
                    }
                }
                else
                {
                    _customLogger.ErrorDatabase(message2);
                }
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return message;
        }

        public IEnumerable<AllMovie> GetAllMovie()
        {
            (IEnumerable<AllMovie> allMovieList, string message) = DapperEntities.QueryDatabase<AllMovie>("SELECT [MovieId]\r\n\t  ,[MovieEnglishName]\r\n      ,[MovieCreateDate]\r\n      ,[MovieModifiedDate]\r\n      ,[MovieIsStatus]\r\n\t  ,[CategoryTitle]\r\n\t  ,[GenreTitle]\r\nFROM ((([Movie] left join [MovieCategory] on Movie.MovieId = MovieCategory.MovieCategory_MovieRef) left join Category on Category.CategoryId = MovieCategory.MovieCategory_CategoryRef) left join [MovieGenre] on Movie.MovieId = MovieGenre.MovieGenre_MovieRef) left join Genre on Genre.GenreId = MovieGenre.MovieGenre_GenreRef\r\nWHERE MovieIsDelete = 0", Connection.FilmLand());
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return allMovieList;
        }

        public (IEnumerable<Movie>, string) GetMovie(Guid movieId)
        {
            (IEnumerable<Movie> movieList, string message) = DapperEntities.QueryDatabase<Movie>("SELECT [MovieId]\r\n      ,[MoviePersionName]\r\n      ,[MovieEnglishName]\r\n      ,[MovieTitle]\r\n      ,[MovieReleaseDate]\r\n      ,[MovieStatus]\r\n      ,[MovieCountryProduct]\r\n      ,[MovieAgeCategory]\r\n      ,[MovieOriginalLanguage]\r\n      ,[MovieIMDBScore]\r\n      ,[MovieAuthor]\r\n      ,[MovieDirector]\r\n      ,[MovieDuration]\r\n      ,[MovieSummary]\r\n      ,[MovieAbout]\r\n      ,[MovieBudget]\r\n      ,[MovieIsStatus]\r\n\t  ,[GenreTitle]\r\n\t  ,[CategoryTitle]\r\nFROM ((([Movie] left join [MovieCategory] on Movie.MovieId = MovieCategory.MovieCategory_MovieRef) left join Category on Category.CategoryId = MovieCategory.MovieCategory_CategoryRef) left join [MovieGenre] on Movie.MovieId = MovieGenre.MovieGenre_MovieRef) left join Genre on Genre.GenreId = MovieGenre.MovieGenre_GenreRef\r\nWHERE Movie.MovieId = @MovieId", Connection.FilmLand(), new { MovieId = movieId });
            if (message == "Success")
            {
                if (movieList.Count() == 0)
                {
                    _customLogger.CustomDatabaseError("Id was not found in the database");
                    return (null, "Not found");
                }
                else
                {
                    _customLogger.SuccessDatabase(message);
                    return (movieList, "Success");
                }
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return (null, "Error");
            }
        }
    }
}
