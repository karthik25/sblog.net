using System;
using System.Collections.Generic;
using sBlog.Net.Akismet.Interfaces;
using sBlog.Net.Akismet;

namespace sBlog.Net.Tests.MockObjects
{
    public class MockAkismetService : IAkismetService
    {
        public MockAkismetService(string apiKey, string blog, string userAgent)
        {

        }

        public bool VerifyKey()
        {
            throw new NotImplementedException();
        }

        public bool CommentCheck(AkismetComment comment)
        {
            var isSpam = false;

            SpamQualifiers.ForEach(spam =>
            {
                if (!isSpam && comment.CommentContent.Contains(spam))
                    isSpam = true;
            });

            return isSpam;
        }

        public void SubmitSpam(AkismetComment comment)
        {
            
        }

        public void SubmitHam(AkismetComment comment)
        {
            
        }

        private static readonly List<string> SpamQualifiers = new List<string> { "idiot", "i love your website :)" };
    }
}
