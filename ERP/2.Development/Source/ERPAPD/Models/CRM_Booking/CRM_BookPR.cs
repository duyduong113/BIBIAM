using ServiceStack.DataAnnotations;
using System;

namespace ERPAPD.Models
{
    //public class CRM_BookPRMoi
    //{
    //    [AutoIncrement]
    //    public int ID { get; set; }
    //    public int BookCode { get; set; }
    //    public string ContractCode { get; set; }
    //    public string Customer { get; set; }
    //    public string Website { get; set; }
    //    public string Category { get; set; }
    //    public string Location { get; set; }
    //    public DateTime DateUp { get; set; }
    //    public int HourUp { get; set; }
    //    public string Link { get; set; }
    //    public string Region { get; set; }
    //    public string Staff { get; set; }
    //    public string Status { get; set; }
    //    public DateTime DateBook { get; set; }
    //    [Ignore]
    //    public string CustomerName { get; set; }
    //}
    public class CRM_BookPR_View
    {
        public long ID { get; set; }
        public long PKBookPR { get; set; }
        public long FKCustomer { get; set; }
        public long NguoiYeuCauBook { get; set; }
        public string Code { get; set; }
        public string ContractCode { get; set; }
        public DateTime NgayTao { get; set; }
        public int NguoiTao { get; set; }
        public long FKContract { get; set; }
        public long PKBookPRLocation { get; set; }
        public long FKBookPR { get; set; }
        public int Website { get; set; }
        public int Category { get; set; }
        public int Location { get; set; }
        //public int Nature { get; set; }
        public DateTime NgayLen { get; set; }
        public int GioLen { get; set; }
        public int GioXuong { get; set; }
        public string Link { get; set; }
        public int VungMien { get; set; }
        public long IdOCM { get; set; }
        public int Status { get; set; }
        public float DonGia { get; set; }
        public int SoLuong { get; set; }
        public string GhiChu { get; set; }
        public DateTime PublishDate { get; set; }
        public string PublishUser { get; set; }
        public int Km { get; set; }
        public int Type { get; set; }
        public string CustomerName { get; set; }
        public string LocationName { get; set; }
        [Ignore]
        public int IDBookPRLocation { get; set; }
        [Ignore]
        public string EmployeeName { get; set; }
        
    }
    public class CRM_BookPR
    {
        [AutoIncrement]
        public long ID { get; set; }
        public long PKBookPR { get; set; }
        public long FKCustomer { get; set; }
        public int NguoiYeuCauBook { get; set; }
        public string Code { get; set; }
        public string ContractCode { get; set; }
        public DateTime NgayTao { get; set; }
        public int NguoiTao { get; set; }
        public int FKContract { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
    }
    public class CRM_BookPRLocation
    {
        [AutoIncrement]
        public long ID { get; set; }
        public long PKBookPRLocation { get; set; }
        public long FKBookPR { get; set; }
        public int Website { get; set; }
        public int Category { get; set; }
        public int Location { get; set; }
       // public int Nature { get; set; }
        public DateTime NgayLen { get; set; }
        public int GioLen { get; set; }
        public int GioXuong { get; set; }
        
        public string Link { get; set; }
        public int VungMien { get; set; }
        public int IdOCM { get; set; }
        public int Status { get; set; }
        public float DonGia { get; set; }
        public int SoLuong { get; set; }
        //public string GhiChu { get; set; }
    
        public string PublishUser { get; set; }
        public int Km { get; set; }
        public int Type { get; set; }
        [Ignore]
        public DateTime PublishDate { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
  
        
    }
}