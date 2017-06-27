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
    public class CRMAliasDefinitionController : CustomController
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
            var data = new List<khu_vuc>();
            if (request.Filters.Any())
            {
                var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                data = khu_vuc.GetCRM_Location_Alias(where, "ma_khu_vuc DESC");
            }
            else
            {
                data = khu_vuc.GetCRM_Location_Alias("1=1", "ma_khu_vuc DESC");
            }
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult Alias_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<khu_vuc> listEx)
        {
            if (asset.Create)
            {
                try
                {   
                    if (listEx != null)
                    {
                        foreach (var regis in listEx)
                        {
                            if (String.IsNullOrEmpty(regis.ten_khu_vuc))
                            {
                                ModelState.AddModelError("", " Vui lòng nhập tên khu vực");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            string id = "";
                            var write = new khu_vuc();
                            var checkID = khu_vuc.GetCRM_Location_Alias("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                            if (checkID != null)
                            {
                                var nextNo = Int32.Parse(checkID.ma_khu_vuc.Substring(1, checkID.ma_khu_vuc.Length - 1)) + 1;
                                id = "A" + String.Format("{0:0000}", nextNo);
                            }
                            else
                            {
                                id = "A0001";
                            }
                            var check = khu_vuc.GetCRM_Location_Alias("1=1", "").Where(s => s.ten_khu_vuc.Trim().ToLower() == regis.ten_khu_vuc.Trim().ToLower() && s.trang_thai == regis.trang_thai).FirstOrDefault();
                            if (check != null)
                            {
                                ModelState.AddModelError("", " Tên khu vực đã tồn tại.");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            write.ma_khu_vuc = id;
                            write.ten_khu_vuc = regis.ten_khu_vuc.Trim();
                            write.trang_thai = regis.trang_thai;
                            write.ngay_tao = DateTime.Now;
                            write.nguoi_tao = currentUser.UserName;
                            write.ngay_cap_nhat = DateTime.Parse("1900-01-01");
                            write.nguoi_cap_nhat ="";
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
        public ActionResult Alias_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<khu_vuc> listEx)
        {
            if (asset.Update)
            {
                try
                {
                    if (listEx != null)
                    {
                        foreach (var regis in listEx)
                        {
                            if (String.IsNullOrEmpty(regis.ten_khu_vuc))
                            {
                                ModelState.AddModelError("", " Vui lòng nhập tên khu vực ");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            var write = new khu_vuc();
                            var check = khu_vuc.GetCRM_Location_Alias("1=1", "").Where(s => s.ten_khu_vuc == regis.ten_khu_vuc && s.trang_thai == regis.trang_thai && s.trang_thai == regis.trang_thai);
                            if (check.Count() > 0)
                            {
                                ModelState.AddModelError("", " Tên khu vực đã tồn tại.");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            write.ma_khu_vuc = regis.ma_khu_vuc;
                            write.ten_khu_vuc = regis.ten_khu_vuc.Trim();
                            write.trang_thai = regis.trang_thai;
                            write.ngay_cap_nhat = DateTime.Now;
                            write.nguoi_cap_nhat = currentUser.UserName;
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
                        var checkexist = CRM_Location_Countries.GetCRM_Location_Countries("1=1", "").Where(s => s.AliasID == item);
                        if (checkexist.Count() > 0)
                        {
                            return Json(new { success = false, alert = "Alias exist in Country" });
                        }
                        var check = new khu_vuc();
                        check.ma_khu_vuc = item;
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
                var Alias = khu_vuc.GetCRM_Location_Alias("1=1", "ma_khu_vuc DESC").ToList();

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\CRM_Location_Alias.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);

                //data sheet
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["CRM_Location_Alias"];

                IEnumerable listData = Alias.ToDataSourceResult(request).Data;

                int rowData = 1;
                foreach (khu_vuc data in listData)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.ma_khu_vuc;
                    dataSheet.Cells[rowData, i++].Value = data.ten_khu_vuc;
                    dataSheet.Cells[rowData, i++].Value = data.trang_thai;
                    dataSheet.Cells[rowData, i++].Value = data.ngay_tao.ToString();
                    dataSheet.Cells[rowData, i++].Value = data.nguoi_tao;
                    if (data.ngay_cap_nhat.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.ngay_cap_nhat.ToString();
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = data.nguoi_cap_nhat;

                }

                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "CRM_Location_Alias_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                string fileName = "CRM_Location_Alias_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
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
                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\CRM_Location_Alias.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);
                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["CRM_Location_Alias"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["CRM_Location_Alias"];
                        int totalRows = oSheet.Dimension.End.Row;
                        for (int i = 2; i <= totalRows; i++)
                        {

                            // get data sheet Staff
                            string aliasID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            string aliasName = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            string active = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "TRUE";


                            try
                            {
                                var write = new khu_vuc();
                                var checkAliasNameExist = khu_vuc.GetCRM_Location_Alias("1=1", "").Where(s => s.ten_khu_vuc.Trim().ToLower() == aliasName.Trim().ToLower());
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

                                    write.ma_khu_vuc = aliasID;
                                    write.ten_khu_vuc = aliasName;
                                    write.trang_thai = Convert.ToBoolean(active);
                                    write.ngay_cap_nhat = DateTime.Now;
                                    write.nguoi_cap_nhat = currentUser.UserName;
                                    write.Update();
                                    total++;
                                }
                                else
                                {
                                    string id = "";
                                    var checkID = khu_vuc.GetCRM_Location_Alias("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                                    if (checkID != null)
                                    {
                                        var nextNo = Int32.Parse(checkID.ma_khu_vuc.Substring(1, checkID.ma_khu_vuc.Length - 1)) + 1;
                                        id = "A" + String.Format("{0:0000}", nextNo);
                                    }
                                    else
                                    {
                                        id = "A0001";
                                    }

                                    write.ma_khu_vuc = id;
                                    write.ten_khu_vuc = aliasName;
                                    write.trang_thai = !String.IsNullOrEmpty(active) ? bool.Parse(active) : true;
                                    write.ngay_tao = DateTime.Now;
                                    write.nguoi_tao = currentUser.UserName;
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
