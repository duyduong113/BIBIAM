using ERPAPD.Models;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceStack.OrmLite;

namespace ERPAPD.Models
{
    public class Deca_Potential_Customer
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(100)]
        public string CustomerID { get; set; }
        [Required]
        [References(typeof(Deca_Company))]
        public int CompanyID { get; set; }
        [StringLengthAttribute(50)]
        public string CompanyName { get; set; }
        [Required]
        [StringLengthAttribute(50)]
        public string EmployeeID { get; set; }
        [Required]
        [StringLengthAttribute(100)]
        public string Fullname { get; set; }
        [StringLengthAttribute(20)]
        public string Sex { get; set; }
        public DateTime? Birthday { get; set; }
        [Required]
        [StringLengthAttribute(20)]
        public string PhysicalID { get; set; }
        [StringLengthAttribute(50)]
        public string HomeTown { get; set; }
        [StringLengthAttribute(100)]
        public string Department { get; set; }
        [StringLengthAttribute(100)]
        public string Position { get; set; }
        public string CustomerGroup { get; set; }
        public DateTime? StartWorkingDate { get; set; }
        [StringLengthAttribute(50)]
        public string Education { get; set; }
        [StringLengthAttribute(50)]
        public string Phone { get; set; }
        [StringLengthAttribute(255)]
        public string Email { get; set; }
        public double Income { get; set; }
        [StringLengthAttribute(50)]
        public string PayrollBank { get; set; }
        public string RequestCreditBank { get; set; }
        public double CreditLimit { get; set; }

        [StringLengthAttribute(255)]
        public string Address { get; set; }
        public bool Active { get; set; }
        public bool HaveCard { get; set; }
        public bool IsNew { get; set; }


        [StringLengthAttribute(20)]
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }

        [StringLength(50)]
        public string SourceType { get; set; }

        [StringLength(50)]
        public string CreditCardStatus { get; set; }

        //vuongnd-add note
        [StringLengthAttribute(255)]
        public string DecaNote { get; set; }
        [StringLengthAttribute(255)]
        public string BankNote { get; set; }

        public bool IsForm { get; set; }
        [StringLength(50)]
        public string RejectReason { get; set; }
        [StringLength(50)]
        public string PhoneTransaction { get; set; }
        [StringLength(1024)]
        public string CurrentAddress { get; set; }

        public DateTime? CICBadDebt { get; set; }
        //request bank tracking
        public DateTime? SubmitedAt { get; set; }
        public DateTime? DecaRequested { get; set; }
        public DateTime? WaitingProfile { get; set; }
        public DateTime? BankDenied { get; set; }
        public DateTime? IssuedCard { get; set; }
        public DateTime? Done { get; set; }
        public DateTime? Cancelled { get; set; }

        [StringLengthAttribute(20)]
        public string PhysicalIDBank { get; set; }
        private string _BranchID;
        [StringLengthAttribute(50)]
        public string BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value == null ? "" : value; }
        }
        [Ignore]
        public string BranchName
        {
            get;
            set;
        }

        [Ignore]
        public string Error { get; set; }
        [Ignore]
        public double Duration
        {
            get
            {
                if (SubmitedAt != null)
                {
                    return (DateTime.Now - SubmitedAt.Value).TotalHours;
                }
                return 0;
            }
        }
        [Ignore]
        public string Gender
        {
            get
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    if (!string.IsNullOrEmpty(this.Sex))
                    {
                        var result = dbConn.FirstOrDefault<Deca_Code_Master>("CodeType={0}", "Gender");
                        if (result == null)
                            return "Chưa biết";
                        else
                            return result.Description;
                    }
                    else return "Chưa biết";
                }
            }
        }
        [Ignore]
        public string HomeTownDescription
        {
            get
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    if (!string.IsNullOrEmpty(this.HomeTown))
                    {
                        var result = dbConn.FirstOrDefault<DC_OCM_Territory>("TerritoryID={0}", this.HomeTown);
                        if (result == null)
                            return "Chưa biết";
                        else
                            return result.TerritoryName;
                    }
                    else return "Chưa biết";
                }
            }
        }
        [Ignore]
        public string PayrollBankDesc
        {
            get
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    if (!string.IsNullOrEmpty(this.PayrollBank))
                    {
                        var result = dbConn.FirstOrDefault<DC_Bank_Definition>("BankID={0}", this.PayrollBank);
                        if (result == null)
                            return "Chưa biết";
                        else
                            return result.BankName;
                    }
                    else return "Chưa biết";
                }
            }
        }

        [Ignore]
        public int OrderbyCreditCardStatus { get; set; }

        [Ignore]
        public string ResultCheck { get; set; }
    }

    public class Deca_Potential_Customer_Log
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(100)]
        public string CustomerID { get; set; }
        public Deca_Potential_Customer Log { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
    }
}