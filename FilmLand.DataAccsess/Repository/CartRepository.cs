﻿using FilmLand.DataAccsess.Repository.IRepository;
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
            (IEnumerable<Cart> siteMenuList, string message) = DapperEntities.QueryDatabase<Cart>("SELECT * FROM Cart WHERE CartIsDelete = 0", Connection.FilmLand());
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

        public string AddSingleCart(SingleCartDTO singleCartDTO)
        {
            Guid cartMovieId = Guid.NewGuid();
            string message = DapperEntities.ExecuteDatabase("INSERT INTO CartMovie (CartMovieId, CartMovie_MovieRef, CartMovie_CartRef) VALUES (@CartMovieId, @CartMovie_MovieRef, @CartMovie_CartRef );", Connection.FilmLand(), new { CartMovieId = cartMovieId, CartMovie_MovieRef = singleCartDTO.CartMovie_MovieRef, CartMovie_CartRef = singleCartDTO.CartMovie_CartRef });
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
        public IEnumerable<SingleCarts> GetAllSingleCart()
        {
            (IEnumerable<SingleCarts> siteMenuList, string message) = DapperEntities.QueryDatabase<SingleCarts>("SELECT  CartId,CartTitle,CartMovieId,[MovieId],[MoviePersionName],[MovieEnglishName],[UploadFilePath] FROM [mohamadiha].[dbo].[Movie] join UploadFile on UploadFile_MovieRef = MovieId join CartMovie on MovieId = CartMovie_MovieRef join Cart on CartMovie_CartRef = CartId WHERE UploadFileTitle = 'CartPicture' AND CartIsDelete = 0", Connection.FilmLand());
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

        public string RemoveSingleCart(Guid cartMovieId)
        {
            string message = DapperEntities.ExecuteDatabase("DELETE FROM CartMovie WHERE CartMovieId = @CartMovieId", Connection.FilmLand(), new { CartMovieId = cartMovieId });
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

        public string RemoveCart(Guid cartId)
        {
            string message = DapperEntities.ExecuteDatabase("UPDATE Cart SET CartIsDelete = 1 WHERE CartId = @CartId", Connection.FilmLand(), new { CartId = cartId });
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
    }
}
