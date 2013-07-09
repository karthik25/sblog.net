using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using sBlog.Net.DB.Services;

namespace sBlog.Net.DB.Core
{
    public class SqlRunner
    {
        private readonly string _connectionString;

        public SqlRunner(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<SchemaVersion> RunScripts(List<SchemaVersion> sqlFiles)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                foreach (var sqlFile in sqlFiles)
                {
                    var contents = GetFileContents(sqlFile.ScriptPath);
                    contents.ForEach(batchCommand =>
                    {
                        using (var sqlCommand = new SqlCommand(batchCommand, sqlConnection))
                        {
                            sqlCommand.ExecuteNonQuery();
                        }
                    });
                }

                sqlConnection.Close();
            }

            return sqlFiles;
        }

        private static List<string> GetFileContents(string sqlFile)
        {
            var content = File.ReadAllText(sqlFile);
            return content.Split(new[] { "GO" }, StringSplitOptions.None).ToList();
        }
    }
}
