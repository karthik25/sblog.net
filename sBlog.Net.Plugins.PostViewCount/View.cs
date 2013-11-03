using System;
using System.ComponentModel.DataAnnotations;

namespace sBlog.Net.Plugins.PostViewCount
{
    public class View
    {
        [Key]
        public int ViewId { get; set; }
        public int PostId { get; set; }
        public DateTime ViewDate { get; set; }
        public string PostUrl { get; set; }
    }
}