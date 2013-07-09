#region Disclaimer/License Info

/* *********************************************** */

// sBlog.Net

// sBlog.Net is a minimalistic blog engine software.

// Homepage: http://sblogproject.net
// Github: http://github.com/karthik25/sBlog.Net

// This project is licensed under the BSD license.  
// See the License.txt file for more information.

/* *********************************************** */

#endregion
using System;
using System.Data.Linq.Mapping;

namespace sBlog.Net.Domain.Entities
{
    [Table(Name = "Schema")]
    public class SchemaEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int SchemaRecordId { get; set; }
        [Column]
        public string ScriptName { get; set; }
        [Column]
        public short MajorVersion { get; set; }
        [Column]
        public short MinorVersion { get; set; }
        [Column]
        public short ScriptVersion { get; set; }
        [Column]
        public DateTime ScriptRunDateTime { get; set; }
    }
}
