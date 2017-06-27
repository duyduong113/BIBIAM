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
    public class TelesalesAvoidCallingTimeFrameController : CustomController
    {
   
        //
        // GET: /AvoidCallingTimeFrame/

        public ActionResult Index()
        {
        //    using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
        //    {
        //        OrmLiteConfig.DialectProvider.UseUnicode = true;
        //        dbConn.DropTables(typeof(DC_AvoidCallingTimeFrame));
        //        const bool overwrite = false;
        //        dbConn.CreateTables(overwrite, typeof(DC_AvoidCallingTimeFrame));
        //    }

        ////    Detail Avoid Calling Time Frame
        //    using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
        //    {
        //        OrmLiteConfig.DialectProvider.UseUnicode = true;
        //        dbConn.DropTables(typeof(DC_DetailAvoidCallingTimeFrame));
        //        const bool overwrite = false;
        //        dbConn.CreateTables(overwrite, typeof(DC_DetailAvoidCallingTimeFrame));
        //    }

            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                ViewData["Asset"] = asset;

                return View();
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
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = new List<DC_AvoidCallingTimeFrame>();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Select<DC_AvoidCallingTimeFrame>("SELECT * FROM DC_AvoidCallingTimeFrame Where " + where);
                    }
                    else
                    {
                        data = dbConn.Select<DC_AvoidCallingTimeFrame>("SELECT * FROM DC_AvoidCallingTimeFrame");
                    }
                    return Json(data.ToDataSourceResult(request));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult Read_DetailAvoidCallingTimeFrame([DataSourceRequest] DataSourceRequest request, string HeaderID)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = new List<DC_DetailAvoidCallingTimeFrame>();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Select<DC_DetailAvoidCallingTimeFrame>("SELECT * FROM DC_DetailAvoidCallingTimeFrame Where " + where);
                    }
                    else
                    {
                        data = dbConn.Select<DC_DetailAvoidCallingTimeFrame>("SELECT * FROM DC_DetailAvoidCallingTimeFrame WHERE HeaderID = '" + HeaderID + "'");
                    }
                    return Json(data.ToDataSourceResult(request));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        [HttpPost]
        public ActionResult Save_AvoidCallingTimeFrame([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_AvoidCallingTimeFrame> listAvoiCalling)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Create)
                {
                    try
                    {
                        if (listAvoiCalling != null && ModelState.IsValid)
                        {
                            foreach (var typ in listAvoiCalling)
                            {
                                if (String.IsNullOrEmpty(typ.HeaderName))
                                {
                                    ModelState.AddModelError("", "Please input Header name");
                                    return Json(listAvoiCalling.ToDataSourceResult(request, ModelState));
                                }
                                string id = "";
                                var data = new DC_AvoidCallingTimeFrame();
                                var checkID = dbConn.Select<DC_AvoidCallingTimeFrame>("SELECT TOP 1 * FROM dbo.DC_AvoidCallingTimeFrame ORDER BY Id DESC").FirstOrDefault();
                                if (checkID != null)
                                {
                                    var nextNo = Int32.Parse(checkID.HeaderID.Substring(4, checkID.HeaderID.Length - 4)) + 1;
                                    id = "ACTF" + String.Format("{0:00000}", nextNo);
                                }
                                else
                                {
                                    id = "ACTF00001";
                                }

                                var checkexist = dbConn.Select<DC_AvoidCallingTimeFrame>("SELECT TOP 1 * FROM DC_AvoidCallingTimeFrame WHERE HeaderName = '" + typ.HeaderName + "'").FirstOrDefault();
                                if (checkexist != null)
                                {
                                    ModelState.AddModelError("", " Header Name  is exists.");
                                    return Json(listAvoiCalling.ToDataSourceResult(request, ModelState));
                                }
                                data.HeaderID = id;
                                data.HeaderName = typ.HeaderName.Trim();
                                data.RowCreatedTime = DateTime.Now;
                                data.RowCreatedUser = currentUser.UserName;
                                data.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                                data.RowLastUpdatedUser = "";
                                dbConn.Insert(data);
                            }
                            dbTrans.Commit();
                            return Json(new { success = true });
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            dbTrans.Rollback();
                            return Json(new { success = false });
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(listAvoiCalling.ToDataSourceResult(request, ModelState));
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to create record");
                    dbTrans.Rollback();
                    return Json(listAvoiCalling.ToDataSourceResult(request, ModelState));
                }

        }

        [HttpPost]
        public ActionResult Save_DetailAvoidCallingTimeFrame([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_DetailAvoidCallingTimeFrame> listDetailAvoiCalling, string HeaderID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Create)
                {
                    try
                    {
                        if (listDetailAvoiCalling != null && ModelState.IsValid)
                        {
                            foreach (var de in listDetailAvoiCalling)
                            {
                                //if (String.IsNullOrEmpty(de.FromHourDate.ToString()) || String.IsNullOrEmpty(de.ToHourDate.ToString()))
                                //{
                                //    ModelState.AddModelError("", "Please input FromHour or ToHour name");
                                //    return Json(listDetailAvoiCalling.ToDataSourceResult(request, ModelState));
                                //}
                                string id = "";
                                var data = new DC_DetailAvoidCallingTimeFrame();
                                var checkID = dbConn.Select<DC_DetailAvoidCallingTimeFrame>("SELECT * FROM dbo.DC_DetailAvoidCallingTimeFrame ORDER BY Id DESC").FirstOrDefault();
                                if (checkID != null)
                                {
                                    var nextNo = Int32.Parse(checkID.DetailHeaderID.Substring(5, checkID.DetailHeaderID.Length - 5)) + 1;
                                    id = "DACTF" + String.Format("{0:00000}", nextNo);
                                }
                                else
                                {
                                    id = "DACTF00001";
                                }
                                data.DetailHeaderID = id;
                                data.HeaderID = HeaderID;
                                data.FromHour = de.FromHour;
                                data.ToHour = de.ToHour;
                                data.RowCreatedTime = DateTime.Now;
                                data.RowCreatedUser = currentUser.UserName;
                                data.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                                data.RowLastUpdatedUser = "";
                                dbConn.Insert(data);
                            }
                            dbTrans.Commit();
                            return Json(new { success = true });
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            dbTrans.Rollback();
                            return Json(new { success = false });
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(listDetailAvoiCalling.ToDataSourceResult(request, ModelState));
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to create record");
                    dbTrans.Rollback();
                    return Json(listDetailAvoiCalling.ToDataSourceResult(request, ModelState));
                }

        }

        [HttpPost]
        public ActionResult Update_AvoidCallingTimeFrame([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_AvoidCallingTimeFrame> listAvoiCalling)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Update)
                {
                    try
                    {
                        if (listAvoiCalling != null && ModelState.IsValid)
                        {
                            foreach (var data in listAvoiCalling)
                            {
                                if (String.IsNullOrEmpty(data.HeaderName))
                                {
                                    ModelState.AddModelError("", "Please input Header Name ");
                                    return Json(listAvoiCalling.ToDataSourceResult(request, ModelState));
                                }
                                var checkexist = dbConn.Select<DC_AvoidCallingTimeFrame>("SELECT TOP 1 * FROM DC_AvoidCallingTimeFrame WHERE HeaderName = '" + data.HeaderName + "'").FirstOrDefault();
                                if (checkexist != null)
                                {
                                    ModelState.AddModelError("", " Header Name is exists.");
                                    return Json(listAvoiCalling.ToDataSourceResult(request, ModelState));
                                }
                                data.RowLastUpdatedTime = DateTime.Now;
                                data.RowLastUpdatedUser = currentUser.UserName;
                                dbConn.Update(data);
                            }
                            dbTrans.Commit();
                            return Json(new { success = true });
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            dbTrans.Rollback();
                            return Json(new { success = false });
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(listAvoiCalling.ToDataSourceResult(request, ModelState));
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to update record");
                    dbTrans.Rollback();
                    return Json(listAvoiCalling.ToDataSourceResult(request, ModelState));
                }
        }

        [HttpPost]
        public ActionResult Update_DetailAvoidCallingTimeFrame([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_DetailAvoidCallingTimeFrame> listDetailAvoiCalling)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Update)
                {
                    try
                    {
                        if (listDetailAvoiCalling != null && ModelState.IsValid)
                        {
                            foreach (var data in listDetailAvoiCalling)
                            {
                                //if (String.IsNullOrEmpty(data.FromHour) && String.IsNullOrEmpty(data.ToHour))
                                //{
                                //    ModelState.AddModelError("", "Please input FromHour, ToHour ");
                                //    return Json(listDetailAvoiCalling.ToDataSourceResult(request, ModelState));
                                //}                              
                                data.RowLastUpdatedTime = DateTime.Now;
                                data.RowLastUpdatedUser = currentUser.UserName;
                                dbConn.Update(data);
                            }
                            dbTrans.Commit();
                            return Json(new { success = true });
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            dbTrans.Rollback();
                            return Json(new { success = false });
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(listDetailAvoiCalling.ToDataSourceResult(request, ModelState));
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to update record");
                    dbTrans.Rollback();
                    return Json(listDetailAvoiCalling.ToDataSourceResult(request, ModelState));
                }
        }

        public ActionResult Delete(string data)
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
                        var data1 = new DC_AvoidCallingTimeFrame();
                        foreach (var item in listRowID)
                        {
                            var checkexist = dbConn.Select<DC_DetailAvoidCallingTimeFrame>("SELECT TOP 1 * FROM DC_DetailAvoidCallingTimeFrame WHERE HeaderID = '" + item + "'").FirstOrDefault();
                            if (checkexist == null)
                            {
                                data1.Id = Int32.Parse(item);
                                dbConn.Delete(data1);
                                success++;
                            }
                            else
                            {
                                error++;
                                return Json(new { success = false, alert = "Không thể xóa vì đã có thông tin chi tiết!" });
                            }
                        }
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

        public ActionResult DeleteDetail(string data)
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
                        var data1 = new DC_DetailAvoidCallingTimeFrame();
                        foreach (var item in listRowID)
                        {
                            //var checkexist = dbConn.Select<DC_Org_AvoidCallingTime>("SELECT TOP 1 * FROM DC_Org_AvoidCallingTime WHERE DetailHeaderID = '" + item + "'").FirstOrDefault();
                            //if (checkexist == null)
                            //{
                                data1.Id = Int32.Parse(item);
                                dbConn.Delete(data1);
                                success++;
                            //}
                            //else
                            //{
                            //    error++;
                            //    return Json(new { success = false, alert = "You can't delete this record" });
                            //}
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

        public ActionResult Export_AvoidCalling([DataSourceRequest]
                                 DataSourceRequest request)
        {
            if (asset.Export)
            {
                //Get the data representing the current grid state - page, sort and filter

                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = dbConn.Select<DC_AvoidCallingTimeFrame>();
                    IEnumerable datas = data.ToDataSourceResult(request).Data;

                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_AvoidCallingTimeFrame.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);
                    ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["AvoidCallingTimeFrame"];
                    int rowData = 1;
                    foreach (DC_AvoidCallingTimeFrame item in datas)
                    {
                        int i = 1;
                        rowData++;
                        dataSheet.Cells[rowData, i++].Value = item.Id;
                        dataSheet.Cells[rowData, i++].Value = item.HeaderID;
                        dataSheet.Cells[rowData, i++].Value = item.HeaderName;
                        dataSheet.Cells[rowData, i++].Value = item.RowCreatedTime;
                        dataSheet.Cells[rowData, i++].Value = item.RowCreatedUser;
                        dataSheet.Cells[rowData, i++].Value = item.RowLastUpdatedTime;
                        dataSheet.Cells[rowData, i++].Value = item.RowLastUpdatedUser;
                    }

                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    string fileName = "DC_AvoidCallingTimeFrame_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    output.Position = 0;
                    return File(output.ToArray(), contentType, fileName);
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to export data");
                return File("", //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_AvoidCallingTimeFrame_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
        }

        public ActionResult ImportFromExcel_AvoidCalling()
        {
            try
            {
                if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                {
                    string fileExtension =
                        System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                    if (fileExtension == ".xlsx")
                    {
                        using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                        using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                        {
                            string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);
                            string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUpload"].FileName);

                            if (System.IO.File.Exists(fileLocation))
                                System.IO.File.Delete(fileLocation);

                            Request.Files["FileUpload"].SaveAs(fileLocation);

                            var rownumber = 2;
                            var total = 0;
                            FileInfo fileInfo = new FileInfo(fileLocation);
                            var excelPkg = new ExcelPackage(fileInfo);
                            FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_AvoidCallingTimeFrame.xlsx"));
                            template.CopyTo(errorFileLocation);
                            FileInfo _fileInfo = new FileInfo(errorFileLocation);
                            var _excelPkg = new ExcelPackage(_fileInfo);
                            ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["AvoidCallingTimeFrame"];
                            ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["AvoidCallingTimeFrame"];
                            int totalRows = oSheet.Dimension.End.Row;
                            for (int i = 2; i <= totalRows; i++)
                            {
                                string Id = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                                string HeaderID = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                                string HeaderName = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                                string RowCreatedTime = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "";
                                string RowCreatedUser = oSheet.Cells[i, 5].Value != null ? oSheet.Cells[i, 5].Value.ToString() : "";
                                string RowLastUpdatedTime = oSheet.Cells[i, 6].Value != null ? oSheet.Cells[i, 6].Value.ToString() : "";
                                string RowLastUpdatedUser = oSheet.Cells[i, 7].Value != null ? oSheet.Cells[i, 7].Value.ToString() : "";
                                try
                                {
                                    if (!String.IsNullOrEmpty(HeaderName))
                                    {
                                        var data = new DC_AvoidCallingTimeFrame();
                                        var checkexist = dbConn.Select<DC_AvoidCallingTimeFrame>("SELECT TOP 1 * FROM DC_AvoidCallingTimeFrame WHERE Id = " + Id + "").FirstOrDefault();
                                        if (checkexist != null)
                                        {
                                            checkexist.HeaderName = HeaderName != null ? HeaderName : "";
                                            checkexist.RowLastUpdatedTime = DateTime.Now;
                                            checkexist.RowLastUpdatedUser = currentUser.UserName;
                                            dbConn.Update(checkexist);
                                            total++;
                                        }
                                        else
                                        {
                                            string id = "";
                                            var checkID = dbConn.Select<DC_AvoidCallingTimeFrame>("SELECT TOP 1 * FROM dbo.DC_AvoidCallingTimeFrame ORDER BY Id DESC").FirstOrDefault();
                                            if (checkID != null)
                                            {
                                                var nextNo = Int32.Parse(checkID.HeaderID.Substring(4, checkID.HeaderID.Length - 4)) + 1;
                                                id = "ACTF" + String.Format("{0:00000}", nextNo);
                                            }
                                            else
                                            {
                                                id = "ACTF00001";
                                            }
                                            data.HeaderID = id;
                                            data.HeaderName = HeaderName;
                                            data.RowCreatedTime = DateTime.Now;
                                            data.RowCreatedUser = currentUser.UserName;
                                            data.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                                            data.RowLastUpdatedUser = "";
                                            dbConn.Save(data);
                                            total++;
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    eSheet.Cells[rownumber, 1].Value = Id;
                                    eSheet.Cells[rownumber, 2].Value = HeaderID;
                                    eSheet.Cells[rownumber, 3].Value = HeaderName;
                                    eSheet.Cells[rownumber, 4].Value = RowCreatedTime;
                                    eSheet.Cells[rownumber, 5].Value = RowCreatedUser;
                                    eSheet.Cells[rownumber, 6].Value = RowLastUpdatedTime;
                                    eSheet.Cells[rownumber, 7].Value = RowLastUpdatedUser;
                                    eSheet.Cells[rownumber, 8].Value = e.Message;
                                    rownumber++;
                                    continue;
                                }
                            }
                            dbTrans.Commit();
                            _excelPkg.Save();

                            return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
                        }
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

