using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class FilePath
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
