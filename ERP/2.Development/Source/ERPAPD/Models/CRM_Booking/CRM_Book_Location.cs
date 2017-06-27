using ServiceStack.DataAnnotations;
using System;

namespace ERPAPD.Models
{
    public class CRM_Book_Location
    {
        [AutoIncrement]
        public long PKBookLocation { get; set; }
        public long FKBookReq { get; set; }
        public string Website { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Nature { get; set; }
        public DateTime NgayLen { get; set; }
        public DateTime NgayXuong { get; set; }
        public int SoNgay { get; set; }
        public int SoTuan { get; set; }
        public float Money { get; set; }
        public string Km { get; set; }
        public DateTime InputDate { get; set; }
        public DateTime? ApprovedTime { get; set; }
        public string AccountStatus { get; set; }
        public long UpbanerId { get; set; }
        public string Label { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        [Ignore]
        public bool Km2 { get; set; }
    }
}