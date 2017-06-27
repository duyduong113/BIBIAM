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
    public class cms_BoxTin
    {
        [AutoIncrement]
        [PrimaryKey]
        public int id { get; set; }
        public string ma_box_tin { get; set; }
        public string ten_box_tin { get; set; }
        public string url_link { get; set; }
        public string ma_chuyen_muc { get; set; }
        public string ma_vi_tri { get; set; }
        public string trang_thai { get; set; }
        public DateTime ngay_tao { get; set; }
        public string nguoi_tao { get; set; }
        public DateTime ngay_cap_nhat { get; set; }
        public string nguoi_cap_nhat { get; set; }
        public int orders { get; set; }
        public string ma_website { get; set; }
        public cms_BoxTin()
        { }
        public cms_BoxTin(int ID, string CODE, string NAME, string URL, string TYPE, string CATEGOGY,string POSITION, string STATUS, DateTime CREATEAT, string CREATEBY, DateTime UPDATEAT, string UPDATEBY,int ORDERS,string WEB)
        {
            this.id = ID;
            this.ma_box_tin = CODE;
            this.ten_box_tin = NAME;
            this.url_link = URL;
            this.ma_chuyen_muc = CATEGOGY;
            this.ma_vi_tri = POSITION;
            this.trang_thai = STATUS;
            this.ngay_tao = CREATEAT;
            this.nguoi_tao = CREATEBY;
            this.nguoi_cap_nhat = CREATEBY;
            this.ngay_cap_nhat = CREATEAT;
            this.orders = ORDERS;
            this.ma_website = WEB;
        }
        public string Delete(string[] ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    List<SqlParameter> lstParameter = new List<SqlParameter>();
                    lstParameter.Clear();
                    lstParameter.Add(new SqlParameter("@id", int.Parse(id)));
                    new SqlHelper().ExecuteNoneQuery("p_BoxTin_Delete", lstParameter);
                }

                return "true";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }
        public string UpSert(List<cms_BoxTin> lstBxTin, string UserName, string Type)
        {
            using (IDbConnection db = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    foreach (cms_BoxTin row in lstBxTin)
                    {
                        var checkID = db.SingleOrDefault<cms_BoxTin>("id={0}", row.id);
                        if (checkID != null)
                        {
                            var check = db.SingleOrDefault<cms_BoxTin>("ma_box_tin={0} and id={1}", row.ma_box_tin, row.id);
                            if (check == null)
                            {
                                var checkExist = db.SingleOrDefault<cms_BoxTin>("ma_box_tin={0}", row.ma_box_tin);
                                if (checkExist != null)
                                    return "Exist";
                            }

                            checkID.ma_chuyen_muc = !string.IsNullOrEmpty(row.ma_chuyen_muc) ? row.ma_chuyen_muc : "";
                            checkID.ma_vi_tri = !string.IsNullOrEmpty(row.ma_vi_tri) ? row.ma_vi_tri : "";
                            checkID.ma_website = !string.IsNullOrEmpty(row.ma_website) ? row.ma_website : "";
                            checkID.orders = row.orders!=0 ? row.orders : 0;
                            checkID.ten_box_tin = !string.IsNullOrEmpty(row.ten_box_tin) ? row.ten_box_tin : "";
                            checkID.trang_thai = !string.IsNullOrEmpty(row.trang_thai) ? row.trang_thai : "";
                            checkID.nguoi_cap_nhat = UserName;
                            checkID.ngay_cap_nhat = DateTime.Now;
                            db.Update(checkID);
                        }
                        else
                        {
                            var checkExist = db.SingleOrDefault<cms_BoxTin>("ma_box_tin={0}", row.ma_box_tin);
                            if (checkExist != null)
                                return "Exist";
                            //sinh mã tự động
                            string ma_tu_tang = String.Empty;
                            var lastId = db.FirstOrDefault<cms_BoxTin>("SELECT TOP 1 * FROM cms_BoxTin ORDER BY id DESC");
                            if (lastId != null)
                            {
                                if (lastId.ma_box_tin.Contains("BOX"))
                                {
                                    var nextNo = Int32.Parse(lastId.ma_box_tin.Substring(3, 3)) + 1;
                                    ma_tu_tang = "BOX" + String.Format("{0:000}", nextNo);
                                }
                            }
                            else
                            {
                                ma_tu_tang = "BOX001";
                            }
                            row.ma_box_tin = ma_tu_tang;
                            row.ma_vi_tri = !string.IsNullOrEmpty(row.ma_vi_tri.ToString()) ? row.ma_vi_tri : "";
                            row.ma_chuyen_muc = !string.IsNullOrEmpty(row.ma_chuyen_muc) ? row.ma_chuyen_muc : "";
                            row.ma_website = !string.IsNullOrEmpty(row.ma_website) ? row.ma_website : "";
                            row.orders = row.orders!=0 ? row.orders : 0;
                            row.ten_box_tin = !string.IsNullOrEmpty(row.ten_box_tin) ? row.ten_box_tin : "";
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