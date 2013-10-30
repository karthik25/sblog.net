using System;
using sBlog.Net.Extensibility.Events;

namespace sBlog.Net.Extensibility.Abstract
{
    public interface IPageEvent
    {
        event EventHandler<PageEventArgs> PageDisplayed;
    }
}