using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BIBIAM.Core.Entities;
using BIBIAM.Core.Data.Providers;
using System.Data.SqlClient;
using ServiceStack.OrmLite;

namespace BIBIAM.Core.Data.DataObject
{
    public class Banner_DAO
    {
        public string CreateUpdate(Banner banner, string UserName, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                try
                {
                   
                        var checkID = db.SingleOrDefault<Banner>("ma_banner={0}", banner.ma_banner);
                        if (checkID != null)
                        {

                            checkID.url_link = !string.IsNullOrEmpty(banner.url_link) ? banner.url_link : "";
                            checkID.image_link = (!string.IsNullOrEmpty(banner.image_link) && checkID.image_link != banner.image_link) ? banner.image_link : checkID.image_link;
                            checkID.image = !string.IsNullOrEmpty(banner.image) ? banner.image : checkID.image;
                            checkID.ma_chuyen_muc = !string.IsNullOrEmpty(banner.ma_chuyen_muc) ? banner.ma_chuyen_muc : "";
                            checkID.loai = !string.IsNullOrEmpty(banner.loai) ? banner.loai : "";
                            checkID.vi_tri = !string.IsNullOrEmpty(banner.vi_tri) ? banner.vi_tri : "";
                            checkID.trang_thai = !string.IsNullOrEmpty(banner.trang_thai) ? banner.trang_thai : "";
                            checkID.nguoi_cap_nhat = UserName;
                            checkID.ngay_cap_nhat = DateTime.Now;
                            db.Update(checkID);
                            
                        }
                        else
                        {
                            var lastId = db.FirstOrDefault<Banner>("SELECT TOP 1 * FROM Banner ORDER BY id DESC");
                            if (lastId != null)
                            {
                                if (lastId.ma_banner.Contains("BAN"))
                                {
                                    var nextNo = Int32.Parse(lastId.ma_banner.Substring(3, 10)) + 1;
                                    banner.ma_banner = "BAN" + String.Format("{0:0000000000}", nextNo);
                                }
                            }
                            else
                            {
                                banner.ma_banner = "BAN" + "0000000001";
                            }
                            banner.url_link = !string.IsNullOrEmpty(banner.url_link) ? banner.url_link : "";
                            banner.image_link = (!string.IsNullOrEmpty(banner.image_link) && banner.image_link != banner.image_link) ? banner.image_link : banner.image_link;
                            banner.image = !string.IsNullOrEmpty(banner.image) ? banner.image : "";
                            banner.ma_chuyen_muc = !string.IsNullOrEmpty(banner.ma_chuyen_muc) ? banner.ma_chuyen_muc : "";
                            banner.vi_tri = !string.IsNullOrEmpty(banner.vi_tri) ? banner.vi_tri : "";
                            banner.loai = !string.IsNullOrEmpty(banner.loai) ? banner.loai : "";
                            banner.trang_thai = !string.IsNullOrEmpty(banner.trang_thai) ? banner.trang_thai : AllConstant.trang_thai.DANG_SU_DUNG;
                            banner.nguoi_tao = UserName;
                            banner.ngay_tao = DateTime.Now;
                            banner.nguoi_cap_nhat = UserName;
                            banner.ngay_cap_nhat = DateTime.Now;
                            db.Insert(banner);
                        }
                        //SyncToMySQL
                        List<SqlParameter> lstParameter = new List<SqlParameter>();
                        lstParameter.Clear();
                        lstParameter.Add(new SqlParameter("@ma_banner", banner.ma_banner));
                        new SqlHelper(connectionString).ExecuteNoneQuery("p_Banner_SyncToMySQL", lstParameter);
                    return "true@@"+banner.ma_banner;
                }
                catch (Exception e)
                {
                    return "false@@"+e.Message;
                }
            }
        }
        public DataTable ReadAll(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select * from Banner order by id desc", param);
        }

        public string UpSert(List<Banner> lstBanners, string UserName, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                try
                {
                    foreach (Banner row in lstBanners)
                    {
                        var checkID = db.SingleOrDefault<Banner>("id={0}", row.id);
                        if (checkID != null)
                        {
                            checkID.url_link = !string.IsNullOrEmpty(row.url_link.ToString()) ? row.url_link : "";

                            checkID.image = !string.IsNullOrEmpty(row.image) ? row.image : "";
                            checkID.ma_chuyen_muc = !string.IsNullOrEmpty(row.ma_chuyen_muc) ? row.ma_chuyen_muc : "";
                            checkID.vi_tri = !string.IsNullOrEmpty(row.vi_tri) ? row.vi_tri : "";
                            checkID.alt = !string.IsNullOrEmpty(row.alt) ? row.alt : "";
                            checkID.trang_thai = !string.IsNullOrEmpty(row.trang_thai) ? row.trang_thai : AllConstant.trang_thai.DANG_SU_DUNG;
                            checkID.nguoi_cap_nhat = UserName;
                            checkID.ngay_cap_nhat = DateTime.Now;
                            db.Update(checkID);
                        }
                        else
                        {
                            row.url_link = !string.IsNullOrEmpty(row.url_link.ToString()) ? row.url_link : "";
                            row.image = !string.IsNullOrEmpty(row.image) ? row.image : "";
                            row.ma_chuyen_muc = !string.IsNullOrEmpty(row.ma_chuyen_muc) ? row.ma_chuyen_muc : "";
                            row.vi_tri = !string.IsNullOrEmpty(row.vi_tri) ? row.vi_tri : "";
                            row.trang_thai = !string.IsNullOrEmpty(row.trang_thai) ? row.trang_thai : AllConstant.trang_thai.DANG_SU_DUNG;
                            row.alt = !string.IsNullOrEmpty(row.alt) ? row.alt : "";
                            row.nguoi_tao = UserName;
                            row.ngay_tao = DateTime.Now;
                            row.nguoi_cap_nhat = UserName;
                            row.ngay_cap_nhat = DateTime.Now;
                            db.Insert(row);
                        }
                    }
                    return "true";
                }
                catch (Exception e)
                {
                    return e.Message.ToString();
                }
            }
        }
    }
}
