using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class SingleCarts
    {
        public Guid CartId { get; set; }
        public string CartTitle { get; set; }
        public Guid MovieId { get; set; }
        public string MoviePersionName { get; set; }
        public string MovieEnglishName { get; set; }
        public string UploadFilePath { get; set; }

    }
}
