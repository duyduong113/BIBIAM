using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using Dapper;
using CMS.Controllers;
using CMS.Helpers;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data;

namespace CMS.Controllers
{
    [Authorize]
    public class CustomDataController : CustomController
    {
        //
        // GET: /CustomData/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetUserDetail(int leaderId)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Users>("SELECT * FROM [Users] WHERE Active = 1 AND Id = " + leaderId);
                return Json(new { success = true, data = data });
            }
        }
        public ActionResult GetFolder()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = new List<DropDownJsonString>();
                data = dbConn.GetDictionary<string, string>("SELECT distinct ten_thu_muc as id, ten_thu_muc as name FROM cms_Merchant_Folder_Info").Select(s => new DropDownJsonString { id = s.Key, name = s.Value }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        //BaoVT add
        protected IEnumerable<Code_Master_Json> GetListCode_Master_Json(string Type)
        {
            string culture = Request.Cookies["_culture"] != null ? Request.Cookies["_culture"].Value : "vi";
            using (IDbConnection db = OrmliteConnection.openConn())
            {
                return db.GetDictionary<string, string>("SELECT Value," + (culture == "vi" ? "Name_Vi" : "Name") + " FROM Code_Master WHERE Type = {0} Order By order_Num".Params(Type)).Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
            }
        }   
        public ActionResult GetCodeMaster(string type)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                string culture = Request.Cookies["_culture"] != null ? Request.Cookies["_culture"].Value : "vi";
                var data = dbConn.GetDictionary<string, string>("SELECT Value, " + (culture == "vi" ? "Name_Vi" : "Name") + " FROM Code_Master WHERE Type = '" + type + "' Order By order_Num").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetCodeMasterName(string type)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {

                var data = dbConn.GetDictionary<string, string>("SELECT Value, Name FROM Code_Master WHERE Type = '" + type + "' Order By order_Num").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        

        public class Company_Json
        {
            public string id { get; set; }
            public string name { get; set; }
        }
        public ActionResult getCompanyCode()
        {
            using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
            {
                var data = new List<Company_Json>();
                data = dbConn.GetDictionary<string, string>("SELECT companycode as id, companyname AS name FROM Company").Select(s => new Company_Json { id = s.Key, name = s.Value }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getUserforBroker()
        {
            using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
            {
                var data = new List<Company_Json>();
                data = dbConn.GetDictionary<string, string>("SELECT name as id, fullName AS name FROM Users").Select(s => new Company_Json { id = s.Key, name = s.Value }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getCompanyCodeByStock()
        {
            using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
            {
                var data = new List<Company_Json>();
                data = dbConn.GetDictionary<string, string>("SELECT stockid as id, stockid as name FROM Company").Select(s => new Company_Json { id = s.Key, name = s.Value }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        

        [OutputCache(CacheProfile = "Cache1Hour")]
        public ActionResult GetCountry(string text)
        {
            using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Territory>("Level='country' AND Name like N'%" + text + "%'").OrderBy(s => s.Id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [OutputCache(CacheProfile = "Cache1Hour")]
        public ActionResult GetCity(int? Id)
        {
            using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Territory>("Level='province' AND ParentId={0}", Id).OrderBy(s => s.Id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult getHierarchy(string level, string parentcode, string hierarchytype)
        {
            using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
            {
                if (string.IsNullOrEmpty(parentcode))
                {
                    var data = dbConn.Select<Hierarchy>(s => s.level == int.Parse(level) && s.hierarchytype == hierarchytype).OrderBy(s => s.id);
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = dbConn.Select<Hierarchy>(s => s.level == int.Parse(level) && s.hierarchytype == hierarchytype && s.parentcode == parentcode).OrderBy(s => s.id);
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult getHashtag()
        {
            using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Hashtag>().OrderBy(s => s.id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [OutputCache(CacheProfile = "Cache1Hour")]
        public ActionResult GetCityS(string text)
        {
            using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Territory>("Level='province' AND Name like N'%" + text + "%'").OrderBy(s => s.Id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [OutputCache(CacheProfile = "Cache1Hour")]
        public ActionResult GetDistrict(int? Id)
        {
            using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Territory>("Level='district' AND ParentId={0}", Id).OrderBy(s => s.Id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [OutputCache(CacheProfile = "Cache1Hour")]
        public ActionResult GetDistrictN(string Name)
        {
            using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Territory>("Level='district' AND ParentId= ISNULL((SELECT TOP 1 Id FROM Territory WHERE Name = N'" + Name + "'),0)").OrderBy(s => s.Id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult GetVIN(string VIN)
        //{
        //    using (var dbConn = CRM.Helpers.OrmliteConnection.openConn())
        //    {
        //        var data = dbConn.Select<tw_VINManagement>("VIN={0}",VIN);
        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public ActionResult GetVINByCarId(int carId)
        //{
        //    using (var dbConn = CRM.Helpers.OrmliteConnection.openConn())
        //    {
        //        //var data = new List<tw_Car_Json1>();
        //        //data = dbConn.GetDictionary<int, string>("SELECT carId, VIN FROM tw_VINManagement WHERE carId=" + carId + " AND status = 'FordCRMVINStatus0002'").Select(s => new tw_Car_Json1 { id = s.Key, name = s.Value }).ToList();
        //        //return Json(data, JsonRequestBehavior.AllowGet);
        //        var data = dbConn.Select<tw_PO_Get>("select v.warehouseId, p.* from tw_PO p, tw_VINManagement v where p.VIN = v.VIN AND v.carId = {0} AND v.status = 'FordCRMVINStatus0002'", carId);
        //        //var data = dbConn.Select<tw_VINManagement>("carId={0} AND status = 'FordCRMVINStatus0002'", carId);
        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //}

       

        public ActionResult GetVINList(string carId)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = dbConn.GetDictionary<string, string>("SELECT VIN, VIN FROM tw_VINManagement WHERE status = 'FordCRMVINStatus0002' AND carId = " + carId).Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

      

        
    }
}