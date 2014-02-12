using System;

namespace sBlog.Net.Extensibility.Events
{
    public class CommentEventArgs : EventArgs
    {
        public int PostOrPageId { get; set; }
        public string PostOrPageUrl { get; set; }
    }
}