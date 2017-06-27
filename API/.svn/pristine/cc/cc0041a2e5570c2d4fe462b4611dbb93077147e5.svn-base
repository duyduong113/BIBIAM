using BIBIAM.Core.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using BIBIAM.Core.Entities;


namespace BIBIAM.Core.Data.DataObject
{
    public class Product_Suggestion_DAO
    {
        public DataTable ReadAll(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select * from Product_Suggestion order by id desc", param);
        }
        public int Delete(string[] ids, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (var id in ids)
                        {
                            List<SqlParameter> param = new List<SqlParameter>();
                            param.Add(new SqlParameter("@id", id));
                            new SqlHelper().ExecuteNoneQuery("delete Product_Suggestion where id = @id", param, CommandType.Text);
                        }
                        dbTrans.Commit();
                        return 1;
                    }
                    catch
                    {
                        dbTrans.Rollback();
                        return 0;
                    }
                }
            }
        }
        public void UpSert(List<Product_Suggestion> lstProductSuggestion, string UserName, string Type, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {

                        foreach (Product_Suggestion row in lstProductSuggestion)
                        {
                            if (Type == "Insert")
                            {
                                row.ngay_tao = row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_tao = row.nguoi_cap_nhat = UserName;
                                db.Save(row);
                            }
                            else
                            {
                                row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_cap_nhat = UserName;
                                db.Update(row);
                            }
                        }
                        dbTrans.Commit();
                    }
                    catch
                    {
                        dbTrans.Rollback();
                    }
                }
            }
        }
    }
}