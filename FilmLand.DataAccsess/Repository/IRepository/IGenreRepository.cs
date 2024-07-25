using FilmLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository.IRepository
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetAllGenre();
    }
}
