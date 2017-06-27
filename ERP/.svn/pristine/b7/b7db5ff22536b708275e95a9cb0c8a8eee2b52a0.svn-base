using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace ERPAPD.Models
{
    public class CRM_BaoLanh
    {
        [AutoIncrement]
        public Int64 pk_bao_lanh { get; set; }
        public Int64 fk_contract { get; set; }
        public int c_nguoi_bao_lanh { get; set; }
        public string c_nguoi_ky_bao_lanh { get; set; }
        public long c_tien_bao_lanh { get; set; }
        public DateTime c_ngay_bao_lanh { get; set; }
        public DateTime c_ngay_tra_no { get; set; }
        public long c_tien_chua_thanh_toan { get; set; }
        public long c_tru_luong { get; set; }
        public string c_ghi_chu { get; set; }
        public DateTime c_input_date { get; set; }
        public int c_nguoi_tao { get; set; }
        public long c_hoan_luong { get; set; }
        public long c_tien_da_thanh_toan { get; set; }
        public long c_da_tru_luong { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }
        [Ignore]
        public string c_ngay_bao_lanh_string { get; set; }
        [Ignore]
        public string c_ngay_tra_no_string { get; set; }
        [Ignore]
        public string NguoiBaoLanhName { get; set; }
        [Ignore]
        public string NguoiKyBaoLanhName { get; set; }
        public static object Save(IEnumerable<CRM_BaoLanh> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_BaoLanh>("pk_bao_lanh= {0}", request.pk_bao_lanh);
                            if (checkExits == null)
                            {
                                if (request.fk_contract != 0)
                                {
                                    var row = new CRM_BaoLanh();
                                    row.fk_contract = (request.fk_contract != 0) ? request.fk_contract : 0;
                                    row.c_nguoi_bao_lanh = (request.c_nguoi_bao_lanh != 0) ? request.c_nguoi_bao_lanh : 0;
                                    row.c_nguoi_ky_bao_lanh = (request.c_nguoi_ky_bao_lanh != "") ? request.c_nguoi_ky_bao_lanh : "0";
                                    row.c_tien_bao_lanh = (request.c_tien_bao_lanh != 0) ? request.c_tien_bao_lanh : 0;

                                    DateTime checkDate = DateTime.Now;
                                    if (DateTime.TryParseExact(request.c_ngay_bao_lanh_string.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out checkDate))
                                    {
                                        row.c_ngay_bao_lanh = !string.IsNullOrEmpty(checkDate.ToString()) ? checkDate : DateTime.Parse("1900-01-01");
                                    }

                                    if (DateTime.TryParseExact(request.c_ngay_tra_no_string.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out checkDate))
                                    {
                                        row.c_ngay_tra_no = !string.IsNullOrEmpty(checkDate.ToString()) ? checkDate : DateTime.Parse("1900-01-01");
                                    }

                                    row.c_tien_chua_thanh_toan = (request.c_tien_chua_thanh_toan != 0) ? request.c_tien_chua_thanh_toan : 0;
                                    row.c_tru_luong = (request.c_tru_luong != 0) ? request.c_tru_luong : 0;
                                    row.c_ghi_chu = !string.IsNullOrEmpty(request.c_ghi_chu) ? request.c_ghi_chu : "";
                                    row.c_input_date = DateTime.Now;
                                    row.c_nguoi_tao = 0;
                                    row.c_hoan_luong = (request.c_hoan_luong != 0) ? request.c_hoan_luong : 0;
                                    row.c_tien_da_thanh_toan = (request.c_tien_da_thanh_toan != 0) ? request.c_tien_da_thanh_toan : 0;
                                    row.c_da_tru_luong =  0;
                                    row.IsNew = 1;

                                    row.RowCreatedUser = username;
                                    row.RowUpdatedUser = "";
                                    row.RowCreatedAt = DateTime.Now;
                                    row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                                    dbConn.Insert(row);
                                }
                                else
                                {
                                    return new { success = false, message = "Data is null!" };
                                }
                            }
                            else {

                                checkExits.fk_contract = (request.fk_contract != 0) ? request.fk_contract : 0;
                                checkExits.c_nguoi_bao_lanh = (request.c_nguoi_bao_lanh != 0) ? request.c_nguoi_bao_lanh : 0;
                                checkExits.c_nguoi_ky_bao_lanh = (request.c_nguoi_ky_bao_lanh != "") ? request.c_nguoi_ky_bao_lanh : "0";
                                checkExits.c_tien_bao_lanh = (request.c_tien_bao_lanh != 0) ? request.c_tien_bao_lanh : 0;

                                DateTime checkDate = DateTime.Now;
                                if (DateTime.TryParseExact(request.c_ngay_bao_lanh_string.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out checkDate))
                                {
                                    checkExits.c_ngay_bao_lanh = !string.IsNullOrEmpty(checkDate.ToString()) ? checkDate : DateTime.Parse("1900-01-01");
                                }

                                if (DateTime.TryParseExact(request.c_ngay_tra_no_string.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out checkDate))
                                {
                                    checkExits.c_ngay_tra_no = !string.IsNullOrEmpty(checkDate.ToString()) ? checkDate : DateTime.Parse("1900-01-01");
                                }

                                checkExits.c_tien_chua_thanh_toan = (request.c_tien_chua_thanh_toan != 0) ? request.c_tien_chua_thanh_toan : 0;
                                checkExits.c_tru_luong = (request.c_tru_luong != 0) ? request.c_tru_luong : 0;
                                checkExits.c_ghi_chu = !string.IsNullOrEmpty(request.c_ghi_chu) ? request.c_ghi_chu : "";
                                checkExits.c_input_date = DateTime.Now;
                                checkExits.c_nguoi_tao = 0;
                                checkExits.c_hoan_luong = (request.c_hoan_luong != 0) ? request.c_hoan_luong : 0;
                                checkExits.c_tien_da_thanh_toan = (request.c_tien_da_thanh_toan != 0) ? request.c_tien_da_thanh_toan : 0;
                                checkExits.c_da_tru_luong = 0;
                              
                                checkExits.RowUpdatedUser = username;
                                checkExits.RowUpdatedAt = DateTime.Now;
                                dbConn.Update(checkExits);
                            }
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