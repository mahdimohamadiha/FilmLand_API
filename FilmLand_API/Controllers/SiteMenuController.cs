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
        public ActionResult<IEnumerable<MenuSite>> GetAllMenuSite()
        {
            _customLogger.StartAPI("Get All Menu Site");
            IEnumerable<MenuSite> menuSiteList = _unitOfWork.SiteMenu.GetAllMenuSite();
            if(menuSiteList == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get All Menu Site");
            return Ok(menuSiteList);
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult PostMenuSite([FromBody] MenuSiteDTO menuSiteDTO)
        {
            _customLogger.StartAPI("Add Menu Site");
            string result  = _unitOfWork.SiteMenu.AddMenuSite(menuSiteDTO);
            if (result == "Success") 
            {
                _customLogger.EndAPI("Add Menu Site");
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("Edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult PutMenuSite(int id, [FromBody] MenuSiteDTO menuSiteDTO)
        {
            _customLogger.StartAPI("Edit Menu Site");
            if (id != menuSiteDTO.Id)
            {
                _customLogger.CustomApiError("parameter id and body id do not match");
                return BadRequest();
            }
            string result = _unitOfWork.SiteMenu.UpdateMenuSite(menuSiteDTO);
            if (result == "Success")
            {
                _customLogger.EndAPI("Edit Menu Site");
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MenuSite> GetMenuSite(int id)
        {
            _customLogger.StartAPI("Get Menu Site");
            MenuSite menuSite = _unitOfWork.SiteMenu.GetMenuSite(id);
            if (menuSite == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get Menu Site");
            return Ok(menuSite);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpDelete("{id:int}")]
        public ActionResult DeleteMenuSite(int id)
        {
            _customLogger.StartAPI("Delete Menu Site");
            MenuSite menuSite = _unitOfWork.SiteMenu.GetMenuSite(id);
            if (menuSite == null)
            {
                _customLogger.CustomApiError("Id does not exist in the database");
                return BadRequest();
            }
            string result = _unitOfWork.SiteMenu.RemoveMenuSite(id);
            if (result == "Success")
            {
                _customLogger.EndAPI("Delete Menu Site");
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
