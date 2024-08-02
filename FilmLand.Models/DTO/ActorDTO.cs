using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models.DTO
{
    public class ActorDTO
    {
        public string ActorName { get; set; }
        public string ActorBirthDay { get; set; }
        public string ActorProfession { get; set; }
        public string ActorBio { get; set; }
        public IFormFile ActorPicture { get; set; }
    }
}
