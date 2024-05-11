using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models.DTO
{
    public class SliderAndFileNameDTO
    {
        public string SliderName { get; set; }
        public string SliderUrl { get; set; }
        public int SliderSort { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
    }
}
