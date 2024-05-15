namespace FilmLand.Models
{
    public class SiteMenu
    {
        public Guid SiteMenuId { get; set; }
        public string SiteMenuName { get; set; }
        public string SiteMenuUrl { get; set; }
        public int SiteMenuSort { get; set; }
        public DateTime SiteMenuCreateDate { get; set; }
        public DateTime SiteMenuModifiedDate { get; set; }
        public bool SiteMenuIsStatus { get; set; }
        public bool SiteMenuIsDelete { get; set; }
    }
}
