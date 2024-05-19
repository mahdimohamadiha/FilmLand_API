using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models.DTO
{
    public class SliderDTO
    {
        public string SliderName { get; set; }
        public string SliderUrl { get; set; }
        public int SliderSort { get; set; }
        public IFormFile File { get; set; }
    }
}
