using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using BIBIAM.Core.Entities;
using System.Data;
using BIBIAM.Core.Data.DataObject;
using MCC.Filters;
using BIBIAM.Core.Data;
using BIBIAM.Core;

namespace MCC.Controllers
{
    [Authorize]
    public class Merchant_Product_PromotionController : CustomController
    {
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                ViewBag.isAdmin = isAdmin;
                return View();
            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                var data = new Merchant_Product_Promotion_DAO().ReadByMerchantID(AppConfigs.MCCConnectionString, isAdmin ? "All" : currentUser.ma_gian_hang);
                return Json(data.ToDataSourceResult(request));
            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult Detail(string id)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                if (String.IsNullOrEmpty(id))
                    return RedirectToAction("Create", "Merchant_Product_Promotion");
                using (var db = MCC.Helpers.OrmliteConnection.openConn())
                {
                    if (isAdmin)
                        ViewBag.data = db.SingleOrDefault<Merchant_Product_Promotion>("ma_chuong_trinh_km = {0}", id);
                    else
                        ViewBag.data = db.SingleOrDefault<Merchant_Product_Promotion>("ma_chuong_trinh_km = {0} and ma_gian_hang = {1}", id, currentUser.ma_gian_hang);

                    if (ViewBag.data == null)
                        return RedirectToAction("Create", "Merchant_Product_Promotion");

                    return View();
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult Create()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var db = MCC.Helpers.OrmliteConnection.openConn())
                {
                    return View();
                }
            }
          
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult UpsertPromotion(Merchant_Product_Promotion promotion, List<string> products,string ma_gian_hang)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
            {
                promotion.ma_gian_hang = ma_gian_hang;
                string rs = new Merchant_Product_Promotion_DAO().UpsertFull(promotion, products, currentUser.name, AppConfigs.MCCConnectionString);
                if (rs.StartsWith("true"))
                    return Json(new { success = true, ma_km = rs.Substring(4, rs.Length - 4), JsonRequestBehavior.AllowGet });
                else
                    return Json(new { success = false, message = rs, JsonRequestBehavior.AllowGet });
            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult Delete(string data)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["delete"]))
            {
                try
                {
                    using (var dbConn = MCC.Helpers.OrmliteConnection.openConn())
                    {
                        string[] separators = { "," };
                        string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        if (ids.Length == 0)
                        {
                            return Json(new { success = false, message = "Chọn các Chương trình khuyến mãi cần xóa!" });
                        }
                        foreach(var id in ids)
                        {
                            var exists = dbConn.FirstOrDefault<Merchant_Product_Promotion>("id={0}".Params(id));
                            if (exists == null)
                            {
                                return Json(new { success = false, error = "Không có khuyến mãi này!" });
                            }
                            if (Convert.ToDateTime(exists.ngay_bat_dau) < DateTime.Now && DateTime.Now < Convert.ToDateTime(exists.ngay_ket_thuc))
                            {
                                return Json(new { success = false, error = exists.ten_chuong_trinh_km + "Đang trong thời gian khuyến mãi, không được xóa!!" });
                            }
                            dbConn.Delete<Merchant_Product_Promotion>("id={0}".Params(id));
                        }
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
                return Json(new { success = false, error = "Không có quyền xóa" });
            }
        }

        public ActionResult Active(string data)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
            {
                try
                {
                    string[] separators = { "," };
                    string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    if (ids.Length == 0)
                    {
                        return Json(new { success = false, message = "Chọn các chương trình khuyến mãi!" });
                    }

                    using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                    {
                        foreach (var id in ids)
                        {
                            var exists = dbConn.FirstOrDefault<Merchant_Product_Promotion>("id={0}".Params(id));
                            dbConn.UpdateOnly(new Merchant_Product_Promotion { trang_thai = exists.trang_thai == AllConstant.trang_thai.DANG_SU_DUNG ? AllConstant.trang_thai.KHONG_SU_DUNG : AllConstant.trang_thai.DANG_SU_DUNG, nguoi_cap_nhat = currentUser.name, ngay_cap_nhat = DateTime.Now }, onlyFields: p => new { p.trang_thai, p.nguoi_cap_nhat, p.ngay_cap_nhat }, where: p => p.id == int.Parse(id));
                        }
                    }
                    return Json(new { success = true, message = "Thành công!" });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }
            }
            else
            {
                return Json(new { success = false, error = "Bạn không có quyền thực hiên chức năng này!" });
            }
        }
    }
}
