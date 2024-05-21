using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class MiniBanner
    {
        public Guid MiniBannerId { get; set; }
        public string MiniBannerName { get; set; }
        public string MiniBannerUrl { get; set; }
        public int MiniBannerSort { get; set; }
        public Guid FileRef { get; set; }
        public DateTime MiniBannerCreateDate { get; set; }
        public DateTime MiniBannerModifiedDate { get; set; }
        public bool MiniBannerIsStatus { get; set; }
        public bool MiniBannerIsDelete { get; set; }
    }
}
