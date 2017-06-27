using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MCC.Helpers;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Collections;
using System.Data.OleDb;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BIBIAM.Core.Entities;
using BIBIAM.Core.Data.DataObject;
using BIBIAM.Core;

namespace MCC.Controllers
{
    public class Merchant_Product_PriceController : CustomController
    {
        // GET: Merchant_Product_Price
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    ViewBag.isAdmin = isAdmin;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (IDbConnection dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
            {
                var data = new DataSourceResult();
                data = KendoApplyFilter.KendoData<BIBIAM.Core.Entities.Merchant_Product_Price>(request);
                return Json(data);
            }
        }

        public ActionResult deleteItem(int id)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["delete"]))
            {
                try
                {
                    using (var dbConn = MCC.Helpers.OrmliteConnection.openConn())
                    {
                        dbConn.Delete<BIBIAM.Core.Entities.Merchant_Product_Price>("id={0}", id);
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

        public ActionResult CreateUpdate(Merchant_Product_Price data)
        {
            try
            {
                
                using (var dbConn = OrmliteConnection.openConn())
                {
                    if (data.id > 0)
                    {
                        if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
                        {
                            if (data.pk_gia == null)
                            {
                                return Json(new { success = false, error = "Yêu cầu nhập pk_gia" });
                            }
                            if (data.fk_san_pham_gian_hang == null)
                            {
                                return Json(new { success = false, error = "Yêu cầu nhập fk_san_pham_gian_hang" });
                            }
                            if (data.don_gia <= 0)
                            {
                                return Json(new { success = false, error = "Yêu cầu nhập đơn giá" });
                            }

                            var exist = dbConn.SingleOrDefault<Merchant_Product_Price>("id={0}", data.id);

                            if (DateTime.Now != data.ngay_tao)
                            {
                                data.ngay_cap_nhat = DateTime.Now;
                                data.nguoi_cap_nhat = User.Identity.Name;
                            }

                            dbConn.UpdateOnly(data,
                                    onlyFields: p =>
                                        new
                                        {
                                            p.pk_gia,
                                            p.fk_san_pham_gian_hang,
                                            p.don_gia,
                                            p.ton_kho,
                                            p.don_gia_khuyen_mai,
                                            p.trang_thai_khuyen_mai,
                                            p.trang_thai_khuyen_mai_tang_qua,
                                            p.phan_tram_khuyen_mai,
                                            p.ngay_tao,
                                            p.nguoi_tao,
                                            p.ngay_cap_nhat,
                                            p.nguoi_cap_nhat
                                        },
                                where: p => p.id == data.id);
                        }
                        else
                        {
                            return Json(new { success = false, error = "Bạn không có quyền chỉnh sửa" });
                        }
                    }
                    else
                    {
                        
                            if (data.pk_gia == null)
                            {
                                return Json(new { success = false, error = "Yêu cầu nhập pk_gia" });
                            }
                            if (data.fk_san_pham_gian_hang == null)
                            {
                                return Json(new { success = false, error = "Yêu cầu nhập fk_san_pham_gian_hang" });
                            }
                            if (data.don_gia <= 0)
                            {
                                return Json(new { success = false, error = "Yêu cầu nhập đơn giá" });
                            }

                            data.ngay_tao = DateTime.Now;
                            data.nguoi_tao = User.Identity.Name;
                            int Id = (int)dbConn.GetLastInsertId();
                            dbConn.Insert(data);                        
                    }

                    //sync mobile app
                    //BackgroundJob.Enqueue(
                    //        () => new Helpers.mobileAppAPI().init("agency", "syncdata", data.id));
                }
                return Json(new { success = true, data = data });
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        //chua biet dung lam gi
        public ActionResult Delete(string data)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["delete"]))
            {
                string result = "";
                string[] st = new string[1];
                st[0] = data;
                if (result == "true")
                    return Json(new { success = true, message = "Thành công" });
                else
                    return Json(new { success = false, message = result });
            }
            else
                return Json(new { success = false, message = "Bạn không có quyền xóa" });
        }

        

        //chua biet dung lam gi
        public ActionResult Upsert(Product_Price item)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]) ||
                accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
            {
                string result = "";
                try
                {
                    List<Product_Price> lstProdPrice = new List<Product_Price>();
                    lstProdPrice.Add(item);
                    result = new Product_Price_DAO().UpSert(lstProdPrice, currentUser.name, AppConfigs.MCCConnectionString);
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