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
    public class Merchant_Catalog_DAO
    {
        public string UpSert(Merchant_Catalog catalog, string UserName, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                try
                {
                    var checkID = db.SingleOrDefault<Merchant_Catalog>("id={0}", catalog.id);
                    if (checkID != null)
                    {
                        catalog.ngay_cap_nhat = DateTime.Now;
                        catalog.nguoi_cap_nhat = UserName;
                        catalog.nguoi_tao = checkID.nguoi_tao;
                        catalog.ngay_tao = checkID.ngay_tao;
                        db.Update(catalog);
                    }
                    else
                    {
                        catalog.nguoi_tao = UserName;
                        catalog.nguoi_cap_nhat = UserName;
                        catalog.ngay_tao = DateTime.Now;
                        catalog.ngay_cap_nhat = DateTime.Now;
                        var lastId = db.FirstOrDefault<Merchant_Catalog>("SELECT TOP 1 * FROM Merchant_Catalog ORDER BY id DESC");
                        if (lastId != null)
                        {
                            if (lastId.ma_catalog.Contains("CAT"))
                            {
                                var nextNo = Int32.Parse(lastId.ma_catalog.Substring(3, 10)) + 1;
                                catalog.ma_catalog = "CAT" + String.Format("{0:0000000000}", nextNo);
                            }
                        }
                        else
                        {
                            catalog.ma_catalog = "CAT" + "0000000001";
                        }
                        db.Insert(catalog);
                    }
                    return "true";
                }
                catch (Exception e)
                {
                    return e.Message.ToString();
                }
            }
        }
        public string Delete(string[] ids, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                try
                {
                    foreach (var id in ids)
                    {
                        db.Delete<Merchant_Catalog>(s => s.id == int.Parse(id));
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

