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
            MovieManagement = new MovieManagementRepository(customLogger);
            Genre = new GenreRepository(customLogger);
            Category = new CategoryRepository(customLogger);
            MovieFile = new MovieFileRepository(customLogger);
            Actor = new ActorRepository(customLogger);
            Movie = new MovieRepository(customLogger);
            MovieFileDetail = new MovieFileDetailRepository(customLogger);
        }
        public ISiteMenuRepository SiteMenu { get; private set; }
        public ISliderRepository Slider { get; private set; }
        public IMiniBannerRepository MiniBanner { get; private set; }
        public IMovieManagementRepository MovieManagement { get; private set; }
        public IGenreRepository Genre { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IMovieFileRepository MovieFile { get; private set; }
        public IActorRepository Actor { get; private set; }
        public IMovieRepository Movie { get; private set; }
        public IMovieFileDetailRepository MovieFileDetail { get; private set; }
    }
}
