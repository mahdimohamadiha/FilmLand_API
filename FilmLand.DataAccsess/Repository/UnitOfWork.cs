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
            Slider = new SliderRepository(customLogger);
            MiniBanner = new MiniBannerRepository(customLogger);
            Movie = new MovieRepository(customLogger);
        }

        public ISiteMenuRepository SiteMenu { get; private set; }
        public ISliderRepository Slider { get; private set; }
        public IMiniBannerRepository MiniBanner { get; private set; }
        public IMovieRepository Movie { get; private set; }

    }
}
