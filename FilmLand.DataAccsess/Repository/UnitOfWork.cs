using FilmLand.DataAccsess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork()
        {
            SiteMenu = new SiteMenuRepository();
        }

        public ISiteMenuRepository SiteMenu { get; private set; }
    }
}
