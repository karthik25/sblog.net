namespace sBlog.Net.Models
{
    public class PagedModel
    {
        public bool NextPageValid { get; set; }
        public int NextPageNumber { get; set; }
        public bool PreviousPageValid { get; set; }
        public int PreviousPageNumber { get; set; }
        public int CurrentPageNumber { get; set; }
    }

}