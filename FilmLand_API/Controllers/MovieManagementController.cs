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
    public class MovieManagementController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICustomLogger _customLogger;

        public MovieManagementController(IUnitOfWork unitOfWork, ICustomLogger customLogger)
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
            List<string> galleryPicPath = new List<string>();
            if (movieDTO.CartPicture == null || movieDTO.CartPicture.Length == 0)
            {
                _customLogger.CustomApiError("No file uploaded");
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Cart", movieDTO.CartPicture.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await movieDTO.CartPicture.CopyToAsync(stream);
            }
            var cartPicPath = "/Cart/" + Path.GetFileName(movieDTO.CartPicture.FileName);
            foreach (var galleryPicture in movieDTO.GalleryPictures)
            {
                if (galleryPicture == null || galleryPicture.Length == 0)
                {
                    _customLogger.CustomApiError("No file uploaded");
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Gallery", galleryPicture.FileName);
                using (var stream = new FileStream(path2, FileMode.Create))
                {
                    await galleryPicture.CopyToAsync(stream);
                }
                galleryPicPath.Add("/Gallery/" + Path.GetFileName(galleryPicture.FileName));
            }
            MovieAndUploadFileDTO movieAndUploadFileDTO = new MovieAndUploadFileDTO
            {
                MoviePersionName = movieDTO.MoviePersionName,
                MovieEnglishName = movieDTO.MovieEnglishName,
                MovieTitle = movieDTO.MovieTitle,
                MovieReleaseDate = movieDTO.MovieReleaseDate,
                MovieStatus = movieDTO.MovieStatus,
                MovieCountryProduct = movieDTO.MovieCountryProduct,
                MovieAgeCategory = movieDTO.MovieAgeCategory,
                MovieOriginalLanguage = movieDTO.MovieOriginalLanguage,
                MovieIMDBScore = movieDTO.MovieIMDBScore,
                MovieAuthor = movieDTO.MovieAuthor,
                MovieDirector = movieDTO.MovieDirector,
                MovieDuration = movieDTO.MovieDuration,
                MovieSummary = movieDTO.MovieSummary,
                MovieAbout = movieDTO.MovieAbout,
                MovieBudget = movieDTO.MovieBudget,
                CategoryId = movieDTO.CategoryId,
                GenreIds = movieDTO.GenreIds,
                CartPicturePath = cartPicPath,
                GalleryPicturesPath = galleryPicPath
            };
            string result = _unitOfWork.MovieManagement.AddMovie(movieAndUploadFileDTO);
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
            IEnumerable<AllMovie> allMovieList = _unitOfWork.MovieManagement.GetAllMovie();

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
            (IEnumerable<Movie> movies, string message) = _unitOfWork.MovieManagement.GetMovie(id);
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
                    CategoryId = g.First().CategoryId,
                    GenreTitles = g.Select(m => m.GenreTitle).Distinct().ToList(),
                    GenreIds = g.Select(m => m.GenreId).Distinct().ToList()

                }).FirstOrDefault();
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
