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
    public class SliderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICustomLogger _customLogger;

        public SliderController(IUnitOfWork unitOfWork, ICustomLogger customLogger)
        {
            _unitOfWork = unitOfWork;
            _customLogger = customLogger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<SliderAndFilePath>> GetAllMenuSite()
        {
            _customLogger.StartAPI("Get All Menu Site");
            IEnumerable<SliderAndFilePath> sliderAndFilePath = _unitOfWork.Slider.GetAllSlider();
            if (sliderAndFilePath == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get All Menu Site");
            return Ok(sliderAndFilePath);
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult PostSlider([FromBody] SliderAndFileNameDTO sliderAndFileNameDTO)
        {
            _customLogger.StartAPI("Add Menu Site");
            string result = _unitOfWork.Slider.AddSlider(sliderAndFileNameDTO);
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
    }
}
