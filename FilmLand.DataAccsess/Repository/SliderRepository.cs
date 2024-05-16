using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
using FilmLand.Logs;
using FilmLand.Models;
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
        public IEnumerable<SliderAndFile> GetAllSlider()
        {
            (IEnumerable<SliderAndFile> sliderAndFile, string message) = DapperEntities.QueryDatabase<SliderAndFile>("SELECT * FROM Slider join [File] on FileRef = FileId WHERE SliderIsDelete = 0 ORDER BY SliderSort", Connection.FilmLand());
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return sliderAndFile;
        }
        public string AddSlider(SliderAndFileDTO sliderAndFileDTO)
        {
            Guid fileId = Guid.NewGuid();
            Guid sliderId = Guid.NewGuid();
            string message = DapperEntities.ExecuteDatabase(@"
                INSERT INTO [File] (FileId, FileName, FilePath, FileExtension, FileCreateDate, FileIsDelete) VALUES (@FileId, @FileName, @FilePath, @FileExtension, GETDATE(), 0); INSERT INTO Slider (SliderId, SliderName, SliderUrl, SliderSort, FileRef, SliderCreateDate, SliderIsStatus, SliderIsDelete) VALUES (@SliderId, @SliderName, @SliderUrl, @SliderSort, @FileRef, GETDATE(), 1, 0)", 
                Connection.FilmLand(), new { FileId = fileId, FileName = sliderAndFileDTO.FileName, FilePath = sliderAndFileDTO.FilePath, FileExtension = sliderAndFileDTO.FileExtension, SliderId = sliderId, SliderName = sliderAndFileDTO.SliderName, SliderUrl = sliderAndFileDTO.SliderUrl, SliderSort = sliderAndFileDTO.SliderSort, FileRef = fileId });
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
        public string UpdateSlider(Guid sliderId, SliderAndFileDTO sliderAndFileDTO)
        {
            (IEnumerable<Slider> sliders, string message) = DapperEntities.QueryDatabase<Slider>("SELECT * FROM Slider WHERE SliderId = @SliderId", Connection.FilmLand(), new { SliderId = sliderId });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return message;
            }
            message = DapperEntities.ExecuteDatabase("UPDATE [File] SET FileName = @FileName, FilePath = @FilePath, FileExtension = @FileExtension, FileModifiedDate = GETDATE() WHERE FileId = @FileId; UPDATE Slider SET SliderName = @SliderName, SliderUrl = @SliderUrl, SliderSort = @SliderSort, SliderModifiedDate = GETDATE() WHERE SliderId = @SliderId", 
                Connection.FilmLand(), new { FileName = sliderAndFileDTO.FileName, FilePath = sliderAndFileDTO.FilePath, FileExtension = sliderAndFileDTO.FileExtension, FileId = sliders.FirstOrDefault().FileRef, SliderName = sliderAndFileDTO.SliderName, SliderUrl = sliderAndFileDTO.SliderUrl, SliderSort = sliderAndFileDTO.SliderSort, SliderId = sliderId });
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
        public (SliderAndFile, string) GetSlider(Guid sliderId)
        {
            (IEnumerable<SliderAndFile> sliderAndFileList, string message) = DapperEntities.QueryDatabase<SliderAndFile>("SELECT * FROM Slider join [File] on FileRef = FileId WHERE SliderId = @SliderId", Connection.FilmLand(), new { SliderId = sliderId });
            if (message == "Success")
            {
                if (sliderAndFileList.Count() == 0)
                {
                    _customLogger.CustomDatabaseError("Id was not found in the database");
                    return (null, "Not found");
                }
                else
                {
                    _customLogger.SuccessDatabase(message);
                    return (sliderAndFileList.FirstOrDefault(), "Success");
                }
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return (null, "Error");
            }
        }
        public string RemoveSlider(Guid sliderId)
        {
            (IEnumerable<Slider> sliders, string message) = DapperEntities.QueryDatabase<Slider>("SELECT * FROM Slider WHERE SliderId = @SliderId", Connection.FilmLand(), new { SliderId = sliderId });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return message;
            }
            message = DapperEntities.ExecuteDatabase("UPDATE Slider SET SliderIsDelete = 1 WHERE SliderId = @SliderId; UPDATE [File] SET FileIsDelete = 1 WHERE FileId = @FileId", Connection.FilmLand(), new { SliderId = sliderId, FileId = sliders.FirstOrDefault().FileRef });;
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
        public string ChangeStatus(Guid sliderId)
        {
            string message = DapperEntities.ExecuteDatabase(@"UPDATE Slider
                                                              SET SliderIsStatus = 
                                                                  CASE 
                                                                      WHEN SliderIsStatus = 1 THEN 0
                                                                      WHEN SliderIsStatus = 0 THEN 1
                                                                  END
                                                              WHERE SliderId = @SliderId;",
                                                              Connection.FilmLand(), new { SliderId = sliderId });
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
