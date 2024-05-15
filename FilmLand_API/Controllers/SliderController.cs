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
        public ActionResult<IEnumerable<SlidersAndFiles>> GetAllSlider()
        {
            _customLogger.StartAPI("Get All Slider");
            IEnumerable<SlidersAndFiles> sliderAndFilePath = _unitOfWork.Slider.GetAllSlider();
            if (sliderAndFilePath == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get All Slider");
            return Ok(sliderAndFilePath);
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult PostSlider([FromBody] SliderAndFileDTO sliderAndFileNameDTO)
        {
            _customLogger.StartAPI("Add Slider");
            string result = _unitOfWork.Slider.AddSlider(sliderAndFileNameDTO);
            if (result == "Success")
            {
                _customLogger.EndAPI("Add Slider");
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
        public ActionResult PutSlider(Guid id, [FromBody] SliderAndFileDTO sliderAndFileDTO)
        {
            _customLogger.StartAPI("Edit Slider");
            string result = _unitOfWork.Slider.UpdateSlider(id, sliderAndFileDTO);
            if (result == "Success")
            {
                _customLogger.EndAPI("Edit Slider");
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
        public ActionResult<SlidersAndFiles> GetSlider(Guid id)
        {
            _customLogger.StartAPI("Get Slider");
            (SlidersAndFiles slidersAndFiles, string message) = _unitOfWork.Slider.GetSlider(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get Slider");
            return Ok(slidersAndFiles);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id:Guid}")]
        public ActionResult DeleteSlider(Guid id)
        {
            _customLogger.StartAPI("Delete Slider");
            (SlidersAndFiles slidersAndFiles, string message) = _unitOfWork.Slider.GetSlider(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            string result = _unitOfWork.Slider.RemoveSlider(id);
            if (result == "Success")
            {
                _customLogger.EndAPI("Delete Slider");
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
            (SlidersAndFiles slidersAndFiles, string message) = _unitOfWork.Slider.GetSlider(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            message = _unitOfWork.Slider.ChangeStatus(id);
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
