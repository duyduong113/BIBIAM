using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceStack.OrmLite;
namespace ERPAPD.Models
{
    public class DC_OCM_Customer
    {
        public int CustomerID { get; set; }
        public string FisrtOrderDate { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string StandardMobilePhone { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public int YearOfBirth { get; set; }
        public int FKProvince { get; set; }
        public int FKDistrict { get; set; }
        public string ContactAddress { get; set; }
        public string RegisteredDate { get; set; }
        public string ActivedDate { get; set; }
        public string UpdatedDate { get; set; }
        public int HoldStatus { get; set; }
        public string HoldReason { get; set; }
        public string HoldUser { get; set; }
        public string ProcessingDate { get; set; }
        public int ActivedStatus { get; set; }
        public string ActivedCode { get; set; }
        public string CheckSum { get; set; }
        public int ChangePassword { get; set; }
        public string Url { get; set; }
        public string Source { get; set; }
        public string Medium { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }
        [Ignore]
        public string GenderText
        {
            get
            {
                if (this.Gender == "0")
                {
                    return "Nữ";
                }
                else return "Nam";
            }
        }
    }
}