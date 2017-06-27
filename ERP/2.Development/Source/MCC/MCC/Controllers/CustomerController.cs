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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using BIBIAM.Core;
namespace MCC.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,create,update,delete,export")]
    public class CustomerController : CustomController
    {
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var db = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    ViewBag.isAdmin = isAdmin;
                    ViewBag.listCity = db.GetDictionary<string, string>("Select ma_thanh_pho as Value , ten_thanh_pho as Name from location_city").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
                    ViewBag.listDistrict = db.Select<Code_Master_Json>("Select ma_quan_huyen as Value , ten_quan_huyen as Name from location_district");
                }
                return View();
            }
            else return RedirectToAction("NoAccess", "Error");
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    var data = new DataSourceResult();
                    if (isAdmin)
                        data = KendoApplyFilter.KendoDataByQuery<Customer>(request, "select b.*,a.ma_gian_hang from Merchant_Customer a left join Customer b on a.ma_khach_hang=b.ma_khach_hang", "");
                    //data = KendoApplyFilter.KendoData<Customer>(request);
                    else
                        data = KendoApplyFilter.KendoData<Customer>(request, "select b.*,a.ma_gian_hang from Merchant_Customer a left join Customer b on a.ma_khach_hang=b.ma_khach_hang where a.ma_gian_hang={0}".Params(currentUser.ma_gian_hang));
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("NoAccess", "Error");


        }
       
        [HttpPost]
        public ActionResult UpdateCustomer(Customer data)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
            {
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    try
                    {
                        var exist = dbConn.SingleOrDefault<Customer>("id={0}", data.id);
                        if (exist != null)
                        {
                            data.ngay_cap_nhat = DateTime.Now;
                            data.nguoi_cap_nhat = User.Identity.Name;
                            dbConn.UpdateOnly(data,
                                            onlyFields: p =>
                                                new
                                                {
                                                    p.ma_khach_hang,
                                                    p.ma_user,
                                                    p.email,
                                                    p.sdt,
                                                    p.hoten,
                                                    p.ngay_cap_nhat,
                                                    p.nguoi_cap_nhat
                                                },
                                        where: p => p.id == data.id);
                            return Json(new { success = true, JsonRequestBehavior.AllowGet });
                        }
                        else return Json(new { success = false, error = "Dữ liệu không hợp lệ", JsonRequestBehavior.AllowGet });
                    }
                    catch (Exception e)
                    {
                        return Json(new { success = false, error = e.Message.ToString(), JsonRequestBehavior.AllowGet });
                    }
                }
            }
            else return RedirectToAction("NoAccess", "Error");
        }

        [HttpPost]
        public ActionResult CreateUpdate(Customer data, List<Customer_Address> details)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (data.id > 0)
                        {
                            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
                            {
                                data.ngay_cap_nhat = DateTime.Now;
                                data.nguoi_cap_nhat = User.Identity.Name;
                                dbConn.UpdateOnly(data,
                                                onlyFields: p =>
                                                    new
                                                    {
                                                        p.email,
                                                        p.sdt,
                                                        p.hoten,
                                                        p.ngay_cap_nhat,
                                                        p.nguoi_cap_nhat
                                                    },
                                            where: p => p.id == data.id);
                            }
                            else
                            {
                                return Json(new { success = false, error = "Don't have permission to update" });
                            }
                        }
                        else
                        {
                            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
                            {
                                var lastId = dbConn.FirstOrDefault<Customer>("SELECT TOP 1 * FROM Customer ORDER BY ma_khach_hang DESC");
                                if (lastId != null)
                                {
                                    var nextNo = Int32.Parse(lastId.ma_khach_hang.Substring(2, 8)) + 1;
                                    data.ma_khach_hang = "KH" + String.Format("{0:00000000}", nextNo);
                                }
                                else
                                {
                                    data.ma_khach_hang = "KH00000001";
                                }

                                data.ma_user = "___";
                                data.ngay_tao = DateTime.Now;
                                data.nguoi_tao = User.Identity.Name;
                                data.ngay_cap_nhat = DateTime.Parse("01/01/1900");

                                dbConn.Insert(data);
                                int Id = (int)dbConn.GetLastInsertId();
                                data.id = Id;
                                var checkMerchant_Customer = dbConn.FirstOrDefault<Merchant_Customer>("ma_gian_hang={0} and ma_khach_hang={1}".Params(currentUser.ma_gian_hang, data.ma_khach_hang));
                                if (checkMerchant_Customer == null)
                                {
                                    Merchant_Customer mc = new Merchant_Customer();
                                    mc.ma_gian_hang = currentUser.ma_gian_hang;
                                    mc.ma_khach_hang = data.ma_khach_hang;
                                    dbConn.Insert(mc);
                                }
                            }
                            else
                            {
                                return Json(new { success = false, error = "Don't have permission to create" });
                            }
                        }
                    }
                    catch (Exception e)
                    {
                           
                        return Json(new { success = false, error = "Lỗi" });
                    }

                    try
                    {
                        if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
                        {
                            //Xoa tat ca dia chi hien tai
                            string ma_kh = dbConn.QueryScalar<string>("select ma_khach_hang from Customer where id = " + data.id + " ");
                            string st = new Customer_DAO().Delete(ma_kh, AppConfigs.MCCConnectionString);
                            //Tạo CustomerAddress
                            if (details != null)
                            {
                                Customer Customertinfo = new Customer();
                                Customer_Address exist = new Customer_Address();
                                foreach (Customer_Address item in details)
                                {
                                    Customertinfo = dbConn.FirstOrDefault<Customer>("ma_khach_hang={0}", ma_kh);
                                    exist = dbConn.FirstOrDefault<Customer_Address>("ma_khach_hang={0}".Params(ma_kh));
                                    item.ma_khach_hang = data.ma_khach_hang;
                                    item.id = dbConn.QueryScalar<int>("select MAX(id)+1 from Customer_Address");
                                    item.ma_user = "";
                                    item.ngay_tao = DateTime.Now;
                                    dbConn.Insert(item);

                                }
                                   
                            }
                        }
                        else
                        {
                            return Json(new { success = false, error = "Không có quyền tạo" });
                        }
                    }
                    catch (Exception e)
                    {
                           
                        return Json(new { success = false, error = "Lỗi" });
                    }
                }
                return Json(new { success = true, data = data });
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }
        public ActionResult GetAddresses([DataSourceRequest] DataSourceRequest request, string ma_khach_hang)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var db = MCC.Helpers.OrmliteConnection.openConn())
                {
                    var data = db.Select<Customer_Address>("ma_khach_hang={0}".Params(ma_khach_hang));
                    return Json(data.ToDataSourceResult(request));
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult GetCity()
        {
            using (var dbConn = MCC.Helpers.OrmliteConnection.openConn())
            {
                var data = new List<location_city>();
                data = dbConn.Select<location_city>("Select ma_thanh_pho as Value , ten_thanh_pho as Name from location_city where trang_thai='1'");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetDistrict(string tinhtp )
        {
            IDbConnection db = OrmliteConnection.openConn();
            try { 
               var data = db.GetDictionary<string, string>("Select ma_quan_huyen as Value , ten_quan_huyen as Name from location_district  where ma_thanh_pho ={0}".Params(tinhtp)).Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value }).ToList();
                return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
    }
}