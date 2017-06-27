using ERPAPD.Helpers;
using Kendo.Mvc.UI;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace ERPAPD.Models
{
    public class CRM_Adv
    {
        [AutoIncrement]
        public Int64 pk_adv { get; set; }
        public string c_code { get; set; }
        public int fk_staff { get; set; }
        public DateTime? c_input_date { get; set; }
        public int c_status { get; set; }
        public int fk_nguoi_duyet { get; set; }
        public DateTime? c_ngay_duyet { get; set; }
        public DateTime? c_ngay_yc_duyet { get; set; }
        public string c_noi_dung_duyet { get; set; }
        public double c_dinh_muc_bn { get; set; }
        public double c_dinh_muc_pr_goi { get; set; }
        public double c_dinh_muc_pr_thuong { get; set; }
        public string c_noi_dung_gui_duyet { get; set; }
        public string c_note { get; set; }
        public string c_noi_dung_xn_dang_dv { get; set; }
        public DateTime? c_from_date { get; set; }
        public DateTime? c_to_date { get; set; }
        public double c_dinh_muc_cpm { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }
        [Ignore]
        public string ActionSelected { get; set; }
        [Ignore]
        public int IsAprroved { get; set; }
        [Ignore]
        public string c_from_dateString { get; set; }
        [Ignore]
        public string c_to_dateString { get; set; }
        [Ignore]
        public int c_dot_order { get; set; }
        [Ignore]
        public string c_publisher { get; set; }
        [Ignore]
        public string c_book_code { get; set; }
        
        public static object Save(CRM_Adv item, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (item != null)
                {
                    try
                    {
                        var checkExits = dbConn.SingleOrDefault<CRM_Adv>("c_code= {0}", item.c_code); // check theo số hợp đồng
                        if (checkExits == null)
                        {
                            var row = new CRM_Adv();
                            row.c_code = !string.IsNullOrEmpty(item.c_code) ? item.c_code.Trim() : "";
                            row.fk_staff = (item.fk_staff != -1) ? item.fk_staff : -1;
                            row.c_input_date = !string.IsNullOrEmpty(item.c_input_date.ToString()) ? item.c_input_date : DateTime.Parse("1900-01-01");
                            row.c_status = -100;
                            row.fk_nguoi_duyet = (item.fk_nguoi_duyet != -1) ? item.fk_nguoi_duyet : -1;
                            row.c_ngay_duyet = !string.IsNullOrEmpty(item.c_ngay_duyet.ToString()) ? item.c_ngay_duyet : DateTime.Parse("1900-01-01");
                            row.c_ngay_yc_duyet = !string.IsNullOrEmpty(item.c_ngay_yc_duyet.ToString()) ? item.c_ngay_yc_duyet : DateTime.Parse("1900-01-01");
                            row.c_noi_dung_duyet = !string.IsNullOrEmpty(item.c_noi_dung_duyet) ? item.c_noi_dung_duyet.Trim() : "";
                            row.c_dinh_muc_bn = (item.c_dinh_muc_bn != 0) ? item.c_dinh_muc_bn : 0;
                            row.c_dinh_muc_pr_goi = (item.c_dinh_muc_pr_goi != 0) ? item.c_dinh_muc_pr_goi : 0;
                            row.c_dinh_muc_pr_thuong = (item.c_dinh_muc_pr_thuong != 0) ? item.c_dinh_muc_pr_thuong : 0;
                            row.c_dinh_muc_cpm = (item.c_dinh_muc_pr_thuong != 0) ? item.c_dinh_muc_pr_thuong : 0;
                            row.c_noi_dung_gui_duyet = !string.IsNullOrEmpty(item.c_noi_dung_gui_duyet) ? item.c_noi_dung_gui_duyet.Trim() : "";
                            row.c_note = !string.IsNullOrEmpty(item.c_note) ? item.c_note.Trim() : "";
                            row.c_noi_dung_xn_dang_dv = !string.IsNullOrEmpty(item.c_noi_dung_xn_dang_dv) ? item.c_noi_dung_xn_dang_dv.Trim() : "";
                            row.c_from_date = !string.IsNullOrEmpty(item.c_from_date.ToString()) ? item.c_from_date : DateTime.Parse("1900-01-01");
                            row.c_to_date = !string.IsNullOrEmpty(item.c_to_date.ToString()) ? item.c_to_date : DateTime.Parse("1900-01-01");
                            row.IsNew = 1;
                            row.RowCreatedUser = username;
                            row.RowUpdatedUser = "";
                            row.RowCreatedAt = DateTime.Now;
                            row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                            dbConn.Insert(row);
                            long id = dbConn.GetLastInsertId();
                            return new { success = true, message = "Save success!", pk_adv = id };
                        }
                        else
                        {
                            checkExits.c_code = !string.IsNullOrEmpty(item.c_code) ? item.c_code.Trim() : "";
                            //checkExits.fk_staff = (item.fk_staff != 0) ? item.fk_staff : 0;
                            //checkExits.c_input_date = !string.IsNullOrEmpty(item.c_input_date.ToString()) ? item.c_input_date : DateTime.Parse("1900-01-01");
                            //checkExits.c_status = (item.c_status != 0) ? item.c_status : 0;
                            //checkExits.fk_nguoi_duyet = (item.fk_nguoi_duyet != 0) ? item.fk_nguoi_duyet : 0;
                            //checkExits.c_ngay_duyet = !string.IsNullOrEmpty(item.c_ngay_duyet.ToString()) ? item.c_ngay_duyet : DateTime.Parse("1900-01-01");
                            //checkExits.c_ngay_yc_duyet = !string.IsNullOrEmpty(item.c_ngay_yc_duyet.ToString()) ? item.c_ngay_yc_duyet : DateTime.Parse("1900-01-01");
                            //checkExits.c_noi_dung_duyet = !string.IsNullOrEmpty(item.c_noi_dung_duyet) ? item.c_noi_dung_duyet.Trim() : "";
                            //checkExits.c_dinh_muc_bn = (item.c_dinh_muc_bn != 0) ? item.c_dinh_muc_bn : 0;
                            //checkExits.c_dinh_muc_pr_goi = (item.c_dinh_muc_pr_goi != 0) ? item.c_dinh_muc_pr_goi : 0;
                            //checkExits.c_dinh_muc_pr_thuong = (item.c_dinh_muc_pr_thuong != 0) ? item.c_dinh_muc_pr_thuong : 0;
                            //checkExits.c_dinh_muc_cpm = (item.c_dinh_muc_pr_thuong != 0) ? item.c_dinh_muc_pr_thuong : 0;
                            //checkExits.c_noi_dung_gui_duyet = !string.IsNullOrEmpty(item.c_noi_dung_gui_duyet) ? item.c_noi_dung_gui_duyet.Trim() : "";
                            //checkExits.c_note = !string.IsNullOrEmpty(item.c_note) ? item.c_note.Trim() : "";
                            //checkExits.c_noi_dung_xn_dang_dv = !string.IsNullOrEmpty(item.c_noi_dung_xn_dang_dv) ? item.c_noi_dung_xn_dang_dv.Trim() : "";
                            //checkExits.c_from_date = !string.IsNullOrEmpty(item.c_from_date.ToString()) ? item.c_from_date : DateTime.Parse("1900-01-01");
                            //checkExits.c_to_date = !string.IsNullOrEmpty(item.c_to_date.ToString()) ? item.c_to_date : DateTime.Parse("1900-01-01");
                            checkExits.RowUpdatedUser = username;
                            checkExits.RowUpdatedAt = DateTime.Now;
                            dbConn.Update(checkExits);
                            return new { success = true, message = "Save success!", pk_adv = checkExits.pk_adv };
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

        [Ignore]
        public string c_customer_name { get; set; }
        [Ignore]
        public string c_customer_type { get; set; }
        [Ignore]
        public string c_staff_name { get; set; }
        [Ignore]
        public string c_region_id { get; set; }
        [Ignore]
        public int c_type { get; set; }
        [Ignore]
        public string c_nguoi_duyet { get; set; }
        [Ignore]
        public string c_website { get; set; }
        [Ignore]
        public string c_category { get; set; }
        [Ignore]
        public int fk_staff_up_ocm { get; set; }
        [Ignore]
        public int c_status_published { get; set; }
        [Ignore]
        public long c_total_money { get; set; }
        [Ignore]
        public int MaxOrder { get; set; }
        [Ignore]
        public double tong_tien_da_xuat_ban { get; set; }
        [Ignore]
        public double tong_tien_hop_dong { get; set; }
        [Ignore]
        public double tong_tien_con_lai { get; set; }
        [Ignore]
        public string c_labels { get; set; }
        [Ignore]
        public string c_location { get; set; }
        [Ignore]
        public int c_vung_mien { get; set; }
        [Ignore]
        public double c_don_gia_sau_ck { get; set; }
        [Ignore]
        public int c_vung_mien_nvkd { get; set; }

        public DataSourceResult GetPage(DataSourceRequest request, string whereCondition)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Page", request.Page));
            param.Add(new SqlParameter("@PageSize", request.PageSize));
            param.Add(new SqlParameter("@WhereCondition", whereCondition));
            param.Add(new SqlParameter("@Sort", ""));
            DataTable dt = new SqlHelper().ExecuteQuery("p_GetAll_ListDNDQC", param);
            var lst = new List<CRM_Adv>();
            foreach (DataRow row in dt.Rows)
            {
                var item = new CRM_Adv();
                item.pk_adv = !row.IsNull("pk_adv") ? int.Parse(row["pk_adv"].ToString()) : 0;
                item.c_code = !row.IsNull("c_code") ? row["c_code"].ToString() : "";
                item.fk_staff = !row.IsNull("fk_staff") ? int.Parse(row["fk_staff"].ToString()) : 0;
                item.c_vung_mien_nvkd = !row.IsNull("c_vung_mien_nvkd") ? int.Parse(row["c_vung_mien_nvkd"].ToString()) : 0;
                item.c_customer_name = !row.IsNull("c_customer_name") ? row["c_customer_name"].ToString() : "";
                item.c_dot_order = !row.IsNull("c_dot_order") ? int.Parse(row["c_dot_order"].ToString()) : 0;
                item.tong_tien_hop_dong = !row.IsNull("tong_tien_hop_dong") ? double.Parse(row["tong_tien_hop_dong"].ToString()) : 0;
                item.c_don_gia_sau_ck = !row.IsNull("c_don_gia_sau_ck") ? double.Parse(row["c_don_gia_sau_ck"].ToString()) : 0;
                item.c_vung_mien = !row.IsNull("c_vung_mien") ? int.Parse(row["c_vung_mien"].ToString()) : 0;
                item.c_type = !row.IsNull("c_type") ? int.Parse(row["c_type"].ToString()) : 0;
                item.c_status = !row.IsNull("c_status") ? int.Parse(row["c_status"].ToString()) : 0;
                item.c_status_published = !row.IsNull("c_status_published") ? int.Parse(row["c_status_published"].ToString()) : 0;
                item.c_ngay_yc_duyet = !row.IsNull("c_ngay_yc_duyet") ? DateTime.Parse(row["c_ngay_yc_duyet"].ToString()) : DateTime.Parse("1900-01-01");
                item.c_ngay_duyet = !row.IsNull("c_ngay_duyet") ? DateTime.Parse(row["c_ngay_duyet"].ToString()) : DateTime.Parse("1900-01-01");
                item.fk_nguoi_duyet = !row.IsNull("c_nguoi_duyet") ? int.Parse(row["c_nguoi_duyet"].ToString()) : 0;
                item.c_publisher = !row.IsNull("c_publisher") ? row["c_publisher"].ToString() : "";
                item.fk_staff_up_ocm = !row.IsNull("fk_staff_up_ocm") ? int.Parse(row["fk_staff_up_ocm"].ToString()) : -1; 
                lst.Add(item);
            }
            request.Filters = null;
            DataSourceResult result = new DataSourceResult();
            result.Data = lst;
            result.Total = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["RowCount"]) : 0;
            return result;

        }
    }
    public class CRM_Adv_CStatus
    {
        [AutoIncrement]
        public string TAT_CA { get; set; }
        public string CHO_DUYET { get; set; }
        public string DA_DUYET { get; set; }
        public string DA_XOA { get; set; }
        public static CRM_Adv_CStatus CountStatus(string UserID, string TypeName)
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

            System.Data.DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,
                System.Data.CommandType.StoredProcedure, "p_select_count_adv_by_status", parameters);
            var lst = new CRM_Adv_CStatus[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                var item = new CRM_Adv_CStatus();
                item.CHO_DUYET = int.Parse(row["CHO_DUYET"].ToString()).ToString("#,##0");
                item.DA_DUYET = int.Parse(row["DA_DUYET"].ToString()).ToString("#,##0");
                item.DA_XOA = int.Parse(row["DA_XOA"].ToString()).ToString("#,##0");
                item.TAT_CA = (int.Parse(row["CHO_DUYET"].ToString()) + int.Parse(row["DA_DUYET"].ToString()) +
                    int.Parse(row["DA_XOA"].ToString())).ToString("#,##0");
                lst[i] = item;
                i++;
            }
            return lst.FirstOrDefault();
        }
    }
    public class CRM_Adv_Duyet
    {
        [AutoIncrement]
        public Int64 pk_duyet { get; set; }
        public int fk_staff { get; set; }
        public int c_type { get; set; }
        public string c_content { get; set; }
        public Int64 fk_adv { get; set; }
        public DateTime? c_ngay_yc_duyet { get; set; }
        public DateTime? c_ngay_duyet { get; set; }
        public int c_dot_order { get; set; }
        public DateTime? c_input_date { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }


        public static object Save(CRM_Adv_Duyet item, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (item != null)
                {
                    try
                    {
                        //foreach (var item in items)
                        //{
                            var checkExits = dbConn.SingleOrDefault<CRM_Adv_Duyet>("fk_adv= {0} AND c_dot_order = {1}", item.fk_adv,item.c_dot_order);
                            if (checkExits == null)
                            {
                                var row = new CRM_Adv_Duyet();
                                row.fk_adv = (item.fk_adv != 0) ? item.fk_adv : 0;
                                row.fk_staff = (item.fk_staff != 0) ? item.fk_staff : 0;
                                row.c_type = (item.c_type != -100) ? item.c_type : -100;
                                row.c_content = !string.IsNullOrEmpty(item.c_content) ? item.c_content : "";
                                row.c_ngay_yc_duyet = !string.IsNullOrEmpty(item.c_ngay_yc_duyet.ToString()) ? DateTime.Parse(item.c_ngay_yc_duyet.ToString()) : DateTime.Parse("1900-01-01");
                                row.c_ngay_duyet = !string.IsNullOrEmpty(item.c_ngay_duyet.ToString()) ? DateTime.Parse(item.c_ngay_duyet.ToString()) : DateTime.Parse("1900-01-01");
                                row.c_dot_order = !string.IsNullOrEmpty(item.c_dot_order.ToString()) ? item.c_dot_order :0;
                                row.c_input_date = !string.IsNullOrEmpty(item.c_input_date.ToString()) ? DateTime.Parse(item.c_input_date.ToString()) : DateTime.Parse("1900-01-01");
                                row.RowCreatedUser = username;
                                row.RowUpdatedUser = "";
                                row.RowCreatedAt = DateTime.Now;
                                row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                                dbConn.Insert(row);
                            }
                            else
                            {
                                checkExits.fk_adv = (item.fk_adv != 0) ? item.fk_adv : 0;
                                checkExits.fk_staff = (item.fk_staff != 0) ? item.fk_staff : 0;
                                checkExits.c_type = (item.c_type != -100) ? item.c_type : -100;
                                checkExits.c_content = !string.IsNullOrEmpty(item.c_content) ? item.c_content : "";
                                checkExits.c_ngay_yc_duyet = !string.IsNullOrEmpty(item.c_ngay_yc_duyet.ToString()) ? DateTime.Parse(item.c_ngay_yc_duyet.ToString()) : DateTime.Parse("1900-01-01");
                                checkExits.c_ngay_duyet = !string.IsNullOrEmpty(item.c_ngay_duyet.ToString()) ? DateTime.Parse(item.c_ngay_duyet.ToString()) : DateTime.Parse("1900-01-01");
                                checkExits.c_dot_order = !string.IsNullOrEmpty(item.c_dot_order.ToString()) ? item.c_dot_order : 0;
                                checkExits.c_input_date = !string.IsNullOrEmpty(item.c_input_date.ToString()) ? DateTime.Parse(item.c_input_date.ToString()) : DateTime.Parse("1900-01-01");
                                checkExits.RowUpdatedUser = username;
                                checkExits.RowUpdatedAt = DateTime.Now;
                                dbConn.Update(checkExits);
                            }
                        //}
                        long id = dbConn.GetLastInsertId();
                        return new { success = true, message = "Save success!", pk_services = id };
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
    public class CRM_Adv_File
    {
        [AutoIncrement]
        public Int64 pk_file { get; set; }
        public Int64 fk_adv { get; set; }
        public string c_type { get; set; }
        public string c_file_name { get; set; }
        public Int64 c_dot_order { get; set; }
        public DateTime? c_input_date { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }
    }

    public class CRM_Adv_Services
    {
        [AutoIncrement]
        public Int64 pk_services { get; set; }
        public Int64 fk_adv { get; set; }
        public int c_type { get; set; }
        public string c_website { get; set; }
        public string c_category { get; set; }
        public string c_location { get; set; }
        public string c_nature { get; set; }
        public DateTime? c_ngay_len { get; set; }
        public DateTime? c_ngay_xuong { get; set; }
        public string c_so_luong { get; set; }
        public string c_size { get; set; }
        public string c_format { get; set; }
        public double c_don_gia { get; set; }
        public int c_km { get; set; }
        public string c_note { get; set; }
        public int c_gio { get; set; }
        public double c_chiet_khau1 { get; set; }
        public double c_chiet_khau2 { get; set; }
        public double c_chiet_khau3 { get; set; }
        public int c_vung_mien { get; set; }
        public DateTime? c_ngay_tra_tien { get; set; }
        public double c_tien_tra { get; set; }
        public double c_luy_ke_da_dang { get; set; }
        public int c_order { get; set; }
        public int c_status { get; set; }
        public DateTime? c_ngay_duyet { get; set; }
        public string c_label { get; set; }
        public DateTime? c_input_date { get; set; }
        public DateTime? c_edit_date { get; set; }
        public int fk_staff_edit { get; set; }
        public string c_book_code { get; set; }
        public int c_dot_order { get; set; }
        public DateTime? c_ngay_up_ocm { get; set; }
        public int fk_staff_up_ocm { get; set; }
        public string c_kenh_qc { get; set; }
        public string c_chuyen_muc_kenhqc { get; set; }
        public string c_vi_tri_kenhqc { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }
        public double c_don_gia_sau_ck { get; set; }
        [Ignore]
        public string c_ngay_lenString { get; set; }
        [Ignore]
        public string c_ngay_xuongString { get; set; }
        [Ignore]
        public string c_ngay_up_ocmString { get; set; }
        [Ignore]
        public string c_ngay_duyetString { get; set; }
        [Ignore]
        public string c_ngay_tra_tienString { get; set; }
        [Ignore]
        public string c_website_name { get; set; }
        [Ignore]
        public string c_category_name { get; set; }
        [Ignore]
        public string c_location_name { get; set; }
        [Ignore]
        public string c_nature_name { get; set; }
        [Ignore]
        public string c_kenh_qc_name { get; set; }
        [Ignore]
        public string c_chuyen_muc_kenhqc_name { get; set; }
        [Ignore]
        public string c_vi_tri_kenhqc_name { get; set; }
        [Ignore]
        public string c_gio_name { get; set; }
        [Ignore]
        public string c_vung_mien_name { get; set; }
        [Ignore]
        public string c_tuan { get; set; }
        [Ignore]
        public string c_thoi_gian { get; set; }

        [Ignore]
        public string c_website_d { get; set; }
        [Ignore]
        public string c_category_d { get; set; }
        [Ignore]
        public string c_location_d { get; set; }
        [Ignore]
        public string c_nature_d { get; set; }
        [Ignore]
        public string c_book_code_d { get; set; }
        [Ignore]
        public int c_total_days { get; set; }

        public static object Save(IEnumerable<CRM_Adv_Services> items, int c_dot_order, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (items != null)
                {
                    try
                    {
                        if (c_dot_order == 0)
                        {
                            var maxOrder = dbConn.QueryScalar<int>("SELECT MAX(c_dot_order) FROM CRM_Adv_Services WHERE fk_adv = '" + items.FirstOrDefault().fk_adv + "'") + 1;
                            c_dot_order = maxOrder;
                        }
                        long fk_adv=0;
                       
                        foreach (var item in items)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Adv_Services>("pk_services= {0}", item.pk_services);
                            if (checkExits == null)
                            {
                                var row = new CRM_Adv_Services();
                                fk_adv = item.fk_adv;
                      
                                row.fk_adv = (item.fk_adv != 0) ? item.fk_adv : 0;
                                row.c_type = (item.c_type != 0) ? item.c_type : 0;
                                row.c_website = !string.IsNullOrEmpty(item.c_website) ? item.c_website : "";
                                row.c_category = !string.IsNullOrEmpty(item.c_category) ? item.c_category : "";
                                row.c_location = !string.IsNullOrEmpty(item.c_location) ? item.c_location : "";
                                row.c_nature = !string.IsNullOrEmpty(item.c_nature) ? item.c_nature : "";
                                row.c_ngay_len = !string.IsNullOrEmpty(item.c_ngay_lenString) ? DateTime.Parse(item.c_ngay_lenString) : DateTime.Parse("1900-01-01");
                                row.c_ngay_xuong = !string.IsNullOrEmpty(item.c_ngay_xuongString) ? DateTime.Parse(item.c_ngay_xuongString) : DateTime.Parse("1900-01-01");
                                row.c_so_luong = !string.IsNullOrEmpty(item.c_so_luong) ? item.c_so_luong : "0";
                                row.c_size = !string.IsNullOrEmpty(item.c_size) ? item.c_size : "";
                                row.c_format = !string.IsNullOrEmpty(item.c_format) ? item.c_format : "";
                                row.c_don_gia = (item.c_don_gia != 0) ? item.c_don_gia : 0;
                                row.c_km = (item.c_km != 0) ? item.c_km : 0;
                                row.c_note = !string.IsNullOrEmpty(item.c_note) ? item.c_note : "";
                                row.c_gio = (item.c_gio != 0) ? item.c_gio : 0;
                                row.c_chiet_khau1 = (item.c_chiet_khau1 != 0) ? item.c_chiet_khau1 : 0;
                                row.c_chiet_khau2 = (item.c_chiet_khau2 != 0) ? item.c_chiet_khau2 : 0;
                                row.c_chiet_khau3 = (item.c_chiet_khau3 != 0) ? item.c_chiet_khau3 : 0;
                                row.c_vung_mien = (item.c_vung_mien != 0) ? item.c_vung_mien : 0;
                                row.c_ngay_tra_tien = !string.IsNullOrEmpty(item.c_ngay_tra_tienString) ? DateTime.Parse(item.c_ngay_tra_tienString) : DateTime.Parse("1900-01-01");
                                row.c_tien_tra = (item.c_tien_tra != 0) ? item.c_tien_tra : 0;
                                row.c_luy_ke_da_dang = (item.c_luy_ke_da_dang != 0) ? item.c_luy_ke_da_dang : 0;
                                row.c_dot_order = c_dot_order;
                                row.c_status = (item.c_status != 0) ? item.c_status : 0;
                                row.c_ngay_duyet = !string.IsNullOrEmpty(item.c_ngay_duyetString) ? DateTime.Parse(item.c_ngay_duyetString) : DateTime.Parse("1900-01-01");
                                row.c_label = !string.IsNullOrEmpty(item.c_label) ? item.c_label : "";
                                row.c_input_date = DateTime.Now;
                                row.c_edit_date = DateTime.Parse("1900-01-01");
                                row.fk_staff_edit = (item.fk_staff_edit != 0) ? item.fk_staff_edit : 0;
                                row.c_book_code = !string.IsNullOrEmpty(item.c_book_code) ? item.c_book_code : "";
                                row.c_ngay_up_ocm = DateTime.Parse("1900-01-01");
                                row.fk_staff_up_ocm = -1;
                                row.c_kenh_qc = !string.IsNullOrEmpty(item.c_kenh_qc) ? item.c_kenh_qc : "";
                                row.c_chuyen_muc_kenhqc = !string.IsNullOrEmpty(item.c_chuyen_muc_kenhqc) ? item.c_chuyen_muc_kenhqc : "";
                                row.c_vi_tri_kenhqc = !string.IsNullOrEmpty(item.c_vi_tri_kenhqc) ? item.c_vi_tri_kenhqc : "";
                                row.IsNew = 1;
                                row.c_don_gia_sau_ck = (item.c_don_gia_sau_ck != 0) ? item.c_don_gia_sau_ck : 0;
                                row.RowCreatedUser = username;
                                row.RowUpdatedUser = "";
                                row.RowCreatedAt = DateTime.Now;
                                row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                                dbConn.Insert(row);
                            }
                            else
                            {
                                fk_adv = item.fk_adv;
                                checkExits.fk_adv = (item.fk_adv != 0) ? item.fk_adv : 0;
                                checkExits.c_type = (item.c_type != 0) ? item.c_type : 0;
                                checkExits.c_website = !string.IsNullOrEmpty(item.c_website) ? item.c_website : "";
                                checkExits.c_category = !string.IsNullOrEmpty(item.c_category) ? item.c_category : "";
                                checkExits.c_location = !string.IsNullOrEmpty(item.c_location) ? item.c_location : "";
                                checkExits.c_nature = !string.IsNullOrEmpty(item.c_nature) ? item.c_nature : "";
                                checkExits.c_ngay_len = !string.IsNullOrEmpty(item.c_ngay_lenString) ? DateTime.Parse(item.c_ngay_lenString) : checkExits.c_ngay_len;
                                checkExits.c_ngay_xuong = !string.IsNullOrEmpty(item.c_ngay_xuongString) ? DateTime.Parse(item.c_ngay_xuongString) : checkExits.c_ngay_xuong;
                                checkExits.c_so_luong = !string.IsNullOrEmpty(item.c_so_luong) ? item.c_so_luong : "0";
                                checkExits.c_size = !string.IsNullOrEmpty(item.c_size) ? item.c_size : "";
                                checkExits.c_format = !string.IsNullOrEmpty(item.c_format) ? item.c_format : "";
                                checkExits.c_don_gia = (item.c_don_gia != 0) ? item.c_don_gia : 0;
                                checkExits.c_km = (item.c_km != 0) ? item.c_km : 0;
                                checkExits.c_note = !string.IsNullOrEmpty(item.c_note) ? item.c_note : "";
                                checkExits.c_gio = (item.c_gio != 0) ? item.c_gio : 0;
                                checkExits.c_chiet_khau1 = (item.c_chiet_khau1 != 0) ? item.c_chiet_khau1 : 0;
                                checkExits.c_chiet_khau2 = (item.c_chiet_khau2 != 0) ? item.c_chiet_khau2 : 0;
                                checkExits.c_chiet_khau3 = (item.c_chiet_khau3 != 0) ? item.c_chiet_khau3 : 0;
                                checkExits.c_vung_mien = (item.c_vung_mien != 0) ? item.c_vung_mien : 0;
                                checkExits.c_ngay_tra_tien = !string.IsNullOrEmpty(item.c_ngay_tra_tienString) ? DateTime.Parse(item.c_ngay_tra_tienString) : checkExits.c_ngay_tra_tien;
                                checkExits.c_tien_tra = (item.c_tien_tra != 0) ? item.c_tien_tra : 0;
                                checkExits.c_luy_ke_da_dang = (item.c_luy_ke_da_dang != 0) ? item.c_luy_ke_da_dang : 0;
                                checkExits.c_order = (item.c_order != 0) ? item.c_order : 0;
                                checkExits.c_status = (item.c_status != 0) ? item.c_status : 0;
                                checkExits.c_ngay_duyet = !string.IsNullOrEmpty(item.c_ngay_duyetString) ? DateTime.Parse(item.c_ngay_duyetString) : checkExits.c_ngay_duyet;
                                checkExits.c_tien_tra = (item.c_tien_tra != 0) ? item.c_tien_tra : 0;
                                checkExits.c_label = !string.IsNullOrEmpty(item.c_label) ? item.c_label : "";
                                checkExits.c_edit_date = DateTime.Now;
                                checkExits.fk_staff_edit = (item.fk_staff_edit != 0) ? item.fk_staff_edit : 0;
                                checkExits.c_book_code = !string.IsNullOrEmpty(item.c_book_code) ? item.c_book_code : "";
                                //checkExits.c_dot_order = (item.c_dot_order != 0) ? item.c_dot_order : 0;
                                //checkExits.c_ngay_up_ocm = !string.IsNullOrEmpty(item.c_ngay_up_ocmString) ? DateTime.Parse(item.c_ngay_up_ocmString) : checkExits.c_ngay_up_ocm;
                                //checkExits.fk_staff_up_ocm = (item.fk_staff_up_ocm != 0) ? item.fk_staff_up_ocm : 0;
                                checkExits.c_kenh_qc = !string.IsNullOrEmpty(item.c_kenh_qc) ? item.c_kenh_qc : "";
                                checkExits.c_chuyen_muc_kenhqc = !string.IsNullOrEmpty(item.c_chuyen_muc_kenhqc) ? item.c_chuyen_muc_kenhqc : "";
                                checkExits.c_vi_tri_kenhqc = !string.IsNullOrEmpty(item.c_vi_tri_kenhqc) ? item.c_vi_tri_kenhqc : checkExits.c_vi_tri_kenhqc;
                                checkExits.c_don_gia_sau_ck = (item.c_don_gia_sau_ck != 0) ? item.c_don_gia_sau_ck : checkExits.c_don_gia_sau_ck;
                                checkExits.RowUpdatedUser = username;
                                checkExits.RowUpdatedAt = DateTime.Now;
                                dbConn.Update(checkExits);
                            }
                        }
                        long id = dbConn.GetLastInsertId();
                        var duyet = dbConn.Where<CRM_Adv_Duyet>(p => p.fk_adv == fk_adv  && p.c_dot_order == c_dot_order).FirstOrDefault();
                        if (duyet == null)
                        {
                            CRM_Adv_Duyet n = new CRM_Adv_Duyet();
                            n.fk_adv = fk_adv;
                            n.fk_staff = -1;
                            n.c_type = -100;
                            n.c_dot_order = c_dot_order;
                            n.c_ngay_yc_duyet =DateTime.Parse("1900-01-01");
                            CRM_Adv_Duyet.Save(n, username);
                        }
                        return new { success = true, message = "Save success!", pk_services = id, c_dot_order= c_dot_order };
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
    public class CRM_Adv_Services_Link
    {
        [AutoIncrement]
        public Int64 pk_link { get; set; }
        public Int64 fk_adv { get; set; }
        public Int64 fk_services { get; set; }
        public string c_counter_link { get; set; }
        public string c_customer_link { get; set; }
        public Int64 c_cpm { get; set; }
        public Int64 c_cpc { get; set; }
        public int c_order { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int IsNew { get; set; }
    }
    public class CRM_Adv_BookingPR_View
    {
        public Int64 ID { get; set; }
        public string Code { get; set; }
        public int Website { get; set; }
        public int Category { get; set; }
        public int Location { get; set; }
        public DateTime NgayLen { get; set; }
        public DateTime NgayXuong { get; set; }
        public int GioLen { get; set; }
        public int VungMien { get; set; }
        public float PriceCharged { get; set; }
        public string WebsiteName { get; set; }
        public string CategoryName { get; set; }
        public string LocationName { get; set; }
        public string RegionName { get; set; }
        public string TimeUpName { get; set; }
    }
    public class CRM_Adv_BookingBanner_View
    {
        public string IDBookLocation { get; set; }
        public string BookCode { get; set; }
        public Int64 Website { get; set; }
        public Int64 Category { get; set; }
        public Int64 Location { get; set; }
        public Int64 Nature { get; set; }
        public DateTime NgayLen { get; set; }
        public DateTime NgayXuong { get; set; }
        public long PriceCharged { get; set; }
        public string WebsiteName { get; set; }
        public string CategoryName { get; set; }
        public string LocationName { get; set; }
        public string RegionName { get; set; }
        public string NatureName { get; set; }
    }
    public class CRM_Adv_Orders
    {
        [AutoIncrement]
        public Int64 pk_order { get; set; }
        public Int64 fk_adv { get; set; }
        public int c_dot_order { get; set; }
        public DateTime? c_ngay_yc { get; set; }
        public string c_noi_dung_yc { get; set; }
        public int c_trang_thai { get; set; }
        public Int64 c_han_muc_bn { get; set; }
        public Int64 c_han_muc_goi_pr { get; set; }
        public Int64 c_han_muc_pr_thuong { get; set; }
        public Int64 c_han_muc_cpm { get; set; }
        public DateTime? c_from_date { get; set; }
        public DateTime? c_to_date { get; set; }
        public string c_ghi_chu { get; set; }
        public int c_nguoi_duyet { get; set; }
        public bool c_xac_nhan_len_trang { get; set; }
        public DateTime? c_ngay_duyet { get; set; }
        public string c_noi_dung_duyet { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public int c_nguoi_xac_nhan { get; set; }
        public DateTime? c_ngay_xac_nhan { get; set; }
        public string c_noi_dung_xac_nhan { get; set; }
        [Ignore]
        public string ActionSelected { get; set; }
        [Ignore]
        public string c_from_dateString { get; set; }
        [Ignore]
        public string c_to_dateString { get; set; }
        [Ignore]
        public string c_ten_nguoi_duyet { get; set; }
        [Ignore]
        public string c_ten_nguoi_xac_nhan { get; set; }
        
        public static object Save(CRM_Adv_Orders item, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (item != null)
                {
                    try
                    {
                        var checkExits = dbConn.SingleOrDefault<CRM_Adv_Orders>("pk_order= {0}", item.pk_order);
                        if (checkExits == null)
                        {
                            var row = new CRM_Adv_Orders();
                            row.fk_adv = (item.fk_adv != 0) ? item.fk_adv : 0;
                            row.c_dot_order = (item.c_dot_order != 0) ? item.c_dot_order : 0;
                            row.c_ngay_yc = !string.IsNullOrEmpty(item.c_ngay_yc.ToString()) ? item.c_ngay_yc : DateTime.Parse("1900-01-01");
                            row.c_noi_dung_yc = !string.IsNullOrEmpty(item.c_noi_dung_yc) ? item.c_noi_dung_yc : "";
                            row.c_trang_thai = (item.c_trang_thai != 0) ? item.c_trang_thai : 0;
                            row.c_han_muc_bn = (item.c_han_muc_bn != 0) ? item.c_han_muc_bn : 0;
                            row.c_han_muc_goi_pr = (item.c_han_muc_goi_pr != 0) ? item.c_han_muc_goi_pr : 0;
                            row.c_han_muc_pr_thuong = (item.c_han_muc_pr_thuong != 0) ? item.c_han_muc_pr_thuong : 0;
                            row.c_han_muc_cpm = (item.c_han_muc_cpm != 0) ? item.c_han_muc_cpm : 0;
                            row.c_from_date = !string.IsNullOrEmpty(item.c_from_date.ToString()) ? item.c_from_date : DateTime.Parse("1900-01-01");
                            row.c_to_date = !string.IsNullOrEmpty(item.c_to_date.ToString()) ? item.c_to_date : DateTime.Parse("1900-01-01");
                            row.c_ghi_chu = !string.IsNullOrEmpty(item.c_ghi_chu) ? item.c_ghi_chu : "";
                            row.c_nguoi_duyet = (item.c_nguoi_duyet != -1) ? item.c_nguoi_duyet : -1;
                            row.c_ngay_duyet = !string.IsNullOrEmpty(item.c_ngay_duyet.ToString()) ? item.c_ngay_duyet : DateTime.Parse("1900-01-01");
                            row.c_noi_dung_duyet = !string.IsNullOrEmpty(item.c_noi_dung_duyet) ? item.c_noi_dung_duyet : "";
                            row.c_xac_nhan_len_trang = !string.IsNullOrEmpty(item.c_xac_nhan_len_trang.ToString()) ? item.c_xac_nhan_len_trang : false;
                            row.c_nguoi_xac_nhan = (item.c_nguoi_xac_nhan != -1) ? item.c_nguoi_xac_nhan : -1;
                            row.c_ngay_xac_nhan = !string.IsNullOrEmpty(item.c_ngay_xac_nhan.ToString()) ? item.c_ngay_xac_nhan : DateTime.Parse("1900-01-01");
                            row.c_noi_dung_xac_nhan = !string.IsNullOrEmpty(item.c_noi_dung_xac_nhan) ? item.c_noi_dung_xac_nhan : "";
                            row.RowCreatedUser = username;
                            row.RowUpdatedUser = "";
                            row.RowCreatedAt = DateTime.Now;
                            row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                            dbConn.Insert(row);
                        }
                        else
                        {
                            checkExits.fk_adv = (item.fk_adv != 0) ? item.fk_adv : 0;
                            checkExits.c_dot_order = (item.c_dot_order != 0) ? item.c_dot_order : 0;
                            checkExits.c_ngay_yc = !string.IsNullOrEmpty(item.c_ngay_yc.ToString()) ? item.c_ngay_yc : DateTime.Parse("1900-01-01");
                            checkExits.c_noi_dung_yc = !string.IsNullOrEmpty(item.c_noi_dung_yc) ? item.c_noi_dung_yc : "";
                            checkExits.c_trang_thai = (item.c_trang_thai != 0) ? item.c_trang_thai : 0;
                            checkExits.c_han_muc_bn = (item.c_han_muc_bn != 0) ? item.c_han_muc_bn : 0;
                            checkExits.c_han_muc_goi_pr = (item.c_han_muc_goi_pr != 0) ? item.c_han_muc_goi_pr : 0;
                            checkExits.c_han_muc_pr_thuong = (item.c_han_muc_pr_thuong != 0) ? item.c_han_muc_pr_thuong : 0;
                            checkExits.c_han_muc_cpm = (item.c_han_muc_cpm != 0) ? item.c_han_muc_cpm : 0;
                            checkExits.c_from_date = !string.IsNullOrEmpty(item.c_from_date.ToString()) ? item.c_from_date : DateTime.Parse("1900-01-01");
                            checkExits.c_to_date = !string.IsNullOrEmpty(item.c_to_date.ToString()) ? item.c_to_date : DateTime.Parse("1900-01-01");
                            checkExits.c_ghi_chu = !string.IsNullOrEmpty(item.c_ghi_chu) ? item.c_ghi_chu : "";
                            checkExits.c_nguoi_duyet = (item.c_nguoi_duyet != -1) ? item.c_nguoi_duyet : -1;
                            checkExits.c_ngay_duyet = !string.IsNullOrEmpty(item.c_ngay_duyet.ToString()) ? item.c_ngay_duyet : DateTime.Parse("1900-01-01");
                            checkExits.c_noi_dung_duyet = !string.IsNullOrEmpty(item.c_noi_dung_duyet) ? item.c_noi_dung_duyet : "";
                            checkExits.c_xac_nhan_len_trang = !string.IsNullOrEmpty(item.c_xac_nhan_len_trang.ToString()) ? item.c_xac_nhan_len_trang : false;
                            checkExits.c_nguoi_xac_nhan = (item.c_nguoi_xac_nhan != -1) ? item.c_nguoi_xac_nhan : -1;
                            checkExits.c_ngay_xac_nhan = !string.IsNullOrEmpty(item.c_ngay_xac_nhan.ToString()) ? item.c_ngay_xac_nhan : DateTime.Parse("1900-01-01");
                            checkExits.c_noi_dung_xac_nhan = !string.IsNullOrEmpty(item.c_noi_dung_xac_nhan) ? item.c_noi_dung_xac_nhan : "";
                            checkExits.RowUpdatedUser = username;
                            checkExits.RowUpdatedAt = DateTime.Now;
                            dbConn.Update(checkExits);
                        }

                        long id = dbConn.GetLastInsertId();
                        return new { success = true, message = "Save success!", pk_order = id };
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


    public class CRM_Adv_Services_View
    {
   
        public Int64 pk_services { get; set; }
        public Int64 fk_adv { get; set; }
        public int c_type { get; set; }
        public int c_dot_order { get; set; }
        public int c_status { get; set; }
        public int c_status_publish { get; set; }
        public string c_website_d { get; set; }
        public string c_category_d { get; set; }
        public string c_location_d { get; set; }
        public string c_nature_d { get; set; }
        public string c_book_code_d { get; set; }


    }
}
