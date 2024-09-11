using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
using FilmLand.Logs;
using FilmLand.Models;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ICustomLogger _customLogger;
        public CartRepository(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }

        public string AddCart(CartDTO cartDTO)
        {
            Guid cartId = Guid.NewGuid();
            string message = DapperEntities.ExecuteDatabase("INSERT INTO Cart (CartId, CartTitle, CartIsDelete) VALUES (@CartId, @CartTitle, 0);", Connection.FilmLand(), new { CartId = cartId, CartTitle = cartDTO.CartTitle });
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return message;
        }

        public IEnumerable<Cart> GetAllCart()
        {
            (IEnumerable<Cart> siteMenuList, string message) = DapperEntities.QueryDatabase<Cart>("SELECT * FROM Cart", Connection.FilmLand());
            if (message == "Success")
            {
                _customLogger.SuccessDatabase(message);
            }
            else
            {
                _customLogger.ErrorDatabase(message);
            }
            return siteMenuList;
        }
    }
}
