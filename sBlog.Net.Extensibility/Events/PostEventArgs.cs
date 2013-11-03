using System;

namespace sBlog.Net.Extensibility.Events
{
    public class PostEventArgs : EventArgs
    {
        public int PostId { get; set; }
        public string PostUrl { get; set; }
    }
}
