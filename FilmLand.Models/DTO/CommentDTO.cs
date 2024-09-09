using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models.DTO
{
    public class CommentDTO
    {
        public string CommentWriter { get; set; }
        public string CommentText { get; set; }
        public Guid MovieRef { get; set; }
        public Guid? ReplyTo { get; set; }

    }
}
