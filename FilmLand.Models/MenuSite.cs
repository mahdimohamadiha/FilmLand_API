namespace FilmLand.Models
{
    public class MenuSite
    {
        public Guid MenuSiteId { get; set; }
        public string MenuSiteName { get; set; }
        public string MenuSiteUrl { get; set; }
        public int MenuSiteSort { get; set; }
        public DateTime MenuSiteCreateDate { get; set; }
        public DateTime MenuSiteModifiedDate { get; set; }
        public bool MenuSiteIsStatus { get; set; }
        public bool MenuSiteIsDelete { get; set; }
    }
}
