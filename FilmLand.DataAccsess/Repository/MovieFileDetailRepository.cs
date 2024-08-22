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
    public class MovieFileDetailRepository : IMovieFileDetailRepository
    {
        private readonly ICustomLogger _customLogger;
        public MovieFileDetailRepository(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }
        public string AddMovieFileDetail(MovieFileDetailDTO movieFileDetailDTO)
        {
            Guid idMovieFileDetail = Guid.NewGuid();
            string message = DapperEntities.ExecuteDatabase("INSERT INTO MovieFileDetail (MovieFileDetailId, MovieFileQuality, MovieFile_MovieURL, MovieFileRef) VALUES (@MovieFileId, @MovieFileQuality, @MovieFile_MovieURL, @MovieFileRef)", Connection.FilmLand(), new { MovieFileId = idMovieFileDetail, MovieFileQuality = movieFileDetailDTO.MovieFileQuality, MovieFile_MovieURL = movieFileDetailDTO.MovieFile_MovieURL, MovieFileRef = movieFileDetailDTO.MovieFileRef });
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
