using System;

namespace sBlog.Net.Extensibility.Events
{
    public class PageEventArgs : EventArgs
    {
        public string RelativeUrl { get; set; }
        public string FullyQualifiedUrl { get; set; }
        public DateTime PageCreatedDate { get; set; }
    }
}