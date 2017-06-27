using System;
using ServiceStack.DataAnnotations;
using System.ComponentModel;

namespace ERPAPD.Models
{
    public class ERPAPD_Contract_Staff_Draff
    {
        [AutoIncrement]
        public int PKStaff { get; set; }
        public int FKContract { get; set; }
        public int UnitId { get; set; }
        public int StaffId { get; set; }
        public int DepartmentId { get; set; }
        public int GroupId { get; set; }
        public int Percent { get; set; }
        public int Money { get; set; }
        public int MoneyInYear { get; set; }
        public int MoneyWebOther { get; set; }
        public string XmlData { get; set; }
        public string Charge { get; set; }
        public int MoneyNextYear { get; set; }
        public int MoneyNextYear2 { get; set; }
        public int IsNew { get; set; }
      
        public DateTime RowCreatedAt { get; set; }
        [DefaultValue("1900-01-01")]
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        [Ignore]
        public string TeamName { get; set; }
        [Ignore]
        public string Province { get; set; }

    }
}