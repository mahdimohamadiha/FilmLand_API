using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
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
        private readonly ILogger<UnitOfWork> _logger;
        public SiteMenuRepository(ILogger<UnitOfWork> logger)
        {
            _logger = logger;
        }
        public IEnumerable<MenuSite> GetAllMenuSite()
        {
            (IEnumerable<MenuSite> menuSiteList, string message) = DapperEntities.QueryDatabase<MenuSite>("SELECT * FROM MenuSite", Connection.FilmLand());
            if (message.Contains("Error"))
            {
                _logger.LogError(message);
            }

            return menuSiteList;
        }

        public string AddMenuSite(MenuSiteDTO menuSiteDTO)
        {
            string result = DapperEntities.ExecuteDatabase("INSERT INTO MenuSite (Id, Name, Url, Sort, CreateDate, IsStatus, IsDelete) VALUES (@Id, @Name, @Url, @Sort, GETDATE(), 1, 0)", Connection.FilmLand(), new { Id = menuSiteDTO.Id, Name = menuSiteDTO.Name, Url = menuSiteDTO.Url, Sort = menuSiteDTO.Sort });
            return result;
        }

        public string UpdateMenuSite(MenuSiteDTO menuSiteDTO)
        {
            string result = DapperEntities.ExecuteDatabase("UPDATE MenuSite SET Name = @Name, Url = @Url, Sort = @Sort, ModifiedDate = GETDATE() WHERE Id = @Id", Connection.FilmLand(), new { Id = menuSiteDTO.Id, Name = menuSiteDTO.Name, Url = menuSiteDTO.Url, Sort = menuSiteDTO.Sort });
            return result;
        }
        public MenuSite GetMenuSite(int id)
        {
            (IEnumerable<MenuSite> menuSite, string message) = DapperEntities.QueryDatabase<MenuSite>("SELECT * FROM MenuSite WHERE Id = @Id", Connection.FilmLand(), new { Id = id });
            if (message.Contains("Error"))
            {
                _logger.LogError(message);
            }
            return menuSite.FirstOrDefault();
        }
        public string RemoveMenuSite(int id)
        {
            string result = DapperEntities.ExecuteDatabase("DELETE FROM MenuSite WHERE Id = @Id", Connection.FilmLand(), new { Id = id });
            return result;
        }
    }
}
