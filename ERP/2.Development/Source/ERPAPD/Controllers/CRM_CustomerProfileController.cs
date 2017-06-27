using ERPAPD.Helpers;
using ERPAPD.Models;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace ERPAPD.Controllers
{
    public class CRM_CustomerProfileController : CustomController
    {
        // GET: CustomerProfile
        public ActionResult Index()
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        ViewData["AllowCreate"] = asset.Create;
                        ViewData["AllowUpdate"] = asset.Update;
                        ViewData["AllowDelete"] = asset.Delete;
                        ViewData["AllowExport"] = asset.Export;
                        ViewBag.listTypeQC = JsonConvert.SerializeObject(dbConn.Select<Parameters>("Type='TypeQC'").OrderBy(s => s.ParamID));
                        ViewBag.listChannelQC = JsonConvert.SerializeObject(dbConn.Select<Parameters>("Type='ChannelQC'").OrderBy(s => s.ParamID));
                        ViewBag.listTypeQCOL = JsonConvert.SerializeObject(dbConn.Select<Parameters>("Type='TypeQCOL'").OrderBy(s => s.ParamID));
                    }
                    catch (Exception) { }
                    finally { dbConn.Close(); }
                }

                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        // GET: View Create
        public ActionResult Create()
        {
            if (asset.Create)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        ViewData["AllowCreate"] = asset.Create;
                        ViewData["AllowUpdate"] = asset.Update;
                        ViewData["AllowDelete"] = asset.Delete;
                        ViewData["AllowExport"] = asset.Export;

                        ViewBag.listTypeQC = dbConn.Select<Parameters>("Type='TypeQC'").OrderBy(s => s.ParamID);
                        ViewBag.listChannelQC = dbConn.Select<Parameters>("Type='ChannelQC'").OrderBy(s => s.ParamID);
                        ViewBag.listTypeQCOL = dbConn.Select<Parameters>("Type='TypeQCOL'").OrderBy(s => s.ParamID);
                    }
                    catch (Exception) { }
                    finally { dbConn.Close(); }
                }

                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }

        // GET: View Create
        public ActionResult Edit(string CustomerID = "")
        {
            if (asset.Update)
            {
                var cus = new CRM_Customer_Profile();
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        cus = dbConn.SingleOrDefault<CRM_Customer_Profile>("CustomerID = {0}", CustomerID);

                        ViewBag.listTypeQC = dbConn.Select<Parameters>("Type='TypeQC'").OrderBy(s => s.ParamID);
                        ViewBag.listChannelQC = dbConn.Select<Parameters>("Type='ChannelQC'").OrderBy(s => s.ParamID);
                        ViewBag.listTypeQCOL = dbConn.Select<Parameters>("Type='TypeQCOL'").OrderBy(s => s.ParamID);
                        ViewBag.c = cus.AdType;
                        ViewBag.b = cus.AdOnlineChannel;
                        ViewBag.e = cus.AdOnlineType;
                    }
                    catch (Exception e) { }
                    finally { dbConn.Close(); }
                }
                return View("Edit", cus);

            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();

                if (asset.View)
                {
                    string strQuery = @"SELECT  cus.*
                                        FROM CRM_Customer_Profile cus
                                        ";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_Customer_Profile>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }


        [ValidateInput(false)]
        public ActionResult SaveNewProfile(CRM_Customer_Profile p, HttpPostedFileBase file)
        {
            if (asset.Create)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                       
                        p.CustomerID = getLastIdCustomer();
                        p.Status = true;
                        p.CreatedAt = DateTime.Now;
                        p.CreatedBy = currentUser.UserName;
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            Helpers.UploadFile.CreateFolder(Server.MapPath("~/Images/CustomerProfile/" + p.CustomerID + "/"));
                            var path = Path.Combine(Server.MapPath("~/Images/CustomerProfile/" + p.CustomerID + "/"), fileName);
                            file.SaveAs(path);
                            p.Avatar = fileName;
                        }
                        dbConn.Insert<CRM_Customer_Profile>(p); 
                    }
                    catch (Exception e)
                    {

                        return Json(new { success = false, message = e.Message });
                    }
                    finally { dbConn.Close(); }

                }

                return Json(new { success = true });
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        [ValidateInput(false)]
        public ActionResult UpdateProfile(CRM_Customer_Profile p, HttpPostedFileBase file,string CustomerID)
        {
            if (asset.Update)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        //var CustomerID = Request.Form["CustomerID"].ToString();
                        p.Status = true;
                        p.UpdatedAt = DateTime.Now;
                        p.UpdatedBy = currentUser.UserName;
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                           // Helpers.UploadFile.CreateFolder(Server.MapPath("~/Images/CustomerProfile/" + p.CustomerID + "/"));
                            var path = Path.Combine(Server.MapPath("~/Images/CustomerProfile/" + p.CustomerID + "/"), fileName);
                            file.SaveAs(path);
                            p.Avatar = fileName;
                        }
                        dbConn.Update(p);
                    }
                    catch (Exception e)
                    {

                        return Json(new { success = false, message = e.Message });
                    }
                    finally { dbConn.Close(); }

                }

                return Json(new { success = true });
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        public ActionResult Delete(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                    {
                        var listCustomer = dbConn.Select<CRM_Customer_Profile>("[ID] IN (" + string.Join(",", listRowID.Select(p => "'" + p + "'")) + ")");
                        dbConn.DeleteAll<CRM_Customer_Profile>(listCustomer);
                        return Json(new { success = true });
                    }
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
          
        }
        private static string getLastIdCustomer()
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                string CustomerID = String.Empty;
                var lastId = dbConn.FirstOrDefault<CRM_Customer_Profile>("SELECT TOP 1 * FROM CRM_Customer_Profile ORDER BY CustomerID DESC");
                if (lastId != null && lastId.CustomerID != "")
                {
                    var nextNo = int.Parse(lastId.CustomerID) + 1;
                    CustomerID = String.Format("{0:00000}", nextNo);
                }
                else
                {
                    CustomerID = "00001";
                }
                return CustomerID;
            }
        }
    }
}