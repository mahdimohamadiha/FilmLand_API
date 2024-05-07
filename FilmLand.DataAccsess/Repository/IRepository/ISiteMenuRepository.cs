using FilmLand.Models;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository.IRepository
{
    public interface ISiteMenuRepository
    {
        IEnumerable<MenuSite> GetAllMenuSite();

        string AddMenuSite(MenuSiteDTO menuSiteDTO);

        string UpdateMenuSite(MenuSiteDTO menuSitedDTO);

        MenuSite GetMenuSite(int id);

        string RemoveMenuSite(int id);
    }
}
