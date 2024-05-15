using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class SlidersAndFiles
    {
        public Guid SliderId { get; set; }
        public string SliderName { get; set; }
        public string SliderUrl { get; set; }
        public int SliderSort { get; set; }
        public DateTime SliderCreateDate { get; set; }
        public DateTime SliderModifiedDate { get; set; }
        public bool SliderIsStatus { get; set; }
        public bool SliderIsDelete { get; set; }
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public DateTime FileCreateDate { get; set; }
        public DateTime FileModifiedDate { get; set; }
        public bool FileIsDelete { get; set; }
        
    }
}
