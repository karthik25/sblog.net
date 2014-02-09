using sBlog.Net.Extensibility.Abstract;
using sBlog.Net.Extensibility.Handlers;

namespace sBlog.Net.Extensibility.Core
{
    public interface IPostPlugin : IPlugin
    {
        PostEventHandler PostHandler { get; set; }
        void RegisterPostEvents(IPostEvent postEvent);
        void UnregisterPostEvents(IPostEvent postEvent); 
    }
}