using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string CommentWriter { get; set; }
        public string CommentText { get; set; }
        public int CommentLike { get; set; }
        public int CommentDisLike { get; set; }
        public DateTime CommentCreateDate { get; set; }
        public bool CommentIsStatus { get; set; }
        public bool CommentIsDelete { get; set; }
        public Guid MovieRef { get; set; }
        public Guid ReplyTo { get; set; }
        public bool? IsProfanity { get; set; }
        public string? Feeling { get; set; }

    }
}
