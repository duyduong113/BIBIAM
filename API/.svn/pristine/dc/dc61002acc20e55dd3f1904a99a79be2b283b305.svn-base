using BIBIAM.Core.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using BIBIAM.Core.Entities;

namespace BIBIAM.Core.Data.DataObject
{
    public class SEO_MerchantProduct_DAO
    {
        public string UpSert(List<SEO_MerchantProduct> lstData, string UserName, string connectionString)
        {
            using (var dbConn = new OrmliteConnection().openConn(connectionString))
            {
                try
                {
                    foreach (SEO_MerchantProduct item in lstData)
                    {
                        if (item.id > 0)
                        {
                            var exist = dbConn.FirstOrDefault<SEO_MerchantProduct>(s => s.ma_san_pham == item.ma_san_pham);
                            exist.og_description = !string.IsNullOrEmpty(item.og_description) ? item.og_description : "";
                            exist.og_title = !string.IsNullOrEmpty(item.og_title) ? item.og_title : "";
                            exist.og_image = !string.IsNullOrEmpty(item.og_image) ? item.og_image : "";
                            exist.og_keyword = !string.IsNullOrEmpty(item.og_keyword) ? item.og_keyword : "";
                            exist.robot = !string.IsNullOrEmpty(item.robot) ? item.robot : "";
                            exist.nguoi_cap_nhat = UserName;
                            exist.ngay_cap_nhat = DateTime.Now;
                            dbConn.Update(exist);
                        }
                        else
                        {
                            item.og_description = !string.IsNullOrEmpty(item.og_description) ? item.og_description : "";
                            item.og_title = !string.IsNullOrEmpty(item.og_title) ? item.og_title : "";
                            item.og_image = !string.IsNullOrEmpty(item.og_image) ? item.og_image : "";
                            item.og_keyword = !string.IsNullOrEmpty(item.og_keyword) ? item.og_keyword : "";
                            item.robot = !string.IsNullOrEmpty(item.robot) ? item.robot : "";
                            item.nguoi_tao = UserName;
                            item.ngay_tao = DateTime.Now;
                            item.nguoi_cap_nhat = UserName;
                            item.ngay_cap_nhat = DateTime.Now;
                            dbConn.Insert(item);
                        }
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
