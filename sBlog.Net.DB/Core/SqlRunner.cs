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
                    if (HasScriptBeenRunAlready(sqlConnection, sqlFile))
                    {
                        SetSuccess(sqlFile);
                        continue;
                    }

                    try
                    {
                        var contents = GetFileContents(sqlFile.ScriptPath);
                        contents.ForEach(batchCommand =>
                        {
                            using (var sqlCommand = new SqlCommand(batchCommand, sqlConnection))
                            {
                                sqlCommand.ExecuteNonQuery();
                            }
                        });

                        InsertRecord(sqlConnection, sqlFile);
                        SetSuccess(sqlFile);
                    }
                    catch (Exception e)
                    {
                        SetFailure(sqlFile, e);
                    }
                }

                sqlConnection.Close();
            }

            return sqlFiles;
        }

        private static void SetSuccess(SchemaVersion sqlFile)
        {
            sqlFile.RunStatus = true;
            sqlFile.ErrorMessage = string.Empty;
            sqlFile.FullErrorMessage = string.Empty;
        }

        private static void SetFailure(SchemaVersion sqlFile, Exception e)
        {
            sqlFile.RunStatus = false;
            sqlFile.ErrorMessage = e.Message;
            sqlFile.FullErrorMessage = e.ToString();
        }

        private static void InsertRecord(SqlConnection sqlConnection, SchemaVersion schemaVersion)
        {
            const string insertSql = @"INSERT INTO dbo.[Schema] VALUES('{0}',{1},{2},{3},'{4}')";
            var stmt = string.Format(insertSql, Path.GetFileName(schemaVersion.ScriptPath), schemaVersion.MajorVersion,
                                     schemaVersion.MinorVersion, schemaVersion.ScriptVersion, DateTime.Now);
            using (var insertCmd = new SqlCommand(stmt, sqlConnection))
            {
                insertCmd.ExecuteNonQuery();
            }
        }

        private static bool HasScriptBeenRunAlready(SqlConnection sqlConnection, SchemaVersion schemaVersion)
        {
            SqlCommand selectCmd = null;
            try
            {
                const string selectSql = @"SELECT COUNT(*) FROM dbo.[Schema] WHERE [MajorVersion] = {0} AND [MinorVersion] = {1} AND [ScriptVersion] = {2}";
                var stmt = string.Format(selectSql, schemaVersion.MajorVersion, schemaVersion.MinorVersion,
                                         schemaVersion.ScriptVersion);
                selectCmd = new SqlCommand(stmt, sqlConnection);
                var count = selectCmd.ExecuteScalar();
                var result = int.Parse(count.ToString());
                return result == 1;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (selectCmd != null)
                {
                    selectCmd.Dispose();
                }
            }
        }

        private static List<string> GetFileContents(string sqlFile)
        {
            var content = File.ReadAllText(sqlFile);
            return content.Split(new[] { "GO" }, StringSplitOptions.None).ToList();
        }
    }
}
