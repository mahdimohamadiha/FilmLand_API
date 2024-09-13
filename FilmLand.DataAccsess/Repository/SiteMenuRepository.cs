using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
using FilmLand.Logs;
using FilmLand.Models;
using FilmLand.Models.DTO;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository
{
    public class SiteMenuRepository : ISiteMenuRepository
    {
        private readonly ICustomLogger _customLogger;
        public SiteMenuRepository(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }
        public IEnumerable<SiteMenu> GetAllSiteMenu()
        {
            (IEnumerable<SiteMenu> siteMenuList, string message) = DapperEntities.QueryDatabase<SiteMenu>("SELECT * FROM SiteMenu WHERE SiteMenuIsDelete = 0 ORDER BY SiteMenuSort", Connection.FilmLand());
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return siteMenuList;
        }

        public IEnumerable<SiteMenu> GetAllSiteMenuHomePage()
        {
            (IEnumerable<SiteMenu> siteMenuList, string message) = DapperEntities.QueryDatabase<SiteMenu>("SELECT * FROM SiteMenu WHERE SiteMenuIsDelete = 0 AND SiteMenuIsStatus = 1 ORDER BY SiteMenuSort", Connection.FilmLand());
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return siteMenuList;
        }

        public string AddSiteMenu(SiteMenuDTO siteMenuDTO)
        {
            Guid idSiteMenu = Guid.NewGuid();
            string message = DapperEntities.ExecuteDatabase("INSERT INTO SiteMenu (SiteMenuId, SiteMenuName, SiteMenuUrl, SiteMenuSort, SiteMenuCreateDate, SiteMenuIsStatus, SiteMenuIsDelete) VALUES (@SiteMenuId, @SiteMenuName, @SiteMenuUrl, @SiteMenuSort, GETDATE(), 1, 0)", Connection.FilmLand(), new { SiteMenuId = idSiteMenu, SiteMenuName = siteMenuDTO.SiteMenuName, SiteMenuUrl = siteMenuDTO.SiteMenuUrl, SiteMenuSort = siteMenuDTO.SiteMenuSort });
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

        public string UpdateSiteMenu(Guid siteMenuId, SiteMenuDTO siteMenuDTO)
        {
            string message = DapperEntities.ExecuteDatabase("UPDATE SiteMenu SET SiteMenuName = @SiteMenuName, SiteMenuUrl = @SiteMenuUrl, SiteMenuSort = @SiteMenuSort, SiteMenuModifiedDate = GETDATE() WHERE SiteMenuId = @SiteMenuId", Connection.FilmLand(), new { SiteMenuId = siteMenuId, SiteMenuName = siteMenuDTO.SiteMenuName, SiteMenuUrl = siteMenuDTO.SiteMenuUrl, SiteMenuSort = siteMenuDTO.SiteMenuSort });
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
        public (SiteMenu, string) GetSiteMenu(Guid siteMenuId)
        {
            (IEnumerable<SiteMenu> siteMenu, string message) = DapperEntities.QueryDatabase<SiteMenu>("SELECT * FROM SiteMenu WHERE SiteMenuId = @SiteMenuId", Connection.FilmLand(), new { SiteMenuId = siteMenuId });
            if (message == "Success")
            {
                if (siteMenu.Count() == 0)
                {
                    _customLogger.CustomDatabaseError("Id was not found in the database");
                    return (null, "Not found");
                }
                else
                {
                    _customLogger.SuccessDatabase(message);
                    return (siteMenu.FirstOrDefault(), "Success");
                }
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return (null, "Error");
            }
        }
        public string RemoveSiteMenu(Guid siteMenuId)
        {
            string message = DapperEntities.ExecuteDatabase("UPDATE SiteMenu SET SiteMenuIsDelete = 1 WHERE SiteMenuId = @SiteMenuId", Connection.FilmLand(), new { SiteMenuId = siteMenuId });
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
        public string ChangeStatus(Guid siteMenuId)
        {
            string message = DapperEntities.ExecuteDatabase(@"UPDATE SiteMenu
                                                              SET SiteMenuIsStatus = 
                                                                  CASE 
                                                                      WHEN SiteMenuIsStatus = 1 THEN 0
                                                                      WHEN SiteMenuIsStatus = 0 THEN 1
                                                                  END
                                                              WHERE SiteMenuId = @SiteMenuId;", 
                                                              Connection.FilmLand(), new { SiteMenuId = siteMenuId });
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
