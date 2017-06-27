using ERPAPD.Helpers;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using OfficeOpenXml;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ERPAPD.Controllers
{
    public class CustomerAccountantController : CustomController
    {
        // GET: CustomerAccountant
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
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {

            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT   cs.*       
                                        FROM [CRM_CustomerAccountant] cs ";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_CustomerAccountant>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_CustomerAccountant> listItem)
        {
            if (asset.Create)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listItem != null)
                        {
                            foreach (var item in listItem)
                            {

                                if (String.IsNullOrEmpty(item.CustomerName))
                                {
                                    ModelState.AddModelError("", "Vui lòng nhập tên khách hàng");
                                    return Json(listItem.ToDataSourceResult(request, ModelState));
                                }
                                item.RowCreatedAt = DateTime.Now;
                                item.RowCreatedUser = currentUser.UserName;

                                dbConn.Insert(item);
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
                        return Json(listItem.ToDataSourceResult(request, ModelState));
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listItem.ToDataSourceResult(request, ModelState));
            }
            return Json(listItem.ToDataSourceResult(request));
        }
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_CustomerAccountant> listItem)
        {
            if (asset.Update)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listItem != null)
                        {
                            foreach (var item in listItem)
                            {

                                if (String.IsNullOrEmpty(item.CustomerName))
                                {
                                    ModelState.AddModelError("", "Vui lòng nhập Mã");
                                    return Json(listItem.ToDataSourceResult(request, ModelState));
                                }

                                item.RowUpdatedAt = DateTime.Now;
                                item.RowUpdatedUser = currentUser.UserName;
                                dbConn.Update(item);
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
                        return Json(listItem.ToDataSourceResult(request, ModelState));
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listItem.ToDataSourceResult(request, ModelState));
            }
            return Json(listItem.ToDataSourceResult(request));
        }
        public ActionResult Delete(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                    {
                        string[] separators = { "@@" };
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listRowID)
                        {
                            var check = dbConn.FirstOrDefault<CRM_CustomerAccountant>("Id={0}", item);
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
        public ActionResult ExportTemlate([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                using (var dbConn = OrmliteConnection.openConn())
                {
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\KHKT.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "KHKT_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    ExcelWorksheet expenseSheet = excelPkg.Workbook.Worksheets["Data"];
                  
                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    output.Position = 0;
                    return File(output.ToArray(), contentType, fileName);
                }

            }
            else
            {
                return Json(new { success = false, message = "Không có quyền cập nhật" });
            }
        }
        [HttpPost]
        public ActionResult ImportFromExcel()
        {
            try
            {
                if (Request.Files["FileUpload"] != null && Request.Files["fileUpload"].ContentLength > 0)
                {
                    string fileExtension =
                            System.IO.Path.GetExtension(Request.Files["fileUpload"].FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["fileUpload"].FileName);
                        string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["fileUpload"].FileName);

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);

                        Request.Files["fileUpload"].SaveAs(fileLocation);
                        //Request.Files["fileUpload"].SaveAs(errorFileLocation);

                        var rownumber = 2;
                        var total = 0;
                        int n = 1;

                        FileInfo fileInfo = new FileInfo(fileLocation);
                        var excelPkg = new ExcelPackage(fileInfo);

                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\KHKT.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);

                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["Data"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["Data"];

                        var data = WorksheetToDataTables(oSheet);

                        DateTime temp = DateTime.Parse("1900-01-01");

                        var listData = data.AsEnumerable().Select(s => new CRM_CustomerAccountant
                        {
                            CustomerName = s["Tên KH"] != null ? s["Tên KH"].ToString() : "",
                            Presenter = s["Đại diện"] != null ? s["Đại diện"].ToString() : "",
                            Position = s["Chức vụ"] != null ? s["Chức vụ"].ToString() : "",
                            Address = s["Địa chỉ"] != null ? s["Địa chỉ"].ToString() : "",
                            Phone = s["Điện thoại"] != null ? s["Điện thoại"].ToString() : "",
                            Fax = s["Fax"] != null ? s["Fax"].ToString() : "",
                            TaxCode = s["Mã số thuế"] != null ? s["Mã số thuế"].ToString() : "",
                            BankAccount = s["Tài khoản"] != null ? s["Tài khoản"].ToString() : "",
                            Bank = s["Ngân hàng"] != null ? s["Ngân hàng"].ToString() : "",
                            Branch = s["Chi nhánh"] != null ? s["Chi nhánh"].ToString() : "",
                            RowCreatedAt = DateTime.Now,
                            RowCreatedUser = currentUser.UserName
                        }).Where( s => s.CustomerName != "").ToList();

                        using (var dbConn = OrmliteConnection.openConn())
                        {

                            // begin save data
                            try
                            {
                                dbConn.InsertAll<CRM_CustomerAccountant>(listData);
                            }
                            catch (Exception ex)
                            {
                                writeErrorToFile(eSheet, rownumber, listData.FirstOrDefault(), ex.Message);
                                rownumber++;
                            }

                        }
                        _excelPkg.Save();

                        return Json(new { success = true, total = listData.Count, totalError = rownumber - 2, link = errorFileLocation });
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = "errors:" + e.Message });
            }

            return Json(new { success = true });
        }
        private DataTable WorksheetToDataTables(ExcelWorksheet oSheet)
        {
            int totalRows = oSheet.Dimension.End.Row;
            int totalCols = oSheet.Dimension.End.Column; ;
            DataTable dt = new DataTable(oSheet.Name);
            DataRow dr = null;
            for (int i = 1; i <= totalRows; i++)
            {
                if (i > 1) dr = dt.Rows.Add();
                for (int j = 1; j <= totalCols; j++)
                {
                    if (i == 1)
                        dt.Columns.Add(oSheet.Cells[i, j].Value.ToString());
                    else
                        dr[j - 1] = oSheet.Cells[i, j].Value;
                }
            }
            return dt;
        }
        public void writeErrorToFile(ExcelWorksheet eSheet, int rownumber, CRM_CustomerAccountant item, string error)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
              
                int i = 1;
                eSheet.Cells[rownumber, i++].Value = item.CustomerName;
                eSheet.Cells[rownumber, i++].Value = error;
            }
        }
        public virtual ActionResult Download(string file)
        {
            string fullPath = Path.Combine(Server.MapPath("~/Excel"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }
    }
}