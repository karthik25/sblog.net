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
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using sBlog.Net.Models;
using System.Web.Mvc;

namespace sBlog.Net.Rss
{
    public static class RssFeedGenerator
    {
        public static SyndicationFeed GetRssFeedData(RssFeedViewModel rssFeedViewModel, UrlHelper urlHelper)
        {
            var mostRecentPost = rssFeedViewModel.Posts.FirstOrDefault();
            var lastUpdateDateTime = (mostRecentPost != null && mostRecentPost.PostEditedDate.HasValue) ? mostRecentPost.PostEditedDate.Value : DateTime.MinValue;
            var feed = new SyndicationFeed(rssFeedViewModel.BlogTitle,
                                           rssFeedViewModel.BlogCaption,
                                           new Uri(rssFeedViewModel.BlogUrl),
                                           string.Format("{0}{1}", DateTime.Now.ToString("MM_dd_yyyy"), DateTime.Now.ToBinary()),
                                           lastUpdateDateTime)
                           {
                               Copyright = new TextSyndicationContent("Copyright " + DateTime.Now.Year),
                               Description =
                                   new TextSyndicationContent("This is the feed for " + rssFeedViewModel.BlogTitle),
                               Generator = rssFeedViewModel.BlogUrl,
                               Items = GetFeedItems(rssFeedViewModel, urlHelper),
                               Language = rssFeedViewModel.BlogLanguage,
                               LastUpdatedTime = lastUpdateDateTime,
                           };

            return feed;
        }

        private static IEnumerable<SyndicationItem> GetFeedItems(RssFeedViewModel rssFeedViewModel, UrlHelper urlHelper)
        {
            return (from post in rssFeedViewModel.Posts 
                    let textContent = new TextSyndicationContent(post.PostContent) 
                    let url = rssFeedViewModel.BlogUrl.TrimEnd('/') + urlHelper.RouteUrl("IndividualPost", new { year = post.PostYear, 
                                                                                                                 month = post.PostMonth, 
                                                                                                                 url = post.PostUrl }) 
                    let date = post.PostEditedDate ?? post.PostAddedDate
                    select new SyndicationItem(post.PostTitle, textContent, new Uri(url), url, date)).ToList();
        }
    }
}
