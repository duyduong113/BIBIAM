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
    public class cms_Positions
    {
        [AutoIncrement]
        [PrimaryKey]
        public int id { get; set; }
        public string ma_vi_tri { get; set; }
        public string ten_vi_tri { get; set; }
        public string loai_vi_tri { get; set; }
        public string trang_thai { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string nguoi_tao { get; set; }
        public DateTime? ngay_cap_nhat { get; set; }
        public string nguoi_cap_nhat { get; set; }


        public cms_Positions()
        { }

        public cms_Positions(int ID, string CODE, string NAME,string TYPE,  string STATUS, DateTime CREATEAT, string CREATEBY, DateTime UPDATEAT, string UPDATEBY)
        {
            this.id = ID;
            this.ma_vi_tri = CODE;
            this.ten_vi_tri = NAME;
            this.loai_vi_tri = TYPE;
            this.trang_thai = STATUS;
            this.ngay_tao = CREATEAT;
            this.nguoi_tao = CREATEBY;
            this.ngay_cap_nhat = UPDATEAT;
            this.nguoi_cap_nhat = UPDATEBY;
        }

        public string Delete(string[] ids)
        {
            using (IDbConnection db = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    int i = 0;
                    foreach (var id in ids)
                    {
                        var ma_vi_tri = db.Select<cms_WCL>(c => c.vi_tri == id);
                        if (ma_vi_tri.Count() == 0)// kiểm tra mã vị trí trong bảng cms_WCL
                        {
                            i++;
                            List<SqlParameter> lstParameter = new List<SqlParameter>();
                            lstParameter.Clear();
                            lstParameter.Add(new SqlParameter("@ma_vi_tri", id));
                            new SqlHelper().ExecuteNoneQuery("p_Positions_Delete", lstParameter);
                        }
                    }
                    if (i == ids.Length)
                        return "true";
                    else
                        return "exist";
                }
                catch (Exception e)
                {
                    return e.Message.ToString();
                }
            }
        }

        public string UpSert(List<cms_Positions> lstCategorys, string UserName, string Type)
        {
            using (IDbConnection db = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    foreach (cms_Positions row in lstCategorys)
                    {
                        var checkID = db.SingleOrDefault<cms_Positions>("id={0}", row.id);
                        var checkUpd = db.SingleOrDefault<cms_Positions>("ma_vi_tri={0} and id={1}", row.ma_vi_tri, row.id);
                        var checkIns = db.SingleOrDefault<cms_Positions>("ma_vi_tri={0}", row.ma_vi_tri);
                        if (checkUpd == null)
                            if (checkIns != null)
                                return "exist_ma_vi_tri";
                        if (checkID != null)
                        {
                            checkID.ten_vi_tri = !string.IsNullOrEmpty(row.ten_vi_tri.ToString()) ? row.ten_vi_tri : "";
                            checkID.loai_vi_tri= !string.IsNullOrEmpty(row.loai_vi_tri) ? row.loai_vi_tri : "";
                            checkID.trang_thai = !string.IsNullOrEmpty(row.trang_thai.ToString()) ? row.trang_thai : "";
                            checkID.nguoi_cap_nhat = UserName;
                            checkID.ngay_cap_nhat = DateTime.Now;
                            db.Update(checkID);
                        }
                        else
                        {
                            row.ma_vi_tri = !string.IsNullOrEmpty(row.ma_vi_tri.ToString()) ? row.ma_vi_tri : "";
                            row.ten_vi_tri = !string.IsNullOrEmpty(row.ten_vi_tri.ToString()) ? row.ten_vi_tri : "";
                            row.loai_vi_tri = !string.IsNullOrEmpty(row.loai_vi_tri) ? row.loai_vi_tri : "";
                            row.trang_thai = !string.IsNullOrEmpty(row.trang_thai.ToString()) ? row.trang_thai : "";
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