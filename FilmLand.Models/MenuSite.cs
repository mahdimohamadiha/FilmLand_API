namespace FilmLand.Models
{
    public class MenuSite
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Sort { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
