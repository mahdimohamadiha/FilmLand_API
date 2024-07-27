using FilmLand.Models;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository.IRepository
{
    public interface IMovieRepository
    {
        string AddMovie(MovieDTO movieDTO);
        IEnumerable<AllMovie> GetAllMovie();
    }
}
