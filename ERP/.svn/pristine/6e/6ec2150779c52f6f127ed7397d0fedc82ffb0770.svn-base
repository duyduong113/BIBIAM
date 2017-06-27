using System;
using ServiceStack.DataAnnotations;
namespace ERPAPD.Models
{
    public class CRM_PageCategory_Mapping
    {   
        [PrimaryKey]
        [AutoIncrement]
        public int RowID { get; set; }
        public int CategoryID { get; set; }
        public int PageID { get; set; }
        public bool Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        [Ignore]
        public string PageName { get; set; }
    }
}