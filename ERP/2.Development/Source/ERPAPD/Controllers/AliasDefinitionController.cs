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
    public class AliasDefinitionController : CustomController
    {
        //
        // GET: /AliasDefinition/


        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Alias_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = new List<DC_Location_Alias>();
            if (request.Filters.Any())
            {
                var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                data = DC_Location_Alias.GetDC_Location_Alias(where, "AliasID DESC");
            }
            else
            {
                data = DC_Location_Alias.GetDC_Location_Alias("1=1", "AliasID DESC");
            }
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult Alias_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Location_Alias> listEx)
        {
            if (asset.Create)
            {
                try
                {
                    if (listEx != null && ModelState.IsValid)
                    {
                        foreach (var regis in listEx)
                        {
                            if (String.IsNullOrEmpty(regis.AliasName))
                            {
                                ModelState.AddModelError("", "Please input Alias Name ");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            string id = "";
                            var write = new DC_Location_Alias();
                            var checkID = DC_Location_Alias.GetDC_Location_Alias("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                            if (checkID != null)
                            {
                                var nextNo = Int32.Parse(checkID.AliasID.Substring(1, checkID.AliasID.Length - 1)) + 1;
                                id = "A" + String.Format("{0:0000}", nextNo);
                            }
                            else
                            {
                                id = "A0001";
                            }
                            var check = DC_Location_Alias.GetDC_Location_Alias("1=1", "").Where(s => s.AliasName.Trim().ToLower() == regis.AliasName.Trim().ToLower() && s.Active == regis.Active).FirstOrDefault();
                            if (check != null)
                            {
                                ModelState.AddModelError("", " Alias Name  is exists.");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            write.AliasID = id;
                            write.AliasName = regis.AliasName.Trim();
                            write.Active = regis.Active;
                            write.RowCreatedTime = DateTime.Now;
                            write.RowCreatedUser = currentUser.UserName;
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
        public ActionResult Alias_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Location_Alias> listEx)
        {
            if (asset.Update)
            {
                try
                {
                    if (listEx != null && ModelState.IsValid)
                    {
                        foreach (var regis in listEx)
                        {
                            if (String.IsNullOrEmpty(regis.AliasName))
                            {
                                ModelState.AddModelError("", "Please input Alias Name ");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            var write = new DC_Location_Alias();
                            var check = DC_Location_Alias.GetDC_Location_Alias("1=1", "").Where(s => s.AliasName == regis.AliasName && s.Active == regis.Active && s.Active == regis.Active);
                            if (check.Count() > 0)
                            {
                                ModelState.AddModelError("", " Alias Name is exists.");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            write.AliasID = regis.AliasID;
                            write.AliasName = regis.AliasName.Trim();
                            write.Active = regis.Active;
                            write.RowLastUpdatedTime = DateTime.Now;
                            write.RowLastUpdatedUser = currentUser.UserName;
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
        public ActionResult DeleteAlias(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        var checkexist = DC_Location_Countries.GetDC_Location_Countries("1=1", "").Where(s => s.AliasID == item);
                        if (checkexist.Count() > 0)
                        {
                            return Json(new { success = false, alert = "Alias exist in Country" });
                        }
                        var check = new DC_Location_Alias();
                        check.AliasID = item;
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
                var Alias = DC_Location_Alias.GetDC_Location_Alias("1=1", "AliasID DESC").ToList();

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Location_Alias.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);

                //data sheet
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["DC_Location_Alias"];

                IEnumerable listData = Alias.ToDataSourceResult(request).Data;

                int rowData = 1;
                foreach (DC_Location_Alias data in listData)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.AliasID;
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

                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_Location_Alias_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                string fileName = "DC_Location_Alias_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
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
                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Location_Alias.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);
                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["DC_Location_Alias"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["DC_Location_Alias"];
                        int totalRows = oSheet.Dimension.End.Row;
                        for (int i = 2; i <= totalRows; i++)
                        {

                            // get data sheet Staff
                            string aliasID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            string aliasName = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            string active = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "TRUE";


                            try
                            {
                                var write = new DC_Location_Alias();
                                var checkAliasNameExist = DC_Location_Alias.GetDC_Location_Alias("1=1", "").Where(s => s.AliasName.Trim().ToLower() == aliasName.Trim().ToLower());
                                if (string.IsNullOrEmpty(aliasName.ToString()))
                                {
                                    eSheet.Cells[rownumber, 2].Value = aliasName;
                                    eSheet.Cells[rownumber, 3].Value = active;
                                    eSheet.Cells[rownumber, 8].Value = "AliasName required";
                                    rownumber++;
                                }
                                if (checkAliasNameExist.Count() > 0)
                                {
                                    //eSheet.Cells[rownumber, 2].Value = aliasName;
                                    //eSheet.Cells[rownumber, 3].Value = active;
                                    //eSheet.Cells[rownumber, 8].Value = "AliasName exist in sytem";
                                    //rownumber++;

                                    write.AliasID = aliasID;
                                    write.AliasName = aliasName;
                                    write.Active = Convert.ToBoolean(active);
                                    write.RowLastUpdatedTime = DateTime.Now;
                                    write.RowLastUpdatedUser = currentUser.UserName;
                                    write.Update();
                                    total++;
                                }
                                else
                                {
                                    string id = "";
                                    var checkID = DC_Location_Alias.GetDC_Location_Alias("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                                    if (checkID != null)
                                    {
                                        var nextNo = Int32.Parse(checkID.AliasID.Substring(1, checkID.AliasID.Length - 1)) + 1;
                                        id = "A" + String.Format("{0:0000}", nextNo);
                                    }
                                    else
                                    {
                                        id = "A0001";
                                    }

                                    write.AliasID = id;
                                    write.AliasName = aliasName;
                                    write.Active = !String.IsNullOrEmpty(active) ? bool.Parse(active) : true;
                                    write.RowCreatedTime = DateTime.Now;
                                    write.RowCreatedUser = currentUser.UserName;
                                    write.Save();
                                    total++;
                                }
                            }
                            catch (Exception e)
                            {
                                eSheet.Cells[rownumber, 2].Value = aliasName;
                                eSheet.Cells[rownumber, 3].Value = active;
                                eSheet.Cells[rownumber, 8].Value = e.Message;
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
