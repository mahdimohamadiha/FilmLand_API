﻿using FilmLand.DataAccsess.Repository.IRepository;
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
            Genre = new GenreRepository(customLogger);
            Category = new CategoryRepository(customLogger);
            MovieFile = new MovieFileRepository(customLogger);
        }
        public ISiteMenuRepository SiteMenu { get; private set; }
        public ISliderRepository Slider { get; private set; }
        public IMiniBannerRepository MiniBanner { get; private set; }
        public IMovieRepository Movie { get; private set; }
        public IGenreRepository Genre { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IMovieFileRepository MovieFile { get; private set; }
    }
}
