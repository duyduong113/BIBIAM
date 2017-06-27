using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ERPAPD.Models
{
    public class CRM_GET_MONEY_MONTH_NEXT
    {
        [AutoIncrement]
        public Int64 pk_gmoney_next { get; set; }
        public Int64 fk_contract { get; set; }
        public DateTime c_payment_date { get; set; }
        public double c_money { get; set; }
        public string c_note { get; set; }
        public string c_status { get; set; }
        public DateTime c_contract_date { get; set; }
        public double c_money_old { get; set; }

        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }
        public DateTime c_ngay_tt_theo_hd { get; set; }
        public double c_tien_tt_theo_hd { get; set; }
        public DateTime c_ngay_du_kien_thu { get; set; }
        public double c_tien_du_kien_thu { get; set; }
        public string c_trang_thai_tt { get; set; }
        public double c_tien_da_tt { get; set; }

        [Ignore]
        public string c_payment_date_string { get; set; }
        [Ignore]
        public string c_ngay_tt_theo_hd_string { get; set; }
        [Ignore]
        public string c_ngay_du_kien_thu_string { get; set; }
        [Ignore]
        public double c_tien_thanh_toan { get; set; }
        [Ignore]
        public int c_so_ngay_qua_han { get; set; }
        [Ignore]
        public double c_tien_con_no { get; set; }
       
        public static object Save(IEnumerable<CRM_GET_MONEY_MONTH_NEXT> items, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        try
                        {

                            var checkExits = dbConn.SingleOrDefault<CRM_GET_MONEY_MONTH_NEXT>("pk_gmoney_next= {0}", item.pk_gmoney_next);
                            if (checkExits == null)
                            {
                                var row = new CRM_GET_MONEY_MONTH_NEXT();
                                row.pk_gmoney_next = (item.pk_gmoney_next != 0) ? item.pk_gmoney_next : 0;
                                row.fk_contract = (item.fk_contract != 0) ? item.fk_contract : 0;
                                row.c_payment_date = !string.IsNullOrEmpty(item.c_payment_date_string) ? DateTime.Parse(item.c_payment_date_string) : DateTime.Parse("1900-01-01");
                                row.c_money = (item.c_tien_du_kien_thu != 0) ? item.c_tien_du_kien_thu : 0;
                                row.c_note = !string.IsNullOrEmpty(item.c_note) ? item.c_note : "";
                                row.c_status = !string.IsNullOrEmpty(item.c_status) ? item.c_status : "";
                                row.c_contract_date = !string.IsNullOrEmpty(item.c_contract_date.ToString()) ? item.c_contract_date : DateTime.Parse("1900-01-01");
                                row.c_money_old = (item.c_tien_tt_theo_hd != 0) ? item.c_tien_tt_theo_hd : 0;

                                row.c_tien_tt_theo_hd = (item.c_tien_tt_theo_hd != 0) ? item.c_tien_tt_theo_hd : 0;
                                row.c_tien_du_kien_thu = (item.c_tien_du_kien_thu != 0) ? item.c_tien_du_kien_thu : 0;
                                row.c_ngay_tt_theo_hd = !string.IsNullOrEmpty(item.c_ngay_tt_theo_hd_string) ? DateTime.Parse(item.c_ngay_tt_theo_hd_string) : DateTime.Parse("1900-01-01");
                                row.c_ngay_du_kien_thu = !string.IsNullOrEmpty(item.c_ngay_du_kien_thu_string) ? DateTime.Parse(item.c_ngay_du_kien_thu_string) : DateTime.Parse("1900-01-01");
                                row.c_trang_thai_tt = "CHUATT";
                                row.c_tien_da_tt = 0;

                                row.RowCreatedUser = username;
                                row.RowUpdatedUser = "";
                                row.RowCreatedAt = DateTime.Now;
                                row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                                dbConn.Insert(row);
                            }
                            else
                            {
                                checkExits.pk_gmoney_next = (item.pk_gmoney_next != 0) ? item.pk_gmoney_next : 0;
                                checkExits.fk_contract = (item.fk_contract != 0) ? item.fk_contract : 0;
                                checkExits.c_payment_date = !string.IsNullOrEmpty(item.c_payment_date_string) ? DateTime.Parse(item.c_payment_date_string) : DateTime.Parse("1900-01-01");
                                checkExits.c_money = (item.c_tien_du_kien_thu != 0) ? item.c_tien_du_kien_thu : 0;
                                checkExits.c_note = !string.IsNullOrEmpty(item.c_note) ? item.c_note : "";
                                checkExits.c_status = !string.IsNullOrEmpty(item.c_status) ? item.c_status : "";
                                checkExits.c_contract_date = !string.IsNullOrEmpty(item.c_tien_tt_theo_hd.ToString()) ? item.c_contract_date : DateTime.Parse("1900-01-01");
                                checkExits.c_money_old = (item.c_tien_tt_theo_hd != 0) ? item.c_tien_tt_theo_hd : 0;

                                checkExits.c_tien_tt_theo_hd = (item.c_tien_tt_theo_hd != 0) ? item.c_tien_tt_theo_hd : 0;
                                checkExits.c_tien_du_kien_thu = (item.c_tien_du_kien_thu != 0) ? item.c_tien_du_kien_thu : 0;
                                checkExits.c_ngay_tt_theo_hd = !string.IsNullOrEmpty(item.c_ngay_tt_theo_hd_string) ? DateTime.Parse(item.c_ngay_tt_theo_hd_string) : DateTime.Parse("1900-01-01");
                                checkExits.c_ngay_du_kien_thu = !string.IsNullOrEmpty(item.c_ngay_du_kien_thu_string) ? DateTime.Parse(item.c_ngay_du_kien_thu_string) : DateTime.Parse("1900-01-01");
                                
                                checkExits.RowUpdatedUser = username;
                                checkExits.RowUpdatedAt = DateTime.Now;
                                dbConn.Update(checkExits);
                            }

                           // long id = dbConn.GetLastInsertId();
                          
                        }
                        catch (Exception e)
                        {
                            return new { success = false, message = e.Message };
                        }
                    }
                    return new { success = true, message = "Save success!"};
                }
                else
                {
                    return new { success = false, message = "Data is null!" };
                }

            }


        }
    }
}