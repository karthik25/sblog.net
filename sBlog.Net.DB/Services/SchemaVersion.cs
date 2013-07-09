using System;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.DB.Services
{
    public class SchemaVersion : IComparable<SchemaVersion>, IEquatable<SchemaVersion>, IEquatable<SchemaEntity>
    {
        public short MajorVersion { get; set; }
        public short MinorVersion { get; set; }
        public short ScriptVersion { get; set; }
        public string ScriptPath { get; set; }        

        public int CompareTo(SchemaVersion other)
        {
            var currentVersion = short.Parse(string.Format("{0}{1}{2}", MajorVersion, MinorVersion, ScriptVersion));
            var otherVersion = short.Parse(string.Format("{0}{1}{2}", other.MajorVersion, other.MinorVersion, other.ScriptVersion));
            return currentVersion.CompareTo(otherVersion);
        }

        public bool Equals(SchemaVersion other)
        {
            if (MajorVersion != other.MajorVersion)
                return false;
            if (MinorVersion != other.MinorVersion)
                return false;
            return ScriptVersion == other.ScriptVersion;
        }

        public bool Equals(SchemaEntity other)
        {
            if (MajorVersion != other.MajorVersion)
                return false;
            if (MinorVersion != other.MinorVersion)
                return false;
            return ScriptVersion == other.ScriptVersion;
        }

        public override string ToString()
        {
            return string.Format("sc{0}{1}{2}", MajorVersion.ToString("00"), MinorVersion.ToString("00"), ScriptVersion.ToString("00"));
        }
    }
}
