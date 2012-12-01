/* Author:      Joel Thoms (http://joel.net)
 * Copyright:   2006 Joel Thoms (http://joel.net)
 * About:       Akismet (http://akismet.com) .Net 2.0 API allow you to check
 *              Akismet's spam database to verify your comments and prevent spam.
 * 
 * Note:        Do not launch 'DEBUG' code on your site.  Only build 'RELEASE' for your site.  Debug code contains
 *              Console.WriteLine's, which are not desireable on a website.
*/
using System;
using System.Net;
using System.IO;
using sBlog.Net.Akismet.Interfaces;

namespace sBlog.Net.Akismet 
{
    public class Akismet : IAkismetService
    {
        const string verifyUrl = "http://rest.akismet.com/1.1/verify-key";
        const string commentCheckUrl = "http://{0}.rest.akismet.com/1.1/comment-check";
        const string submitSpamUrl = "http://{0}.rest.akismet.com/1.1/submit-spam";
        const string submitHamUrl = "http://{0}.rest.akismet.com/1.1/submit-ham";

        string apiKey = null;
        string userAgent = "Joel.Net's Akismet API/1.0";
        string blog = null;

        public string CharSet = "UTF-8";

        /// <summary>Creates an Akismet API object.</summary>
        /// <param name="apiKey">Your wordpress.com API key.</param>
        /// <param name="blog">URL to your blog</param>
        /// <param name="userAgent">Name of application using API.  example: "Joel.Net's Akismet API/1.0"</param>
        /// <remarks>Accepts required fields 'apiKey', 'blog', 'userAgent'.</remarks>
        public Akismet(string apiKey, string blog, string userAgent) {
            this.apiKey = apiKey;
            if (userAgent != null) this.userAgent = userAgent + " | Akismet/1.11";
            this.blog = blog;
        }

        /// <summary>Verifies your wordpress.com key.</summary>
        /// <returns>'True' is key is valid.</returns>
        public bool VerifyKey() {
            bool value = false;

            string response = HttpPost(verifyUrl, String.Format("key={0}&blog={1}", apiKey, UrlEncoder.UrlEncode(blog)), CharSet);
            value = (response == "valid") ? true : false;

#if DEBUG
            Console.WriteLine("VerifyKey() = {0}.", value);
#endif

            return value;
        }

        /// <summary>Checks AkismetComment object against Akismet database.</summary>
        /// <param name="comment">AkismetComment object to check.</param>
        /// <returns>'True' if spam, 'False' if not spam.</returns>
        public bool CommentCheck(AkismetComment comment) {
            bool value = false;

            value = Convert.ToBoolean(HttpPost(String.Format(commentCheckUrl, apiKey), CreateData(comment), CharSet));

#if DEBUG
            Console.WriteLine("CommentCheck() = {0}.", value);
#endif

            return value;
        }

        /// <summary>Submits AkismetComment object into Akismet database.</summary>
        /// <param name="comment">AkismetComment object to submit.</param>
        public void SubmitSpam(AkismetComment comment) {
            string value = HttpPost(String.Format(submitSpamUrl, apiKey), CreateData(comment), CharSet);
#if DEBUG
            Console.WriteLine("SubmitSpam() = {0}.", value);
#endif
        }

        /// <summary>Retracts false positive from Akismet database.</summary>
        /// <param name="comment">AkismetComment object to retract.</param>
        public void SubmitHam(AkismetComment comment) {
            string value = HttpPost(String.Format(submitHamUrl, apiKey), CreateData(comment), CharSet);
#if DEBUG
            Console.WriteLine("SubmitHam() = {0}.", value);
#endif
        }



        #region - Private Methods (CreateData, HttpPost) -

        /// <summary>Takes an AkismetComment object and returns an (escaped) string of data to POST.</summary>
        /// <param name="comment">AkismetComment object to translate.</param>
        /// <returns>A System.String containing the data to POST to Akismet API.</returns>
        private string CreateData(AkismetComment comment) {
            string value = String.Format("blog={0}&user_ip={1}&user_agent={2}&referrer={3}&permalink={4}&comment_type={5}" +
                "&comment_author={6}&comment_author_email={7}&comment_author_url={8}&comment_content={9}",
                UrlEncoder.UrlEncode(comment.Blog),
                UrlEncoder.UrlEncode(comment.UserIp),
                UrlEncoder.UrlEncode(comment.UserAgent),
                UrlEncoder.UrlEncode(comment.Referrer),
                UrlEncoder.UrlEncode(comment.Permalink),
                UrlEncoder.UrlEncode(comment.CommentType),
                UrlEncoder.UrlEncode(comment.CommentAuthor),
                UrlEncoder.UrlEncode(comment.CommentAuthorEmail),
                UrlEncoder.UrlEncode(comment.CommentAuthorUrl),
                UrlEncoder.UrlEncode(comment.CommentContent)
            );

            return value;
        }

        /// <summary>Sends HttpPost</summary>
        /// <param name="url">URL to Post data to.</param>
        /// <param name="data">Data to post. example: key=&ltwordpress-key&gt;&blog=http://joel.net</param>
        /// <param name="charSet">Character set of your blog. example: UTF-8</param>
        /// <returns>A System.String containing the Http Response.</returns>
        private string HttpPost(string url, string data, string charSet) {
            string value = "";

            // Initialize Connection
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=" + charSet;
            request.UserAgent = userAgent;
            request.ContentLength = data.Length;

#if DEBUG
            Console.WriteLine("request({0}) = {1}", url, data);
#endif

            // Write Data
            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(data);
            writer.Close();

            // Read Response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            value = reader.ReadToEnd();
            reader.Close();

#if DEBUG
            Console.WriteLine("response = {0}", value);
#endif

            return value;
        }
        #endregion
    }
}