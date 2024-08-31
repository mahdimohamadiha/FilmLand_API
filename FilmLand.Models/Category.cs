using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public DateTime CategoryCreateDate { get; set; }
        public DateTime CategoryModifiedDate { get; set; }
        public bool CategoryIsStatus { get; set; }
        public bool CategoryIsDelete { get; set; }
        public string CategoryParameter { get; set; }
        public int CategoryOrder { get; set; }

    }
}
