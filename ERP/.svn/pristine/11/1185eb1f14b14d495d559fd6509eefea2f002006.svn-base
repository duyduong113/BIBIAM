using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Data;
using System.IO;
using System.Collections;
using System.Data.OleDb;
using NPOI.HSSF.UserModel;
using System.Dynamic;
using BIBIAM.Core.Data.DataObject;
using BIBIAM.Core.Entities;

namespace ERPAPD.Controllers
{
    public class PropertyController : CustomController
    {
        // GET: Property
        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                ViewData["Asset"] = asset;
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.listType = dbConn.Select<Models.Parameters>(s=>s.Type== "loai_thong_so" && s.Status == true);
                }
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        #region Read_Action

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = new Property_DAO().ReadAll();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        #endregion

        #region Create_Action
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,[Bind(Prefix = "models")] List<Property> list)
        {
            if (asset.Create)
            {
                if (list != null)
                {
                    var result = new Property_DAO().UpSert(list, currentUser.UserName, "Insert");
                    if (result == "true")
                        return Json(new { success = true, message = "Thành công" });
                    else
                        return Json(new { success = false, message = result });

                }
                ModelState.AddModelError("", "Model not valid");
                return Json(new { success = false, message = "" });
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(new { success = false, message = "" });
            }
        }

        #endregion

        #region Update_Action      

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
        [Bind(Prefix = "models")] List<Property> list)
        {        
            if (asset.Update)
            {
                if (list != null)
                {
                    var result = new Property_DAO().UpSert(list, currentUser.UserName,"Updated");
                    if (result == "true")
                        return Json(new { success = true, message = "Thành công" });
                    else
                        return Json(new { success = false, message = result });
                   
                }
                ModelState.AddModelError("", "Model not valid");
                return Json(new { success = false, message = "" });
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(new { success = false, message = "" });
            }
        }
        #endregion      
        public ActionResult Delete(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    var result = new Property_DAO().Delete(listRowID);
                    if (result == "true")
                        return Json(new { success = true, message = "Thành công" });
                    else
                        return Json(new { success = false, message = result });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to create record" });
            }
        }
    }
}