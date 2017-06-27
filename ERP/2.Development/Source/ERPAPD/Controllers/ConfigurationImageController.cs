using ERPAPD.Helpers;
using ERPAPD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using ServiceStack.OrmLite;

namespace ERPAPD.Controllers
{
    public class ConfigurationImageController : CustomController
    {
        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewBag.Create = asset.Create;
                ViewBag.Update = asset.Update;
                ViewBag.Delete = asset.Delete;
                ViewBag.Export = asset.Export;
                ViewBag.listWebsite = CRM_Website.GetList();
                using (IDbConnection db = OrmliteConnection.openConn())
                {
                    ViewBag.listType = db.Select<Parameters>(s => s.Type == "IMGResizeType");
                    ViewBag.listImageType = db.Select<Parameters>(s => s.Type == "ImageType");
                }
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult GetResize(string type)
        {
            if (asset.View)
            {
                using (IDbConnection db = OrmliteConnection.openConn())
                {   
                    var str = db.Select<Parameters>("select * from Parameters where Type = 'IMGAutoResize' and ParamID like '" + type + "%'");                    
                    if (str != null)
                        return Json(new { success = true, str, JsonRequestBehavior.AllowGet });
                    else
                        return Json(new { success = false, message = "That bai" });
                }
            }
            else
                return Json(new { success = false, message = "Bạn không có quyền" });
        }
        public ActionResult Update(int idWidth, string Width, int idHeight, string Height)
        {
            if (asset.View)
            {
                using (IDbConnection db = OrmliteConnection.openConn())
                {
                    var checkWidth = db.FirstOrDefault<Parameters>(s => s.ID == idWidth);
                    var checkHeight = db.FirstOrDefault<Parameters>(s => s.ID == idHeight);
                    if (checkWidth != null)
                    {
                        checkWidth.Value = Width;
                        checkWidth.UpdatedAt = DateTime.Now;
                        checkWidth.UpdatedBy = currentUser.UserName;
                        db.Update(checkWidth);
                    }
                    if (checkHeight != null)
                    {
                        checkHeight.Value = Height;
                        checkHeight.UpdatedAt = DateTime.Now;
                        checkHeight.UpdatedBy = currentUser.UserName;
                        db.Update(checkHeight);
                    }
                }
                return Json(new { success = true, message = "Cập nhật thành công", JsonRequestBehavior.AllowGet });
            }
            else
                return Json(new { success = false, message = "Bạn không có quyền", JsonRequestBehavior.AllowGet });
        }
    }
}
