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
    public class ActorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICustomLogger _customLogger;

        public ActorController(IUnitOfWork unitOfWork, ICustomLogger customLogger)
        {
            _unitOfWork = unitOfWork;
            _customLogger = customLogger;
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddActor([FromForm] ActorDTO actorDTO)
        {
            _customLogger.StartAPI("Add Actor");
            if (actorDTO.ActorPicture == null || actorDTO.ActorPicture.Length == 0)
            {
                _customLogger.CustomApiError("No file uploaded");
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Actor", actorDTO.ActorPicture.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await actorDTO.ActorPicture.CopyToAsync(stream);
            }
            var actorPicPath = "/Actor/" + Path.GetFileName(actorDTO.ActorPicture.FileName);
            ActorAndUploadFileDTO actorAndUploadFileDTO = new ActorAndUploadFileDTO
            {
                ActorName = actorDTO.ActorName,
                ActorBirthDay = actorDTO.ActorBirthDay,
                ActorProfession = actorDTO.ActorProfession,
                ActorBio = actorDTO.ActorBio,
                ActorPath = actorPicPath
            };
            string result = _unitOfWork.Actor.AddActor(actorAndUploadFileDTO);
            if (result == "Success")
            {
                _customLogger.EndAPI("Add Actor");
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Summary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ActorSummary>> GetAllActorSummary()
        {
            _customLogger.StartAPI("Get Actor Summary");
            IEnumerable<ActorSummary> allActorSummaryList = _unitOfWork.Actor.GetAllActorSummary();
            if (allActorSummaryList == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get Actor Summary");
            return Ok(allActorSummaryList);
        }
    }
}
