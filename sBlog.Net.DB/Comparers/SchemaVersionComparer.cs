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
using System.Collections.Generic;
using sBlog.Net.DB.Services;

namespace sBlog.Net.DB.Comparers
{
    public class SchemaVersionComparer : IComparer<SchemaVersion>
    {
        public int Compare(SchemaVersion x, SchemaVersion y)
        {
            var currentVersion = short.Parse(string.Format("{0}{1}{2}", x.MajorVersion, x.MinorVersion, x.ScriptVersion));
            var otherVersion = short.Parse(string.Format("{0}{1}{2}", y.MajorVersion, y.MinorVersion, y.ScriptVersion));
            return currentVersion.CompareTo(otherVersion);
        }
    }
}
