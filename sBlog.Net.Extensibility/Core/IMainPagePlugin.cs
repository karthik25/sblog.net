using sBlog.Net.Extensibility.Abstract;

namespace sBlog.Net.Extensibility.Core
{
    public interface IMainPagePlugin : IPlugin
    {
        void RegisterMainPageEvents(IMainPageEvent mainPageEvent);
    }
}