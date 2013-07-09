using sBlog.Net.DB.Enumerations;

namespace sBlog.Net.DB.Services
{
    public class SetupStatus
    {
        public SetupStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
