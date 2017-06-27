using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using BIBIAM.Core.Entities;
using MCC.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using BIBIAM.Core.Data.DataObject;
using CloudinaryDotNet;
using System.Web;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using MCC.Helpers;
using MCC.Filters;
using System.Text.RegularExpressions;
using System.Text;
using BIBIAM.Core.Data;
using BIBIAM.Core;

namespace MCC.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete,export")]
    public class Merchant_Product_RelatedController : CustomController
    {
        //Mục lục
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
            {
                ViewBag.isAdmin = isAdmin;
                return View();
            }
            else return RedirectToAction("NoAccess", "Error");
        }
        //Đọc lưới
        public ActionResult Read([DataSourceRequest] DataSourceRequest request, string ma_san_pham)
        {

            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                // using (var db = MCC.Helpers.OrmliteConnection.openConn())
                {


                    //data = KendoApplyFilter.KendoData<Merchant_Product_Related>(request, " ma_san_pham ={0} ".Params(ma_san_pham_de_nghi));
                    var data = new Merchant_Product_Related_DAO().ReadAll(AppConfigs.MCCConnectionString, ma_san_pham);
                    return Json(data.ToDataSourceResult(request));
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }
        //Đọc lưới Grid
        public ActionResult Deletesp(string ma_san_pham, string ma_san_pham_de_nghi)
        {
            if (isAdmin && accessDetail != null && (accessDetail.access["all"] || accessDetail.access["delete"]))
            {                string st = new Merchant_Product_Related_DAO().Delete(ma_san_pham, ma_san_pham_de_nghi, AppConfigs.MCCConnectionString);
                if (st == "true")
                    return Json(new { success = true, message = "Thành công" });
                else
                    ModelState.AddModelError("", st);
            }
            return Json(new { success = false, message = "Bạn " });
        }
        public ActionResult ReadDetail([DataSourceRequest]DataSourceRequest request)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    //var data = new List<Merchant_Product>();
                    if (isAdmin)
                        return Json(dbConn.Select<Merchant_Product>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                    else

                        return Json(dbConn.Select<Merchant_Product>(s => s.trang_thai == AllConstant.trang_thai.DANG_SU_DUNG).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

                }
            }
            return RedirectToAction("NoAccess", "Error");

        }
        public ActionResult getNameFromHierarchy(string ma_phan_cap)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    var data = dbConn.FirstOrDefault<Hierarchy>(s => s.ma_phan_cap == ma_phan_cap);
                    return Json(new { success = true, data = data, JsonRequestBehavior.AllowGet });
                }
            }
            else return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult ReadHierarchy([DataSourceRequest] DataSourceRequest request, int cap, string ma_phan_cap_cha = "")
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    //dbConn.ChangeDatabase(AllConstant.ERPDBName);
                    var loai_phan_cap = dbConn.FirstOrDefault<Code_Master>(s => s.Name == "Product" && s.Type == "HierarchyType");
                    var data = dbConn.Select<Hierarchy>(s => s.trang_thai == AllConstant.trang_thai.DANG_SU_DUNG && s.loai_phan_cap == loai_phan_cap.Value && s.cap == cap && s.ma_phan_cap_cha == ma_phan_cap_cha).OrderBy(s => s.ten_phan_cap);
                    return Json(data.ToDataSourceResult(request));
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }
        //THÊM SẢN PHẨM VÀO LƯỚI
        public ActionResult AddProduct_Related(string data, string ma_san_pham)
        {
            if (isAdmin && accessDetail != null && (accessDetail.access["all"] || accessDetail.access["delete"]))
            {

                string[] separators = { "@@" };
                string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);


                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn())
                {
                    //xoa tat ca cac sp de nghi cus ma_san_pham
                    dbConn.Delete<BIBIAM.Core.Entities.Merchant_Product_Related>("ma_san_pham={0}".Params(ma_san_pham));
                    
                    foreach (string id in ids)
                    {
                        //get ma sp de nghi theo id
                        var product = dbConn.FirstOrDefault<Merchant_Product>(s => s.id == int.Parse(id));

                        //insert ma sp de nghi vao 
                        BIBIAM.Core.Entities.Merchant_Product_Related productrelated = new BIBIAM.Core.Entities.Merchant_Product_Related();

                        productrelated.ma_san_pham = ma_san_pham;
                        productrelated.ma_gian_hang = product.ma_gian_hang;
                        productrelated.ma_san_pham_de_nghi = product.ma_san_pham;
                        productrelated.ngay_tao = DateTime.Now;
                        productrelated.nguoi_tao = currentUser.name;
                        dbConn.Insert(productrelated);
                    }

                }
                return Json(new { success = true, message = "Thành công" });

            }
            return Json(new { success = false, message = "Bạn " });
        }

        [HttpPost]
        [ValidateInput(false)]
        
        public ActionResult GetImages(string ma_san_pham)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var db = MCC.Helpers.OrmliteConnection.openConn())
                {
                    List<Merchant_Product_Image> data;
                    if (isAdmin)
                        data = db.Select<Merchant_Product_Image>("ma_san_pham = {0}".Params(ma_san_pham));
                    else
                        data = db.Select<Merchant_Product_Image>("ma_san_pham = {0} and ma_gian_hang={1}".Params(ma_san_pham, currentUser.ma_gian_hang));
                    return Json(new { success = true, data = data, JsonRequestBehavior.AllowGet });
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult deleteItem(int id)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["delete"]))
            {
                try
                {
                    using (var dbConn = MCC.Helpers.OrmliteConnection.openConn())
                    {
                        dbConn.Delete<BIBIAM.Core.Entities.Merchant_Product_Related>("id={0}", id);
                        return Json(new { success = true });
                    }
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }
            }
            else
            {
                return Json(new { success = false, error = "Bạn không có quyền xóa" });
            }
        }

    }
}