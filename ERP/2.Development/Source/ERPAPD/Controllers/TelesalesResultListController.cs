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
    public class TelesalesResultListController : CustomController
    {

        public ActionResult Index()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                if (asset.View)
                {
                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;
                    ViewData["Asset"] = asset;
                   
                        ViewData["UserGroups"] = dbConn.Select<Groups>();
                    
                    ViewBag.AllowView = true;

                    //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                    //{
                    //    OrmLiteConfig.DialectProvider.UseUnicode = true;
                    //    dbConn.DropTables(typeof(DC_Telesales_ResultList));
                    //    const bool overwrite = false;
                    //    dbConn.CreateTables(overwrite, typeof(DC_Telesales_ResultList));
                    //}
                    ViewBag.ListResultParent = dbConn.Select<DC_Telesales_ResultList>("Select * From DC_Telesales_ResultList Where SubId = '0'").ToList();
                    return View();
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
            }
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<DC_Telesales_ResultList>();
                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                    data = dbConn.Select<DC_Telesales_ResultList>("SELECT * FROM DC_Telesales_ResultList WHERE " + where).OrderBy(p => p.SubId).ToList();
                }
                else
                {
                    data = dbConn.Select<DC_Telesales_ResultList>().OrderBy(p => p.Id).ToList();
                }
                return Json(data.ToDataSourceResult(request));
            }
        }

        public ActionResult GetResutlList()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<DC_Telesales_ResultList>();
                data = dbConn.Select<DC_Telesales_ResultList>("Select * From DC_Telesales_ResultList Where SubId = '0'").ToList();
                return Json(data);
            }
        }

        [HttpPost]
        public ActionResult SaveResult(string SubID, string Description)
        {
            if (asset.View)
            {
                if (string.IsNullOrEmpty(SubID) || string.IsNullOrEmpty(Description))
                {
                    return Json(new { success = false, error = "Please input data" });
                }
                else
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            var data = new DC_Telesales_ResultList();
                            var lastId = DC_Telesales_ResultList.GetLastID();
                            string a = "";
                            if (lastId == null)
                            {
                                a = "R000";
                            }
                            else
                            {
                                var num = lastId.Id.Substring(2, lastId.Id.ToString().Length - 2);
                                var id = Int16.Parse(num.ToString()) + 1;
                                a = "R" + String.Format("{0:000}", id);
                            }
                            data.Id = a;
                            data.Active = true;
                            data.SubId = SubID;
                            data.Description = Description;
                            data.RowCreatedTime = DateTime.Now;
                            data.RowCreatedUser = currentUser.UserName;
                            data.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                            data.RowLastUpdatedUser = "";
                            dbConn.Insert(data);
                            dbTrans.Commit();
                            return Json(new { success = true });
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            return Json(new { success = false, error = e.Message });
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record ");
                return Json(new { success = false, error = "You don't have permission to create record" });
            }
        }


        [HttpPost]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<DC_Telesales_ResultList> listResult)
        {
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        if (listResult != null && ModelState.IsValid)
                        {
                            foreach (var item in listResult)
                            {
                                if (String.IsNullOrEmpty(item.Description))
                                {
                                    ModelState.AddModelError("", "Please input field [Description]");
                                    return Json(listResult.ToDataSourceResult(request, ModelState));
                                }

                                var checkDescription = dbConn.Select<DC_Telesales_ResultList>("SELECT * FROM DC_Telesales_ResultList WHERE Description = '" + item.Description + "'");
                                if (checkDescription.Count > 0)
                                {
                                    ModelState.AddModelError("", "[Description] exist in system");
                                    return Json(listResult.ToDataSourceResult(request, ModelState));
                                }

                                var lastId = DC_Telesales_ResultList.GetLastID();
                                string a = "";
                                if (lastId == null)
                                {
                                    a = "R000";
                                }
                                else
                                {
                                    var num = lastId.Id.Substring(2, lastId.Id.ToString().Length - 2);
                                    var id = Int16.Parse(num.ToString()) + 1;
                                    a = "R" + String.Format("{0:000}", id);
                                }

                                if (!string.IsNullOrEmpty(item.SubId))
                                {
                                    item.SubId = item.SubId;
                                }
                                else
                                {
                                    item.SubId = "0";
                                }

                                if (item.Id == item.SubId)
                                {
                                    ModelState.AddModelError("", "Can't update this record: Self -own");
                                    return Json(listResult.ToDataSourceResult(request, ModelState));
                                }

                                item.Active = item.Active;
                                item.Id = a;
                                item.Description = item.Description;
                                item.RowCreatedUser = currentUser.UserName;
                                item.RowCreatedTime = DateTime.Now;
                                item.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                                item.RowLastUpdatedUser = "";
                                dbConn.Insert(item);

                                dbTrans.Commit();
                                return Json(new { success = true });
                            }
                            return Json(listResult.ToDataSourceResult(request, ModelState));
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
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

        [HttpPost]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<DC_Telesales_ResultList> listResult)
        {
            if (asset.Update && asset.Update)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        if (listResult != null && ModelState.IsValid)
                        {
                            foreach (var item in listResult)
                            {
                                if (String.IsNullOrEmpty(item.Description))
                                {
                                    ModelState.AddModelError("", "Please input field [Description]");
                                    return Json(listResult.ToDataSourceResult(request, ModelState));
                                }

                                var checkDescription = dbConn.Select<DC_Telesales_ResultList>("SELECT * FROM DC_Telesales_ResultList WHERE Description = '" + item.Description + "' AND [Id] <> '" + item.Id + "'");
                                if (checkDescription.Count > 0)
                                {
                                    ModelState.AddModelError("", "[Description] exist in system");
                                    return Json(listResult.ToDataSourceResult(request, ModelState));
                                }

                                if (!string.IsNullOrEmpty(item.SubId))
                                {
                                    item.SubId = item.SubId;
                                }
                                else
                                {
                                    item.SubId = "0";
                                }

                                if (item.Id == item.SubId)
                                {
                                    ModelState.AddModelError("", "Can't update this record: Self -own");
                                    return Json(listResult.ToDataSourceResult(request, ModelState));
                                }
                                item.Active = item.Active;
                                item.Id = item.Id;
                                item.Description = item.Description;
                                item.RowLastUpdatedTime = DateTime.Now;
                                item.RowLastUpdatedUser = currentUser.UserName;
                                dbConn.Update(item);
                            }
                            dbTrans.Commit();
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
                        dbTrans.Rollback();
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


        public FileResult Export([DataSourceRequest]
                                 DataSourceRequest request, string organizationId)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                if (asset.Export)
                {
                    List<DC_Telesales_ResultList> d = new List<DC_Telesales_ResultList>();
                    d = dbConn.Select<DC_Telesales_ResultList>("Select * From DC_Telesales_ResultList").ToList();

                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Telesales_ResultList.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    //data sheet
                    ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["TelesaleResult Master"];
                    IEnumerable listData = d.ToDataSourceResult(request).Data;
                    int rowData = 1;
                    foreach (DC_Telesales_ResultList data in listData)
                    {
                        int i = 1;
                        rowData++;
                        dataSheet.Cells[rowData, i++].Value = data.Id;
                        dataSheet.Cells[rowData, i++].Value = data.Description;
                        if (data.SubId == "0")
                        {
                            dataSheet.Cells[rowData, i++].Value = "";
                        }
                        else
                        {
                            var dataSub = dbConn.Select<DC_Telesales_ResultList>("Select * From DC_Telesales_ResultList Where SubId = '" + data.SubId + "'").ToList().FirstOrDefault();
                            dataSheet.Cells[rowData, i++].Value = dataSub.Description;
                        }
                        dataSheet.Cells[rowData, i++].Value = data.Active;
                        dataSheet.Cells[rowData, i++].Value = data.RowCreatedUser;
                        dataSheet.Cells[rowData, i++].Value = data.RowCreatedTime.ToString();
                        dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedUser;
                        dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedTime.ToString();
                    }

                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    string fileName = "DC_Telesales_Result" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    output.Position = 0;
                    return File(output.ToArray(), contentType, fileName);
                }
                else
                {
                    string fileName = "DC_Telesales_Result" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    return File("", contentType, fileName);
                }
            }

        }

        public ActionResult Remove(string data)
        {
            if (asset.Delete)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        string[] separators = { "@@" };
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listRowID)
                        {
                            var dataCheck = new List<DC_Telesales_ResultList>();
                            dataCheck = dbConn.Select<DC_Telesales_ResultList>("Select * From DC_Telesales_ResultList Where SubId = '" + item + "'").ToList();
                            if (dataCheck.Count > 0)
                            {
                                return Json(new { success = false, message = "Can't delete record: Self -own" });
                            }
                            else
                            {
                                DC_Telesales_ResultList check = new DC_Telesales_ResultList();
                                check.Id = item;
                                dbConn.Delete(check);
                            }
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        return Json(new { success = false, message = e });
                    }
                    return Json(new { success = true });
                }
            }
            else
            {
                return Json(new { success = false, message = "You don't have permission to delete record" });
            }
        }

        //[HttpPost]
        //public ActionResult ImportFromExcel()
        //{
        //    try
        //    {
        //        if (Request.Files["FileUpload"] != null && Request.Files["fileUpload"].ContentLength > 0)
        //        {
        //            string fileExtension =
        //                System.IO.Path.GetExtension(Request.Files["fileUpload"].FileName);

        //            if (fileExtension == ".xlsx")
        //            {
        //                using (var dbConn = Helpers.OrmliteConnection.openConn())
        //                using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
        //                {
        //                    string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["fileUpload"].FileName);
        //                    string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["fileUpload"].FileName);

        //                    if (System.IO.File.Exists(fileLocation))
        //                        System.IO.File.Delete(fileLocation);

        //                    Request.Files["fileUpload"].SaveAs(fileLocation);
        //                    var importdetail = Request["ImportDetail"];

        //                    FileInfo fileInfo = new FileInfo(fileLocation);
        //                    var excelPkg = new ExcelPackage(fileInfo);

        //                    FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_ChartOfAccount.xlsx"));
        //                    template.CopyTo(errorFileLocation);
        //                    FileInfo _fileInfo = new FileInfo(errorFileLocation);
        //                    var _excelPkg = new ExcelPackage(_fileInfo);

        //                    if (importdetail == "false")
        //                    {
        //                        var rownumber = 2;
        //                        var total = 0;

        //                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["COA"];
        //                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["COA"];

        //                        //remove row
        //                        int totalRows = oSheet.Dimension.End.Row;

        //                        for (int i = 2; i <= totalRows; i++)
        //                        {
        //                            var Id = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value : "";
        //                            var accId = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value : "";
        //                            var accName = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value : "";
        //                            var active = oSheet.Cells[i, 6].Value != null ? oSheet.Cells[i, 6].Value : "";

        //                            try
        //                            {
        //                                if (string.IsNullOrEmpty(accId.ToString()) || string.IsNullOrEmpty(accName.ToString()))
        //                                {
        //                                    eSheet.Cells[rownumber, 1].Value = Id;
        //                                    eSheet.Cells[rownumber, 2].Value = accId;
        //                                    eSheet.Cells[rownumber, 3].Value = accName;
        //                                    eSheet.Cells[rownumber, 5].Value = active;
        //                                    eSheet.Cells[rownumber, 10].Value = "AccID,AccName is not valid";
        //                                }
        //                                else
        //                                {
        //                                    if (!string.IsNullOrEmpty(Id.ToString()))
        //                                    {
        //                                        var existCOA = dbConn.Single<DC_Cost_ChartOfAccount>("Id={0}", Id);
        //                                        existCOA.AccID = accId.ToString();
        //                                        existCOA.AccName = accName.ToString();
        //                                        existCOA.Active = Boolean.Parse(active.ToString());
        //                                        existCOA.RowLastUpdatedTime = DateTime.Now;
        //                                        existCOA.RowLastUpdatedUser = currentUser.UserName;
        //                                        dbConn.Save(existCOA);
        //                                        total++;
        //                                    }
        //                                    else
        //                                    {
        //                                        var coa = new DC_Cost_ChartOfAccount();
        //                                        coa.AccID = accId.ToString();
        //                                        coa.AccName = accName.ToString();
        //                                        coa.Active = Boolean.Parse(active.ToString());
        //                                        coa.RowCreatedTime = DateTime.Now;
        //                                        coa.RowCreatedUser = currentUser.UserName;
        //                                        coa.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
        //                                        coa.BaseLine = DateTime.Parse("1900-01-01");
        //                                        dbConn.Save(coa);
        //                                        total++;
        //                                    }

        //                                }
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                eSheet.Cells[rownumber, 1].Value = Id;
        //                                eSheet.Cells[rownumber, 2].Value = accId;
        //                                eSheet.Cells[rownumber, 3].Value = accName;
        //                                eSheet.Cells[rownumber, 5].Value = active;
        //                                eSheet.Cells[rownumber, 10].Value = e.Message;
        //                                rownumber++;
        //                                continue;
        //                            }
        //                        }

        //                        dbTrans.Commit();
        //                        _excelPkg.Save();

        //                        return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
        //                    }
        //                    else
        //                    {
        //                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["COA"];
        //                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["COA"];
        //                        int totalRows2 = oSheet.Dimension.End.Row;

        //                        for (int i = 2; i <= totalRows2; i++)
        //                        {
        //                            eSheet.Cells[i, 1].Value = oSheet.Cells[i, 1].Value;
        //                            eSheet.Cells[i, 2].Value = oSheet.Cells[i, 2].Value;
        //                            eSheet.Cells[i, 3].Value = oSheet.Cells[i, 3].Value;
        //                            eSheet.Cells[i, 4].Value = oSheet.Cells[i, 4].Value;
        //                            eSheet.Cells[i, 5].Value = oSheet.Cells[i, 5].Value;
        //                            eSheet.Cells[i, 6].Value = oSheet.Cells[i, 6].Value;
        //                            eSheet.Cells[i, 7].Value = oSheet.Cells[i, 7].Value;
        //                            eSheet.Cells[i, 8].Value = oSheet.Cells[i, 8].Value;
        //                            eSheet.Cells[i, 9].Value = oSheet.Cells[i, 9].Value;
        //                            eSheet.Cells[i, 10].Value = oSheet.Cells[i, 10].Value;
        //                        }

        //                        //department
        //                        var rownumber = 2;
        //                        var total = 0;

        //                        ExcelWorksheet sSheet = excelPkg.Workbook.Worksheets["SubCOA"];
        //                        ExcelWorksheet eSSheet = _excelPkg.Workbook.Worksheets["SubCOA"];

        //                        //remove row
        //                        int totalRows = sSheet.Dimension.End.Row;

        //                        for (int i = 2; i <= totalRows; i++)
        //                        {
        //                            var subcoaId = sSheet.Cells[i, 1].Value != null ? sSheet.Cells[i, 1].Value : "";
        //                            var accId = sSheet.Cells[i, 2].Value != null ? sSheet.Cells[i, 2].Value : "";
        //                            var subAccName = sSheet.Cells[i, 3].Value != null ? sSheet.Cells[i, 3].Value : "";
        //                            var balance = sSheet.Cells[i, 4].Value != null ? sSheet.Cells[i, 4].Value : "";
        //                            var baseline = sSheet.Cells[i, 5].Value != null ? sSheet.Cells[i, 5].Value : "";
        //                            var active = sSheet.Cells[i, 6].Value != null ? sSheet.Cells[i, 6].Value : "";

        //                            try
        //                            {
        //                                if (string.IsNullOrEmpty(subcoaId.ToString()) || string.IsNullOrEmpty(accId.ToString()) || string.IsNullOrEmpty(subAccName.ToString()))
        //                                {
        //                                    eSSheet.Cells[rownumber, 1].Value = subcoaId;
        //                                    eSSheet.Cells[rownumber, 2].Value = accId;
        //                                    eSSheet.Cells[rownumber, 3].Value = subAccName;
        //                                    eSSheet.Cells[rownumber, 4].Value = balance;
        //                                    eSSheet.Cells[rownumber, 5].Value = baseline;
        //                                    eSSheet.Cells[rownumber, 6].Value = active;
        //                                    eSSheet.Cells[rownumber, 11].Value = "SubAccID,AccID,SubAccName is not valid";
        //                                }
        //                                else
        //                                {
        //                                    var subCoa = new DC_Cost_Sub_ChartOfAccount();
        //                                    subCoa.SubAccID = subcoaId.ToString();
        //                                    subCoa.AccID = dbConn.Single<DC_Cost_ChartOfAccount>("AccID={0}", accId) != null ? dbConn.Single<DC_Cost_ChartOfAccount>("AccID={0}", accId).Id : 0;
        //                                    subCoa.SubAccName = subAccName.ToString();
        //                                    subCoa.RowCreatedTime = DateTime.Now;
        //                                    subCoa.RowCreatedUser = currentUser.UserName;
        //                                    subCoa.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
        //                                    subCoa.BaseLine = DateTime.Parse("1900-01-01");
        //                                    dbConn.Save(subCoa);
        //                                    total++;
        //                                }
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                eSSheet.Cells[rownumber, 1].Value = subcoaId;
        //                                eSSheet.Cells[rownumber, 2].Value = accId;
        //                                eSSheet.Cells[rownumber, 3].Value = subAccName;
        //                                eSSheet.Cells[rownumber, 4].Value = balance;
        //                                eSSheet.Cells[rownumber, 5].Value = baseline;
        //                                eSSheet.Cells[rownumber, 6].Value = active;
        //                                eSSheet.Cells[rownumber, 11].Value = e.Message;
        //                                rownumber++;
        //                                continue;
        //                            }
        //                        }

        //                        dbTrans.Commit();
        //                        _excelPkg.Save();

        //                        return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                return Json(new { success = false, error = "File extension is not valid. *.xlsx please." });
        //            }
        //        }
        //        else
        //        {
        //            return Json(new { success = false, error = "File upload null" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, error = ex.Message });
        //    }
        //}
    }
}
