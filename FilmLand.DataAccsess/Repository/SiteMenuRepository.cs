using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
using FilmLand.Models;
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
    }
}
