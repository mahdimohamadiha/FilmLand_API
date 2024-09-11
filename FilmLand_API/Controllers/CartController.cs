﻿using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Logs;
using FilmLand.Models;
using FilmLand.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmLand_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICustomLogger _customLogger;

        public CartController(IUnitOfWork unitOfWork, ICustomLogger customLogger)
        {
            _unitOfWork = unitOfWork;
            _customLogger = customLogger;
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult PostCart(CartDTO cartDTO)
        {
            _customLogger.StartAPI("Add Cart");
            string result = _unitOfWork.Cart.AddCart(cartDTO);
            if (result == "Success")
            {
                _customLogger.EndAPI("Add Cart");
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Movies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<SiteMenu>> GetAllSiteMenu([FromQuery] string searchQuery)
        {
            _customLogger.StartAPI("Get All Cart");
            MovieParameterDTO obj = new MovieParameterDTO
            {
                CategoryParameter = "all",
                GenreParameter = "all",
            };
            IEnumerable<Movies> movies = _unitOfWork.Movie.GetMovies(obj);
            if (movies == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var searchWords = searchQuery
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(word => word.Trim().ToLower())
                .ToHashSet();

            var filteredActorSummaries = movies
                .Where(movie =>
                {
                    var actorNameWords = movie.MovieEnglishName
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(word => word.Trim().ToLower());

                    return searchWords.Any(searchWord =>
                        actorNameWords.Any(actorNameWord =>
                            actorNameWord.StartsWith(searchWord)));
                })
                .ToList();
            _customLogger.EndAPI("Get All Cart");
            return Ok(filteredActorSummaries);
        }
    }
}
