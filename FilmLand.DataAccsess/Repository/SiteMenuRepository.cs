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
        public List<MenuSite> GetAllSiteMenu()
        {
            var menuSite = DapperEntities.ReturnList<MenuSite>("SELECT * FROM MenuSite", Connection.FilmLand());
            return menuSite.ToList();
        }

        public string AddSiteMenu(MenuSiteDTO menuSitedDTO)
        {
            string result = DapperEntities.Insert("INSERT INTO MenuSite (Id, Name, Url, Sort, CreateDate, IsStatus, IsDelete) VALUES (@Id, @Name, @Url, @Sort, GETDATE(), 1, 0)", Connection.FilmLand(), new { Id = menuSitedDTO.Id, Name = menuSitedDTO.Name, Url = menuSitedDTO.Url, Sort = menuSitedDTO.Sort });
            return result;
        }
    }
}
