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
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using sBlog.Net.ShortCodeManager.Entities;
using System.IO;
using System.Text.RegularExpressions;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.ShortCodeManager
{
    public class SyntaxPossibilities : IEnumerator, IEnumerable
    {
        private List<SyntaxPossibility> _syntaxPossibilities;
        private readonly IPathMapper _pathMapper;
        private readonly string _selectedBrushes;
        private readonly string _basePath;
        int _current = -1;

        public SyntaxPossibilities(IPathMapper pathMapper, string selectedBrushes)
        {
            _pathMapper = pathMapper;
            _selectedBrushes = selectedBrushes;
            _basePath = _pathMapper.MapPath("~/Content/codeHighlighter/scripts");
            AddSyntaxPossibilities();
        }
        
        public object Current
        {
            get { return _syntaxPossibilities[_current]; }
        }

        public bool MoveNext()
        {
            _current++;
            return _current < _syntaxPossibilities.Count;
        }

        public void Reset()
        {
            _current = -1;
        }

        public IEnumerator GetEnumerator()
        {
            return _syntaxPossibilities.GetEnumerator();
        }

        public SyntaxPossibility FindPossibility(string brushName)
        {
            return _syntaxPossibilities.SingleOrDefault(p => p.BrushName == brushName);
        }

        private void AddSyntaxPossibilities()
        {
            _syntaxPossibilities = new List<SyntaxPossibility>();

            var files = Directory.GetFiles(_basePath, "shBrush*.js");
            var brushes = GetSelectedBrushes();

            files.ToList().ForEach(file =>
            {
                var r1 = new Regex(@"shBrush([A-Za-z0-9\-]+).js");
                var match = r1.Match(Path.GetFileName(file));

                r1 = new Regex(@"Brush.aliases(.*?)=(.*?)\[(.*?)\];");
                var match2 = r1.Match(File.ReadAllText(file));

                var aliases = new List<string>();
                match2.Groups[3].Value.Replace("'", "").Split(',').ToList().ForEach(split => aliases.Add(split.Trim()));

                _syntaxPossibilities.Add(new SyntaxPossibility { BrushName = match.Groups[1].Value, PossibleAliases = aliases, IsSelected = brushes.Contains(match.Groups[1].Value)});
            });
        }

        private List<string> GetSelectedBrushes()
        {
            return !string.IsNullOrEmpty(_selectedBrushes)
                              ? _selectedBrushes.Split('~').ToList()
                              : new List<string>();
        }
    }
}
