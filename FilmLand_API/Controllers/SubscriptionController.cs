using FilmLand.DataAccsess.Repository;
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
    public class SubscriptionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICustomLogger _customLogger;

        public SubscriptionController(IUnitOfWork unitOfWork, ICustomLogger customLogger)
        {
            _unitOfWork = unitOfWork;
            _customLogger = customLogger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Subscription>> GetAllSubscription()
        {
            _customLogger.StartAPI("Get All Subscription");
            IEnumerable<Subscription> Subscriptions = _unitOfWork.Subscription.GetAllSubscription();
            if (Subscriptions == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get All Subscription");
            return Ok(Subscriptions);
        }

        [HttpPut("Buy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutSubscription(UserAndSubscriptionDTO userAndSubDTO)
        {
            _customLogger.StartAPI("Buy Subscription");
            (User user, string message) = _unitOfWork.User.GetUser(userAndSubDTO.UserId);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            string result = _unitOfWork.Subscription.UpdateUser(userAndSubDTO.UserId, userAndSubDTO.SubscriptionId);
            if (result == "Success")
            {
                _customLogger.EndAPI("Buy Subscription");
                return NoContent();
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
        public ActionResult<SubscriptionSummary> GetSubscription(Guid id)
        {
            _customLogger.StartAPI("Get Subscription");
            (SubscriptionSummary subscriptionSummary, string message) = _unitOfWork.Subscription.GetSubscription(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get Mini Banner");
            return Ok(subscriptionSummary);
        }

        [HttpGet("Check/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<User> GetSubscriptionUser(Guid id)
        {
            _customLogger.StartAPI("Get Subscription User");
            IEnumerable<User> Subscription = _unitOfWork.Subscription.GetSubscriptionUser(id);
            if (Subscription.FirstOrDefault().SubscriptionRef == Guid.Empty)
            {
                return Ok("false");
            }
            _customLogger.EndAPI("Get Subscription User");
            return Ok("true");
        }
    }


}
