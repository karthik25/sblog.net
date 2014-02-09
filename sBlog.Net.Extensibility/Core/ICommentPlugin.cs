using sBlog.Net.Extensibility.Abstract;

namespace sBlog.Net.Extensibility.Core
{
    public interface ICommentPlugin : IPlugin
    {
        void RegisterCommentEvent(ICommentEvent commentEvent);
    }
}