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
    public class PlanKPIController : CustomController
    {
        //
        // GET: /PlanKPI/
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(Deca_Plan_KPI), typeof(Deca_Telesales_Agent_KPI));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(Deca_Plan_KPI), typeof(Deca_Telesales_Agent_KPI));
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
                    data = KendoApplyFilter.KendoData<Deca_Plan_KPI>(request);
                }
                return Json(data);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_Plan_KPI> items)
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
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_Plan_KPI> items)
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
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_PlanKPI.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_PlanKPI" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    var data = new List<Deca_Plan_KPI>();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Select<Deca_Plan_KPI>(where).ToList();
                    }
                    else
                    {
                        data = dbConn.Select<Deca_Plan_KPI>().ToList();
                    }


                    ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["PlanKPI"];

                    int rowData = 1;

                    foreach (var item in data)
                    {
                        int i = 1;
                        rowData++;
                        dataSheet.Cells[rowData, i++].Value = item.Name;
                        dataSheet.Cells[rowData, i++].Value = item.Year;
                        dataSheet.Cells[rowData, i++].Value = item.Month1;
                        dataSheet.Cells[rowData, i++].Value = item.Month2;
                        dataSheet.Cells[rowData, i++].Value = item.Month3;
                        dataSheet.Cells[rowData, i++].Value = item.Month4;
                        dataSheet.Cells[rowData, i++].Value = item.Month5;
                        dataSheet.Cells[rowData, i++].Value = item.Month6;
                        dataSheet.Cells[rowData, i++].Value = item.Month7;
                        dataSheet.Cells[rowData, i++].Value = item.Month8;
                        dataSheet.Cells[rowData, i++].Value = item.Month9;
                        dataSheet.Cells[rowData, i++].Value = item.Month10;
                        dataSheet.Cells[rowData, i++].Value = item.Month11;
                        dataSheet.Cells[rowData, i++].Value = item.Month12;
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
            int totalCols = 18;
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

                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_PlanKPI.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);

                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["PlanKPI"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["PlanKPI"];

                        var data = WorksheetToDataTables(oSheet);
                        double temp;

                        var listData = data.AsEnumerable().Select(s => new Deca_Plan_KPI
                        {
                            Name = s["Name"] != null ? s["Name"].ToString() : "",
                            Year = s["Year"] != null ? s["Year"].ToString() : "",
                            Month1 = s["Month1"] == null ? 0 : double.TryParse(s["Month1"].ToString(), out temp) ? double.Parse(s["Month1"].ToString()) : 0,
                            Month2 = s["Month2"] == null ? 0 : double.TryParse(s["Month2"].ToString(), out temp) ? double.Parse(s["Month2"].ToString()) : 0,
                            Month3 = s["Month3"] == null ? 0 : double.TryParse(s["Month3"].ToString(), out temp) ? double.Parse(s["Month3"].ToString()) : 0,
                            Month4 = s["Month4"] == null ? 0 : double.TryParse(s["Month4"].ToString(), out temp) ? double.Parse(s["Month4"].ToString()) : 0,
                            Month5 = s["Month5"] == null ? 0 : double.TryParse(s["Month5"].ToString(), out temp) ? double.Parse(s["Month5"].ToString()) : 0,
                            Month6 = s["Month6"] == null ? 0 : double.TryParse(s["Month6"].ToString(), out temp) ? double.Parse(s["Month6"].ToString()) : 0,
                            Month7 = s["Month7"] == null ? 0 : double.TryParse(s["Month7"].ToString(), out temp) ? double.Parse(s["Month7"].ToString()) : 0,
                            Month8 = s["Month8"] == null ? 0 : double.TryParse(s["Month8"].ToString(), out temp) ? double.Parse(s["Month8"].ToString()) : 0,
                            Month9 = s["Month9"] == null ? 0 : double.TryParse(s["Month9"].ToString(), out temp) ? double.Parse(s["Month9"].ToString()) : 0,
                            Month10 = s["Month10"] == null ? 0 : double.TryParse(s["Month10"].ToString(), out temp) ? double.Parse(s["Month10"].ToString()) : 0,
                            Month11 = s["Month11"] == null ? 0 : double.TryParse(s["Month11"].ToString(), out temp) ? double.Parse(s["Month11"].ToString()) : 0,
                            Month12 = s["Month12"] == null ? 0 : double.TryParse(s["Month12"].ToString(), out temp) ? double.Parse(s["Month12"].ToString()) : 0
                        }).ToList();

                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {
                            if (n == 1)
                            {
                                var required = listData.Where(s =>
                                        String.IsNullOrEmpty(s.Name) ||
                                        String.IsNullOrEmpty(s.Year));
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
                                    var exist = dbConn.SingleOrDefault<Deca_Plan_KPI>("Name={0} AND Year={1}", item.Name, item.Year);
                                    if (exist != null)
                                    {
                                        exist.Month1 = item.Month1;
                                        exist.Month2 = item.Month2;
                                        exist.Month3 = item.Month3;
                                        exist.Month4 = item.Month4;
                                        exist.Month5 = item.Month5;
                                        exist.Month6 = item.Month6;
                                        exist.Month7 = item.Month7;
                                        exist.Month8 = item.Month8;
                                        exist.Month9 = item.Month9;
                                        exist.Month10 = item.Month10;
                                        exist.Month11 = item.Month11;
                                        exist.Month12 = item.Month12;
                                        exist.UpdatedAt = DateTime.Now;
                                        exist.UpdatedBy = currentUser.UserName;
                                        dbConn.Update(exist);
                                        total++;
                                    }
                                    else
                                    {
                                        var newData = new Deca_Plan_KPI();
                                        newData.Name = item.Name;
                                        newData.Year = item.Year;
                                        newData.Month1 = item.Month1;
                                        newData.Month2 = item.Month2;
                                        newData.Month3 = item.Month3;
                                        newData.Month4 = item.Month4;
                                        newData.Month5 = item.Month5;
                                        newData.Month6 = item.Month6;
                                        newData.Month7 = item.Month7;
                                        newData.Month8 = item.Month8;
                                        newData.Month9 = item.Month9;
                                        newData.Month10 = item.Month10;
                                        newData.Month11 = item.Month11;
                                        newData.Month12 = item.Month12;
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

        public void writeErrorToFile(ExcelWorksheet eSheet, int rownumber, Deca_Plan_KPI item, string error)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {

                int i = 1;
                eSheet.Cells[rownumber, i++].Value = item.Name;
                eSheet.Cells[rownumber, i++].Value = item.Year;
                eSheet.Cells[rownumber, i++].Value = item.Month1;
                eSheet.Cells[rownumber, i++].Value = item.Month2;
                eSheet.Cells[rownumber, i++].Value = item.Month3;
                eSheet.Cells[rownumber, i++].Value = item.Month4;
                eSheet.Cells[rownumber, i++].Value = item.Month5;
                eSheet.Cells[rownumber, i++].Value = item.Month6;
                eSheet.Cells[rownumber, i++].Value = item.Month7;
                eSheet.Cells[rownumber, i++].Value = item.Month8;
                eSheet.Cells[rownumber, i++].Value = item.Month9;
                eSheet.Cells[rownumber, i++].Value = item.Month10;
                eSheet.Cells[rownumber, i++].Value = item.Month11;
                eSheet.Cells[rownumber, i++].Value = item.Month12;
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
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_PlanKPI.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_PlanKPI" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
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