using System;
using sBlog.Net.Extensibility.Abstract;
using sBlog.Net.Extensibility.Events;

namespace sBlog.Net.Extensibility.Handlers
{
    public class PageEventHandler : IPageEvent
    {
        public event EventHandler<PageEventArgs> PageDisplayed;
    }
}