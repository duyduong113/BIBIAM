using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using ERPAPD.Helpers;
using System.Data;
using OfficeOpenXml;
using ServiceStack.OrmLite;


namespace ERPAPD.Controllers
{
    public class CRMDistrictController : CustomController
    {
        //
        // GET: /DistrictDefinition/
        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                ViewBag.listalias = khu_vuc.GetCRM_Location_Alias("1=1", "AliasName ASC");
                ViewBag.listcountry = CRM_Location_Countries.GetCRM_Location_Countries("1=1", "CountryName ASC");
                ViewBag.listregion = CRM_Location_Region.GetCRM_Location_Regions("1=1", "RegionName ASC");
                ViewBag.listcity = CRM_Location_City.GetCRM_Location_Citys("1=1", "CityName ASC");
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult District_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {

                var data = new List<CRM_Location_District>();
                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                    data = CRM_Location_District.GetCRM_Location_Districts(where, "DistrictID DESC"); 
                }
                else
                {
                    data = CRM_Location_District.GetCRM_Location_Districts("1=1", "DistrictID DESC"); 
                }
                var a = new DataSourceResult();
                a.Data = data;
                a.Total = data.Count();
                return Json(a);
            }

        }
        public ActionResult District_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Location_District> listEx)
        {
            if (asset.Create)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listEx != null)
                        {
                            foreach (var regis in listEx)
                            {
                                if (String.IsNullOrEmpty(regis.DistrictName))
                                {
                                    ModelState.AddModelError("", "Please input District Name ");
                                    return Json(listEx.ToDataSourceResult(request, ModelState));
                                }
                                if (String.IsNullOrEmpty(regis.CityID))
                                {
                                    ModelState.AddModelError("", "Please input City Name");
                                    return Json(listEx.ToDataSourceResult(request, ModelState));
                                }
                                string id = "";
                                var write = new CRM_Location_District();
                                var checkID = dbConn.Select<CRM_Location_District>().OrderByDescending(m => m.RowCreatedTime).FirstOrDefault();
                                if (checkID != null)
                                {
                                    var nextNo = Int32.Parse(checkID.DistrictID.Substring(1, checkID.DistrictID.Length - 1)) + 1;
                                    id = "D" + String.Format("{0:0000}", nextNo);
                                }
                                else
                                {
                                    id = "D0001";
                                }
                                var check = dbConn.FirstOrDefault<CRM_Location_District>("DistrictName COLLATE Latin1_General_CI_AI LIKE {0} AND CityID = {1}", regis.DistrictName, regis.CityID);
                                if (check != null)
                                {
                                    ModelState.AddModelError("", " District Name  is exists.");
                                    return Json(listEx.ToDataSourceResult(request, ModelState));
                                }
                                write.DistrictID = id;
                                write.DistrictName = regis.DistrictName.Trim();
                                write.Active = regis.Active;
                                write.RowLastUpdatedTime = DateTime.Now;
                                write.RowLastUpdatedUser = currentUser.UserName;
                                write.RowCreatedTime = DateTime.Now;
                                write.RowCreatedUser = currentUser.UserName;
                                write.CityID = regis.CityID != null ? regis.CityID : "";
                                dbConn.Insert(write);
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
        public ActionResult District_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Location_District> listEx)
        {
            if (asset.Update)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listEx != null && ModelState.IsValid)
                        {
                            foreach (var regis in listEx)
                            {
                                if (String.IsNullOrEmpty(regis.DistrictName))
                                {
                                    ModelState.AddModelError("", "Please input District Name ");
                                    return Json(listEx.ToDataSourceResult(request, ModelState));
                                }
                                //if (String.IsNullOrEmpty(regis.RegionID))
                                //{
                                //    ModelState.AddModelError("", "Please input Region Name");
                                //    return Json(listEx.ToDataSourceResult(request, ModelState));
                                //}
                                var write = dbConn.FirstOrDefault<CRM_Location_District>("DistrictID={0}", regis.DistrictID);
                                var check = dbConn.FirstOrDefault<CRM_Location_District>("DistrictName COLLATE Latin1_General_CI_AI LIKE {0} AND CityID = {1}", regis.DistrictName, regis.CityID);
                                if (check != null)
                                {
                                    ModelState.AddModelError("", " District Name  is exists.");
                                    return Json(listEx.ToDataSourceResult(request, ModelState));
                                }
                                write.DistrictID = regis.DistrictID;
                                write.DistrictName = regis.DistrictName.Trim();
                                write.Active = regis.Active;
                                write.RowLastUpdatedTime = DateTime.Now;
                                write.RowLastUpdatedUser = currentUser.UserName;
                                write.CityID = regis.CityID != null ? regis.CityID : "";
                                dbConn.Update(write);
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
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(listEx.ToDataSourceResult(request, ModelState));
            }
            return Json(listEx.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Delete(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        string[] separators = { "@@" };
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listRowID)
                        {
                            var check = dbConn.FirstOrDefault<CRM_Location_District>("DistrictID={0}", item);
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
        public FileResult ExportExcel([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var country = new List<CRM_Location_District>();

                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                        country =  CRM_Location_District.GetCRM_Location_Districts(where, "DistrictID DESC"); 

                    }
                    else
                    {
                        country = CRM_Location_District.GetCRM_Location_Districts("1=1", "DistrictID DESC"); 
                    }

                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\CRM_Location_District.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    //data sheet
                    ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["CRM_Location_District"];

                    IEnumerable listData = country.ToDataSourceResult(request).Data;

                    int rowData = 1;
                    foreach (CRM_Location_District data in listData)
                    {
                        int i = 1;
                        rowData++;
                        dataSheet.Cells[rowData, i++].Value = data.DistrictID;
                        dataSheet.Cells[rowData, i++].Value = data.DistrictName;
                        dataSheet.Cells[rowData, i++].Value = (!String.IsNullOrEmpty(data.CityID) ? data.CityID : "") + " - " + (!String.IsNullOrEmpty(data.CityName) ? data.CityName : "");
                        dataSheet.Cells[rowData, i++].Value = data.RegionName;
                        dataSheet.Cells[rowData, i++].Value = data.CountryName;
                        dataSheet.Cells[rowData, i++].Value = data.AliasName;
                        dataSheet.Cells[rowData, i++].Value = data.Active;
                        dataSheet.Cells[rowData, i++].Value = data.RowCreatedTime.ToString();
                        dataSheet.Cells[rowData, i++].Value = data.RowCreatedUser;
                        if (data.RowLastUpdatedTime != null)
                        {
                            dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedTime.ToString();
                        }
                        else
                        {
                            dataSheet.Cells[rowData, i++].Value = "";
                        }
                        dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedUser;

                    }

                    //data sheet
                    var alias = DC_Location_Region.GetDC_Location_Regions("1=1", "CountryID DESC").ToList();
                    ExcelWorksheet dataSheetAliasID = excelPkg.Workbook.Worksheets["List"];
                    IEnumerable listDataAliasID = alias.ToDataSourceResult(request).Data;
                    int rowDataAliasID = 1;
                    foreach (DC_Location_Region data in listDataAliasID)
                    {
                        int i = 1;
                        rowDataAliasID++;
                        dataSheetAliasID.Cells[rowDataAliasID, i++].Value = (!String.IsNullOrEmpty(data.RegionID) ? data.RegionID : "") + " - " + (!String.IsNullOrEmpty(data.RegionName) ? data.RegionName : "");
                    }

                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    string fileName = "CRM_Location_District_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    output.Position = 0;
                    return File(output.ToArray(), contentType, fileName);
                }
            }
            else
            {
                string fileName = "CRM_Location_District_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentType, fileName);
            }
        }
        public ActionResult ImportFromExcel()
        {
            try
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                    {
                        string fileExtension =
                            System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                        if (fileExtension == ".xlsx")
                        {
                            string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);
                            string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUpload"].FileName);

                            if (System.IO.File.Exists(fileLocation))
                                System.IO.File.Delete(fileLocation);

                            Request.Files["FileUpload"].SaveAs(fileLocation);
                            //Request.Files["fileUpload"].SaveAs(errorFileLocation);

                            var rownumber = 2;
                            var total = 0;

                            FileInfo fileInfo = new FileInfo(fileLocation);
                            var excelPkg = new ExcelPackage(fileInfo);

                            FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\CRM_Location_District.xlsx"));
                            template.CopyTo(errorFileLocation);
                            FileInfo _fileInfo = new FileInfo(errorFileLocation);
                            var _excelPkg = new ExcelPackage(_fileInfo);

                            ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["CRM_Location_District"];
                            ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["CRM_Location_District"];

                            //remove row
                            int totalRows = oSheet.Dimension.End.Row;
                            for (int i = 2; i <= totalRows; i++)
                            {
                                string DistrictID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                                string DistrictName = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                                string cityName = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                                //string countryName = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                                string active = oSheet.Cells[i, 6].Value != null ? oSheet.Cells[i, 7].Value.ToString() : "TRUE";
                                string id_city = !String.IsNullOrEmpty(cityName) ? cityName.Substring(0, cityName.LastIndexOf("-")).Trim() : "";

                                try
                                {
                                    var write = new CRM_Location_District();
                                    var check = dbConn.FirstOrDefault<CRM_Location_District>("DistrictName COLLATE Latin1_General_CI_AI LIKE {0} AND CityID = {1}", DistrictName, id_city);
                                    var checkCity = dbConn.FirstOrDefault<CRM_Location_District>("CityID={0}", id_city);
                                    if (string.IsNullOrEmpty(cityName.ToString()) || string.IsNullOrEmpty(DistrictName.ToString()))
                                    {
                                        eSheet.Cells[rownumber, 2].Value = DistrictName;
                                        eSheet.Cells[rownumber, 3].Value = cityName;
                                        eSheet.Cells[rownumber, 6].Value = active;
                                        eSheet.Cells[rownumber, 11].Value = "district, city required";
                                        rownumber++;
                                    }
                                    else if (checkCity != null)
                                    {
                                        write.DistrictID = DistrictID;
                                        write.DistrictName = DistrictName;
                                        write.Active = Convert.ToBoolean(active);
                                        write.RowCreatedTime = DateTime.Now;
                                        write.RowCreatedUser = currentUser.UserName;
                                        write.RowLastUpdatedTime = DateTime.Now;
                                        write.RowLastUpdatedUser = currentUser.UserName;
                                        write.CityID = id_city;
                                        dbConn.Update(write);
                                        total++;
                                    }
                                    //else if (checkCountry == null)
                                    //{
                                    //    eSheet.Cells[rownumber, 2].Value = DistrictName;
                                    //    eSheet.Cells[rownumber, 3].Value = regionName;
                                    //    eSheet.Cells[rownumber, 6].Value = active;
                                    //    eSheet.Cells[rownumber, 11].Value = "countryName not exist in system";
                                    //    rownumber++;
                                    //}
                                    else
                                    {
                                        string id = "";
                                        var checkID = dbConn.Select<CRM_Location_District>().OrderByDescending(m => m.RowCreatedTime).FirstOrDefault();
                                        if (checkID != null)
                                        {
                                            var nextNo = Int32.Parse(checkID.DistrictID.Substring(1, checkID.DistrictID.Length - 1)) + 1;
                                            id = "A" + String.Format("{0:0000}", nextNo);
                                        }
                                        else
                                        {
                                            id = "A0001";
                                        }

                                        write.DistrictID = id;
                                        write.DistrictName = DistrictName;
                                        write.Active = bool.Parse(active);
                                        write.RowCreatedTime = DateTime.Now;
                                        write.RowCreatedUser = currentUser.UserName;
                                        write.RowLastUpdatedTime = DateTime.Now;
                                        write.RowLastUpdatedUser = currentUser.UserName;
                                        write.CityID = id_city;
                                        dbConn.Insert(write);
                                        total++;
                                    }

                                }
                                catch (Exception e)
                                {
                                    eSheet.Cells[rownumber, 2].Value = DistrictName;
                                    eSheet.Cells[rownumber, 3].Value = cityName;
                                    eSheet.Cells[rownumber, 5].Value = active;
                                    eSheet.Cells[rownumber, 10].Value = e.Message;
                                    rownumber++;
                                    continue;
                                }
                            }
                            _excelPkg.Save();

                            return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
                        }
                        else
                        {
                            return Json(new { success = false, error = "File extension is not valid. *.xlsx please." });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, error = "File upload null" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
