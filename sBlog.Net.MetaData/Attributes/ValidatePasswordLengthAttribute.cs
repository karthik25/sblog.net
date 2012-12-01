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
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace sBlog.Net.MetaData.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "'{0}' must be at between {1}  and {2} characters long.";
        private readonly int _minCharacters;
        private readonly int _maxCharacters;

        public ValidatePasswordLengthAttribute(int minCharacters, int maxCharacters)
            : base(DefaultErrorMessage)
        {
            _minCharacters = minCharacters;
            _maxCharacters = maxCharacters;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                name, _minCharacters,_maxCharacters);
        }

        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            return (valueAsString != null && valueAsString.Length >= _minCharacters);
        }
    }
}