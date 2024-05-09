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
            (IEnumerable<MenuSite> menuSiteList, string message) = DapperEntities.QueryDatabase<MenuSite>("SELECT * FROM MenuSite", Connection.FilmLand());
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
            string message = DapperEntities.ExecuteDatabase("INSERT INTO MenuSite (Id, Name, Url, Sort, CreateDate, IsStatus, IsDelete) VALUES (@Id, @Name, @Url, @Sort, GETDATE(), 1, 0)", Connection.FilmLand(), new { Id = menuSiteDTO.Id, Name = menuSiteDTO.Name, Url = menuSiteDTO.Url, Sort = menuSiteDTO.Sort });
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

        public string UpdateMenuSite(MenuSiteDTO menuSiteDTO)
        {
            string message = DapperEntities.ExecuteDatabase("UPDATE MenuSite SET Name = @Name, Url = @Url, Sort = @Sort, ModifiedDate = GETDATE() WHERE Id = @Id", Connection.FilmLand(), new { Id = menuSiteDTO.Id, Name = menuSiteDTO.Name, Url = menuSiteDTO.Url, Sort = menuSiteDTO.Sort });
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
        public MenuSite GetMenuSite(int id)
        {
            (IEnumerable<MenuSite> menuSite, string message) = DapperEntities.QueryDatabase<MenuSite>("SELECT * FROM MenuSite WHERE Id = @Id", Connection.FilmLand(), new { Id = id });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return menuSite.FirstOrDefault();
        }
        public string RemoveMenuSite(int id)
        {
            string message = DapperEntities.ExecuteDatabase("DELETE FROM MenuSite WHERE Id = @Id", Connection.FilmLand(), new { Id = id });
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
