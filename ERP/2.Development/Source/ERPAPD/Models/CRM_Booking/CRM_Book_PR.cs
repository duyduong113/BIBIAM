using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Linq;

namespace ERPAPD.Models
{
    public class CRM_Book_PR
    {
        [AutoIncrement]
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

        public static long Save(CRM_Book_PR item, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (item != null)
                {
                    try
                    {
                       
                            var checkExits = dbConn.SingleOrDefault<CRM_Book_PR>("PKBookPR= {0}", item.PKBookPR);
                            if (checkExits == null)
                            {
                                var row = new CRM_Book_PR();
                                row.PKBookPR = (item.PKBookPR != 0) ? item.PKBookPR : 0;
                                row.FKCustomer = (item.FKCustomer != 0) ? item.FKCustomer : 0;
                                row.NguoiYeuCauBook = (item.NguoiYeuCauBook != 0) ? item.NguoiYeuCauBook : 0;
                                row.Code = (item.Code != "") ? item.Code : "";
                                row.ContractCode = (item.ContractCode != "") ? item.ContractCode : "";
                                row.NgayTao = DateTime.Now;
                                row.NguoiTao = (item.NguoiTao != 0) ? item.NguoiTao : 0;
                                row.FKContract = (item.FKContract != 0) ? item.FKContract : 0;
                                row.RowCreatedUser = username;
                                row.RowUpdatedUser = "";
                                row.RowCreatedAt = DateTime.Now;
                                row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                                dbConn.Insert(row);
                                return dbConn.GetLastInsertId();

                            }
                            else {
                           
                                checkExits.FKCustomer = (item.FKCustomer != 0) ? item.FKCustomer : 0;
                                checkExits.NguoiYeuCauBook = (item.NguoiYeuCauBook != 0) ? item.NguoiYeuCauBook : 0;
                                checkExits.Code = (item.Code != "") ? item.Code : "";
                                checkExits.ContractCode = (item.ContractCode != "") ? item.ContractCode : "";
                                checkExits.NgayTao = DateTime.Now;
                                checkExits.NguoiTao = (item.NguoiTao != 0) ? item.NguoiTao : 0;
                                checkExits.FKContract = (item.FKContract != 0) ? item.FKContract : 0;
                                checkExits.RowUpdatedUser = username;
                                checkExits.RowUpdatedAt = DateTime.Now;
                                dbConn.Update(checkExits);
                                return checkExits.PKBookPR;
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception() { };
                    }
                }
                else
                {
                    throw new Exception() { };
                }
            }

        }
    }

    public class CRM_Book_PR_View
    {
        public long PKBookPR { get; set; }
        public long FKCustomer { get; set; }
        public int NguoiYeuCauBook { get; set; }
        public string Code { get; set; }
        public string ContractCode { get; set; }
        public DateTime NgayTao { get; set; }
        public int NguoiTao { get; set; }
        public int FKContract { get; set; }

        public long PKBookPRLocation { get; set; }
        public long FKBookPR { get; set; }
        public int Website { get; set; }
        public int Category { get; set; }
        public int Location { get; set; }
        public string Nature { get; set; }
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
        public int Type { get; set; }

        public string CustomerName { get; set; }
        public string LocationName { get; set; }

    }
}