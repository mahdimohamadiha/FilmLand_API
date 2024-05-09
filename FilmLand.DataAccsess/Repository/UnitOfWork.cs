using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Logs;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ICustomLogger customLogger)
        {
            SiteMenu = new SiteMenuRepository(customLogger);
        }

        public ISiteMenuRepository SiteMenu { get; private set; }
    }
}
