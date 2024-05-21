using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class MiniBannerAndFile
    {
        public Guid MiniBannerId { get; set; }
        public string MiniBannerName { get; set; }
        public string MiniBannerUrl { get; set; }
        public int MiniBannerSort { get; set; }
        public DateTime MiniBannerCreateDate { get; set; }
        public DateTime MiniBannerModifiedDate { get; set; }
        public bool MiniBannerIsStatus { get; set; }
        public bool MiniBannerIsDelete { get; set; }
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public DateTime FileCreateDate { get; set; }
        public DateTime FileModifiedDate { get; set; }
        public bool FileIsDelete { get; set; }
    }
}
