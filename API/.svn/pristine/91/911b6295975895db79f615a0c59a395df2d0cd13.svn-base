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
    public class Image_Info_DAO
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
                        var checkID = db.SingleOrDefault<Image_Info>("id={0}", ids[0]);
                        foreach (var id in ids)
                        {
                            db.Delete<Image_Info>(s=>s.id==int.Parse(id));                   
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
        public string UpSert(List<Image_Info> list, string UserName, string Type, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
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
                                string ma_tu_tang = String.Empty;
                                var lastId = db.FirstOrDefault<Image_Info>("SELECT TOP 1 * FROM Image_Info ORDER BY id DESC");
                                if (lastId != null)
                                {
                                    if (lastId.ma_thong_tin_anh.Contains("IM"))
                                    {
                                        var nextNo = Int32.Parse(lastId.ma_thong_tin_anh.Substring(2, 13)) + 1;
                                        ma_tu_tang = "IM" + String.Format("{0:0000000000000}", nextNo);
                                    }
                                }
                                else
                                {
                                    ma_tu_tang = "IM0000000000001";
                                }
                                row.ma_thong_tin_anh = ma_tu_tang;
                                row.ngay_tao = DateTime.Now;
                                row.ngay_duyet = row.ngay_cap_nhat =  DateTime.Parse("1900-01-01");
                                row.nguoi_tao = UserName;
                                row.nguoi_cap_nhat = row.nguoi_duyet =  "";
                                db.Insert(row);
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
