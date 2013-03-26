using System.Collections.Generic;

namespace sBlog.Net.Models
{    
    public class AuthorListingViewModel : PagedModel
    {        
        public List<AuthorModel> Authors { get; set; }

        public string BlogName { get; set; }
        public string BlogCaption { get; set; }
    }    
}