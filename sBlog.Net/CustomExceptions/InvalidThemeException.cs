using System;

namespace sBlog.Net.CustomExceptions
{
    public class InvalidThemeException : Exception
    {
        public InvalidThemeException()
        {
            
        }

        public InvalidThemeException(string message, params object[] parameters)
            : base(string.Format(message, parameters))
        {
            
        }
    }
}