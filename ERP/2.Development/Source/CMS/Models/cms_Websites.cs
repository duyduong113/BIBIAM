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
    public class cms_Websites
    {
        [AutoIncrement]
        [PrimaryKey]
        public int id { get; set; }
        public string ma_website { get; set; }
        public string ten_website { get; set; }
        public string gioi_thieu { get; set; }
        public string mo_ta { get; set; }
        public string trang_thai { get; set; }
        public DateTime? ngay_tao { get; set; }
        public string nguoi_tao { get; set; }
        public DateTime? ngay_cap_nhat { get; set; }
        public string nguoi_cap_nhat { get; set; }
        public string url_website { get; set; }

    
        public cms_Websites()
        { }

        public cms_Websites(int ID,string CODE, string NAME, string INTRO, string DES, string STATUS, DateTime CREATEAT, string CREATEBY, DateTime UPDATEAT, string UPDATEBY ,string LINK)
        {
            this.id = ID;
            this.ma_website = CODE;
            this.ten_website = NAME;
            this.gioi_thieu = INTRO;
            this.mo_ta = DES;
            this.trang_thai = STATUS;
            this.ngay_tao = CREATEAT;
            this.nguoi_tao = CREATEBY;
            this.ngay_cap_nhat = UPDATEAT;
            this.nguoi_cap_nhat = UPDATEBY;
            this.url_website = LINK;
        }


        public string Delete(string[] ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    List<SqlParameter> lstParameter = new List<SqlParameter>();
                    lstParameter.Clear();
                    lstParameter.Add(new SqlParameter("@ma_website", id));
                    new SqlHelper().ExecuteNoneQuery("p_Websites_Delete", lstParameter);
                }
                   
                return "true";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        public string UpSert(List<cms_Websites> lstWebsites, string UserName, string Type)
        {
            using (IDbConnection db = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    foreach (cms_Websites row in lstWebsites)
                    {
                        var checkID = db.SingleOrDefault<cms_Websites>("id={0}", row.id);
                        string ma_tu_tang = String.Empty;

                        var lastId = db.FirstOrDefault<cms_Websites>("SELECT TOP 1 * FROM cms_Websites ORDER BY ma_website DESC");
                        if (lastId != null)
                        {
                            var nextNo = Int32.Parse(lastId.ma_website.Substring(3, 3))+1;
                            ma_tu_tang = "WEB" + String.Format("{0:000}", nextNo);
                        }
                        else
                        {
                            ma_tu_tang = "WEB001";
                        }

                        if (checkID != null)
                        {
                            checkID.ten_website = !string.IsNullOrEmpty(row.ten_website.ToString()) ? row.ten_website : "";
                            checkID.gioi_thieu = !string.IsNullOrEmpty(row.gioi_thieu) ? row.gioi_thieu : "";
                            checkID.mo_ta = !string.IsNullOrEmpty(row.mo_ta) ? row.mo_ta : "";
                            checkID.trang_thai = !string.IsNullOrEmpty(row.trang_thai.ToString()) ? row.trang_thai : "";
                            checkID.nguoi_cap_nhat = UserName;
                            checkID.ngay_cap_nhat = DateTime.Now;
                            db.Update(checkID);
                        }
                        else
                        {
                            row.ma_website = ma_tu_tang;
                            row.ten_website = !string.IsNullOrEmpty(row.ten_website.ToString()) ? row.ten_website : "";
                            row.gioi_thieu = !string.IsNullOrEmpty(row.gioi_thieu) ? row.gioi_thieu : "";
                            row.mo_ta = !string.IsNullOrEmpty(row.mo_ta) ? row.mo_ta : "";
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