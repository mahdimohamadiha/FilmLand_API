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
    public class CommentRepository : ICommentRepository
    {
        private readonly ICustomLogger _customLogger;
        public CommentRepository(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }

        public string AddComment(CommentDTO commentDTO)
        {
            Guid idComment = Guid.NewGuid();
            string message = DapperEntities.ExecuteDatabase("INSERT INTO Comment (CommentId, CommentWriter, CommentText, CommentLike, CommentDisLike, CommentCreateDate, CommentIsStatus, CommentIsDelete, MovieRef, ReplyTo, IsProfanity) VALUES (@CommentId, @CommentWriter, @CommentText, 0, 0, GETDATE(), 1, 0, @MovieRef, @ReplyTo, @IsProfanity);", Connection.FilmLand(), new { CommentId = idComment, CommentWriter = commentDTO.CommentWriter, CommentText = commentDTO.CommentText, MovieRef = commentDTO.MovieRef, ReplyTo = commentDTO.ReplyTo , IsProfanity = commentDTO.IsProfanity });
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
        public IEnumerable<Comment> GetComment(Guid idMovie)
        {
            (IEnumerable<Comment> siteMenuList, string message) = DapperEntities.QueryDatabase<Comment>("SELECT * FROM Comment WHERE MovieRef = @MovieRef AND IsProfanity = 0", Connection.FilmLand(), new { MovieRef = idMovie });
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