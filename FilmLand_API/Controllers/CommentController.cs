using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Logs;
using FilmLand.Models;
using FilmLand.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FilmLand_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICustomLogger _customLogger;

        public CommentController(IUnitOfWork unitOfWork, ICustomLogger customLogger)
        {
            _unitOfWork = unitOfWork;
            _customLogger = customLogger;
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddComment(CommentDTO commentDTO)
        {
            _customLogger.StartAPI("Add Comment");
            string result = _unitOfWork.Comment.AddComment(commentDTO);
            if (result == "Success")
            {
                _customLogger.EndAPI("Add Comment");
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Comment>> GetComment(Guid id)
        {
            _customLogger.StartAPI("Get Comment");
            IEnumerable<Comment> comments = _unitOfWork.Comment.GetComment(id);
            _customLogger.EndAPI("Get Comment");
            return Ok(comments);
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Comment>> GetAllComment(string filter)
        {
            _customLogger.StartAPI("Get All Comment");
            IEnumerable<Comment> comments = _unitOfWork.Comment.GetAllComment(filter);
            _customLogger.EndAPI("Get All Comment");
            return Ok(comments);
        }

        [HttpGet("AllProfanity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Comment>> GetProfanityComment()
        {
            _customLogger.StartAPI("Get AllProfanity Comment");
            IEnumerable<Comment> comments = _unitOfWork.Comment.GetProfanityComment();
            _customLogger.EndAPI("Get AllProfanity Comment");
            return Ok(comments);
        }

        [HttpPut("EditProfanity/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult EditProfanityComment(Guid id)
        {
            _customLogger.StartAPI("Edit Profanity Comment");
            string result = _unitOfWork.Comment.UpdateComment(id);
            if (result == "Success")
            {
                _customLogger.EndAPI("Edit Profanity Comment");
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id:Guid}")]
        public ActionResult DeleteSiteMenu(Guid id)
        {
            _customLogger.StartAPI("Delete Comment");
            string result = _unitOfWork.Comment.RemoveComment(id);
            if (result == "Success")
            {
                _customLogger.EndAPI("Delete Comment");
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("Answer/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult EditAnswer(Guid id)
        {
            _customLogger.StartAPI("Edit Answer Comment");
            string result = _unitOfWork.Comment.UpdateAnswed(id);
            if (result == "Success")
            {
                _customLogger.EndAPI("Edit Answer Comment");
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
