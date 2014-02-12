using sBlog.Net.Extensibility.Handlers;

namespace sBlog.Net.Extensibility.Abstract
{
    public interface ICommentEvent
    {
        event CommentEventHandler.CommentHandler CommentDisplayed;
        event CommentEventHandler.CommentHandler CommentAdded;
    }
}