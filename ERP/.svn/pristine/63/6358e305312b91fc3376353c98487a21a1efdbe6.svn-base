using ServiceStack.DataAnnotations;
using System;
namespace ERPAPD.Models
{
    public class CRM_Customer_Profile
    {
        [AutoIncrement]
        public int ID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string BusinessCategoryID { get; set; }
        public DateTime FoundedDate { get; set; }
        public string AdType { get; set; } //Loai hinh đã 
        public string AdOnlineChannel { get; set; }
        public string AdOnlineType { get; set; }
        // public string ImplementProjectID { get; set; } //Các dự án đang thực hiện
        public string Avatar { get; set; }
        public string DescriptionCompany { get; set; }
        public string Refer { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        [Ignore]
        public string[] projectArray { get; set; }

        public CRM_Customer_Profile()
        {
            this.ID = string.IsNullOrEmpty(ID.ToString()) ? ID : 0;
            this.CustomerID = string.IsNullOrEmpty(CustomerID) ? CustomerID : "";
            this.CustomerName = string.IsNullOrEmpty(CustomerName) ? CustomerName : "";
            this.Address = string.IsNullOrEmpty(Address) ? Address : "";
            this.Phone = string.IsNullOrEmpty(Phone) ? Phone : "";
            this.Website = string.IsNullOrEmpty(Website) ? Website : "";
            this.BusinessCategoryID = string.IsNullOrEmpty(BusinessCategoryID) ? BusinessCategoryID : "";
            this.FoundedDate = string.IsNullOrEmpty(FoundedDate.ToString()) ? FoundedDate : DateTime.Now;
            this.Avatar = string.IsNullOrEmpty(Avatar) ? Avatar : Avatar;
            this.DescriptionCompany = string.IsNullOrEmpty(DescriptionCompany) ? DescriptionCompany : DescriptionCompany;
            this.Refer = string.IsNullOrEmpty(Refer) ? DescriptionCompany : Refer;
            this.Status = string.IsNullOrEmpty(Status.ToString()) ? Status : true;
            this.UpdatedAt = string.IsNullOrEmpty(UpdatedAt.ToString()) ? UpdatedAt : DateTime.Parse("1900-01-01");
            this.CreatedAt = string.IsNullOrEmpty(CreatedAt.ToString()) ? CreatedAt : DateTime.Parse("1900-01-01");
            this.CreatedBy = string.IsNullOrEmpty(CreatedBy) ? CreatedBy : "";
            this.UpdatedBy = string.IsNullOrEmpty(UpdatedBy) ? UpdatedBy : "";
        }

    }
    public class CRM_Customer_Profile_Projects
    {
        [AutoIncrement]
        public int ID { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectImages { get; set; }
        public string Place { get; set; }
        public string Size { get; set; }
        public string StatusQuo { get; set; } //Hiện trạng
        public string AmtApartment { get; set; }
        public string TraditionalChannel { get; set; } //Kenh truyền thông
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
    public class CRM_Customer_Profile_Images
    {
        [AutoIncrement]
        public int ID { get; set; }
        public string CustomerID { get; set; }
        public string ProjectID { get; set; }
        public string ImagesAdOnline { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
    public class CRM_Customer_Profile_Detail
    {
        [AutoIncrement]
        public int ID { get; set; }
        public string CustomerID { get; set; }
        public string IntroduceCompany { get; set; }
        public string HistoryCompany { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
    public class CRM_Customer_Profile_Detail_History
    {
        [AutoIncrement]
        public int ID { get; set; }
        public string CustomerID { get; set; }
        public string YearEvent { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class CRM_Customer_Profile_Meta
    {
        public string CustomerID { get; set; }
        public string Factor { get; set; }
        public string Value { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}