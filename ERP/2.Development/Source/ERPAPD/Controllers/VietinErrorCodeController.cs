using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;
using OfficeOpenXml;
using System.IO;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class VietinErrorCodeController : CustomController
    {
        //
        // GET: /VietinErrorCode/
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(Deca_Vietin_Error_Code));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(Deca_Vietin_Error_Code));
            //}
            if (asset.View)
            {
                ViewBag.Assets = asset;
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<Deca_Vietin_Error_Code>(request);
                }
                return Json(data);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_Vietin_Error_Code> items)
        {
            if (asset.Create)
            {
                if (items != null && ModelState.IsValid)
                {
                    try
                    {
                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {
                            foreach (var item in items)
                            {
                                item.CreatedAt = DateTime.Now;
                                item.CreatedBy = currentUser.UserName;
                                dbConn.Insert(item);
                                item.Id = (int)dbConn.GetLastInsertId();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("Error", e.Message);
                    }

                }
            }
            else
            {
                ModelState.AddModelError("", "Don't have permission");
            }
            return Json(items.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_Vietin_Error_Code> items)
        {
            if (asset.Update)
            {
                if (items != null && ModelState.IsValid)
                {
                    try
                    {
                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {
                            foreach (var item in items)
                            {
                                item.UpdatedAt = DateTime.Now;
                                item.UpdatedBy = currentUser.UserName;
                                dbConn.Update(item);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }

                }
            }
            else
            {
                ModelState.AddModelError("", "Don't have permission");
            }

            return Json(items.ToDataSourceResult(request, ModelState));
        }

        public ActionResult ExportData([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Vietin_ErrorCode.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_Vietin_ErrorCode" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    var data = new List<Deca_Vietin_Error_Code>();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Select<Deca_Vietin_Error_Code>(where).ToList();
                    }
                    else
                    {
                        data = dbConn.Select<Deca_Vietin_Error_Code>().ToList();
                    }


                    ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["VieitinErrorCode"];

                    int rowData = 1;

                    foreach (var item in data)
                    {
                        int i = 1;
                        rowData++;
                        dataSheet.Cells[rowData, i++].Value = item.Code;
                        dataSheet.Cells[rowData, i++].Value = item.Description;
                        dataSheet.Cells[rowData, i++].Value = item.CreatedAt;
                        dataSheet.Cells[rowData, i++].Value = item.CreatedBy;
                        dataSheet.Cells[rowData, i++].Value = item.UpdatedAt;
                        dataSheet.Cells[rowData, i++].Value = item.UpdatedBy;
                    }

                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    output.Position = 0;
                    return File(output.ToArray(), contentType, fileName);
                }
            }
            else
            {
                return Json(new { success = false });
            }

        }

        private DataTable WorksheetToDataTables(ExcelWorksheet oSheet)
        {
            int totalRows = oSheet.Dimension.End.Row;
            int totalCols = 2;
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

                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Vietin_ErrorCode.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);

                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["VieitinErrorCode"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["VieitinErrorCode"];

                        var data = WorksheetToDataTables(oSheet);
                        int temp;

                        var listData = data.AsEnumerable().Select(s => new Deca_Vietin_Error_Code
                        {
                            Code = s["Code"] != null ? s["Code"].ToString() : "",
                            Description = s["Description"] != null ? s["Description"].ToString() : ""
                        }).ToList();

                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {
                            if (n == 1)
                            {
                                var required = listData.Where(s => String.IsNullOrEmpty(s.Code) ||
                                        String.IsNullOrEmpty(s.Description));
                                foreach (var item in required)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, "Vui lòng nhập đầy đủ thông tin bắt buộc");
                                    rownumber++;
                                }
                                listData = listData.Except(required).ToList();
                                if (listData.Count() > 0)
                                {
                                    n = 1;
                                }
                                else
                                {
                                    n = 0;
                                }
                            }

                            foreach (var item in listData)
                            {
                                try
                                {
                                    var exist = dbConn.SingleOrDefault<Deca_Vietin_Error_Code>("Code={0}", item.Code);
                                    if (exist != null)
                                    {
                                        exist.Description = item.Description;
                                        exist.UpdatedAt = DateTime.Now;
                                        exist.UpdatedBy = currentUser.UserName;
                                        dbConn.Update(exist);
                                        total++;
                                    }
                                    else
                                    {
                                        var newData = new Deca_Vietin_Error_Code();
                                        newData.Code = item.Code;
                                        newData.Description = item.Description;
                                        newData.CreatedAt = DateTime.Now;
                                        newData.CreatedBy = currentUser.UserName;
                                        dbConn.Insert(newData);
                                        total++;
                                    }
                                }
                                catch (Exception e)
                                {
                                    writeErrorToFile(eSheet, rownumber, item, e.Message);
                                    rownumber++;
                                }

                            }

                        }
                        _excelPkg.Save();

                        return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = "errors:" + e.Message });
            }

            return Json(new { success = true });
        }

        public void writeErrorToFile(ExcelWorksheet eSheet, int rownumber, Deca_Vietin_Error_Code item, string error)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {

                int i = 1;
                eSheet.Cells[rownumber, i++].Value = item.Code;
                eSheet.Cells[rownumber, i++].Value = item.Description;
                eSheet.Cells[rownumber, i++].Value = item.CreatedAt;
                eSheet.Cells[rownumber, i++].Value = item.CreatedBy;
                eSheet.Cells[rownumber, i++].Value = item.UpdatedAt;
                eSheet.Cells[rownumber, i++].Value = item.UpdatedBy;
                eSheet.Cells[rownumber, i++].Value = error;
            }
        }

        [HttpGet]
        public virtual ActionResult Download(string file)
        {
            string fullPath = Path.Combine(Server.MapPath("~/Excel"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }

        public ActionResult ExportTemplate()
        {
            if (asset.Export)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Vietin_ErrorCode.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_Vietin_ErrorCode" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    output.Position = 0;
                    return File(output.ToArray(), contentType, fileName);
                }
            }
            else
            {
                return Json(new { success = false });
            }

        }
    }
}