using BIBIAM.Core.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using BIBIAM.Core.Entities;

namespace BIBIAM.Core.Data.DataObject
{
    public class Product_Hierarchy_DAO
    {
         public DataTable ReadAll(string connectionString)
        {
            //List<SqlParameter> param = new List<SqlParameter>();
            //return new SqlHelper().ExecuteString("select a.*,b.ten_san_pham from Product_Hierarchy a right join Product_Info b on a.ma_san_pham = b.ma_san_pham order by id desc", param);        
            //using (IDbConnection dbConn = new OrmliteConnection().openConn())
            //{
            //    var data = dbConn.SqlList<DataTable>("p_Get_Product_Hirerchy");
            //    return data.First();
            //}
            return new SqlHelper(connectionString).ExecuteQuery("p_Get_Product_Hirerchy", new List<SqlParameter>());
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
                            db.Delete<Product_Hierarchy>(s=>s.id==int.Parse(id));
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

        public string UpSert(List<Product_Hierarchy> list, string UserName, string Type, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (Product_Hierarchy row in list)
                        {
                            var exit = db.FirstOrDefault<Product_Hierarchy>(s=>s.id==row.id);
                            if (exit != null)
                            {
                                row.ngay_tao = exit.ngay_tao;
                                row.nguoi_tao = exit.nguoi_tao;
                                row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_cap_nhat = UserName;
                                db.Update(EmptyIfNull(row));
                            }
                            else
                            {
                                row.trang_thai = AllConstant.trang_thai.DANG_SU_DUNG;
                                row.ngay_tao = DateTime.Now;
                                row.ngay_cap_nhat = DateTime.Parse("1900-01-01");
                                row.nguoi_tao = UserName;
                                row.nguoi_cap_nhat = "";
                                db.Insert(EmptyIfNull(row));
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
        private Product_Hierarchy EmptyIfNull(Product_Hierarchy p)
        {
            p.ma_cay_phan_cap_1 = String.IsNullOrEmpty(p.ma_cay_phan_cap_1) ? "" : p.ma_cay_phan_cap_1;
            p.ma_cay_phan_cap_2 = String.IsNullOrEmpty(p.ma_cay_phan_cap_2) ? "" : p.ma_cay_phan_cap_2;
            p.ma_cay_phan_cap_3 = String.IsNullOrEmpty(p.ma_cay_phan_cap_3) ? "" : p.ma_cay_phan_cap_3;
            p.ma_cay_phan_cap_4 = String.IsNullOrEmpty(p.ma_cay_phan_cap_4) ? "" : p.ma_cay_phan_cap_4;
            p.ma_cay_phan_cap_5 = String.IsNullOrEmpty(p.ma_cay_phan_cap_5) ? "" : p.ma_cay_phan_cap_5;
            p.ma_cay_phan_cap_6 = String.IsNullOrEmpty(p.ma_cay_phan_cap_6) ? "" : p.ma_cay_phan_cap_6;
            p.ma_cay_phan_cap_7 = String.IsNullOrEmpty(p.ma_cay_phan_cap_7) ? "" : p.ma_cay_phan_cap_7;
            p.ma_cay_phan_cap_8 = String.IsNullOrEmpty(p.ma_cay_phan_cap_8) ? "" : p.ma_cay_phan_cap_8;
            p.ma_cay_phan_cap_9 = String.IsNullOrEmpty(p.ma_cay_phan_cap_9) ? "" : p.ma_cay_phan_cap_9;
            p.ma_cay_phan_cap_10 = String.IsNullOrEmpty(p.ma_cay_phan_cap_10) ? "" : p.ma_cay_phan_cap_10;  
            return p;
        }
    }
}