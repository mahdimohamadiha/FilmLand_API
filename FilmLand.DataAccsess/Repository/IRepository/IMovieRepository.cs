using FilmLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository.IRepository
{
    public interface IMovieRepository
    {
        (Movie_MoreInfo, string) GetMovie(Guid movieId);
    }
}
