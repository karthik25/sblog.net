using System;
using System.ComponentModel.Composition;
using sBlog.Net.Extensibility.Abstract;
using sBlog.Net.Extensibility.Concrete;
using sBlog.Net.Extensibility.Enumerations;
using sBlog.Net.Extensibility.Events;
using sBlog.Net.Extensibility.Handlers;

namespace sBlog.Net.Plugins.PostViewCount
{
    [Export(typeof(IPlugin))]
    public class PostViewCountPlugin : IPlugin
    {
        public void Initialize(PluginContext pluginContext)
        {
            
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

        public void RegisterApplicationEvents(IApplicationEvent applicationEvent)
        {
            throw new NotImplementedException();
        }

        public void RegisterMainPageEvents(IMainPageEvent mainPageEvent)
        {
            throw new NotImplementedException();
        }

        public PostEventHandler PostHandler { get; set; }

        public void RegisterPostEvents(IPostEvent postEvent)
        {
            postEvent.PostDisplayed += PostEventOnPostDisplayed;
        }

        private void PostEventOnPostDisplayed(PostEventArgs postEventArgs)
        {
            // Use EF to log this into the database
        }

        public void UnregisterPostEvents(IPostEvent postEvent)
        {
            postEvent.PostDisplayed -= RemovePostEventOnPostDisplayed;
        }

        private void RemovePostEventOnPostDisplayed(PostEventArgs postEventArgs)
        {
            
        }

        public void RegisterPageEvents(IPageEvent pageEvent)
        {
            throw new NotImplementedException();
        }

        public void RegisterCommentEvent(ICommentEvent commentEvent)
        {
            throw new NotImplementedException();
        }

        public void RegisterLinkEvent(ILinkEvent linkEvent)
        {
            throw new NotImplementedException();
        }
    }
}
