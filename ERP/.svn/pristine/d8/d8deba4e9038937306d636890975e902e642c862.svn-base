using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceStack.OrmLite;
namespace ERPAPD.Models
{
    public class Deca_Customer
    {
        [AutoIncrement]
        public int Id { get; set; }
        [Index(Unique = true)]
        [StringLengthAttribute(100)]
        public string CustomerID { get; set; }
        [Required]
        [References(typeof(Deca_Company))]
        public int CompanyID { get; set; }
        private string _BranchID;
        [StringLengthAttribute(50)]
        public string BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value == null ? "" : value; }
        }
        [Ignore]
        public string BranchName { get; set; }
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
        public bool MaritalStatus { get; set; }
        [Required]
        [StringLengthAttribute(20)]
        public string PhysicalID { get; set; }
        [Required]
        [StringLengthAttribute(50)]
        public string MobilePhone { get; set; }
        [StringLengthAttribute(50)]
        public string Phone { get; set; }
        [StringLengthAttribute(255)]
        public string Email { get; set; }
        [StringLengthAttribute(50)]
        public string HomeTown { get; set; }
        [StringLengthAttribute(255)]
        public string Address { get; set; }
        [StringLengthAttribute(100)]
        public string Department { get; set; }
        [StringLengthAttribute(100)]
        public string Position { get; set; }
        public DateTime? StartWorkingDate { get; set; }
        [StringLengthAttribute(50)]
        public string Education { get; set; }
        public double Income { get; set; }
        [StringLengthAttribute(50)]
        public string PayrollBank { get; set; }
        public string CreditBank { get; set; }
        public double CreditLimit { get; set; }
        public string CardType { get; set; }
        public string CardRanking { get; set; }
        [StringLengthAttribute(20)]
        public string Status { get; set; }
        [StringLengthAttribute(50)]
        public string OnlineAccount { get; set; }
        public bool IsBlacklist { get; set; }
        public bool ExistedToken { get; set; }
        public bool IsNew { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
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
        public string CreditBankDesc
        {
            get
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    if (!string.IsNullOrEmpty(this.CreditBank))
                    {
                        var result = dbConn.FirstOrDefault<DC_Bank_Definition>("BankID={0}", this.CreditBank);
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
        public string StatusDesc
        {
            get
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    if (!string.IsNullOrEmpty(this.Status))
                    {
                        var result = dbConn.FirstOrDefault<Deca_Code_Master>("CodeID={0}", this.Status);
                        if (result == null)
                            return "Chưa biết";
                        else
                            return result.Description;
                    }
                    else return "Chưa biết";
                }
            }
        }
    }

    public class Deca_Customer_Log
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(100)]
        public string CustomerID { get; set; }
        public Deca_Customer Log { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
    }
    public class Deca_Customer_Telesale
    {
        public string CustomerID { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public double Income { get; set; }
        public double CreditLimit { get; set; }
        public string PhysicalID { get; set; }
    }
}