using FilmLand.Models;
using FilmLand.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.DataAccsess.Repository.IRepository
{
    public interface ICommentRepository
    {
        string AddComment(CommentDTO commentDTO);
        IEnumerable<Comment> GetComment(Guid idMovie);
        IEnumerable<Comment> GetAllComment(string filter);
    }
}
