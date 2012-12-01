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

        protected virtual DateTime GetDateTime()
        {
            return DateTime.Now;
        }      

        private void AddArchives()
        {
            var dateTime = GetDateTime();
            var currentYear = dateTime.Year;
            var currentMonth = dateTime.Month;
            var dateTimeFormatInfo = new DateTimeFormatInfo();

            var post = _postsList.OrderByDescending(p => p.PostEditedDate).LastOrDefault();

            if (post != null)
            {
                var endYear = post.PostAddedDate.Year;

                while (currentYear >= endYear)
                {
                    if (_postsList.Any(p => p.PostAddedDate.Year == currentYear && p.PostAddedDate.Month == currentMonth))
                    {
                        Archives.Add(new Archive
                        {
                            Year = currentYear.ToString(),
                            Month = currentMonth.ToString("00"),
                            MonthYear = string.Format("{0} {1}", dateTimeFormatInfo.GetMonthName(currentMonth), currentYear)
                        });
                    }

                    if (currentMonth - 1 < 1)
                    {
                        currentMonth = 12;
                        currentYear--;
                    }
                    else
                    {
                        currentMonth--;
                    }
                }
            }
        }
    }
}