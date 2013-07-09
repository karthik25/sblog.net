using System;
using System.IO;
using sBlog.Net.DB.Services;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.DB.Helpers
{
    public static class SchemaVersionParser
    {
        public static SchemaVersion Parse(this string schemaVersionFile)
        {
            var schemaVersion = Path.GetFileName(schemaVersionFile);

            if (schemaVersion == null || !schemaVersion.StartsWith("sc"))
                throw new Exception("Unable to identify the file name");

            return new SchemaVersion
            {
                MajorVersion = short.Parse(schemaVersion.Substring(2, 2)),
                MinorVersion = short.Parse(schemaVersion.Substring(4, 2)),
                ScriptVersion = short.Parse(schemaVersion.Substring(6, 2)),
                ScriptPath = schemaVersionFile
            };
        }

        public static SchemaVersion Parse(this SchemaEntity schemaEntity)
        {
            return new SchemaVersion
            {
                MajorVersion = schemaEntity.MajorVersion,
                MinorVersion = schemaEntity.MinorVersion,
                ScriptVersion = schemaEntity.ScriptVersion
            };
        }
    }
}
