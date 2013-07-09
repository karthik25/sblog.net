using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace sBlog.Net.MetaData.MetaData
{
    public class DatabaseSetupModelMetaData
    {
        [Required(ErrorMessage = "Connction string is required")]
        [DisplayName("Connection String")]
        public object ConnectionString { get; set; }
    }
}
