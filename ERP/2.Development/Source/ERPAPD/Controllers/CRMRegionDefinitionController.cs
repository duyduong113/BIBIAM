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

namespace ERPAPD.Controllers
{
    public class CRMRegionDefinitionController : CustomController
    {
        //
        // GET: /RegionDefinition/


        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                ViewBag.listcountry = CRM_Location_Countries.GetCRM_Location_Countries("1=1", "CountryName ASC");
                ViewBag.listalias = khu_vuc.GetCRM_Location_Alias("1=1", "AliasName ASC");
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Region_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = new List<CRM_Location_Region>();
            if (request.Filters.Any())
            {
                var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                data = CRM_Location_Region.GetCRM_Location_Regions(where, "RegionID DESC");
            }
            else
            {
                data = CRM_Location_Region.GetCRM_Location_Regions("1=1", "RegionID DESC");
            }
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult Region_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Location_Region> listEx)
        {
            if (asset.Create)
            {
                try
                {
                    if (listEx != null )
                    {
                        foreach (var regis in listEx)
                        {
                            if (String.IsNullOrEmpty(regis.RegionName))
                            {
                                ModelState.AddModelError("", "Please input Region Name ");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            //if (String.IsNullOrEmpty(regis.CountryID))
                            //{
                            //    ModelState.AddModelError("", "Please input Country Name");
                            //    return Json(listEx.ToDataSourceResult(request, ModelState));
                            //}
                            string id = "";
                            var write = new CRM_Location_Region();
                            var checkID = CRM_Location_Region.GetCRM_Location_Regions("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                            if (checkID != null)
                            {
                                var nextNo = Int32.Parse(checkID.RegionID.Substring(1, checkID.RegionID.Length - 1)) + 1;
                                id = "R" + String.Format("{0:0000}", nextNo);
                            }
                            else
                            {
                                id = "R0001";
                            }
                            var check = CRM_Location_Region.GetCRM_Location_Regions("1=1", "").Where(s => s.RegionName.Trim().ToLower() == regis.RegionName.Trim().ToLower() && s.CountryID == regis.CountryID && s.Active == regis.Active).FirstOrDefault();
                            if (check != null)
                            {
                                ModelState.AddModelError("", " Region Name  is exists.");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            write.RegionID = id;
                            write.RegionName = regis.RegionName.Trim();
                            write.Active = regis.Active;
                            write.RowCreatedTime = DateTime.Now;
                            write.RowCreatedUser = currentUser.UserName;
                            write.CountryID = regis.CountryID != null ? regis.CountryID : "";
                            write.Save();
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
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listEx.ToDataSourceResult(request, ModelState));
            }
            return Json(listEx.ToDataSourceResult(request));
        }
        public ActionResult Region_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Location_Region> listEx)
        {
            if (asset.Update)
            {
                try
                {
                    if (listEx != null)
                    {
                        foreach (var regis in listEx)
                        {
                            if (String.IsNullOrEmpty(regis.RegionName))
                            {
                                ModelState.AddModelError("", "Please input Region Name ");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            //if (String.IsNullOrEmpty(regis.CountryID))
                            //{
                            //    ModelState.AddModelError("", "Please input Country Name");
                            //    return Json(listEx.ToDataSourceResult(request, ModelState));
                            //}
                            var write = new CRM_Location_Region();
                            var check = CRM_Location_Region.GetCRM_Location_Regions("1=1", "").Where(s => s.RegionName.Trim().ToLower() == regis.RegionName.Trim().ToLower() && s.CountryID == regis.CountryID && s.Active == regis.Active);
                            if (check.Count() > 0)
                            {
                                ModelState.AddModelError("", "Region Name is exists.");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            write.RegionID = regis.RegionID;
                            write.RegionName = regis.RegionName.Trim();
                            write.Active = regis.Active;
                            write.RowLastUpdatedTime = DateTime.Now;
                            write.RowLastUpdatedUser = currentUser.UserName;
                            write.CountryID = regis.CountryID != null ? regis.CountryID : "";
                            write.Update();
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
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
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
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        var checkexist = CRM_Location_City.GetCRM_Location_Citys("1=1", "").Where(s => s.RegionID == item);
                        if (checkexist.Count() > 0)
                        {
                            return Json(new { success = false, alert = "Region exist in City" });
                        }
                        var check = new CRM_Location_Region();
                        check.RegionID = item;
                        check.Delete();
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
                //var region = CRM_Location_Region.GetAllCRM_Location_Regions().OrderByDescending(s => s.RegionID).ToList();
                var region = CRM_Location_Region.GetCRM_Location_Regions("1=1", "RegionID DESC");
                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\CRM_Location_Region.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["CRM_Location_Region"];
                IEnumerable listData = region.ToDataSourceResult(request).Data;
                int rowData = 1;
                foreach (CRM_Location_Region data in listData)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.RegionID;
                    dataSheet.Cells[rowData, i++].Value = data.RegionName;
                    dataSheet.Cells[rowData, i++].Value = (!String.IsNullOrEmpty(data.CountryID) ? data.CountryID : "") + " - " + (!String.IsNullOrEmpty(data.CountryName) ? data.CountryName : "");
                    dataSheet.Cells[rowData, i++].Value = data.AliasName;
                    dataSheet.Cells[rowData, i++].Value = data.Active;
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedTime.ToString();
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedUser;
                    if (data.RowLastUpdatedTime.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedTime.ToString();
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedUser;

                }
                var listCountry = DC_Location_Countries.GetAllDC_Location_Countries().OrderByDescending(s => s.AliasID).ToList();
                ExcelWorksheet dataSheetAliasID = excelPkg.Workbook.Worksheets["List"];
                IEnumerable listDataAliasID = listCountry.ToDataSourceResult(request).Data;

                int rowDataAliasID = 1;
                foreach (DC_Location_Countries data in listDataAliasID)
                {
                    int i = 1;
                    rowDataAliasID++;
                    dataSheetAliasID.Cells[rowDataAliasID, i++].Value = (!String.IsNullOrEmpty(data.CountryID) ? data.CountryID : "") + " - " + (!String.IsNullOrEmpty(data.CountryID) ? data.CountryName : "");
                }

                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "CRM_Location_Region_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                string fileName = "CRM_Location_Region_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentType, fileName);
            }
        }
        public ActionResult ImportFromExcel()
        {
            try
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

                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\CRM_Location_Region.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);

                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["CRM_Location_Region"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["CRM_Location_Region"];

                        //remove row
                        int totalRows = oSheet.Dimension.End.Row;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            string regionID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            string regionName = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            string countryName = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                            string active = oSheet.Cells[i, 5].Value != null ? oSheet.Cells[i, 5].Value.ToString() : "TRUE";
                            string id_country = !String.IsNullOrEmpty(countryName) ? countryName.Substring(0, countryName.LastIndexOf("-")).Trim() : "";

                            try
                            {
                                var write = new CRM_Location_Region();
                                var checkRegion = CRM_Location_Region.GetCRM_Location_Regions("[RegionName] = N'" + regionName + "' AND [CountryID] = '" + id_country + "'", "").FirstOrDefault();
                                var checkCountry = DC_Location_Countries.GetDC_Location_Countries("[CountryID] = '" + id_country + "'", "").FirstOrDefault();
                                if (string.IsNullOrEmpty(regionName.ToString()) || string.IsNullOrEmpty(countryName.ToString()))
                                {
                                    eSheet.Cells[rownumber, 2].Value = regionName;
                                    eSheet.Cells[rownumber, 3].Value = countryName;
                                    eSheet.Cells[rownumber, 5].Value = active;
                                    eSheet.Cells[rownumber, 10].Value = "regionName, countryName required";
                                    rownumber++;
                                }
                                else if (checkRegion != null)
                                {
                                    write.RegionID = regionID;
                                    write.RegionName = regionName;
                                    write.Active = Convert.ToBoolean(active);
                                    write.RowLastUpdatedTime = DateTime.Now;
                                    write.RowLastUpdatedUser = currentUser.UserName;
                                    write.CountryID = id_country;
                                    write.Update();
                                    total++;
                                }
                                else if (checkCountry == null)
                                {
                                    eSheet.Cells[rownumber, 2].Value = regionName;
                                    eSheet.Cells[rownumber, 3].Value = countryName;
                                    eSheet.Cells[rownumber, 5].Value = active;
                                    eSheet.Cells[rownumber, 10].Value = "countryName not exist in system";
                                    rownumber++;
                                }
                                else
                                {
                                    string id = "";
                                    var checkID = CRM_Location_Region.GetCRM_Location_Regions("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                                    if (checkID != null)
                                    {
                                        var nextNo = Int32.Parse(checkID.RegionID.Substring(1, checkID.RegionID.Length - 1)) + 1;
                                        id = "A" + String.Format("{0:0000}", nextNo);
                                    }
                                    else
                                    {
                                        id = "A0001";
                                    }

                                    write.RegionID = id;
                                    write.RegionName = regionName;
                                    write.Active = bool.Parse(active);
                                    write.RowCreatedTime = DateTime.Now;
                                    write.RowCreatedUser = currentUser.UserName;
                                    write.CountryID = id_country;
                                    write.Save();
                                    total++;
                                }
                            }
                            catch (Exception e)
                            {
                                eSheet.Cells[rownumber, 2].Value = regionName;
                                eSheet.Cells[rownumber, 3].Value = countryName;
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
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
