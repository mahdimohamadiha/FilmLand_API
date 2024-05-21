using FilmLand.DataAccsess.Repository;
using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
using FilmLand.Logs;
using FilmLand.Models;
using FilmLand.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmLand_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SiteMenuController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICustomLogger _customLogger;

        public SiteMenuController(IUnitOfWork unitOfWork, ICustomLogger customLogger)
        {
            _unitOfWork = unitOfWork;
            _customLogger = customLogger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<SiteMenu>> GetAllSiteMenu()
        {
            _customLogger.StartAPI("Get All Site Menu");
            IEnumerable<SiteMenu> siteMenuList = _unitOfWork.SiteMenu.GetAllSiteMenu();
            if(siteMenuList == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get All Site Menu");
            return Ok(siteMenuList);
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult PostSiteMenu([FromBody] SiteMenuDTO siteMenuDTO)
        {
            _customLogger.StartAPI("Add Site Menu");
            string result  = _unitOfWork.SiteMenu.AddSiteMenu(siteMenuDTO);
            if (result == "Success") 
            {
                _customLogger.EndAPI("Add Site Menu");
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("Edit/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult PutSiteMenu(Guid id, [FromBody] SiteMenuDTO siteMenuDTO)
        {
            _customLogger.StartAPI("Edit Site Menu");
            (SiteMenu siteMenu, string message) = _unitOfWork.SiteMenu.GetSiteMenu(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            string result = _unitOfWork.SiteMenu.UpdateSiteMenu(id, siteMenuDTO);
            if (result == "Success")
            {
                _customLogger.EndAPI("Edit Site Menu");
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
        public ActionResult<SiteMenu> GetSiteMenu(Guid id)
        {
            _customLogger.StartAPI("Get Site Menu");
            (SiteMenu menuSite, string message) = _unitOfWork.SiteMenu.GetSiteMenu(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get Site Menu");
            return Ok(menuSite);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id:Guid}")]
        public ActionResult DeleteSiteMenu(Guid id)
        {
            _customLogger.StartAPI("Delete Site Menu");
            (SiteMenu siteMenu, string message) = _unitOfWork.SiteMenu.GetSiteMenu(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            string result = _unitOfWork.SiteMenu.RemoveSiteMenu(id);
            if (result == "Success")
            {
                _customLogger.EndAPI("Delete Site Menu");
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("ChangeStatus/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult ChangeStatus(Guid id)
        {
            _customLogger.StartAPI("Change Status");
            (SiteMenu siteMenu, string message) = _unitOfWork.SiteMenu.GetSiteMenu(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            message = _unitOfWork.SiteMenu.ChangeStatus(id);
            if (message == "Success")
            {
                _customLogger.EndAPI("Change Status");
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
