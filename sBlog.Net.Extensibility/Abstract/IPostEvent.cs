using sBlog.Net.Extensibility.Handlers;

namespace sBlog.Net.Extensibility.Abstract
{
    public interface IPostEvent
    {
        event PostEventHandler.PostHandler PostDisplayed;
    }
}