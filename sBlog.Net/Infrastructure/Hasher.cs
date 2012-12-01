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
using System.Reflection;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.Infrastructure
{
    public static class Hasher
    {
        private const string DefaultHasher = "sBlog.Net.Domain.Hashers.Md5Hasher";

        public static IHasher Instance
        {
            get
            {
                IHasher iHasher;
                var assemblyName = Assembly.Load("sBlog.Net.Domain").CodeBase;
                try
                {
                    var hasher = ApplicationConfiguration.HasherTypeName;
                    iHasher = (IHasher)Activator.CreateInstanceFrom(assemblyName, hasher).Unwrap();
                }
                catch
                {
                    var instance = Activator.CreateInstanceFrom(assemblyName, DefaultHasher).Unwrap();
                    iHasher = (IHasher)instance;
                }
                
                return iHasher;
            }
        }
    }
}