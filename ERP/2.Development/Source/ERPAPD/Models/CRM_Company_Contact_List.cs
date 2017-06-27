using System;

namespace ERPAPD.Models
{
    public class CRM_Company_Contact_List
    {
        public string ContactID { get; set; }
        public string CompanyID { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string Mobile { get; set; }
        public string Officephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public string Leadby { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public DateTime? RowCreatedTime { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime? RowLastUpdatedTime { get; set; }
        public string RowLastUpdatedUser { get; set; }


    }
}