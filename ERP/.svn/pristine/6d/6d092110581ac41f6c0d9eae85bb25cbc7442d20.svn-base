using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using System.IO;
using OfficeOpenXml;
using ERPAPD.Helpers;
using ServiceStack.OrmLite;

namespace ERPAPD.Controllers
{
    [Authorize]
    public class CallHistoryManagementController : CustomController
    {
        //
        // GET: /CallHistoryManagement/
        public ActionResult Index()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                if (asset.View)
                {
                    ViewData["ResultList"] = dbConn.Select<DC_Telesales_ResultList>();
                    ViewBag.listAgent = dbConn.Select<Users>("Groups LIKE '%Telesales%'");
                    return View();
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
            }
        }
        public ActionResult CallHistoryRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(KendoApplyFilter.KendoData<DC_Telesales_CallHistory>(request));
        }


        public ActionResult ExportData([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_CallHistory.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_CallHistory_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string query = "";
                    request.PageSize = 50000;
                    var data = KendoApplyFilter.KendoData<DC_Telesales_CallHistory>(request);
                    ExcelWorksheet expenseSheet = excelPkg.Workbook.Worksheets["CallHistory"];
                    var dataResult = dbConn.Select<DC_Telesales_ResultList>();
                    int rowData = 1;

                    foreach (DC_Telesales_CallHistory item in data.Data)
                    {
                        int i = 1;
                        rowData++;

                        expenseSheet.Cells[rowData, i++].Value = item.CustomerID;
                        expenseSheet.Cells[rowData, i++].Value = item.CustomerName;
                        expenseSheet.Cells[rowData, i++].Value = item.Phone;
                        expenseSheet.Cells[rowData, i++].Value = item.PhysicalID;
                        expenseSheet.Cells[rowData, i++].Value = item.CustRule;
                        expenseSheet.Cells[rowData, i++].Value = dataResult.FirstOrDefault(a => a.Id == item.ResultID) == null ? "" : dataResult.FirstOrDefault(a => a.Id == item.ResultID).Description;
                        expenseSheet.Cells[rowData, i++].Value = dataResult.FirstOrDefault(a => a.Id == item.SubResultID) == null ? "" : dataResult.FirstOrDefault(a => a.Id == item.SubResultID).Description;
                        expenseSheet.Cells[rowData, i++].Value = item.Content;
                        expenseSheet.Cells[rowData, i++].Value = item.RecallTime;
                        expenseSheet.Cells[rowData, i++].Value = item.isDone.ToString();
                        expenseSheet.Cells[rowData, i++].Value = item.Source;
                        expenseSheet.Cells[rowData, i++].Value = item.OrderID;
                        expenseSheet.Cells[rowData, i++].Value = item.RefType;
                        expenseSheet.Cells[rowData, i++].Value = item.RefID;
                        expenseSheet.Cells[rowData, i++].Value = item.CreatedAt;
                        expenseSheet.Cells[rowData, i++].Value = item.CreatedBy;
                        expenseSheet.Cells[rowData, i++].Value = item.UpdatedAt;
                        expenseSheet.Cells[rowData, i++].Value = item.UpdatedBy;
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