using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Models
{
    public class PostModel
    {
        public PostEntity Post { get; set; }
        public bool DisqusEnabled { get; set; }
        public string RootUrl { get; set; }
    }    
}
