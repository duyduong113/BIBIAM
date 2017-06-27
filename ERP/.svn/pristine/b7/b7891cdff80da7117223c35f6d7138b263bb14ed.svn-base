using MCC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using Kendo.Mvc.UI;
using System.Data;
using MCC.Helpers;
using System.IO;
using OfficeOpenXml;
using CloudinaryDotNet;
using System.Configuration;
using Hangfire;
using MCC.Filters;
using BIBIAM.Core;

namespace MCC.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update")]
    public class UserProfileController : CustomController
    {
        //
        // GET: /UserProfile/
        public ActionResult Index()
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.SingleOrDefault<tw_User>("name={0}", currentUser.name);
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult CreateUpdate(tw_User data, HttpPostedFileBase file)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())

                {
                    var exist = dbConn.SingleOrDefault<tw_User>("name={0}", currentUser.name);
                    data.imagesPublicId = exist.imagesPublicId;
                    data.imagesSize = exist.imagesSize;
                    data.updatedAt = DateTime.Now;
                    data.updatedBy = currentUser.name;
                    if (file != null && file.ContentLength > 0)
                    {
                        string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                        if (fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif" || fileExtension == ".jpg")
                        {
                            string publicId = "CRM/User/" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + file.FileName.Substring(0, file.FileName.Length - fileExtension.Length);
                            //data.imagesPublicId = publicId;
                            string LocalPath = "";
                            data.imagesPublicId = new AzureHelper().UploadImageToAzure(AllConstant.FoldderName_Merchant, file, currentUser.name, ref LocalPath);
                            // imagesSize = new Helpers.CloudinaryAPI().Upload(file, publicId);
                            //data.imagesSize = imagesSize;
                        }
                        else
                        {
                            return Json(new { success = false, error = "Please select correct file type." });
                        }
                    }
                    dbConn.UpdateOnly(data,
                            onlyFields: p =>
                                new
                                {
                                    p.fullName,
                                    p.phone,
                                    p.address,
                                    p.country,
                                    p.city,
                                    p.district,
                                    p.birthday,
                                    p.gender,
                                    p.imagesPublicId,
                                    p.imagesSize,
                                    p.updatedAt,
                                    p.updatedBy
                                },
                        where: p => p.id == exist.id);

                }
                return Json(new { success = true, data = data });
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }
    }
}