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

        string UpdateMenuSite(Guid id ,MenuSiteDTO menuSitedDTO);

        (MenuSite, string) GetMenuSite(Guid id);

        string RemoveMenuSite(Guid id);

        string ChangeStatus(Guid menuSiteId);
    }
}
