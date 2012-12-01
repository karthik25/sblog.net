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
// http://stackoverflow.com/a/1122519/312219
using System;
using System.Text;

namespace sBlog.Net.Domain.Generators
{
    public static class RandomStringGenerator
    {
        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);

        public static string RandomString()
        {
            return RandomString(32);
        }

        public static string RandomString(int size)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
