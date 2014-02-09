using sBlog.Net.Extensibility.Abstract;
using sBlog.Net.Extensibility.Handlers;

namespace sBlog.Net.Extensibility.Core
{
    public interface IPagePlugin : IPlugin
    {
        PageEventHandler PageHandler { get; set; }
        void RegisterPageEvents(IPageEvent pageEvent); 
    }
}