using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Models
{
    public class MovieFileList
    {
        public Guid MovieFileId { get; set; }
        public string MovieFileChapter { get; set; }
        public string MovieFileEpisode { get; set; }
        public string MovieFileDubbing { get; set; }
        public bool MovieFileIsCensored { get; set; }
        public string MovieFileSubtitleURL { get; set; }
        public List<string> MovieFile_MovieURL { get; set; }
        public List<int> MovieFileQuality { get; set; }
    }
}
