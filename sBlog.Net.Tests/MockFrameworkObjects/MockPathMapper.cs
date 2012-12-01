using System;
using sBlog.Net.Domain.Interfaces;
using System.Reflection;

namespace sBlog.Net.Tests.MockFrameworkObjects
{
    internal class MockPathMapper : IPathMapper
    {
        public string MapPath(string relativePath)
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var baseDirectory = path.Substring(0, path.IndexOf("sBlog.Net.Tests")) + "sBlog.Net" + relativePath.Substring(1);
            return baseDirectory;
        }
    }
}
