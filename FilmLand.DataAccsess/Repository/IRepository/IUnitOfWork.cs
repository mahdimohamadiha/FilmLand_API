using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ISiteMenuRepository SiteMenu { get; }
        ISliderRepository Slider { get; }
        IMiniBannerRepository MiniBanner { get; }
        IMovieRepository Movie { get; }
        IGenreRepository Genre { get; }
        ICategoryRepository Category { get; }
    }
}
