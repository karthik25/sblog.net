using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sBlog.Net.ShortCodeManager;
using sBlog.Net.ShortCodeManager.Entities;
using sBlog.Net.Tests.MockFrameworkObjects;
using sBlog.Net.Domain.Interfaces;
using System.IO;

namespace sBlog.Net.Tests.PostShortCodeManager
{
    [TestClass]
    public class PostShortCodeManagerTests
    {
        [TestMethod]
        public void Can_Generate_CSharp_Possiblity()
        {
            var aliases = new List<string> { "c#", "c-sharp", "csharp" };
            IPathMapper pathMapper = new MockPathMapper();
            var syntaxPossiblities = new SyntaxPossibilities(pathMapper, "CSharp");
            var csharpPossiblity = syntaxPossiblities.FindPossibility("CSharp");
            Assert.IsNotNull(csharpPossiblity);
            aliases.ForEach(alias => Assert.IsTrue(csharpPossiblity.PossibleAliases.Contains(alias)));
        }

        [TestMethod]
        public void Can_Generate_Ruby_Possiblity()
        {
            var aliases = new List<string> { "ruby", "rails", "ror", "rb" };
            var pathMapper = new MockPathMapper();
            var syntaxPossiblities = new SyntaxPossibilities(pathMapper, "CSharp");
            var csharpPossiblity = syntaxPossiblities.FindPossibility("Ruby");
            Assert.IsNotNull(csharpPossiblity);
            aliases.ForEach(alias => Assert.IsTrue(csharpPossiblity.PossibleAliases.Contains(alias)));
        }

        [TestMethod]
        public void Can_Generate_Java_Possiblity()
        {
            var aliases = new List<string> { "java" };
            var pathMapper = new MockPathMapper();
            var syntaxPossiblities = new SyntaxPossibilities(pathMapper, "CSharp");
            var csharpPossiblity = syntaxPossiblities.FindPossibility("Java");
            Assert.IsNotNull(csharpPossiblity);
            aliases.ForEach(alias => Assert.IsTrue(csharpPossiblity.PossibleAliases.Contains(alias)));
        }

        [TestMethod]
        public void Can_Generate_Cpp_Possiblity()
        {
            var aliases = new List<string> { "cpp", "c" };
            var pathMapper = new MockPathMapper();
            var syntaxPossiblities = new SyntaxPossibilities(pathMapper, "CSharp");
            var csharpPossiblity = syntaxPossiblities.FindPossibility("Cpp");
            Assert.IsNotNull(csharpPossiblity);
            aliases.ForEach(alias => Assert.IsTrue(csharpPossiblity.PossibleAliases.Contains(alias)));
        }

        [TestMethod]
        public void Can_Generate_JScript_Possiblity()
        {
            var aliases = new List<string> { "js", "jscript", "javascript" };
            var pathMapper = new MockPathMapper();
            var syntaxPossiblities = new SyntaxPossibilities(pathMapper, "CSharp");
            var csharpPossiblity = syntaxPossiblities.FindPossibility("JScript");
            Assert.IsNotNull(csharpPossiblity);
            aliases.ForEach(alias => Assert.IsTrue(csharpPossiblity.PossibleAliases.Contains(alias)));
        }

        [TestMethod]
        public void Can_Generate_Css_Possiblity()
        {
            var aliases = new List<string> { "css" };
            var pathMapper = new MockPathMapper();
            var syntaxPossiblities = new SyntaxPossibilities(pathMapper, "CSharp");
            var csharpPossiblity = syntaxPossiblities.FindPossibility("Css");
            Assert.IsNotNull(csharpPossiblity);
            aliases.ForEach(alias => Assert.IsTrue(csharpPossiblity.PossibleAliases.Contains(alias)));
        }

        [TestMethod]
        public void Can_Generate_Possibilities_For_Various_Brushes()
        {
            var possibleBrushes = new List<string> { "AppleScript", "AS3", "Bash", "ColdFusion",
                                                     "Cpp", "CSharp", "Css", "Delphi", "Diff",
                                                     "Erlang", "Groovy", "Java", "JavaFX", "JScript",
                                                     "Perl", "Php", "Plain", "PowerShell", "Python",
                                                     "Ruby", "Sass", "Scala", "Sql", "Vb", "Xml" };
            var pathMapper = new MockPathMapper();
            var syntaxPossiblities = new SyntaxPossibilities(pathMapper, "CSharp");
            possibleBrushes.ForEach(brush => Assert.IsNotNull(syntaxPossiblities.FindPossibility(brush)));
            possibleBrushes.ForEach(brush =>
            {
                SyntaxPossibility possibility = syntaxPossiblities.FindPossibility(brush);
                possibility.PossibleAliases.ForEach(alias => Assert.IsFalse(string.IsNullOrEmpty(alias)));
            });
        }

        [TestMethod]
        public void Path_Mapper_Can_Identify_A_Folder()
        {
            var pathMapper = new MockPathMapper();
            var path = pathMapper.MapPath("~/Content/codeHighlighter/scripts");
            Assert.IsNotNull(path);
            Assert.AreNotEqual(string.Empty, path);
            Assert.IsTrue(path.Contains("Content/codeHighlighter/scripts"));
            Assert.IsTrue(Directory.Exists(path));
        }
    }
}
