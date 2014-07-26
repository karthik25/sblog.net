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
using System.Linq;
using System.Data.Linq;
using sBlog.Net.Domain.Entities;
using System.Data.SqlClient;

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
                setupStatus.Message = "Please specify the connection string within the \"connectionString\" attribute in the sblognetSettings configuration section.";
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
            var dataContext = new DataContext(ApplicationDomainConfiguration.ConnectionString);
            var settings = dataContext.GetTable<SettingsEntity>();
            var singleOrDefault = settings.SingleOrDefault(k => k.KeyName == "InstallationComplete");
            return singleOrDefault != null && bool.Parse(singleOrDefault.KeyValue);
        }
    }

    public class SetupStatus
    {
        public bool SetupValid { get; set; }
        public string CssClass { get; set; }
        public string Message { get; set; }
    }
}
