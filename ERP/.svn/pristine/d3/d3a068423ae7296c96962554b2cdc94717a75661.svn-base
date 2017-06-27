using ERPAPD.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Web.Script.Serialization;
using ServiceStack.OrmLite;

namespace ERPAPD.Helpers
{
    public class DecaAPI
    {
        public static DC_Order_Response createOCMOrder(DC_OCM_Create_Order order)
        {
            DC_Order_Response apiModel = new DC_Order_Response();
            try
            {
                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        var log = new DC_OCM_Create_Order_Log();
                        log.log = order;
                        log.CreatedAt = DateTime.Now;
                        log.CreatedBy = "apiLog";
                        dbConn.Insert(log);
                    }
                }
                catch (Exception)
                {

                }

                var request = JsonConvert.SerializeObject(order);
                var data = RestfulClient.POST(ConfigurationManager.AppSettings["decaOrderAPI"].ToString().Trim(), request);

                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        var log = new DC_OCM_Create_Order_Log();
                        log.log = new DC_OCM_Create_Order { code_token = order.code_token, ghi_chu = data };
                        log.CreatedAt = DateTime.Now;
                        log.CreatedBy = "apiLog";
                        dbConn.Insert(log);
                    }
                }
                catch (Exception)
                {

                }

                if (!String.IsNullOrEmpty(data))
                {
                    JavaScriptSerializer objJavascript = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 };
                    apiModel = objJavascript.Deserialize<DC_Order_Response>(data);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return apiModel;

        }

        public static DC_Order_Response updateOCMOrder(DC_OCM_Update order)
        {
            DC_Order_Response apiModel = new DC_Order_Response();
            try
            {
                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        var log = new DC_OCM_Update_Log();
                        log.log = order;
                        log.CreatedAt = DateTime.Now;
                        log.CreatedBy = "apiLog";
                        dbConn.Insert(log);
                    }
                }
                catch (Exception)
                {

                }

                var request = JsonConvert.SerializeObject(order);
                var data = RestfulClient.POST(ConfigurationManager.AppSettings["decaOrderAPIUpdate"].ToString().Trim(), request);

                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        var log = new DC_OCM_Create_Order_Log();
                        log.log = new DC_OCM_Create_Order { code_token = order.code_token, ghi_chu = data };
                        log.CreatedAt = DateTime.Now;
                        log.CreatedBy = "apiLog";
                        dbConn.Insert(log);
                    }
                }
                catch (Exception)
                {

                }

                if (!String.IsNullOrEmpty(data))
                {
                    JavaScriptSerializer objJavascript = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 };
                    apiModel = objJavascript.Deserialize<DC_Order_Response>(data);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return apiModel;

        }
    }
}