using FilmLand.Models;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository.IRepository
{
    public interface IMovieManagementRepository
    {
        string AddMovie(MovieAndUploadFileDTO movieDTO);
        IEnumerable<AllMovie> GetAllMovie();
        //(IEnumerable<Movie>, string) GetMovie(Guid movieId);
        string RemoveMovie(Guid movieId);
    }
}
