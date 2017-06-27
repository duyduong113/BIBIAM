using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using System.Collections;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Globalization;
using System.Web;
using System.Data.OleDb;
using System.Dynamic;
using OfficeOpenXml;
using ERPAPD.Helpers;
using System.Threading.Tasks;
using DevTrends.MvcDonutCaching;
using System.Xml.Linq;
using Hangfire;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System.Configuration;

namespace ERPAPD.Controllers
{
    [Authorize]
    public class AvoidCallingTimeCompanyController : CustomController
    {

        public ActionResult Index()
        {
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;
                    ViewData["Asset"] = asset;

                    ViewBag.listCom = dbConn.Select<Deca_Company>();
                   
                        //OrmLiteConfig.DialectProvider.UseUnicode = true;
                        //dbConn.DropTables(typeof(DC_Telesales_AvoidCallingTimeCompany));
                        //const bool overwrite = false;
                        //dbConn.CreateTables(overwrite, typeof(DC_Telesales_AvoidCallingTimeCompany));
                    //ViewBag.CountCompany = DC_Telesales_AvoidCallingTimeCompany.CountCompany();
                    //ViewBag.CountCustomerInCom = DC_Telesales_AvoidCallingTimeCompany.CountCustomerInCom();
                    ViewBag.listComDisticnt = dbConn.Select<Deca_Company>();

                    
                        ViewBag.listAvoidCallingTimeFrame = dbConn.Select<DC_AvoidCallingTimeFrame>("SELECT Id,HeaderName FROM DC_AvoidCallingTimeFrame");
                        ViewBag.listAvoidCallingTimeFramedistinct = dbConn.Select<DC_AvoidCallingTimeFrame>("select distinct HeaderName,Id from DC_AvoidCallingTimeFrame");

                    return View();
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult Read_AvoidCallingTimeFrame([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = DC_DetailAvoidCallingTimeFrame.GetAll();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
      


        [HttpPost]
        public ActionResult Update_AvoidCallingTimeFrame([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<DC_DetailAvoidCallingTimeFrame> listResult)
        {
            if (asset.Update)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listResult != null && ModelState.IsValid)
                        {
                            foreach (var item in listResult)
                            {
                                var data = dbConn.Select<DC_Telesales_AvoidCallingTimeCompany>("select * from DC_Telesales_AvoidCallingTimeCompany WHERE Id={0}", item.Id).FirstOrDefault();
                                data.Monday = item.Monday;
                                data.Tuesday = item.Tuesday;
                                data.Wednesday = item.Wednesday;
                                data.Thursday = item.Thursday;
                                data.Friday = item.Friday;
                                data.Saturday = item.Saturday;
                                data.Sunday = item.Sunday;
                                data.RowLastUpdatedTime = DateTime.Now;
                                data.RowLastUpdatedUser = currentUser.UserName;
                                dbConn.Update(data);
                            }
                            return Json(new { success = true });
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
                    }
                    catch (Exception e)
                    {
                        return Json(new { success = false, error = e.Message });
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record ");
                return Json(listResult.ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult GetHeaderName(string HeaderID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<DC_DetailAvoidCallingTimeFrame>("select Id,LEFT(CAST(FromHour AS TIME),5) + ' - ' + LEFT(CAST(ToHour AS TIME),5) AS Item from DC_DetailAvoidCallingTimeFrame WHERE HeaderID = {0}", HeaderID);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Save_Com_AVoidCallingTime(string AvoidCallTime, string TimeFrameDetail, string listCom,
            bool Monday, bool Tueday, bool Wednesday, bool Thusday, bool Friday, bool Saturday, bool Sunday)
        {
            if (asset.Create)
            {
                TimeFrameDetail = string.IsNullOrEmpty(TimeFrameDetail) ? "All" : TimeFrameDetail;
                if (!string.IsNullOrEmpty(AvoidCallTime))
                {
                    try
                    {
                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {
                            string[] separators = { ";;" };
                            var listdata = listCom.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                            if (listdata.Count() == 0)
                            {
                                return Json(new { success = false, error = "Please input field (*)" });
                            }

                            foreach (var id in listdata)
                            {
                                if (TimeFrameDetail == "All")
                                {
                                    var datadetail = dbConn.Select<DC_DetailAvoidCallingTimeFrame>("HeaderID={0}", AvoidCallTime);
                                    foreach (var item in datadetail)
                                    {
                                        var checkexist = dbConn.Select<DC_Telesales_AvoidCallingTimeCompany>("SELECT * FROM DC_Telesales_AvoidCallingTimeCompany WHERE HeaderID = " + AvoidCallTime + " AND DetailHeaderID = " + item.Id + " AND CompanyID = '" + id + "'");
                                        if (checkexist.Count <= 0)
                                        {
                                            var data = new DC_Telesales_AvoidCallingTimeCompany();
                                            data.CompanyID = id;
                                            data.HeaderID = AvoidCallTime;
                                            data.DetailHeaderID = item.Id.ToString();
                                            data.RowCreatedTime = DateTime.Now;
                                            data.RowCreatedUser = currentUser.UserName;
                                            data.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                                            data.RowLastUpdatedUser = "";
                                            data.Monday = Monday;
                                            data.Tuesday = Tueday;
                                            data.Wednesday = Wednesday;
                                            data.Thursday = Thusday;
                                            data.Friday = Friday;
                                            data.Saturday = Saturday;
                                            data.Sunday = Sunday;
                                            dbConn.Insert(data); ;
                                        }
                                        else
                                        {
                                            return Json(new { success = false, error = "Avoid Call Time is exists." });
                                        }

                                    }
                                }
                                else
                                {
                                    var checkexist = dbConn.Select<DC_Telesales_AvoidCallingTimeCompany>("SELECT * FROM DC_Telesales_AvoidCallingTimeCompany WHERE HeaderID = " + AvoidCallTime + " AND DetailHeaderID = " + TimeFrameDetail + " AND CompanyID = '" + id + "'");
                                    if (checkexist.Count <= 0)
                                    {
                                        var data = new DC_Telesales_AvoidCallingTimeCompany();
                                        data.CompanyID = id;
                                        data.HeaderID = AvoidCallTime;
                                        data.DetailHeaderID = TimeFrameDetail;
                                        data.RowCreatedTime = DateTime.Now;
                                        data.RowCreatedUser = currentUser.UserName;
                                        data.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                                        data.RowLastUpdatedUser = "";
                                        data.Monday = Monday;
                                        data.Tuesday = Tueday;
                                        data.Wednesday = Wednesday;
                                        data.Thursday = Thusday;
                                        data.Friday = Friday;
                                        data.Saturday = Saturday;
                                        data.Sunday = Sunday;
                                        dbConn.Insert(data);
                                    }
                                    else
                                    {
                                        return Json(new { success = false, error = "Avoid Call Time is exists." });
                                    }

                                }
                            }
                            return Json(new { success = true });
                        }
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, error = ex });
                    }
                }
                else
                {
                    return Json(new { success = false, error = "Please input field (*)" });
                }
            }
            else
            {
                return Json(new { success = false, error = "You don't have permission to add record" });
            }
        }

        public ActionResult Delete_Com_AvoidCallingTime(string data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
                if (asset.Delete)
                {
                    int success = 0;
                    int error = 0;
                    try
                    {
                        string[] separators = { "@@" };
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        var data1 = new DC_Telesales_AvoidCallingTimeCompany();
                        foreach (var item in listRowID)
                        {
                            data1.Id = Int32.Parse(item);
                            dbConn.Delete(data1);
                        }
                        success++;
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, alert = ex.Message });
                    }
                    return Json(new { success = true, totalSuccess = success, totalError = error });
                }
                else
                {
                    return Json(new { success = false, alert = "You don't have permission to delete record" });
                }
        }

        public ActionResult Export([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.Export)
            {
                var data = DC_DetailAvoidCallingTimeFrame.GetAll().ToList();
                IEnumerable datas = data.ToDataSourceResult(request).Data;

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_AvoidCallingTimeCompany.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["AvoidCallingTimeCompany"];
                int rowData = 1;
                foreach (DC_DetailAvoidCallingTimeFrame item in datas)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = item.Item;
                    dataSheet.Cells[rowData, i++].Value = item.FromHour;
                    dataSheet.Cells[rowData, i++].Value = item.ToHour;
                    dataSheet.Cells[rowData, i++].Value = item.Monday;
                    dataSheet.Cells[rowData, i++].Value = item.Tuesday;
                    dataSheet.Cells[rowData, i++].Value = item.Wednesday;
                    dataSheet.Cells[rowData, i++].Value = item.Thursday;
                    dataSheet.Cells[rowData, i++].Value = item.Friday;
                    dataSheet.Cells[rowData, i++].Value = item.Saturday;
                    dataSheet.Cells[rowData, i++].Value = item.Sunday;
                    dataSheet.Cells[rowData, i++].Value = item.RowCreatedUser;
                    dataSheet.Cells[rowData, i++].Value = item.RowCreatedTime;
                    dataSheet.Cells[rowData, i++].Value = item.RowLastUpdatedUser;
                    dataSheet.Cells[rowData, i++].Value = item.RowLastUpdatedTime;
                }

                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_AvoidCallingTimeCompany_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to export data");
                return File("", //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_AvoidCallingTimeCompany_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
        }

    }
}
