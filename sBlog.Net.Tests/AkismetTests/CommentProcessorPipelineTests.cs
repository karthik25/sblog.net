using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sBlog.Net.Akismet;
using sBlog.Net.Akismet.Entities;
using sBlog.Net.Tests.Helpers;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Tests.AkismetTests
{
    [TestClass]
    public class CommentProcessorPipelineTests
    {
        [TestMethod]
        public void Can_Process_Comment_When_Akistmet_Is_Disabled()
        {
            var comment = MockObjectFactory.CreateCommentRepository();
            var settings = MockObjectFactory.CreateSettingsRepository();
            var akismet = MockObjectFactory.CreateAkismetService();
            var error = MockObjectFactory.CreateErrorLogger();
            var commentEntity = GetHamComment(1000,4);
            var requestData = new RequestData();

            var commentProcessorPipeline = new CommentProcessorPipeline(comment, settings, akismet, error, commentEntity, requestData);
            var akismetStatus = commentProcessorPipeline.ProcessComment();

            Assert.IsFalse(akismetStatus.IsSpam);
            Assert.IsFalse(akismetStatus.IsHam);

            var commentsByPost = comment.GetCommentsByPostID(4);
            Assert.IsNotNull(commentsByPost.First());
            Assert.AreEqual(commentEntity.CommentContent, commentsByPost.First().CommentContent);
        }

        [TestMethod]
        public void Can_Process_Comment_When_Akistmet_Is_Enabled_And_Delete_Is_Disabled_Ham()
        {
            var comment = MockObjectFactory.CreateCommentRepository();
            var settings = MockObjectFactory.CreateSettingsRepository(2);
            var akismet = MockObjectFactory.CreateAkismetService();
            var error = MockObjectFactory.CreateErrorLogger();
            var commentEntity = GetHamComment(1001,5);
            var requestData = new RequestData();

            var commentProcessorPipeline = new CommentProcessorPipeline(comment, settings, akismet, error, commentEntity, requestData);
            var akismetStatus = commentProcessorPipeline.ProcessComment();

            Assert.IsFalse(akismetStatus.IsSpam);
            Assert.IsTrue(akismetStatus.IsHam);

            var commentsByPost = comment.GetCommentsByPostID(5);
            Assert.IsNotNull(commentsByPost.First());
            Assert.AreEqual(commentEntity.CommentContent, commentsByPost.First().CommentContent);
        }

        [TestMethod]
        public void Can_Process_Comment_When_Akistmet_Is_Enabled_And_Delete_Is_Disabled_Spam()
        {
            var comment = MockObjectFactory.CreateCommentRepository();
            var settings = MockObjectFactory.CreateSettingsRepository(2);
            var akismet = MockObjectFactory.CreateAkismetService();
            var error = MockObjectFactory.CreateErrorLogger();
            var commentEntity = GetSpamComment(1002, 6);
            var requestData = new RequestData();

            var commentProcessorPipeline = new CommentProcessorPipeline(comment, settings, akismet, error, commentEntity, requestData);
            var akismetStatus = commentProcessorPipeline.ProcessComment();

            Assert.IsTrue(akismetStatus.IsSpam);
            Assert.IsFalse(akismetStatus.IsHam);

            var commentsByPost = comment.GetAllComments();
            var commentInQn = commentsByPost.Single(c => c.CommentID == 1002 && c.PostID == 6);
            Assert.IsNotNull(commentInQn);
            Assert.AreEqual(2, commentInQn.CommentStatus);
        }

        [TestMethod]
        public void Can_Process_Comment_When_Akistmet_Is_Enabled_And_Delete_Is_Enabled_Ham()
        {
            var comment = MockObjectFactory.CreateCommentRepository();
            var settings = MockObjectFactory.CreateSettingsRepository(3);
            var akismet = MockObjectFactory.CreateAkismetService();
            var error = MockObjectFactory.CreateErrorLogger();
            var commentEntity = GetHamComment(1003, 7);
            var requestData = new RequestData();

            var commentProcessorPipeline = new CommentProcessorPipeline(comment, settings, akismet, error, commentEntity, requestData);
            var akismetStatus = commentProcessorPipeline.ProcessComment();

            Assert.IsFalse(akismetStatus.IsSpam);
            Assert.IsTrue(akismetStatus.IsHam);

            var commentsByPost = comment.GetCommentsByPostID(7);
            Assert.IsNotNull(commentsByPost.First());
            Assert.AreEqual(commentEntity.CommentContent, commentsByPost.First().CommentContent);
        }

        [TestMethod]
        public void Can_Process_Comment_When_Akistmet_Is_Enabled_And_Delete_Is_Enabled_Spam()
        {
            var comment = MockObjectFactory.CreateCommentRepository();
            var settings = MockObjectFactory.CreateSettingsRepository(3);
            var akismet = MockObjectFactory.CreateAkismetService();
            var error = MockObjectFactory.CreateErrorLogger();
            var commentEntity = GetSpamComment(1004, 8);
            var requestData = new RequestData();

            var commentProcessorPipeline = new CommentProcessorPipeline(comment, settings, akismet, error, commentEntity, requestData);
            var akismetStatus = commentProcessorPipeline.ProcessComment();

            Assert.IsTrue(akismetStatus.IsSpam);
            Assert.IsFalse(akismetStatus.IsHam);

            var commentsByPost = comment.GetAllComments();
            var commentInQn = commentsByPost.SingleOrDefault(c => c.CommentID == 1004 && c.PostID == 8);
            Assert.IsNull(commentInQn);
        }

        private CommentEntity GetSpamComment(int commentID, int postID)
        {
            var commentEntity = new CommentEntity {CommentID = commentID, PostID = postID, CommentContent = "idiot"};

            return commentEntity;
        }

        private CommentEntity GetHamComment(int commentID, int postID)
        {
            var commentEntity = new CommentEntity
                {CommentID = commentID, PostID = postID, CommentContent = "how does this work?"};

            return commentEntity;
        }
    }
}
