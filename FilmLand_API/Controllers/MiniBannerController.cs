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
    public class MiniBannerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICustomLogger _customLogger;

        public MiniBannerController(IUnitOfWork unitOfWork, ICustomLogger customLogger)
        {
            _unitOfWork = unitOfWork;
            _customLogger = customLogger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<MiniBannerAndFile>> GetAllMiniBanner()
        {
            _customLogger.StartAPI("Get All Mini Banner");
            IEnumerable<MiniBannerAndFile> miniBannerAndFileList = _unitOfWork.MiniBanner.GetAllMiniBanner();
            if (miniBannerAndFileList == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get All Mini Banner");
            return Ok(miniBannerAndFileList);
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostMiniBanner([FromForm] MiniBannerDTO miniBannerDTO)
        {
            _customLogger.StartAPI("Add Mini Banner");
            if (miniBannerDTO.File == null || miniBannerDTO.File.Length == 0)
            {
                _customLogger.CustomApiError("No file uploaded");
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MiniBanner", miniBannerDTO.File.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await miniBannerDTO.File.CopyToAsync(stream);
            }
            var fileName = Path.GetFileName(miniBannerDTO.File.FileName);
            var FileNameWithoutExtension = Path.GetFileNameWithoutExtension(miniBannerDTO.File.FileName);
            var filePath = "/MiniBanner/";
            var fileExtension = Path.GetExtension(fileName);

            MiniBannerAndFileDTO miniBannerAndFileDTO = new MiniBannerAndFileDTO
            {
                MiniBannerName = miniBannerDTO.MiniBannerName,
                MiniBannerUrl = miniBannerDTO.MiniBannerUrl,
                MiniBannerSort = miniBannerDTO.MiniBannerSort,
                FileName = FileNameWithoutExtension,
                FilePath = filePath,
                FileExtension = fileExtension
            };
            string result = _unitOfWork.MiniBanner.AddMiniBanner(miniBannerAndFileDTO);
            if (result == "Success")
            {
                _customLogger.EndAPI("Add Mini Banner");
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
        public async Task<IActionResult> PutMiniBanner(Guid id, [FromForm] MiniBannerDTO miniBannerDTO)
        {
            _customLogger.StartAPI("Edit Mini Banner");
            (MiniBannerAndFile miniBannerAndFile, string message) = _unitOfWork.MiniBanner.GetMiniBanner(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (miniBannerDTO.File == null || miniBannerDTO.File.Length == 0)
            {
                _customLogger.CustomApiError("No file uploaded");
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "minibanner", miniBannerDTO.File.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await miniBannerDTO.File.CopyToAsync(stream);
            }
            var fileName = Path.GetFileName(miniBannerDTO.File.FileName);
            var FileNameWithoutExtension = Path.GetFileNameWithoutExtension(miniBannerDTO.File.FileName);
            var filePath = "/MiniBanner/";
            var fileExtension = Path.GetExtension(fileName);

            MiniBannerAndFileDTO miniBannerAndFileDTO = new MiniBannerAndFileDTO
            {
                MiniBannerName = miniBannerDTO.MiniBannerName,
                MiniBannerUrl = miniBannerDTO.MiniBannerUrl,
                MiniBannerSort = miniBannerDTO.MiniBannerSort,
                FileName = FileNameWithoutExtension,
                FilePath = filePath,
                FileExtension = fileExtension
            };
            string result = _unitOfWork.MiniBanner.UpdateMiniBanner(id, miniBannerAndFileDTO);
            if (result == "Success")
            {
                _customLogger.EndAPI("Edit Mini Banner");
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
        public ActionResult<MiniBannerAndFile> GetMiniBanner(Guid id)
        {
            _customLogger.StartAPI("Get Mini Banner");
            (MiniBannerAndFile miniBannerAndFile, string message) = _unitOfWork.MiniBanner.GetMiniBanner(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _customLogger.EndAPI("Get Mini Banner");
            return Ok(miniBannerAndFile);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id:Guid}")]
        public ActionResult DeleteMiniBanner(Guid id)
        {
            _customLogger.StartAPI("Delete Mini Banner");
            (MiniBannerAndFile miniBannerAndFile, string message) = _unitOfWork.MiniBanner.GetMiniBanner(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            string result = _unitOfWork.MiniBanner.RemoveMiniBanner(id);
            if (result == "Success")
            {
                _customLogger.EndAPI("Delete Mini Banner");
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
            (MiniBannerAndFile miniBannerAndFile, string message) = _unitOfWork.MiniBanner.GetMiniBanner(id);
            if (message == "Not found")
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (message == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            message = _unitOfWork.MiniBanner.ChangeStatus(id);
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
