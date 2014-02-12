using sBlog.Net.Extensibility.Abstract;
using sBlog.Net.Extensibility.Events;

namespace sBlog.Net.Extensibility.Handlers
{
    public class PageEventHandler : IPageEvent
    {
        public delegate void PageHandler(PageEventArgs e);

        public event PageHandler PageDisplayed;

        protected virtual void OnPageDisplayed(PageEventArgs e)
        {
            var handler = PageDisplayed;
            if (handler != null) handler(e);
        }

        public void Fire(PageEventArgs eventArgs)
        {
            foreach (PageHandler handler in PageDisplayed.GetInvocationList())
            {
                handler(eventArgs);
            }
        }
    }
}