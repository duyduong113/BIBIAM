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
    public class Merchant_InfoController : CustomController
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
                    ViewBag.listStatusXuatBan = db.Select<BIBIAM.Core.Entities.Parameters>(s => s.Type == "StatusPublish").OrderBy(s => s.ID);
                    ViewBag.listStatusDuyet = db.Select<BIBIAM.Core.Entities.Parameters>(s => s.Type == "TTDSP").OrderBy(s => s.ID);
                    ViewBag.listCity = db.Select<CRM_Location_City>();
                    ViewBag.listDistrict = db.Select<CRM_Location_District>();
                }
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        //public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    if (asset.View)
        //    {
        //        var data = new Merchant_Info_DAO().ReadAll();
        //        return Json(data.ToDataSourceResult(request));
        //    }
        //    else
        //    {
        //        return RedirectToAction("NoAccessRights", "Error");
        //    }
        //}

        //#region Read_Action

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    //var data = Merchant_Info.ReadAll();
                    DataTable data = new DataTable();
                    string st = string.Empty;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(AllConstant.UriAPI);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = client.GetAsync("api/Merchant_Info/GetAll?key=" + AllConstant.KeyAPI).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            st = response.Content.ReadAsStringAsync().Result;
                            var test = JsonConvert.DeserializeObject(st);
                            data = (DataTable)JsonConvert.DeserializeObject(test.ToString(), (typeof(DataTable)));
                        }
                    }
                    return Json(data.ToDataSourceResult(request));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        //#endregion
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Merchant_Info> listrow)
        //{
        //    ModelState.Clear(); // phải clear
        //    if (asset.Create)
        //    {

        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(AllConstant.UriAPI);
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            string data = JsonConvert.SerializeObject(listrow);
        //            HttpResponseMessage response = client.GetAsync("api/Merchant_Info/UpSert?key=" + AllConstant.KeyAPI + "&data=" + data + "&UserName=" + currentUser.UserName + "&Type=Insert").Result;
        //            if (response.IsSuccessStatusCode)
        //            {
        //                string st = response.Content.ReadAsStringAsync().Result;
        //                st = (string)JsonConvert.DeserializeObject(st, (typeof(string)));
        //                if (String.IsNullOrEmpty(st))
        //                    return Json(new { success = true, message = "Thành công" });
        //                else
        //                    ModelState.AddModelError("", st);
        //            }
        //            else
        //                ModelState.AddModelError("", "Vui lòng nhập mã gian hàng");
        //        }

        //        return Json(listrow.ToDataSourceResult(request, ModelState));
        //    }
        //    ModelState.AddModelError("", "Bạn không có quyền Create");
        //    return Json(new List<Merchant_Info>().ToDataSourceResult(request, ModelState));
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Merchant_Info> listrow)
        {
            ModelState.Clear(); // phải clear
            if (asset.Update)
            {
                //string data = JsonConvert.SerializeObject(listrow);
                //using (var dbConn = Helpers.OrmliteConnection.openConn())
                //{
                //    foreach (var row in listrow)
                //    {
                //        row.UpdatedAt = DateTime.Now;
                //        row.UpdatedBy = currentUser.UserName;
                //        dbConn.Update(row);
                //    }
                //}

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(AllConstant.UriAPI);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string data = JsonConvert.SerializeObject(listrow);
                    HttpResponseMessage response = client.GetAsync("api/Merchant_Info/UpSert?key=" + AllConstant.KeyAPI + "&data=" + data + "&UserName=" + currentUser.UserName + "&Type=Update").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string st = response.Content.ReadAsStringAsync().Result;
                        st = (string)JsonConvert.DeserializeObject(st, (typeof(string)));
                        if (String.IsNullOrEmpty(st))
                            return Json(new { success = true, message = "Thành công" });
                        else
                            ModelState.AddModelError("", st);
                    }
                    else
                        ModelState.AddModelError("", "Vui lòng nhập tên loại sản phẩm");
                }
                return Json(listrow.ToDataSourceResult(request, ModelState));
            }
            ModelState.AddModelError("", "Bạn không có quyền Update");
            return Json(new List<Merchant_Info>().ToDataSourceResult(request, ModelState));
        }
        public ActionResult DeletePage(string data)
        {
            if (asset.Delete)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(AllConstant.UriAPI);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("api/Merchant_Info/Delete?key=" + AllConstant.KeyAPI + "&data=" + data).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string st = response.Content.ReadAsStringAsync().Result;
                        if ((bool)JsonConvert.DeserializeObject(st, (typeof(bool))))
                            return Json(new { success = true, message = "Thành công" });
                    }
                }
            }
            return Json(new { success = false, message = "Bạn " });
        }

        public ActionResult Create(Merchant_Info item, HttpPostedFileBase file)
        {
            if (asset.Create || asset.Update)
            {
                string result = "";
                try
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var refix = "Merchant_Info_" + currentUser.UserName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        Helpers.UploadFile.CreateFolder(Server.MapPath("~/Images/Merchant_Info/"));
                        var path = Path.Combine(Server.MapPath("~/Images/Merchant_Info/"), refix + Path.GetExtension(fileName));
                        file.SaveAs(path);
                        item.logo_gian_hang = refix + Path.GetExtension(fileName);
                    }
                    List<Merchant_Info> lstMerchantInfo = new List<Merchant_Info>();
                    lstMerchantInfo.Add(item);
                    result = new Merchant_Info_DAO().UpSert(lstMerchantInfo, currentUser.UserName, "Insert");
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
        public ActionResult GetDistrictIDByProviceID(string CityID)
        {
            using (IDbConnection db = OrmliteConnection.openConn())
            {
                try
                {
                    var data = db.Select<CRM_Location_District>("SELECT DistrictID,DistrictName FROM CRM_Location_District WHERE CityID = '" + CityID + "'");
                    return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return RedirectToAction("NoAccess", "Error");
                    //return Json(new { success = false, message = e.Message });
                }
            }

        }
    }
}