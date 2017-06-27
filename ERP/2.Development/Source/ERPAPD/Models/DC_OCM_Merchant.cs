using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceStack.OrmLite;
namespace ERPAPD.Models
{
    public class DC_OCM_Merchant
    {
        public decimal PKMerchantID { get; set; }

        public string MerchantID { get; set; }

        public string MerchantName { get; set; }

        public string ShortName { get; set; }

        public string EnglishName { get; set; }

        public string SubDomain { get; set; }

        public string Phone { get; set; }

        public string MobilePhone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string PersonalEmail { get; set; }

        public string Address { get; set; }

        public int FKProvince { get; set; }

        public string Website { get; set; }

        public string SignOffDate { get; set; }

        public string ActiveDate { get; set; }

        public decimal LegalPage { get; set; }

        public string LegalPageReason { get; set; }

        public decimal CreateMerchantStatus { get; set; }

        public decimal TrainingMerchantManager { get; set; }

        public decimal PublishedStatus { get; set; }

        public decimal ApprovedStatus { get; set; }

        public string UpdatedUser { get; set; }

        public string PublishedUser { get; set; }

        public string AprrovedStatus { get; set; }

        public string UpdatedDate { get; set; }

        public string PublishedDate { get; set; }

        public string ApprovedDate { get; set; }

        public string Url { get; set; }

        public string Hotline { get; set; }

        public string Descr { get; set; }

        public string Keyword { get; set; }

        public string ShortDescr { get; set; }

        public DateTime RowCreatedAt { get; set; }

        public DateTime RowUpdatedAt { get; set; }

        public string RowCreatedUser { get; set; }

        public string RowUpdatedUser { get; set; }

        public int IsNew { get; set; }
    }
    public class DC_OCM_MerchantModelView
    {
        public decimal PKMerchantID { get; set; }

        public string MerchantName { get; set; }

    }
}