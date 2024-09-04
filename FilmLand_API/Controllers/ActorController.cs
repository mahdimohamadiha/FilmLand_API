using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Logs;
using FilmLand.Models;
using FilmLand.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;

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

        [HttpGet("Filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ActorSummary>> GetFilterActorSummary([FromQuery] string searchQuery)
        {
            _customLogger.StartAPI("Get Filter Actor Summary");

            // Fetch all actor summaries
            IEnumerable<ActorSummary> allActorSummaryList = _unitOfWork.Actor.GetAllActorSummary();

            // Check for null
            if (allActorSummaryList == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            // Split the search query into individual words
            var searchWords = searchQuery
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(word => word.Trim().ToLower())
                .ToHashSet(); // Use a HashSet for fast lookups

            // Filter the actor summaries
            var filteredActorSummaries = allActorSummaryList
                .Where(actor =>
                {
                    // Split actor's name into words and convert to lowercase
                    var actorNameWords = actor.ActorName
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(word => word.Trim().ToLower());

                    // Check if any word in actor's name starts with any of the search terms
                    return searchWords.Any(searchWord =>
                        actorNameWords.Any(actorNameWord =>
                            actorNameWord.StartsWith(searchWord)));
                })
                .ToList();

            // Log the end of the API processing
            _customLogger.EndAPI("Get Filter Actor Summary");

            // Return the filtered results
            return Ok(filteredActorSummaries);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Actor> GetActor(Guid id)
        {
            _customLogger.StartAPI("Get Actor");
            (Actor actor, string message) = _unitOfWork.Actor.GetActor(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get Actor");
            return Ok(actor);
        }


    }
}
