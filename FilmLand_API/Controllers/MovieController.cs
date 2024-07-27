using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Logs;
using FilmLand.Models;
using FilmLand.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMovie([FromForm] MovieDTO movieDTO)
        {
            _customLogger.StartAPI("Add Movie");
            string result = _unitOfWork.Movie.AddMovie(movieDTO);
            if (result == "Success")
            {
                _customLogger.EndAPI("Add Movie");
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ResponseAllMovie>> GetAllMovie()
        {
            _customLogger.StartAPI("Get All Movie");
            IEnumerable<AllMovie> allMovieList = _unitOfWork.Movie.GetAllMovie();

            var responseAllMovies = allMovieList
                .GroupBy(m => m.MovieId)
                .Select(g => new ResponseAllMovie
                {
                    MovieId = g.Key,
                    MovieEnglishName = g.First().MovieEnglishName,
                    MovieCreateDate = g.First().MovieCreateDate,
                    MovieModifiedDate = g.First().MovieModifiedDate,
                    MovieIsStatus = g.First().MovieIsStatus,
                    CategoryTitle = g.First().CategoryTitle,
                    GenreTitles = g.Select(m => m.GenreTitle).Distinct().ToList()
                })
            .ToList();
            if (allMovieList == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get All Movie");
            return Ok(responseAllMovies);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Movie> GetMovie(Guid id)
        {
            _customLogger.StartAPI("Get Movie");
            (IEnumerable<Movie> movies, string message) = _unitOfWork.Movie.GetMovie(id);
            var responseMovies = movies
                .GroupBy(m => m.MovieId)
                .Select(g => new ResponseMovie
                {
                    MovieId = g.Key,
                    MoviePersionName = g.First().MoviePersionName,
                    MovieEnglishName = g.First().MovieEnglishName,
                    MovieTitle = g.First().MovieTitle,
                    MovieReleaseDate = g.First().MovieReleaseDate,
                    MovieStatus = g.First().MovieStatus,
                    MovieCountryProduct = g.First().MovieCountryProduct,
                    MovieAgeCategory = g.First().MovieAgeCategory,
                    MovieOriginalLanguage = g.First().MovieOriginalLanguage,
                    MovieIMDBScore = g.First().MovieIMDBScore,
                    MovieAuthor = g.First().MovieAuthor,
                    MovieDirector = g.First().MovieDirector,
                    MovieDuration = g.First().MovieDuration,
                    MovieSummary = g.First().MovieSummary,
                    MovieAbout = g.First().MovieAbout,
                    MovieBudget = g.First().MovieBudget,
                    MovieLike = g.First().MovieLike,
                    MovieDislike = g.First().MovieDislike,
                    MovieCollectionRef = g.First().MovieCollectionRef,
                    MovieCreateDate = g.First().MovieCreateDate,
                    MovieModifiedDate = g.First().MovieModifiedDate,
                    MovieIsStatus = g.First().MovieIsStatus,
                    MovieIsDelete = g.First().MovieIsDelete,
                    CategoryTitle = g.First().CategoryTitle,
                    GenreTitles = g.Select(m => m.GenreTitle).Distinct().ToList()
                })
                .ToList();
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get Movie");
            return Ok(responseMovies);
        }
    }
}
