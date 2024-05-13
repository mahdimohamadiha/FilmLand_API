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
        public IEnumerable<MenuSite> GetAllMenuSite()
        {
            (IEnumerable<MenuSite> menuSiteList, string message) = DapperEntities.QueryDatabase<MenuSite>("SELECT * FROM MenuSites ORDER BY MenuSiteSort", Connection.FilmLand());
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return menuSiteList;
        }

        public string AddMenuSite(MenuSiteDTO menuSiteDTO)
        {
            Guid idMenuSite = Guid.NewGuid();
            string message = DapperEntities.ExecuteDatabase("INSERT INTO MenuSites (MenuSiteId, MenuSiteName, MenuSiteUrl, MenuSiteSort, MenuSiteCreateDate, MenuSiteIsStatus, MenuSiteIsDelete) VALUES (@MenuSiteId, @MenuSiteName, @MenuSiteUrl, @MenuSiteSort, GETDATE(), 1, 0)", Connection.FilmLand(), new { MenuSiteId = idMenuSite, MenuSiteName = menuSiteDTO.MenuSiteName, MenuSiteUrl = menuSiteDTO.MenuSiteUrl, MenuSiteSort = menuSiteDTO.MenuSiteSort });
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

        public string UpdateMenuSite(Guid menuSiteId, MenuSiteDTO menuSiteDTO)
        {
            string message = DapperEntities.ExecuteDatabase("UPDATE MenuSites SET MenuSiteName = @MenuSiteName, MenuSiteUrl = @MenuSiteUrl, MenuSiteSort = @MenuSiteSort, MenuSiteModifiedDate = GETDATE() WHERE MenuSiteId = @MenuSiteId", Connection.FilmLand(), new { MenuSiteId = menuSiteId, Name = menuSiteDTO.MenuSiteName, MenuSiteUrl = menuSiteDTO.MenuSiteUrl, MenuSiteSort = menuSiteDTO.MenuSiteSort });
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
        public (MenuSite, string) GetMenuSite(Guid menuSiteId)
        {
            (IEnumerable<MenuSite> menuSite, string message) = DapperEntities.QueryDatabase<MenuSite>("SELECT * FROM MenuSites WHERE MenuSiteId = @MenuSiteId", Connection.FilmLand(), new { MenuSiteId = menuSiteId });
            if (message == "Success")
            {
                if (menuSite.Count() == 0)
                {
                    _customLogger.CustomDatabaseError("Id was not found in the database");
                    return (null, "Not found");
                }
                else
                {
                    _customLogger.SuccessDatabase(message);
                    return (menuSite.FirstOrDefault(), "Success");
                }
            }
            else
            {
                _customLogger.ErrorDatabase(message);
                return (null, "Error");
            }
        }
        public string RemoveMenuSite(Guid menuSiteId)
        {
            string message = DapperEntities.ExecuteDatabase("DELETE FROM MenuSites WHERE MenuSiteId = @MenuSiteId", Connection.FilmLand(), new { MenuSiteId = menuSiteId });
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
        public string ChangeStatus(Guid menuSiteId)
        {
            string message = DapperEntities.ExecuteDatabase(@"UPDATE MenuSites
                                                              SET MenuSiteIsStatus = 
                                                                  CASE 
                                                                      WHEN MenuSiteIsStatus = 1 THEN 0
                                                                      WHEN MenuSiteIsStatus = 0 THEN 1
                                                                  END
                                                              WHERE MenuSiteId = @MenuSiteId;", 
                                                              Connection.FilmLand(), new { MenuSiteId = menuSiteId });
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
