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
            string message = DapperEntities.ExecuteDatabase("INSERT INTO MovieFile (MovieFileId, MovieFileChapter, MovieFileEpisode, MovieFileDubbing, MovieFileIsCensored, MovieFileSubtitleURL, MovieFileCreateDate, MovieFileIsStatus, MovieFileIsDelete, MovieRef) VALUES (@MovieFileId, @MovieFileChapter, @MovieFileEpisode, @MovieFileDubbing, @MovieFileIsCensored, @MovieFileSubtitleURL, GETDATE(), 1, 0 , @MovieRef)", Connection.FilmLand(), new { MovieFileId = idMovieFile, MovieFileChapter = movieFileDTO.MovieFileChapter, MovieFileEpisode = movieFileDTO.MovieFileEpisode, MovieFileDubbing = movieFileDTO.MovieFileDubbing, MovieFileIsCensored = movieFileDTO.MovieFileIsCensored, MovieFileSubtitleURL = movieFileDTO.MovieFileSubtitleURL, MovieRef = movieFileDTO.MovieRef });
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
            (IEnumerable<MovieFile> allMovieFileList, string message) = DapperEntities.QueryDatabase<MovieFile>("SELECT [MovieFileId]\r\n\t  ,[MovieFileChapter]\r\n      ,[MovieFileEpisode]\r\n      ,[MovieFileDubbing]\r\n      ,[MovieFileIsCensored]\r\n      ,[MovieFileSubtitleURL]\r\n\t  ,[MovieFileQuality]\r\n\t  ,[MovieFile_MovieURL]\r\nFROM [MovieFile] left join [MovieFileDetail] on MovieFile.MovieFileId = MovieFileDetail.MovieFileRef\r\nWHERE [MovieRef] = @MovieId AND MovieFileIsDelete = 0 order by MovieFileEpisode", Connection.FilmLand(), new { MovieId = movieId });
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

        public string RemoveMovieFile(Guid movieFileId)
        {
            string message = DapperEntities.ExecuteDatabase("UPDATE MovieFile SET MovieFileIsDelete = 1 WHERE MovieFileId = @MovieFile", Connection.FilmLand(), new { MovieFile = movieFileId });
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

        public (MovieFileSummary, string) GetMovieFile(Guid movieFileId)
        {
            (IEnumerable<MovieFileSummary> siteMenu, string message) = DapperEntities.QueryDatabase<MovieFileSummary>("SELECT [MovieFileId]\r\n      ,[MovieFileChapter]\r\n      ,[MovieFileEpisode]\r\n      ,[MovieFileDubbing]\r\n      ,[MovieFileIsCensored]\r\n      ,[MovieFileSubtitleURL]\r\nFROM [MovieFile]\r\nWHERE MovieFileId = @MovieFileId AND MovieFileIsDelete = 0", Connection.FilmLand(), new { MovieFileId = movieFileId });
            if (message == "Success")
            {
                if (siteMenu.Count() == 0)
                {
                    _customLogger.CustomDatabaseError("Id was not found in the database");
                    return (null, "Not found");
                }
                else
                {
                    _customLogger.SuccessDatabase(message);
                    return (siteMenu.FirstOrDefault(), "Success");
                }
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return (null, "Error");
            }
        }

        public string UpdateMovieFile(Guid movieFileId, MovieFileDTO movieFileDTO)
        {
            string message = DapperEntities.ExecuteDatabase("UPDATE MovieFile SET MovieFileChapter = @MovieFileChapter, MovieFileEpisode = @MovieFileEpisode, MovieFileDubbing = @MovieFileDubbing, MovieFileIsCensored = @MovieFileIsCensored, MovieFileSubtitleURL = @MovieFileSubtitleURL \r\nWHERE MovieFileId = @MovieFileId", Connection.FilmLand(), new { MovieFileId = movieFileId, MovieFileChapter = movieFileDTO.MovieFileChapter, MovieFileEpisode = movieFileDTO.MovieFileEpisode, MovieFileDubbing = movieFileDTO.MovieFileDubbing, MovieFileIsCensored = movieFileDTO.MovieFileIsCensored, MovieFileSubtitleURL = movieFileDTO.MovieFileSubtitleURL });
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
