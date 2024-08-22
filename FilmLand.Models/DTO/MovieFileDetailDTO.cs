using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models.DTO
{
    public class MovieFileDetailDTO
    {
        public int MovieFileQuality { get; set; }
        public string MovieFile_MovieURL { get; set; }
        public Guid MovieFileRef {  get; set; }
    }
}
