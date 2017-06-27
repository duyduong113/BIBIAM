using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using System.Collections;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web;
using ERPAPD.Helpers;
using System.Data.OleDb;
using System.Data;
using System.Dynamic;
using OfficeOpenXml;

namespace ERPAPD.Controllers
{
    public class CountryDefinitionController : CustomController
    {
        //
        // GET: /CountryDefinition/

        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                ViewBag.listalias = DC_Location_Alias.GetDC_Location_Alias("1=1", "AliasName ASC");
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Country_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = new List<DC_Location_Countries>();
            if (request.Filters.Any())
            {
                var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                data = DC_Location_Countries.GetDC_Location_Countries(where, "CountryID DESC");
            }
            else
            {
                data = DC_Location_Countries.GetDC_Location_Countries("1=1", "CountryID DESC");
            }
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult Country_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Location_Countries> listEx)
        {
            if (asset.Create)
            {
                try
                {
                    if (listEx != null && ModelState.IsValid)
                    {
                        foreach (var regis in listEx)
                        {
                            if (String.IsNullOrEmpty(regis.CountryName))
                            {
                                ModelState.AddModelError("", "Please input Country Name ");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            string id = "";
                            var write = new DC_Location_Countries();
                            var checkID = DC_Location_Countries.GetDC_Location_Countries("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                            if (checkID != null)
                            {
                                var nextNo = Int32.Parse(checkID.CountryID.Substring(1, checkID.CountryID.Length - 1)) + 1;
                                id = "C" + String.Format("{0:0000}", nextNo);
                            }
                            else
                            {
                                id = "C0001";
                            }
                            var check = DC_Location_Countries.GetDC_Location_Countries("1=1", "").Where(s => s.CountryName.Trim().ToLower() == regis.CountryName.Trim().ToLower() && s.AliasID == regis.AliasID && s.Active == regis.Active).FirstOrDefault();
                            if (check != null)
                            {
                                ModelState.AddModelError("", " Country Name  is exists.");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            write.CountryID = id;
                            write.CountryName = regis.CountryName.Trim();
                            write.Active = regis.Active;
                            write.RowCreatedTime = DateTime.Now;
                            write.RowCreatedUser = currentUser.UserName;
                            write.AliasID = regis.AliasID != null ? regis.AliasID : "";
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
            return Json(listEx.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Country_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Location_Countries> listEx)
        {
            if (asset.Update)
            {
                try
                {
                    if (listEx != null && ModelState.IsValid)
                    {
                        foreach (var regis in listEx)
                        {
                            if (String.IsNullOrEmpty(regis.CountryName))
                            {
                                ModelState.AddModelError("", "Please input Country Name ");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            var write = new DC_Location_Countries();
                            var check = DC_Location_Countries.GetDC_Location_Countries("1=1", "").Where(s => s.CountryName.Trim().ToLower() == regis.CountryName.Trim().ToLower() && s.AliasID == regis.AliasID && s.Active == regis.Active);
                            if (check.Count() > 0)
                            {
                                ModelState.AddModelError("", "Country Name is exists.");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            write.CountryID = regis.CountryID;
                            write.CountryName = regis.CountryName.Trim();
                            write.Active = regis.Active;
                            write.RowLastUpdatedTime = DateTime.Now;
                            write.RowLastUpdatedUser = currentUser.UserName;
                            write.AliasID = regis.AliasID != null ? regis.AliasID : "";
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
            return Json(listEx.ToDataSourceResult(request, ModelState));
        }
        public ActionResult DeleteCountry(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        var checkexist = DC_Location_Region.GetDC_Location_Regions("1=1", "").Where(s => s.CountryID == item);
                        if (checkexist.Count() > 0)
                        {
                            return Json(new { success = false, alert = "Country exist in Region" });
                        }
                        var check = new DC_Location_Countries();
                        check.CountryID = item;
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
                var country = DC_Location_Countries.GetAllDC_Location_Countries().OrderByDescending(s => s.CountryID).ToList();

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Location_Country.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);

                //data sheet
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["DC_Location_Country"];

                IEnumerable listData = country.ToDataSourceResult(request).Data;

                int rowData = 1;
                foreach (DC_Location_Countries data in listData)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.CountryID;
                    dataSheet.Cells[rowData, i++].Value = data.CountryName;
                    dataSheet.Cells[rowData, i++].Value = (!String.IsNullOrEmpty(data.AliasID) ? data.AliasID : "") + " - " + (!String.IsNullOrEmpty(data.AliasName) ? data.AliasName : "");
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

                //data sheet
                var alias = DC_Location_Alias.GetAllDC_Location_Alias().OrderByDescending(s => s.AliasID).ToList();
                ExcelWorksheet dataSheetAliasID = excelPkg.Workbook.Worksheets["List"];
                IEnumerable listDataAliasID = alias.ToDataSourceResult(request).Data;

                int rowDataAliasID = 1;
                foreach (DC_Location_Alias data in listDataAliasID)
                {
                    int i = 1;
                    rowDataAliasID++;
                    dataSheetAliasID.Cells[rowDataAliasID, i++].Value = (!String.IsNullOrEmpty(data.AliasID) ? data.AliasID : "") + " - " + (!String.IsNullOrEmpty(data.AliasName) ? data.AliasName : "");


                }

                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_Location_Country_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                string fileName = "DC_Location_Country_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
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

                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Location_Country.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);

                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["DC_Location_Country"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["DC_Location_Country"];

                        //remove row
                        int totalRows = oSheet.Dimension.End.Row;
                        for (int i = 2; i <= totalRows; i++)
                        {

                            // get data sheet Staff
                            string countryID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            string countryName = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            string alias = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                            string active = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "TRUE";
                            string id_Alias = !String.IsNullOrEmpty(alias) ? alias.Substring(0, alias.LastIndexOf("-")).Trim() : "";

                            try
                            {
                                var write = new DC_Location_Countries();
                                var checkCountryName = DC_Location_Countries.GetDC_Location_Countries("[CountryName] = N'" + countryName + "' AND [AliasID] = '" + id_Alias + "'", "").FirstOrDefault();
                                var checkAlias = DC_Location_Alias.GetDC_Location_Alias("[AliasID] = '" + id_Alias + "'", "").FirstOrDefault();
                                if (string.IsNullOrEmpty(countryName.ToString()) || string.IsNullOrEmpty(alias.ToString()))
                                {
                                    eSheet.Cells[rownumber, 1].Value = countryName;
                                    eSheet.Cells[rownumber, 2].Value = alias;
                                    eSheet.Cells[rownumber, 3].Value = active;
                                    eSheet.Cells[rownumber, 8].Value = "countryName, Alias required";
                                    rownumber++;
                                }
                                else if (checkCountryName != null)
                                {
                                    //eSheet.Cells[rownumber, 1].Value = countryName;
                                    //eSheet.Cells[rownumber, 2].Value = alias;
                                    //eSheet.Cells[rownumber, 3].Value = active;
                                    //eSheet.Cells[rownumber, 8].Value = "countryName & Alias exist in system";
                                    //rownumber++;
                                    write.CountryID = countryID;
                                    write.CountryName = countryName.Trim();
                                    write.Active = Convert.ToBoolean(active);
                                    write.RowLastUpdatedTime = DateTime.Now;
                                    write.RowLastUpdatedUser = currentUser.UserName;
                                    write.AliasID = id_Alias;
                                    write.Update();
                                }
                                else if (checkAlias == null)
                                {
                                    eSheet.Cells[rownumber, 2].Value = countryName;
                                    eSheet.Cells[rownumber, 3].Value = alias;
                                    eSheet.Cells[rownumber, 4].Value = active;
                                    eSheet.Cells[rownumber, 9].Value = "Alias not exist in system";
                                    rownumber++;
                                }
                                else
                                {
                                    string id = "";

                                    var checkID = DC_Location_Countries.GetDC_Location_Countries("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                                    if (checkID != null)
                                    {
                                        var nextNo = Int32.Parse(checkID.CountryID.Substring(1, checkID.CountryID.Length - 1)) + 1;
                                        id = "C" + String.Format("{0:0000}", nextNo);
                                    }
                                    else
                                    {
                                        id = "C0001";
                                    }

                                    write.CountryID = id;
                                    write.CountryName = countryName.Trim();
                                    write.Active = bool.Parse(active);
                                    write.RowCreatedTime = DateTime.Now;
                                    write.RowCreatedUser = currentUser.UserName;
                                    write.AliasID = id_Alias;
                                    write.Save();
                                }
                                total++;
                            }

                            catch (Exception e)
                            {
                                eSheet.Cells[rownumber, 2].Value = countryName;
                                eSheet.Cells[rownumber, 3].Value = alias;
                                eSheet.Cells[rownumber, 4].Value = active;
                                eSheet.Cells[rownumber, 9].Value = e.Message;
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
