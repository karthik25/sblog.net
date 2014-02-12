using System;

namespace sBlog.Net.Extensibility.Events
{
    public class ApplicationEventArgs : EventArgs
    {
        public int PageNumber { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }

        public Exception Exception { get; set; }
        public DateTime EventDate { get; set; }
    }
}