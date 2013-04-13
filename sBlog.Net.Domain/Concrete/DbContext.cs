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
using System.Data.SqlClient;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.Domain.Concrete
{
    public class DbContext
    {
        public bool IsConnectionStringPopulated()
        {
            return !string.IsNullOrEmpty(ApplicationDomainConfiguration.ConnectionString);
        }

        public SetupStatus IsConnectionStringValid()
        {
            var setupStatus = new SetupStatus();
            var status = IsConnectionStringPopulated();
            if (status)
            {
                try
                {
                    IsInstallationComplete();
                    setupStatus.Message = "Seems like you have a valid connection string.";
                    setupStatus.CssClass = "confirm";
                }
                catch (Exception ex)
                {
                    status = false;
                    setupStatus.Message = ex is SqlException ? "Seems the connection string specified in the web.config is invalid. Please verify the connection string."
                        : "Settings table in the database has an incorrect value. Are you sure the database installation of sBlog is complete?";
                    setupStatus.CssClass = "error";
                }
            }
            else
            {
                setupStatus.Message = "Please specify the connection string within the \"AppDb\" application key.";
                setupStatus.CssClass = "warning";
            }

            setupStatus.SetupValid = status;

            return setupStatus;
        }

        public bool IsCredentialsValid(string connectionString)
        {
            var appConnectionString = ApplicationDomainConfiguration.ConnectionString;
            return IsConnectionStringPopulated() && appConnectionString == connectionString;
        }

        public bool IsInstallationComplete()
        {
            ISettings settings = new Settings();
            return settings.InstallationComplete;
        }
    }

    public class SetupStatus
    {
        public bool SetupValid { get; set; }
        public string CssClass { get; set; }
        public string Message { get; set; }
    }
}
