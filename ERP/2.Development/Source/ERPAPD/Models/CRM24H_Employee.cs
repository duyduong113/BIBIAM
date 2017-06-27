using ServiceStack.DataAnnotations;
using System;

namespace ERPAPD.Models
{
    public class ERPAPD_Employee
    {
        [AutoIncrement]
        public int RowID { get; set; }
        [AutoIncrement]
        public string PKEmployeeID { get; set; }
        public int RefEmployeeID { get; set; }
        public int  FKUnit { get; set; }
        public int  FKPosition { get; set; }
        public string  Code { get; set; }
        public string  Name { get; set; }
        public string  Address { get; set; }
        public string  Email { get; set; }
        public string  TelLocal { get; set; }
        public string  TelePhone { get; set; }
        public string  TelMobile { get; set; }
        public string  TelHome { get; set; }
        public string  Fax { get; set; }
        public string  UserName { get; set; }
        public string  PassWord { get; set; }
        public int  Order { get; set; }
        public int Status { get; set; }
        public int Role { get; set; }
        public int Sex { get; set; }
        public DateTime BirthDay { get; set; }
        public int  Group { get; set; }
        public string  InternalOrder { get; set; }
        public string  DN { get; set; }
        public string  AsciiName { get; set; }
        public string  UpperName { get; set; }
        public DateTime StartWorkDate { get; set; }
        public string  EmployeeLink { get; set; }
        public int  StatusOff { get; set; }
        public DateTime DateOff { get; set; }
        public int  Display { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string  RowCreatedUser { get; set; }
        public string  RowUpdatedUser { get; set; }
        public int  IsNew { get; set; }
        [Ignore]
        public int Unit { get; set; }
         [Ignore]
        public bool IsActive { get; set; }
         [Ignore]
         public bool isStatusOff { get; set; }
         [Ignore]
         public int Region { get; set; }
         [Ignore]
         public bool isDisplay { get; set; }
        
    }
}