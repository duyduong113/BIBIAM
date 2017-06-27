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
    public class Merchant_Image_Info_DAO
    {
        public DataTable ReadAll(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select * from Image_Info", new List<SqlParameter>());
        }

        public string Delete(string[] ids, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var checkID = db.SingleOrDefault<Merchant_Image_Info>("id={0}", ids[0]);
                        foreach (var id in ids)
                        {
                            db.Delete<Merchant_Image_Info>(s => s.id == int.Parse(id));
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

        public string UpSert(List<Merchant_Image_Info> list, string UserName, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
               // using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (Merchant_Image_Info row in list)
                        {
                            var checkID = db.SingleOrDefault<Merchant_Image_Info>("id={0}", row.id);
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
                                List<SqlParameter> lstParameter = new List<SqlParameter>();
                                lstParameter.Add(new SqlParameter("@ma_gian_hang", row.ma_gian_hang));
                                lstParameter.Add(new SqlParameter("@ten_anh", row.ten_anh));
                                lstParameter.Add(new SqlParameter("@loai", row.loai));
                                lstParameter.Add(new SqlParameter("@ma_anh_goc", row.ma_anh_goc));
                                lstParameter.Add(new SqlParameter("@mo_ta", row.mo_ta));
                                lstParameter.Add(new SqlParameter("@mo_ta_khong_dau", row.mo_ta_khong_dau));
                                lstParameter.Add(new SqlParameter("@thu_muc", row.thu_muc));
                                lstParameter.Add(new SqlParameter("@url", row.url));
                                lstParameter.Add(new SqlParameter("@duong_dan_day_du", row.duong_dan_day_du));
                                lstParameter.Add(new SqlParameter("@dung_luong", row.dung_luong));
                                lstParameter.Add(new SqlParameter("@chieu_rong", row.chieu_rong));
                                lstParameter.Add(new SqlParameter("@chieu_cao", row.chieu_cao));
                                lstParameter.Add(new SqlParameter("@nguoi_tao", row.nguoi_tao));
                                lstParameter.Add(new SqlParameter("@nguoi_cap_nhat", row.nguoi_cap_nhat));
                                new SqlHelper(connectionString).ExecuteNoneQuery("p_Merchant_Image_Info_Create", lstParameter);
                            }
                        }
                       // dbTrans.Commit();
                        return "true";
                    }
                    catch (Exception e)
                    {
                     //   dbTrans.Rollback();
                        return e.Message.ToString();
                    }
                }
            }
        }
    }
}
