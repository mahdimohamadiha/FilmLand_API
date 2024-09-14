using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
using FilmLand.Logs;
using FilmLand.Models;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public (Movie_MoreInfo, string) GetMovie(Guid movieId)
        {
            (IEnumerable<MovieAndCategoryAndCartPic> movieAndCategoryAndCartPics, string message) = DapperEntities.QueryDatabase<MovieAndCategoryAndCartPic>("SELECT [MovieId]\r\n      ,[MoviePersionName]\r\n      ,[MovieEnglishName]\r\n      ,[MovieTitle]\r\n\t  ,[UploadFilePath]\r\n      ,[MovieReleaseDate]\r\n      ,[MovieStatus]\r\n      ,[MovieCountryProduct]\r\n      ,[MovieAgeCategory]\r\n      ,[MovieOriginalLanguage]\r\n      ,[MovieIMDBScore]\r\n      ,[MovieAuthor]\r\n      ,[MovieDirector]\r\n      ,[MovieDuration]\r\n      ,[MovieSummary]\r\n      ,[MovieAbout]\r\n      ,[MovieBudget]\r\n      ,[MovieLike]\r\n      ,[MovieDislike]\r\n\t  ,[CategoryTitle]\r\nFROM [Movie] join [MovieCategory] ON Movie.MovieId = MovieCategory.MovieCategory_MovieRef join Category on Category.CategoryId = MovieCategory.MovieCategory_CategoryRef join UploadFile ON UploadFile.UploadFile_MovieRef = Movie.MovieId\r\nWHERE MovieId = @MovieId AND MovieIsStatus = 1 AND MovieIsDelete = 0 AND UploadFileTitle = 'CartPicture'", Connection.FilmLand(), new { MovieId = movieId });
            if (message == "Success")
            {
                if (movieAndCategoryAndCartPics.Count() == 0)
                {
                    _customLogger.CustomDatabaseError("Id was not found in the database");
                    return (null, "Not found");
                }
                else
                {
                    _customLogger.SuccessDatabase(message);
                    (IEnumerable<Genre> genre, string message2) = DapperEntities.QueryDatabase<Genre>("SELECT [GenreTitle]\r\nFROM [Genre] join [MovieGenre] ON Genre.GenreId = MovieGenre.MovieGenre_GenreRef\r\nWHERE MovieGenre_MovieRef = @MovieId AND GenreIsStatus = 1 AND GenreIsDelete = 0", Connection.FilmLand(), new { MovieId = movieId });
                    (IEnumerable<UploadFile> galleryPic, string message3) = DapperEntities.QueryDatabase<UploadFile>("SELECT [UploadFilePath]\r\nFROM [UploadFile]\r\nWHERE UploadFile_MovieRef = @MovieId AND UploadFileIsStatus = 1 AND UploadFileIsDelete = 0 AND UploadFileTitle = 'GalleryPicture'", Connection.FilmLand(), new { MovieId = movieId });
                    (IEnumerable<ActorAndUploadFile> Actor, string message4) = DapperEntities.QueryDatabase<ActorAndUploadFile>("SELECT [UploadFilePath],\r\n\t   [ActorName]\r\nFROM [MovieActor] join Actor ON MovieActor.MovieActor_ActorRef = Actor.ActorId JOIN UploadFile ON UploadFile.UploadFileId = Actor.Actor_UploadFileRef\r\nWHERE MovieActor_MovieRef = @MovieId AND ActorIsStatus = 1 AND ActorIsDelete = 0 AND UploadFileTitle = 'ActorPicture'", Connection.FilmLand(), new { MovieId = movieId });
                    Movie_MoreInfo movie_MoreInfo = new Movie_MoreInfo
                    {
                        MovieTitle = movieAndCategoryAndCartPics.FirstOrDefault().MovieTitle,
                        MoviePersionName = movieAndCategoryAndCartPics.FirstOrDefault().MoviePersionName,
                        MovieEnglishName = movieAndCategoryAndCartPics.FirstOrDefault().MovieEnglishName,
                        MovieAbout = movieAndCategoryAndCartPics.FirstOrDefault().MovieAbout,
                        MovieSummary = movieAndCategoryAndCartPics.FirstOrDefault().MovieSummary,
                        MovieReleaseDate = movieAndCategoryAndCartPics.FirstOrDefault().MovieReleaseDate,
                        MovieStatus = movieAndCategoryAndCartPics.FirstOrDefault().MovieStatus,
                        MovieAuthor = movieAndCategoryAndCartPics.FirstOrDefault().MovieAuthor,
                        MovieBudget = movieAndCategoryAndCartPics.FirstOrDefault().MovieBudget,
                        MovieDuration = movieAndCategoryAndCartPics.FirstOrDefault().MovieDuration,
                        MovieOriginalLanguage = movieAndCategoryAndCartPics.FirstOrDefault().MovieOriginalLanguage,
                        MovieDirector = movieAndCategoryAndCartPics.FirstOrDefault().MovieDirector,
                        MovieIMDBScore = movieAndCategoryAndCartPics.FirstOrDefault().MovieIMDBScore,
                        MovieAgeCategory = movieAndCategoryAndCartPics.FirstOrDefault().MovieAgeCategory,
                        MovieCountryProduct = movieAndCategoryAndCartPics.FirstOrDefault().MovieCountryProduct,
                        MovieLike = movieAndCategoryAndCartPics.FirstOrDefault().MovieLike,
                        MovieDislike = movieAndCategoryAndCartPics.FirstOrDefault().MovieDislike,
                        GenreTitle = genre.Select(m => m.GenreTitle).Distinct().ToList(),
                        CategoryTitle = movieAndCategoryAndCartPics.FirstOrDefault().CategoryTitle,
                        CartPicPath = movieAndCategoryAndCartPics.FirstOrDefault().UploadFilePath,
                        GalleryPicPath = galleryPic.Select(m => m.UploadFilePath).Distinct().ToList(),
                        ActorName = Actor.Select(m => m.ActorName).Distinct().ToList(),
                        ActorPicPath = Actor.Select(m => m.UploadFilePath).Distinct().ToList(),
                    };
                    return (movie_MoreInfo, "Success");
                }
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return (null, "Error");
            }
        }

        public IEnumerable<Movies> GetMovies(MovieParameterDTO movieParameterDTO)
        {
            string para = "";
            if (movieParameterDTO.CategoryParameter == "all")
            {
                if (movieParameterDTO.GenreParameter == "all")
                {
                    para = "";

                }
                else
                {
                    para = $"GenreParameter = '{movieParameterDTO.GenreParameter}' AND";
                }
            }
            else
            {
                if (movieParameterDTO.GenreParameter == "all")
                {
                    para = $"CategoryParameter = '{movieParameterDTO.CategoryParameter}' AND";

                }
                else
                {
                    para = $"CategoryParameter = '{movieParameterDTO.CategoryParameter}' AND GenreParameter = '{movieParameterDTO.GenreParameter}' AND";
                    ;

                }
            }
            (IEnumerable<Movies> movies, string message) = DapperEntities.QueryDatabase<Movies>("SELECT MovieId, MoviePersionName, MovieEnglishName, MovieReleaseDate, MovieCountryProduct, MovieIMDBScore, MovieSummary, UploadFilePath FROM Movie join MovieCategory on MovieId = MovieCategory_MovieRef join Category on CategoryId = MovieCategory_CategoryRef join MovieGenre on MovieId = MovieGenre_MovieRef join Genre on GenreId = MovieGenre_GenreRef join UploadFile on MovieId = UploadFile_MovieRef WHERE " + para + " UploadFileTitle = 'CartPicture' AND MovieIsDelete = 0 GROUP BY MovieId, MoviePersionName, MovieEnglishName, MovieReleaseDate, MovieCountryProduct, MovieIMDBScore, MovieSummary, UploadFilePath", Connection.FilmLand());
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return movies;
        }
    }
}
