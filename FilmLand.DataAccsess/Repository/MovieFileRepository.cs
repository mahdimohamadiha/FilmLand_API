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
    public class MovieFileRepository : IMovieFileRepository
    {
        private readonly ICustomLogger _customLogger;
        public MovieFileRepository(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }
        public string AddMovieFile(MovieFileDTO movieFileDTO)
        {
            Guid idMovieFile = Guid.NewGuid();
            string message = DapperEntities.ExecuteDatabase("INSERT INTO MovieFile (MovieFileId, MovieFileChapter, MovieFileEpisode, MovieFileQuality, MovieFileDubbing, MovieFileIsCensored, MovieFileSubtitleURL, MovieFile_MovieURL, MovieFileCreateDate, MovieFileIsStatus, MovieFileIsDelete, MovieRef) VALUES (@MovieFileId, @MovieFileChapter, @MovieFileEpisode, @MovieFileQuality, @MovieFileDubbing, @MovieFileIsCensored, @MovieFileSubtitleURL, @MovieFile_MovieURL, GETDATE(), 1, 0 , @MovieRef)", Connection.FilmLand(), new { MovieFileId = idMovieFile, MovieFileChapter = movieFileDTO.MovieFileChapter, MovieFileEpisode = movieFileDTO.MovieFileEpisode, MovieFileQuality = movieFileDTO.MovieFileQuality, MovieFileDubbing = movieFileDTO.MovieFileDubbing, MovieFileIsCensored = movieFileDTO.MovieFileIsCensored, MovieFileSubtitleURL = movieFileDTO.MovieFileSubtitleURL, MovieFile_MovieURL = movieFileDTO.MovieFile_MovieURL, MovieRef = movieFileDTO.MovieRef });
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
        public IEnumerable<MovieFile> GetAllMovieFile(Guid movieId)
        {
            (IEnumerable<MovieFile> allMovieFileList, string message) = DapperEntities.QueryDatabase<MovieFile>("SELECT [MovieFileId]\r\n      ,[MovieFileChapter]\r\n      ,[MovieFileEpisode]\r\n      ,[MovieFileQuality]\r\n      ,[MovieFileDubbing]\r\n      ,[MovieFileIsCensored]\r\n      ,[MovieFileSubtitleURL]\r\n      ,[MovieFile_MovieURL]\r\n      ,[MovieFileCreateDate]\r\n      ,[MovieFileModifiedDate]\r\n      ,[MovieFileIsStatus]\r\n      ,[MovieFileIsDelete]\r\n      ,[MovieRef]\r\n  FROM [MovieFile]\r\n  WHERE MovieRef = @MovieId", Connection.FilmLand(), new { MovieID = movieId });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return allMovieFileList;
        }
    }
}
