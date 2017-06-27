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
    public class ContractManagerController : CustomController
    {
        //
        // GET: /Organization/
        public ActionResult Index()
        {
            if (asset.View)
            {
                //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                //{
                //    OrmLiteConfig.DialectProvider.UseUnicode = true;
                //    dbConn.DropTables(typeof(CRM_Contract_ListManager));
                //    const bool overwrite = false;
                //    dbConn.CreateTables(overwrite, typeof(CRM_Contract_ListManager));
                //}
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {

                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;
                    ViewData["AllowImport"] = asset.Export;
                    ViewData["Asset"] = asset;
                }
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            string whereCondition = "";
            if (request.Filters.Count > 0)
            {
                whereCondition = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
            }
            var data = dbConn.Select<CRM_Contract_ListManager>(whereCondition);
            return Json(data.ToDataSourceResult(request));
        }
        //
        // GET: /DeliveryManage/Create
        public ActionResult Create(CRM_Contract_ListManager item)
        {
            IDbConnection db = Helpers.OrmliteConnection.openConn();
            try
            {
                if (!string.IsNullOrEmpty(item.FullName) && !string.IsNullOrEmpty(item.CityName) && !string.IsNullOrEmpty(item.AcountName))
                {
                    var isExistName = db.SingleOrDefault<CRM_Contract_ListManager>("Select * from CRM_Contract_ListManager Where FullName='" + item.FullName + "'");
                    if (isExistName == null)
                    {
                        if (asset.Create && item.CreatedAt == null && item.CreatedBy == null)
                        {
                            //if (isExist != null)
                            //    return Json(new { success = false, message = "Mã lý do đã tồn tại!" });
                            item.FullName = !string.IsNullOrEmpty(item.FullName) ? item.FullName : "";
                            item.CityName = !string.IsNullOrEmpty(item.CityName) ? item.CityName : "";
                            item.Position = !string.IsNullOrEmpty(item.Position) ? item.Position : "";
                            item.Address = !string.IsNullOrEmpty(item.Address) ? item.Address : "";
                            item.Phone = !string.IsNullOrEmpty(item.Phone) ? item.Phone : "";
                            item.Fax = !string.IsNullOrEmpty(item.Fax) ? item.Fax : "";
                            item.AcountName = !string.IsNullOrEmpty(item.AcountName) ? item.AcountName : "";
                            item.BankName = !string.IsNullOrEmpty(item.BankName) ? item.BankName : "";
                            item.BranchName = !string.IsNullOrEmpty(item.BranchName) ? item.BranchName : "";
                            item.TaxNumber = !string.IsNullOrEmpty(item.BranchName) ? item.BranchName : "";
                            item.CreatedAt = DateTime.Now;
                            item.CreatedBy = currentUser.UserName;
                            item.UpdatedAt = DateTime.Parse("1900-01-01");
                            item.UpdatedBy = "";
                            db.Insert<CRM_Contract_ListManager>(item);
                            long lastInsertId = db.GetLastInsertId();
                            return Json(new { success = true, ID = lastInsertId, CreatedBy = item.CreatedBy, CreatedAt = item.CreatedAt });
                        }
                        else if (asset.Update)
                        {
                            item.FullName = !string.IsNullOrEmpty(item.FullName) ? item.FullName : "";
                            item.CityName = !string.IsNullOrEmpty(item.CityName) ? item.CityName : "";
                            item.Position = !string.IsNullOrEmpty(item.Position) ? item.Position : "";
                            item.Address = !string.IsNullOrEmpty(item.Address) ? item.Address : "";
                            item.Phone = !string.IsNullOrEmpty(item.Phone) ? item.Phone : "";
                            item.Fax = !string.IsNullOrEmpty(item.Fax) ? item.Fax : "";
                            item.AcountName = !string.IsNullOrEmpty(item.AcountName) ? item.AcountName : "";
                            item.BankName = !string.IsNullOrEmpty(item.BankName) ? item.BankName : "";
                            item.BranchName = !string.IsNullOrEmpty(item.BranchName) ? item.BranchName : "";
                            item.TaxNumber = !string.IsNullOrEmpty(item.BranchName) ? item.BranchName : "";
                            item.UpdatedAt = DateTime.Now;
                            item.UpdatedBy = currentUser.UserName;
                            db.Update<CRM_Contract_ListManager>(item);
                            return Json(new { success = true });
                        }
                        else
                            return Json(new { success = false, message = "Bạn không có quyền" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Mã tham số đã tồn tại!" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Chưa nhập giá trị" });
                }
            }
            catch (Exception e)
            {
                //log.Error("Parameter - Create - " + e.Message);
                return Json(new { success = false, message = e.Message });
            }
            finally { db.Close(); }
        }

        public ActionResult UpdateDetail([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<CRM_Contract_ListManager> list)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            if (asset.Update)
            {
                if (list != null)
                {
                    try
                    {
                        foreach (var item in list)
                        {

                            if (dbConn.Select<CRM_Contract_ListManager>(s => s.ID == item.ID).Count() > 0)
                            {
                                dbConn.Update<CRM_Contract_ListManager>(set: "CityName = '" + item.CityName + "', FullName = '" +
                                    item.FullName + "', Position =N'" + item.Position + "' , Address =N'" + item.Address + "', Phone ='" +
                                    item.Phone + "', Fax =N'" + item.Fax + "' , AcountName =N'" + item.AcountName + "', BankName ='" +
                                    item.BankName + "', BankName =N'" + item.BankName + "' , BranchName =N'" + item.BranchName + "', TaxNumber ='" +
                                    item.TaxNumber + "', UpdatedAt ='" +
                                    currentUser.UserName + "'", where: "ID = " + item.ID);
                            }
                            else
                            {
                                var data = new CRM_Contract_ListManager();
                                data.CityName = item.CityName;
                                data.FullName = item.FullName;
                                data.Position = item.Position;
                                data.Address = item.Address;
                                data.Phone = !string.IsNullOrEmpty(item.Phone) ? item.Phone : "";
                                data.Fax = !string.IsNullOrEmpty(item.Fax) ? item.Fax : "";
                                data.AcountName = !string.IsNullOrEmpty(item.AcountName) ? item.AcountName : "";
                                data.BankName = !string.IsNullOrEmpty(item.BankName) ? item.BankName : "";
                                data.BranchName = !string.IsNullOrEmpty(item.BranchName) ? item.BranchName : "";
                                data.TaxNumber = !string.IsNullOrEmpty(item.BranchName) ? item.BranchName : "";
                                data.CreatedAt = DateTime.Now;
                                data.CreatedBy = currentUser.UserName;
                                data.UpdatedAt = DateTime.Parse("1900-01-01");
                                data.UpdatedBy = "";
                                dbConn.Insert<CRM_Contract_ListManager>(data);
                            }
                        }
                        ModelState.AddModelError("Thành công!", "Lưu thành công.");
                        return Json(new { sussess = true });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("error", ex.Message);
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                }
                return Json(new { sussess = true });
            }
            else if (asset.Create)
            {
                foreach (var item in list)
                {
                    var isExist = dbConn.GetByIdOrDefault<CRM_Contract_ListManager>(item.ID);
                    if (isExist == null)
                    {
                        var data = new CRM_Contract_ListManager();
                        data.CityName = item.CityName;
                        data.FullName = item.FullName;
                        data.Position = item.Position;
                        data.Address = item.Address;
                        data.Phone = !string.IsNullOrEmpty(item.Phone) ? item.Phone : "";
                        data.Fax = !string.IsNullOrEmpty(item.Fax) ? item.Fax : "";
                        data.AcountName = !string.IsNullOrEmpty(item.AcountName) ? item.AcountName : "";
                        data.BankName = !string.IsNullOrEmpty(item.BankName) ? item.BankName : "";
                        data.BranchName = !string.IsNullOrEmpty(item.BranchName) ? item.BranchName : "";
                        data.TaxNumber = !string.IsNullOrEmpty(item.BranchName) ? item.BranchName : "";
                        data.CreatedAt = DateTime.Now;
                        data.CreatedBy = currentUser.UserName;
                        data.UpdatedAt = DateTime.Parse("1900-01-01");
                        data.UpdatedBy = "";
                        dbConn.Insert<CRM_Contract_ListManager>(data);
                    }
                }
                ModelState.AddModelError("Thành công!", "Lưu thành công.");
                return Json(new { sussess = true });
            }
            else
            {
                ModelState.AddModelError("error", "Bạn không có quyền cập nhật.");
                return Json(list.ToDataSourceResult(request, ModelState));
            }
        }
        public ActionResult DeleteItem(string data)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    var detail = new CRM_Contract_ListManager();
                    foreach (var item in listdata)
                    {
                        if (dbConn.Select<CRM_Contract_ListManager>(s => s.ID == int.Parse(item)).Count() > 0)
                        {
                            var success = dbConn.Delete<CRM_Contract_ListManager>(where: "ID = '" + item + "'") >= 1;

                            if (!success)
                            {
                                return Json(new { success = false, message = "Không thể lưu" });
                            }
                        }
                    }
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            else
            {
                return Json(new { success = false, message = "Không có quyền xóa." });
            }
        }
        public ActionResult GetItemByFullName(string fullName)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = dbConn.Select<CRM_Contract_ListManager>(m => m.FullName == fullName);
                return Json(new { data, success = true }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}