using sBlog.Net.Extensibility.Abstract;
using sBlog.Net.Extensibility.Handlers;

namespace sBlog.Net.Extensibility.Core
{
    public interface IApplicationPlugin : IPlugin
    {
        ApplicationEventHandler ApplicationHandler { get; set; }
        void RegisterApplicationEvents(IApplicationEvent applicationEvent);
        void UnregisterApplicationEvents(IApplicationEvent applicationEvent);
    }
}