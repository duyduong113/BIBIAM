using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DecaPay.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using DecaPay.Helpers;
using System.Data;
using OfficeOpenXml;
using System.IO;
using Kendo.Mvc;
using System.ComponentModel;

namespace DecaPay.Controllers
{
    [Authorize]
    [RequireHttps]
    public class VNMCustomerController : CustomController
    {
        //
        // GET: /DecaCustomer/
        public ActionResult Index()
        {
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.Assets = asset;
                }
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<Sales_Order_Customer>(request);
                }
                return Json(data);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Sales_Order_Customer> items)
        {
            if (asset.Create)
            {
                if (items != null && ModelState.IsValid)
                {
                    try
                    {
                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {
                            foreach (var item in items)
                            {
                                using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                                {
                                    item.MetaSearch = Helpers.convertToUnSign3.Init(item.FirstName.ToLower()) + ";" + Helpers.convertToUnSign3.Init(item.LastName.ToLower()) + ";" + (!String.IsNullOrEmpty(item.Phone) ? Helpers.convertToUnSign3.Init(item.Phone.ToLower()) : "") + ";" + (!String.IsNullOrEmpty(item.Email) ? Helpers.convertToUnSign3.Init(item.Email.ToLower()) + ";" : "");
                                    item.CreatedAt = DateTime.Now;
                                    item.CreatedBy = User.Identity.Name;
                                    dbConn.Insert(item);
                                    int Id = (int)dbConn.GetLastInsertId();
                                    item.Id = Id;
                                    string datetime = DateTime.Now.ToString("yyMMdd");
                                    item.CustomerId = "CU" + datetime + Id;
                                    dbConn.Update(item);
                                    dbTrans.Commit();
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }

                }
            }
            else
            {
                ModelState.AddModelError("", "Don't have permission");
            }
            return Json(items.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Sales_Order_Customer> items)
        {
            if (asset.Update)
            {
                if (items != null && ModelState.IsValid)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var item in items)
                        {
                            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                            {
                                item.MetaSearch = Helpers.convertToUnSign3.Init(item.FirstName.ToLower()) + ";" + Helpers.convertToUnSign3.Init(item.LastName.ToLower()) + ";" + (!String.IsNullOrEmpty(item.Phone) ? Helpers.convertToUnSign3.Init(item.Phone.ToLower()) : "") + ";" + (!String.IsNullOrEmpty(item.Email) ? Helpers.convertToUnSign3.Init(item.Email.ToLower()) + ";" : "");
                                item.UpdatedAt = DateTime.Now;
                                item.UpdatedBy = User.Identity.Name;
                                dbConn.UpdateOnly(item, onlyFields: p => new
                                {
                                    p.FirstName,
                                    p.LastName,
                                    p.Birthday,
                                    p.PlaceOfBirth,
                                    p.Gender,
                                    p.Job,
                                    p.MaritalStatus,
                                    p.TaxNumber,
                                    p.Fax,
                                    p.Phone,
                                    p.Email,
                                    p.Address,
                                    p.Type,
                                    p.MetaSearch,
                                    p.UpdatedAt,
                                    p.UpdatedBy
                                }, where: p => p.Id == item.Id);
                                dbTrans.Commit();
                            }
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Don't have permission");
            }

            return Json(items.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Excel_Export(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

    }
}