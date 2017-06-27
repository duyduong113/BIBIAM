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
using BIBIAM.Core.Entities;
using BIBIAM.Core.Data.DataObject;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class UsersController : CustomController
    {
        //
        // GET: /Users/
        public ActionResult Index()
        {
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.listDepartment = dbConn.Select<DropDownListDataList>(@"SELECT a.ma_phan_cap as Code,a.ten_phan_cap as Name FROM Hierarchy A ");//CRM_Department.GetAllCRM_Departments();
                    ViewBag.listTeam = dbConn.Select<CRM_Team>();// CRM_Team.GetAllCRM_Teams();
                    ViewBag.listPosition = dbConn.Select<CRM_Position>(); //CRM_Position.GetAllCRM_Positions();
                    ViewData["listGroup"] = dbConn.Select<Groups>("Active = {0}", true).Select(s => new GroupViewModel { Id = s.Id, Name = s.Name });
                }
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }
        public ActionResult getListDepartment()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = CRM_Department.GetAllCRM_Departments();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getListTeam()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = CRM_Team.GetAllCRM_Teams();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getListPosition()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = CRM_Position.GetAllCRM_Positions();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getListGender()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = "";
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getListLevel(string Position)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<DC_Position_Level>("Id ={0}", Position);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Active(string data)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    int i = 0;
                    string[] separators = { "@@" };
                    var listid = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var id in listid)
                    {
                        var item = dbConn.FirstOrDefault<Users>("Id={0}", id);
                        if (asset.Update && item.UserName != "administrator")
                        {
                            item.Active = true;
                            item.LastUpdatedDateTime = DateTime.Now;
                            item.LastUpdatedUser = currentUser.UserName;
                            dbConn.Update(item);
                            i++;
                        }
                    }
                    dbTrans.Commit();
                    return Json(new { success = true, message = i });
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    return Json(new { success = false, error = e.Message });
                }
            }
        }
        public ActionResult Inactive(string data)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    int i = 0;
                    string[] separators = { "@@" };
                    var listid = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var id in listid)
                    {
                        var item = dbConn.FirstOrDefault<Users>("Id={0}", id);
                        if (asset.Update && item.UserName != "administrator")
                        {
                            item.Active = false;
                            item.LastUpdatedDateTime = DateTime.Now;
                            item.LastUpdatedUser = currentUser.UserName;
                            dbConn.Update(item);
                            i++;
                        }
                    }
                    dbTrans.Commit();
                    return Json(new { success = true, message = i });
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    return Json(new { success = false, error = e.Message });
                }
            }
        }
       
        public ActionResult ResetPass(string data)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    int i = 0;
                    string[] separators = { "@@" };
                    var listid = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var id in listid)
                    {
                        var item = dbConn.FirstOrDefault<Users>("Id={0}", id);
                        if (asset.Update && item.UserName != "administrator")
                        {
                            string mailPassword = item.UserName + "@erp2016";
                            item.Password = Helpers.GetMd5Hash.Generate(mailPassword);
                            item.IsAppReset = true;
                            item.LastUpdatedDateTime = DateTime.Now;
                            item.LastUpdatedUser = currentUser.UserName;
                            dbConn.Update(item);
                            SendMail.SendEmail(item.Email, "", "", "ResetPassword", item.UserName, mailPassword, Url.Content(Request.Url.AbsoluteUri).Replace("Users/ResetPass", "Account/ChangePassword"), item.FullName);
                            i++;
                        }
                    }
                    dbTrans.Commit();
                    return Json(new { success = true, message = i });
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    return Json(new { success = false, error = e.Message });
                }
            }
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<Users>();
                if (asset.View)
                {
                    if (request.Filters.Any())
                    {
                        var filter = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = (from user in dbConn.Select<Users>()
                                join employee in dbConn.Select<EmployeeInfo>()
                                on user.Id equals employee.Id
                                select new Users
                                {
                                    Id = user.Id,
                                    UserName = user.UserName,
                                    Password = user.Password,
                                    FullName = user.FullName,
                                    Email = user.Email,
                                    Phone = user.Phone,
                                    ImageUrl = user.ImageUrl,
                                    Groups = user.Groups,
                                    IsAppUser = user.IsAppUser,
                                    Active = user.Active,
                                    CreatedAt = user.CreatedAt,
                                    CreatedBy = user.CreatedBy,
                                    UpdatedAt = user.UpdatedAt,
                                    UpdatedBy = user.UpdatedBy,
                                    CreatedDatetime = employee.CreatedDatetime,
                                    CreatedUser = employee.CreatedUser,
                                    LastUpdatedDateTime = employee.LastUpdatedDateTime,
                                    LastUpdatedUser = employee.LastUpdatedUser,
                                    DepartmentID = employee.DepartmentID.ToString(),
                                    Team = employee.Team,
                                    Position = employee.Position,
                                    Gender = employee.Gender,
                                    CompanyID = employee.CompanyID,
                                    LevelID = employee.LevelID,
                                    Description = employee.Description,
                                    Birthday = employee.Birthday,
                                    StartWorkingDay = employee.StartWorkingDay,
                                    TerminatedDate = employee.TerminatedDate,
                                }).ToList();
                    }
                    else
                    {
                        data = (from user in dbConn.Select<Users>()
                                join employee in dbConn.Select<EmployeeInfo>()
                                on user.Id equals employee.Id
                                select new Users
                                {
                                    Id = user.Id,
                                    UserName = user.UserName,
                                    Password = user.Password,
                                    FullName = user.FullName,
                                    Email = user.Email,
                                    Phone = user.Phone,
                                    ImageUrl = user.ImageUrl,
                                    IsAppUser = user.IsAppUser,
                                    Groups = user.Groups,
                                    Active = user.Active,
                                    CreatedAt = user.CreatedAt,
                                    CreatedBy = user.CreatedBy,
                                    UpdatedAt = user.UpdatedAt,
                                    UpdatedBy = user.UpdatedBy,
                                    CreatedDatetime = employee.CreatedDatetime,
                                    CreatedUser = employee.CreatedUser,
                                    LastUpdatedDateTime = employee.LastUpdatedDateTime,
                                    LastUpdatedUser = employee.LastUpdatedUser,
                                    DepartmentID = employee.DepartmentID.ToString(),
                                    Team = employee.Team,
                                    Position = employee.Position,
                                    Gender = employee.Gender,
                                    CompanyID = employee.CompanyID,
                                    LevelID = employee.LevelID,
                                    Description = employee.Description,
                                    Birthday = employee.Birthday,
                                    StartWorkingDay = employee.StartWorkingDay,
                                    TerminatedDate = employee.TerminatedDate,
                                }).ToList();
                    }
                }
                return Json(data.ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Exclude = "info")] Users item)
        {
         
            if (asset.Create && ModelState.IsValid)
            {
                if (item != null)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        string mailPassword = "";
                        if (string.IsNullOrEmpty(item.Password))
                        {
                            mailPassword = item.UserName + "1234@";
                            item.Password = Helpers.GetMd5Hash.Generate(item.UserName + "1234@");
                        }
                        else
                        {
                            mailPassword = item.Password;
                            item.Password = Helpers.GetMd5Hash.Generate(item.Password);
                        }
                        string email = dbConn.Scalar<string>("SELECT Email FROM Users WHERE Email = '" + item.Email + "'");
                        if (email != null)
                        {
                            ModelState.AddModelError("", "Email này đã tồn tại.");
                            return Json(new Users[] { item }.ToDataSourceResult(request, ModelState));
                        }
                        string username = dbConn.Scalar<string>("SELECT UserName FROM Users WHERE UserName = '" + item.UserName + "'");
                        if (username != null)
                        {
                            ModelState.AddModelError("", "Tài khoản này đã tồn tại.");
                            return Json(new Users[] { item }.ToDataSourceResult(request, ModelState));
                        }
                        item.CreatedAt = DateTime.Now;
                        item.CreatedBy = User.Identity.Name;
                        dbConn.Insert(item);
                        long id = dbConn.GetLastInsertId();
                        EmployeeInfo em = new EmployeeInfo();
                        em.Active = 1;
                        em.Birthday = DateTime.Now;
                        em.CompanyID = item.CompanyID;
                        em.CreatedDatetime = DateTime.Now;
                        em.CreatedUser = User.Identity.Name;
                        em.DepartmentID = item.DepartmentID != null ? int.Parse(item.DepartmentID) : 0;
                        em.Description = item.Description != null ? item.Description : "";
                        em.Email = item.Email != null ? item.Email : "";
                        em.Gender = item.Gender != null ? item.Gender : "";
                        em.XLiteID = item.XLiteID != null ? item.XLiteID : "";
                        em.XLiteCode = item.XLiteCode != null ? item.XLiteCode : "";
                        em.Description = item.Description != null ? item.Description : "";
                        em.Id = int.Parse(id.ToString());
                        em.LastUpdatedDateTime = DateTime.Now;
                        em.LastUpdatedUser = User.Identity.Name;
                        em.LevelID = item.LevelID != null ? item.LevelID : "";
                        em.Phone = item.Phone != null ? item.Phone : "";
                        em.Position = item.Position != null ? item.Position : "";
                        em.StartWorkingDay = DateTime.Now;
                        em.Team = item.Team != null ? item.Team : "";
                        em.TerminatedDate = DateTime.Now;
                        em.UserName = item.UserName;
                        em.Region = dbConn.Select<CRM_Hierarchy>(@"SELECT * FROM CRM_Hierarchy where HierarchyID= (SELECT ParentID FROM CRM_Hierarchy where HierarchyID=" + em.DepartmentID + ")").FirstOrDefault().HierarchyID;
                        var RefID =  dbConn.SingleOrDefault<EmployeeInfo>(@"SELECT '1' AS Id,MAX(RefStaffId) AS RefStaffId FROM EmployeeInfo");
                        if (RefID != null)
                        {
                            em.RefStaffId = RefID.RefStaffId + 1;
                        }
                        dbConn.Insert(em);
                        SendMail.SendEmail(item.Email, "", "", "CreateUser", item.UserName, mailPassword, Url.Content(Request.Url.AbsoluteUri).Replace("Users/Create", "Account/ChangePassword"), item.FullName);

                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Don't have permission");
            }
            return Json(new Users[] { item }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Exclude = "info")] Users item)
        {
           
            if (asset.Update)
            {
                if (item !=null)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        string mailPassword = "";
                        if (item.UserName == "administrator")
                        {
                            ModelState.AddModelError("", "Can't edit user admin");
                        }
                        else
                        {
                            var exist = dbConn.SingleOrDefault<Users>("Id={0}", item.Id);
                            //string email = dbConn.Scalar<string>("SELECT Email FROM Users WHERE Email = '" + item.Email + "' AND Id <> " + item.Id);
                            //if (email != null)
                            //{
                            //    ModelState.AddModelError("", "Email này đã tồn tại.");
                            //    return Json(new Users[] { item }.ToDataSourceResult(request, ModelState));
                            //}
                            item.CreatedAt = exist.CreatedAt;
                            item.Birthday = exist.Birthday;
                            item.CreatedDatetime = exist.CreatedDatetime;
                            item.LastUpdatedDateTime = exist.LastUpdatedDateTime;
                            item.UpdatedAt = DateTime.Now;
                            item.UpdatedBy = User.Identity.Name;
                            dbConn.Update(item);
                            EmployeeInfo em = dbConn.SingleOrDefault<EmployeeInfo>("Id={0}", item.Id);
                            //em.CreatedDatetime = DateTime.Now;
                            //em.CreatedUser = User.Identity.Name;
                            if (!string.IsNullOrEmpty(item.DepartmentID))
                            {
                                em.DepartmentID = int.Parse(item.DepartmentID);
                            }
                            else em.DepartmentID = 0;
                            em.Email = item.Email;
                            em.LastUpdatedDateTime = DateTime.Now;
                            em.LastUpdatedUser = User.Identity.Name;
                            em.Phone = item.Phone;
                            em.Gender = item.Gender;
                            em.Position = item.Position;
                            em.Team = item.Team;
                            em.XLiteID = item.XLiteID != null ? item.XLiteID : "";
                            em.XLiteCode = item.XLiteCode != null ? item.XLiteCode : "";
                            em.Description = item.Description != null ? item.Description : "";
                            dbConn.Update(em);

                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Don't have permission");
            }
            return Json(new Users[] { item }.ToDataSourceResult(request));
        }
        [HttpPost]
        public ActionResult Excel_Export(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        public ActionResult SaveImage(IEnumerable<HttpPostedFileBase> files)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/Content/image/upload/"), fileName);

                    // The files are not actually saved in this demo
                    TempData["fileName"] = fileName;
                    file.SaveAs(physicalPath);
                }
            }
            // Return an empty string to signify success
            return Content("");
        }
    }
}