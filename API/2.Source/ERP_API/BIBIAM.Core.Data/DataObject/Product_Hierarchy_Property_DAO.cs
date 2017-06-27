using BIBIAM.Core.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using BIBIAM.Core.Entities;


namespace BIBIAM.Core.Data.DataObject
{
    public class Product_Hierarchy_Property_DAO
    {
        public DataTable ReadAll(string connectionString)
        {
            return new SqlHelper(connectionString).SelectQuery("select * from Product_Hierarchy_Property where trang_thai=1 order by id desc");
        }
        public string Delete(string[] ids, string connectionString)
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
                            new SqlHelper().ExecuteNoneQuery("delete Product_Hierarchy_Property where id = @id", param, CommandType.Text);
                        }
                        dbTrans.Commit();
                        return "true";
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return ex.Message;
                    }
                }
            }
        }
        public string UpSert(List<Product_Hierarchy_Property> lstProductHierarchyProperty, string UserName, string Type, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (Product_Hierarchy_Property row in lstProductHierarchyProperty)
                        {
                            if (Type == "Insert")
                            {
                                row.ngay_tao = row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_tao = row.nguoi_cap_nhat = UserName;
                                db.Insert(row);
                            }
                            else
                            {
                                row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_cap_nhat = UserName;
                                db.Update(row);
                            }
                        }
                        dbTrans.Commit();
                        return "true";
                    }
                    catch(Exception ex)
                    {
                      
                        dbTrans.Rollback();
                        return ex.Message;
                    }
                }
            }
        }
        public void setDetail(ref List<Product_Hierarchy_Property> list, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                foreach (Product_Hierarchy_Property item in list)
                {
                    item.detail = db.Select<Property_Detail>("select * from ERPAPD..Property_Detail where ma_thong_so ={0}", item.ma_thong_so);
                }
            }
        }
    }
}