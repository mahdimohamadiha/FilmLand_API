using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models.DTO
{
    public class MovieFileDTO
    {
        public string MovieFileChapter { get; set; }
        public string MovieFileEpisode { get; set; }
        public int MovieFileQuality { get; set; }
        public string MovieFileDubbing { get; set; }
        public bool MovieFileIsCensored { get; set; }
        public string? MovieFileSubtitleURL { get; set; }
        public string MovieFile_MovieURL { get; set; }
        public Guid? MovieRef { get; set; }
    }
}
