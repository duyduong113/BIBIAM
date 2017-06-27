using BIBIAM.Core.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using BIBIAM.Core.Entities;

namespace BIBIAM.Core.Data.DataObject
{
    public class Product_Property_DAO
    {
        public DataTable ReadAll(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select * from Product_Property order by id desc", param);
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
                            new SqlHelper().ExecuteNoneQuery("delete Product_Property where id = @id", param, CommandType.Text);
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
        public void UpSert(List<Product_Property> lstProduct_Property, string UserName, string Type, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {

                        foreach (Product_Property row in lstProduct_Property)
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

        public List<Product_Property> ConvertDatatableToList(DataTable dt)
        {
            List<Product_Property> lstResult = new List<Product_Property>();

            foreach(DataRow row in dt.Rows)
            {
                Product_Property item = new Product_Property();
                item.id = int.Parse(row["id"].ToString());
                item.ma_san_pham = row["ma_san_pham"].ToString();
                item.ma_gian_hang = row["ma_gian_hang"].ToString();
                item.ma_thong_so = row["ma_thong_so"].ToString();
                item.ten_thong_so = row["ten_thong_so"].ToString();
                item.gia_tri_thong_so = row["gia_tri_thong_so"].ToString();
                item.loai = row["loai"].ToString();
            }
            return lstResult;
        }
    }
}
