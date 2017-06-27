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
    public class Image_Info
    {
        [AutoIncrement]
        [PrimaryKey]
        public int id { get; set; }
        public string ma_website { get; set; }
        public string ma_thong_tin_anh { get; set; }
        public string ten_anh { get; set; }
        public int loai { get; set; }
        public string ma_anh_goc { get; set; }
        public string mo_ta { get; set; }
        public string mo_ta_khong_dau { get; set; }
        public string thu_muc { get; set; }
        public string url { get; set; }
        public string duong_dan_day_du { get; set; }
        public float dung_luong { get; set; }
        public float chieu_rong { get; set; }
        public float chieu_cao { get; set; }
        public string nguoi_duyet { get; set; }
        public DateTime ngay_duyet { get; set; }
        public string trang_thai_duyet { get; set; }
        public string trang_thai { get; set; }
        public DateTime ngay_tao { get; set; }
        public string nguoi_tao { get; set; }
        public DateTime ngay_cap_nhat { get; set; }
        public string nguoi_cap_nhat { get; set; }
        public Image_Info()
        { }
        public string UpSert(List<Image_Info> list, string UserName)
        {
            using (IDbConnection db = Helpers.OrmliteConnection.openConn())
            {
                {
                    try
                    {
                        foreach (Image_Info row in list)
                        {
                            var checkID = db.SingleOrDefault<Image_Info>("id={0}", row.id);
                            if (checkID != null)
                            {
                                row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_cap_nhat = UserName;
                                row.nguoi_tao = checkID.nguoi_tao;
                                row.ngay_tao = checkID.ngay_tao;
                                db.Update(row);
                            }
                            else
                            {
                                row.nguoi_tao = UserName;
                                row.nguoi_cap_nhat = UserName;
                                row.nguoi_duyet = UserName;
                                row.ngay_tao = DateTime.Now;
                                row.ngay_cap_nhat = DateTime.Now;
                                row.ngay_duyet = DateTime.Now;
                                var lastId = db.FirstOrDefault<Image_Info>("SELECT TOP 1 * FROM Image_Info ORDER BY id DESC");
                                if (lastId != null)
                                {
                                    if (lastId.ma_thong_tin_anh.Contains("IM"))
                                    {
                                        var nextNo = Int32.Parse(lastId.ma_thong_tin_anh.Substring(2, 13)) + 1;
                                        row.ma_thong_tin_anh = "IM" + String.Format("{0:0000000000000}", nextNo);
                                    }
                                }
                                else
                                {
                                    row.ma_thong_tin_anh = "IM" + "0000000000001";
                                }
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

}