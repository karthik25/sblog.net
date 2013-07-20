using System.Collections.Generic;
using sBlog.Net.DB.Services;
using sBlog.Net.MetaData.MetaData;
using System.ComponentModel.DataAnnotations;

namespace sBlog.Net.Areas.Prepare.Models
{
    [MetadataType(typeof(DatabaseSetupModelMetaData))]
    public class DatabaseSetupModel
    {
        public string ConnectionString { get; set; }
        public string MessageCss { get; set; }
        public string Message { get; set; }
        public List<string> Scripts { get; set; }

        public List<SchemaVersion> Results { get; set; }
        public string CompleteException { get; set; }
    }
}
