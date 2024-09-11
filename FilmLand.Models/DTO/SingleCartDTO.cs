using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models.DTO
{
    public class SingleCartDTO
    {
        public Guid CartMovie_MovieRef {  get; set; }
        public Guid CartMovie_CartRef { get; set; }

    }
}
