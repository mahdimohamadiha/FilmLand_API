using FilmLand.DataAccsess.Repository;
using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
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

        public SiteMenuController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<MenuSite>> GetAllMenuSite()
        {
            IEnumerable<MenuSite> menuSiteList = _unitOfWork.SiteMenu.GetAllMenuSite();
            if(menuSiteList == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(menuSiteList);
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult PostMenuSite([FromBody] MenuSiteDTO menuSiteDTO)
        {
            string result  = _unitOfWork.SiteMenu.AddMenuSite(menuSiteDTO);
            if (result == "Success") 
            {
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
            if (id != menuSiteDTO.Id)
            {
                return BadRequest();
            }

            string result = _unitOfWork.SiteMenu.UpdateMenuSite(menuSiteDTO);
            if (result == "Success")
            {
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
            MenuSite menuSite = _unitOfWork.SiteMenu.GetMenuSite(id);
            if (menuSite == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(menuSite);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpDelete("{id:int}")]
        public ActionResult DeleteMenuSite(int id)
        {
            MenuSite menuSite = _unitOfWork.SiteMenu.GetMenuSite(id);
            if (menuSite == null)
            {
                return BadRequest();
            }
            string result = _unitOfWork.SiteMenu.RemoveMenuSite(id);
            if (result == "Success")
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
