using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Linq;

namespace ERPAPD.Models
{
    public class CRM_BookPR_Location
    {
        [AutoIncrement]
        public long PKBookPRLocation { get; set; }
        public long FKBookPR { get; set; }
        public int Website { get; set; }
        public int Category { get; set; }
        public int Location { get; set; }
        public int Nature { get; set; }
        public DateTime NgayLen { get; set; }
        public int GioLen { get; set; }
        public int GioXuong { get; set; }
        public string Link { get; set; }
        public int VungMien { get; set; }
        public int IdOCM { get; set; }
        public int Status { get; set; }
        public float DonGia { get; set; }
        public int SoLuong { get; set; }
        public string GhiChu { get; set; }
        public DateTime PublishDate { get; set; }
        public string PublishUser { get; set; }
        public int Km { get; set; }
        public string ContractCode { get; set; }
        public long FKContract { get; set; }
        public int Type { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
      

        public static object Save(CRM_BookPR_Location item, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (item != null)
                {
                    try
                    {
                       
                            var checkExits = dbConn.SingleOrDefault<CRM_BookPR_Location>("PKBookPRLocation= {0}", item.PKBookPRLocation);
                            if (checkExits == null)
                            {
                                var row = new CRM_BookPR_Location();
                                row.PKBookPRLocation = (item.PKBookPRLocation != 0) ? item.PKBookPRLocation : 0;
                                row.FKBookPR = (item.FKBookPR != 0) ? item.FKBookPR : 0;
                                row.Website = (item.Website != 0) ? item.Website : 0;
                                row.Category = (item.Category != 0) ? item.Category : 0;
                                row.Location = (item.Location != 0) ? item.Location : 0;
                                row.Nature = (item.Nature != 0) ? item.Nature : 0;
                                row.NgayLen = !string.IsNullOrEmpty(item.NgayLen.ToString()) ? item.NgayLen : DateTime.Parse("1900-01-01");
                                row.GioLen = (item.GioLen != 0) ? item.GioLen : 0;
                                row.GioXuong = (item.GioXuong != 0) ? item.GioXuong : 0;
                                row.Link = (item.Link != "") ? item.Link : "";
                                row.VungMien = (item.VungMien != 0) ? item.VungMien : 0;
                                row.IdOCM = (item.IdOCM != 0) ? item.IdOCM : 0;
                                row.Status = (item.Status != 0) ? item.Status : 0;
                                row.DonGia = (item.DonGia != 0) ? item.DonGia : 0;
                                row.SoLuong = (item.SoLuong != 0) ? item.SoLuong : 0;
                                row.GhiChu = (item.GhiChu != "") ? item.GhiChu : "";
                                row.PublishDate = DateTime.Now;
                                row.PublishUser = (item.PublishUser != "") ? item.PublishUser : "";
                                row.Km = (item.Km != 0) ? item.Km : 0;
                                row.ContractCode = (item.ContractCode != "") ? item.ContractCode : "";
                                row.FKContract = (item.FKContract != 0) ? item.FKContract : 0;
                                row.Type = (item.Type != 0) ? item.Type : 0;
                                row.RowCreatedUser = username;
                                row.RowUpdatedUser = "";
                                row.RowCreatedAt = DateTime.Now;
                                row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                                dbConn.Insert(row);


                            }
                            else {
                                checkExits.FKBookPR = (item.FKBookPR != 0) ? item.FKBookPR : 0;
                                checkExits.Website = (item.Website != 0) ? item.Website : 0;
                                checkExits.Category = (item.Category != 0) ? item.Category : 0;
                                checkExits.Location = (item.Location != 0) ? item.Location : 0;
                                checkExits.Nature = (item.Nature != 0) ? item.Nature : 0;
                                checkExits.NgayLen = !string.IsNullOrEmpty(item.NgayLen.ToString()) ? item.NgayLen : DateTime.Parse("1900-01-01");
                                checkExits.GioLen = (item.GioLen != 0) ? item.GioLen : 0;
                                checkExits.GioXuong = (item.GioXuong != 0) ? item.GioXuong : 0;
                                checkExits.Link = (item.Link != "") ? item.Link : "";
                                checkExits.VungMien = (item.VungMien != 0) ? item.VungMien : 0;
                                checkExits.IdOCM = (item.IdOCM != 0) ? item.IdOCM : 0;
                                checkExits.Status = (item.Status != 0) ? item.Status : 0;
                                checkExits.DonGia = (item.DonGia != 0) ? item.DonGia : 0;
                                checkExits.SoLuong = (item.SoLuong != 0) ? item.SoLuong : 0;
                                checkExits.GhiChu = (item.GhiChu != "") ? item.GhiChu : "";
                                checkExits.PublishDate = DateTime.Now;
                                checkExits.PublishUser = (item.PublishUser != "") ? item.PublishUser : "";
                                checkExits.Km = (item.Km != 0) ? item.Km : 0;
                                checkExits.ContractCode = (item.ContractCode != "") ? item.ContractCode : "";
                                checkExits.FKContract = (item.FKContract != 0) ? item.FKContract : 0;
                                checkExits.Type = (item.Type != 0) ? item.Type : 0;
                                checkExits.FKContract = (item.FKContract != 0) ? item.FKContract : 0;
                                checkExits.RowUpdatedUser = username;
                                checkExits.RowUpdatedAt = DateTime.Now;
                                dbConn.Update(checkExits);
                            }
                        
                        return new { success = true, message = "Save success!" };

                    }
                    catch (Exception e)
                    {
                        return new { success = false, message = e.Message };
                    }
                }
                else
                {    // BaoHV 
                    return new { success = true, message = "Data is null!" };
                }

            }

        }
    }
}