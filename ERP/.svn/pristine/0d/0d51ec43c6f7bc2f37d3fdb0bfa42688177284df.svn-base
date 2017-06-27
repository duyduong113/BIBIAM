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
    public class cms_Categorys
    {
        [AutoIncrement]
        [PrimaryKey]
        public int id { get; set; }
        public string ma_chuyen_muc { get; set; }
        public string ten_chuyen_muc { get; set; }
        public string website { get; set; }
        public string trang_thai { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string nguoi_tao { get; set; }
        public DateTime? ngay_cap_nhat { get; set; }
        public string nguoi_cap_nhat { get; set; }


        public cms_Categorys()
        { }

        public cms_Categorys(int ID, string CODE, string NAME,string WEB, string STATUS, DateTime CREATEAT, string CREATEBY, DateTime UPDATEAT, string UPDATEBY)
        {
            this.id = ID;
            this.ma_chuyen_muc = CODE;
            this.ten_chuyen_muc = NAME;
            this.website = WEB;
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
                    lstParameter.Add(new SqlParameter("@ma_chuyen_muc", id));
                    new SqlHelper().ExecuteNoneQuery("p_Categorys_Delete", lstParameter);
                }

                return "true";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        public string UpSert(List<cms_Categorys> lstCategorys, string UserName, string Type)
        {
            using (IDbConnection db = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    foreach (cms_Categorys row in lstCategorys)
                    {
                        var checkID = db.SingleOrDefault<cms_Categorys>("id={0}", row.id);
                      
                        if (checkID != null)
                        {
                            if (checkID.website != row.website)
                            {
                                string ma_tu_tang = String.Empty;
                                var lastId = db.FirstOrDefault<cms_Categorys>
                                    ("SELECT TOP 1 * FROM cms_Categorys where website={0} ORDER BY ma_chuyen_muc DESC".Params(row.website));
                                if (lastId != null)
                                {
                                    if (lastId.ma_chuyen_muc.Contains(row.website))
                                    {
                                        var nextNo = Int32.Parse(lastId.ma_chuyen_muc.Substring(row.website.Length + 4, 2)) + 1;
                                        ma_tu_tang = row.website + "_CM" + String.Format("{0:000}", nextNo);
                                    }
                                }
                                else
                                {
                                    ma_tu_tang = row.website + "_CM001";
                                }
                                checkID.ma_chuyen_muc = ma_tu_tang;
                            }
                            checkID.ten_chuyen_muc = !string.IsNullOrEmpty(row.ten_chuyen_muc.ToString()) ? row.ten_chuyen_muc : "";
                            checkID.website= !string.IsNullOrEmpty(row.website) ? row.website : "";
                            checkID.trang_thai = !string.IsNullOrEmpty(row.trang_thai.ToString()) ? row.trang_thai : "";
                            checkID.nguoi_cap_nhat = UserName;
                            checkID.ngay_cap_nhat = DateTime.Now;
                            db.Update(checkID);
                        }
                        else
                        {
                            string ma_tu_tang = String.Empty;
                            var lastId = db.FirstOrDefault<cms_Categorys>
                                ("SELECT TOP 1 * FROM cms_Categorys where website={0} ORDER BY ma_chuyen_muc DESC".Params(row.website));
                            if (lastId != null)
                            {
                                if (lastId.ma_chuyen_muc.Contains(row.website))
                                {
                                    var nextNo = Int32.Parse(lastId.ma_chuyen_muc.Substring(row.website.Length + 4, 2)) + 1;
                                    ma_tu_tang = row.website + "_CM" + String.Format("{0:000}", nextNo);
                                }
                            }
                            else
                            {
                                ma_tu_tang = row.website + "_CM001";
                            }

                            row.ma_chuyen_muc = ma_tu_tang;
                            row.ma_chuyen_muc = !string.IsNullOrEmpty(row.ma_chuyen_muc.ToString()) ? row.ma_chuyen_muc : "";
                            row.ten_chuyen_muc = !string.IsNullOrEmpty(row.ten_chuyen_muc.ToString()) ? row.ten_chuyen_muc : "";
                            row.website = !string.IsNullOrEmpty(row.website) ? row.website : "";
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