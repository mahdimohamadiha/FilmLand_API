using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models.DTO
{
    public class MiniBannerDTO
    {
        public string MiniBannerName { get; set; }
        public string MiniBannerUrl { get; set; }
        public int MiniBannerSort { get; set; }
        public IFormFile File { get; set; }
    }
}
