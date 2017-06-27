using ERPAPD.Helpers;
using ERPAPD.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ServiceStack.OrmLite;

namespace ERPAPD.Controllers
{
    public class CRMContactListController : CustomController
    {
        // GET: CRMContactList
        public ActionResult Index()
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                // ViewBag.listSubCategory = dbConn.Select<CRM_SubCategory>(s => s.Active == true);
                ViewBag.listCategory = dbConn.Select<CRM_CategoryHierarchy>(@"SELECT ID,[HierarchyID],Value,ParentID,[Level] FROM CRM_CategoryHierarchy WHERE Level=1 ");
                ViewBag.listSubCategory = dbConn.Select<CRM_CategoryHierarchy>(@"SELECT ID,[HierarchyID],Value,ParentID,[Level] FROM CRM_CategoryHierarchy WHERE Level<>1 AND Level<>0");
                ViewBag.listCustomer = dbConn.Select<ERPAPD_Customer>("SELECT TOP 10 CustomerID,CustomerName FROM ERPAPD_Customer ");
                ViewBag.listGender = dbConn.Select<Parameters>(s => s.Type == "Gender");
                ViewBag.listColumns = dbConn.Select<ERPAPD_Contacts>("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'ERPAPD_Contacts'");
                if (Request.Cookies["permissions_contacts_list_" + currentUser.Id] != null)
                {
                    ViewBag.listColumnsCookie = Request.Cookies["permissions_contacts_list_" + currentUser.Id];
                }
                else
                {
                    ViewBag.listColumnsUserConfig = dbConn.Select<Users_Permissions>("SELECT CustomerInfo FROM Users_Permissions WHERE UserID = 1");
                }
                return View();
            }
        }
        public ActionResult CRMContactList_Read([DataSourceRequest]DataSourceRequest request, string CustomerID)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT co.*,cus.CustomerName AS CustomerName FROM  ERPAPD_Contacts co 
                    LEFT JOIN ERPAPD_Customer cus ON cus.CustomerID = co.CustomerID where FKCustomer='" + CustomerID+"'";
                    data = KendoApplyFilter.KendoDataByQuery<ERPAPD_Contacts>(request, strQuery, "");
                    request.Filters = null;
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult getSubCategory(string ParentID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                if (ParentID != null && ParentID != "")
                {
                    var data = dbConn.Select<ERPAPD_Contacts>(@"SELECT ID,[HierarchyID],Value,ParentID,[Level] FROM CRM_CategoryHierarchy
                                    WHERE  ParentID='" + int.Parse(ParentID) + "'");
                    return Json(new { data = data, success = true });
                }
                else
                {
                    var data = new ERPAPD_Contacts();
                    return Json(new { data = data, success = true });
                }
            }
        }
        public ActionResult GetlistUser(string PKContactID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                if (PKContactID != null && PKContactID != "")
                {
                    var data = dbConn.Select<ERPAPD_Contacts>(s => s.PKContactID == int.Parse(PKContactID)).FirstOrDefault();
                    return Json(new { data = data, success = true });
                }
                else
                {
                    var data = new ERPAPD_Contacts();
                    return Json(new { data = data, success = true });
                }
            }
        }
        public ActionResult CRMContactList_Update(ERPAPD_Contacts request)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    if (request.PKContactID != 0)
                    {
                        var contacts = dbConn.FirstOrDefault<ERPAPD_Contacts>("PKContactID={0}", request.PKContactID);
                        contacts.FirstName = !string.IsNullOrEmpty(request.FirstName) ? request.FirstName.Trim() : "";
                        contacts.MiddleName = !string.IsNullOrEmpty(request.MiddleName) ? request.MiddleName.Trim() : "";
                        contacts.LastName = !string.IsNullOrEmpty(request.LastName) ? request.LastName.Trim() : "";
                        contacts.Name = contacts.FirstName + " " + contacts.MiddleName + " " + contacts.LastName;
                        contacts.DayOfBirth = request.DayOfBirth != 0 ? request.DayOfBirth : 1;
                        contacts.MonthOfBirth = request.MonthOfBirth != 0 ? request.MonthOfBirth : 1;
                        contacts.YearOfBirth = request.YearOfBirth != 0 ? request.YearOfBirth : 1900;
                        contacts.Birthday = DateTime.Parse(contacts.YearOfBirth + "-" + contacts.MonthOfBirth + "-" + contacts.DayOfBirth);
                        contacts.Category = !string.IsNullOrEmpty(request.Category) ? request.Category : "";
                        contacts.Mobile = !string.IsNullOrEmpty(request.Mobile) ? request.Mobile.Trim() : "";
                        contacts.TelephoneHome = !string.IsNullOrEmpty(request.TelephoneHome) ? request.TelephoneHome.Trim() : "";
                        contacts.TelephoneOffice = !string.IsNullOrEmpty(request.TelephoneOffice) ? request.TelephoneOffice.Trim() : "";
                        contacts.Email = !string.IsNullOrEmpty(request.Email) ? request.Email.Trim() : "";
                        contacts.Notes = !string.IsNullOrEmpty(request.Notes) ? request.Notes.Trim() : "";
                        contacts.Title = !string.IsNullOrEmpty(request.Title) ? request.Title.Trim() : "";
                        contacts.FKCustomer = request.FKCustomer;
                        contacts.Decided = request.Decided;
                        contacts.Sex = request.Sex;
                        contacts.Decided = request.Decided;
                        contacts.Status = request.Status;
                        contacts.RowUpdatedAt = DateTime.Now;
                        contacts.RowUpdatedUser = currentUser.UserName;
                        contacts.IsNew = 1;
                        dbConn.Update(contacts);
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {

                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }
        public ActionResult CRMContactList_Create(ERPAPD_Contacts request)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    if (request.PKContactID == 0)
                    {
                        var contacts = new ERPAPD_Contacts();
                        //contacts.newContact();
                        contacts.FirstName = !string.IsNullOrEmpty(request.FirstName) ? request.FirstName.Trim() : "";
                        contacts.MiddleName = !string.IsNullOrEmpty(request.MiddleName) ? request.MiddleName.Trim() : "";
                        contacts.LastName = !string.IsNullOrEmpty(request.LastName) ? request.LastName.Trim() : "";
                        contacts.Gender = !string.IsNullOrEmpty(request.Gender) ? request.Gender.Trim() : "";
                        contacts.Name = contacts.FirstName + " " + contacts.MiddleName + " " + contacts.LastName;
                        contacts.DayOfBirth = request.DayOfBirth != 0 ? request.DayOfBirth : 1;
                        contacts.MonthOfBirth = request.MonthOfBirth != 0 ? request.MonthOfBirth : 1;
                        contacts.YearOfBirth = request.YearOfBirth != 0 ? request.YearOfBirth : 1900;
                        contacts.Birthday = DateTime.Parse(contacts.YearOfBirth + "-" + contacts.MonthOfBirth + "-" + contacts.DayOfBirth);
                        contacts.Category = !string.IsNullOrEmpty(request.Category) ? request.Category : "";
                        contacts.Mobile = !string.IsNullOrEmpty(request.Mobile) ? request.Mobile.Trim() : "";
                        contacts.TelephoneHome = !string.IsNullOrEmpty(request.TelephoneHome) ? request.TelephoneHome.Trim() : "";
                        contacts.TelephoneOffice = !string.IsNullOrEmpty(request.TelephoneOffice) ? request.TelephoneOffice.Trim() : "";
                        contacts.Email = !string.IsNullOrEmpty(request.Email) ? request.Email.Trim() : "";
                        contacts.Notes = !string.IsNullOrEmpty(request.Notes) ? request.Notes.Trim() : "";
                        contacts.Title = !string.IsNullOrEmpty(request.Title) ? request.Title.Trim() : "";
                        contacts.CustomerID = request.CustomerID;
                        contacts.Decided = request.Decided;
                        contacts.Sex = request.Sex;
                        contacts.Status = "HOAT_DONG";
                        contacts.RowCreatedAt = DateTime.Now;
                        contacts.RowCreatedUser = currentUser.UserName;
                        contacts.RowUpdatedAt = DateTime.Parse("1900-01-01");
                        contacts.RowUpdatedUser = "";
                        contacts.IsNew = 1;
                        dbConn.Insert(contacts);
                    }
                    else
                    {
                        var contactsExit = dbConn.FirstOrDefault<ERPAPD_Contacts>("PKContactID={0}", request.PKContactID);
                        contactsExit.FirstName = !string.IsNullOrEmpty(request.FirstName) ? request.FirstName.Trim() : "";
                        contactsExit.MiddleName = !string.IsNullOrEmpty(request.MiddleName) ? request.MiddleName.Trim() : "";
                        contactsExit.LastName = !string.IsNullOrEmpty(request.LastName) ? request.LastName.Trim() : "";
                        contactsExit.Gender = !string.IsNullOrEmpty(request.Gender) ? request.Gender.Trim() : "";
                        contactsExit.Name = contactsExit.FirstName + " " + contactsExit.MiddleName + " " + contactsExit.LastName;
                        contactsExit.DayOfBirth = request.DayOfBirth != 0 ? request.DayOfBirth : 1;
                        contactsExit.MonthOfBirth = request.MonthOfBirth != 0 ? request.MonthOfBirth : 1;
                        contactsExit.YearOfBirth = request.YearOfBirth != 0 ? request.YearOfBirth : 1900;
                        contactsExit.Birthday = DateTime.Parse(contactsExit.YearOfBirth + "-" + contactsExit.MonthOfBirth + "-" + contactsExit.DayOfBirth);
                        contactsExit.Category = !string.IsNullOrEmpty(request.Category) ? request.Category : "";
                        contactsExit.Mobile = !string.IsNullOrEmpty(request.Mobile) ? request.Mobile.Trim() : "";
                        contactsExit.TelephoneHome = !string.IsNullOrEmpty(request.TelephoneHome) ? request.TelephoneHome.Trim() : "";
                        contactsExit.TelephoneOffice = !string.IsNullOrEmpty(request.TelephoneOffice) ? request.TelephoneOffice.Trim() : "";
                        contactsExit.Email = !string.IsNullOrEmpty(request.Email) ? request.Email.Trim() : "";
                        contactsExit.Notes = !string.IsNullOrEmpty(request.Notes) ? request.Notes.Trim() : "";
                        contactsExit.Title = !string.IsNullOrEmpty(request.Title) ? request.Title.Trim() : "";
                        contactsExit.CustomerID = request.CustomerID;
                        contactsExit.Decided = request.Decided;
                        contactsExit.Sex = request.Sex;
                        contactsExit.Status = request.Status;
                        contactsExit.RowUpdatedAt = DateTime.Now;
                        contactsExit.RowUpdatedUser = currentUser.UserName;
                        contactsExit.IsNew = 1;
                        dbConn.Update(contactsExit);
                    }

                }
                catch (Exception e)
                {

                    return Json(new { success = false, message = e.Message });
                }
            }
            return Json(new { success = true });
        }
        public ActionResult Delete(string id)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    string[] separators = { "@@" };
                    var listRowID = id.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        var success = dbConn.Delete<ERPAPD_Contacts>(where: "PKContactID = '" + item + "'") >= 1;
                        if (!success)
                        {
                            return Json(new { success = false, message = "Không thể lưu" });
                        }
                    }
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, alert = ex.Message });
            }
        }
        public ActionResult GetCategory()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<CRM_Category>();
                data = dbConn.Select<CRM_Category>(s => s.Active == true);
                return Json(new { data = data, success = true }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult GetSubCategory()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<CRM_SubCategory>();
                data = dbConn.Select<CRM_SubCategory>(s => s.Active == true);
                return Json(new { data = data, success = true }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult GetCustomer()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<ERPAPD_Customer>();
                data = dbConn.Select<ERPAPD_Customer>();
                return Json(new { data = data, success = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Create_permissions(string request)
        {

            if (currentUser.Id == 2)
            {
                var Users_Permissions = new Users_Permissions();
                var rs = Users_Permissions.Save(request, currentUser);
                return Json(rs);
            }
            else
            {
                var cookieName = "permissions_contacts_list_" + currentUser.Id;
                Response.Cookies[cookieName].Value = request;
                Response.Cookies[cookieName].Expires = DateTime.Now.AddMinutes(15); // add expiry time
                return Json(new { success = true });
            }


        }
    }
}