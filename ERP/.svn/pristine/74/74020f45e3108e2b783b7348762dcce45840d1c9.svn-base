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

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class CompanyController : CustomController
    {
        //
        // GET: /Organization/
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(Deca_Company_Log), typeof(Deca_Company));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(Deca_Company_Log), typeof(Deca_Company));
            //}
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(DC_Company_Branch));
            //    const bool overwrite = true;
            //    dbConn.CreateTables(overwrite, typeof(DC_Company_Branch));
            //}
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(DC_Company_Contractor));
            //    const bool overwrite = true;
            //    dbConn.CreateTables(overwrite, typeof(DC_Company_Contractor));
            //}

            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(DC_Company_Action));
            //    const bool overwrite = true;
            //    dbConn.CreateTables(overwrite, typeof(DC_Company_Action));
            //}
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.Region = dbConn.Select<DC_Location_Region>().OrderBy(a => a.RegionName);
                    ViewBag.City = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName);
                    ViewBag.District = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "District").OrderBy(a => a.TerritoryName);
                    ViewBag.CompanyType = DC_Company_Type.GetAllDC_Company_Types();
                    ViewBag.CompanyStatus = dbConn.Select<DC_Stage_Definition>();
                    ViewBag.AssigneeList = dbConn.Select<Users>().OrderBy(a => a.UserName);
                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;
                    ViewData["Asset"] = asset;
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
                    data = KendoApplyFilter.KendoData<Deca_Company>(request);
                }
                return Json(data);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_Company> items)
        {
            if (asset.Create)
            {
                if (items != null && ModelState.IsValid)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var item in items)
                        {
                            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                            {
                                try
                                {
                                    item.IsNew = true;
                                    item.CreatedAt = DateTime.Now;
                                    item.CreatedBy = User.Identity.Name;
                                    dbConn.Insert(item);
                                    int Id = (int)dbConn.GetLastInsertId();
                                    item.Id = Id;
                                    item.CompanyID = "C" + DateTime.Now.ToString("yyMMdd") + Id;
                                    //item.CompanyID = "C" + DateTime.Now.ToString("yyMMdd") + string.Format("{0:000}", Id);
                                    dbConn.Update(item);

                                    var log = new Deca_Company_Log();
                                    log.CompanyID = item.CompanyID;
                                    log.Log = item;
                                    log.CreatedAt = DateTime.Now;
                                    log.CreatedBy = currentUser.UserName;
                                    dbConn.Insert(log);

                                    dbTrans.Commit();
                                }
                                catch (Exception ex)
                                {
                                    ModelState.AddModelError("", ex.Message);
                                    return Json(items.ToDataSourceResult(request, ModelState));
                                }

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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_Company> items)
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
                                item.IsNew = true;
                                item.UpdatedAt = DateTime.Now;
                                item.UpdatedBy = User.Identity.Name;
                                dbConn.Update(item);

                                var log = new Deca_Company_Log();
                                log.CompanyID = item.CompanyID;
                                log.Log = item;
                                log.CreatedAt = DateTime.Now;
                                log.CreatedBy = currentUser.UserName;
                                dbConn.Insert(log);
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
        public ActionResult Branch_Read([DataSourceRequest]DataSourceRequest request, string CompanyID)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<DC_Company_Branch>(request, "CompanyID = '" + CompanyID + "'");
                }
                return Json(data);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveBranch([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<DC_Company_Branch> items, string CompanyID)
        {
            if (asset.Create)
            {
                if (items != null && ModelState.IsValid)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var item in items)
                        {
                            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                            {
                                try
                                {
                                    if (String.IsNullOrEmpty(item.BranchName))
                                    {
                                        ModelState.AddModelError("Lỗi", "Vui lòng nhập ít nhất 1 thông tin </br> - Tên chi nhánh");
                                        return Json(items.ToDataSourceResult(request, ModelState));

                                    }
                                    item.CompanyID = CompanyID;
                                    item.CreatedAt = DateTime.Now;
                                    item.CreatedBy = User.Identity.Name;
                                    dbConn.Insert(item);
                                    int Id = (int)dbConn.GetLastInsertId();
                                    item.ID = Id;
                                    item.BranchID = "BR" + DateTime.Now.ToString("yyMMdd") + Id;
                                    dbConn.Update(item);
                                    dbTrans.Commit();
                                }
                                catch (Exception ex)
                                {
                                    ModelState.AddModelError("", ex.Message);
                                    return Json(items.ToDataSourceResult(request, ModelState));
                                }

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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateBranch([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<DC_Company_Branch> items, string CompanyID)
        {
            if (asset.Update)
            {
                if (items != null && ModelState.IsValid)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var item in items)
                        {

                            if (String.IsNullOrEmpty(item.BranchName))
                            {
                                ModelState.AddModelError("Lỗi", "Vui lòng nhập ít nhất 1 thông tin </br> - Tên chi nhánh");
                                return Json(items.ToDataSourceResult(request, ModelState));

                            }
                            item.CompanyID = CompanyID;
                            item.UpdatedAt = DateTime.Now;
                            item.UpdatedBy = User.Identity.Name;
                            dbConn.Update(item);
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

        public ActionResult ReadContactor([DataSourceRequest]DataSourceRequest request, string CompanyID)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<DC_Company_Contractor>(request, "CompanyID = '" + CompanyID + "'");
                }
                return Json(data);
            }
        }
        [HttpPost]
        public ActionResult SaveContactor([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Company_Contractor> listCompany, string CompanyID)
        {
            if (asset.Create)
            {
                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {

                        if (listCompany != null && ModelState.IsValid)
                        {
                            foreach (var item in listCompany)
                            {
                                if (String.IsNullOrEmpty(item.FullName))
                                {
                                    ModelState.AddModelError("Lỗi", "Vui lòng nhập ít nhất 1 thông tin </br> - Tên người liên hệ");
                                    return Json(listCompany.ToDataSourceResult(request, ModelState));

                                }
                                item.CompanyID = CompanyID;
                                item.CreatedAt = DateTime.Now;
                                item.CreatedBy = currentUser.UserName;
                                dbConn.Insert(item);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Lỗi", e.Message);
                    return Json(listCompany.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listCompany.ToDataSourceResult(request, ModelState));
            }
            return Json(listCompany.ToDataSourceResult(request, ModelState));

        }
        [HttpPost]
        public ActionResult UpdateContactor([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Company_Contractor> listCompany, string CompanyID)
        {
            if (asset.Update)
            {
                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        if (listCompany != null && ModelState.IsValid)
                        {

                            foreach (var regis in listCompany)
                            {
                                if (String.IsNullOrEmpty(regis.FullName))
                                {
                                    ModelState.AddModelError("Lỗi", "Vui lòng nhập ít nhất 1 thông tin </br> - Tên người liên hệ");
                                    return Json(new { success = false });
                                }
                                regis.CompanyID = CompanyID;
                                regis.UpdatedAt = DateTime.Now;
                                regis.UpdatedBy = currentUser.UserName;
                                dbConn.Update(regis);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listCompany.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(listCompany.ToDataSourceResult(request, ModelState));
            }
            return Json(listCompany.ToDataSourceResult(request, ModelState));

        }

        public ActionResult ReadAction([DataSourceRequest] DataSourceRequest request, string CompanyID)
        {
            if (asset.View)
            {

                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<DC_Company_Action>(request, " CompanyID = '" + CompanyID + "'");
                }
                return Json(data);
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        [HttpPost]
        public ActionResult SaveAction([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Company_Action> listCompany, string CompanyID)
        {
            if (asset.Create)
            {
                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {

                        if (listCompany != null && ModelState.IsValid)
                        {
                            foreach (var typ in listCompany)
                            {
                                if (String.IsNullOrEmpty(typ.Action))
                                {
                                    ModelState.AddModelError("Lỗi", "Vui lòng nhập ít nhất 1 thông tin </br> - Tên hoạt động");
                                    return Json(listCompany.ToDataSourceResult(request, ModelState));

                                }
                                typ.CompanyID = CompanyID;
                                typ.CreatedAt = DateTime.Now;
                                typ.CreatedBy = currentUser.UserName;
                                dbConn.Insert(typ);
                            }


                        }
                        else
                        {
                            ModelState.AddModelError("Lỗi", "");
                            return Json(new { success = false });
                        }
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Lỗi", e.Message);
                    return Json(listCompany.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listCompany.ToDataSourceResult(request, ModelState));
            }
            return Json(listCompany.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult UpdateAction([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Company_Action> listCompany, string CompanyID)
        {
            if (asset.Update)
            {
                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        if (listCompany != null && ModelState.IsValid)
                        {

                            foreach (var item in listCompany)
                            {
                                if (String.IsNullOrEmpty(item.Action))
                                {
                                    ModelState.AddModelError("Lỗi", "Vui lòng nhập ít nhất 1 thông tin </br>- Tên hoạt động");
                                    return Json(listCompany.ToDataSourceResult(request, ModelState));

                                }
                                item.CompanyID = CompanyID;
                                item.CreatedAt = DateTime.Now;
                                item.CreatedBy = currentUser.UserName;
                                dbConn.Update(item);

                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Lỗi", "");
                            return Json(new { success = false });
                        }
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Lỗi", e.Message);
                    return Json(listCompany.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("Lỗi", "You don't have permission to update record");
                return Json(listCompany.ToDataSourceResult(request, ModelState));
            }
            return Json(listCompany.ToDataSourceResult(request, ModelState));

        }


        public JsonResult GetCompany()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                return Json(dbConn.Select<Deca_Company>().OrderBy(a => a.Id), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetBranch(string CompanyID)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                if (String.IsNullOrEmpty(CompanyID))
                {
                    return Json(dbConn.Select<DC_Company_Branch>(), JsonRequestBehavior.AllowGet);
                }
                return Json(dbConn.Select<DC_Company_Branch>("[CompanyID] = {0}", CompanyID), JsonRequestBehavior.AllowGet);
            }
        }
    }
}