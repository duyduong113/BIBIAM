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
    public class Merchant_Product_Promotion_Detail_DAO
    {
        public DataTable ReadAll(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select * from Merchant_Product_Promotion_Detail", param);
        }
        public string Delete(string[] ids, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var checkID = db.SingleOrDefault<Merchant_Product>("id={0}", ids[0]);
                        foreach (var id in ids)
                        {
                            List<SqlParameter> param = new List<SqlParameter>();
                            param.Add(new SqlParameter("@id", id));
                            new SqlHelper().ExecuteNoneQuery("delete Merchant_Product_Promotion_Detail where id = @id", param, CommandType.Text);
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
        public string Upsert(List<Merchant_Product_Promotion_Detail> list, string ma_chuong_trinh_km, string UserName, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (Merchant_Product_Promotion_Detail item in list)
                        {
                            var checkID = db.SingleOrDefault<Merchant_Product_Promotion_Detail>("id={0}", item.id);
                            if (checkID != null)
                            {
                                checkID.loai = item.loai;
                                checkID.gia_tri = item.gia_tri;
                                db.Update(checkID);
                            }
                            else
                            {
                                string PromotionID = String.Empty;
                                var lastId = db.FirstOrDefault<Merchant_Product_Promotion_Detail>("SELECT TOP 1 * FROM Merchant_Product_Promotion ORDER BY id DESC");
                                if (lastId != null)
                                {
                                    if (lastId.ma_chuong_trinh_km.Contains("KM"))
                                    {
                                        var nextNo = Int32.Parse(lastId.ma_chuong_trinh_km.Substring(2, 7)) + 1;
                                        PromotionID = "KM" + String.Format("{0:0000000}", nextNo);
                                    }
                                }
                                else
                                {
                                    PromotionID = "KM" + "0000001";
                                }                                
                                item.ma_chuong_trinh_km = PromotionID;
                                item.ngay_tao = DateTime.Now;
                                item.nguoi_tao = UserName;                                
                                db.Insert(item);
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
    }
}
