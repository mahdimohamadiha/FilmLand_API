using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class Slider
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Sort { get; set; }
        public Guid PicRef { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
