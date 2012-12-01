#region Disclaimer/License Info

/* *********************************************** */

// sBlog.Net

// sBlog.Net is a minimalistic blog engine software.

// Homepage: http://sblogproject.net
// Github: http://github.com/karthik25/sBlog.Net

// This project is licensed under the BSD license.  
// See the License.txt file for more information.

/* *********************************************** */

#endregion

using sBlog.Net.Akismet.Entities;
using sBlog.Net.Akismet.Extensions;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Akismet.Interfaces;

namespace sBlog.Net.Akismet
{
    public class AkismetPipeline
    {
        private readonly IAkismetService _akismetService;

        public AkismetPipeline(IAkismetService akismetService)
        {
            _akismetService = akismetService;
        }

        public AkismetStatus Process(AkismetComment comment, ISettings blogSettings)
        {            
            var commentStatus = new AkismetStatus(comment);
            commentStatus.CheckIfSpamOrHam(_akismetService)
                         .SubmitSpam(_akismetService)
                         .SubmitHam(_akismetService);            

            return commentStatus;
        }
    }
}
