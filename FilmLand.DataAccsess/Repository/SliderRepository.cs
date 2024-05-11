using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
using FilmLand.Logs;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository
{
    public class SliderRepository : ISliderRepository
    {
        private readonly ICustomLogger _customLogger;
        public SliderRepository(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }
        public string AddSlider(SliderAndFileNameDTO sliderAndFileNameDTO)
        {
            Guid idFilePath = Guid.NewGuid();
            string message = DapperEntities.ExecuteDatabase("INSERT INTO FilePath (Id, Name, Path, Extension, CreateDate, IsDelete) VALUES (@Id, @Name, @Path, @Extension, GETDATE(), 0)", Connection.FilmLand(), new { Id = idFilePath, Name = sliderAndFileNameDTO.FileName, Path = sliderAndFileNameDTO.FilePath, Extension = sliderAndFileNameDTO.FileExtension });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return message;
            }
            Guid idSlider = Guid.NewGuid();
            message = DapperEntities.ExecuteDatabase("INSERT INTO Slider (Id, Name, Url, Sort, PicRef, CreateDate, IsStatus, IsDelete) VALUES (@Id, @Name, @Url, @Sort, @PicRef, GETDATE(), 1, 0)", Connection.FilmLand(), new { Id = idSlider, Name = sliderAndFileNameDTO.SliderName, Url = sliderAndFileNameDTO.SliderUrl, Sort = sliderAndFileNameDTO.SliderSort, PicRef = idFilePath });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return message;
        }
    }
}
