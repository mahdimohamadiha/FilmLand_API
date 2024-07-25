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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICustomLogger _customLogger;
        public CategoryRepository(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }
        public IEnumerable<Category> GetAllCategory()
        {
            (IEnumerable<Category> categoryList, string message) = DapperEntities.QueryDatabase<Category>("SELECT * FROM Category WHERE CategoryIsDelete = 0", Connection.FilmLand());
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return categoryList;
        }
    }
}
