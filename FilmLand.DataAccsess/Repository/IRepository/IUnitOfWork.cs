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
        IMovieManagementRepository MovieManagement { get; }
        IGenreRepository Genre { get; }
        ICategoryRepository Category { get; }
        IMovieFileRepository MovieFile { get; }
        IActorRepository Actor { get; }
    }
}
