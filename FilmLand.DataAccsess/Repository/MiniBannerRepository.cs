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
    public class MiniBannerRepository : IMiniBannerRepository
    {
        private readonly ICustomLogger _customLogger;
        public MiniBannerRepository(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }
        public IEnumerable<MiniBannerAndFile> GetAllMiniBanner()
        {
            (IEnumerable<MiniBannerAndFile> miniBannerAndFileList, string message) = DapperEntities.QueryDatabase<MiniBannerAndFile>("SELECT * FROM MiniBanner join [File] on FileRef = FileId WHERE MiniBannerIsDelete = 0 ORDER BY MiniBannerSort", Connection.FilmLand());
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return miniBannerAndFileList;
        }
        public string AddMiniBanner(MiniBannerAndFileDTO miniBannerAndFileDTO)
        {
            Guid fileId = Guid.NewGuid();
            Guid miniBannerId = Guid.NewGuid();
            string message = DapperEntities.ExecuteDatabase(@"
                INSERT INTO [File] (FileId, FileName, FilePath, FileExtension, FileCreateDate, FileIsDelete) VALUES (@FileId, @FileName, @FilePath, @FileExtension, GETDATE(), 0); INSERT INTO MiniBanner (MiniBannerId, MiniBannerName, MiniBannerUrl, MiniBannerSort, FileRef, MiniBannerCreateDate, MiniBannerIsStatus, MiniBannerIsDelete) VALUES (@MiniBannerId, @MiniBannerName, @MiniBannerUrl, @MiniBannerSort, @FileRef, GETDATE(), 1, 0)",
                Connection.FilmLand(), new { FileId = fileId, FileName = miniBannerAndFileDTO.FileName, FilePath = miniBannerAndFileDTO.FilePath, FileExtension = miniBannerAndFileDTO.FileExtension, MiniBannerId = miniBannerId, MiniBannerName = miniBannerAndFileDTO.MiniBannerName, MiniBannerUrl = miniBannerAndFileDTO.MiniBannerUrl, MiniBannerSort = miniBannerAndFileDTO.MiniBannerSort, FileRef = fileId });
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
        public string UpdateMiniBanner(Guid miniBannerId, MiniBannerAndFileDTO miniBannerAndFileDTO)
        {
            (IEnumerable<MiniBanner> miniBannerList, string message) = DapperEntities.QueryDatabase<MiniBanner>("SELECT * FROM MiniBanner WHERE MiniBannerId = @MiniBannerId", Connection.FilmLand(), new { MiniBannerId = miniBannerId });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return message;
            }
            message = DapperEntities.ExecuteDatabase("UPDATE [File] SET FileName = @FileName, FilePath = @FilePath, FileExtension = @FileExtension, FileModifiedDate = GETDATE() WHERE FileId = @FileId; UPDATE MiniBanner SET MiniBannerName = @MiniBannerName, MiniBannerUrl = @MiniBannerUrl, MiniBannerSort = @MiniBannerSort, MiniBannerModifiedDate = GETDATE() WHERE MiniBannerId = @MiniBannerId",
                Connection.FilmLand(), new { FileName = miniBannerAndFileDTO.FileName, FilePath = miniBannerAndFileDTO.FilePath, FileExtension = miniBannerAndFileDTO.FileExtension, FileId = miniBannerList.FirstOrDefault().FileRef, MiniBannerName = miniBannerAndFileDTO.MiniBannerName, MiniBannerUrl = miniBannerAndFileDTO.MiniBannerUrl, MiniBannerSort = miniBannerAndFileDTO.MiniBannerSort, MiniBannerId = miniBannerId });
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

        public (MiniBannerAndFile, string) GetMiniBanner(Guid miniBannerId)
        {
            (IEnumerable<MiniBannerAndFile> miniBannerAndFileList, string message) = DapperEntities.QueryDatabase<MiniBannerAndFile>("SELECT * FROM MiniBanner join [File] on FileRef = FileId WHERE MiniBannerId = @MiniBannerId", Connection.FilmLand(), new { MiniBannerId = miniBannerId });
            if (message == "Success")
            {
                if (miniBannerAndFileList.Count() == 0)
                {
                    _customLogger.CustomDatabaseError("Id was not found in the database");
                    return (null, "Not found");
                }
                else
                {
                    _customLogger.SuccessDatabase(message);
                    return (miniBannerAndFileList.FirstOrDefault(), "Success");
                }
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return (null, "Error");
            }
        }

        public string RemoveMiniBanner(Guid miniBannerId)
        {
            (IEnumerable<MiniBanner> miniBannerList, string message) = DapperEntities.QueryDatabase<MiniBanner>("SELECT * FROM MiniBanner WHERE MiniBannerId = @MiniBannerId", Connection.FilmLand(), new { MiniBannerId = miniBannerId });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return message;
            }
            message = DapperEntities.ExecuteDatabase("UPDATE MiniBanner SET MiniBannerIsDelete = 1 WHERE MiniBannerId = @MiniBannerId; UPDATE [File] SET FileIsDelete = 1 WHERE FileId = @FileId", Connection.FilmLand(), new { MiniBannerId = miniBannerId, FileId = miniBannerList.FirstOrDefault().FileRef });
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

        public string ChangeStatus(Guid miniBannerId)
        {
            string message = DapperEntities.ExecuteDatabase(@"UPDATE MiniBanner
                                                              SET MiniBannerIsStatus = 
                                                                  CASE 
                                                                      WHEN MiniBannerIsStatus = 1 THEN 0
                                                                      WHEN MiniBannerIsStatus = 0 THEN 1
                                                                  END
                                                              WHERE MiniBannerId = @MiniBannerId;",
                                                              Connection.FilmLand(), new { MiniBannerId = miniBannerId });
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
