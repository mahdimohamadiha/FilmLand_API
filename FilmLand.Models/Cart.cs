using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public string CartTitle { get; set; }
        public bool CartIsDelete { get; set; }

    }
}
