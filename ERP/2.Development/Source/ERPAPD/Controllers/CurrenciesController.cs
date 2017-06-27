using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CurrenciesController : CustomController
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
                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    //ViewData["AllowExport"] = asset.Export;
                    ViewData["Asset"] = asset;
//                    ViewBag.listCurrencies = dbConn.Select<Currencys>(@"Select a.ID,a.CurrencyCode,a.CurrencyName +' ( '+ a.CurrencySymbol+' )' AS CurrencyName 
//                                            from Currencys a
//                                            LEFT JOIN Currencies b
//                                            ON a.CurrencyCode=b.CurrencyCode 
//                                            where b.CurrencyCode IS NULL
//                                            ").OrderBy(a => a.ID);
                    var CurrencyName = "";
                    try
                    {
                        CurrencyName = dbConn.FirstOrDefault<tien_te>(@"SELECT cs.*, mter.ten_tien_te +' - '+mter.ma_tien_te  AS ten_tien_te
                            FROM [ti_gia_tien_te] cs LEFT JOIN tien_te mter
                            ON cs.ma_tien_te=mter.ma_tien_te
							WHERE cs.tien_te_mac_dinh=1").ten_tien_te;
                    }
                    catch
                    {
                        CurrencyName = "";
                    }
                    finally
                    { 
                        ViewData["CurrencyNameDF"] = CurrencyName; 
                    }
                }
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                // var data = new DataSourceResult();
                var data = new List<ti_gia_tien_te>();
                if (asset.View)
                {
                    //data = KendoApplyFilter.KendoData<Currencies>(request);
                    data = dbConn.Query<ti_gia_tien_te>(@"SELECT cs.*, mter.ten_tien_te,mter.ki_hieu_tien_te,mter.quoc_gia 
                            FROM [ti_gia_tien_te] cs LEFT JOIN tien_te mter
                            ON cs.ma_tien_te=mter.ma_tien_te");
                }
                return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(string Id)
        {
            if (asset.Delete)
            {
                try
                {
                    var dbConn = Helpers.OrmliteConnection.openConn();
                    string[] separators = { "@@" };
                    var listRowID = Id.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        dbConn.Delete<ti_gia_tien_te>(s => s.ID == int.Parse(item));
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to create record" });
            }
        }
        public ActionResult SetDefault(string Id)
        {
            if (asset.Update)
            {
                try
                {
                    var dbConn = Helpers.OrmliteConnection.openConn();
                    if (!string.IsNullOrEmpty(Id))
                    {
                        dbConn.Update<ti_gia_tien_te>(set: "tien_te_mac_dinh=0 , ngay_cap_nhat='" + DateTime.Now + "', nguoi_cap_nhat='" + currentUser.UserName + "'", where: " tien_te_mac_dinh=1");
                        dbConn.Update<ti_gia_tien_te>(set: "tien_te_mac_dinh=1 , ngay_cap_nhat='" + DateTime.Now + "', nguoi_cap_nhat='" + currentUser.UserName + "'", where: "ma_tien_te='" + Id + "'");
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to updated record" });
            }
        }
        
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ti_gia_tien_te item)
        {
            if (asset.Create)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            var check = dbConn.Select<ti_gia_tien_te>(m => m.ma_tien_te == item.ma_tien_te);
                            if (check == null | check.Count == 0)
                            {
                                item.ngay_tao = DateTime.Now;
                                item.nguoi_tao = currentUser.UserName;
                                item.ngay_cap_nhat = DateTime.Parse("1900-01-01");
                                item.nguoi_cap_nhat = "";
                                if (Request["DefaultCurrency"] == "true")
                                {
                                    item.tien_te_mac_dinh = true;
                                }
                                else
                                {
                                    item.tien_te_mac_dinh = false;
                                }
                                dbConn.Insert(item);
                                dbTrans.Commit();
                                return Json(new { success = true, message = "" });
                            }
                            else
                            {
                                item.ngay_tao = DateTime.Now;
                                item.nguoi_tao = currentUser.UserName;
                                item.ngay_cap_nhat = DateTime.Now;
                                item.nguoi_cap_nhat = currentUser.UserName;
                                dbConn.Update(item);
                                dbTrans.Commit();
                                return Json(new { success = true, message = "" });
                            }
                        }
                        catch (Exception ex)
                        {
                            return Json(new { success = false, message = ex.Message });
                        }
                    }
                }
            }
            else
            {
                return Json(new { success = false, message = "You don't have permission" });
            }


        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create1([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ti_gia_tien_te> items)
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
                                    //item.IsNew = true;
                                    //item.CreatedAt = DateTime.Now;
                                    //item.CreatedBy = User.Identity.Name;
                                    //dbConn.Insert(item);
                                    //int Id = (int)dbConn.GetLastInsertId();
                                    //item.Id = Id;
                                    //item.CompanyID = "C" + DateTime.Now.ToString("yyMMdd") + Id;
                                    ////item.CompanyID = "C" + DateTime.Now.ToString("yyMMdd") + string.Format("{0:000}", Id);
                                    //dbConn.Update(item);
                                    //var log = new Deca_Company_Log();
                                    //log.CompanyID = item.CompanyID;
                                    //log.Log = item;
                                    //log.CreatedAt = DateTime.Now;
                                    //log.CreatedBy = currentUser.UserName;
                                    //dbConn.Insert(log);

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
        public ActionResult Update1([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ti_gia_tien_te> items)
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
                                //item.IsNew = true;
                                //item.UpdatedAt = DateTime.Now;
                                //item.UpdatedBy = User.Identity.Name;
                                //dbConn.Update(item);
                                //var log = new Deca_Company_Log();
                                //log.CompanyID = item.CompanyID;
                                //log.Log = item;
                                //log.CreatedAt = DateTime.Now;
                                //log.CreatedBy = currentUser.UserName;
                                //dbConn.Insert(log);
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

        public ActionResult GetListCurrency([DataSourceRequest]DataSourceRequest request)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                return Json(dbConn.Select<ti_gia_tien_te>().OrderBy(a => a.ID), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetDetailCurrencyCode(string CurrencyCode)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = dbConn.Select<ti_gia_tien_te>(@"SELECT cs.*, mter.ten_tien_te,mter.ki_hieu_tien_te,mter.quoc_gia 
                            FROM [ti_gia_tien_te] cs LEFT JOIN tien_te mter
                            ON cs.ma_tien_te=mter.ma_tien_te WHERE cs.ma_tien_te='" + CurrencyCode + "'");
                return Json(new { data, success = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCurrency(string CurrencyCode)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = dbConn.Select<tien_te>(m => m.ma_tien_te == CurrencyCode);
                return Json(new { data, success = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCurrencyDefault()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var CurrencyName = "";
                var data = "";
                try
                {
                    CurrencyName = dbConn.FirstOrDefault<tien_te>(@"SELECT cs.*, mter.ten_tien_te +' - '+mter.ma_tien_te  AS ten_tien_te
                            FROM [ti_gia_tien_te] cs LEFT JOIN tien_te mter
                            ON cs.ma_tien_te=mter.ma_tien_te
							WHERE cs.tien_te_mac_dinh=1").ten_tien_te;
                }
                catch
                {
                    CurrencyName = "";
                }
                finally
                {
                    data = CurrencyName;
                }
                return Json(new { data, success = true }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}