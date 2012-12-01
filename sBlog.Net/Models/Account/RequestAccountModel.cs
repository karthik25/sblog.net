using System.ComponentModel.DataAnnotations;
using sBlog.Net.MetaData.MetaData;

namespace sBlog.Net.Models.Account
{
    [MetadataType(typeof(RequestAccountModelMetaData))]
    public class RequestAccountModel
    {
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorMessage { get; set; }
    }
}
