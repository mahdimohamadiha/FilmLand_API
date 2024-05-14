﻿using FilmLand.DataAccsess.Repository.IRepository;
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
        public IEnumerable<SliderAndFilePath> GetAllSlider()
        {
            (IEnumerable<SliderAndFilePath> sliderAndFilePath, string message) = DapperEntities.QueryDatabase<SliderAndFilePath>("SELECT * FROM Sliders join Files on FileRef = FileId", Connection.FilmLand());
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return sliderAndFilePath;
        }
        public string AddSlider(SliderAndFileDTO sliderAndFileNameDTO)
        {
            Guid filePathId = Guid.NewGuid();
            Guid sliderId = Guid.NewGuid();
            string message = DapperEntities.ExecuteDatabase(@"
                INSERT INTO Files (FileId, FileName, FilePath, FileExtension, FileCreateDate, FileIsDelete) VALUES (@FileId, @FileName, @FilePath, @FileExtension, GETDATE(), 0); INSERT INTO Sliders (SliderId, SliderName, SliderUrl, SliderSort, FileRef, SliderCreateDate, SliderIsStatus, SliderIsDelete) VALUES (@SliderId, @SliderName, @SliderUrl, @SliderSort, @FileRef, GETDATE(), 1, 0)", 
                Connection.FilmLand(), new { FileId = filePathId, FileName = sliderAndFileNameDTO.FileName, FilePath = sliderAndFileNameDTO.FilePath, FileExtension = sliderAndFileNameDTO.FileExtension, SliderId = sliderId, SliderName = sliderAndFileNameDTO.SliderName, SliderUrl = sliderAndFileNameDTO.SliderUrl, SliderSort = sliderAndFileNameDTO.SliderSort, FileRef = filePathId });
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
            (IEnumerable<Sliders> sliders, string message) = DapperEntities.QueryDatabase<Sliders>("SELECT * FROM Sliders WHERE SliderId = @SliderId", Connection.FilmLand(), new { SliderId = sliderId });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return message;
            }
            message = DapperEntities.ExecuteDatabase("UPDATE Files SET FileName = @FileName, FilePath = @FilePath, FileExtension = @FileExtension, FileModifiedDate = GETDATE() WHERE FileId = @FileId; UPDATE Sliders SET SliderName = @SliderName, SliderUrl = @SliderUrl, SliderSort = @SliderSort, SliderModifiedDate = GETDATE() WHERE SliderId = @SliderId", 
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
    }
}
