using System.ComponentModel.Composition;
using sBlog.Net.Extensibility.Abstract;
using sBlog.Net.Extensibility.Concrete;
using sBlog.Net.Extensibility.Core;
using sBlog.Net.Extensibility.Enumerations;
using sBlog.Net.Extensibility.Events;
using sBlog.Net.Extensibility.Handlers;

namespace sBlog.Net.Plugins.PostViewCount
{
    [Export(typeof(IPlugin))]
    public class PostViewCountPlugin : IPostPlugin
    {
        private PostViewCountDataContext _postViewCountDataContext;

        public void Initialize(PluginContext pluginContext)
        {
            _postViewCountDataContext = new PostViewCountDataContext();
        }

        public void Dispose()
        {
            
        }

        public string Name 
        { 
            get { return "PostViewCount"; } 
        }
        public string Version 
        {
            get { return "1.0"; }
        }
        public string Guid
        {
            get { return "098fa1e0-6ec4-490d-adcc-414ac175550e"; }
        }

        public PluginType Type 
        { 
            get
            {
                return PluginType.Post;
            } 
        }

        public PostEventHandler PostHandler { get; set; }

        public void RegisterPostEvents(IPostEvent postEvent)
        {
            postEvent.PostDisplayed += PostEventOnPostDisplayed;
        }

        private void PostEventOnPostDisplayed(PostEventArgs postEventArgs)
        {
            _postViewCountDataContext.InsertPostView(postEventArgs.PostId, postEventArgs.PostUrl);
        }

        public void UnregisterPostEvents(IPostEvent postEvent)
        {
            postEvent.PostDisplayed -= RemovePostEventOnPostDisplayed;
        }

        private void RemovePostEventOnPostDisplayed(PostEventArgs postEventArgs)
        {
            
        }
    }
}
