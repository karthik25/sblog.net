using sBlog.Net.Extensibility.Handlers;

namespace sBlog.Net.Extensibility.Abstract
{
    public interface IApplicationEvent
    {
        event ApplicationEventHandler.ApplicationHandler ApplicationStarted;
        event ApplicationEventHandler.ApplicationHandler MainPageDisplayed;
        event ApplicationEventHandler.ApplicationHandler ApplicationError;
    }
}