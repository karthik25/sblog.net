using System.Collections.Generic;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Models
{
    public class AuthorModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserDisplayName { get; set; }
        public List<PostEntity> Posts { get; set; }
    }
}