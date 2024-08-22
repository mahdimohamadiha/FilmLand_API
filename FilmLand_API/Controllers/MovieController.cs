﻿using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Logs;
using FilmLand.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmLand_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICustomLogger _customLogger;

        public MovieController(IUnitOfWork unitOfWork, ICustomLogger customLogger)
        {
            _unitOfWork = unitOfWork;
            _customLogger = customLogger;
        }

        [HttpGet("Detail/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Movie_MoreInfo> GetMovie(Guid id)
        {
            _customLogger.StartAPI("Get Movie");
            (Movie_MoreInfo movie_MoreInfo, string message) = _unitOfWork.Movie.GetMovie(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get Movie");
            return Ok(movie_MoreInfo);
        }

        [HttpGet("MovieFile/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<MovieFile>> GetSingleMovieAllFile(Guid id)
        {
            _customLogger.StartAPI("Get All Movie File");
            IEnumerable<MovieFile> movieFileList = _unitOfWork.MovieFile.GetAllMovieFile(id);
            var responseAllMovieFile = movieFileList
                .GroupBy(m => m.MovieFileId)
                .Select(g => new MovieFileList
                {
                    MovieFileId = g.First().MovieFileId,
                    MovieFileChapter = g.First().MovieFileChapter,
                    MovieFileEpisode = g.First().MovieFileEpisode,
                    MovieFileDubbing = g.First().MovieFileDubbing,
                    MovieFileIsCensored = g.First().MovieFileIsCensored,
                    MovieFileQuality = g.Select(m => m.MovieFileQuality).Distinct().ToList(),
                    MovieFile_MovieURL = g.Select(m => m.MovieFile_MovieURL).Distinct().ToList(),
                    MovieFileSubtitleURL = g.First().MovieFileSubtitleURL,
                });

            if (movieFileList == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get All Mvoie File");
            return Ok(responseAllMovieFile);
        }
    }
}
