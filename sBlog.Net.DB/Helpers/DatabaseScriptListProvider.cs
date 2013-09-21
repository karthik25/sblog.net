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
using System.IO;
using System.Linq;
using sBlog.Net.DB.Comparers;
using sBlog.Net.DB.Services;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.DB.Helpers
{
    public static class DatabaseScriptListProvider
    {
        public static List<SchemaVersion> GetScriptsToExecute(this ISchema schemaRepository, List<SchemaVersion> schemaVersions)
        {
            var scriptsRan = schemaRepository.GetSchemaEntries().Select(s => new SchemaVersion { 
                MajorVersion = s.MajorVersion, 
                MinorVersion = s.MinorVersion, 
                ScriptVersion = s.ScriptVersion 
            });
            var scriptsToBeRan = schemaVersions.Except(scriptsRan, new SchemaVersionEqualityComparer()).ToList();
            scriptsToBeRan.Sort(new SchemaVersionComparer());

            return scriptsToBeRan.ToList();
        }

        public static SortedSet<SchemaVersion> GetAvailableScripts(this IPathMapper pathMapper)
        {
            var sortedList = new SortedSet<SchemaVersion>();
            var filePath = pathMapper.MapPath("~/Sql");
            if (filePath != null)
            {
                var files = Directory.GetFiles(filePath).ToList();
                files.ForEach(f =>
                {
                    var schemaItem = f.Parse();
                    sortedList.Add(schemaItem);
                });
            }
            return sortedList;
        }
    }
}
