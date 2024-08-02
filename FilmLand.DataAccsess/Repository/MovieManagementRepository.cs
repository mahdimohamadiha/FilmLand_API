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
    public class MovieManagementRepository : IMovieManagementRepository
    {
        private readonly ICustomLogger _customLogger;
        public MovieManagementRepository(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }
        public string AddMovie(MovieAndUploadFileDTO movieDTO)
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
            Guid idUploadFile = Guid.NewGuid();
            string message4 = DapperEntities.ExecuteDatabase("INSERT INTO UploadFile (UploadFileId, UploadFileTitle, UploadFilePath, UploadFile_MovieRef, UploadFileCreateDate, UploadFileIsStatus, UploadFileIsDelete) VALUES (@UploadFileId, @UploadFileTitle, @UploadFilePath, @UploadFile_MovieRef, GETDATE(), 1, 0);", Connection.FilmLand(), new { UploadFileId = idUploadFile, UploadFileTitle = "CartPicture", UploadFilePath = movieDTO.CartPicturePath, UploadFile_MovieRef = idMovie });
            if (message4 == "Success")
            {
                _customLogger.SuccessDatabase(message4);
            }
            else
            {
                _customLogger.ErrorDatabase(message4);
            }
            foreach (var galleryPic in movieDTO.GalleryPicturesPath)
            {
                Guid idUploadFile2 = Guid.NewGuid();
                string message5 = DapperEntities.ExecuteDatabase("INSERT INTO UploadFile (UploadFileId, UploadFileTitle, UploadFilePath, UploadFile_MovieRef, UploadFileCreateDate, UploadFileIsStatus, UploadFileIsDelete) VALUES (@UploadFileId, @UploadFileTitle, @UploadFilePath, @UploadFile_MovieRef, GETDATE(), 1, 0);", Connection.FilmLand(), new { UploadFileId = idUploadFile2, UploadFileTitle = "GalleryPicture", UploadFilePath = galleryPic, UploadFile_MovieRef = idMovie });
                if (message4 == "Success")
                {
                    _customLogger.SuccessDatabase(message5);
                }
                else
                {
                    _customLogger.ErrorDatabase(message5);
                }
            }
            foreach (var ActorId in movieDTO.ActorIds)
            {
                Guid idMovieActor = Guid.NewGuid();
                string message6 = DapperEntities.ExecuteDatabase("INSERT INTO MovieActor (MovieActorId, MovieActor_MovieRef, MovieActor_ActorRef) VALUES (@MovieActorId, @MovieActor_MovieRef, @MovieActor_ActorRef);", Connection.FilmLand(), new { MovieActorId = idMovieActor, MovieActor_MovieRef = idMovie, MovieActor_ActorRef = ActorId });
                if (message4 == "Success")
                {
                    _customLogger.SuccessDatabase(message6);
                }
                else
                {
                    _customLogger.ErrorDatabase(message6);
                }
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
            (IEnumerable<Movie> movieList, string message) = DapperEntities.QueryDatabase<Movie>("SELECT [MovieId]\r\n\t\t,[MoviePersionName]\r\n\t\t,[MovieEnglishName]\r\n\t\t,[MovieTitle]\r\n\t\t,[MovieReleaseDate]\r\n\t\t,[MovieStatus]\r\n\t\t,[MovieCountryProduct]\r\n\t\t,[MovieAgeCategory]\r\n\t\t,[MovieOriginalLanguage]\r\n\t\t,[MovieIMDBScore]\r\n\t\t,[MovieAuthor]\r\n\t\t,[MovieDirector]\r\n\t\t,[MovieDuration]\r\n\t\t,[MovieSummary]\r\n\t\t,[MovieAbout]\r\n\t\t,[MovieBudget]\r\n\t\t,[MovieIsStatus]\r\n\t\t,[GenreTitle]\r\n\t\t,[GenreId]\r\n\t\t,[CategoryTitle]\r\n\t\t,[CategoryId]\r\nFROM ((([Movie] left join [MovieCategory] on Movie.MovieId = MovieCategory.MovieCategory_MovieRef) left join Category on Category.CategoryId = MovieCategory.MovieCategory_CategoryRef) left join [MovieGenre] on Movie.MovieId = MovieGenre.MovieGenre_MovieRef) left join Genre on Genre.GenreId = MovieGenre.MovieGenre_GenreRef\r\nWHERE Movie.MovieId = @MovieId", Connection.FilmLand(), new { MovieId = movieId });
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
