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
    public class PageTypeController : CustomController
    {
        //
        // GET: /PageType/

        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewBag.Create = asset.Create;
                ViewBag.Update = asset.Update;
                ViewBag.Delete = asset.Delete;
                ViewBag.Export = asset.Export;
                ViewBag.listWebsite = CRM_Website.GetList();
                var a = AllConstant.KeyAPI;
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
                using (IDbConnection dbConn = OrmliteConnection.openConn())
                {
                    //var data = CRM_PageType.ReadAll();
                    DataTable data = new DataTable();
                    //string st = "[{\"PageTypeID\":1,\"PageTypeName\":\"asdasdsad\",\"RefID\":\"asdasd\",\"Status\":true,\"CreatedAt\":\"2016-09-09T18:43:46.553\",\"CreatedBy\":\"administrator\",\"UpdatedAt\":\"2016-09-09T18:43:46.553\",\"UpdatedBy\":\"administrator\"}]";
                    string st = string.Empty;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(AllConstant.UriAPI);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = client.GetAsync("api/PageType/GetAllDataTable?key=" + AllConstant.KeyAPI + "&type=datatable").Result;

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

        #endregion
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_PageType> listrow)
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
                    HttpResponseMessage response = client.GetAsync("api/PageType/UpSert?key=" + AllConstant.KeyAPI + "&data=" + data + "&UserName=" + currentUser.UserName + "&Type=Insert").Result;
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
            return Json(new List<CRM_PageType>().ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_PageType> listrow)
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
                    HttpResponseMessage response = client.GetAsync("api/PageType/UpSert?key=" + AllConstant.KeyAPI + "&data=" + data + "&UserName=" + currentUser.UserName + "&Type=Update").Result;
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
            return Json(new List<CRM_PageType>().ToDataSourceResult(request, ModelState));
        }
        public ActionResult DeletePage(string data)
        {
            if (asset.Delete)
            {
                //using (var dbConn = Helpers.OrmliteConnection.openConn())
                //{
                //    string[] separators = { "@@" };
                //    var ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (var id in ids)
                //    {
                //        CRM_PageType.Delete(id);
                //    }
                //    return Json(new { success = true, message = "Thành công" });
                //}

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(AllConstant.UriAPI);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var content = new FormUrlEncodedContent(new[] 
                    {
                        new KeyValuePair<string, string>("key", AllConstant.KeyAPI),
                        new KeyValuePair<string, string>("data", data)
                    });
                    HttpResponseMessage response = client.GetAsync("api/PageType/Delete?key=" + AllConstant.KeyAPI + "&data=" + data).Result;
                    //HttpResponseMessage response = client.PostAsync("api/PageType/Delete", content).Result;
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