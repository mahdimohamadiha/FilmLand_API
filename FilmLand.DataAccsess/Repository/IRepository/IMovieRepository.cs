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
        (Movie_MoreInfo, string) GetMovie(Guid movieId);
        IEnumerable<Movies> GetMovies(MovieParameterDTO movieParameterDTO);
    }
}
