using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using Kendo.Mvc.UI;
using System.Data;
using CMS.Helpers;
using System.IO;
using OfficeOpenXml;
using CloudinaryDotNet;
using System.Configuration;
using Hangfire;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CMS.Filters;
using System.Net;
using Kendo.Mvc;
using System.ComponentModel;
using System.Data.SqlClient;

namespace CMS.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete,export,reset password,update voip")]
    public class UserManagementController : CustomController
    {
        public UserManagementController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public UserManagementController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        //
        // GET: /UserManagement/
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (IDbConnection dbConn = CMS.Helpers.OrmliteConnection.openConn())
                {
                    //dbConn.DropTables(typeof(tw_UserInShowroom));
                    //const bool overwritte = false;
                    //dbConn.CreateTables(overwritte, typeof(tw_UserInShowroom));

                    ViewBag.listGroup = dbConn.GetDictionary<Int64, string>("SELECT id, name FROM UserGroup WHERE active = 1").Select(s => new UserGroup_Json { id = s.Key, name = s.Value });
                    ViewBag.listShowroom = dbConn.GetDictionary<Int64, string>("SELECT id, name FROM tw_ShowroomBranch WHERE active = 1").Select(s => new UserGroup_Json { id = s.Key, name = s.Value });
                    ViewBag.isAdmin = isAdmin;
                    return View();
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = CMS.Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                data = KendoApplyFilter.KendoData<Users>(request);
                return Json(data);
            }
        }

        [HttpPost]
        public ActionResult CreateUpdate(Users data, HttpPostedFileBase file, string password)
        {
            try
            {
                using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
                { 
                    if (data.id > 0)
                    {
                        if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
                        {
                            var exist = dbConn.SingleOrDefault<Users>("id={0}", data.id);
                            data.imagesPublicId = exist.imagesPublicId;
                            //data.imagesSize = exist.imagesSize;
                            data.updatedAt = DateTime.Now;
                            data.updatedBy = currentUser.name;
                            if (file != null && file.ContentLength > 0)
                            {
                                string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                                if (fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".jpg")
                                {
                                    //string publicId = "CRM/Localization/" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + file.FileName.Substring(0, file.FileName.Length - fileExtension.Length);
                                    //data.imagesPublicId = publicId;
                                    //var imagesSize = new Helpers.CloudinaryAPI().Upload(file, publicId);
                                    //data.imagesSize = imagesSize;
                                    data.imagesPublicId = new AzureHelper().UploadImageToAzure("UserManagement", file, currentUser.name);
                                }
                                else
                                {
                                    return Json(new { success = false, error = "Vui lòng chọn đúng file ảnh." });
                                }
                            }
                            //RevertImage(data);
                            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update voip"]))
                            {
                                dbConn.UpdateOnly(data,
                                    onlyFields: p =>
                                        new
                                        {
                                            p.extension
                                        },
                                where: p => p.id == data.id);
                            }

                            dbConn.UpdateOnly(data,
                                    onlyFields: p =>
                                        new
                                        {
                                            p.homePage,
                                            p.fullName,
                                            p.phone,
                                            p.address,
                                            p.country,
                                            p.city,
                                            p.district,
                                            p.birthday,
                                            p.gender,
                                            p.imagesPublicId,
                                            p.active,
                                            p.updatedAt,
                                            p.updatedBy,
                                            p.companycode
                                        },
                                where: p => p.id == data.id);
                            //Insert qua FE                         
                        }
                        else
                        {
                            return Json(new { success = false, error = "Don't have permission to update" });
                        }
                    }
                    else
                    {
                        if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
                        {
                            if (!String.IsNullOrEmpty(password))
                            {
                                var exist = dbConn.SingleOrDefault<Users>("email={0}", data.email);
                                if (exist == null)
                                {
                                    var user = new ApplicationUser() { UserName = Helpers.RemoveVietNameChar.Remove(data.name), PhoneNumber = data.phone, Email = data.email };
                                    var result = UserManager.Create(user, password);
                                    if (result.Succeeded)
                                    {
                                        data.userKey = user.Id;
                                        data.registerAt = DateTime.Now;
                                        data.createdAt = DateTime.Now;
                                        data.createdBy = currentUser.name;
                                        //data.ma_gian_hang = currentUser.ma_gian_hang;
                                        ///
                                        dbConn.Insert(data);
                                        int Id = (int)dbConn.GetLastInsertId();
                                        data.id = Id;
                                        if (file != null && file.ContentLength > 0)
                                        {
                                            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                                            if (fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".jpg")
                                            {
                                                //string publicId = "CRM/Localization/" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + file.FileName.Substring(0, file.FileName.Length - fileExtension.Length);
                                                //data.imagesPublicId = publicId;
                                                //var imagesSize = new Helpers.CloudinaryAPI().Upload(file, publicId);
                                                //data.imagesSize = imagesSize;
                                                data.imagesPublicId = new AzureHelper().UploadImageToAzure("UserManagement", file, currentUser.name);
                                            }
                                            else
                                            {
                                                return Json(new { success = false, error = "Vui lòng chọn đúng file ảnh." });
                                            }
                                        }
                                     
                                    }
                                    else
                                    {
                                        return Json(new { success = false, error = AddErrors(result) });
                                    }
                                }
                                else
                                {
                                    return Json(new { success = false, error = "Email is existed" });
                                }
                            }
                            else
                            {
                                return Json(new { success = false, error = "Please input password" });
                            }
                        }
                        else
                        {
                            return Json(new { success = false, error = "Don't have permission to create" });
                        }

                    }

                    if (data.groups != null && data.groups.Count > 0)
                    {
                        foreach (var item in data.groups)
                        {
                            var exist = dbConn.SingleOrDefault<UserInGroup>("userId={0} AND groupId={1}", data.id, item);
                            if (exist == null)
                            {
                                var userInGroup = new UserInGroup();
                                userInGroup.userId = data.id;
                                userInGroup.groupId = item;
                                userInGroup.createdAt = DateTime.Now;
                                userInGroup.createdBy = currentUser.name;
                                dbConn.Insert(userInGroup);
                            }
                        }
                        dbConn.Delete<UserInGroup>("userId = {0} AND groupId NOT IN (" + String.Join(",", data.groups.Select(s => s)) + ")", data.id);
                    }
                    else
                    {
                        var userInGroup = new UserInGroup();
                        userInGroup.userId = data.id;
                        userInGroup.groupId = 2;
                        userInGroup.createdAt = DateTime.Now;
                        userInGroup.createdBy = currentUser.name;
                        dbConn.Insert(userInGroup);
                    }

                    if (data.showrooms != null && data.showrooms.Count > 0)
                    {
                        foreach (var item in data.showrooms)
                        {
                            var exist = dbConn.SingleOrDefault<tw_UserInShowroom>("userId={0} AND showroomId={1}", data.id, item);
                            if (exist == null)
                            {
                                var userInShowroom = new tw_UserInShowroom();
                                userInShowroom.userId = data.id;
                                userInShowroom.showroomId = item;
                                userInShowroom.createdAt = DateTime.Now;
                                userInShowroom.createdBy = currentUser.name;
                                dbConn.Insert(userInShowroom);
                            }
                        }
                        dbConn.Delete<tw_UserInShowroom>("userId = {0} AND showroomId NOT IN (" + String.Join(",", data.showrooms.Select(s => s)) + ")", data.id);
                    }
            
                }
                return Json(new { success = true, data = data });
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        private string AddErrors(IdentityResult result)
        {
            return String.Join(",", result.Errors.Select(s => s));
        }

        public ActionResult ExportData([DataSourceRequest]DataSourceRequest request)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\User_Template.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);

                string fileName = "User_Template_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                var data = new List<Users>();
                if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["export"]))
                {
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Select<Users>(where).ToList();
                    }
                    else
                    {
                        data = dbConn.Select<Users>().ToList();
                    }
                }
                ExcelWorksheet Sheet = excelPkg.Workbook.Worksheets["Data"];

                int rowData = 1;
                var territory = dbConn.Select<Territory>();
                foreach (var item in data)
                {
                    int i = 1;
                    rowData++;
                    Sheet.Cells[rowData, i++].Value = item.name;
                    Sheet.Cells[rowData, i++].Value = item.fullName;
                    Sheet.Cells[rowData, i++].Value = item.gender;
                    Sheet.Cells[rowData, i++].Value = item.birthday;
                    Sheet.Cells[rowData, i++].Value = item.phone;
                    Sheet.Cells[rowData, i++].Value = item.email;
                    Sheet.Cells[rowData, i++].Value = item.address;
                    Sheet.Cells[rowData, i++].Value = territory.SingleOrDefault(s => s.Id == item.country) != null ? territory.SingleOrDefault(s => s.Id == item.country).Name : "";
                    Sheet.Cells[rowData, i++].Value = territory.SingleOrDefault(s => s.Id == item.city) != null ? territory.SingleOrDefault(s => s.Id == item.city).Name : "";
                    Sheet.Cells[rowData, i++].Value = territory.SingleOrDefault(s => s.Id == item.district) != null ? territory.SingleOrDefault(s => s.Id == item.district).Name : "";

                }
                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
        }

        public ActionResult ResetPass(string username)
        {
            try
            {
                if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["reset password"]))
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        var aspUser = UserManager.FindByName(username);
                        var user = dbConn.SingleOrDefault<Users>("name={0}", username);
                        if (aspUser != null && user != null)
                        {
                            string body = string.Empty;
                            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/EmailTemplate/resetPassUserTemplate.html")))
                            {
                                body = reader.ReadToEnd();
                            }

                            string newPass = Helpers.RandomString.Generate(8);
                            body = body.Replace("{fullName}", user.fullName);
                            body = body.Replace("{newPass}", newPass);

                            UserManager.RemovePassword(aspUser.Id);
                            IdentityResult result = UserManager.AddPassword(aspUser.Id, newPass);
                            if (result.Succeeded)
                            {
                              
                            }
                            else
                            {
                                return Json(new { success = false, error = AddErrors(result) });
                            }
                        }
                        else
                        {
                            return Json(new { success = false, error = "User is not existed" });
                        }

                        return Json(new { success = true });
                    }
                }
                else
                {
                    return Json(new { success = false, error = "Don't have permission to reset pass" });
                }

            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        
    
    }
}