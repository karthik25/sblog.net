using sBlog.Net.Extensibility.Concrete;
using sBlog.Net.Extensibility.Enumerations;
using sBlog.Net.Extensibility.Handlers;

namespace sBlog.Net.Extensibility.Abstract
{
    public interface IPlugin
    {
        /// <summary>
        /// Initializes the specified plugin context.
        /// </summary>
        void Initialize(PluginContext pluginContext);

        /// <summary>
        /// Disposes of a plugin
        /// </summary>
        void Dispose();

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        string Version { get; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        string Guid { get; }

        PluginType Type { get; }

        void RegisterApplicationEvents(IApplicationEvent applicationEvent);

        void RegisterMainPageEvents(IMainPageEvent mainPageEvent);

        PostEventHandler PostHandler { get; set; }
        void RegisterPostEvents(IPostEvent postEvent);
        void UnregisterPostEvents(IPostEvent postEvent);

        void RegisterPageEvents(IPageEvent pageEvent);

        void RegisterCommentEvent(ICommentEvent commentEvent);
        
        void RegisterLinkEvent(ILinkEvent linkEvent);
    }
}