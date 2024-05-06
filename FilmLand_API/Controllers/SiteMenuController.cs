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
        public ActionResult<IEnumerable<MenuSite>> GetMenuSite()
        {
            IEnumerable<MenuSite> result = _unitOfWork.SiteMenu.GetAllSiteMenu();
            if(result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(result);
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> PostMenuSite([FromBody] MenuSiteDTO menuSiteDTO)
        {
            string result  = _unitOfWork.SiteMenu.AddSiteMenu(menuSiteDTO);
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
        public IActionResult PutMenuSite(int id, [FromBody] MenuSiteDTO menuSiteDTO)
        {
            if (id != menuSiteDTO.Id)
            {
                return BadRequest();
            }

            string result = _unitOfWork.SiteMenu.UpdateSiteMenu(menuSiteDTO);
            if (result == "Success")
            {
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}
