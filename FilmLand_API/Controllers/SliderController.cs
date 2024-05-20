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
        public ActionResult<IEnumerable<SliderAndFile>> GetAllSlider()
        {
            _customLogger.StartAPI("Get All Slider");
            IEnumerable<SliderAndFile> sliderAndFile = _unitOfWork.Slider.GetAllSlider();
            if (sliderAndFile == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get All Slider");
            return Ok(sliderAndFile);
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostSlider([FromForm] SliderDTO sliderDTO)
        {
            _customLogger.StartAPI("Add Slider");
            if (sliderDTO.File == null || sliderDTO.File.Length == 0)
            {
                return BadRequest(new { success = false, message = "No file uploaded" });
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "slider", sliderDTO.File.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await sliderDTO.File.CopyToAsync(stream);
            }
            var fileName = Path.GetFileName(sliderDTO.File.FileName);
            var FileNameWithoutExtension = Path.GetFileNameWithoutExtension(sliderDTO.File.FileName);
            var filePath = "/Slider/";  
            var fileExtension = Path.GetExtension(fileName);

            SliderAndFileDTO sliderAndFileDTO = new SliderAndFileDTO
            {
                SliderName = sliderDTO.SliderName,
                SliderUrl = sliderDTO.SliderUrl,
                SliderSort = sliderDTO.SliderSort,
                FileName = FileNameWithoutExtension,
                FilePath = filePath,
                FileExtension = fileExtension
            };
            string result = _unitOfWork.Slider.AddSlider(sliderAndFileDTO);
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
        public async Task<IActionResult> PutSlider(Guid id, [FromForm] SliderDTO sliderDTO)
        {
            _customLogger.StartAPI("Edit Slider");
            if (sliderDTO.File == null || sliderDTO.File.Length == 0)
            {
                _customLogger.CustomApiError("No file uploaded");
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "slider", sliderDTO.File.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await sliderDTO.File.CopyToAsync(stream);
            }
            var fileName = Path.GetFileName(sliderDTO.File.FileName);
            var FileNameWithoutExtension = Path.GetFileNameWithoutExtension(sliderDTO.File.FileName);
            var filePath = "/Slider/";
            var fileExtension = Path.GetExtension(fileName);

            SliderAndFileDTO sliderAndFileDTO = new SliderAndFileDTO
            {
                SliderName = sliderDTO.SliderName,
                SliderUrl = sliderDTO.SliderUrl,
                SliderSort = sliderDTO.SliderSort,
                FileName = FileNameWithoutExtension,
                FilePath = filePath,
                FileExtension = fileExtension
            };
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
        public ActionResult<SliderAndFile> GetSlider(Guid id)
        {
            _customLogger.StartAPI("Get Slider");
            (SliderAndFile sliderAndFile, string message) = _unitOfWork.Slider.GetSlider(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get Slider");
            return Ok(sliderAndFile);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id:Guid}")]
        public ActionResult DeleteSlider(Guid id)
        {
            _customLogger.StartAPI("Delete Slider");
            (SliderAndFile sliderAndFile, string message) = _unitOfWork.Slider.GetSlider(id);
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
            (SliderAndFile sliderAndFile, string message) = _unitOfWork.Slider.GetSlider(id);
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
