using sBlog.Net.Extensibility.Abstract;
using sBlog.Net.Extensibility.Events;

namespace sBlog.Net.Extensibility.Handlers
{
    public class PostEventHandler : IPostEvent
    {
        public delegate void PostHandler(PostEventArgs e);

        public event PostHandler PostDisplayed;        

        protected virtual void OnPostDisplayed(PostEventArgs e)
        {
            var handler = PostDisplayed;
            if (handler != null) handler(e);
        }

        public void Fire(PostEventArgs eventArgs)
        {
            
        }
    }
}