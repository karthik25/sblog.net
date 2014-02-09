using sBlog.Net.Extensibility.Abstract;

namespace sBlog.Net.Extensibility.Core
{
    public interface ILinkPlugin : IPlugin
    {
        void RegisterLinkEvent(ILinkEvent linkEvent);
    }
}