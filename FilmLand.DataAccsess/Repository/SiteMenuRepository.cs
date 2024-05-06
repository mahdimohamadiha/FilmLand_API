using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
using FilmLand.Models;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository
{
    public class SiteMenuRepository : ISiteMenuRepository
    {
        public IEnumerable<MenuSite> GetAllSiteMenu()
        {
            IEnumerable<MenuSite> menuSite = DapperEntities.QueryDatabase<MenuSite>("SELECT * FROM MenuSite", Connection.FilmLand());
            return menuSite;
        }

        public string AddSiteMenu(MenuSiteDTO menuSitedDTO)
        {
            string result = DapperEntities.ExecuteDatabase("INSERT INTO MenuSite (Id, Name, Url, Sort, CreateDate, IsStatus, IsDelete) VALUES (@Id, @Name, @Url, @Sort, GETDATE(), 1, 0)", Connection.FilmLand(), new { Id = menuSitedDTO.Id, Name = menuSitedDTO.Name, Url = menuSitedDTO.Url, Sort = menuSitedDTO.Sort });
            return result;
        }

        public string UpdateSiteMenu(MenuSiteDTO menuSitedDTO)
        {
            string result = DapperEntities.ExecuteDatabase("UPDATE MenuSite SET Name = @Name, Url = @Url, Sort = @Sort, ModifiedDate = GETDATE() WHERE Id = @Id", Connection.FilmLand(), new { Id = menuSitedDTO.Id, Name = menuSitedDTO.Name, Url = menuSitedDTO.Url, Sort = menuSitedDTO.Sort });
            return result;
        }
    }
}
