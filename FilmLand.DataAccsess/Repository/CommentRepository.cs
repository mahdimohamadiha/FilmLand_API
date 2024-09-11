using FilmLand.DataAccsess.Repository.IRepository;
using FilmLand.Database;
using FilmLand.Logs;
using FilmLand.Models;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
            string message = DapperEntities.ExecuteDatabase("INSERT INTO Comment (CommentId, CommentWriter, CommentText, CommentLike, CommentDisLike, CommentCreateDate, CommentIsStatus, CommentIsDelete, MovieRef, ReplyTo, IsProfanity, Feeling, IsAnswered) VALUES (@CommentId, @CommentWriter, @CommentText, 0, 0, GETDATE(), 1, 0, @MovieRef, @ReplyTo, @IsProfanity, @Feeling, @IsAnswered);", Connection.FilmLand(), new { CommentId = idComment, CommentWriter = commentDTO.CommentWriter, CommentText = commentDTO.CommentText, MovieRef = commentDTO.MovieRef, ReplyTo = commentDTO.ReplyTo , IsProfanity = commentDTO.IsProfanity, Feeling = commentDTO.Feeling, IsAnswered = commentDTO.IsAnswered });
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
            (IEnumerable<Comment> siteMenuList, string message) = DapperEntities.QueryDatabase<Comment>("SELECT * FROM Comment WHERE MovieRef = @MovieRef AND IsProfanity = 0 AND CommentIsDelete = 0", Connection.FilmLand(), new { MovieRef = idMovie });
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

        public IEnumerable<Comment> GetAllComment(string filter)
        {
            (IEnumerable<Comment> siteMenuList, string message) = DapperEntities.QueryDatabase<Comment>("SELECT *\r\nFROM Comment\r\nWHERE IsProfanity = 0\r\n AND CommentIsDelete = 0 AND ReplyTo IS NULL  AND CommentCreateDate >= DATEADD(" + filter + ", -1, GETDATE())\r\nORDER BY CommentCreateDate DESC ", Connection.FilmLand());
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

        public IEnumerable<Comment> GetProfanityComment()
        {
            (IEnumerable<Comment> siteMenuList, string message) = DapperEntities.QueryDatabase<Comment>("SELECT * FROM Comment WHERE IsProfanity = 1 AND CommentIsDelete = 0", Connection.FilmLand());
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

        public string UpdateComment(Guid commentId)
        {
            string message = DapperEntities.ExecuteDatabase("UPDATE Comment\r\nSET IsProfanity = 0\r\nWHERE CommentId = @CommentId", Connection.FilmLand(), new { CommentId = commentId });
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

        public string RemoveComment(Guid commentId)
        {
            string message = DapperEntities.ExecuteDatabase("UPDATE Comment\r\nSET CommentIsDelete = 1\r\nWHERE CommentId = @CommentId;", Connection.FilmLand(), new { CommentId = commentId });
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