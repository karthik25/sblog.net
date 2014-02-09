using sBlog.Net.Extensibility.Abstract;

namespace sBlog.Net.Extensibility.Core
{
    public interface IApplicationPlugin : IPlugin
    {
        void RegisterApplicationEvents(IApplicationEvent applicationEvent);
    }
}