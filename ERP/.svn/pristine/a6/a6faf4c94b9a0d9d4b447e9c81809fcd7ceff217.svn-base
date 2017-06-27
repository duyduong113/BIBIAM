using ERPAPD.Helpers;
using ERPAPD.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using Kendo.Mvc.Extensions;

namespace ERPAPD.Controllers
{
    public class CompanyContactListController : CustomController
    {
        // GET: CompanyContactList
        public ActionResult Index()
        {
            ViewData["AllowCreate"] = asset.Create;
            ViewData["AllowUpdate"] = asset.Update;
            ViewData["AllowDelete"] = asset.Delete;
            ViewData["AllowExport"] = asset.Export;
            return View();
        }
        public ActionResult CompanyContactList_Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT * FROM  CRM_Company_Contact_List";  
                    data = KendoApplyFilter.KendoDataByQuery<CRM_Company_Contact_List>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult CompanyContactList_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Company_Contact_List> listEx)
        {
            if (asset.Create)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listEx != null)
                        {
                            foreach (var regis in listEx)
                            {
                                if (String.IsNullOrEmpty(regis.FullName))
                                {
                                    ModelState.AddModelError("", "Vui nhập tên");
                                    return Json(listEx.ToDataSourceResult(request, ModelState));
                                }
                                if (String.IsNullOrEmpty(regis.CompanyID))
                                {
                                    ModelState.AddModelError("", "Vui lòng chọn Com");
                                    return Json(listEx.ToDataSourceResult(request, ModelState));
                                }

                                string id = "";
                                var contacts = new CRM_Company_Contact_List();

                                var checkID = dbConn.Select<CRM_Company_Contact_List>().OrderByDescending(m => m.RowCreatedTime).FirstOrDefault();
                                if (checkID != null)
                                {
                                    
                                    var nextNo = Int64.Parse(checkID.ContactID.Substring(3, checkID.ContactID.Length - 3)) + 1;
                                    id = "CTL" + String.Format("{0:00000000000}", nextNo);
                                }
                                else
                                {
                                    id = "CTL00000000001";
                                }

                                //var check = dbConn.FirstOrDefault<DC_Location_Wards>("WardsName COLLATE Latin1_General_CI_AI LIKE {0} AND DistrictID = {1}", regis.WardsName, regis.DistrictID);
                                //if (check != null)
                                //{
                                //    ModelState.AddModelError("", " Phường/Xã đã tồn tại trong Quận/Huyện này");
                                //    return Json(listEx.ToDataSourceResult(request, ModelState));
                                //}

                                contacts.ContactID = id;

                                contacts.CompanyID = regis.CompanyID;
                                contacts.FullName = regis.FullName.Trim();
                                contacts.Gender = regis.Gender;
                                contacts.Birthday = regis.Birthday;
                                contacts.Mobile = regis.Mobile.Trim();
                                contacts.Officephone = regis.Officephone.Trim();
                                contacts.Fax = regis.Fax.Trim();
                                contacts.Email = regis.Email.Trim();
                                contacts.Address = regis.Address.Trim();
                                contacts.Position = regis.Position.Trim();
                                contacts.Leadby = regis.Leadby.Trim();
                                contacts.Description = "";
                                contacts.Note = "";
                               
                                contacts.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                                contacts.RowLastUpdatedUser = "";
                                contacts.RowCreatedTime = DateTime.Now;
                                contacts.RowCreatedUser = currentUser.UserName;

                                dbConn.Insert(contacts);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        return Json(listEx.ToDataSourceResult(request, ModelState));
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listEx.ToDataSourceResult(request, ModelState));
            }
            return Json(listEx.ToDataSourceResult(request));
        }
        public ActionResult CompanyContactList_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Company_Contact_List> listEx)
        {
            if (asset.Update)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listEx != null)
                        {
                            foreach (var regis in listEx)
                            {

                                var contacts = dbConn.FirstOrDefault<CRM_Company_Contact_List>("ContactID={0}", regis.ContactID);

                                contacts.ContactID = regis.ContactID;

                                contacts.CompanyID = regis.CompanyID;
                                contacts.FullName = regis.FullName.Trim();
                                contacts.Gender = regis.Gender;
                                contacts.Birthday = regis.Birthday;
                                contacts.Mobile = regis.Mobile.Trim();
                                contacts.Officephone = regis.Officephone.Trim();
                                contacts.Fax = regis.Fax.Trim();
                                contacts.Email = regis.Email.Trim();
                                contacts.Address = regis.Address.Trim();
                                contacts.Position = regis.Position.Trim();
                                contacts.Leadby = regis.Leadby.Trim();

                                contacts.RowLastUpdatedTime = DateTime.Now;
                                contacts.RowLastUpdatedUser = currentUser.UserName;
                                dbConn.Update(contacts);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        return Json(listEx.ToDataSourceResult(request, ModelState));
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listEx.ToDataSourceResult(request, ModelState));
            }
            return Json(listEx.ToDataSourceResult(request));
        }
        public ActionResult Delete(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                    {
                        string[] separators = { "@@" };
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listRowID)
                        {
                            var check = dbConn.FirstOrDefault<CRM_Company_Contact_List>("ContactID={0}", item);
                            dbConn.Delete(check);
                        }
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
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }
    }
}