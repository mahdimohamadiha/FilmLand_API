using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class MovieFile
    {
        public Guid MovieFileId { get; set; }
        public string MovieFileChapter { get; set; }
        public string MovieFileEpisode { get; set; }
        public int MovieFileQuality { get; set; }
        public string MovieFileDubbing { get; set; }
        public bool MovieFileIsCensored { get; set; }
        public string MovieFileSubtitleURL { get; set; }
        public string MovieFile_MovieURL { get; set; }
        public DateTime MovieFileCreateDate { get; set; }
        public DateTime MovieFileModifiedDate { get; set; }
        public bool MovieFileIsStatus { get; set; }
        public bool MovieFileIsDelete { get; set; }
        public Guid MovieRef { get; set; }
    }
}
