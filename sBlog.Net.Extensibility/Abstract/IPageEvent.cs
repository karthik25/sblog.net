using sBlog.Net.Extensibility.Handlers;

namespace sBlog.Net.Extensibility.Abstract
{
    public interface IPageEvent
    {
        event PageEventHandler.PageHandler PageDisplayed;
    }
}