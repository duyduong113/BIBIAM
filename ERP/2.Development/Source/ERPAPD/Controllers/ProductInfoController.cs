using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ERPAPD.Controllers
{
    public class ProductInfoController : CustomController
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
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {                    
                    DataTable data = new DataTable();                    
                    string st = string.Empty;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(AllConstant.UriAPI);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = client.GetAsync("api/ProductInfo/GetAll?key=" + AllConstant.KeyAPI).Result;

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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<BIBIAM.Core.Entities.CRM_ProductInfo> listrow)
        {
            ModelState.Clear(); // phải clear
            if (asset.Create)
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(AllConstant.UriAPI);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string data = JsonConvert.SerializeObject(listrow);
                    HttpResponseMessage response = client.GetAsync("api/ProductInfo/UpSert?key=" + AllConstant.KeyAPI + "&data=" + data + "&UserName=" + currentUser.UserName + "&Type=Insert").Result;
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
            ModelState.AddModelError("", "Bạn không có quyền Create");
            return Json(new List<BIBIAM.Core.Entities.CRM_ProductInfo>().ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<BIBIAM.Core.Entities.CRM_ProductInfo> listrow)
        {
            ModelState.Clear(); // phải clear
            if (asset.Update)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(AllConstant.UriAPI);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string data = JsonConvert.SerializeObject(listrow);
                    HttpResponseMessage response = client.GetAsync("api/ProductInfo/UpSert?key=" + AllConstant.KeyAPI + "&data=" + data + "&UserName=" + currentUser.UserName + "&Type=Update").Result;
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
            return Json(new List<BIBIAM.Core.Entities.CRM_ProductInfo>().ToDataSourceResult(request, ModelState));
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
                    HttpResponseMessage response = client.GetAsync("api/ProductInfo/Delete?key=" + AllConstant.KeyAPI + "&data=" + data).Result;
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
    }
}