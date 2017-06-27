using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
//using BIBIAM.Core.Entities;
//using BIBIAM.Core.Data.Providers;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using System.IO;
using System.Web;
using CMS.Helpers;

namespace CMS.Models
{
    public class cms_Merchant_Folder_Info_DAO
    {
        public string Insert(cms_Merchant_Folder_Info item)
        {
            using (IDbConnection db = Helpers.OrmliteConnection.openConn())
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var checkID = db.FirstOrDefault<cms_Merchant_Folder_Info>(s => s.ten_thu_muc == item.ten_thu_muc && s.ma_gian_hang == item.ma_gian_hang);
                        if (checkID != null)
                        {
                            return "Folder was exists";
                        }
                        else
                        {
                            item.ngay_tao = DateTime.Now;
                            db.Insert(item);
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
