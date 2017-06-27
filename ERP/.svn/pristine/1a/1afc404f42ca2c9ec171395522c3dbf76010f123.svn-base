using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;
using System.Data.SqlClient;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Globalization;

namespace ERPAPD.Models
{
    public class CRM_Contract
    {
        [AutoIncrement]
        public Int32 pk_contract { get; set; }
        public string c_code { get; set; }
        public string c_internal_code { get; set; }
        public string c_name { get; set; }
        public string c_customer_code { get; set; }
        public string c_agency_code { get; set; }
        public string c_contract_type { get; set; }
        public DateTime? c_begin_date { get; set; }
        public DateTime? c_end_date { get; set; }
        public int c_week { get; set; }
        public DateTime? c_update { get; set; }
        public DateTime? c_down_date { get; set; }
        public DateTime? c_real_update { get; set; }
        public DateTime? c_real_downdate { get; set; }
        public string c_opportunity_code { get; set; }
        public string c_marketing_code { get; set; }
        public string c_promotion_code { get; set; }
        public int c_check_vat { get; set; }
        public double c_total_money { get; set; } // Doanh so ky
        public double c_total_vat { get; set; } //Total VAT
        public double c_total_value { get; set; } //Tong gia tri hop dong
        public double c_total_money_in_year { get; set; } //Doanh so thuc hien
        public double c_total_money_next_year { get; set; } //Doanh so chuyen nam sau
        public double c_customer_total_money { get; set; } // Doanh so khac
        public double c_outside_money { get; set; } //Website khac
        public double c_guarantee_money { get; set; }
        public double c_24h_total_money { get; set; }
        public double c_commission { get; set; }
        public double c_chi_phi_vp { get; set; }
        public double c_chi_phi_gg { get; set; }
        public string c_debt_status { get; set; }
        ///  tiền còn nợ
        public double c_balance { get; set; }
        public int c_unit_id { get; set; }
        public string c_category_code { get; set; }
        public string c_note { get; set; }
        public string c_website_status { get; set; }
        public string c_ascii_name { get; set; }
        public string c_upper_name { get; set; }
        public int c_staff_id { get; set; }
        public string c_status { get; set; }
        public string c_file_name { get; set; }
        public int c_area_id { get; set; }
        public string c_str_update { get; set; }
        public string c_satisfaction_status { get; set; }
        public string c_xml_data { get; set; }
        public DateTime? c_revenue_date { get; set; }
        public DateTime? c_guarantee_date { get; set; }
        public int c_guarantee_staff { get; set; }
        public string c_website { get; set; }
        public Int32 c_contract_number { get; set; }
        public string c_str_real_update { get; set; }
        public string c_location { get; set; }
        public string c_labels { get; set; }
        public double c_get_money_next_week { get; set; }
        public int c_ageny_check { get; set; }
        public string c_agency_type { get; set; }
        public double c_dt_ngoai_chinh_sach { get; set; } //Doanh thu ngoai chinh sach
        public double c_phan_tram_ncs { get; set; }
        public int c_24h_singer { get; set; }
        public string c_customer_singer { get; set; }
        public double c_mar_money_in_year { get; set; }
        public double c_mar_money_next_year { get; set; }
        public double c_mar_money_other { get; set; }
        public string c_customer_acc { get; set; }
        public int c_check_mkt { get; set; }
        public string c_note_mkt { get; set; }
        public int c_banner_up { get; set; }
        public DateTime? c_input_date { get; set; }
        public string c_book_code { get; set; }
        public int c_cancel_up { get; set; }
        public double c_arrears_money { get; set; }
        public DateTime? c_ngay_huy_hd { get; set; }
        public DateTime? c_ngay_bao_huy { get; set; }
        public double c_ds_huy { get; set; }
        public string c_ly_do_huy { get; set; }
        public int c_year { get; set; }
        public int c_month { get; set; }
        public int c_huy_status { get; set; }
        public int IsNew { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public string c_service_type { get; set; }
        [Ignore]
        public double tien_xuat_hoa_don { get; set; }
        [Ignore]
        public double c_phan_tram_phan_bo { get; set; }
        [Ignore]
        public string c_contract_code { get; set; } // Ma hop dong
        [Ignore]
        public string c_customer_name { get; set; } //Ten KH
        [Ignore]
        public string c_customer_type { get; set; } //Ten KH
        [Ignore]
        public int c_regionid { get; set; }
        [Ignore]
        public string c_region_name { get; set; }
        [Ignore]
        public int c_teamid { get; set; }
        [Ignore]
        public int c_staffid { get; set; }
        [Ignore]
        public string c_staff_name { get; set; }
        [Ignore]
        public double c_tien_khong_tinh { get; set; }
        [Ignore]
        public string la_dai_ly { get; set; }
        [Ignore]
        public string dang_quang_cao { get; set; }
        [Ignore]
        public double c_dt_da_qc_den_hom_nay { get; set; }
        [Ignore]
        public double c_dt_da_xuat_ban { get; set; }
        [Ignore]
        public double c_payment_money { get; set; }
        [Ignore]
        public string c_status_name { get; set; }
        [Ignore]
        public string c_teamname { get; set; }
        [Ignore]
        public double chi_phi_marketing_da_tra { get; set; }
        [Ignore]
        public double chi_phi_marketing_chua_tra { get; set; }

        [Ignore]
        public double tien_chua_xuat_hoa_don { get; set; }
        [Ignore]
        public string ghi_chu_dang_qc { get; set; }
        [Ignore]
        public DateTime c_payment_date { get; set; }
        [Ignore]
        public DateTime c_ngay_ve_hai_chieu { get; set; }
        [Ignore]
        public DateTime c_ngay_len_xuong_thuc_te { get; set; }
        [Ignore]
        public int c_het_han { get; set; }
        [Ignore]
        public int c_het_gia_tri { get; set; }

        [Ignore]
        public DateTime c_min_ngay_len_thuc_te { get; set; }

        [Ignore]
        public DateTime c_max_ngay_len_xuong_thuc_te { get; set; }
        //string date
        [Ignore]
        public string StrRevenueDate { get; set; }
        [Ignore]
        public string StrRevenueDateCancel { get; set; }
        [Ignore]
        public string StrEndDate { get; set; }

        [Ignore]
        public string StrTotalMoney { get; set; }
        [Ignore]
        public string StrTotalVat { get; set; }
        [Ignore]
        public string StrTotalValue { get; set; }
        [Ignore]
        public string StrTotalInYear { get; set; }
        [Ignore]
        public string StrTotalNextYear { get; set; }

        [Ignore]
        public string StrTotalCustomerTotalMoney { get; set; }
        [Ignore]
        public string StrOutsideMoney { get; set; }
        [Ignore]
        public string StrDSOutCS { get; set; }
        [Ignore]
        public string StrDSHuy { get; set; }
        [Ignore]
        public string StrDateHuy { get; set; }

       
        // v field to export bang ke
        [Ignore]
        public DateTime c_ngay_len { get; set; }
        [Ignore]
        public DateTime c_ngay_xuong { get; set; }
        [Ignore]
        public string c_website_name { get; set; }
        [Ignore]
        public string c_category_name { get; set; }
        [Ignore]
        public string c_location_name { get; set; }
        [Ignore]
        public double c_don_gia_public { get; set; }
        [Ignore]
        public double c_don_gia_tt { get; set; }
        [Ignore]
        public double c_tong_tien { get; set; }
        [Ignore]
        public int c_so_ngay { get; set; }
        [Ignore]
        public int c_number { get; set; }
        [Ignore]
        public double c_don_gia_theo_ngay { get; set; }
        [Ignore]
        public int c_ngay_bu { get; set; }
        public static DataTable Export(int Page, int PageSize, string where)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Page", Page));
            param.Add(new SqlParameter("@PageSize", PageSize));
            param.Add(new SqlParameter("@WhereCondition", where));
            param.Add(new SqlParameter("@Sort", ""));
            return new SqlHelper().ExecuteQuery("p_Get_AllContract", param);
        }
        public static DataTable GetPage(int Page, int PageSize, string where)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Page", Page));
            param.Add(new SqlParameter("@PageSize", PageSize));
            param.Add(new SqlParameter("@WhereCondition", where));
            param.Add(new SqlParameter("@Sort", ""));
            return new SqlHelper().ExecuteQuery("p_Get_AllContract", param);
            //var lst = new List<CRM_Contract>();
            //foreach (DataRow row in dt.Rows)
            //{
            //    var item = new CRM_Contract();
            //    item.pk_contract = !row.IsNull("pk_contract") ? Convert.ToInt32(row["pk_contract"].ToString()) : 0;
            //    item.c_contract_code = !row.IsNull("c_contract_code") ? row["c_contract_code"].ToString() : "";
            //    item.c_contract_type = !row.IsNull("c_contract_type") ? row["c_contract_type"].ToString() : "";
            //    item.c_service_type = !row.IsNull("c_service_type") ? row["c_service_type"].ToString() : "";
            //    item.c_book_code = !row.IsNull("c_book_code") ? row["c_book_code"].ToString() : "";
            //    item.c_ds_huy = !row.IsNull("c_ds_huy") ? Convert.ToDouble(row["c_ds_huy"].ToString()) : 0;
            //    item.c_phan_tram_phan_bo = !row.IsNull("c_phan_tram_phan_bo") ? Convert.ToDouble(row["c_phan_tram_phan_bo"].ToString()) : 0;
            //    item.c_year = !row.IsNull("c_year") ? Convert.ToInt32(row["c_year"].ToString()) : 0;
            //    item.c_week = !row.IsNull("c_week") ? Convert.ToInt32(row["c_week"].ToString()) : 0;
            //    item.c_month = !row.IsNull("c_month") ? Convert.ToInt32(row["c_month"].ToString()) : 0;
            //    item.c_customer_code = !row.IsNull("c_book_code") ? row["c_book_code"].ToString() : "";
            //    item.c_customer_name = !row.IsNull("c_customer_name") ? row["c_customer_name"].ToString() : "";
            //    item.c_ngay_ve_hai_chieu = !row.IsNull("c_ngay_ve_hai_chieu") ? DateTime.Parse(row["c_ngay_ve_hai_chieu"].ToString()) : DateTime.Parse("01/01/1900");
            //    item.c_min_ngay_len_thuc_te = !row.IsNull("c_min_ngay_len_thuc_te") ? DateTime.Parse(row["c_min_ngay_len_thuc_te"].ToString()) : DateTime.Parse("01/01/1900");
            //    item.c_max_ngay_len_xuong_thuc_te = !row.IsNull("c_max_ngay_len_xuong_thuc_te") ? DateTime.Parse(row["c_max_ngay_len_xuong_thuc_te"].ToString()) : DateTime.Parse("01/01/1900");
            //    item.c_het_han = !row.IsNull("c_het_han") ? Convert.ToInt32(row["c_het_han"].ToString()) : 0;
            //    item.c_het_gia_tri = !row.IsNull("c_het_gia_tri") ? Convert.ToInt32(row["c_het_gia_tri"].ToString()) : 0;
            //    item.c_teamname = !row.IsNull("c_teamname") ? row["c_teamname"].ToString() : "";
            //    item.c_labels = !row.IsNull("c_labels") ? row["c_labels"].ToString() : "";
            //    item.c_regionid = !row.IsNull("c_regionid") ? Convert.ToInt32(row["c_regionid"].ToString()) : 0;
            //    item.c_teamid = !row.IsNull("c_teamid") ? Convert.ToInt32(row["c_teamid"].ToString()) : 0;
            //    item.c_staffid = !row.IsNull("c_staffid") ? Convert.ToInt32(row["c_staffid"].ToString()) : 0;
            //    item.c_staff_name = !row.IsNull("c_staff_name") ? row["c_staff_name"].ToString() : "";
            //    item.c_total_money = !row.IsNull("c_total_money") ? Convert.ToDouble(row["c_total_money"].ToString()) : 0;
            //    item.c_total_vat = !row.IsNull("c_total_vat") ? Convert.ToDouble(row["c_total_vat"].ToString()) : 0;
            //    item.c_total_value = !row.IsNull("c_total_value") ? Convert.ToDouble(row["c_total_value"].ToString()) : 0;
            //    item.c_total_money_in_year = !row.IsNull("c_total_money_in_year") ? Convert.ToDouble(row["c_total_money_in_year"].ToString()) : 0;
            //    item.c_total_money_next_year = !row.IsNull("c_total_money_next_year") ? Convert.ToDouble(row["c_total_money_next_year"].ToString()) : 0;
            //    item.c_tien_khong_tinh = !row.IsNull("c_tien_khong_tinh") ? Convert.ToDouble(row["c_tien_khong_tinh"].ToString()) : 0;
            //    item.c_get_money_next_week = !row.IsNull("c_get_money_next_week") ? Convert.ToDouble(row["c_get_money_next_week"].ToString()) : 0;
            //    item.c_commission = !row.IsNull("c_commission") ? Convert.ToDouble(row["c_commission"].ToString()) : 0;
            //    item.la_dai_ly = !row.IsNull("la_dai_ly") ? row["la_dai_ly"].ToString() : "";
            //    item.dang_quang_cao = !row.IsNull("dang_quang_cao") ? row["dang_quang_cao"].ToString() : "";
            //    item.c_status = !row.IsNull("c_status") ? row["c_status"].ToString() : "";
            //    item.c_status_name = !row.IsNull("c_status_name") ? row["c_status_name"].ToString() : "";
            //    item.c_dt_da_qc_den_hom_nay = !row.IsNull("c_dt_da_qc_den_hom_nay") ? Convert.ToDouble(row["c_dt_da_qc_den_hom_nay"].ToString()) : 0;
            //    item.c_dt_da_xuat_ban = !row.IsNull("c_dt_da_xuat_ban") ? Convert.ToDouble(row["c_dt_da_xuat_ban"].ToString()) : 0;
            //    item.c_payment_money = !row.IsNull("c_payment_money") ? Convert.ToDouble(row["c_payment_money"].ToString()) : 0;
            //    item.c_balance = !row.IsNull("c_balance") ? Convert.ToDouble(row["c_balance"].ToString()) : 0;
            //    item.tien_xuat_hoa_don = !row.IsNull("tien_xuat_hoa_don") ? Convert.ToDouble(row["tien_xuat_hoa_don"].ToString()) : 0;
            //    item.tien_chua_xuat_hoa_don = !row.IsNull("tien_chua_xuat_hoa_don") ? Convert.ToDouble(row["tien_chua_xuat_hoa_don"].ToString()) : 0;

            //    lst.Add(item);
            //}
            //request.Filters = null;
            //DataSourceResult result = new DataSourceResult();
            //result.Data = lst;
            //result.Total = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["RowCount"]) : 0;
            //return result;

        }
        public List<CRM_Contract> GetExport(DataSourceRequest request, string whereCondition)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Page", request.Page));
            param.Add(new SqlParameter("@PageSize", request.PageSize));
            param.Add(new SqlParameter("@WhereCondition", whereCondition));
            param.Add(new SqlParameter("@Sort", ""));
            DataTable dt = new SqlHelper().ExecuteQuery("p_Get_AllContract", param);
            var lst = new List<CRM_Contract>();
            foreach (DataRow row in dt.Rows)
            {
                var item = new CRM_Contract();
                item.pk_contract = !row.IsNull("pk_contract") ? Convert.ToInt32(row["pk_contract"].ToString()) : 0;
                item.c_contract_code = !row.IsNull("c_contract_code") ? row["c_contract_code"].ToString() : "";
                item.c_contract_type = !row.IsNull("c_contract_type") ? row["c_contract_type"].ToString() : "";
                item.c_book_code = !row.IsNull("c_book_code") ? row["c_book_code"].ToString() : "";
                item.c_ds_huy = !row.IsNull("c_ds_huy") ? Convert.ToDouble(row["c_ds_huy"].ToString()) : 0;
                item.c_phan_tram_phan_bo = !row.IsNull("c_phan_tram_phan_bo") ? Convert.ToDouble(row["c_phan_tram_phan_bo"].ToString()) : 0;
                item.c_year = !row.IsNull("c_year") ? Convert.ToInt32(row["c_year"].ToString()) : 0;
                item.c_week = !row.IsNull("c_week") ? Convert.ToInt32(row["c_week"].ToString()) : 0;
                item.c_month = !row.IsNull("c_month") ? Convert.ToInt32(row["c_month"].ToString()) : 0;
                item.c_customer_code = !row.IsNull("c_book_code") ? row["c_book_code"].ToString() : "";
                item.c_customer_name = !row.IsNull("c_customer_name") ? row["c_customer_name"].ToString() : "";
                item.c_ngay_ve_hai_chieu = !row.IsNull("c_ngay_ve_hai_chieu") ? DateTime.Parse(row["c_ngay_ve_hai_chieu"].ToString()) : DateTime.Parse("01/01/1900");
                item.c_min_ngay_len_thuc_te = !row.IsNull("c_min_ngay_len_thuc_te") ? DateTime.Parse(row["c_min_ngay_len_thuc_te"].ToString()) : DateTime.Parse("01/01/1900");
                item.c_max_ngay_len_xuong_thuc_te = !row.IsNull("c_max_ngay_len_xuong_thuc_te") ? DateTime.Parse(row["c_max_ngay_len_xuong_thuc_te"].ToString()) : DateTime.Parse("01/01/1900");
                item.c_het_han = !row.IsNull("c_het_han") ? Convert.ToInt32(row["c_het_han"].ToString()) : 0;
                item.c_het_gia_tri = !row.IsNull("c_het_gia_tri") ? Convert.ToInt32(row["c_het_gia_tri"].ToString()) : 0;
                item.c_teamname = !row.IsNull("c_teamname") ? row["c_teamname"].ToString() : "";
                item.c_labels = !row.IsNull("c_labels") ? row["c_labels"].ToString() : "";
                item.c_regionid = !row.IsNull("c_regionid") ? Convert.ToInt32(row["c_regionid"].ToString()) : 0;
                item.c_teamid = !row.IsNull("c_teamid") ? Convert.ToInt32(row["c_teamid"].ToString()) : 0;
                item.c_staffid = !row.IsNull("c_staffid") ? Convert.ToInt32(row["c_staffid"].ToString()) : 0;
                item.c_staff_name = !row.IsNull("c_staff_name") ? row["c_staff_name"].ToString() : "";
                item.c_total_money = !row.IsNull("c_total_money") ? Convert.ToDouble(row["c_total_money"].ToString()) : 0;
                item.c_total_vat = !row.IsNull("c_total_vat") ? Convert.ToDouble(row["c_total_vat"].ToString()) : 0;
                item.c_total_value = !row.IsNull("c_total_value") ? Convert.ToDouble(row["c_total_value"].ToString()) : 0;
                item.c_total_money_in_year = !row.IsNull("c_total_money_in_year") ? Convert.ToDouble(row["c_total_money_in_year"].ToString()) : 0;
                item.c_total_money_next_year = !row.IsNull("c_total_money_next_year") ? Convert.ToDouble(row["c_total_money_next_year"].ToString()) : 0;
                item.c_tien_khong_tinh = !row.IsNull("c_tien_khong_tinh") ? Convert.ToDouble(row["c_tien_khong_tinh"].ToString()) : 0;
                item.c_get_money_next_week = !row.IsNull("c_get_money_next_week") ? Convert.ToDouble(row["c_get_money_next_week"].ToString()) : 0;
                item.c_commission = !row.IsNull("c_commission") ? Convert.ToDouble(row["c_commission"].ToString()) : 0;
                item.la_dai_ly = !row.IsNull("la_dai_ly") ? row["la_dai_ly"].ToString() : "";
                item.dang_quang_cao = !row.IsNull("dang_quang_cao") ? row["dang_quang_cao"].ToString() : "";
                item.c_status = !row.IsNull("c_status") ? row["c_status"].ToString() : "";
                item.c_status_name = !row.IsNull("c_status_name") ? row["c_status_name"].ToString() : "";
                item.c_dt_da_qc_den_hom_nay = !row.IsNull("c_dt_da_qc_den_hom_nay") ? Convert.ToDouble(row["c_dt_da_qc_den_hom_nay"].ToString()) : 0;
                item.c_dt_da_xuat_ban = !row.IsNull("c_dt_da_xuat_ban") ? Convert.ToDouble(row["c_dt_da_xuat_ban"].ToString()) : 0;
                item.c_payment_money = !row.IsNull("c_payment_money") ? Convert.ToDouble(row["c_payment_money"].ToString()) : 0;
                item.c_balance = !row.IsNull("c_balance") ? Convert.ToDouble(row["c_balance"].ToString()) : 0;
                item.tien_xuat_hoa_don = !row.IsNull("tien_xuat_hoa_don") ? Convert.ToDouble(row["tien_xuat_hoa_don"].ToString()) : 0;
                item.tien_chua_xuat_hoa_don = !row.IsNull("tien_chua_xuat_hoa_don") ? Convert.ToDouble(row["tien_chua_xuat_hoa_don"].ToString()) : 0;

                lst.Add(item);
            }

            return lst;

        }
        public static object Save(CRM_Contract item, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (item != null)
                {
                    try
                    {

                        var checkExits = dbConn.SingleOrDefault<CRM_Contract>("pk_contract= {0}", item.pk_contract);
                        if (checkExits == null)
                        {
                            
                            if (!string.IsNullOrEmpty(item.c_book_code))
                            {
                                var checkbook = dbConn.Select<CRM_Book_PR>(s =>s.Code == item.c_book_code && s.ContractCode!="" && s.ContractCode!=item.c_code);
                                if(checkbook.Count > 0)
                                {
                                    return new { success = false, message = "Mã book này đã được book cho hợp đồng: " + checkbook.SingleOrDefault().ContractCode + "" };
                                }
                                else
                                {
                                    var checkbook2 = dbConn.Select<CRM_Book_PR>(s => s.Code == item.c_book_code).FirstOrDefault();
                                    if(checkbook2 != null)
                                    {
                                        checkbook2.ContractCode = item.c_code;
                                        dbConn.Update(checkbook2);
                                    }    
                                }
                            }
                            var row = new CRM_Contract();
                            CultureInfo cul = CultureInfo.CurrentCulture;
                            row.c_internal_code = !string.IsNullOrEmpty(item.c_internal_code) ? item.c_internal_code.Trim() : "";
                            row.c_customer_code = !string.IsNullOrEmpty(item.c_customer_code) ? item.c_customer_code.Trim() : "";
                            row.c_code = !string.IsNullOrEmpty(item.c_code) ? item.c_code.Trim() : "";
                            row.c_name = !string.IsNullOrEmpty(item.c_name) ? item.c_name.Trim() : "";
                            row.c_agency_code = !string.IsNullOrEmpty(item.c_agency_code) ? item.c_agency_code.Trim() : "";
                            row.c_contract_type = !string.IsNullOrEmpty(item.c_contract_type) ? item.c_contract_type.Trim() : "";
                            row.c_begin_date = !string.IsNullOrEmpty(item.c_begin_date.ToString()) ? item.c_begin_date : DateTime.Parse("1900-01-01");
                            row.c_end_date = !string.IsNullOrEmpty(item.StrEndDate) ? DateTime.Parse(item.StrEndDate) : DateTime.Parse("1900-01-01");
                            row.c_week = cul.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

                            row.c_update = !string.IsNullOrEmpty(item.c_update.ToString()) ? item.c_update : DateTime.Parse("1900-01-01");
                            row.c_down_date = !string.IsNullOrEmpty(item.c_down_date.ToString()) ? item.c_down_date : DateTime.Parse("1900-01-01");
                            row.c_real_update = !string.IsNullOrEmpty(item.c_real_update.ToString()) ? item.c_real_update : DateTime.Parse("1900-01-01");
                            row.c_real_downdate = !string.IsNullOrEmpty(item.c_real_downdate.ToString()) ? item.c_real_downdate : DateTime.Parse("1900-01-01");
                            row.c_opportunity_code = !string.IsNullOrEmpty(item.c_opportunity_code) ? item.c_opportunity_code.Trim() : "";
                            row.c_marketing_code = !string.IsNullOrEmpty(item.c_marketing_code) ? item.c_marketing_code.Trim() : "";
                            row.c_promotion_code = !string.IsNullOrEmpty(item.c_promotion_code) ? item.c_promotion_code.Trim() : "";
                            row.c_check_vat = (item.c_check_vat != 0) ? item.c_check_vat : 0;

                            row.c_total_money = (convertNumber.currencyToNumber(item.StrTotalMoney) != 0) ? convertNumber.currencyToNumber(item.StrTotalMoney) : 0;
                            row.c_total_vat = (convertNumber.currencyToNumber(item.StrTotalVat) != 0) ? convertNumber.currencyToNumber(item.StrTotalVat) : 0;
                            row.c_total_value = (convertNumber.currencyToNumber(item.StrTotalValue) != 0) ? convertNumber.currencyToNumber(item.StrTotalValue) : 0;
                            row.c_total_money_in_year = (convertNumber.currencyToNumber(item.StrTotalMoney) != 0) ? convertNumber.currencyToNumber(item.StrTotalMoney) : 0;
                            row.c_total_money_next_year = (convertNumber.currencyToNumber(item.StrTotalNextYear) != 0) ? convertNumber.currencyToNumber(item.StrTotalNextYear) : 0;
                            row.c_customer_total_money = (convertNumber.currencyToNumber(item.StrTotalCustomerTotalMoney) != 0) ? convertNumber.currencyToNumber(item.StrTotalCustomerTotalMoney) : 0;
                            row.c_outside_money = (convertNumber.currencyToNumber(item.StrOutsideMoney) != 0) ? convertNumber.currencyToNumber(item.StrOutsideMoney) : 0;
                            row.c_guarantee_money = (item.c_guarantee_money != 0) ? item.c_guarantee_money : 0;
                            row.c_24h_total_money = (item.c_24h_total_money != 0) ? item.c_24h_total_money : 0;
                            row.c_commission = (item.c_commission != 0) ? item.c_commission : 0;


                            row.c_debt_status = !string.IsNullOrEmpty(item.c_debt_status) ? item.c_debt_status.Trim() : "";
                            row.c_balance = (item.c_balance != 0) ? item.c_balance : 0;
                            row.c_unit_id = (item.c_unit_id != 0) ? item.c_unit_id : 0;
                            row.c_category_code = !string.IsNullOrEmpty(item.c_category_code) ? item.c_category_code.Trim() : "";
                            row.c_note = !string.IsNullOrEmpty(item.c_note) ? item.c_note.Trim() : "";
                            row.c_website_status = !string.IsNullOrEmpty(item.c_website_status) ? item.c_website_status.Trim() : "";
                            row.c_ascii_name = !string.IsNullOrEmpty(item.c_ascii_name) ? item.c_ascii_name.Trim() : "";
                            row.c_upper_name = !string.IsNullOrEmpty(item.c_upper_name) ? item.c_upper_name.Trim() : "";
                            row.c_staff_id = (item.c_staff_id != 0) ? item.c_staff_id : 0;
                            row.c_status = !string.IsNullOrEmpty(item.c_status) ? item.c_status.Trim() : "";
                            row.c_file_name = !string.IsNullOrEmpty(item.c_file_name) ? item.c_file_name.Trim() : "";
                            row.c_area_id = (item.c_area_id != 0) ? item.c_area_id : 0;
                            row.c_str_update = !string.IsNullOrEmpty(item.c_str_update) ? item.c_str_update.Trim() : "";
                            row.c_satisfaction_status = !string.IsNullOrEmpty(item.c_satisfaction_status) ? item.c_satisfaction_status.Trim() : "";
                            row.c_xml_data = !string.IsNullOrEmpty(item.c_xml_data) ? item.c_xml_data.Trim() : "";

                            row.c_revenue_date = !string.IsNullOrEmpty(item.StrRevenueDate) ? DateTime.Parse(item.StrRevenueDate) : DateTime.Parse("1900-01-01");
                            row.c_ngay_huy_hd = !string.IsNullOrEmpty(item.StrRevenueDateCancel) ? DateTime.Parse(item.StrRevenueDateCancel) : DateTime.Parse("1900-01-01");
                            row.c_guarantee_date = !string.IsNullOrEmpty(item.c_guarantee_date.ToString()) ? item.c_guarantee_date : DateTime.Parse("1900-01-01");
                            row.c_guarantee_staff = (item.c_guarantee_staff != 0) ? item.c_guarantee_staff : 0;
                            row.c_website = !string.IsNullOrEmpty(item.c_website) ? item.c_website.Trim() : "";
                            row.c_contract_number = (item.c_contract_number != 0) ? item.c_contract_number : 0;
                            row.c_str_real_update = !string.IsNullOrEmpty(item.c_str_real_update) ? item.c_str_real_update.Trim() : "";
                            row.c_location = !string.IsNullOrEmpty(item.c_location) ? item.c_location.Trim() : "";
                            row.c_labels = !string.IsNullOrEmpty(item.c_labels) ? item.c_labels.Trim() : "";
                            row.c_get_money_next_week = (item.c_get_money_next_week != 0) ? item.c_get_money_next_week : 0;
                            row.c_ageny_check = (item.c_ageny_check != 0) ? item.c_ageny_check : 0;

                            row.c_agency_type = !string.IsNullOrEmpty(item.c_agency_type) ? item.c_agency_type.Trim() : "";
                            row.c_dt_ngoai_chinh_sach = (convertNumber.currencyToNumber(item.StrDSOutCS) != 0) ? convertNumber.currencyToNumber(item.StrDSOutCS) : 0;
                            row.c_phan_tram_ncs = (item.c_phan_tram_ncs != 0) ? item.c_phan_tram_ncs : 0;
                            row.c_24h_singer = (item.c_24h_singer != 0) ? item.c_24h_singer : 0;
                            row.c_customer_singer = !string.IsNullOrEmpty(item.c_customer_singer) ? item.c_customer_singer.Trim() : "";
                            row.c_mar_money_in_year = (item.c_mar_money_in_year != 0) ? item.c_mar_money_in_year : 0;
                            row.c_mar_money_next_year = (item.c_mar_money_next_year != 0) ? item.c_mar_money_next_year : 0;
                            row.c_mar_money_other = (item.c_mar_money_other != 0) ? item.c_mar_money_other : 0;
                            row.c_customer_acc = !string.IsNullOrEmpty(item.c_customer_acc) ? item.c_customer_acc.Trim() : "";
                            row.c_check_mkt = (item.c_check_mkt != 0) ? item.c_check_mkt : 0;

                            row.c_note_mkt = !string.IsNullOrEmpty(item.c_note_mkt) ? item.c_note_mkt.Trim() : "";
                            row.c_banner_up = (item.c_banner_up != 0) ? item.c_banner_up : 0;
                            row.c_input_date = !string.IsNullOrEmpty(item.c_input_date.ToString()) ? item.c_input_date : DateTime.Parse("1900-01-01");
                            row.c_book_code = !string.IsNullOrEmpty(item.c_book_code) ? item.c_book_code.Trim() : "";
                            row.c_cancel_up = (item.c_cancel_up != 0) ? item.c_cancel_up : 0;
                            row.c_arrears_money = (item.c_arrears_money != 0) ? item.c_arrears_money : 0;
                            row.c_ngay_bao_huy = !string.IsNullOrEmpty(item.StrDateHuy) ? DateTime.Parse(item.StrDateHuy) : DateTime.Parse("1900-01-01");
                            row.c_ds_huy = (convertNumber.currencyToNumber(item.StrDSHuy) != 0) ? convertNumber.currencyToNumber(item.StrDSHuy) : 0;
                            row.c_ly_do_huy = !string.IsNullOrEmpty(item.c_ly_do_huy) ? item.c_ly_do_huy.Trim() : "";
                            row.c_year = cul.Calendar.GetYear(DateTime.Now);
                            row.c_month = cul.Calendar.GetMonth(DateTime.Now);
                            row.c_huy_status = (item.c_huy_status != 0) ? item.c_huy_status : 0;
                            row.IsNew = 1;
                            row.RowCreatedUser = username;
                            row.RowUpdatedUser = "";
                            row.RowCreatedAt = DateTime.Now;
                            row.RowUpdatedAt = DateTime.Parse("1900-01-01");

                            dbConn.Insert(row);
                            long id = dbConn.GetLastInsertId();
                            
                            return new { success = true, message = "Save success!", PK = id };
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(item.c_book_code))
                            {
                                var checkbook = dbConn.Select<CRM_Book_PR>(s => s.Code == item.c_book_code && s.ContractCode != "" && s.ContractCode != item.c_code);
                                if (checkbook.Count > 0)
                                {
                                    return new { success = false, message = "Mã book này đã được book cho hợp đồng: " + checkbook.SingleOrDefault().ContractCode + "" };
                                }
                                else
                                {
                                    var checkbook2 = dbConn.Select<CRM_Book_PR>(s => s.Code == item.c_book_code).FirstOrDefault();
                                    if (checkbook2 != null)
                                    {
                                        checkbook2.ContractCode = item.c_code;
                                        dbConn.Update(checkbook2);
                                    }
                                }
                            }
                            checkExits.c_internal_code = !string.IsNullOrEmpty(item.c_internal_code) ? item.c_internal_code.Trim() : "";
                            checkExits.c_customer_code = !string.IsNullOrEmpty(item.c_customer_code) ? item.c_customer_code.Trim() : "";
                            checkExits.c_code = !string.IsNullOrEmpty(item.c_code) ? item.c_code.Trim() : "";
                            checkExits.c_name = !string.IsNullOrEmpty(item.c_name) ? item.c_name.Trim() : "";
                            checkExits.c_agency_code = !string.IsNullOrEmpty(item.c_agency_code) ? item.c_agency_code.Trim() : "";
                            checkExits.c_contract_type = !string.IsNullOrEmpty(item.c_contract_type) ? item.c_contract_type.Trim() : "";
                            checkExits.c_begin_date = !string.IsNullOrEmpty(item.c_begin_date.ToString()) ? item.c_begin_date : DateTime.Parse("1900-01-01");
                            checkExits.c_end_date = !string.IsNullOrEmpty(item.StrEndDate) ? DateTime.Parse(item.StrEndDate) : checkExits.c_end_date;
                            //checkExits.c_week = (item.c_week != 0) ? item.c_week : 0;
                            checkExits.c_update = !string.IsNullOrEmpty(item.c_update.ToString()) ? item.c_update : DateTime.Parse("1900-01-01");
                            checkExits.c_down_date = !string.IsNullOrEmpty(item.c_down_date.ToString()) ? item.c_down_date : DateTime.Parse("1900-01-01");
                            checkExits.c_real_update = !string.IsNullOrEmpty(item.c_real_update.ToString()) ? item.c_real_update : DateTime.Parse("1900-01-01");
                            checkExits.c_real_downdate = !string.IsNullOrEmpty(item.c_real_downdate.ToString()) ? item.c_real_downdate : DateTime.Parse("1900-01-01");
                            checkExits.c_opportunity_code = !string.IsNullOrEmpty(item.c_opportunity_code) ? item.c_opportunity_code.Trim() : "";
                            checkExits.c_marketing_code = !string.IsNullOrEmpty(item.c_marketing_code) ? item.c_marketing_code.Trim() : "";
                            checkExits.c_promotion_code = !string.IsNullOrEmpty(item.c_promotion_code) ? item.c_promotion_code.Trim() : "";
                            checkExits.c_check_vat = (item.c_check_vat != 0) ? item.c_check_vat : 0;
                            checkExits.c_total_money = (convertNumber.currencyToNumber(item.StrTotalMoney) != 0) ? convertNumber.currencyToNumber(item.StrTotalMoney) : 0;
                            checkExits.c_total_vat = (convertNumber.currencyToNumber(item.StrTotalVat) != 0) ? convertNumber.currencyToNumber(item.StrTotalVat) : 0;
                            checkExits.c_total_value = (convertNumber.currencyToNumber(item.StrTotalValue) != 0) ? convertNumber.currencyToNumber(item.StrTotalValue) : 0;
                            checkExits.c_total_money_in_year = (convertNumber.currencyToNumber(item.StrTotalMoney) != 0) ? convertNumber.currencyToNumber(item.StrTotalMoney) : 0;
                            checkExits.c_total_money_next_year = (convertNumber.currencyToNumber(item.StrTotalNextYear) != 0) ? convertNumber.currencyToNumber(item.StrTotalNextYear) : 0;
                            checkExits.c_customer_total_money = (convertNumber.currencyToNumber(item.StrTotalCustomerTotalMoney) != 0) ? convertNumber.currencyToNumber(item.StrTotalCustomerTotalMoney) : 0;
                            checkExits.c_outside_money = (convertNumber.currencyToNumber(item.StrOutsideMoney) != 0) ? convertNumber.currencyToNumber(item.StrOutsideMoney) : 0;
                            checkExits.c_guarantee_money = (item.c_guarantee_money != 0) ? item.c_guarantee_money : 0;
                            checkExits.c_24h_total_money = (item.c_24h_total_money != 0) ? item.c_24h_total_money : 0;
                            checkExits.c_commission = (item.c_commission != 0) ? item.c_commission : 0;
                            checkExits.c_debt_status = !string.IsNullOrEmpty(item.c_debt_status) ? item.c_debt_status.Trim() : "";
                            checkExits.c_balance = (item.c_balance != 0) ? item.c_balance : 0;
                            checkExits.c_unit_id = (item.c_unit_id != 0) ? item.c_unit_id : 0;
                            checkExits.c_category_code = !string.IsNullOrEmpty(item.c_category_code) ? item.c_category_code.Trim() : "";
                            checkExits.c_note = !string.IsNullOrEmpty(item.c_note) ? item.c_note.Trim() : "";
                            checkExits.c_website_status = !string.IsNullOrEmpty(item.c_website_status) ? item.c_website_status.Trim() : "";
                            checkExits.c_ascii_name = !string.IsNullOrEmpty(item.c_ascii_name) ? item.c_ascii_name.Trim() : "";
                            checkExits.c_upper_name = !string.IsNullOrEmpty(item.c_upper_name) ? item.c_upper_name.Trim() : "";
                            checkExits.c_staff_id = (item.c_staff_id != 0) ? item.c_staff_id : 0;
                            checkExits.c_status = !string.IsNullOrEmpty(item.c_status) ? item.c_status.Trim() : "";
                            checkExits.c_file_name = !string.IsNullOrEmpty(item.c_file_name) ? item.c_file_name.Trim() : "";
                            checkExits.c_area_id = (item.c_area_id != 0) ? item.c_area_id : 0;
                            checkExits.c_str_update = !string.IsNullOrEmpty(item.c_str_update) ? item.c_str_update.Trim() : "";
                            checkExits.c_satisfaction_status = !string.IsNullOrEmpty(item.c_satisfaction_status) ? item.c_satisfaction_status.Trim() : "";
                            checkExits.c_xml_data = !string.IsNullOrEmpty(item.c_xml_data) ? item.c_xml_data.Trim() : "";
                            checkExits.c_revenue_date = !string.IsNullOrEmpty(item.StrRevenueDate) ? DateTime.Parse(item.StrRevenueDate) : checkExits.c_revenue_date;
                            checkExits.c_guarantee_date = !string.IsNullOrEmpty(item.c_guarantee_date.ToString()) ? item.c_guarantee_date : DateTime.Parse("1900-01-01");
                            checkExits.c_guarantee_staff = (item.c_guarantee_staff != 0) ? item.c_guarantee_staff : 0;
                            checkExits.c_website = !string.IsNullOrEmpty(item.c_website) ? item.c_website.Trim() : "";
                            checkExits.c_contract_number = (item.c_contract_number != 0) ? item.c_contract_number : 0;
                            checkExits.c_str_real_update = !string.IsNullOrEmpty(item.c_str_real_update) ? item.c_str_real_update.Trim() : "";
                            checkExits.c_location = !string.IsNullOrEmpty(item.c_location) ? item.c_location.Trim() : "";
                            checkExits.c_labels = !string.IsNullOrEmpty(item.c_labels) ? item.c_labels.Trim() : "";
                            checkExits.c_get_money_next_week = (item.c_get_money_next_week != 0) ? item.c_get_money_next_week : 0;
                            checkExits.c_ageny_check = (item.c_ageny_check != 0) ? item.c_ageny_check : 0;
                            checkExits.c_agency_type = !string.IsNullOrEmpty(item.c_agency_type) ? item.c_agency_type.Trim() : "";
                            checkExits.c_dt_ngoai_chinh_sach = (convertNumber.currencyToNumber(item.StrDSOutCS) != 0) ? convertNumber.currencyToNumber(item.StrDSOutCS) : 0;
                            checkExits.c_phan_tram_ncs = (item.c_phan_tram_ncs != 0) ? item.c_phan_tram_ncs : 0;
                            checkExits.c_24h_singer = (item.c_24h_singer != 0) ? item.c_24h_singer : 0;
                            checkExits.c_customer_singer = !string.IsNullOrEmpty(item.c_customer_singer) ? item.c_customer_singer.Trim() : "";
                            checkExits.c_mar_money_in_year = (item.c_mar_money_in_year != 0) ? item.c_mar_money_in_year : 0;
                            checkExits.c_mar_money_next_year = (item.c_mar_money_next_year != 0) ? item.c_mar_money_next_year : 0;
                            checkExits.c_mar_money_other = (item.c_mar_money_other != 0) ? item.c_mar_money_other : 0;
                            checkExits.c_customer_acc = !string.IsNullOrEmpty(item.c_customer_acc) ? item.c_customer_acc.Trim() : "";
                            checkExits.c_check_mkt = (item.c_check_mkt != 0) ? item.c_check_mkt : 0;
                            checkExits.c_note_mkt = !string.IsNullOrEmpty(item.c_note_mkt) ? item.c_note_mkt.Trim() : "";
                            checkExits.c_banner_up = (item.c_banner_up != 0) ? item.c_banner_up : 0;
                            checkExits.c_input_date = !string.IsNullOrEmpty(item.c_input_date.ToString()) ? item.c_input_date : DateTime.Parse("1900-01-01");
                            checkExits.c_book_code = !string.IsNullOrEmpty(item.c_book_code) ? item.c_book_code.Trim() : "";
                            checkExits.c_cancel_up = (item.c_cancel_up != 0) ? item.c_cancel_up : 0;
                            checkExits.c_arrears_money = (item.c_arrears_money != 0) ? item.c_arrears_money : 0;
                            checkExits.c_ngay_huy_hd = !string.IsNullOrEmpty(item.StrRevenueDateCancel) ? DateTime.Parse(item.StrRevenueDateCancel) : DateTime.Parse("1900-01-01");
                            checkExits.c_ngay_bao_huy = !string.IsNullOrEmpty(item.StrDateHuy) ? DateTime.Parse(item.StrDateHuy) : DateTime.Parse("1900-01-01");
                            checkExits.c_ds_huy = (convertNumber.currencyToNumber(item.StrDSHuy) != 0) ? convertNumber.currencyToNumber(item.StrDSHuy) : 0;
                            checkExits.c_ly_do_huy = !string.IsNullOrEmpty(item.c_ly_do_huy) ? item.c_ly_do_huy.Trim() : "";
                            //checkExits.c_year = (item.c_year != 0) ? item.c_year : 0;
                            //checkExits.c_month = (item.c_month != 0) ? item.c_month : 0;
                            checkExits.c_huy_status = (item.c_huy_status != 0) ? item.c_huy_status : 0;
                            checkExits.RowUpdatedUser = username;
                            checkExits.RowUpdatedAt = DateTime.Now;
                            dbConn.Update(checkExits);
                            return new { success = true, message = "Save success!" };
                        }
                    }
                    catch (Exception e)
                    {
                        return new { success = false, message = e.Message };
                    }
                }
                else
                {
                    return new { success = false, message = "Data is null!" };
                }

            }


        }
        public static long Sync(CRM_Contract item, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (item != null)
                {
                    try
                    {
                        var row = new CRM_Contract();
                        row.c_internal_code = !string.IsNullOrEmpty(item.c_internal_code) ? item.c_internal_code.Trim() : "";
                        row.c_customer_code = !string.IsNullOrEmpty(item.c_customer_code) ? item.c_customer_code.Trim() : "";
                        row.c_code = !string.IsNullOrEmpty(item.c_code) ? item.c_code.Trim() : "";
                        row.c_name = !string.IsNullOrEmpty(item.c_name) ? item.c_name.Trim() : "";
                        row.c_agency_code = !string.IsNullOrEmpty(item.c_agency_code) ? item.c_agency_code.Trim() : "";
                        row.c_contract_type = !string.IsNullOrEmpty(item.c_contract_type) ? item.c_contract_type.Trim() : "";
                        row.c_begin_date = !string.IsNullOrEmpty(item.c_begin_date.ToString()) ? item.c_begin_date : DateTime.Parse("1900-01-01");
                        row.c_end_date = !string.IsNullOrEmpty(item.c_end_date.ToString()) ? item.c_end_date : DateTime.Parse("1900-01-01");
                        row.c_week = (item.c_week != 0) ? item.c_week : 0;
                        row.c_update = !string.IsNullOrEmpty(item.c_update.ToString()) ? item.c_update : DateTime.Parse("1900-01-01");
                        row.c_down_date = !string.IsNullOrEmpty(item.c_down_date.ToString()) ? item.c_down_date : DateTime.Parse("1900-01-01");
                        row.c_real_update = !string.IsNullOrEmpty(item.c_real_update.ToString()) ? item.c_real_update : DateTime.Parse("1900-01-01");
                        row.c_real_downdate = !string.IsNullOrEmpty(item.c_real_downdate.ToString()) ? item.c_real_downdate : DateTime.Parse("1900-01-01");
                        row.c_opportunity_code = !string.IsNullOrEmpty(item.c_opportunity_code) ? item.c_opportunity_code.Trim() : "";
                        row.c_marketing_code = !string.IsNullOrEmpty(item.c_marketing_code) ? item.c_marketing_code.Trim() : "";
                        row.c_promotion_code = !string.IsNullOrEmpty(item.c_promotion_code) ? item.c_promotion_code.Trim() : "";
                        row.c_check_vat = (item.c_check_vat != 0) ? item.c_check_vat : 0;

                        row.c_total_money = (item.c_total_money != 0) ? item.c_total_money : 0;
                        row.c_total_vat = (item.c_total_vat != 0) ? item.c_total_vat : 0;
                        row.c_total_value = (item.c_total_value != 0) ? item.c_total_value : 0;
                        row.c_total_money_in_year = (item.c_total_money_in_year != 0) ? item.c_total_money_in_year : 0;
                        row.c_total_money_next_year = (item.c_total_money_next_year != 0) ? item.c_total_money_next_year : 0;
                        row.c_customer_total_money = (item.c_customer_total_money != 0) ? item.c_customer_total_money : 0;
                        row.c_outside_money = (item.c_outside_money != 0) ? item.c_outside_money : 0;
                        row.c_guarantee_money = (item.c_guarantee_money != 0) ? item.c_guarantee_money : 0;
                        row.c_24h_total_money = (item.c_24h_total_money != 0) ? item.c_24h_total_money : 0;
                        row.c_commission = (item.c_commission != 0) ? item.c_commission : 0;


                        row.c_debt_status = !string.IsNullOrEmpty(item.c_debt_status) ? item.c_debt_status.Trim() : "";
                        row.c_balance = (item.c_balance != 0) ? item.c_balance : 0;
                        row.c_unit_id = (item.c_unit_id != 0) ? item.c_unit_id : 0;
                        row.c_category_code = !string.IsNullOrEmpty(item.c_category_code) ? item.c_category_code.Trim() : "";
                        row.c_note = !string.IsNullOrEmpty(item.c_note) ? item.c_note.Trim() : "";
                        row.c_website_status = !string.IsNullOrEmpty(item.c_website_status) ? item.c_website_status.Trim() : "";
                        row.c_ascii_name = !string.IsNullOrEmpty(item.c_ascii_name) ? item.c_ascii_name.Trim() : "";
                        row.c_upper_name = !string.IsNullOrEmpty(item.c_upper_name) ? item.c_upper_name.Trim() : "";
                        row.c_staff_id = (item.c_staff_id != 0) ? item.c_staff_id : 0;
                        row.c_status = !string.IsNullOrEmpty(item.c_status) ? item.c_status.Trim() : "";
                        row.c_file_name = !string.IsNullOrEmpty(item.c_file_name) ? item.c_file_name.Trim() : "";
                        row.c_area_id = (item.c_area_id != 0) ? item.c_area_id : 0;
                        row.c_str_update = !string.IsNullOrEmpty(item.c_str_update) ? item.c_str_update.Trim() : "";
                        row.c_satisfaction_status = !string.IsNullOrEmpty(item.c_satisfaction_status) ? item.c_satisfaction_status.Trim() : "";
                        row.c_xml_data = !string.IsNullOrEmpty(item.c_xml_data) ? item.c_xml_data.Trim() : "";

                        row.c_revenue_date = !string.IsNullOrEmpty(item.c_revenue_date.ToString()) ? item.c_revenue_date : DateTime.Parse("1900-01-01");
                        row.c_guarantee_date = !string.IsNullOrEmpty(item.c_guarantee_date.ToString()) ? item.c_guarantee_date : DateTime.Parse("1900-01-01");
                        row.c_guarantee_staff = (item.c_guarantee_staff != 0) ? item.c_guarantee_staff : 0;
                        row.c_website = !string.IsNullOrEmpty(item.c_website) ? item.c_website.Trim() : "";
                        row.c_contract_number = (item.c_contract_number != 0) ? item.c_contract_number : 0;
                        row.c_str_real_update = !string.IsNullOrEmpty(item.c_str_real_update) ? item.c_str_real_update.Trim() : "";
                        row.c_location = !string.IsNullOrEmpty(item.c_location) ? item.c_location.Trim() : "";
                        row.c_labels = !string.IsNullOrEmpty(item.c_labels) ? item.c_labels.Trim() : "";
                        row.c_get_money_next_week = (item.c_get_money_next_week != 0) ? item.c_get_money_next_week : 0;
                        row.c_ageny_check = (item.c_ageny_check != 0) ? item.c_ageny_check : 0;

                        row.c_agency_type = !string.IsNullOrEmpty(item.c_agency_type) ? item.c_agency_type.Trim() : "";
                        row.c_dt_ngoai_chinh_sach = (item.c_dt_ngoai_chinh_sach != 0) ? item.c_dt_ngoai_chinh_sach : 0;
                        row.c_phan_tram_ncs = (item.c_phan_tram_ncs != 0) ? item.c_phan_tram_ncs : 0;
                        row.c_24h_singer = (item.c_24h_singer != 0) ? item.c_24h_singer : 0;
                        row.c_customer_singer = !string.IsNullOrEmpty(item.c_customer_singer) ? item.c_customer_singer.Trim() : "";
                        row.c_mar_money_in_year = (item.c_mar_money_in_year != 0) ? item.c_mar_money_in_year : 0;
                        row.c_mar_money_next_year = (item.c_mar_money_next_year != 0) ? item.c_mar_money_next_year : 0;
                        row.c_mar_money_other = (item.c_mar_money_other != 0) ? item.c_mar_money_other : 0;
                        row.c_customer_acc = !string.IsNullOrEmpty(item.c_customer_acc) ? item.c_customer_acc.Trim() : "";
                        row.c_check_mkt = (item.c_check_mkt != 0) ? item.c_check_mkt : 0;

                        row.c_note_mkt = !string.IsNullOrEmpty(item.c_note_mkt) ? item.c_note_mkt.Trim() : "";
                        row.c_banner_up = (item.c_banner_up != 0) ? item.c_banner_up : 0;
                        row.c_input_date = !string.IsNullOrEmpty(item.c_input_date.ToString()) ? item.c_input_date : DateTime.Parse("1900-01-01");
                        row.c_book_code = !string.IsNullOrEmpty(item.c_book_code) ? item.c_book_code.Trim() : "";
                        row.c_cancel_up = (item.c_cancel_up != 0) ? item.c_cancel_up : 0;
                        row.c_arrears_money = (item.c_arrears_money != 0) ? item.c_arrears_money : 0;
                        row.c_ngay_huy_hd = !string.IsNullOrEmpty(item.c_ngay_huy_hd.ToString()) ? item.c_ngay_huy_hd : DateTime.Parse("1900-01-01");
                        row.c_ngay_bao_huy = !string.IsNullOrEmpty(item.c_ngay_bao_huy.ToString()) ? item.c_ngay_bao_huy : DateTime.Parse("1900-01-01");
                        row.c_ds_huy = (item.c_ds_huy != 0) ? item.c_ds_huy : 0;
                        row.c_ly_do_huy = !string.IsNullOrEmpty(item.c_ly_do_huy) ? item.c_ly_do_huy.Trim() : "";
                        row.c_year = (item.c_year != 0) ? item.c_year : 0;
                        row.c_month = (item.c_month != 0) ? item.c_month : 0;
                        row.c_huy_status = (item.c_huy_status != 0) ? item.c_huy_status : 0;
                        row.IsNew = 1;

                        row.RowCreatedUser = username;
                        row.RowUpdatedUser = "";
                        row.RowCreatedAt = DateTime.Now;
                        row.RowUpdatedAt = DateTime.Parse("1900-01-01");

                        dbConn.Insert(row);
                        return dbConn.GetLastInsertId();


                    }
                    catch (Exception e)
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }

            }


        }
        public List<CRM_Contract> Export_BangKe(string code, DateTime dateUp, DateTime dateDown)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@customercode", code));
            param.Add(new SqlParameter("@ngay_len_hd", dateUp));
            param.Add(new SqlParameter("@ngay_xuong_hd", dateDown));
            DataTable dt = new SqlHelper().ExecuteQuery("p_ERPAPD_Export_BangKe", param);
            var lst = new List<CRM_Contract>();
            foreach (DataRow row in dt.Rows)
            {
                var item = new CRM_Contract();
                item.pk_contract = !row.IsNull("fk_contract") ? Convert.ToInt32(row["fk_contract"].ToString()) : 0;
                item.c_customer_code = !row.IsNull("c_customer_code") ? row["c_customer_code"].ToString() : "";
                item.c_customer_name = !row.IsNull("c_customer_name") ? row["c_customer_name"].ToString() : "";
                item.c_labels = !row.IsNull("c_label") ? row["c_label"].ToString() : "";

                item.c_ngay_len = !row.IsNull("c_ngay_len") ? DateTime.Parse(row["c_ngay_len"].ToString()) : DateTime.Parse("01/01/1900");
                item.c_ngay_xuong = !row.IsNull("c_ngay_xuong") ? DateTime.Parse(row["c_ngay_xuong"].ToString()) : DateTime.Parse("01/01/1900");
                item.c_so_ngay = !row.IsNull("c_so_ngay") ? Convert.ToInt32(row["c_so_ngay"].ToString()) : 0;
                item.c_website_name = !row.IsNull("c_website_name") ? row["c_website_name"].ToString() : "";
                item.c_category_name = !row.IsNull("c_category_name") ? row["c_category_name"].ToString() : "";
                item.c_location_name = !row.IsNull("c_location_name") ? row["c_location_name"].ToString() : "";

                item.c_don_gia_public = !row.IsNull("c_don_gia_public") ? Convert.ToDouble(row["c_don_gia_public"].ToString()) : 0;
                item.c_don_gia_tt = !row.IsNull("c_don_gia_tt") ? Convert.ToDouble(row["c_don_gia_tt"].ToString()) : 0;
                item.c_tong_tien = !row.IsNull("c_tong_tien") ? Convert.ToDouble(row["c_tong_tien"].ToString()) : 0;
                item.c_number = !row.IsNull("c_number") ? Convert.ToInt32(row["c_number"].ToString()) : 0;
                item.c_don_gia_theo_ngay = !row.IsNull("c_don_gia_theo_ngay") ? Convert.ToDouble(row["c_don_gia_theo_ngay"].ToString()) : 0;
                item.c_ngay_bu = !row.IsNull("c_ngay_bu") ? Convert.ToInt32(row["c_ngay_bu"].ToString()) : 0;

                lst.Add(item);
            }
            return lst;

        }
    }
    public class CRM_Contract_Draff
    {
        [AutoIncrement]

        public Int32 PKContractDraft { get; set; }
        public string CustomerCode { get; set; }
        public string Code { get; set; }
        public string LoaiHopDong { get; set; }
        public int FKStaff { get; set; }
        public string Labels { get; set; }
        public string CategoryCode { get; set; }
        public string AgencyType { get; set; }
        public string BenA { get; set; }
        public string DaiDienA { get; set; }
        public string ChucVuA { get; set; }
        public string DiaChiA { get; set; }
        public string DienThoaiA { get; set; }
        public string FaxA { get; set; }
        public string TaiKhoanA { get; set; }
        public string NganHangA { get; set; }
        public string ChiNhanhA { get; set; }
        public string MaSoThueA { get; set; }
        public int KhachHangCaNhan { get; set; }
        public string SoCmtA { get; set; }
        public DateTime NgayCapCmtA { get; set; }
        public string NoiCapCmtA { get; set; }

        public string BenB { get; set; }
        public string DaiDienB { get; set; }
        public string ChucVuB { get; set; }
        public string DiaChiB { get; set; }
        public string DienThoaiB { get; set; }
        public string FaxB { get; set; }
        public string TaiKhoanB { get; set; }
        public string NganHangB { get; set; }
        public string ChiNhanhB { get; set; }
        public string MaSoThueB { get; set; }

        public int KhuyenMaiChung { get; set; }
        public string ChietKhauChungText1 { get; set; }
        public string ChietKhauChungText2 { get; set; }
        public string ChietKhauChungText3 { get; set; }
        public float ChietKhauChung1 { get; set; }
        public float ChietKhauChung2 { get; set; }
        public float ChietKhauChung3 { get; set; }
        public float GiamTrucTiep { get; set; }

        public string NguoiNhanMar { get; set; }
        public string BoPhanNguoiNhanMar { get; set; }
        public int DienThoaiNguoiNhanMar { get; set; }
        public string SoTkNguoiNhanMar { get; set; }
        public string c_ngan_hang_nguoi_nhan_mar { get; set; }
        public string c_chi_nhanh_nguoi_nhan_mar { get; set; }

        public float PhiMar { get; set; }
        public int LoaiDichVu { get; set; }
        public string DieuKhoan { get; set; }
        /// <summary>
        /// 0: Đang Soạn
        /// 1:Đã duyệt 
        /// 2: Chờ duyệt
        /// 10: Đã đồng bộ
        /// 100: Từ chối
        /// </summary>
        public int TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string Dieu2 { get; set; }
        public int PhuongThucThanhToan { get; set; }
        public DateTime NgayVeHaiChieu { get; set; }
        public DateTime NgayTao { get; set; }
        public int KFUnit { get; set; }
        public int KFGroup { get; set; }
        public int CheckVat { get; set; }
        public long TongTienHD { get; set; }
        public string LyDoDuyet { get; set; }
        public string LyDoGuiDuyet { get; set; }
        public string FileDuyet { get; set; }
        public string FileGuiDuyet { get; set; }

        public DateTime ThoiGianGuiDuyet { get; set; }
        public DateTime ThoiGianDuyet { get; set; }
        public int NguoiDuyet { get; set; }
        public string BookCode { get; set; }
        public string PhuLuc1 { get; set; }
        public string PhuLuc2 { get; set; }
        public string PhuLuc3 { get; set; }
        public string PhuLuc4 { get; set; }
        public string PhuLuc5 { get; set; }

        public string CustomerID { get; set; }
        public int IsNew { get; set; }

        public int NguoiDongBo { get; set; }

        public DateTime RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        [Ignore]
        public string ActionSelected { get; set; }

        [Ignore]
        public string ShortName { get; set; }

        [Ignore]
        public string ContractTypeName { get; set; }
        [Ignore]
        public string StatusName { get; set; }

        [Ignore]
        public double TotalMoney { get; set; }
        [Ignore]
        public double TotalMoneyInYear { get; set; }
        [Ignore]
        public string CustomerName { get; set; }
        [Ignore]
        public string CategoryID { get; set; }
        [Ignore]
        public string CustomerType { get; set; }
        [Ignore]
        public string CategoryName { get; set; }
        [Ignore]
        public string CustomerTypeName { get; set; }
        [Ignore]
        public string CategorySubID { get; set; }
        [Ignore]
        public string CategorySubName { get; set; }
        [Ignore]
        public int StatusApproved { get; set; }
        [Ignore]
        public int UserApproved { get; set; }
        [Ignore]
        public DateTime RequestDate { get; set; }
        [Ignore]
        public DateTime ApprovedDate { get; set; }
        [Ignore]
        public string UserNameApproved { get; set; }
        [Ignore]
        public string StatusNameApproved { get; set; }
        [Ignore]
        public int UserIDStaff { get; set; }
        [Ignore]
        public string UserNameStaff { get; set; }
        [Ignore]
        public int RegionID { get; set; }
        [Ignore]
        public string RegionName { get; set; }
        [Ignore]
        public int TeamID { get; set; }
        [Ignore]
        public string TeamName { get; set; }
        [Ignore]
        public string NgayVeHaiChieuString { get; set; }
        [Ignore]
        public int StatusOrderByStaus { get; set; }

        public static object Save(CRM_Contract_Draff item, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (item != null)
                {
                    try
                    {

                        var checkExits = dbConn.SingleOrDefault<CRM_Contract_Draff>("PKContractDraft= {0}", item.PKContractDraft);
                        if (checkExits == null)
                        {
                            var row = new CRM_Contract_Draff();
                            row.CustomerCode = !string.IsNullOrEmpty(item.CustomerCode) ? item.CustomerCode.Trim() : "";
                            row.Code = !string.IsNullOrEmpty(item.Code) ? item.Code.Trim() : "";
                            row.LoaiHopDong = !string.IsNullOrEmpty(item.LoaiHopDong) ? item.LoaiHopDong.Trim() : "";
                            row.FKStaff = (item.FKStaff != 0) ? item.FKStaff : 0;
                            row.Labels = !string.IsNullOrEmpty(item.Labels) ? item.Labels.Trim() : "";
                            row.CategoryCode = !string.IsNullOrEmpty(item.CategoryCode) ? item.CategoryCode.Trim() : "";
                            row.AgencyType = !string.IsNullOrEmpty(item.AgencyType) ? item.AgencyType.Trim() : "";
                            row.BenA = !string.IsNullOrEmpty(item.BenA) ? item.BenA.Trim() : "";
                            row.DaiDienA = !string.IsNullOrEmpty(item.DaiDienA) ? item.DaiDienA.Trim() : "";
                            row.ChucVuA = !string.IsNullOrEmpty(item.ChucVuA) ? item.ChucVuA.Trim() : "";
                            row.DiaChiA = !string.IsNullOrEmpty(item.DiaChiA) ? item.DiaChiA.Trim() : "";
                            row.DienThoaiA = !string.IsNullOrEmpty(item.DienThoaiA) ? item.DienThoaiA.Trim() : "";
                            row.FaxA = !string.IsNullOrEmpty(item.FaxA) ? item.FaxA.Trim() : "";
                            row.TaiKhoanA = !string.IsNullOrEmpty(item.TaiKhoanA) ? item.TaiKhoanA.Trim() : "";
                            row.NganHangA = !string.IsNullOrEmpty(item.NganHangA) ? item.NganHangA.Trim() : "";
                            row.ChiNhanhA = !string.IsNullOrEmpty(item.ChiNhanhA) ? item.ChiNhanhA.Trim() : "";
                            row.MaSoThueA = !string.IsNullOrEmpty(item.MaSoThueA) ? item.MaSoThueA.Trim() : "";
                            row.KhachHangCaNhan = (item.KhachHangCaNhan != 0) ? item.KhachHangCaNhan : 0;
                            row.SoCmtA = !string.IsNullOrEmpty(item.SoCmtA) ? item.SoCmtA.Trim() : "";
                            row.NgayCapCmtA = (!string.IsNullOrEmpty(item.NgayCapCmtA.ToString()) && !item.NgayCapCmtA.ToString().Contains("/0001")) ?
                                item.NgayCapCmtA : DateTime.Parse("1900-01-01");
                            row.NoiCapCmtA = !string.IsNullOrEmpty(item.NoiCapCmtA) ? item.NoiCapCmtA.Trim() : "";

                            row.BenB = !string.IsNullOrEmpty(item.BenB) ? item.BenB.Trim() : "";
                            row.DaiDienB = !string.IsNullOrEmpty(item.DaiDienB) ? item.DaiDienB.Trim() : "";
                            row.ChucVuB = !string.IsNullOrEmpty(item.ChucVuB) ? item.ChucVuB.Trim() : "";
                            row.DiaChiB = !string.IsNullOrEmpty(item.DiaChiB) ? item.DiaChiB.Trim() : "";
                            row.DienThoaiB = !string.IsNullOrEmpty(item.DienThoaiB) ? item.DienThoaiB.Trim() : "";
                            row.FaxB = !string.IsNullOrEmpty(item.FaxB) ? item.FaxB.Trim() : "";
                            row.TaiKhoanB = !string.IsNullOrEmpty(item.TaiKhoanB) ? item.TaiKhoanB.Trim() : "";
                            row.NganHangB = !string.IsNullOrEmpty(item.NganHangB) ? item.NganHangB.Trim() : "";
                            row.ChiNhanhB = !string.IsNullOrEmpty(item.ChiNhanhB) ? item.ChiNhanhB.Trim() : "";
                            row.MaSoThueB = !string.IsNullOrEmpty(item.MaSoThueB) ? item.MaSoThueB.Trim() : "";

                            row.KhuyenMaiChung = (item.KhuyenMaiChung != 0) ? item.KhuyenMaiChung : 0;
                            row.ChietKhauChungText1 = !string.IsNullOrEmpty(item.ChietKhauChungText1) ? item.ChietKhauChungText1.Trim() : "";
                            row.ChietKhauChungText2 = !string.IsNullOrEmpty(item.ChietKhauChungText2) ? item.ChietKhauChungText2.Trim() : "";
                            row.ChietKhauChungText3 = !string.IsNullOrEmpty(item.ChietKhauChungText3) ? item.ChietKhauChungText3.Trim() : "";
                            row.ChietKhauChung1 = (item.ChietKhauChung1 != 0) ? item.ChietKhauChung1 : 0;
                            row.ChietKhauChung2 = (item.ChietKhauChung2 != 0) ? item.ChietKhauChung2 : 0;
                            row.ChietKhauChung3 = (item.ChietKhauChung3 != 0) ? item.ChietKhauChung3 : 0;
                            row.GiamTrucTiep = (item.GiamTrucTiep != 0) ? item.GiamTrucTiep : 0;

                            row.NguoiNhanMar = !string.IsNullOrEmpty(item.NguoiNhanMar) ? item.NguoiNhanMar.Trim() : "";
                            row.BoPhanNguoiNhanMar = !string.IsNullOrEmpty(item.BoPhanNguoiNhanMar) ? item.BoPhanNguoiNhanMar.Trim() : "";
                            row.DienThoaiNguoiNhanMar = (item.DienThoaiNguoiNhanMar != 0) ? item.DienThoaiNguoiNhanMar : 0;
                            row.SoTkNguoiNhanMar = !string.IsNullOrEmpty(item.SoTkNguoiNhanMar) ? item.SoTkNguoiNhanMar.Trim() : "";
                            row.c_ngan_hang_nguoi_nhan_mar = !string.IsNullOrEmpty(item.c_ngan_hang_nguoi_nhan_mar) ? item.c_ngan_hang_nguoi_nhan_mar.Trim() : "";
                            row.c_chi_nhanh_nguoi_nhan_mar = !string.IsNullOrEmpty(item.c_chi_nhanh_nguoi_nhan_mar) ? item.c_chi_nhanh_nguoi_nhan_mar.Trim() : "";

                            row.PhiMar = (item.PhiMar != 0) ? item.PhiMar : 0;
                            row.LoaiDichVu = (item.LoaiDichVu != 0) ? item.LoaiDichVu : 0;
                            if (item.DieuKhoan != null)
                            {
                                if (!item.DieuKhoan.Trim().Contains("UnicodeConverted"))
                                    row.DieuKhoan = !string.IsNullOrEmpty(item.DieuKhoan) ? item.DieuKhoan.Trim() + "<p hidden> UnicodeConverted <p>" : "";
                                else
                                    row.DieuKhoan = !string.IsNullOrEmpty(item.DieuKhoan) ? item.DieuKhoan.Trim() : "";
                            }
                            else
                            {
                                row.DieuKhoan = !string.IsNullOrEmpty(item.DieuKhoan) ? item.DieuKhoan.Trim() : "";
                            }
                            //row.DieuKhoan = !string.IsNullOrEmpty(item.DieuKhoan) ? item.DieuKhoan.Trim() : "";
                            row.TrangThai = (item.TrangThai != 0) ? item.TrangThai : 0;
                            if (item.GhiChu != null)
                            {
                                if (!item.GhiChu.Trim().Contains("UnicodeConverted"))
                                    row.GhiChu = !string.IsNullOrEmpty(item.GhiChu) ? item.GhiChu.Trim() + "<p hidden> UnicodeConverted <p>" : "";
                                else
                                    row.GhiChu = !string.IsNullOrEmpty(item.GhiChu) ? item.GhiChu.Trim() : "";
                            }
                            else
                            {
                                row.GhiChu = !string.IsNullOrEmpty(item.GhiChu) ? item.GhiChu.Trim() : "";
                            }
                            if (item.Dieu2 != null)
                            {
                                if (!item.Dieu2.Trim().Contains("UnicodeConverted"))
                                    row.Dieu2 = !string.IsNullOrEmpty(item.Dieu2) ? item.Dieu2.Trim() + "<p hidden> UnicodeConverted <p>" : "";
                                else
                                    row.Dieu2 = !string.IsNullOrEmpty(item.Dieu2) ? item.Dieu2.Trim() : "";
                            }
                            else
                            {
                                row.Dieu2 = !string.IsNullOrEmpty(item.Dieu2) ? item.Dieu2.Trim() : "";
                            }

                            //row.GhiChu = !string.IsNullOrEmpty(item.GhiChu) ? item.GhiChu.Trim() : "";
                            //row.Dieu2 = !string.IsNullOrEmpty(item.SoTkNguoiNhanMar) ? item.SoTkNguoiNhanMar.Trim() : "";
                            row.PhuongThucThanhToan = (item.PhuongThucThanhToan != 0) ? item.PhuongThucThanhToan : 0;
                            row.NgayVeHaiChieu = (!string.IsNullOrEmpty(item.NgayVeHaiChieu.ToString()) && !item.NgayVeHaiChieu.ToString().Contains("/0001"))
                                ? item.NgayVeHaiChieu : DateTime.Parse("1900-01-01");
                            row.NgayTao = (!item.NgayTao.ToString().Contains("/0001") && !string.IsNullOrEmpty(item.NgayTao.ToString()))
                                ? item.NgayTao : DateTime.Parse("1900-01-01");

                            row.KFUnit = (item.KFUnit != 0) ? item.KFUnit : 0;
                            row.KFGroup = (item.KFGroup != 0) ? item.KFGroup : 0;
                            row.CheckVat = (item.CheckVat != 0) ? item.CheckVat : 0;
                            row.TongTienHD = (item.TongTienHD != 0) ? item.TongTienHD : 0;

                            row.LyDoDuyet = !string.IsNullOrEmpty(item.LyDoDuyet) ? item.LyDoDuyet.Trim() : "";
                            row.LyDoGuiDuyet = !string.IsNullOrEmpty(item.LyDoGuiDuyet) ? item.LyDoGuiDuyet.Trim() : "";
                            row.FileDuyet = !string.IsNullOrEmpty(item.FileDuyet) ? item.FileDuyet.Trim() : "";
                            row.FileGuiDuyet = !string.IsNullOrEmpty(item.FileGuiDuyet) ? item.FileGuiDuyet.Trim() : "";
                            row.ThoiGianGuiDuyet = (!item.ThoiGianGuiDuyet.ToString().Contains("/0001") && !string.IsNullOrEmpty(item.ThoiGianGuiDuyet.ToString()))
                                ? item.ThoiGianGuiDuyet : DateTime.Parse("1900-01-01");
                            row.ThoiGianDuyet = (!item.ThoiGianDuyet.ToString().Contains("/0001") && !string.IsNullOrEmpty(item.ThoiGianDuyet.ToString()))
                                ? item.ThoiGianDuyet : DateTime.Parse("1900-01-01");
                            row.NguoiDuyet = (item.NguoiDuyet != 0) ? item.NguoiDuyet : 0;
                            row.BookCode = !string.IsNullOrEmpty(item.BookCode) ? item.BookCode.Trim() : "";
                            row.PhuLuc1 = !string.IsNullOrEmpty(item.PhuLuc1) ? item.PhuLuc1.Trim() : "";
                            row.PhuLuc2 = !string.IsNullOrEmpty(item.PhuLuc2) ? item.PhuLuc2.Trim() : "";
                            row.PhuLuc3 = !string.IsNullOrEmpty(item.PhuLuc3) ? item.PhuLuc3.Trim() : "";
                            row.PhuLuc4 = !string.IsNullOrEmpty(item.PhuLuc4) ? item.PhuLuc4.Trim() : "";
                            row.PhuLuc5 = !string.IsNullOrEmpty(item.PhuLuc5) ? item.PhuLuc5.Trim() : "";
                            row.IsNew = 1;
                            row.NgayTao = DateTime.Now;
                            row.CustomerID = !string.IsNullOrEmpty(item.CustomerID) ? item.CustomerID.Trim() : "";
                            row.RowCreatedUser = username;
                            row.RowUpdatedUser = "";
                            row.RowCreatedAt = DateTime.Now;
                            row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                            var user = dbConn.FirstOrDefault<EmployeeInfo>(p => p.UserName == username);
                            if (user != null)
                            {
                                row.FKStaff = !string.IsNullOrEmpty(user.RefStaffId.ToString()) ? user.RefStaffId : 0;
                                row.KFGroup = !string.IsNullOrEmpty(user.Team) ? int.Parse(user.Team) : 0;
                                row.KFUnit = !string.IsNullOrEmpty(user.Region) ? int.Parse(user.Region) : 0;
                            }
                            else
                            {
                                row.FKStaff = 0;
                                row.KFGroup = 0;
                                row.KFUnit = 0;
                            }
                         
                            dbConn.Insert(row);
                            long id = dbConn.GetLastInsertId();
                            //update thông tin khách hàng
                            var itemKH = dbConn.Select<ERPAPD_Customer>(s => s.CodeLink == row.CustomerCode).FirstOrDefault();
                            if (itemKH != null)
                            {
                                itemKH.AgencyType = row.AgencyType!= itemKH.AgencyType? row.AgencyType: itemKH.AgencyType;
                                itemKH.Category = row.CategoryCode!= itemKH.Category ? row.CategoryCode: itemKH.Category;
                                itemKH.Label = row.Labels!= itemKH.Label ? row.Labels : itemKH.Label;
                                itemKH.Phone2 = row.DienThoaiA!= itemKH.Phone2? row.DienThoaiA : itemKH.Phone2;
                                itemKH.Fax = row.FaxA!= itemKH.Fax ? row.FaxA : itemKH.Fax;
                                itemKH.TaxCode = row.MaSoThueA != itemKH.TaxCode? row.MaSoThueA : itemKH.TaxCode;
                                itemKH.Position = row.ChucVuA!= itemKH.Position ? row.ChucVuA : itemKH.Position;
                                itemKH.Sponsor = row.DaiDienA != itemKH.Sponsor ? row.DaiDienA : itemKH.Sponsor;
                                itemKH.Address2 = row.DiaChiA != itemKH.Address2 ? row.DiaChiA : itemKH.Address2;
                                itemKH.BankCode = row.TaiKhoanA != itemKH.BankCode? row.TaiKhoanA : itemKH.BankCode;
                                itemKH.BankBranch = row.ChiNhanhA!= itemKH.BankBranch? row.ChiNhanhA : itemKH.BankBranch;
                                itemKH.BankName = row.NganHangA != itemKH.BankName ? row.NganHangA : itemKH.BankName;
                                dbConn.Update(itemKH);
                            }
                            return new { success = true, message = "Save success!", PK = id };
                        }
                        else
                        {
                            //checkExits.CustomerCode = !string.IsNullOrEmpty(item.CustomerCode) ? item.CustomerCode.Trim() : "";
                            checkExits.Code = !string.IsNullOrEmpty(item.Code) ? item.Code.Trim() : "";
                            checkExits.LoaiHopDong = !string.IsNullOrEmpty(item.LoaiHopDong) ? item.LoaiHopDong.Trim() : "";
                            checkExits.FKStaff = checkExits.FKStaff;
                            checkExits.Labels = !string.IsNullOrEmpty(item.Labels) ? item.Labels.Trim() : "";
                            checkExits.CategoryCode = !string.IsNullOrEmpty(item.CategoryCode) ? item.CategoryCode.Trim() : "";
                            checkExits.AgencyType = !string.IsNullOrEmpty(item.AgencyType) ? item.AgencyType.Trim() : "";

                            checkExits.BenA = !string.IsNullOrEmpty(item.BenA) ? item.BenA.Trim() : "";
                            checkExits.DaiDienA = !string.IsNullOrEmpty(item.DaiDienA) ? item.DaiDienA.Trim() : "";
                            checkExits.ChucVuA = !string.IsNullOrEmpty(item.ChucVuA) ? item.ChucVuA.Trim() : "";
                            checkExits.DiaChiA = !string.IsNullOrEmpty(item.DiaChiA) ? item.DiaChiA.Trim() : "";
                            checkExits.DienThoaiA = !string.IsNullOrEmpty(item.DienThoaiA) ? item.DienThoaiA.Trim() : "";
                            checkExits.FaxA = !string.IsNullOrEmpty(item.FaxA) ? item.FaxA.Trim() : "";
                            checkExits.TaiKhoanA = !string.IsNullOrEmpty(item.TaiKhoanA) ? item.TaiKhoanA.Trim() : "";
                            checkExits.NganHangA = !string.IsNullOrEmpty(item.NganHangA) ? item.NganHangA.Trim() : "";
                            checkExits.ChiNhanhA = !string.IsNullOrEmpty(item.ChiNhanhA) ? item.ChiNhanhA.Trim() : "";
                            checkExits.MaSoThueA = !string.IsNullOrEmpty(item.MaSoThueA) ? item.MaSoThueA.Trim() : "";
                            checkExits.KhachHangCaNhan = (item.KhachHangCaNhan != 0) ? item.KhachHangCaNhan : 0;
                            checkExits.SoCmtA = !string.IsNullOrEmpty(item.SoCmtA) ? item.SoCmtA.Trim() : "";
                            checkExits.NgayCapCmtA = !item.NgayCapCmtA.ToString().Contains("/0001") ? item.NgayCapCmtA : checkExits.NgayCapCmtA;
                            checkExits.NoiCapCmtA = !string.IsNullOrEmpty(item.NoiCapCmtA) ? item.NoiCapCmtA.Trim() : "";

                            checkExits.BenB = !string.IsNullOrEmpty(item.BenB) ? item.BenB.Trim() : "";
                            checkExits.DaiDienB = !string.IsNullOrEmpty(item.DaiDienB) ? item.DaiDienB.Trim() : "";
                            checkExits.ChucVuB = !string.IsNullOrEmpty(item.ChucVuB) ? item.ChucVuB.Trim() : "";
                            checkExits.DiaChiB = !string.IsNullOrEmpty(item.DiaChiB) ? item.DiaChiB.Trim() : "";
                            checkExits.DienThoaiB = !string.IsNullOrEmpty(item.DienThoaiB) ? item.DienThoaiB.Trim() : "";
                            checkExits.FaxB = !string.IsNullOrEmpty(item.FaxB) ? item.FaxB.Trim() : "";
                            checkExits.TaiKhoanB = !string.IsNullOrEmpty(item.TaiKhoanB) ? item.TaiKhoanB.Trim() : "";
                            checkExits.NganHangB = !string.IsNullOrEmpty(item.NganHangB) ? item.NganHangB.Trim() : "";
                            checkExits.ChiNhanhB = !string.IsNullOrEmpty(item.ChiNhanhB) ? item.ChiNhanhB.Trim() : "";
                            checkExits.MaSoThueB = !string.IsNullOrEmpty(item.MaSoThueB) ? item.MaSoThueB.Trim() : "";

                            checkExits.KhuyenMaiChung = (item.KhuyenMaiChung != 0) ? item.KhuyenMaiChung : 0;
                            checkExits.ChietKhauChungText1 = !string.IsNullOrEmpty(item.ChietKhauChungText1) ? item.ChietKhauChungText1.Trim() : "";
                            checkExits.ChietKhauChungText2 = !string.IsNullOrEmpty(item.ChietKhauChungText2) ? item.ChietKhauChungText2.Trim() : "";
                            checkExits.ChietKhauChungText3 = !string.IsNullOrEmpty(item.ChietKhauChungText3) ? item.ChietKhauChungText3.Trim() : "";
                            checkExits.ChietKhauChung1 = (item.ChietKhauChung1 != 0) ? item.ChietKhauChung1 : 0;
                            checkExits.ChietKhauChung2 = (item.ChietKhauChung2 != 0) ? item.ChietKhauChung2 : 0;
                            checkExits.ChietKhauChung3 = (item.ChietKhauChung3 != 0) ? item.ChietKhauChung3 : 0;
                            checkExits.GiamTrucTiep = (item.GiamTrucTiep != 0) ? item.GiamTrucTiep : 0;

                            checkExits.NguoiNhanMar = !string.IsNullOrEmpty(item.NguoiNhanMar) ? item.NguoiNhanMar.Trim() : "";
                            checkExits.BoPhanNguoiNhanMar = !string.IsNullOrEmpty(item.BoPhanNguoiNhanMar) ? item.BoPhanNguoiNhanMar.Trim() : "";
                            checkExits.DienThoaiNguoiNhanMar = (item.DienThoaiNguoiNhanMar != 0) ? item.DienThoaiNguoiNhanMar : 0;
                            checkExits.SoTkNguoiNhanMar = !string.IsNullOrEmpty(item.SoTkNguoiNhanMar) ? item.SoTkNguoiNhanMar.Trim() : "";
                            checkExits.c_ngan_hang_nguoi_nhan_mar = !string.IsNullOrEmpty(item.c_ngan_hang_nguoi_nhan_mar) ? item.c_ngan_hang_nguoi_nhan_mar.Trim() : "";
                            checkExits.c_chi_nhanh_nguoi_nhan_mar = !string.IsNullOrEmpty(item.c_chi_nhanh_nguoi_nhan_mar) ? item.c_chi_nhanh_nguoi_nhan_mar.Trim() : "";

                            checkExits.PhiMar = (item.PhiMar != 0) ? item.PhiMar : 0;
                            checkExits.LoaiDichVu = (item.LoaiDichVu != 0) ? item.LoaiDichVu : 0;
                            if (item.DieuKhoan != null)
                            {
                                if (!item.DieuKhoan.Trim().Contains("UnicodeConverted"))
                                    checkExits.DieuKhoan = !string.IsNullOrEmpty(item.DieuKhoan) ? item.DieuKhoan.Trim() + "<p hidden> UnicodeConverted <p>" : "";
                                else
                                    checkExits.DieuKhoan = !string.IsNullOrEmpty(item.DieuKhoan) ? item.DieuKhoan.Trim() : "";
                            }
                            checkExits.TrangThai = (item.TrangThai != 0) ? item.TrangThai : 0;
                            if (item.GhiChu != null)
                            {
                                if (!item.GhiChu.Trim().Contains("UnicodeConverted"))
                                    checkExits.GhiChu = !string.IsNullOrEmpty(item.GhiChu) ? item.GhiChu.Trim() + "<p hidden> UnicodeConverted <p>" : "";
                                else
                                    checkExits.GhiChu = !string.IsNullOrEmpty(item.GhiChu) ? item.GhiChu.Trim() : "";
                            }
                            if (item.Dieu2 != null)
                            {
                                if (!item.Dieu2.Trim().Contains("UnicodeConverted"))
                                    checkExits.Dieu2 = !string.IsNullOrEmpty(item.Dieu2) ? item.Dieu2.Trim() + "<p hidden> UnicodeConverted <p>" : "";
                                else
                                    checkExits.Dieu2 = !string.IsNullOrEmpty(item.Dieu2) ? item.Dieu2.Trim() : "";
                            }
                            checkExits.PhuongThucThanhToan = (item.PhuongThucThanhToan != 0) ? item.PhuongThucThanhToan : 0;
                            checkExits.NgayVeHaiChieu = !item.NgayVeHaiChieu.ToString().Contains("/0001") ? item.NgayVeHaiChieu : checkExits.NgayVeHaiChieu;
                            checkExits.NgayTao = !item.NgayTao.ToString().Contains("/0001") ? item.NgayTao : checkExits.NgayTao;
                            checkExits.KFUnit = checkExits.KFUnit;
                            checkExits.KFGroup = checkExits.KFGroup;
                            checkExits.CheckVat = (item.CheckVat != 0) ? item.CheckVat : 0;
                            checkExits.TongTienHD = (item.TongTienHD != 0) ? item.TongTienHD : 0;

                            checkExits.LyDoDuyet = !string.IsNullOrEmpty(item.LyDoDuyet) ? item.LyDoDuyet.Trim() : "";
                            checkExits.LyDoGuiDuyet = !string.IsNullOrEmpty(item.LyDoGuiDuyet) ? item.LyDoGuiDuyet.Trim() : "";
                            checkExits.FileDuyet = !string.IsNullOrEmpty(item.FileDuyet) ? item.FileDuyet.Trim() : "";
                            checkExits.FileGuiDuyet = !string.IsNullOrEmpty(item.FileGuiDuyet) ? item.FileGuiDuyet.Trim() : "";
                            checkExits.ThoiGianGuiDuyet = !item.ThoiGianGuiDuyet.ToString().Contains("/0001") ? item.ThoiGianGuiDuyet : checkExits.ThoiGianGuiDuyet;
                            checkExits.ThoiGianDuyet = !item.ThoiGianDuyet.ToString().Contains("/0001") ? item.ThoiGianDuyet : checkExits.ThoiGianDuyet;
                            checkExits.NguoiDuyet = (item.NguoiDuyet != 0) ? item.NguoiDuyet : 0;
                            checkExits.BookCode = !string.IsNullOrEmpty(item.BookCode) ? item.BookCode.Trim() : "";
                            checkExits.PhuLuc1 = !string.IsNullOrEmpty(item.PhuLuc1) ? item.PhuLuc1.Trim() : "";
                            checkExits.PhuLuc2 = !string.IsNullOrEmpty(item.PhuLuc2) ? item.PhuLuc2.Trim() : "";
                            checkExits.PhuLuc3 = !string.IsNullOrEmpty(item.PhuLuc3) ? item.PhuLuc3.Trim() : "";
                            checkExits.PhuLuc4 = !string.IsNullOrEmpty(item.PhuLuc4) ? item.PhuLuc4.Trim() : "";

                            checkExits.PhuLuc5 = !string.IsNullOrEmpty(item.PhuLuc5) ? item.PhuLuc5.Trim() : "";

                            checkExits.CustomerID = !string.IsNullOrEmpty(item.CustomerID) ? item.CustomerID.Trim() : "";
                            checkExits.RowUpdatedUser = username;
                            checkExits.RowUpdatedAt = DateTime.Now;

                            dbConn.Update(checkExits);
                            //update thông tin khách hàng
                            //update thông tin khách hàng
                            var itemKH = dbConn.Select<ERPAPD_Customer>(s => s.CustomerID == checkExits.CustomerID).FirstOrDefault();
                            if (itemKH != null)
                            {
                                itemKH.AgencyType = checkExits.AgencyType != itemKH.AgencyType ? checkExits.AgencyType : itemKH.AgencyType;
                                itemKH.Category = checkExits.CategoryCode != itemKH.Category ? checkExits.CategoryCode : itemKH.Category;
                                itemKH.Label = checkExits.Labels != itemKH.Label ? checkExits.Labels : itemKH.Label;
                                itemKH.Phone2 = checkExits.DienThoaiA != itemKH.Phone2 ? checkExits.DienThoaiA : itemKH.Phone2;
                                itemKH.Fax = checkExits.FaxA != itemKH.Fax ? checkExits.FaxA : itemKH.Fax;
                                itemKH.TaxCode = checkExits.MaSoThueA != itemKH.TaxCode ? checkExits.MaSoThueA : itemKH.TaxCode;
                                itemKH.Position = checkExits.ChucVuA != itemKH.Position ? checkExits.ChucVuA : itemKH.Position;
                                itemKH.Sponsor = checkExits.DaiDienA != itemKH.Sponsor ? checkExits.DaiDienA : itemKH.Sponsor;
                                itemKH.Address2 = checkExits.DiaChiA != itemKH.Address2 ? checkExits.DiaChiA : itemKH.Address2;
                                itemKH.BankCode = checkExits.TaiKhoanA != itemKH.BankCode ? checkExits.TaiKhoanA : itemKH.BankCode;
                                itemKH.BankBranch = checkExits.ChiNhanhA != itemKH.BankBranch ? checkExits.ChiNhanhA : itemKH.BankBranch;
                                itemKH.BankName = checkExits.NganHangA != itemKH.BankName ? checkExits.NganHangA : itemKH.BankName;
                                dbConn.Update(itemKH);
                            }
                            return new { success = true, message = "Save success!" };
                        }



                    }
                    catch (Exception e)
                    {
                        return new { success = false, message = e.Message };
                    }
                }
                else
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }
       
        public DataSourceResult GetPage(DataSourceRequest request, string whereCondition, int group)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Page", request.Page));
            param.Add(new SqlParameter("@PageSize", request.PageSize));
            param.Add(new SqlParameter("@WhereCondition", whereCondition));
            param.Add(new SqlParameter("@Sort", ""));
            param.Add(new SqlParameter("@Group", group));
            DataTable dt = new SqlHelper().ExecuteQuery("p_GetAll_ContractStaff", param);
            var lst = new List<CRM_Contract_Draff>();
            foreach (DataRow row in dt.Rows)
            {
                var item = new CRM_Contract_Draff();
                item.PKContractDraft = !row.IsNull("PKContractDraft") ? int.Parse(row["PKContractDraft"].ToString()) : 0;
                item.Code = !row.IsNull("Code") ? row["Code"].ToString() : "";
                item.LoaiHopDong = !row.IsNull("LoaiHopDong") ? row["LoaiHopDong"].ToString() : "";
                item.ContractTypeName = !row.IsNull("ContractTypeName") ? row["ContractTypeName"].ToString() : "";
                item.TotalMoney = !row.IsNull("TotalMoney") ? long.Parse(row["TotalMoney"].ToString()) : 0;
                item.TongTienHD = !row.IsNull("TongTienHD") ? long.Parse(row["TongTienHD"].ToString()) : 0;
                item.CustomerID = !row.IsNull("CustomerID") ? row["CustomerID"].ToString() : "";
                item.CustomerCode = !row.IsNull("CustomerCode") ? row["CustomerCode"].ToString() : "";
                item.CustomerName = !row.IsNull("CustomerName") ? row["CustomerName"].ToString() : "";
                item.ShortName = !row.IsNull("ShortName") ? row["ShortName"].ToString() : "";
                item.CategoryID = !row.IsNull("CategoryID") ? row["CategoryID"].ToString() : "";
                item.CategoryName = !row.IsNull("CategoryName") ? row["CategoryName"].ToString() : "";
                item.CustomerTypeName = !row.IsNull("CustomerTypeName") ? row["CustomerTypeName"].ToString() : "";
                item.CustomerType = !row.IsNull("CustomerType") ? row["CustomerType"].ToString() : "";
                item.CategorySubID = !row.IsNull("CategorySubID") ? row["CategorySubID"].ToString() : "";
                item.CategorySubName = !row.IsNull("CategorySubName") ? row["CategorySubName"].ToString() : "";
                item.StatusApproved = !row.IsNull("StatusApproved") ? Convert.ToInt32(row["StatusApproved"].ToString()) : 0;
                item.UserApproved = !row.IsNull("UserApproved") ? Convert.ToInt32(row["UserApproved"].ToString()) : 0;
                item.RequestDate = !row.IsNull("RequestDate") ? DateTime.Parse(row["RequestDate"].ToString()) : DateTime.Parse("01/01/1900");
                item.ApprovedDate = !row.IsNull("ApprovedDate") ? DateTime.Parse(row["ApprovedDate"].ToString()) : DateTime.Parse("01/01/1900");
                item.CategoryCode = !row.IsNull("CategoryCode") ? row["CategoryCode"].ToString() : "";
                item.TrangThai = !row.IsNull("TrangThai") ? Convert.ToInt32(row["TrangThai"].ToString()) : 0;
                item.ThoiGianDuyet = !row.IsNull("ThoiGianDuyet") ? DateTime.Parse(row["ThoiGianDuyet"].ToString()) : DateTime.Parse("01/01/1900");
                item.UserNameApproved = !row.IsNull("UserNameApproved") ? row["UserNameApproved"].ToString() : "";
                item.StatusNameApproved = !row.IsNull("StatusNameApproved") ? row["StatusNameApproved"].ToString() : "";
                item.UserIDStaff = !row.IsNull("UserIDStaff") ? Convert.ToInt32(row["UserIDStaff"].ToString()) : 0;
                item.UserNameStaff = !row.IsNull("UserNameStaff") ? row["UserNameStaff"].ToString() : "";
                item.RegionID = !row.IsNull("RegionID") ? Convert.ToInt32(row["RegionID"].ToString()) : 0;
                item.RegionName = !row.IsNull("RegionName") ? row["RegionName"].ToString() : "";
                item.TeamID = !row.IsNull("TeamID") ? Convert.ToInt32(row["TeamID"].ToString()) : 0;
                item.TeamName = !row.IsNull("TeamName") ? row["TeamName"].ToString() : "";
                item.Labels = !row.IsNull("Labels") ? row["Labels"].ToString() : "";
                item.NgayTao = !row.IsNull("NgayTao") ? DateTime.Parse(row["NgayTao"].ToString()) : DateTime.Parse("01/01/1900");
                item.StatusOrderByStaus = !row.IsNull("StatusOrderByStaus") ? Convert.ToInt32(row["StatusOrderByStaus"].ToString()) : 0;
                lst.Add(item);
            }
            request.Filters = null;
            DataSourceResult result = new DataSourceResult();
            result.Data = lst;
            result.Total = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["RowCount"]) : 0;
            return result;

        }
    }
    public class CRM_Contract_Mar_Draff
    {
        [AutoIncrement]
        public string MarID { get; set; }
        public string FKContract { get; set; }
        public string Receiver { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string Fee { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowUpdatedUser { get; set; }

    }
    public class CRM_Contract_Product_Draff
    {
        [AutoIncrement]
        public int PKProduct { get; set; }
        [AutoIncrement]
        public int FKContract { get; set; }

        public string Website { get; set; }
        public string ProductType { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Nature { get; set; }
        public int Unit { get; set; }
        public int Number { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public int STT { get; set; }
        public double CPM { get; set; }
        public double CPC { get; set; }
        public string HUONG { get; set; }
        public string NguonDan { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowUpdatedUser { get; set; }
        public int FKProduct { get; set; }
        public int IsNew { get; set; }
        public double CKTienMat { get; set; }
        public string CTKMCKTien { get; set; }
        public double TotalAmtSevicePlus { get; set; }
        public double TotalAmtBeforeDiscount { get; set; }
        public double TotalAmtAfterDiscount { get; set; }
        [Ignore]
        public double TotalAmt { get; set; }

        [Ignore]
        public double DonGiaSauGiam { get; set; }
        [Ignore]
        public List<CRM_Contract_ListTime_Draff> ListTime { get; set; }
        [Ignore]
        public List<CRM_Contract_ListPromotionProduct_Draff> ListPromotion { get; set; }

        [Ignore]
        public DateTime DateUp { get; set; }
        [Ignore]
        public DateTime DateDown { get; set; }
        [Ignore]
        public string Week { get; set; }
        [Ignore]
        public int Discount { get; set; }
        [Ignore]
        public int Discount1 { get; set; }
        [Ignore]
        public int Discount2 { get; set; }
        [Ignore]
        public int Discount3 { get; set; }
        [Ignore]
        public float Money { get; set; }
        [Ignore]
        public int NumberDay { get; set; }
        [Ignore]
        public string Promotion { get; set; }
        [Ignore]

        public string ProductName { get; set; }
        [Ignore]
        public string WebsiteName { get; set; }
        [Ignore]
        public string CategoryName { get; set; }
        [Ignore]
        public string LocationName { get; set; }
        [Ignore]
        public string NatureName { get; set; }
        [Ignore]
        public string RegionName { get; set; }

        public static object Save(IEnumerable<CRM_Contract_Product_Draff> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Contract_Product_Draff>("PKProduct= {0}", request.PKProduct);
                            if (checkExits == null)
                            {
                                if (request.FKContract != 0)
                                {
                                    var row = new CRM_Contract_Product_Draff();
                                    row.FKContract = (request.FKContract != 0) ? request.FKContract : 0;
                                    row.Website = !string.IsNullOrEmpty(request.Website) ? request.Website.Trim() : "";
                                    row.ProductType = !string.IsNullOrEmpty(request.ProductType) ? request.ProductType.Trim() : "";
                                    row.Location = !string.IsNullOrEmpty(request.Location) ? request.Location.Trim() : "";
                                    row.Category = !string.IsNullOrEmpty(request.Category) ? request.Category.Trim() : "";
                                    row.DateUp = !string.IsNullOrEmpty(request.DateUp.ToString()) ? request.DateUp : DateTime.Parse("1900-01-01");
                                    row.DateDown = !string.IsNullOrEmpty(request.DateDown.ToString()) ? request.DateDown : DateTime.Parse("1900-01-01");
                                    row.Nature = !string.IsNullOrEmpty(request.Nature) ? request.Nature.Trim() : "";

                                    row.Unit = (request.Unit != 0) ? request.Unit : 0;
                                    row.Number = (request.Number != 0) ? request.Number : 0;
                                    row.Size = !string.IsNullOrEmpty(request.Size) ? request.Size.Trim() : "";
                                    row.Price = (request.Price != 0) ? request.Price : 0;
                                    row.STT = (request.STT != 0) ? request.STT : 0;

                                    row.CPM = (request.CPM != 0) ? request.CPM : 0;
                                    row.CPC = (request.CPC != 0) ? request.CPC : 0;
                                    row.HUONG = !string.IsNullOrEmpty(request.HUONG) ? request.HUONG.Trim() : "";
                                    row.NguonDan = !string.IsNullOrEmpty(request.NguonDan) ? request.NguonDan.Trim() : "";
                                    row.FKProduct = (request.FKProduct != 0) ? request.FKProduct : 0;
                                    row.IsNew = 1;
                                    row.CKTienMat = (request.CKTienMat != 0) ? request.CKTienMat : 0;
                                    row.CTKMCKTien = !string.IsNullOrEmpty(request.CTKMCKTien) ? request.CTKMCKTien.Trim() : "";

                                    row.RowCreatedUser = username;
                                    row.RowUpdatedUser = "";
                                    row.RowCreatedAt = DateTime.Now;
                                    row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                                    dbConn.Insert(row);
                                    Int64 id = dbConn.GetLastInsertId();
                                    CRM_Contract_ListTime_Draff.Save(request.ListTime, username, id);
                                    CRM_Contract_ListPromotionProduct_Draff.Save(request.ListPromotion, username, id);
                                }
                                else
                                {
                                    return new { success = false, message = "Data is null!" };
                                }
                            }
                            else
                            {

                                checkExits.Website = !string.IsNullOrEmpty(request.Website) ? request.Website.Trim() : "";
                                checkExits.ProductType = !string.IsNullOrEmpty(request.ProductType) ? request.ProductType.Trim() : "";
                                checkExits.Location = !string.IsNullOrEmpty(request.Location) ? request.Location.Trim() : "";
                                checkExits.Category = !string.IsNullOrEmpty(request.Category) ? request.Category.Trim() : "";
                                checkExits.DateUp = !string.IsNullOrEmpty(request.DateUp.ToString()) ? request.DateUp : DateTime.Parse("1900-01-01");
                                checkExits.DateDown = !string.IsNullOrEmpty(request.DateDown.ToString()) ? request.DateDown : DateTime.Parse("1900-01-01");
                                checkExits.Nature = !string.IsNullOrEmpty(request.Nature) ? request.Nature.Trim() : "";

                                checkExits.Unit = (request.Unit != 0) ? request.Unit : 0;
                                checkExits.Number = (request.Number != 0) ? request.Number : 0;
                                checkExits.Size = !string.IsNullOrEmpty(request.Size) ? request.Size.Trim() : "";
                                checkExits.Price = (request.Price != 0) ? request.Price : 0;
                                checkExits.STT = (request.STT != 0) ? request.STT : 0;

                                checkExits.CPM = (request.CPM != 0) ? request.CPM : 0;
                                checkExits.CPC = (request.CPC != 0) ? request.CPC : 0;
                                checkExits.HUONG = !string.IsNullOrEmpty(request.HUONG) ? request.HUONG.Trim() : "";
                                checkExits.NguonDan = !string.IsNullOrEmpty(request.NguonDan) ? request.NguonDan.Trim() : "";
                                checkExits.FKProduct = (request.FKProduct != 0) ? request.FKProduct : 0;
                                checkExits.IsNew = 1;
                                checkExits.CKTienMat = (request.CKTienMat != 0) ? request.CKTienMat : 0;
                                checkExits.CTKMCKTien = !string.IsNullOrEmpty(request.CTKMCKTien) ? request.CTKMCKTien.Trim() : "";
                                checkExits.RowUpdatedUser = username;
                                checkExits.RowUpdatedAt = DateTime.Now;
                                dbConn.Update(checkExits);
                                var rs = CRM_Contract_ListTime_Draff.Save(request.ListTime, username, request.PKProduct);
                                var rs1 = CRM_Contract_ListPromotionProduct_Draff.Save(request.ListPromotion, username, request.PKProduct);
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
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }
        public static List<CRM_Contract_Product_Draff> getProductByPKContract(int PKContract)
        {
            var listproduct = new List<CRM_Contract_Product_Draff>();
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                listproduct = dbConn.Select<CRM_Contract_Product_Draff>(@"
                        Select bu.*,
                        (select  Sum(NumberDay) from CRM_Contract_ListTime_Draff where FKProduct = bu.PKProduct) AS NumberDay, 
                        (select  name from ERPAPD_List where code = bu.Website and FKListtype = 20) AS WebsiteName,
                        (select  name from ERPAPD_List where code = bu.ProductType and FKListtype = 19) AS ProductName,
                        (select  name from ERPAPD_List where code = bu.Nature and FKListtype = 25) AS NatureName,
                         (select  name from ERPAPD_List where code = bu.Category and FKListtype = 22) AS CategoryName,
                         (select  name from ERPAPD_List where code = bu.Location and FKListtype = 23) AS LocationName, 
                         (select  name from ERPAPD_List where code = bu.Nature and FKListtype = 25) AS NatureName,
                        (select  Value from CRM_Hierarchy where HierarchyID = bu.Unit) AS c_region_name,
                        (select  Value from CRM_Hierarchy where HierarchyID = bu.Unit) AS RegionName
                         FROM CRM_Contract_Product_Draff bu 
                        WHERE bu.FKContract = " + PKContract);
                if (listproduct.Count > 0)
                {
                    foreach (var item in listproduct)
                    {
                        var listTime = dbConn.Select<CRM_Contract_ListTime_Draff>("FKProduct ={0}", item.PKProduct);
                        var listPromotion = dbConn.Select<CRM_Contract_ListPromotionProduct_Draff>("FKProduct={0}", item.PKProduct);
                        item.ListTime = listTime;
                        item.ListPromotion = listPromotion;
                    }
                }
            }

            return listproduct;
        }
    }
    public class CRM_Contract_Staff_Draff
    {
        [AutoIncrement]
        public Int64 PKStaff { get; set; }
        public Int64 FKContract { get; set; }
        public int UnitId { get; set; }
        public int StaffId { get; set; }
        public int DepartmentId { get; set; }
        public int GroupId { get; set; }
        public int Percent { get; set; }
        public double Money { get; set; }
        public double MoneyInYear { get; set; }
        public double MoneyWebOther { get; set; }
        public string XmlData { get; set; }
        public string Charge { get; set; }
        public double MoneyNextYear { get; set; }
        public double MoneyNextYear2 { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }

        [Ignore]
        public string TeamName { get; set; }
        [Ignore]
        public string FullName { get; set; }
        [Ignore]
        public string Province { get; set; }

        public static object Save(IEnumerable<CRM_Contract_Staff_Draff> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Contract_Staff_Draff>("PKStaff= {0}", request.PKStaff);
                            if (checkExits == null)
                            {
                                if (request.FKContract != 0)
                                {
                                    var row = new CRM_Contract_Staff_Draff();
                                    row.FKContract = (request.FKContract != 0) ? request.FKContract : 0;
                                    row.UnitId = (request.UnitId != 0) ? request.UnitId : 0;
                                    row.StaffId = (request.StaffId != 0) ? request.StaffId : 0;
                                    row.DepartmentId = (request.DepartmentId != 0) ? request.DepartmentId : 0;
                                    row.GroupId = (request.GroupId != 0) ? request.GroupId : 0;
                                    row.Percent = (request.Percent != 0) ? request.Percent : 0;
                                    row.Money = (request.Money != 0) ? request.Money : 0;
                                    row.MoneyInYear = (request.MoneyInYear != 0) ? request.MoneyInYear : 0;
                                    row.MoneyWebOther = (request.MoneyWebOther != 0) ? request.MoneyWebOther : 0;
                                    row.XmlData = !string.IsNullOrEmpty(request.XmlData) ? request.XmlData.Trim() : "";
                                    row.Charge = !string.IsNullOrEmpty(request.Charge) ? request.Charge.Trim() : "";
                                    row.MoneyNextYear = (request.MoneyNextYear != 0) ? request.MoneyNextYear : 0;
                                    row.MoneyNextYear2 = (request.MoneyNextYear2 != 0) ? request.MoneyNextYear2 : 0;
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
                            else
                            {
                                checkExits.UnitId = (request.UnitId != 0) ? request.UnitId : 0;
                                checkExits.StaffId = (request.StaffId != 0) ? request.StaffId : 0;
                                checkExits.DepartmentId = (request.DepartmentId != 0) ? request.DepartmentId : 0;
                                checkExits.GroupId = (request.GroupId != 0) ? request.GroupId : 0;
                                checkExits.Percent = (request.Percent != 0) ? request.Percent : 0;
                                checkExits.Money = (request.Money != 0) ? request.Money : 0;
                                checkExits.MoneyInYear = (request.MoneyInYear != 0) ? request.MoneyInYear : 0;
                                checkExits.MoneyWebOther = (request.MoneyWebOther != 0) ? request.MoneyWebOther : 0;
                                checkExits.XmlData = !string.IsNullOrEmpty(request.XmlData) ? request.XmlData.Trim() : "";
                                checkExits.Charge = !string.IsNullOrEmpty(request.Charge) ? request.Charge.Trim() : "";
                                checkExits.MoneyNextYear = (request.MoneyNextYear != 0) ? request.MoneyNextYear : 0;
                                checkExits.MoneyNextYear2 = (request.MoneyNextYear2 != 0) ? request.MoneyNextYear2 : 0;
                                checkExits.IsNew = 1;

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
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }
    }
    public class CRM_Contract_Bill
    {
        [AutoIncrement]
        public long PkBill { get; set; }
        public long FKContract { get; set; }
        public DateTime? BillDate { get; set; }
        public long Money { get; set; }
        public string BillCode { get; set; }
        public DateTime? InputDate { get; set; }
        public int FkStaff { get; set; }
        public int Type { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }
        public string Note { get; set; }
        [Ignore]
        public string BillDateString { get; set; }

        public static object Save(IEnumerable<CRM_Contract_Bill> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Contract_Bill>("PkBill= {0}", request.PkBill);
                            if (checkExits == null)
                            {
                                if (request.FKContract != 0)
                                {
                                    var row = new CRM_Contract_Bill();
                                    row.FKContract = (request.FKContract != 0) ? request.FKContract : 0;
                                    DateTime paymentDate = DateTime.Now;
                                    if (DateTime.TryParseExact(request.BillDateString.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out paymentDate))
                                    {
                                        row.BillDate = !string.IsNullOrEmpty(paymentDate.ToString()) ? paymentDate : DateTime.Parse("1900-01-01");
                                    }
                                    row.InputDate = !string.IsNullOrEmpty(request.InputDate.ToString()) ? request.InputDate : DateTime.Now;
                                    row.Money = (request.Money != 0) ? request.Money : 0;
                                    row.BillCode = !string.IsNullOrEmpty(request.BillCode) ? request.BillCode.Trim() : "";
                                    row.Note = !string.IsNullOrEmpty(request.Note) ? request.Note.Trim() : "";
                                    row.Type = (request.Type != 0) ? request.Type : 0;
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
                            else
                            {
                                checkExits.FKContract = (request.FKContract != 0) ? request.FKContract : 0;
                                checkExits.FKContract = (request.FKContract != 0) ? request.FKContract : 0;

                                DateTime paymentDate = DateTime.Now;
                                if (DateTime.TryParseExact(request.BillDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out paymentDate))
                                {
                                    checkExits.BillDate = !string.IsNullOrEmpty(paymentDate.ToString()) ? paymentDate : DateTime.Parse("1900-01-01");
                                }
                                checkExits.InputDate = !string.IsNullOrEmpty(request.InputDate.ToString()) ? request.InputDate : DateTime.Now;
                                checkExits.Money = (request.Money != 0) ? request.Money : 0;
                                checkExits.BillCode = !string.IsNullOrEmpty(request.BillCode) ? request.BillCode.Trim() : "";
                                checkExits.Note = !string.IsNullOrEmpty(request.Note) ? request.Note.Trim() : "";
                                checkExits.Type = (request.Type != 0) ? request.Type : 0;
                                checkExits.IsNew = 1;

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
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }
    }
    public class CRM_Contract_Cost
    {
        [AutoIncrement]
        public Int64 PKCost { get; set; }
        public Int64 FKContract { get; set; }
        public int Staff { get; set; }
        public DateTime CostDate { get; set; }
        public string CostType { get; set; }
        public float Money { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public string AsciiName { get; set; }
        public string UpperName { get; set; }
        public string FileName { get; set; }
        public string XmlData { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }

    }
    public class CRM_Contract_Other_Website
    {
        [AutoIncrement]
        public Int64 PKWebsite { get; set; }
        public Int64 FkContract { get; set; }
        public string WebsiteCode { get; set; }
        public float Money { get; set; }
        public string XmlData { get; set; }

        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }

    }
    public class CRM_Contract_Product
    {
        [AutoIncrement]
        public Int32 PKContractProduct { get; set; }
        public Int32 FkContract { get; set; }
        public Int32 FkProduct { get; set; }
        public string Code { get; set; }
        public string Website { get; set; }
        public string ProductType { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }

        public double Price { get; set; }
        public double MoneyNextYear { get; set; }
        public string XmlData { get; set; }
        public double Reduction_money { get; set; }
        public int Percent { get; set; }
        public double UnitPrice { get; set; }
        public double PreferencesMoney { get; set; }

        public string PageType { get; set; }
        public string Nature { get; set; }
        public string BoxCheck { get; set; }
        public double Number { get; set; }
        public double PricePublic { get; set; }
        public double PriceCharged { get; set; }
        public string Promotion { get; set; }

        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }
        [Ignore]
        public string WebsiteName { get; set; }
        [Ignore]
        public string ProductTypeName { get; set; }
        [Ignore]
        public string CategoryName { get; set; }
        [Ignore]
        public string LocationName { get; set; }
        [Ignore]
        public string NatureName { get; set; }
        [Ignore]
        public string c_book_code { get; set; }
        [Ignore]
        public DateTime NgayLen { get; set; }
        [Ignore]
        public int GioLen { get; set; }
        [Ignore]
        public string AdvChannel { get; set; }
        [Ignore]
        public string AdvChannelName { get; set; }
        [Ignore]
        public DateTime DateUp { get; set; }
        [Ignore]
        public DateTime DateDown { get; set; }
        [Ignore]
        public int VungMien { get; set; }
        [Ignore]
        public DateTime NgayXuong { get; set; }
        [Ignore]
        public string Label { get; set; }
        [Ignore]
        public int Quantity { get; set; }
        [Ignore]
        public int WebsiteID { get; set; }
        [Ignore]
        public int CategoryID { get; set; }
        [Ignore]
        public int LocationID { get; set; }
        [Ignore]
        public int NatureID { get; set; }
        [Ignore]
        public int TenVungMien { get; set; }

        public static object Save(IEnumerable<CRM_Contract_Product> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Contract_Product>("PKContractProduct= {0}", request.PKContractProduct);
                            if (checkExits == null)
                            {
                                if (request.FkContract != 0)
                                {
                                    var row = new CRM_Contract_Product();
                                    row.FkContract = (request.FkContract != 0) ? request.FkContract : 0;
                                    row.FkProduct = (request.FkProduct != 0) ? request.FkProduct : 0;
                                    row.Code = !string.IsNullOrEmpty(request.Code) ? request.Code.Trim() : "";
                                    row.Website = !string.IsNullOrEmpty(request.Website) ? request.Website.Trim() : "";
                                    row.ProductType = !string.IsNullOrEmpty(request.ProductType) ? request.ProductType.Trim() : "";
                                    row.Location = !string.IsNullOrEmpty(request.Location) ? request.Location.Trim() : "";
                                    row.Category = !string.IsNullOrEmpty(request.Category) ? request.Category.Trim() : "";

                                    row.Price = (request.Price != 0) ? request.Price : 0;
                                    row.XmlData = !string.IsNullOrEmpty(request.XmlData) ? request.XmlData.Trim() : "";
                                    row.MoneyNextYear = (request.MoneyNextYear != 0) ? request.MoneyNextYear : 0;
                                    row.Reduction_money = (request.Reduction_money != 0) ? request.Reduction_money : 0;
                                    row.Percent = (request.Percent != 0) ? request.Percent : 0;
                                    row.UnitPrice = (request.UnitPrice != 0) ? request.UnitPrice : 0;
                                    row.PreferencesMoney = (request.PreferencesMoney != 0) ? request.PreferencesMoney : 0;

                                    row.PageType = !string.IsNullOrEmpty(request.PageType) ? request.PageType.Trim() : "";
                                    row.Nature = !string.IsNullOrEmpty(request.Nature) ? request.Nature.Trim() : "";
                                    row.BoxCheck = !string.IsNullOrEmpty(request.BoxCheck) ? request.BoxCheck.Trim() : "";
                                    row.Number = (request.Number != 0) ? request.Number : 0;
                                    row.PricePublic = (request.PricePublic != 0) ? request.PricePublic : 0;
                                    row.PriceCharged = (request.PriceCharged != 0) ? request.PriceCharged : 0;
                                    row.Promotion = !string.IsNullOrEmpty(request.Promotion) ? request.Promotion.Trim() : "";
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
                            else
                            {

                                checkExits.FkContract = (request.FkContract != 0) ? request.FkContract : 0;
                                checkExits.FkProduct = (request.FkProduct != 0) ? request.FkProduct : 0;
                                checkExits.Code = !string.IsNullOrEmpty(request.Code) ? request.Code.Trim() : "";
                                checkExits.Website = !string.IsNullOrEmpty(request.Website) ? request.Website.Trim() : "";
                                checkExits.ProductType = !string.IsNullOrEmpty(request.ProductType) ? request.ProductType.Trim() : "";
                                checkExits.Location = !string.IsNullOrEmpty(request.Location) ? request.Location.Trim() : "";
                                checkExits.Category = !string.IsNullOrEmpty(request.Category) ? request.Category.Trim() : "";

                                checkExits.Price = (request.Price != 0) ? request.Price : 0;
                                checkExits.XmlData = !string.IsNullOrEmpty(request.XmlData) ? request.XmlData.Trim() : "";
                                checkExits.MoneyNextYear = (request.MoneyNextYear != 0) ? request.MoneyNextYear : 0;
                                checkExits.Reduction_money = (request.Reduction_money != 0) ? request.Reduction_money : 0;
                                checkExits.Percent = (request.Percent != 0) ? request.Percent : 0;
                                checkExits.UnitPrice = (request.UnitPrice != 0) ? request.UnitPrice : 0;
                                checkExits.PreferencesMoney = (request.PreferencesMoney != 0) ? request.PreferencesMoney : 0;

                                checkExits.PageType = !string.IsNullOrEmpty(request.PageType) ? request.PageType.Trim() : "";
                                checkExits.Nature = !string.IsNullOrEmpty(request.Nature) ? request.Nature.Trim() : "";
                                checkExits.BoxCheck = !string.IsNullOrEmpty(request.BoxCheck) ? request.BoxCheck.Trim() : "";
                                checkExits.Number = (request.Number != 0) ? request.Number : 0;
                                checkExits.PricePublic = (request.PricePublic != 0) ? request.PricePublic : 0;
                                checkExits.PriceCharged = (request.PriceCharged != 0) ? request.PriceCharged : 0;
                                checkExits.Promotion = !string.IsNullOrEmpty(request.Promotion) ? request.Promotion.Trim() : "";
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
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }
    }
    public class CRM_Contract_Product_CPM
    {
        [AutoIncrement]
        public Int64 PKCpm { get; set; }
        public Int64 FkContract { get; set; }
        public string Code { get; set; }
        public string AdvChannel { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public DateTime DateUp { get; set; }
        public DateTime DateDown { get; set; }
        public Int64 UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Int64 Discount { get; set; }
        public Int64 TotalAmt { get; set; }
        public DateTime InputDate { get; set; }
        public double DSChuyen { get; set; }
        
        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }
        [Ignore]
        public int WebsiteID { get; set; }
        [Ignore]
        public string c_kenh_qc_name { get; set; }
        [Ignore]
        public string c_chuyen_muc_kenhqc_name { get; set; }
        [Ignore]
        public string c_vi_tri_kenhqc_name { get; set; }
        [Ignore]
        public string StrDateUp { get; set; }
        [Ignore]
        public string StrDateDown { get; set; }

        public static object Save(IEnumerable<CRM_Contract_Product_CPM> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Contract_Product_CPM>("PKCpm= {0}", request.PKCpm);
                            if (checkExits == null)
                            {
                                if (request.FkContract != 0)
                                {
                                    var row = new CRM_Contract_Product_CPM();
                                    row.FkContract = (request.FkContract != 0) ? request.FkContract : 0;
                                    row.Code = !string.IsNullOrEmpty(request.Code) ? request.Code.Trim() : "";
                                    row.AdvChannel = !string.IsNullOrEmpty(request.AdvChannel) ? request.AdvChannel.Trim() : "";
                                    row.Location = !string.IsNullOrEmpty(request.Location) ? request.Location.Trim() : "";
                                    row.Category = !string.IsNullOrEmpty(request.Category) ? request.Category.Trim() : "";
                                    row.DateUp = !string.IsNullOrEmpty(request.StrDateUp) ? DateTime.Parse(request.StrDateUp) : DateTime.Parse("1900-01-01");
                                    row.DateDown = !string.IsNullOrEmpty(request.StrDateDown) ? DateTime.Parse(request.StrDateDown) : DateTime.Parse("1900-01-01");
                                    row.InputDate = DateTime.Parse("1900-01-01");
                                    row.DSChuyen = (request.DSChuyen != 0) ? request.DSChuyen : 0;
                                    row.UnitPrice = (request.UnitPrice != 0) ? request.UnitPrice : 0;
                                    row.Quantity = (request.Quantity != 0) ? request.Quantity : 0;
                                    row.Discount = (request.Discount != 0) ? request.Discount : 0;
                                    row.TotalAmt = (request.TotalAmt != 0) ? request.TotalAmt : 0;
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
                            else
                            {

                                checkExits.FkContract = (request.FkContract != 0) ? request.FkContract : 0;
                                checkExits.Code = !string.IsNullOrEmpty(request.Code) ? request.Code.Trim() : "";
                                checkExits.AdvChannel = !string.IsNullOrEmpty(request.AdvChannel) ? request.AdvChannel.Trim() : "";
                                checkExits.Location = !string.IsNullOrEmpty(request.Location) ? request.Location.Trim() : "";
                                checkExits.Category = !string.IsNullOrEmpty(request.Category) ? request.Category.Trim() : "";
                                checkExits.DateUp = !string.IsNullOrEmpty(request.StrDateUp) ? DateTime.Parse(request.StrDateUp) : DateTime.Parse("1900-01-01");
                                checkExits.DateDown = !string.IsNullOrEmpty(request.StrDateDown) ? DateTime.Parse(request.StrDateDown) : DateTime.Parse("1900-01-01");
                                checkExits.InputDate = DateTime.Parse("1900-01-01");
                                checkExits.DSChuyen = (request.DSChuyen != 0) ? request.DSChuyen : 0;
                                checkExits.UnitPrice = (request.UnitPrice != 0) ? request.UnitPrice : 0;
                                checkExits.Quantity = (request.Quantity != 0) ? request.Quantity : 0;
                                checkExits.Discount = (request.Discount != 0) ? request.Discount : 0;
                                checkExits.TotalAmt = (request.TotalAmt != 0) ? request.TotalAmt : 0;
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
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }
    }
    public class CRM_Contract_Product_CPM_Draff
    {
        [AutoIncrement]
        public Int64 PKCpm { get; set; }
        public Int64 FkContract { get; set; }
        public string Code { get; set; }
        public string AdvChannel { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public DateTime DateUp { get; set; }
        public DateTime DateDown { get; set; }
        public Int64 UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Int64 Discount { get; set; }
        public Int64 TotalAmt { get; set; }
        public DateTime InputDate { get; set; }

        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }
        [Ignore]
        public string DateUpString { get; set; }
        [Ignore]
        public string DateDownString { get; set; }
        public static object Save(IEnumerable<CRM_Contract_Product_CPM_Draff> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Contract_Product_CPM_Draff>("PKCpm= {0}", request.PKCpm);
                            if (checkExits == null)
                            {
                                if (request.FkContract != 0)
                                {
                                    var row = new CRM_Contract_Product_CPM_Draff();
                                    row.FkContract = (request.FkContract != 0) ? request.FkContract : 0;
                                    row.Code = !string.IsNullOrEmpty(request.Code) ? request.Code.Trim() : "";
                                    row.AdvChannel = !string.IsNullOrEmpty(request.AdvChannel) ? request.AdvChannel.Trim() : "";
                                    row.Location = !string.IsNullOrEmpty(request.Location) ? request.Location.Trim() : "";
                                    row.Category = !string.IsNullOrEmpty(request.Category) ? request.Category.Trim() : "";
                                    row.DateUp = !string.IsNullOrEmpty(request.DateUpString.ToString()) ? DateTime.Parse(request.DateUpString) : DateTime.Parse("1900-01-01");
                                    row.DateDown = !string.IsNullOrEmpty(request.DateDownString.ToString()) ? DateTime.Parse(request.DateDownString) : DateTime.Parse("1900-01-01");
                                    row.InputDate = DateTime.Parse("1900-01-01");

                                    row.UnitPrice = (request.UnitPrice != 0) ? request.UnitPrice : 0;
                                    row.Quantity = (request.Quantity != 0) ? request.Quantity : 0;
                                    row.Discount = (request.Discount != 0) ? request.Discount : 0;
                                    row.TotalAmt = (request.TotalAmt != 0) ? request.TotalAmt : 0;
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
                            else
                            {

                                checkExits.FkContract = (request.FkContract != 0) ? request.FkContract : 0;
                                checkExits.Code = !string.IsNullOrEmpty(request.Code) ? request.Code.Trim() : "";
                                checkExits.AdvChannel = !string.IsNullOrEmpty(request.AdvChannel) ? request.AdvChannel.Trim() : "";
                                checkExits.Location = !string.IsNullOrEmpty(request.Location) ? request.Location.Trim() : "";
                                checkExits.Category = !string.IsNullOrEmpty(request.Category) ? request.Category.Trim() : "";
                                checkExits.DateUp = !string.IsNullOrEmpty(request.DateUpString.ToString()) ? DateTime.Parse(request.DateUpString) : checkExits.DateUp;
                                checkExits.DateDown = !string.IsNullOrEmpty(request.DateDownString.ToString()) ? DateTime.Parse(request.DateDownString) : checkExits.DateDown;
                                checkExits.InputDate = DateTime.Parse("1900-01-01");

                                checkExits.UnitPrice = (request.UnitPrice != 0) ? request.UnitPrice : 0;
                                checkExits.Quantity = (request.Quantity != 0) ? request.Quantity : 0;
                                checkExits.Discount = (request.Discount != 0) ? request.Discount : 0;
                                checkExits.TotalAmt = (request.TotalAmt != 0) ? request.TotalAmt : 0;
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
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }
    }
    public class CRM_Contract_Product_CPM_Draft
    {
        [AutoIncrement]
        public Int64 PKCpm { get; set; }
        public Int64 FkContract { get; set; }
        public string Code { get; set; }
        public string AdvChannel { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public DateTime DateUp { get; set; }
        public DateTime DateDown { get; set; }
        public Int64 UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Int64 Discount { get; set; }
        public Int64 TotalAmt { get; set; }
        public DateTime InputDate { get; set; }

        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }
        public static object Save(IEnumerable<CRM_Contract_Product_CPM_Draft> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Contract_Product_CPM_Draft>("PKCpm= {0}", request.PKCpm);
                            if (checkExits == null)
                            {
                                if (request.FkContract != 0)
                                {
                                    var row = new CRM_Contract_Product_CPM_Draft();
                                    row.FkContract = (request.FkContract != 0) ? request.FkContract : 0;
                                    row.Code = !string.IsNullOrEmpty(request.Code) ? request.Code.Trim() : "";
                                    row.AdvChannel = !string.IsNullOrEmpty(request.AdvChannel) ? request.AdvChannel.Trim() : "";
                                    row.Location = !string.IsNullOrEmpty(request.Location) ? request.Location.Trim() : "";
                                    row.Category = !string.IsNullOrEmpty(request.Category) ? request.Category.Trim() : "";
                                    row.DateUp = !string.IsNullOrEmpty(request.DateUp.ToString()) ? request.DateUp : DateTime.Parse("1900-01-01");
                                    row.DateDown = !string.IsNullOrEmpty(request.DateDown.ToString()) ? request.DateDown : DateTime.Parse("1900-01-01");
                                    row.InputDate = DateTime.Parse("1900-01-01");

                                    row.UnitPrice = (request.UnitPrice != 0) ? request.UnitPrice : 0;
                                    row.Quantity = (request.Quantity != 0) ? request.Quantity : 0;
                                    row.Discount = (request.Discount != 0) ? request.Discount : 0;
                                    row.TotalAmt = (request.TotalAmt != 0) ? request.TotalAmt : 0;
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
                            else
                            {

                                checkExits.FkContract = (request.FkContract != 0) ? request.FkContract : 0;
                                checkExits.Code = !string.IsNullOrEmpty(request.Code) ? request.Code.Trim() : "";
                                checkExits.AdvChannel = !string.IsNullOrEmpty(request.AdvChannel) ? request.AdvChannel.Trim() : "";
                                checkExits.Location = !string.IsNullOrEmpty(request.Location) ? request.Location.Trim() : "";
                                checkExits.Category = !string.IsNullOrEmpty(request.Category) ? request.Category.Trim() : "";
                                checkExits.DateUp = !string.IsNullOrEmpty(request.DateUp.ToString()) ? request.DateUp : DateTime.Parse("1900-01-01");
                                checkExits.DateDown = !string.IsNullOrEmpty(request.DateDown.ToString()) ? request.DateDown : DateTime.Parse("1900-01-01");
                                checkExits.InputDate = DateTime.Parse("1900-01-01");

                                checkExits.UnitPrice = (request.UnitPrice != 0) ? request.UnitPrice : 0;
                                checkExits.Quantity = (request.Quantity != 0) ? request.Quantity : 0;
                                checkExits.Discount = (request.Discount != 0) ? request.Discount : 0;
                                checkExits.TotalAmt = (request.TotalAmt != 0) ? request.TotalAmt : 0;
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
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }
    }
    public class CRM_Contract_Product_Packet
    {
        [AutoIncrement]
        public Int64 PKPacket { get; set; }
        public Int64 FKContract { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime DateUp { get; set; }
        public DateTime DateDown { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }

        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        [Ignore]
        public string DateUpString { get; set; }
        [Ignore]
        public string DateDownString { get; set; }
        public static object Save(IEnumerable<CRM_Contract_Product_Packet> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Contract_Product_Packet>("PKPacket= {0}", request.PKPacket);
                            if (checkExits == null)
                            {
                                if (request.FKContract != 0)
                                {
                                    var row = new CRM_Contract_Product_Packet();
                                    row.FKContract = (request.FKContract != 0) ? request.FKContract : 0;
                                    row.Code = !string.IsNullOrEmpty(request.Code) ? request.Code.Trim() : "";
                                    row.Name = !string.IsNullOrEmpty(request.Name) ? request.Name.Trim() : "";
                                    row.DateUp = !string.IsNullOrEmpty(request.DateUpString.ToString()) ? DateTime.Parse(request.DateUpString) : DateTime.Parse("1900-01-01");
                                    row.DateDown = !string.IsNullOrEmpty(request.DateDownString.ToString()) ? DateTime.Parse(request.DateDownString) : DateTime.Parse("1900-01-01");
                                    row.UnitPrice = (request.UnitPrice != 0) ? request.UnitPrice : 0;
                                    row.Discount = (request.Discount != 0) ? request.Discount : 0;
                                    row.Total = (request.Total != 0) ? request.Total : 0;

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
                            else
                            {

                                checkExits.Code = !string.IsNullOrEmpty(request.Code) ? request.Code.Trim() : "";
                                checkExits.Name = !string.IsNullOrEmpty(request.Name) ? request.Name.Trim() : "";
                                checkExits.DateUp = !string.IsNullOrEmpty(request.DateUpString.ToString()) ? DateTime.Parse(request.DateUpString) : checkExits.DateUp;
                                checkExits.DateDown = !string.IsNullOrEmpty(request.DateDown.ToString()) ? DateTime.Parse(request.DateDownString) : checkExits.DateDown;
                                checkExits.UnitPrice = (request.UnitPrice != 0) ? request.UnitPrice : 0;
                                checkExits.Discount = (request.Discount != 0) ? request.Discount : 0;
                                checkExits.Total = (request.Total != 0) ? request.Total : 0;
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
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }
    }
    public class CRM_Contract_Product_Packet_Draff
    {
        [AutoIncrement]
        public Int64 PKPacket { get; set; }
        public Int64 FKContract { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime DateUp { get; set; }
        public DateTime DateDown { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }

        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        [Ignore]
        public string DateUpString { get; set; }
        [Ignore]
        public string DateDownString { get; set; }
        public static object Save(IEnumerable<CRM_Contract_Product_Packet_Draff> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Contract_Product_Packet_Draff>("PKPacket= {0}", request.PKPacket);
                            if (checkExits == null)
                            {
                                if (request.FKContract != 0)
                                {
                                    var row = new CRM_Contract_Product_Packet_Draff();
                                    row.FKContract = (request.FKContract != 0) ? request.FKContract : 0;
                                    row.Code = !string.IsNullOrEmpty(request.Code) ? request.Code.Trim() : "";
                                    row.Name = !string.IsNullOrEmpty(request.Name) ? request.Name.Trim() : "";
                                    row.Type = "GOI";
                                    row.DateUp = !string.IsNullOrEmpty(request.DateUpString) ? DateTime.Parse(request.DateUpString) : DateTime.Parse("1900-01-01");
                                    row.DateDown = !string.IsNullOrEmpty(request.DateDownString.ToString()) ? DateTime.Parse(request.DateDownString) : DateTime.Parse("1900-01-01");
                                    row.UnitPrice = (request.UnitPrice != 0) ? request.UnitPrice : 0;
                                    row.Discount = (request.Discount != 0) ? request.Discount : 0;
                                    row.Total = (request.Total != 0) ? request.Total : 0;

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
                            else
                            {

                                checkExits.Code = !string.IsNullOrEmpty(request.Code) ? request.Code.Trim() : "";
                                checkExits.Name = !string.IsNullOrEmpty(request.Name) ? request.Name.Trim() : "";
                                checkExits.DateUp = !string.IsNullOrEmpty(request.DateUpString.ToString()) ? DateTime.Parse(request.DateUpString) : checkExits.DateUp;
                                checkExits.DateDown = !string.IsNullOrEmpty(request.DateDown.ToString()) ? DateTime.Parse(request.DateDownString) : checkExits.DateDown;
                                checkExits.UnitPrice = (request.UnitPrice != 0) ? request.UnitPrice : 0;
                                checkExits.Discount = (request.Discount != 0) ? request.Discount : 0;
                                checkExits.Total = (request.Total != 0) ? request.Total : 0;
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
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }
    }
    public class CRM_Contract_Time_Draff
    {
        [AutoIncrement]
        public Int64 PKTime { get; set; }
        public DateTime DateUp { get; set; }
        public DateTime DateDown { get; set; }
        public string Week { get; set; }
        public int NumberDay { get; set; }
        public float Discount1 { get; set; }
        public float Discount2 { get; set; }
        public float Discount3 { get; set; }
        public float Money { get; set; }
        public string Promotion { get; set; }
        public int FKProduct { get; set; }
        public int FKContract { get; set; }
        public int Stt { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }

    }
    public class CRM_Contract_ListTime_Draff
    {
        [AutoIncrement]
        public int PKTime { get; set; }
        public Int64 FKProduct { get; set; }
        public int FKContract { get; set; }
        public DateTime DateUp { get; set; }
        public DateTime DateDown { get; set; }
        public string Week { get; set; }
        public int NumberDay { get; set; }
        public int TotalAmtNoVAT { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        [Ignore]
        public string NgayLen { get; set; }
        [Ignore]
        public string NgayXuong { get; set; }
        public static object Save(IEnumerable<CRM_Contract_ListTime_Draff> listItem, string username, Int64 FKProduct)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Contract_ListTime_Draff>("PKTime= {0}", request.PKTime);
                            if (checkExits == null)
                            {
                                if (FKProduct != 0)
                                {
                                    var row = new CRM_Contract_ListTime_Draff();
                                    row.FKProduct = FKProduct;
                                    row.FKContract = (request.FKContract != 0) ? request.FKContract : 0;
                                    row.DateUp = !string.IsNullOrEmpty(request.NgayLen) ? DateTime.Parse(request.NgayLen) : DateTime.Parse("1900-01-01");
                                    row.DateDown = !string.IsNullOrEmpty(request.NgayXuong) ? DateTime.Parse(request.NgayXuong) : DateTime.Parse("1900-01-01");
                                    row.Week = !string.IsNullOrEmpty(request.Week) ? request.Week.Trim() : "";
                                    row.NumberDay = (request.NumberDay != 0) ? request.NumberDay : 0;
                                    row.TotalAmtNoVAT = (request.TotalAmtNoVAT != 0) ? request.TotalAmtNoVAT : 0;

                                    row.RowCreatedUser = username;
                                    row.RowUpdatedUser = "";
                                    row.RowCreatedAt = DateTime.Now;
                                    row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                                    dbConn.Insert(row);
                                }
                                else
                                {
                                    return new { success = false, message = "Chưa có FKProduct!" };
                                }
                            }
                            else
                            {
                                checkExits.DateUp = !string.IsNullOrEmpty(request.NgayLen.ToString()) ? DateTime.Parse(request.NgayLen) : DateTime.Parse("1900-01-01");
                                checkExits.DateDown = !string.IsNullOrEmpty(request.NgayXuong.ToString()) ? DateTime.Parse(request.NgayXuong) : DateTime.Parse("1900-01-01");
                                checkExits.Week = !string.IsNullOrEmpty(request.Week) ? request.Week.Trim() : "";
                                checkExits.NumberDay = (request.NumberDay != 0) ? request.NumberDay : 0;
                                checkExits.TotalAmtNoVAT = (request.TotalAmtNoVAT != 0) ? request.TotalAmtNoVAT : 0;

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
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }

    }
    public class CRM_Contract_TotalAmount_Product_Draff
    {
        public float TotalAmtNoVAT { get; set; }
    }
    public class CRM_Contract_ListPromotionProduct_Draff
    {
        [AutoIncrement]
        public int PKPromotion { get; set; }
        public Int64 FKProduct { get; set; }
        public int FKContract { get; set; }
        public float Discount { get; set; }
        public string Promotion { get; set; }
        public double AmtNoVAT { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }

        public static object Save(IEnumerable<CRM_Contract_ListPromotionProduct_Draff> listItem, string username, Int64 FKProduct)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Contract_ListPromotionProduct_Draff>("PKPromotion= {0}", request.PKPromotion);
                            if (checkExits == null)
                            {
                                if (FKProduct != 0)
                                {
                                    var row = new CRM_Contract_ListPromotionProduct_Draff();
                                    row.FKProduct = FKProduct;
                                    row.FKContract = (request.FKContract != 0) ? request.FKContract : 0;
                                    row.Promotion = !string.IsNullOrEmpty(request.Promotion) ? request.Promotion.Trim() : "";
                                    row.Discount = (request.Discount != 0) ? request.Discount : 0;
                                    row.AmtNoVAT = (request.AmtNoVAT != 0) ? request.AmtNoVAT : 0;

                                    row.RowCreatedUser = username;
                                    row.RowUpdatedUser = "";
                                    row.RowCreatedAt = DateTime.Now;
                                    row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                                    dbConn.Insert(row);
                                }
                                else
                                {
                                    return new { success = false, message = "Chưa có FKProduct!" };
                                }
                            }
                            else
                            {
                                checkExits.Promotion = !string.IsNullOrEmpty(request.Promotion) ? request.Promotion.Trim() : "";
                                checkExits.Discount = (request.Discount != 0) ? request.Discount : 0;
                                checkExits.AmtNoVAT = (request.AmtNoVAT != 0) ? request.AmtNoVAT : 0;

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
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }

    }
    public class CRM_Contract_Draff_Promotion
    {
        [AutoIncrement]
        public int ID { get; set; }
        public int FKContract { get; set; }
        public double ChietKhauChung { get; set; }
        public double TienMatChung { get; set; }
        public double TTSauCKChung { get; set; }
        public string GhiChu { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        [Ignore]
        public int SumDiscount { get; set; }

        public static object Save(IEnumerable<CRM_Contract_Draff_Promotion> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Contract_Draff_Promotion>("ID= {0}", request.ID);
                            if (checkExits == null)
                            {
                                if (request.FKContract != 0)
                                {
                                    var row = new CRM_Contract_Draff_Promotion();
                                    row.FKContract = (request.FKContract != 0) ? request.FKContract : 0;
                                    row.ChietKhauChung = (request.ChietKhauChung != 0) ? request.ChietKhauChung : 0;
                                    row.TienMatChung = (request.TienMatChung != 0) ? request.TienMatChung : 0;
                                    row.TTSauCKChung = (request.TTSauCKChung != 0) ? request.TTSauCKChung : 0;
                                    row.GhiChu = !string.IsNullOrEmpty(request.GhiChu) ? request.GhiChu.Trim() : "";

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
                            else
                            {
                                checkExits.ChietKhauChung = (request.ChietKhauChung != 0) ? request.ChietKhauChung : 0;
                                checkExits.TienMatChung = (request.TienMatChung != 0) ? request.TienMatChung : 0;
                                checkExits.TTSauCKChung = (request.TTSauCKChung != 0) ? request.TTSauCKChung : 0;
                                checkExits.GhiChu = !string.IsNullOrEmpty(request.GhiChu) ? request.GhiChu.Trim() : "";

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
                {
                    return new { success = false, message = "Data is null!" };
                }
            }

        }
    }
    public class CRM_Contract_Draff_QuickSort
    {
        [AutoIncrement]
        public string TAT_CA { get; set; }
        public string DANG_SOAN { get; set; }
        public string CHO_DUYET { get; set; }
        public string DA_DUYET { get; set; }
        public string DA_DONG_BO { get; set; }
        public string TU_CHOI { get; set; }
        public string HUY_DIEU_CHINH { get; set; }
        public string DA_XOA { get; set; }
        public static CRM_Contract_Draff_QuickSort CountStatus(string UserID,string TypeName)
        {
            System.Data.SqlClient.SqlParameter[] parameters = new SqlParameter[2];
            int y= 0;
            parameters[y] = new SqlParameter();
            parameters[y].ParameterName = "@UserName";
            parameters[y].Value = UserID;
            y++;
            parameters[y] = new SqlParameter();
            parameters[y].ParameterName = "@TypeName";
            parameters[y].Value = TypeName;

            System.Data.DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,
                System.Data.CommandType.StoredProcedure, "p_select_count_contract_draff_by_status", parameters);
            var lst = new CRM_Contract_Draff_QuickSort[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                var item = new CRM_Contract_Draff_QuickSort();
                item.DANG_SOAN = int.Parse(row["DANG_SOAN"].ToString()).ToString("#,##0");
                item.CHO_DUYET = int.Parse(row["CHO_DUYET"].ToString()).ToString("#,##0");
                item.DA_DUYET = int.Parse(row["DA_DUYET"].ToString()).ToString("#,##0");
                item.DA_DONG_BO = int.Parse(row["DA_DONG_BO"].ToString()).ToString("#,##0");
                item.TU_CHOI = int.Parse(row["TU_CHOI"].ToString()).ToString("#,##0");
                item.HUY_DIEU_CHINH = int.Parse(row["HUY_DIEU_CHINH"].ToString()).ToString("#,##0");
                item.DA_XOA = int.Parse(row["DA_XOA"].ToString()).ToString("#,##0");
                item.TAT_CA = (int.Parse(row["DANG_SOAN"].ToString()) + int.Parse(row["CHO_DUYET"].ToString()) + int.Parse(row["DA_DONG_BO"].ToString()) +
                    int.Parse(row["DA_DUYET"].ToString()) + int.Parse(row["TU_CHOI"].ToString()) + int.Parse(row["HUY_DIEU_CHINH"].ToString()) +
                    int.Parse(row["DA_XOA"].ToString())).ToString("#,##0");
                lst[i] = item;
                i++;
            }
            return lst.FirstOrDefault();
        }

    }
    public class CRM_Contract_QuickSort
    {
        [AutoIncrement]
        public string TAT_CA { get; set; }
        public string SAP_HET_HAN { get; set; }
        public string SAP_HET_TIEN { get; set; }

        public static CRM_Contract_QuickSort CountStatus(string UserID,string TypeName)
        {
            System.Data.SqlClient.SqlParameter[] parameters = new SqlParameter[2];
            int y = 0;
            parameters[y] = new SqlParameter();
            parameters[y].ParameterName = "@UserName";
            parameters[y].Value = UserID;
            y++;
            parameters[y] = new SqlParameter();
            parameters[y].ParameterName = "@TypeName";
            parameters[y].Value = TypeName;

            System.Data.DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, "p_select_count_contract_by_payment", parameters);
            var lst = new CRM_Contract_QuickSort[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                var item = new CRM_Contract_QuickSort();
                item.SAP_HET_HAN = int.Parse(row["SAP_HET_HAN"].ToString()).ToString("#,##0");
                item.SAP_HET_TIEN = int.Parse(row["SAP_HET_TIEN"].ToString()).ToString("#,##0");
                item.TAT_CA = int.Parse(row["TAT_CA"].ToString()).ToString("#,##0");
                lst[i] = item;
                i++;
            }
            return lst.FirstOrDefault();
        }

    }
    public class CRM_Contract_History_Action
    {
        public enum Action2
        {
            GUI,
            DUYET,
            TUCHOI,
            DONGBO,
            DIEUCHINH,
            DANGSOAN,
            HUYDB
        };
        [AutoIncrement]
        public int ID { get; set; }
        public int ContractID { get; set; }
        public string Action { get; set; }
        public int UserRequest { get; set; }
        public DateTime RequestAt { get; set; }
        public int UserApproved { get; set; }
        public DateTime ApprovedAt { get; set; }
        public string Reason { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FileGuiDuyet { get; set; }
        public string CreatedUser { get; set; }
        [Ignore]
        public string UserNameApprover { get; set; }
        [Ignore]
        public string UserNameRequester { get; set; }
        [Ignore]
        public string EmailRequester { get; set; }
        [Ignore]
        public string StatusName { get; set; }


        public static object write(int ContractID, string Action, int UserRequest,
            DateTime RequestAt, int UserApproved, DateTime ApprovedAt, string Reason, string Note, string userCreate, string FileGuiDuyet)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    var item = new CRM_Contract_History_Action();
                    item.ContractID = ContractID;
                    item.Action = Action;
                    item.UserRequest = UserRequest;
                    item.RequestAt = RequestAt;
                    item.UserApproved = UserApproved;
                    item.ApprovedAt = ApprovedAt;
                    item.Reason = Reason;
                    item.Note = Note;
                    item.CreatedAt = DateTime.Now;
                    item.CreatedUser = userCreate;
                    item.FileGuiDuyet = FileGuiDuyet;
                    dbConn.Insert(item);
                }
                return new { success = true };
            }
            catch (Exception e)
            {
                return new { success = false, message = e.Message };
            }
        }
    }
    public class CRM_Contract_Select
    {
        public int pk_contract { get; set; }
        public double c_ds_huy { get; set; }
        public int c_phan_tram_phan_bo { get; set; }
        public int c_week { get; set; } //tuan 
        public int c_year { get; set; } // thang
        public int c_month { get; set; } // thang
        public string c_contract_code { get; set; } // Ma hop dong
        public string c_book_code { get; set; } //Ma Book   
        public string c_customer_code { get; set; } // Ma KH
        public string c_customer_name { get; set; } //Ten KH
        public string c_labels { get; set; } //Nhan hang
        public int c_regionid { get; set; }
        public int c_teamid { get; set; }
        public int c_staffid { get; set; }
        public string c_staff_name { get; set; }
        public double c_total_money { get; set; }
        public double c_total_vat { get; set; }
        public double c_total_value { get; set; }
        public double c_total_money_in_year { get; set; }
        public double c_total_money_next_year { get; set; }
        public double c_tien_khong_tinh { get; set; }
        public double c_get_money_next_week { get; set; }
        public double c_commission { get; set; }
        public string la_dai_ly { get; set; }
        public string dang_quang_cao { get; set; }
        public string c_status { get; set; } //Trang thai
        public double c_dt_da_qc_den_hom_nay { get; set; }
        public double c_dt_da_xuat_ban { get; set; }
        public double c_payment_money { get; set; } // Tien da thu
        public double c_balance { get; set; } // Cong no
        public string c_status_name { get; set; }
        public double chi_phi_marketing_da_tra { get; set; }
        public double chi_phi_marketing_chua_tra { get; set; }
        public double tien_xuat_hoa_don { get; set; }
        public double tien_chua_xuat_hoa_don { get; set; }
        public string ghi_chu_dang_qc { get; set; }
        public DateTime c_payment_date { get; set; }
        public DateTime c_ngay_ve_hai_chieu { get; set; }
        public DateTime c_ngay_len_xuong_thuc_te { get; set; }
        public string c_filter { get; set; }

    }

    public class CRM_Real_Update
    {
        [AutoIncrement]
        public int pk_update { get; set; }
        public int fk_contract { get; set; }
        public DateTime c_real_update { get; set; }
        public DateTime c_real_downdate { get; set; }
        public string c_xml_data { get; set; }
        public string c_time { get; set; }
        public string c_note { get; set; }
        public string c_location { get; set; }
        public string c_category { get; set; }
        public string c_website { get; set; }
        public string c_nature { get; set; }
        public string c_size { get; set; }
        public string c_km { get; set; }
        public int c_staff_id { get; set; }
        public Int64 c_don_gia { get; set; }
        public int c_ngay_bu { get; set; }
        public int c_ocm_id { get; set; }
        public string c_link_counter { get; set; }
        public DateTime c_input_date { get; set; }
        public int fk_book_req { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }
        [Ignore]
        public string WebsiteName { get; set; }
        [Ignore]
        public string CategoryName { get; set; }
        [Ignore]
        public string LocationName { get; set; }
        [Ignore]
        public string NatureName { get; set; }
        [Ignore]
        public string EmployerName { get; set; }
        [Ignore]
        public string StrRealUpdate { get; set; }
        [Ignore]
        public string StrRealDowndate { get; set; }
        [Ignore]
        public int c_days { get; set; }
        [Ignore]
        public int c_km_days { get; set; }
        public static object Save(IEnumerable<CRM_Real_Update> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Real_Update>("pk_update= {0}", request.pk_update);
                            if (checkExits == null)
                            {
                                if (request.fk_contract != 0)
                                {
                                    var row = new CRM_Real_Update();
                                    row.fk_contract = (request.fk_contract != 0) ? request.fk_contract : 0;
                                    row.c_website = !string.IsNullOrEmpty(request.c_website) ? request.c_website.Trim() : "";
                                    row.c_location = !string.IsNullOrEmpty(request.c_location) ? request.c_location.Trim() : "";
                                    row.c_category = !string.IsNullOrEmpty(request.c_category) ? request.c_category.Trim() : "";
                                    row.c_xml_data = !string.IsNullOrEmpty(request.c_xml_data) ? request.c_xml_data.Trim() : "";
                                    row.c_nature = !string.IsNullOrEmpty(request.c_nature) ? request.c_nature.Trim() : "";
                                    row.c_real_update = !string.IsNullOrEmpty(request.StrRealUpdate) ? DateTime.Parse(request.StrRealUpdate) : DateTime.Parse("1900-01-01");
                                    row.c_real_downdate = !string.IsNullOrEmpty(request.StrRealDowndate) ? DateTime.Parse(request.StrRealDowndate) : DateTime.Parse("1900-01-01");
                                    row.c_xml_data = !string.IsNullOrEmpty(request.c_xml_data) ? request.c_xml_data.Trim() : "";
                                    row.c_note = !string.IsNullOrEmpty(request.c_note) ? request.c_note.Trim() : "";
                                    row.c_size = !string.IsNullOrEmpty(request.c_size) ? request.c_size.Trim() : "";
                                    row.c_km = !string.IsNullOrEmpty(request.c_km) ? request.c_km.Trim() : "";
                                    row.c_staff_id = (request.c_staff_id != 0) ? request.c_staff_id : 0;
                                    row.c_don_gia = (request.c_don_gia != 0) ? request.c_don_gia : 0;
                                    row.c_ngay_bu = (request.c_ngay_bu != 0) ? request.c_ngay_bu : 0;
                                    row.c_ocm_id = (request.c_ocm_id != 0) ? request.c_ocm_id : 0;
                                    row.c_link_counter = !string.IsNullOrEmpty(request.c_link_counter) ? request.c_link_counter.Trim() : "";
                                    row.c_time = !string.IsNullOrEmpty(request.c_time) ? request.c_time.Trim() : "";
                                    row.c_input_date = DateTime.Parse("1900-01-01");
                                    row.fk_book_req = (request.fk_book_req != 0) ? request.fk_book_req : 0;

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
                            else
                            {
                                checkExits.c_website = !string.IsNullOrEmpty(request.c_website) ? request.c_website.Trim() : "";
                                checkExits.c_location = !string.IsNullOrEmpty(request.c_location) ? request.c_location.Trim() : "";
                                checkExits.c_category = !string.IsNullOrEmpty(request.c_category) ? request.c_category.Trim() : "";
                                checkExits.c_xml_data = !string.IsNullOrEmpty(request.c_xml_data) ? request.c_xml_data.Trim() : "";
                                checkExits.c_nature = !string.IsNullOrEmpty(request.c_nature) ? request.c_nature.Trim() : "";
                                checkExits.c_real_update = !string.IsNullOrEmpty(request.StrRealUpdate) ? DateTime.Parse(request.StrRealUpdate) : checkExits.c_real_update;
                                checkExits.c_real_downdate = !string.IsNullOrEmpty(request.StrRealDowndate) ? DateTime.Parse(request.StrRealDowndate) : checkExits.c_real_downdate;
                                checkExits.c_xml_data = !string.IsNullOrEmpty(request.c_xml_data) ? request.c_xml_data.Trim() : "";
                                checkExits.c_note = !string.IsNullOrEmpty(request.c_note) ? request.c_note.Trim() : "";
                                checkExits.c_size = !string.IsNullOrEmpty(request.c_size) ? request.c_size.Trim() : "";
                                checkExits.c_km = !string.IsNullOrEmpty(request.c_km) ? request.c_km.Trim() : "";
                                checkExits.c_staff_id = (request.c_staff_id != 0) ? request.c_staff_id : 0;
                                checkExits.c_don_gia = (request.c_don_gia != 0) ? request.c_don_gia : 0;
                                checkExits.c_ngay_bu = (request.c_ngay_bu != 0) ? request.c_ngay_bu : 0;
                                checkExits.c_ocm_id = (request.c_ocm_id != 0) ? request.c_ocm_id : 0;
                                checkExits.c_link_counter = !string.IsNullOrEmpty(request.c_link_counter) ? request.c_link_counter.Trim() : "";
                                checkExits.c_time = !string.IsNullOrEmpty(request.c_time) ? request.c_time.Trim() : "";
                                checkExits.c_input_date = DateTime.Parse("1900-01-01");
                                checkExits.fk_book_req = (request.fk_book_req != 0) ? request.fk_book_req : 0;

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
                {
                    return new { success = true, message = "Data is null!" };
                }

            }

        }

    }

    public class CRM_Debit_Management
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
    public class CRM_Real_PRLocation
    {
        [AutoIncrement]
        public long PKPRLocation { get; set; }
        public int FKBookPR { get; set; }
        public long FKContract { get; set; }
        public string Website { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public DateTime NgayLen { get; set; }
        public string GhiChu { get; set; }
        public int VungMien { get; set; }
        public long IdOCM { get; set; }
        public int Status { get; set; }
        public float DonGia { get; set; }
        public int SoLuong { get; set; }
        public string PublishUser { get; set; }
        public int Km { get; set; }


        public DateTime PublishDate { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        [Ignore]
        public string WebsiteName { get; set; }
        [Ignore]
        public string CategoryName { get; set; }
        [Ignore]
        public string LocationName { get; set; }
        [Ignore]
        public string TenVungMien { get; set; }
        [Ignore]
        public string StrRealUpDate { get; set; }
        [Ignore]
        public string StrPublishDate { get; set; }
        public static object Save(IEnumerable<CRM_Real_PRLocation> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Real_PRLocation>("PKPRLocation= {0}", request.PKPRLocation);
                            if (checkExits == null)
                            {
                                if (request.FKContract != 0)
                                {
                                    var row = new CRM_Real_PRLocation();
                                    row.FKContract = (request.FKContract != 0) ? request.FKContract : 0;
                                    row.FKBookPR = (request.FKBookPR != 0) ? request.FKBookPR : 0;
                                    row.Website = !string.IsNullOrEmpty(request.Website) ? request.Website.Trim() : "";
                                    row.Location = !string.IsNullOrEmpty(request.Location) ? request.Location.Trim() : "";
                                    row.Category = !string.IsNullOrEmpty(request.Category) ? request.Category.Trim() : "";
                                    row.NgayLen = !string.IsNullOrEmpty(request.StrRealUpDate) ? DateTime.Parse(request.StrRealUpDate) : DateTime.Parse("1900-01-01");
                                    row.PublishDate = !string.IsNullOrEmpty(request.StrPublishDate) ? DateTime.Parse(request.StrPublishDate) : DateTime.Parse("1900-01-01");
                                    row.GhiChu = !string.IsNullOrEmpty(request.GhiChu) ? request.GhiChu.Trim() : "";
                                    row.VungMien = request.VungMien;
                                    row.Km = (request.Km != 0) ? request.Km : 0;
                                    row.DonGia = (request.DonGia != 0) ? request.DonGia : 0;
                                    row.SoLuong = (request.SoLuong != 0) ? request.SoLuong : 0;
                                    row.IdOCM = (request.IdOCM != 0) ? request.IdOCM : 0;
                                    row.Status = (request.Status != 0) ? request.Status : 0;
                                    row.PublishUser = !string.IsNullOrEmpty(request.PublishUser) ? request.PublishUser.Trim() : "";

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
                            else
                            {
                                checkExits.FKBookPR = (request.FKBookPR != 0) ? request.FKBookPR : 0;
                                checkExits.Website = !string.IsNullOrEmpty(request.Website) ? request.Website.Trim() : "";
                                checkExits.Location = !string.IsNullOrEmpty(request.Location) ? request.Location.Trim() : "";
                                checkExits.Category = !string.IsNullOrEmpty(request.Category) ? request.Category.Trim() : "";
                                checkExits.NgayLen = !string.IsNullOrEmpty(request.StrRealUpDate) ? DateTime.Parse(request.StrRealUpDate) : checkExits.NgayLen;
                                checkExits.PublishDate = !string.IsNullOrEmpty(request.StrPublishDate) ? DateTime.Parse(request.StrPublishDate) : checkExits.PublishDate;
                                checkExits.GhiChu = !string.IsNullOrEmpty(request.GhiChu) ? request.GhiChu.Trim() : "";
                                checkExits.Km = (request.Km != 0) ? request.Km : 0;
                                checkExits.DonGia = (request.DonGia != 0) ? request.DonGia : 0;
                                checkExits.SoLuong = (request.SoLuong != 0) ? request.SoLuong : 0;
                                checkExits.IdOCM = (request.IdOCM != 0) ? request.IdOCM : 0;
                                checkExits.Status = (request.Status != 0) ? request.Status : 0;
                                checkExits.PublishUser = !string.IsNullOrEmpty(request.PublishUser) ? request.PublishUser.Trim() : "";

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

                {
                    //BaoHV Edit success = true
                    return new { success = true, message = "Data is null!" };
                }

            }

        }

    }


    public class CRM_SuggestContract
    {
        public string c_code { get; set; }
        public long c_total_money { get; set; }
        public string CustomerName { get; set; }
        public string CustomerType { get; set; }
        public string CategoryName { get; set; }
        public string c_book_code { get; set; }
        public string c_labels { get; set; }
        public string c_contract_type { get; set; }
        
    }
    public class CRM_Contract_Draff_AdditionalList
    {
        [AutoIncrement]
        public Int32 IDRow { get; set; }
        public Int32 FKContractDraft { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string BookCode { get; set; }
        public string Code { get; set; }
        public string Deliver { get; set; }
        public string Team { get; set; }
        public string Category { get; set; }
        public string Labels { get; set; }

        public string Staff01 { get; set; }
        public string Staff02 { get; set; }
        public string Staff03 { get; set; }
        public int Percent01 { get; set; }
        public int Percent02 { get; set; }
        public int Percent03 { get; set; }

        public double DSSign { get; set; }
        public double DSSign01 { get; set; }
        public double DSSign02 { get; set; }
        public double DSSign03 { get; set; }

        public double DSReal { get; set; }
        public double DSReal01 { get; set; }
        public double DSReal02 { get; set; }
        public double DSReal03 { get; set; }

        public double DSNextYear { get; set; }
        public double DSNextYear01 { get; set; }
        public double DSNextYear02 { get; set; }
        public double DSNextYear03 { get; set; }

        public double DSOther { get; set; }
        public double DSOther01 { get; set; }
        public double DSOther02 { get; set; }
        public double DSOther03 { get; set; }

        public double MoneyDiscountOut { get; set; }
        public string ToTalValueAdd { get; set; }

        public double MKT { get; set; }
        public double MKTValueItem { get; set; }
        public double MKTReal { get; set; }
        public double MKTNextYear { get; set; }
        public double MKTOther { get; set; }

        public double MKTO { get; set; }
        public double MKTOValueItem { get; set; }
        public double MKTOReal { get; set; }
        public double MKTONextYear { get; set; }
        public double MKTOOther { get; set; }

        public double MKTSum { get; set; }
        public double MKTSumValueItem { get; set; }
        public double MKTSumReal { get; set; }
        public double MKTSumNextYear { get; set; }
        public double MKTSumOther { get; set; }

        public string Receiver { get; set; }
        public string AgentRe { get; set; }
        public string CoPhoneRe { get; set; }
        public string PhoneRe { get; set; }
        public string BankNumber { get; set; }
        public string BankName { get; set; }

        public string CustomerContact { get; set; }
        public string AgentCus { get; set; }
        public string PhoneCus { get; set; }
        public string InforOther { get; set; }
        public string StaffSign { get; set; }
        public string StaffTeamSign { get; set; }
        public string StaffLeadSign { get; set; }

        public string PacketSale { get; set; }
        public string AgencyType { get; set; }
        public string DateRevenus { get; set; }
        public int DiscountOut { get; set; }
        public int DiscountOn { get; set; }
        public int HasOwe { get; set; }
        public int CustomerType { get; set; }
        public int Status { get; set; }

        public DateTime RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        [Ignore]
        public string StrDSSign { get; set; }
        [Ignore]
        public string StrDSSign01 { get; set; }
        [Ignore]
        public string StrDSSign02 { get; set; }
        [Ignore]
        public string StrDSSign03 { get; set; }
        [Ignore]
        public string StrDSReal { get; set; }
        [Ignore]
        public string StrDSReal01 { get; set; }
        [Ignore]
        public string StrDSReal02 { get; set; }
        [Ignore]
        public string StrDSReal03 { get; set; }
        [Ignore]
        public string StrDSNextYear { get; set; }
        [Ignore]
        public string StrDSNextYear01 { get; set; }
        [Ignore]
        public string StrDSNextYear02 { get; set; }
        [Ignore]
        public string StrDSNextYear03 { get; set; }
        [Ignore]
        public string StrDSOther { get; set; }
        [Ignore]
        public string StrDSOther01 { get; set; }
        [Ignore]
        public string StrDSOther02 { get; set; }
        [Ignore]
        public string StrDSOther03 { get; set; }
        [Ignore]
        public string StrMKT { get; set; }
        [Ignore]
        public string StrMKTValueItem { get; set; }
        [Ignore]
        public string StrMKTReal { get; set; }
        [Ignore]
        public string StrMKTNextYear { get; set; }
        [Ignore]
        public string StrMKTOther { get; set; }
        [Ignore]
        public string StrMKTO { get; set; }
        [Ignore]
        public string StrMKTOValueItem { get; set; }
        [Ignore]
        public string StrMKTOReal { get; set; }
        [Ignore]
        public string StrMKTONextYear { get; set; }
        [Ignore]
        public string StrMKTOOther { get; set; }
        [Ignore]
        public string StrMKTSum { get; set; }
        [Ignore]
        public string StrMKTSumValueItem { get; set; }
        [Ignore]
        public string StrMKTSumReal { get; set; }
        [Ignore]
        public string StrMKTSumNextYear { get; set; }
        [Ignore]
        public string StrMKTSumOther { get; set; }
        [Ignore]
        public string StrMoneyDiscountOut { get; set; }
        public static object Save(CRM_Contract_Draff_AdditionalList item, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (item != null)
                {
                    try
                    {

                        var checkExits = dbConn.SingleOrDefault<CRM_Contract_Draff_AdditionalList>("FKContractDraft= {0}", item.FKContractDraft);
                        if (checkExits == null)
                        {
                            var row = new CRM_Contract_Draff_AdditionalList();

                            row.FKContractDraft = (item.FKContractDraft != 0) ? item.FKContractDraft : 0;
                            row.CustomerName = !string.IsNullOrEmpty(item.CustomerName) ? item.CustomerName.Trim() : "";
                            row.CustomerCode = !string.IsNullOrEmpty(item.CustomerCode) ? item.CustomerCode.Trim() : "";
                            row.BookCode = !string.IsNullOrEmpty(item.BookCode) ? item.BookCode.Trim() : "";
                            row.Code = !string.IsNullOrEmpty(item.Code) ? item.Code.Trim() : "";
                            row.Deliver = !string.IsNullOrEmpty(item.Deliver) ? item.Deliver.Trim() : "";
                            row.Team = !string.IsNullOrEmpty(item.Team) ? item.Team.Trim() : "";
                            row.Category = !string.IsNullOrEmpty(item.Category) ? item.Category.Trim() : "";
                            row.Labels = !string.IsNullOrEmpty(item.Labels) ? item.Labels.Trim() : "";
                            row.Staff01 = !string.IsNullOrEmpty(item.Staff01) ? item.Staff01.Trim() : "";
                            row.Staff02 = !string.IsNullOrEmpty(item.Staff02) ? item.Staff02.Trim() : "";
                            row.Staff03 = !string.IsNullOrEmpty(item.Staff03) ? item.Staff03.Trim() : "";

                            row.Percent01 = (item.Percent01 != 0) ? item.Percent01 : 0;
                            row.Percent02 = (item.Percent02 != 0) ? item.Percent02 : 0;
                            row.Percent03 = (item.Percent03 != 0) ? item.Percent03 : 0;

                            row.DSSign = (convertNumber.currencyToNumber(item.StrDSSign) != 0) ? convertNumber.currencyToNumber(item.StrDSSign) : 0;
                            row.DSSign01 = (convertNumber.currencyToNumber(item.StrDSSign01) != 0) ? convertNumber.currencyToNumber(item.StrDSSign01) : 0;
                            row.DSSign02 = (convertNumber.currencyToNumber(item.StrDSSign02) != 0) ? convertNumber.currencyToNumber(item.StrDSSign02) : 0;
                            row.DSSign03 = (convertNumber.currencyToNumber(item.StrDSSign03) != 0) ? convertNumber.currencyToNumber(item.StrDSSign03) : 0;

                            row.DSReal = (convertNumber.currencyToNumber(item.StrDSReal) != 0) ? convertNumber.currencyToNumber(item.StrDSReal) : 0;
                            row.DSReal01 = (convertNumber.currencyToNumber(item.StrDSReal01) != 0) ? convertNumber.currencyToNumber(item.StrDSReal01) : 0;
                            row.DSReal02 = (convertNumber.currencyToNumber(item.StrDSReal02) != 0) ? convertNumber.currencyToNumber(item.StrDSReal02) : 0;
                            row.DSReal03 = (convertNumber.currencyToNumber(item.StrDSReal03) != 0) ? convertNumber.currencyToNumber(item.StrDSReal03) : 0;

                            row.DSNextYear = (convertNumber.currencyToNumber(item.StrDSNextYear) != 0) ? convertNumber.currencyToNumber(item.StrDSNextYear) : 0;
                            row.DSNextYear01 = (convertNumber.currencyToNumber(item.StrDSNextYear01) != 0) ? convertNumber.currencyToNumber(item.StrDSNextYear01) : 0;
                            row.DSNextYear02 = (convertNumber.currencyToNumber(item.StrDSNextYear02) != 0) ? convertNumber.currencyToNumber(item.StrDSNextYear02) : 0;
                            row.DSNextYear03 = (convertNumber.currencyToNumber(item.StrDSNextYear03) != 0) ? convertNumber.currencyToNumber(item.StrDSNextYear03) : 0;

                            row.DSOther = (convertNumber.currencyToNumber(item.StrDSOther) != 0) ? convertNumber.currencyToNumber(item.StrDSOther) : 0;
                            row.DSOther01 = (convertNumber.currencyToNumber(item.StrDSOther01) != 0) ? convertNumber.currencyToNumber(item.StrDSOther01) : 0;
                            row.DSOther02 = (convertNumber.currencyToNumber(item.StrDSOther02) != 0) ? convertNumber.currencyToNumber(item.StrDSOther02) : 0;
                            row.DSOther03 = (convertNumber.currencyToNumber(item.StrDSOther03) != 0) ? convertNumber.currencyToNumber(item.StrDSOther03) : 0;

                            row.MoneyDiscountOut = (convertNumber.currencyToNumber(item.StrMoneyDiscountOut) != 0) ? convertNumber.currencyToNumber(item.StrMoneyDiscountOut) : 0;
                            row.ToTalValueAdd = !string.IsNullOrEmpty(item.ToTalValueAdd) ? item.ToTalValueAdd.Trim() : "";

                            row.MKT = (convertNumber.currencyToNumber(item.StrMKT) != 0) ? convertNumber.currencyToNumber(item.StrMKT) : 0;
                            row.MKTValueItem = (convertNumber.currencyToNumber(item.StrMKTValueItem) != 0) ? convertNumber.currencyToNumber(item.StrMKTValueItem) : 0;
                            row.MKTNextYear = (convertNumber.currencyToNumber(item.StrMKTNextYear) != 0) ? convertNumber.currencyToNumber(item.StrMKTNextYear) : 0;
                            row.MKTReal = (convertNumber.currencyToNumber(item.StrMKTReal) != 0) ? convertNumber.currencyToNumber(item.StrMKTReal) : 0;
                            row.MKTOther = (convertNumber.currencyToNumber(item.StrMKTOther) != 0) ? convertNumber.currencyToNumber(item.StrMKTOther) : 0;

                            row.MKTO = (convertNumber.currencyToNumber(item.StrMKTO) != 0) ? convertNumber.currencyToNumber(item.StrMKTO) : 0;
                            row.MKTOValueItem = (convertNumber.currencyToNumber(item.StrMKTOValueItem) != 0) ? convertNumber.currencyToNumber(item.StrMKTOValueItem) : 0;
                            row.MKTONextYear = (convertNumber.currencyToNumber(item.StrMKTONextYear) != 0) ? convertNumber.currencyToNumber(item.StrMKTONextYear) : 0;
                            row.MKTOReal = (convertNumber.currencyToNumber(item.StrMKTOReal) != 0) ? convertNumber.currencyToNumber(item.StrMKTOReal) : 0;
                            row.MKTOOther = (convertNumber.currencyToNumber(item.StrMKTOOther) != 0) ? convertNumber.currencyToNumber(item.StrMKTOOther) : 0;

                            row.MKTSum = (convertNumber.currencyToNumber(item.StrMKTSum) != 0) ? convertNumber.currencyToNumber(item.StrMKTSum) : 0;
                            row.MKTSumValueItem = (convertNumber.currencyToNumber(item.StrMKTSumValueItem) != 0) ? convertNumber.currencyToNumber(item.StrMKTSumValueItem) : 0;
                            row.MKTSumNextYear = (convertNumber.currencyToNumber(item.StrMKTSumNextYear) != 0) ? convertNumber.currencyToNumber(item.StrMKTSumNextYear) : 0;
                            row.MKTSumReal = (convertNumber.currencyToNumber(item.StrMKTSumReal) != 0) ? convertNumber.currencyToNumber(item.StrMKTSumReal) : 0;
                            row.MKTSumOther = (convertNumber.currencyToNumber(item.StrMKTSumOther) != 0) ? convertNumber.currencyToNumber(item.StrMKTSumOther) : 0;

                            row.Receiver = !string.IsNullOrEmpty(item.Receiver) ? item.Receiver.Trim() : "";
                            row.AgentRe = !string.IsNullOrEmpty(item.AgentRe) ? item.AgentRe.Trim() : "";
                            row.CoPhoneRe = !string.IsNullOrEmpty(item.CoPhoneRe) ? item.CoPhoneRe.Trim() : "";
                            row.PhoneRe = !string.IsNullOrEmpty(item.PhoneRe) ? item.PhoneRe.Trim() : "";
                            row.BankNumber = !string.IsNullOrEmpty(item.BankNumber) ? item.BankNumber.Trim() : "";
                            row.BankName = !string.IsNullOrEmpty(item.BankName) ? item.BankName.Trim() : "";
                            row.CustomerContact = !string.IsNullOrEmpty(item.CustomerContact) ? item.CustomerContact.Trim() : "";
                            row.AgentCus = !string.IsNullOrEmpty(item.AgentCus) ? item.AgentCus.Trim() : "";
                            row.PhoneCus = !string.IsNullOrEmpty(item.PhoneCus) ? item.PhoneCus.Trim() : "";
                            row.InforOther = !string.IsNullOrEmpty(item.InforOther) ? item.InforOther.Trim() : "";

                            row.StaffSign = !string.IsNullOrEmpty(item.StaffSign) ? item.StaffSign.Trim() : "";
                            row.StaffTeamSign = !string.IsNullOrEmpty(item.StaffTeamSign) ? item.StaffTeamSign.Trim() : "";
                            row.StaffLeadSign = !string.IsNullOrEmpty(item.StaffLeadSign) ? item.StaffLeadSign.Trim() : "";

                            row.PacketSale = !string.IsNullOrEmpty(item.PacketSale) ? item.PacketSale.Trim() : "";
                            row.AgencyType = !string.IsNullOrEmpty(item.AgencyType) ? item.AgencyType.Trim() : "";
                            row.DateRevenus = !string.IsNullOrEmpty(item.DateRevenus) ? item.DateRevenus.Trim() : "";
                            row.DiscountOut = (item.DiscountOut != 0) ? item.DiscountOut : 0;
                            row.DiscountOn = (item.DiscountOn != 0) ? item.DiscountOn : 0;
                            row.HasOwe = (item.HasOwe != 0) ? item.HasOwe : 0;
                            row.CustomerType = (item.CustomerType != 0) ? item.CustomerType : 0;
                            row.Status = 0;

                            row.RowCreatedUser = username;
                            row.RowUpdatedUser = "";
                            row.RowCreatedAt = DateTime.Now;
                            row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                            var user = dbConn.FirstOrDefault<EmployeeInfo>(p => p.UserName == username);

                            dbConn.Insert(row);
                            return new { success = true, message = "Save success!" };
                        }
                        else
                        {

                            checkExits.CustomerName = !string.IsNullOrEmpty(item.CustomerName) ? item.CustomerName.Trim() : "";
                            checkExits.CustomerCode = !string.IsNullOrEmpty(item.CustomerCode) ? item.CustomerCode.Trim() : "";
                            checkExits.BookCode = !string.IsNullOrEmpty(item.BookCode) ? item.BookCode.Trim() : "";
                            checkExits.Code = !string.IsNullOrEmpty(item.Code) ? item.Code.Trim() : "";
                            checkExits.Deliver = !string.IsNullOrEmpty(item.Deliver) ? item.Deliver.Trim() : "";
                            checkExits.Team = !string.IsNullOrEmpty(item.Team) ? item.Team.Trim() : "";
                            checkExits.Category = !string.IsNullOrEmpty(item.Category) ? item.Category.Trim() : "";
                            checkExits.Labels = !string.IsNullOrEmpty(item.Labels) ? item.Labels.Trim() : "";
                            checkExits.Staff01 = !string.IsNullOrEmpty(item.Staff01) ? item.Staff01.Trim() : "";
                            checkExits.Staff02 = !string.IsNullOrEmpty(item.Staff02) ? item.Staff02.Trim() : "";
                            checkExits.Staff03 = !string.IsNullOrEmpty(item.Staff03) ? item.Staff03.Trim() : "";

                            checkExits.Percent01 = (item.Percent01 != 0) ? item.Percent01 : 0;
                            checkExits.Percent02 = (item.Percent02 != 0) ? item.Percent02 : 0;
                            checkExits.Percent03 = (item.Percent03 != 0) ? item.Percent03 : 0;

                            checkExits.DSSign = (convertNumber.currencyToNumber(item.StrDSSign) != 0) ? convertNumber.currencyToNumber(item.StrDSSign) : 0;
                            checkExits.DSSign01 = (convertNumber.currencyToNumber(item.StrDSSign01) != 0) ? convertNumber.currencyToNumber(item.StrDSSign01) : 0;
                            checkExits.DSSign02 = (convertNumber.currencyToNumber(item.StrDSSign02) != 0) ? convertNumber.currencyToNumber(item.StrDSSign02) : 0;
                            checkExits.DSSign03 = (convertNumber.currencyToNumber(item.StrDSSign03) != 0) ? convertNumber.currencyToNumber(item.StrDSSign03) : 0;

                            checkExits.DSReal = (convertNumber.currencyToNumber(item.StrDSReal) != 0) ? convertNumber.currencyToNumber(item.StrDSReal) : 0;
                            checkExits.DSReal01 = (convertNumber.currencyToNumber(item.StrDSReal01) != 0) ? convertNumber.currencyToNumber(item.StrDSReal01) : 0;
                            checkExits.DSReal02 = (convertNumber.currencyToNumber(item.StrDSReal02) != 0) ? convertNumber.currencyToNumber(item.StrDSReal02) : 0;
                            checkExits.DSReal03 = (convertNumber.currencyToNumber(item.StrDSReal03) != 0) ? convertNumber.currencyToNumber(item.StrDSReal03) : 0;

                            checkExits.DSNextYear = (convertNumber.currencyToNumber(item.StrDSNextYear) != 0) ? convertNumber.currencyToNumber(item.StrDSNextYear) : 0;
                            checkExits.DSNextYear01 = (convertNumber.currencyToNumber(item.StrDSNextYear01) != 0) ? convertNumber.currencyToNumber(item.StrDSNextYear01) : 0;
                            checkExits.DSNextYear02 = (convertNumber.currencyToNumber(item.StrDSNextYear02) != 0) ? convertNumber.currencyToNumber(item.StrDSNextYear02) : 0;
                            checkExits.DSNextYear03 = (convertNumber.currencyToNumber(item.StrDSNextYear03) != 0) ? convertNumber.currencyToNumber(item.StrDSNextYear03) : 0;

                            checkExits.DSOther = (convertNumber.currencyToNumber(item.StrDSOther) != 0) ? convertNumber.currencyToNumber(item.StrDSOther) : 0;
                            checkExits.DSOther01 = (convertNumber.currencyToNumber(item.StrDSOther01) != 0) ? convertNumber.currencyToNumber(item.StrDSOther01) : 0;
                            checkExits.DSOther02 = (convertNumber.currencyToNumber(item.StrDSOther02) != 0) ? convertNumber.currencyToNumber(item.StrDSOther02) : 0;
                            checkExits.DSOther03 = (convertNumber.currencyToNumber(item.StrDSOther03) != 0) ? convertNumber.currencyToNumber(item.StrDSOther03) : 0;

                            checkExits.MoneyDiscountOut = (convertNumber.currencyToNumber(item.StrMoneyDiscountOut) != 0) ? convertNumber.currencyToNumber(item.StrMoneyDiscountOut) : 0;
                            checkExits.ToTalValueAdd = !string.IsNullOrEmpty(item.ToTalValueAdd) ? item.ToTalValueAdd.Trim() : "";

                            checkExits.MKT = (convertNumber.currencyToNumber(item.StrMKT) != 0) ? convertNumber.currencyToNumber(item.StrMKT) : 0;
                            checkExits.MKTValueItem = (convertNumber.currencyToNumber(item.StrMKTValueItem) != 0) ? convertNumber.currencyToNumber(item.StrMKTValueItem) : 0;
                            checkExits.MKTNextYear = (convertNumber.currencyToNumber(item.StrMKTNextYear) != 0) ? convertNumber.currencyToNumber(item.StrMKTNextYear) : 0;
                            checkExits.MKTReal = (convertNumber.currencyToNumber(item.StrMKTReal) != 0) ? convertNumber.currencyToNumber(item.StrMKTReal) : 0;
                            checkExits.MKTOther = (convertNumber.currencyToNumber(item.StrMKTOther) != 0) ? convertNumber.currencyToNumber(item.StrMKTOther) : 0;

                            checkExits.MKTO = (convertNumber.currencyToNumber(item.StrMKTO) != 0) ? convertNumber.currencyToNumber(item.StrMKTO) : 0;
                            checkExits.MKTOValueItem = (convertNumber.currencyToNumber(item.StrMKTOValueItem) != 0) ? convertNumber.currencyToNumber(item.StrMKTOValueItem) : 0;
                            checkExits.MKTONextYear = (convertNumber.currencyToNumber(item.StrMKTONextYear) != 0) ? convertNumber.currencyToNumber(item.StrMKTONextYear) : 0;
                            checkExits.MKTOReal = (convertNumber.currencyToNumber(item.StrMKTOReal) != 0) ? convertNumber.currencyToNumber(item.StrMKTOReal) : 0;
                            checkExits.MKTOOther = (convertNumber.currencyToNumber(item.StrMKTOOther) != 0) ? convertNumber.currencyToNumber(item.StrMKTOOther) : 0;

                            checkExits.MKTSum = (convertNumber.currencyToNumber(item.StrMKTSum) != 0) ? convertNumber.currencyToNumber(item.StrMKTSum) : 0;
                            checkExits.MKTSumValueItem = (convertNumber.currencyToNumber(item.StrMKTSumValueItem) != 0) ? convertNumber.currencyToNumber(item.StrMKTSumValueItem) : 0;
                            checkExits.MKTSumNextYear = (convertNumber.currencyToNumber(item.StrMKTSumNextYear) != 0) ? convertNumber.currencyToNumber(item.StrMKTSumNextYear) : 0;
                            checkExits.MKTSumReal = (convertNumber.currencyToNumber(item.StrMKTSumReal) != 0) ? convertNumber.currencyToNumber(item.StrMKTSumReal) : 0;
                            checkExits.MKTSumOther = (convertNumber.currencyToNumber(item.StrMKTSumOther) != 0) ? convertNumber.currencyToNumber(item.StrMKTSumOther) : 0;

                            checkExits.Receiver = !string.IsNullOrEmpty(item.Receiver) ? item.Receiver.Trim() : "";
                            checkExits.AgentRe = !string.IsNullOrEmpty(item.AgentRe) ? item.AgentRe.Trim() : "";
                            checkExits.CoPhoneRe = !string.IsNullOrEmpty(item.CoPhoneRe) ? item.CoPhoneRe.Trim() : "";
                            checkExits.PhoneRe = !string.IsNullOrEmpty(item.PhoneRe) ? item.PhoneRe.Trim() : "";
                            checkExits.BankNumber = !string.IsNullOrEmpty(item.BankNumber) ? item.BankNumber.Trim() : "";
                            checkExits.BankName = !string.IsNullOrEmpty(item.BankName) ? item.BankName.Trim() : "";
                            checkExits.CustomerContact = !string.IsNullOrEmpty(item.CustomerContact) ? item.CustomerContact.Trim() : "";
                            checkExits.AgentCus = !string.IsNullOrEmpty(item.AgentCus) ? item.AgentCus.Trim() : "";
                            checkExits.PhoneCus = !string.IsNullOrEmpty(item.PhoneCus) ? item.PhoneCus.Trim() : "";
                            checkExits.InforOther = !string.IsNullOrEmpty(item.InforOther) ? item.InforOther.Trim() : "";

                            checkExits.StaffSign = !string.IsNullOrEmpty(item.StaffSign) ? item.StaffSign.Trim() : "";
                            checkExits.StaffTeamSign = !string.IsNullOrEmpty(item.StaffTeamSign) ? item.StaffTeamSign.Trim() : "";
                            checkExits.StaffLeadSign = !string.IsNullOrEmpty(item.StaffLeadSign) ? item.StaffLeadSign.Trim() : "";

                            checkExits.PacketSale = !string.IsNullOrEmpty(item.PacketSale) ? item.PacketSale.Trim() : "";
                            checkExits.AgencyType = !string.IsNullOrEmpty(item.AgencyType) ? item.AgencyType.Trim() : "";
                            checkExits.DateRevenus = !string.IsNullOrEmpty(item.DateRevenus) ? item.DateRevenus.Trim() : "";
                            checkExits.DiscountOut = (item.DiscountOut != 0) ? item.DiscountOut : 0;
                            checkExits.DiscountOn = (item.DiscountOn != 0) ? item.DiscountOn : 0;
                            checkExits.HasOwe = (item.HasOwe != 0) ? item.HasOwe : 0;
                            checkExits.CustomerType = (item.CustomerType != 0) ? item.CustomerType : 0;

                            checkExits.StaffLeadSign = !string.IsNullOrEmpty(item.StaffLeadSign) ? item.StaffLeadSign.Trim() : "";
                            checkExits.RowUpdatedUser = username;
                            checkExits.RowUpdatedAt = DateTime.Now;

                            dbConn.Update(checkExits);
                            return new { success = true, message = "Save success!" };
                        }



                    }
                    catch (Exception e)
                    {
                        return new { success = false, message = e.Message };
                    }
                }
                else
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }
        public static object comfirm(Int64 PKContract, bool status)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var checkExits = dbConn.SingleOrDefault<CRM_Contract_Draff_AdditionalList>("FKContractDraft= {0}", PKContract);
                    if (checkExits != null)
                    {
                        checkExits.Status = (status) ? 1 : 0;
                        checkExits.RowUpdatedAt = DateTime.Now;
                        dbConn.Update(checkExits);
                        return new { success = true, message = "Save success!" };
                    }
                    else
                    {
                        return new { success = false, message = "NULL" };
                    }
                }
                catch (Exception e)
                {
                    return new { success = false, message = e.Message };

                }
            }
        }

    }
    public class CRM_Contract_Draff_Additional_Staff
    {
        [AutoIncrement]
        public Int64 PKStaff { get; set; }
        public Int64 FKContract { get; set; }
        public int UnitId { get; set; }
        public int StaffId { get; set; }
        public int GroupId { get; set; }
        public int Percent { get; set; }
        public double Money { get; set; }
        public double MoneyInYear { get; set; }
        public double MoneyWebOther { get; set; }
        public string Charge { get; set; }
        public double MoneyNextYear { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }

        [Ignore]
        public string TeamName { get; set; }
        [Ignore]
        public string FullName { get; set; }
        [Ignore]
        public string Province { get; set; }

        public static object Save(IEnumerable<CRM_Contract_Draff_Additional_Staff> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Contract_Draff_Additional_Staff>("PKStaff= {0}", request.PKStaff);
                            if (checkExits == null)
                            {
                                if (request.FKContract != 0)
                                {
                                    var row = new CRM_Contract_Draff_Additional_Staff();
                                    row.FKContract = (request.FKContract != 0) ? request.FKContract : 0;
                                    row.UnitId = (request.UnitId != 0) ? request.UnitId : 0;
                                    row.StaffId = (request.StaffId != 0) ? request.StaffId : 0;
                                    row.GroupId = (request.GroupId != 0) ? request.GroupId : 0;
                                    row.Percent = (request.Percent != 0) ? request.Percent : 0;
                                    row.Money = (request.Money != 0) ? request.Money : 0;
                                    row.MoneyInYear = (request.MoneyInYear != 0) ? request.MoneyInYear : 0;
                                    row.MoneyWebOther = (request.MoneyWebOther != 0) ? request.MoneyWebOther : 0;
                                    row.Charge = !string.IsNullOrEmpty(request.Charge) ? request.Charge.Trim() : "";
                                    row.MoneyNextYear = (request.MoneyNextYear != 0) ? request.MoneyNextYear : 0;

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
                            else
                            {
                                checkExits.UnitId = (request.UnitId != 0) ? request.UnitId : 0;
                                checkExits.StaffId = (request.StaffId != 0) ? request.StaffId : 0;
                                checkExits.GroupId = (request.GroupId != 0) ? request.GroupId : 0;
                                checkExits.Percent = (request.Percent != 0) ? request.Percent : 0;
                                checkExits.Money = (request.Money != 0) ? request.Money : 0;
                                checkExits.MoneyInYear = (request.MoneyInYear != 0) ? request.MoneyInYear : 0;
                                checkExits.MoneyWebOther = (request.MoneyWebOther != 0) ? request.MoneyWebOther : 0;
                                checkExits.Charge = !string.IsNullOrEmpty(request.Charge) ? request.Charge.Trim() : "";
                                checkExits.MoneyNextYear = (request.MoneyNextYear != 0) ? request.MoneyNextYear : 0;

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
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }
    }

}