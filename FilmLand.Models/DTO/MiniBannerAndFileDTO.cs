using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models.DTO
{
    public class MiniBannerAndFileDTO
    {
        public string MiniBannerName { get; set; }
        public string MiniBannerUrl { get; set; }
        public int MiniBannerSort { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
    }
}
