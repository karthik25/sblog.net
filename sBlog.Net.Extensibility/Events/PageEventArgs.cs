using System;

namespace sBlog.Net.Extensibility.Events
{
    public class PageEventArgs : EventArgs
    {
        public int PageId { get; set; }
        public string PageUrl { get; set; }
    }
}