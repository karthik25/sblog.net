using sBlog.Net.Extensibility.Abstract;
using sBlog.Net.Extensibility.Events;

namespace sBlog.Net.Extensibility.Handlers
{
    public class CommentEventHandler : ICommentEvent
    {
        public delegate void CommentHandler(CommentEventArgs e);

        public event CommentHandler CommentAdded;
        public event CommentHandler CommentDisplayed;

        protected virtual void OnCommentAdded(CommentEventArgs e)
        {
            var handler = CommentAdded;
            if (handler != null) handler(e);
        }

        protected virtual void OnCommentDisplayed(CommentEventArgs e)
        {
            var handler = CommentDisplayed;
            if (handler != null) handler(e);
        }

        public void FireAdded(CommentEventArgs eventArgs)
        {
            foreach (CommentHandler handler in CommentAdded.GetInvocationList())
            {
                handler(eventArgs);
            }
        }

        public void FireDisplayed(CommentEventArgs eventArgs)
        {
            foreach (CommentHandler handler in CommentDisplayed.GetInvocationList())
            {
                handler(eventArgs);
            }
        }
    }
}