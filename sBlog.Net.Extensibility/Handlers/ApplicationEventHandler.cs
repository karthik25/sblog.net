using sBlog.Net.Extensibility.Abstract;
using sBlog.Net.Extensibility.Events;

namespace sBlog.Net.Extensibility.Handlers
{
    public class ApplicationEventHandler : IApplicationEvent
    {
        public delegate void ApplicationHandler(ApplicationEventArgs e);

        public event ApplicationHandler ApplicationStarted;
        public event ApplicationHandler MainPageDisplayed;
        public event ApplicationHandler ApplicationError;

        protected virtual void OnApplicationStarted(ApplicationEventArgs e)
        {
            var handler = ApplicationStarted;
            if (handler != null) handler(e);
        }

        protected virtual void OnMainPageDisplayed(ApplicationEventArgs e)
        {
            var handler = MainPageDisplayed;
            if (handler != null) handler(e);
        }

        protected virtual void OnApplicationError(ApplicationEventArgs e)
        {
            var handler = ApplicationError;
            if (handler != null) handler(e);
        }

        public void FireApplicationStarted(ApplicationEventArgs eventArgs)
        {
            foreach (ApplicationHandler handler in ApplicationStarted.GetInvocationList())
            {
                handler(eventArgs);
            }
        }

        public void FireMainPageDisplayed(ApplicationEventArgs eventArgs)
        {
            foreach (ApplicationHandler handler in MainPageDisplayed.GetInvocationList())
            {
                handler(eventArgs);
            }
        }

        public void FireApplicationError(ApplicationEventArgs eventArgs)
        {
            foreach (ApplicationHandler handler in ApplicationError.GetInvocationList())
            {
                handler(eventArgs);
            }
        }
    }
}