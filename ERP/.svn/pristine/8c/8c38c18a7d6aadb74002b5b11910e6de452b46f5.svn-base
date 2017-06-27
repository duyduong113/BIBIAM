using CMS.Helpers;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class cms_WCL
    {
        [AutoIncrement]
        [PrimaryKey]
        public int id { get; set; }
        public string vi_tri { get; set; }
        public string website { get; set; }
        public string chuyen_muc { get; set; }
        public string trang_thai { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string nguoi_tao { get; set; }
        public DateTime? ngay_cap_nhat { get; set; }
        public string nguoi_cap_nhat { get; set; }

        public cms_WCL()
        { }

        public cms_WCL(int ID, string POS, string WEB, string CATE, string STATUS, DateTime CREATEAT, string CREATEBY, DateTime UPDATEAT, string UPDATEBY)
        {
            this.id = ID;
            this.vi_tri = POS;
            this.website = WEB;
            this.chuyen_muc = CATE;
            this.trang_thai = STATUS;
            this.ngay_tao = CREATEAT;
            this.nguoi_tao = CREATEBY;
            this.ngay_cap_nhat = UPDATEAT;
            this.nguoi_cap_nhat = UPDATEBY;
        }

        public string Delete(string[] ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    List<SqlParameter> lstParameter = new List<SqlParameter>();
                    lstParameter.Clear();
                    lstParameter.Add(new SqlParameter("@id", id));
                    new SqlHelper().ExecuteNoneQuery("p_WCL_Delete", lstParameter);
                }

                return "true";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        public string UpSert(List<cms_WCL> lstWCLs, string UserName, string Type, string ma_vi_tri)
        {
            using (IDbConnection db = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    foreach (cms_WCL row in lstWCLs)
                    {
                        var checkID = db.SingleOrDefault<cms_WCL>("id={0}", row.id);
                        var checkUpd = db.SingleOrDefault<cms_WCL>("website={0} and id={1} and chuyen_muc={2} and vi_tri={3} ", row.website, row.id, row.chuyen_muc, ma_vi_tri);
                        var checkIns = db.SingleOrDefault<cms_WCL>("website={0} and chuyen_muc={1} and vi_tri={2}", row.website, row.chuyen_muc,ma_vi_tri);
                        if (checkUpd == null)
                            if (checkIns != null)
                                return "exist_ma_chuyen_muc";
                        if (checkID != null)
                        {
                            //checkID.vi_tri = !string.IsNullOrEmpty(row.vi_tri.ToString()) ? row.vi_tri : "";
                            checkID.website = !string.IsNullOrEmpty(row.website.ToString()) ? row.website : "";
                            checkID.chuyen_muc = !string.IsNullOrEmpty(row.chuyen_muc) ? row.chuyen_muc : "";
                            checkID.trang_thai = !string.IsNullOrEmpty(row.trang_thai) ? row.trang_thai : "";
                            checkID.nguoi_cap_nhat = UserName;
                            checkID.ngay_cap_nhat = DateTime.Now;
                            db.Update(checkID);
                        }
                        else
                        {
                            row.vi_tri = !string.IsNullOrEmpty(ma_vi_tri) ? ma_vi_tri : "";
                            row.website = !string.IsNullOrEmpty(row.website.ToString()) ? row.website : "";
                            row.chuyen_muc = !string.IsNullOrEmpty(row.chuyen_muc) ? row.chuyen_muc : "";
                            row.trang_thai = !string.IsNullOrEmpty(row.trang_thai) ? row.trang_thai : "";
                            row.nguoi_tao = UserName;
                            row.ngay_tao = DateTime.Now;
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