using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class Admin
    {
        public Guid AdminId { get; set; }
        public string Adminname { get; set; }
        public string UserPassword { get; set; }
        public bool UserIsStatus { get; set; }
        public bool UserIsDelete { get; set; }
    }
}
