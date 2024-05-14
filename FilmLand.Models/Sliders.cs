using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class Sliders
    {
        public Guid SliderId { get; set; }
        public string SliderName { get; set; }
        public string SliderUrl { get; set; }
        public int SliderSort { get; set; }
        public Guid FileRef { get; set; }
        public DateTime SliderCreateDate { get; set; }
        public DateTime SliderModifiedDate { get; set; }
        public bool SliderIsStatus { get; set; }
        public bool SliderIsDelete { get; set; }
    }
}
