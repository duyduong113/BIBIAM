using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Collections;
using System.Data.OleDb;
using NPOI.HSSF.UserModel;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BIBIAM.Core.Entities;
using BIBIAM.Core.Data.DataObject;

namespace ERPAPD.Controllers
{
    public class Product_PriceController : CustomController
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
                    ViewBag.listProduct = db.Select<Product_Info>();
                    ViewBag.listStatus = db.Select<BIBIAM.Core.Entities.Parameters>(s => s.Type == "TTSP").OrderBy(s => s.ID);
                    ViewBag.listStatusDuyet = db.Select<BIBIAM.Core.Entities.Parameters>(s => s.Type == "TTDSP").OrderBy(s => s.ID);
                    ViewBag.listStatusXuatBan = db.Select<BIBIAM.Core.Entities.Parameters>(s => s.Type == "StatusPublish").OrderBy(s => s.ID);
                }
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = new Product_Price_DAO().ReadAll();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Delete(string data)
        {
            if (asset.Delete)
            {
                string result = "";
                string[] st = new string[1];
                st[0] = data;
                result = new Product_Price_DAO().Delete(st).ToString();
                if (result == "true")
                    return Json(new { success = true, message = "Thành công" });
                else
                    return Json(new { success = false, message = result });
            }
            else
                return Json(new { success = false, message = "Bạn không có quyền xóa" });
        }
        public ActionResult Upsert(Product_Price item)
        {
            if (asset.Create || asset.Update)
            {
                string result = "";
                try
                {
                    List<Product_Price> lstProdPrice = new List<Product_Price>();
                    lstProdPrice.Add(item);
                    result = new Product_Price_DAO().UpSert(lstProdPrice, currentUser.UserName, "Insert");
                    if (result == "true")
                    {
                        if (item.id == 0)// 0 insert, 1 update
                            return Json(new { success = true, type = 0 });
                        else
                            return Json(new { success = true, type = 1 });
                    }
                    else
                        return Json(new { success = false, message = result });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = "Có lỗi file upload" + e.Message });
                }
            }
            else
            {
                return Json(new { success = false, message = "Bạn không có quyền." });

            }
        }
    }
}