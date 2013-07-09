using System.Collections.Generic;
using sBlog.Net.DB.Services;

namespace sBlog.Net.DB.Comparers
{
    public class SchemaVersionEqualityComparer : IEqualityComparer<SchemaVersion>
    {
        public bool Equals(SchemaVersion x, SchemaVersion y)
        {
            if (x.MajorVersion != y.MajorVersion)
                return false;
            if (x.MinorVersion != y.MinorVersion)
                return false;
            return x.ScriptVersion == y.ScriptVersion;
        }

        public int GetHashCode(SchemaVersion obj)
        {
            return obj.MajorVersion ^ obj.MinorVersion ^ obj.ScriptVersion;
        }
    }
}
