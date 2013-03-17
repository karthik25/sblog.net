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
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Xml;
using sBlog.Net.Akismet;
using sBlog.Net.Akismet.Entities;
using sBlog.Net.Models.Comments;
using sBlog.Net.Domain.Entities;
using System.Collections.Generic;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Akismet.Interfaces;
using sBlog.Net.FluentExtensions;

namespace sBlog.Net.Controllers
{
    public class CommentController : BlogController
    {
        private readonly IPost _postRepository;
        private readonly IComment _commentsRepository;
        private readonly ICacheService _cacheService;
        private readonly IError _errorLogger;
        private IAkismetService _akismetService;

        public CommentController(IPost postRepository, IComment commentsRepository, ISettings settingsRepository, ICacheService cacheService, IError errorLogger)
            : base(settingsRepository)
        {
            _postRepository = postRepository;
            _cacheService = cacheService;
            _commentsRepository = commentsRepository;
            _errorLogger = errorLogger;
        }

        //
        // GET: /comment/add

        public ActionResult Add(CommentViewModel commentViewModel)
        {
            var commentStatus = false;
            if (ModelState.IsValid)
            {
                if ((commentViewModel.DisplayName == null && commentViewModel.IsHuman == "on") || Request.IsAuthenticated)
                {
                    commentViewModel.Comment.PostID = commentViewModel.Post.PostID;
                    if (Request.IsAuthenticated)
                        commentViewModel.Comment.UserID = GetUserId();
                    var commentProcessor = GetCommentProcessor(commentViewModel.Comment);
                    commentProcessor.ProcessComment();
                    commentStatus = true;
                }
            }
            else
            {
                TempData["CommentErrors"] = GetModelErrors();
            }

            return RedirectByPostType(commentViewModel, commentStatus);
        }

        //
        // GET: /comments/recent

        [ChildActionOnly]
        public ActionResult RecentComments()
        {
            var comments = GetRecentComments();            
            return PartialView("RecentComments", comments);
        }

        private List<RecentComment> GetRecentComments()
        {
            var recents = new List<RecentComment>();
            if (!SettingsRepository.DisqusEnabled)
            {
                var allPosts = Request.IsAuthenticated
                                   ? _postRepository.GetPosts().Concat(_postRepository.GetPages()).ToList()
                                   : _cacheService.GetPostsFromCache(_postRepository, CachePostsUnauthKey)
                                                  .Concat(_cacheService.GetPagesFromCache(_postRepository,
                                                                                          CachePagesUnauthKey)).ToList();
                var topComments = allPosts.SelectMany(p => p.Comments)
                                          .Where(c => c.CommentStatus == 0)
                                          .OrderByDescending(c => c.CommentPostedDate)
                                          .Take(5)
                                          .ToList();
                topComments.ForEach(comment =>
                    {
                        var post = allPosts.Single(p => p.PostID == comment.PostID);
                        recents.Add(new RecentComment
                            {
                                CommentContent = comment.CommentContent,
                                PostAddedDate = post.PostAddedDate,
                                PostUrl = post.PostUrl,
                                EntryType = post.EntryType
                            });
                    });
            }
            else
            {
                var rssUrl = string.Format("http://{0}.disqus.com/latest.rss", SettingsRepository.BlogDisqusShortName);
                var reader = XmlReader.Create(rssUrl);
                var feed = SyndicationFeed.Load(reader);

                if (feed != null)
                {
                    const string pattern = @"<(.|\n)*?>";
                    recents.AddRange(feed.Items.Take(5).Select(syndicationItem => new RecentComment
                        {
                            PostUrl = syndicationItem.Links.First().Uri.ToString(), DisqusComment = true, CommentContent = Regex.Replace(syndicationItem.Summary.Text, pattern, string.Empty)
                        }));
                }
            }

            return recents;
        }

        private ActionResult RedirectByPostType(CommentViewModel commentViewModel, bool commentingStatus)
        {
            var commentStatus = commentingStatus ? "comment-posted" : "comment-errored";
            if (commentViewModel.Post.EntryType == 1)
            {
                return RedirectToRoute("IndividualPost", new
                {
                    year = commentViewModel.Post.PostAddedDate.Year,
                    month = commentViewModel.Post.PostAddedDate.Month.ToString("00"),
                    url = commentViewModel.Post.PostUrl,
                    status = commentStatus
                });
            }
            return RedirectToRoute("Pages", new { pageUrl = commentViewModel.Post.PostUrl, status = commentStatus });
        }

        private List<string> GetModelErrors()
        {
            var errors = new List<string>();
            ModelState.Values.ToList().ForEach(val =>
            {
                if (val.Errors.Count > 0)
                    errors.Add(val.Errors.First().ErrorMessage);
            });
            return errors;
        }

        private CommentProcessorPipeline GetCommentProcessor(CommentEntity commentEntity)
        {
            _akismetService = new Akismet.Akismet(SettingsRepository.BlogAkismetKey,
                                                  SettingsRepository.BlogAkismetUrl,
                                                  Request.UserAgent);
            return new CommentProcessorPipeline(_commentsRepository, SettingsRepository, _akismetService, _errorLogger, commentEntity, GetRequestData());
        }

        private RequestData GetRequestData()
        {
            if (Request.UrlReferrer != null)
            {
                var requestData = new RequestData
                                      {
                                          Blog = SettingsRepository.BlogAkismetUrl,
                                          UserIp = Request.UserHostAddress,
                                          UserAgent = Request.UserAgent,
                                          Referrer = Request.UrlReferrer.ToString(),
                                          IsAuthenticated = Request.IsAuthenticated
                                      };
                return requestData;
            }
            return null;
        }
    }
}
