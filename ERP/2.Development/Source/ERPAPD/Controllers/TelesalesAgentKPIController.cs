using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;
using OfficeOpenXml;
using System.IO;
using Kendo.Mvc;
using System.ComponentModel;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class TelesalesAgentKPIController : CustomController
    {
        //
        // GET: /TelesalesAgentKPI/
        public ActionResult Index()
        {
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.listAgent = dbConn.Select<Users>().Where(s => s.Groups != null && s.Groups.Select(k => k.Id).Contains(2));
                }

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
                    data = KendoApplyFilter.KendoData<Deca_Telesales_Agent_KPI>(request);
                }
                return Json(data);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_Telesales_Agent_KPI> items)
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_Telesales_Agent_KPI> items)
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
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Telesales_Agent_KPI.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_Telesales_Agent_KPI" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    var data = new List<Deca_Telesales_Agent_KPI>();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Select<Deca_Telesales_Agent_KPI>(where).ToList();
                    }
                    else
                    {
                        data = dbConn.Select<Deca_Telesales_Agent_KPI>().ToList();
                    }


                    ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["TelesalesAgentKPI"];

                    int rowData = 1;

                    foreach (var item in data)
                    {
                        int i = 1;
                        rowData++;
                        dataSheet.Cells[rowData, i++].Value = item.Agent;
                        dataSheet.Cells[rowData, i++].Value = item.Revenue;
                        dataSheet.Cells[rowData, i++].Value = item.Order;
                        dataSheet.Cells[rowData, i++].Value = item.Call;
                        dataSheet.Cells[rowData, i++].Value = item.TalkTime;
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
            int totalCols = 9;
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

                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Telesales_Agent_KPI.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);

                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["TelesalesAgentKPI"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["TelesalesAgentKPI"];

                        var data = WorksheetToDataTables(oSheet);
                        double temp;

                        var listData = data.AsEnumerable().Select(s => new Deca_Telesales_Agent_KPI
                        {
                            Agent = s["Agent"] != null ? s["Agent"].ToString() : "",
                            Month = s["Month"] != null ? s["Month"].ToString() : "",
                            Revenue = s["Revenue"] != null ? double.Parse(s["Revenue"].ToString()) : 0,
                            Order = s["Order"] != null ? double.Parse(s["Order"].ToString()) : 0,
                            Call = s["Call"] != null ? double.Parse(s["Call"].ToString()) : 0,
                            Customer = s["Customer"] != null ? double.Parse(s["Customer"].ToString()) : 0,
                            TalkTime = s["TalkTime"] != null ? double.Parse(s["TalkTime"].ToString()) : 0
                        }).ToList();

                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {
                            if (n == 1)
                            {
                                var required = listData.Where(s =>
                                        String.IsNullOrEmpty(s.Agent) ||
                                        String.IsNullOrEmpty(s.Month));
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
                                    var exist = dbConn.SingleOrDefault<Deca_Telesales_Agent_KPI>("Agent={0} AND Month={1}", item.Agent, item.Month);
                                    if (exist != null)
                                    {
                                        exist.Revenue = item.Revenue;
                                        exist.Order = item.Order;
                                        exist.Call = item.Call;
                                        exist.Customer = item.Customer;
                                        exist.TalkTime = item.TalkTime;
                                        exist.UpdatedAt = DateTime.Now;
                                        exist.UpdatedBy = currentUser.UserName;
                                        dbConn.Update(exist);
                                        total++;
                                    }
                                    else
                                    {
                                        var newData = new Deca_Telesales_Agent_KPI();
                                        newData.Agent = item.Agent;
                                        newData.Month = item.Month;
                                        newData.Revenue = item.Revenue;
                                        newData.Order = item.Order;
                                        newData.Call = item.Call;
                                        newData.Customer = item.Customer;
                                        newData.TalkTime = item.TalkTime;
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

        public void writeErrorToFile(ExcelWorksheet eSheet, int rownumber, Deca_Telesales_Agent_KPI item, string error)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {

                int i = 1;
                eSheet.Cells[rownumber, i++].Value = item.Agent;
                eSheet.Cells[rownumber, i++].Value = item.Month;
                eSheet.Cells[rownumber, i++].Value = item.Revenue;
                eSheet.Cells[rownumber, i++].Value = item.Order;
                eSheet.Cells[rownumber, i++].Value = item.Call;
                eSheet.Cells[rownumber, i++].Value = item.Customer;
                eSheet.Cells[rownumber, i++].Value = item.TalkTime;
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
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Telesales_Agent_KPI.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_Telesales_Agent_KPI" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["TelesalesAgentKPI"];

                    var listAgent = dbConn.Select<Users>().Where(s => s.Groups != null && s.Groups.Select(k => k.Id).Contains(2));
                    int rowData = 1;
                    foreach (var item in listAgent)
                    {
                        int i = 1;
                        rowData++;
                        dataSheet.Cells[rowData, i++].Value = item.UserName;
                        dataSheet.Cells[rowData, i++].Value = DateTime.Now.ToString("MM-yyyy");
                        dataSheet.Cells[rowData, i++].Value = 0;
                        dataSheet.Cells[rowData, i++].Value = 0;
                        dataSheet.Cells[rowData, i++].Value = 0;
                        dataSheet.Cells[rowData, i++].Value = 0;
                        dataSheet.Cells[rowData, i++].Value = 0;
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
    }
}