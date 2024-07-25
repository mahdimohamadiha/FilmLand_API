using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
using FilmLand.Logs;
using FilmLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ICustomLogger _customLogger;
        public GenreRepository(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }

        public IEnumerable<Genre> GetAllGenre()
        {
            (IEnumerable<Genre> genreList, string message) = DapperEntities.QueryDatabase<Genre>("SELECT * FROM Genre WHERE GenreIsDelete = 0", Connection.FilmLand());
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return genreList;
        }
    }
}
