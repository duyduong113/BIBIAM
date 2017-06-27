using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BIBIAM.Core.Entities;
using BIBIAM.Core.Data.Providers;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using System.IO;
using System.Web;

namespace BIBIAM.Core.Data.DataObject
{
    public class Product_Info_DAO
    {
        public DataTable ReadAll(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteQuery("sp_GetAll_ProductInfo", new List<SqlParameter>());
        }

        public DataTable ReadAllNonSelected(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteQuery("sp_GetAll_NonSelected_ProductInfo", new List<SqlParameter>());
        }

        public string Delete(string[] ids, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (var id in ids)
                        {
                            var checkID = db.FirstOrDefault<Product_Info>(s => s.id == int.Parse(id));
                            db.Delete<Product_Info>(s => s.id == int.Parse(id));
                            // Xóa ảnh
                            if (checkID != null)
                            {
                                if (!String.IsNullOrEmpty(checkID.url))
                                {
                                    var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Images/Product_Info/"), checkID.url);
                                    if (!String.IsNullOrEmpty(path))
                                    {
                                        System.IO.File.Delete(path);
                                    }
                                }
                            }
                        }
                        dbTrans.Commit();
                        return "true";
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return e.Message.ToString();
                    }
                }
            }
        }
        public string UpSert(List<Product_Info> lstProdInfo, string UserName, string Type, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (Product_Info row in lstProdInfo)
                        {
                            var checkID = db.SingleOrDefault<Product_Info>("id={0}", row.id);
                            if (checkID != null && (Type == "Update" || Type == "Sync"))
                            {
                                if (String.IsNullOrEmpty(row.url))
                                    row.url = checkID.url;
                                else
                                {
                                    if (!String.IsNullOrEmpty(checkID.url))
                                    {
                                        var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Images/Product_Info/"), checkID.url);
                                        if (!String.IsNullOrEmpty(path))
                                        {
                                            System.IO.File.Delete(path);
                                        }
                                    }
                                }
                                row.ngay_tao = checkID.ngay_tao;
                                row.ngay_duyet = checkID.ngay_duyet;
                                row.ngay_xuat_ban = checkID.ngay_xuat_ban;

                                row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_cap_nhat = UserName;

                                if (checkID.trang_thai_duyet != row.trang_thai_duyet && (row.trang_thai_duyet == BIBIAM.Core.AllConstant.trang_thai_duyet.DA_DUYET || row.trang_thai_duyet == BIBIAM.Core.AllConstant.trang_thai_duyet.TU_CHOI))
                                {
                                    row.ngay_duyet = DateTime.Now;
                                    row.nguoi_duyet = UserName;
                                }

                                if (checkID.trang_thai_xuat_ban != row.trang_thai_xuat_ban && (row.trang_thai_xuat_ban == BIBIAM.Core.AllConstant.trang_thai_xuat_ban.DA_XUAT_BAN))
                                {
                                    row.ngay_xuat_ban = DateTime.Now;
                                    row.nguoi_xuat_ban = UserName;
                                }
                                db.Update(EmptyIfNull(row));

                                List<SqlParameter> lstParameter = new List<SqlParameter>();

                                if (Type == "Sync")
                                {
                                    string rs = Sync(row, UserName, connectionString);
                                    if (rs != "")
                                        return rs;
                                }

                                //Product_Info item = EmptyIfNull(row);
                                //List<SqlParameter> lstParameter = new List<SqlParameter>();
                                //lstParameter.Add(new SqlParameter("@id", item.id));
                                //lstParameter.Add(new SqlParameter("@ma_san_pham", item.ma_san_pham));
                                //lstParameter.Add(new SqlParameter("@ma_loai_san_pham", item.ma_loai_san_pham));
                                //lstParameter.Add(new SqlParameter("@ten_san_pham", item.ten_san_pham));
                                //lstParameter.Add(new SqlParameter("@mo_ta", item.mo_ta));
                                //lstParameter.Add(new SqlParameter("@tu_khoa", item.tu_khoa));
                                //lstParameter.Add(new SqlParameter("@tag", item.tag));
                                //lstParameter.Add(new SqlParameter("@slug", item.slug));
                                //lstParameter.Add(new SqlParameter("@trong_so", item.trong_so));
                                //lstParameter.Add(new SqlParameter("@nguoi_duyet", item.nguoi_duyet));
                                //lstParameter.Add(new SqlParameter("@ngay_duyet", checkID.ngay_duyet));
                                //lstParameter.Add(new SqlParameter("@trang_thai_duyet", item.trang_thai_duyet));
                                //lstParameter.Add(new SqlParameter("@nguoi_xuat_ban", item.nguoi_xuat_ban));
                                //lstParameter.Add(new SqlParameter("@ngay_xuat_ban", checkID.ngay_xuat_ban));
                                //lstParameter.Add(new SqlParameter("@trang_thai_xuat_ban", item.trang_thai_xuat_ban));
                                //lstParameter.Add(new SqlParameter("@trang_thai", item.trang_thai));
                                //lstParameter.Add(new SqlParameter("@ngay_tao", checkID.ngay_tao));
                                //lstParameter.Add(new SqlParameter("@nguoi_tao", item.nguoi_tao));
                                //lstParameter.Add(new SqlParameter("@ngay_cap_nhat", item.ngay_cap_nhat));
                                //lstParameter.Add(new SqlParameter("@nguoi_cap_nhat", item.nguoi_cap_nhat));
                                //lstParameter.Add(new SqlParameter("@url", item.url));
                                //lstParameter.Add(new SqlParameter("@Type", "Update"));
                                //lstParameter.Add(new SqlParameter("@IsSync", 1));
                                //new SqlHelper().ExecuteNoneQuery("p_Product_Info", lstParameter);
                            }
                            else if (checkID == null && (Type == "Insert" || Type == "Sync"))
                            {
                                string ProductID = String.Empty;
                                var lastId = db.FirstOrDefault<Product_Info>("SELECT TOP 1 * FROM Product_Info ORDER BY id DESC");
                                if (lastId != null)
                                {
                                    if (lastId.ma_san_pham.Contains("SP"))
                                    {
                                        var nextNo = Int32.Parse(lastId.ma_san_pham.Substring(2, 7)) + 1;
                                        ProductID = "SP" + String.Format("{0:0000000}", nextNo);
                                    }
                                }
                                else
                                {
                                    ProductID = "SP" + "0000001";
                                }
                                // Chưa có mã loại sản phẩm
                                row.ma_san_pham = ProductID;
                                //row.ngay_duyet = row.ngay_xuat_ban = row.ngay_cap_nhat = DateTime.Now;//chua biet
                                row.ngay_tao = DateTime.Now;
                                row.nguoi_tao = UserName;
                                db.Insert(EmptyIfNull(row));

                                List<SqlParameter> lstParameter = new List<SqlParameter>();
                                if (Type == "Sync")
                                {
                                    string rs = Sync(row, UserName, connectionString);
                                    if (rs != "")
                                        return rs;
                                }

                                //Product_Info item = EmptyIfNull(row);
                                //List<SqlParameter> lstParameter = new List<SqlParameter>();
                                //lstParameter.Add(new SqlParameter("@id", item.id));
                                //lstParameter.Add(new SqlParameter("@ma_san_pham", item.ma_san_pham));
                                //lstParameter.Add(new SqlParameter("@ma_loai_san_pham", item.ma_loai_san_pham));
                                //lstParameter.Add(new SqlParameter("@ten_san_pham", item.ten_san_pham));
                                //lstParameter.Add(new SqlParameter("@mo_ta", item.mo_ta));
                                //lstParameter.Add(new SqlParameter("@tu_khoa", item.tu_khoa));
                                //lstParameter.Add(new SqlParameter("@tag", item.tag));
                                //lstParameter.Add(new SqlParameter("@slug", item.slug));
                                //lstParameter.Add(new SqlParameter("@trong_so", item.trong_so));
                                //lstParameter.Add(new SqlParameter("@nguoi_duyet", item.nguoi_duyet));
                                //lstParameter.Add(new SqlParameter("@ngay_duyet", item.ngay_duyet));
                                //lstParameter.Add(new SqlParameter("@trang_thai_duyet", item.trang_thai_duyet));
                                //lstParameter.Add(new SqlParameter("@nguoi_xuat_ban", item.nguoi_xuat_ban));
                                //lstParameter.Add(new SqlParameter("@ngay_xuat_ban", item.ngay_xuat_ban));
                                //lstParameter.Add(new SqlParameter("@trang_thai_xuat_ban", item.trang_thai_xuat_ban));
                                //lstParameter.Add(new SqlParameter("@trang_thai", item.trang_thai));
                                //lstParameter.Add(new SqlParameter("@ngay_tao", item.ngay_tao));
                                //lstParameter.Add(new SqlParameter("@nguoi_tao", item.nguoi_tao));
                                //lstParameter.Add(new SqlParameter("@ngay_cap_nhat", item.ngay_cap_nhat));
                                //lstParameter.Add(new SqlParameter("@nguoi_cap_nhat", item.nguoi_cap_nhat));
                                //lstParameter.Add(new SqlParameter("@url", item.url));
                                //lstParameter.Add(new SqlParameter("@Type", "Insert"));
                                //lstParameter.Add(new SqlParameter("@IsSync", 1));
                                //new SqlHelper().ExecuteNoneQuery("p_Product_Info", lstParameter);
                            }
                        }
                        dbTrans.Commit();
                        return "true";
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return e.Message.ToString();
                    }
                }
            }
        }
        public string Sync(Product_Info item, string UserName, string connectionString)
        {
            List<SqlParameter> lstParameter = new List<SqlParameter>();
            if (!CheckBeforeSync(item))
                return "Sản phẩm chưa đủ điều kiện Sync !\n Vui lòng kiểm tra tất cả trạng thái !";
            lstParameter.Clear();
            lstParameter.Add(new SqlParameter("@id", item.id));
            lstParameter.Add(new SqlParameter("@ma_san_pham", item.ma_san_pham));
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var pro_hier = db.FirstOrDefault<Product_Hierarchy>(s => s.ma_san_pham == item.ma_san_pham && s.trang_thai == AllConstant.trang_thai.DANG_SU_DUNG);
                        if (pro_hier != null)
                        {
                            pro_hier.ngay_cap_nhat = DateTime.Now;
                            pro_hier.nguoi_cap_nhat = UserName;
                            db.Update(pro_hier);
                        }
                        else
                        {
                            dbTrans.Rollback();
                            return "Cây phân cấp chưa có hoặc trạng thái không hoạt động!";
                        }
                        var pro_price = db.Select<Product_Price>(s => s.ma_san_pham == item.ma_san_pham && s.trang_thai == AllConstant.trang_thai.DANG_SU_DUNG && s.trang_thai_duyet == AllConstant.trang_thai_duyet.DA_DUYET && s.trang_thai_xuat_ban == AllConstant.trang_thai_xuat_ban.DA_XUAT_BAN);
                        if (pro_price == null)
                        {
                            return "Chưa có giá sản phẩm thõa mãn!";
                        }
                        foreach (Product_Price iitem in pro_price)
                        {
                            iitem.ngay_cap_nhat = DateTime.Now;
                            iitem.nguoi_cap_nhat = UserName;
                            db.Update(iitem);
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return e.Message;
                    }
                }
            }
            new SqlHelper(connectionString).ExecuteNoneQuery("p_Product_SyncFullToMySQL", lstParameter);
            return "";
        }
        public string UpdateStatus(string id, string UserName, string type, string status, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var checkID = db.SingleOrDefault<Product_Info>("id={0}", id);
                        if (checkID != null)
                        {
                            if (type == "duyet")
                            {
                                if (checkID.trang_thai == AllConstant.trang_thai.DANG_SU_DUNG)
                                {
                                    checkID.trang_thai_duyet = status;
                                }
                                else
                                {
                                    return "Không được duyệt do sản phẩm chưa hoạt động";
                                }
                                //db.Update(set: "trang_thai_duyet={0}".Params(status), where: "id='" + id +"'");
                            }
                            if (type == "xuatban")
                            {
                                if (checkID.trang_thai == AllConstant.trang_thai.DANG_SU_DUNG)
                                {
                                    if (checkID.trang_thai_duyet != AllConstant.trang_thai_duyet.TU_CHOI)
                                    {
                                        checkID.trang_thai_duyet = AllConstant.trang_thai_duyet.DA_DUYET;
                                        checkID.trang_thai_xuat_ban = status;
                                        //db.Update(set: "trang_thai_xuat_ban={0},ngay_cap_nhat={1},nguoi_cap_nhat={2}".Params(status, DateTime.Now, UserName), where: "id='" + id + "'");
                                    }
                                    else
                                    {
                                        return "Không được phép xuất bản do sản phẩm đã bị từ chối duyệt";
                                    }
                                }
                                else
                                {
                                    return "Chưa được xuất bản do sản phẩm chưa hoạt động";
                                }
                            }
                            checkID.ngay_cap_nhat = DateTime.Now;
                            checkID.nguoi_cap_nhat = UserName;
                            db.Update(checkID);
                        }
                        dbTrans.Commit();
                        return "true";
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return e.Message.ToString();
                    }
                }
            }
        }
        private Product_Info EmptyIfNull(Product_Info p1)
        {
            p1.ma_san_pham = !string.IsNullOrEmpty(p1.ma_san_pham) ? p1.ma_san_pham : "";
            p1.ma_loai_san_pham = (p1.ma_loai_san_pham == null) ? "" : p1.ma_loai_san_pham;
            p1.ten_san_pham = (p1.ten_san_pham == null) ? "" : p1.ten_san_pham;
            p1.mo_ta = (p1.mo_ta == null) ? "" : p1.mo_ta;
            p1.tu_khoa = (p1.tu_khoa == null) ? "" : p1.tu_khoa;
            p1.tag = (p1.tag == null) ? "" : p1.tag;
            p1.slug = (p1.slug == null) ? "" : p1.slug;
            p1.trong_so = string.IsNullOrEmpty(p1.trong_so.ToString()) ? 0 : p1.trong_so;
            p1.nguoi_duyet = (p1.nguoi_duyet == null) ? "" : p1.nguoi_duyet;
            p1.nguoi_xuat_ban = (p1.nguoi_xuat_ban == null) ? "" : p1.nguoi_xuat_ban;
            p1.nguoi_cap_nhat = (p1.nguoi_cap_nhat == null) ? "" : p1.nguoi_cap_nhat;
            p1.trang_thai_duyet = (p1.trang_thai_duyet == null) ? "" : p1.trang_thai_duyet;
            p1.trang_thai_xuat_ban = (p1.trang_thai_xuat_ban == null) ? "" : p1.trang_thai_xuat_ban;
            p1.trang_thai = (p1.trang_thai == null) ? "" : p1.trang_thai;
            p1.url = (p1.url == null) ? "" : p1.url;
            p1.ngay_cap_nhat = string.IsNullOrEmpty(p1.ngay_cap_nhat.ToString()) ? DateTime.Parse("1900-01-01") : p1.ngay_cap_nhat;
            p1.ngay_duyet = string.IsNullOrEmpty(p1.ngay_duyet.ToString()) ? DateTime.Parse("1900-01-01") : p1.ngay_duyet;
            p1.ngay_xuat_ban = string.IsNullOrEmpty(p1.ngay_xuat_ban.ToString()) ? DateTime.Parse("1900-01-01") : p1.ngay_xuat_ban;
            return p1;
        }

        private bool CheckBeforeSync(Product_Info p)
        {
            if (p.trang_thai != AllConstant.trang_thai.DANG_SU_DUNG)
                return false;
            else if (p.trang_thai_duyet != AllConstant.trang_thai_duyet.DA_DUYET)
                return false;
            else if (p.trang_thai_xuat_ban != AllConstant.trang_thai_xuat_ban.DA_XUAT_BAN)
                return false;
            return true;
        }
    }
}
