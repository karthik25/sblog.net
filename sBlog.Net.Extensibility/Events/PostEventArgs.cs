using System;

namespace sBlog.Net.Extensibility.Events
{
    public class PostEventArgs : EventArgs
    {
        public string RelativeUrl { get; set; }
        public string FullyQualifiedUrl { get; set; }
        public DateTime PostCreatedDate { get; set; }
    }
}
