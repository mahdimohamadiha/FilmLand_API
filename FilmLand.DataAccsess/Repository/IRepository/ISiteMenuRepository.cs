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
        IEnumerable<SiteMenu> GetAllSiteMenu();
        IEnumerable<SiteMenu> GetAllSiteMenuHomePage();

        string AddSiteMenu(SiteMenuDTO menuSiteDTO);

        string UpdateSiteMenu(Guid id ,SiteMenuDTO menuSitedDTO);

        (SiteMenu, string) GetSiteMenu(Guid id);

        string RemoveSiteMenu(Guid id);

        string ChangeStatus(Guid menuSiteId);
    }
}
