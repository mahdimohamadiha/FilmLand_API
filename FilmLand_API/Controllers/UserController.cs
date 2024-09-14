using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Logs;
using FilmLand.Models;
using FilmLand.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FilmLand_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICustomLogger _customLogger;

        public UserController(IUnitOfWork unitOfWork, ICustomLogger customLogger)
        {
            _unitOfWork = unitOfWork;
            _customLogger = customLogger;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Register([FromBody] RegisterDTO registerDTO)
        {
            _customLogger.StartAPI("Register user");
            (Guid userId ,string result) = _unitOfWork.User.Register(registerDTO);
            if (result == "Success")
            {
                _customLogger.EndAPI("Register user");
                return StatusCode(StatusCodes.Status201Created, userId);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Guid> Login([FromBody] LoginDTO loginDTO)
        {
            _customLogger.StartAPI("Login user");
            (IEnumerable<User> users, string massage) = _unitOfWork.User.Login(loginDTO);
            if (massage == "Success")
            {
                if (users.Count() == 0)
                {
                    _customLogger.CustomApiError("User not found");
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                _customLogger.EndAPI("Login user");
                return Ok(users.FirstOrDefault().UserId);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("LoginAdmin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Guid> LoginAdmin([FromBody] AdminDTO adminDTO)
        {
            _customLogger.StartAPI("Login user");
            (IEnumerable<Admin> admins, string massage) = _unitOfWork.User.LoginAdmin(adminDTO);
            if (massage == "Success")
            {
                if (admins.Count() == 0)
                {
                    _customLogger.CustomApiError("User not found");
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                _customLogger.EndAPI("Login user");
                return Ok(admins.FirstOrDefault().AdminId);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
