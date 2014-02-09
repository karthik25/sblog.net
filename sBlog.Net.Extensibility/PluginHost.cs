using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Extensibility.Concrete;
using sBlog.Net.Extensibility.Core;
using sBlog.Net.Extensibility.Events;
using sBlog.Net.Extensibility.Handlers;

namespace sBlog.Net.Extensibility
{
    public sealed class PluginHost : IPluginHost
    {
        private static readonly object InstanceLock = new object();

        private AggregateCatalog _aggregateCatalog;
        private CompositionContainer _container;
        private CompositionBatch _batch;

        private static volatile PluginHost _pluginHost;

        [ImportMany(typeof(IPlugin), AllowRecomposition = true)]
        public IEnumerable<IPlugin> Plugins;

        private PluginHost()
        {

        }

        public static PluginHost Instance
        {
            get
            {
                if (_pluginHost == null)
                {
                    lock (InstanceLock)
                    {
                        if (_pluginHost == null)
                            _pluginHost = new PluginHost();
                    }
                }
                return _pluginHost;
            }
        }

        public void Compose()
        {
            ComposeParts();
            InitializePlugins();
        }

        public void Decompose()
        {
            ReleasePlugins();
            DisposeParts();
        }

        private void DisposeParts()
        {
            if (_container == null) return;
            _container.Catalog.Dispose();
            _container.Dispose();
            _container = null;
        }

        private void ReleasePlugins()
        {

        }

        private void InitializePlugins()
        {
            if (Plugins != null)
            {
                var httpContext = HttpContext.Current;
                var user = (IUserInfo)httpContext.User;
                var pluginContext = new PluginContext
                {
                    Request = httpContext.Request,
                    IsAuthenticated = httpContext.User != null && httpContext.User.Identity.IsAuthenticated,
                    UserId = user == null ? string.Empty : user.UserId,
                    UserToken = user == null ? string.Empty : user.UserToken
                };

                foreach (var plugin in Plugins)
                {
                    plugin.Initialize(pluginContext);

                    if (plugin is IPostPlugin)
                    {
                        var postHandler = new PostEventHandler();
                        ((IPostPlugin)plugin).RegisterPostEvents(postHandler);
                        ((IPostPlugin)plugin).PostHandler = postHandler;
                    }
                    if (plugin is IPagePlugin)
                    {
                        
                    }
                }
            }
        }

        public void RaisePostEvents(int postId, string postUrl)
        {
            var eventArgs = new PostEventArgs { PostId = postId, PostUrl = postUrl };
            var postPlugins = Plugins.Where(p => (p as IPostPlugin) != null);
            foreach (var plugin in postPlugins.Where(postHandler => postHandler != null).Cast<IPostPlugin>())
            {
                plugin.PostHandler.Fire(eventArgs);
            }
        }

        private void ComposeParts()
        {
            _aggregateCatalog = new AggregateCatalog(new ComposablePartCatalog[]
                                   {
                                      new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin")),
                                      new AssemblyCatalog(Assembly.GetExecutingAssembly())
                                   });
            _container = new CompositionContainer(_aggregateCatalog);
            _batch = new CompositionBatch();
            _batch.AddPart(this);

            _container.Compose(_batch);
        }
    }
}
