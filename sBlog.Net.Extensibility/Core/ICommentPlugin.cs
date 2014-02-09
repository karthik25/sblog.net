using sBlog.Net.Extensibility.Abstract;
using sBlog.Net.Extensibility.Handlers;

namespace sBlog.Net.Extensibility.Core
{
    public interface ICommentPlugin : IPlugin
    {
        CommentEventHandler CommentHandler { get; set; }
        void RegisterCommentEvent(ICommentEvent commentEvent);
        void UnregisterCommentEvent(ICommentEvent commentEvent);
    }
}