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
using sBlog.Net.Domain.Entities;
using System.Globalization;

namespace sBlog.Net.Collections
{
    public class ArchiveCollection : IEnumerator, IEnumerable
    {
        private readonly List<PostEntity> _postsList;
        protected List<Archive> Archives = new List<Archive>();
        int _current = -1;

        public ArchiveCollection(List<PostEntity> postsList)
        {
            _postsList = postsList;
            AddArchives();
        }

        public object Current
        {
            get { return Archives[_current]; }
        }

        public bool MoveNext()
        {
            _current++;
            return _current < Archives.Count;
        }

        public void Reset()
        {
            _current = -1;
        }

        public IEnumerator GetEnumerator()
        {
            return Archives.GetEnumerator();
        }

        private void AddArchives()
        {
            var dateTimeFormatInfo = new DateTimeFormatInfo();
            var group = _postsList.GroupBy(p => new { p.PostAddedDate.Year, p.PostAddedDate.Month })
                                  .OrderByDescending(g => g.Key.Year)
                                  .ThenByDescending(g => g.Key.Month);
            var archives = @group.Select(g => new Archive
                           {
                               MonthYear = string.Format("{0} {1} ({2})", dateTimeFormatInfo.GetMonthName(g.Key.Month), g.Key.Year, g.Count()),
                               Year = g.Key.Year.ToString(CultureInfo.InvariantCulture),
                               Month = g.Key.Month.ToString("00")
                           }).ToList();
            Archives.AddRange(archives);
        }
    }
}