using BIBIAM.Core;
using BIBIAM.Core.Data.DataObject;
using BIBIAM.Core.Entities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MCC.Filters;
using MCC.Helpers;
using MCC.Models;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MCC.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create")]
    public class Merchant_CatalogController : CustomController
    {
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                ViewBag.isAdmin = isAdmin;
                return View();
            }
            else
            {
                return RedirectToAction("NoAccess", "Error");
            }
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request, string ma_gian_hang = "")
        {

            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (IDbConnection db = OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    var data = new DataSourceResult();
                    if (!isAdmin)
                    {
                        data = KendoApplyFilter.KendoData<Merchant_Catalog>(request, "ma_gian_hang={0}".Params(currentUser.ma_gian_hang));
                        //return Json(db.Select<Merchant_Catalog>(s => s.ma_gian_hang == currentUser.ma_gian_hang).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ma_gian_hang.Trim()))
                        {
                            //return Json(db.Select<Merchant_Catalog>(s => s.ma_gian_hang == ma_gian_hang).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                            data = KendoApplyFilter.KendoData<Merchant_Catalog>(request, " ma_gian_hang={0}".Params(currentUser.ma_gian_hang));
                        }
                        else
                        {
                            //return Json(db.Select<Merchant_Catalog>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                            data = KendoApplyFilter.KendoData<Merchant_Catalog>(request);
                        }
                    }
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Save(List<HttpPostedFileBase> files)
        {
            try
            {
                int i = 0;
                Merchant_Folder_Info folder = new Merchant_Folder_Info();
                folder.ma_gian_hang = currentUser.ma_gian_hang;
                folder.ten_thu_muc = AllConstant.FoldderName_Merchant_Catalog;
                folder.ngay_tao = DateTime.Now;
                folder.nguoi_tao = currentUser.name;
                folder.ngay_cap_nhat = DateTime.Now;
                folder.nguoi_cap_nhat = currentUser.name;
                string a = new Merchant_Folder_Info_DAO().Insert(folder, AppConfigs.MCCConnectionString);
                foreach (var item in files)
                {
                    string LocalPath = "";
                    string LocalFolderPath = "";
                    string url_link = new AzureHelper().UploadFileToAzure("Catalog", item, currentUser.name, ref LocalFolderPath, ref LocalPath);
                    //update catalog info
                    Merchant_Catalog catalog = new Merchant_Catalog();
                    catalog.dung_luong = item.ContentLength;
                    catalog.duong_dan_day_du = url_link;
                    catalog.ma_gian_hang = currentUser.ma_gian_hang;
                    catalog.ten_catalog = Path.GetFileNameWithoutExtension(item.FileName);
                    catalog.thu_muc = LocalFolderPath;
                    catalog.url = DateTime.Now.ToString("yyyyMMddHHmmssfff") + currentUser.name + "-" + item.FileName;
                    string b = new Merchant_Catalog_DAO().UpSert(catalog, currentUser.name, AppConfigs.MCCConnectionString);

                    if (!string.IsNullOrEmpty(url_link)) i++;
                }
                return Json(new { success = true, message = "Upload thành công " + i.ToString() + " file!" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
        public ActionResult Delete(string data)
        {
            if (isAdmin && accessDetail != null && (accessDetail.access["all"] || accessDetail.access["delete"]))
            {

                string[] separators = { "," };
                string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (ids.Length == 0)
                {
                    return Json(new { success = false, message = "Chọn Catalog cần xóa!" });
                }
                string st = new Merchant_Catalog_DAO().Delete(ids, AppConfigs.MCCConnectionString);
                if (st == "true")
                    return Json(new { success = true, message = "Thành công!" });
                else
                    ModelState.AddModelError("", st);
            }
            return Json(new { success = false, message = "Xóa không thành công! " });
        }
    }
}