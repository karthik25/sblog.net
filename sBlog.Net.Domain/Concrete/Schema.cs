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
/* Schema.cs 
 * 
 * This class extends the DefaultDisposable class,
 * Which implements the IDisposable interface for this class.
 * 
 * If you modify the class to add more disposable managed
 * resources, you can remove DefaultDisposable and implement
 * the Dispose() method yourself
 * 
 * */
using System.Linq;
using System.Data.Linq;
using sBlog.Net.Domain.Entities;
using System.Collections.Generic;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.Domain.Concrete
{
    public class Schema : DefaultDisposable, ISchema
    {
        private readonly Table<SchemaEntity> _schemaEntitiesTable;

        public Schema()
        {
            _schemaEntitiesTable = context.GetTable<SchemaEntity>();
        }

        public List<SchemaEntity> GetSchemaEntries()
        {
            return _schemaEntitiesTable.ToList();
        }

        public void AddSchemaEntity(SchemaEntity schemaEntity)
        {
            if (schemaEntity != null)
            {
                _schemaEntitiesTable.InsertOnSubmit(schemaEntity);
                context.SubmitChanges();
            }
        }

        public SchemaEntity GetMostRecentSchemaEntity()
        {
            return _schemaEntitiesTable.OrderByDescending(s => s.ScriptRunDateTime)
                                       .FirstOrDefault();
        }
    }
}
