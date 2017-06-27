using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BIBIAM.Core.Entities;
using BIBIAM.Core.Data.Providers;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using BIBIAM.Core.Data;

namespace BIBIAM.Core.Data.DataObject
{
    public class Merchant_History_DAO
    {
        public string createHistory(string TableName, string MerchantID, string Value, string Username, string connectionString = "")
        {
            using (var db = connectionString != "" ? new OrmliteConnection().openConn(connectionString) : new OrmliteConnection().openConn())
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {                        
                        db.ExecuteNonQuery("update Merchant_History set trang_thai = {2} where ten_bang = {0} and ma_gian_hang = {1}".Params(TableName, MerchantID, AllConstant.trang_thai.KHONG_SU_DUNG));
                        Merchant_History history = new Merchant_History();
                        history.ten_bang = TableName;
                        history.ma_gian_hang = MerchantID;
                        history.ngay_tao = DateTime.Now;
                        history.nguoi_tao = Username;
                        history.gia_tri = Value;
                        history.trang_thai = AllConstant.trang_thai.DANG_SU_DUNG;
                        db.Insert<Merchant_History>(history);
                        dbTrans.Commit();
                        return "true";
                    }
                    catch(Exception e)
                    {
                        dbTrans.Rollback();
                        return e.Message.ToString();
                    }                    
                }
            }            
        }
        public string createHistory(string TableName, string MerchantID, string Value, string Username, IDbConnection db)
        {
            try
            {
                db.ExecuteNonQuery("update Merchant_History set trang_thai = {2} where ten_bang = {0} and ma_gian_hang = {1}".Params(TableName, MerchantID, AllConstant.trang_thai.KHONG_SU_DUNG));
                Merchant_History history = new Merchant_History();
                history.ten_bang = TableName;
                history.ma_gian_hang = MerchantID;
                history.ngay_tao = DateTime.Now;
                history.nguoi_tao = Username;
                history.gia_tri = Value;
                history.trang_thai = AllConstant.trang_thai.DANG_SU_DUNG;
                db.Insert<Merchant_History>(history);
                return "true";
            }
            catch (Exception e)
            {                       
                return e.Message.ToString();
            }
        }
        public string getValueHistory(string TableName, string MerchantID, string connectionString = "")
        {
            using (var db = connectionString != "" ? new OrmliteConnection().openConn(connectionString) : new OrmliteConnection().openConn())
            {
                var  data= db.FirstOrDefault<Merchant_History>("trang_thai = {0} and ten_bang = {1} and ma_gian_hang = {2} ", AllConstant.trang_thai.DANG_SU_DUNG, TableName, MerchantID);
                if (data != null)
                {
                    return data.gia_tri;
                }
                else
                    return "";
            }
        }
        public string getValueHistory(string TableName, string MerchantID, IDbConnection db)
        {          
            return db.FirstOrDefault<Merchant_History>("trang_thai = {0} and ten_bang = {1} and ma_gian_hang = {2} ", AllConstant.trang_thai.DANG_SU_DUNG, TableName, MerchantID).gia_tri;
        }
    }      
}
