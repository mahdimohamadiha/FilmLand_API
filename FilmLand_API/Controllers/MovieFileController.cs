using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Logs;
using FilmLand.Models;
using FilmLand.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmLand_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieFileController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICustomLogger _customLogger;

        public MovieFileController(IUnitOfWork unitOfWork, ICustomLogger customLogger)
        {
            _unitOfWork = unitOfWork;
            _customLogger = customLogger;
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMovieFile([FromForm] MovieFileDTO movieFileDTO)
        {
            _customLogger.StartAPI("Add Movie File");
            string result = _unitOfWork.MovieFile.AddMovieFile(movieFileDTO);
            if (result == "Success")
            {
                _customLogger.EndAPI("Add Movie File");
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<MovieFile>> GetAllMovieFile(Guid id)
        {
            _customLogger.StartAPI("Get All Movie File");
            IEnumerable<MovieFile> movieFileList = _unitOfWork.MovieFile.GetAllMovieFile(id);
            if (movieFileList == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get All Mvoie File");
            return Ok(movieFileList);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id:Guid}")]
        public ActionResult DeleteMovieFile(Guid id)
        {
            _customLogger.StartAPI("Delete Movie File");
            (MovieFileSummary movieFileSummary, string message) = _unitOfWork.MovieFile.GetMovieFile(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            string result = _unitOfWork.MovieFile.RemoveMovieFile(id);
            if (result == "Success")
            {
                _customLogger.EndAPI("Delete Movie File");
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Single/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MovieFile> GetMovieFile(Guid id)
        {
            _customLogger.StartAPI("Get Movie File");
            (MovieFileSummary movieFileSummary, string message) = _unitOfWork.MovieFile.GetMovieFile(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get Movie File");
            return Ok(movieFileSummary);
        }

        [HttpPut("Edit/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult PutMovieFile(Guid id, [FromBody] MovieFileDTO movieFileDTO)
        {
            _customLogger.StartAPI("Edit Movie File");
            (MovieFileSummary movieFileSummary, string message) = _unitOfWork.MovieFile.GetMovieFile(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            string result = _unitOfWork.MovieFile.UpdateMovieFile(id, movieFileDTO);
            if (result == "Success")
            {
                _customLogger.EndAPI("Edit Movie File");
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
