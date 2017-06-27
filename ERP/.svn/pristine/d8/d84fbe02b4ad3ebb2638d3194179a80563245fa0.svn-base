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
    public class TelesalesQuestionListController : CustomController
    {
        //

        public ActionResult Index()
        {
            ViewData["AllowCreate"] = asset.Create;
            ViewData["AllowUpdate"] = asset.Update;
            ViewData["AllowDelete"] = asset.Delete;
            ViewData["AllowExport"] = asset.Export;
            OrmLiteConfig.DialectProvider = new SqlServerOrmLiteDialectProvider();
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //OrmLiteConfig.DialectProvider.UseUnicode = true;
                    //dbConn.DropTables(typeof(DC_Telesales_QuestionList));
                    //const bool overwrite = false;
                    //dbConn.CreateTables(overwrite, typeof(DC_Telesales_QuestionList));

                    //if (dbConn.Select<DC_Telesales_QuestionList>().Count() == 0)
                    //{
                    //    dbConn.Insert(new DC_Telesales_QuestionList { ID = 1, RegionID = "demo", Question = "Question 1", AnswerType = "Answer 1", RowCreatedUser = "administrator", RowCreatedTime = DateTime.Now, RowLastUpdatedUser = "administrator", RowLastUpdatedTime = DateTime.Now });
                    //}
                    var listRegion = dbConn.Select<DC_OCM_Territory>("[Level]={0}", "Region");
                    listRegion.Add(new DC_OCM_Territory { TerritoryID = "All", TerritoryName = " Toàn quốc" });
                    ViewBag.listRegion = listRegion.OrderBy(a => a.TerritoryName);
                    return View();
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult listRegion()
        {
            var data = DC_Location_Region.GetAllDC_Location_Regions().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult listAnswerType()
        {
            var listAnswerType = new List<Object>();
            listAnswerType.Add(new { Value = "Yes/No", Name = "Yes/No" });
            listAnswerType.Add(new { Value = "Free text", Name = "Free text" });
            listAnswerType.Add(new { Value = "Number", Name = "Number" });
            return Json(listAnswerType, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<DC_Telesales_QuestionList>();
                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                    data = dbConn.Select<DC_Telesales_QuestionList>(where);
                }
                else
                {
                    data = dbConn.Select<DC_Telesales_QuestionList>();
                }
                return Json(data.ToDataSourceResult(request));
            }
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Telesales_QuestionList> list)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    if (list != null && ModelState.IsValid)
                    {
                        foreach (var item in list)
                        {
                            if (string.IsNullOrEmpty(item.RegionID))
                            {
                                ModelState.AddModelError("", "Please select Region.");
                                return Json(list.ToDataSourceResult(request, ModelState));
                                // return Json(new { success = false, error = "Please select Region" });
                            }

                            if (string.IsNullOrEmpty(item.AnswerType))
                            {
                                ModelState.AddModelError("", "Please select AnswerType.");
                                return Json(list.ToDataSourceResult(request, ModelState));
                                //return Json(new { success = false, error = "Please select AnswerType" });
                            }

                            if (string.IsNullOrEmpty(item.Question))
                            {
                                ModelState.AddModelError("", "Please select Question.");
                                return Json(list.ToDataSourceResult(request, ModelState));
                                // return Json(new { success = false, error = "Please select Question" });
                            }

                            var trackKey = dbConn.Where<DC_Telesales_QuestionList>(new { RegionID = item.RegionID, AnswerType = item.AnswerType, Question = item.Question });
                            if (trackKey.Count > 0)
                            {
                                ModelState.AddModelError("", "The question in this region already exists");
                                return Json(list.ToDataSourceResult(request, ModelState));
                                ///return Json(new { success = false, error = "The question in this region already exists" });
                            }

                            item.RowCreatedTime = DateTime.Now;
                            item.RowCreatedUser = currentUser.UserName;
                            item.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                            item.RowLastUpdatedUser = "";
                            dbConn.Insert(item);
                            dbTrans.Commit();
                        }
                        return Json(new { success = true });
                    }
                    //return Json(new { success = true });
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    ModelState.AddModelError("", e.Message);
                    return Json(new { succsess = false });
                }
                return Json(list.ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Telesales_QuestionList> list)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    if (list != null && ModelState.IsValid)
                    {
                        foreach (var item in list)
                        {
                            if (string.IsNullOrEmpty(item.RegionID))
                            {
                                ModelState.AddModelError("", "Please select Region.");
                                return Json(list.ToDataSourceResult(request, ModelState));
                            }

                            if (string.IsNullOrEmpty(item.AnswerType))
                            {
                                ModelState.AddModelError("", "Please select AnswerType.");
                                return Json(list.ToDataSourceResult(request, ModelState));
                            }

                            if (string.IsNullOrEmpty(item.Question))
                            {
                                ModelState.AddModelError("", "Please select Question.");
                                return Json(list.ToDataSourceResult(request, ModelState));

                            }

                            var trackKey = dbConn.Where<DC_Telesales_QuestionList>(new { RegionID = item.RegionID, AnswerType = item.AnswerType, Question = item.Question });
                            if (trackKey.Count > 0)
                            {
                                ModelState.AddModelError("", "The question in this region already exists");
                                return Json(list.ToDataSourceResult(request, ModelState));

                            }
                            item.RowLastUpdatedTime = DateTime.Now;
                            item.RowLastUpdatedUser = currentUser.UserName;
                            dbConn.Update(item);
                            dbTrans.Commit();
                        }
                        return Json(new { success = true });
                    }
                    //return Json(new { success = true });
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    ModelState.AddModelError("", e.Message);
                    return Json(new { succsess = false });
                }
                return Json(list.ToDataSourceResult(request, ModelState));
            }
        }

        public ActionResult Delete(string data)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    string[] separators = { "@@" };
                    var listid = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var id in listid)
                    {
                        dbConn.Delete<DC_Telesales_QuestionList>(q => q.Where(p => p.ID == int.Parse(id)));
                    }
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

        public FileResult Export(string where)
        {
            if (asset.Export)
            {
                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Telesales_QuestionList.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);

                //data sheet Region.
                var listRegion = DC_Location_Region.GetAllDC_Location_Regions().ToList();
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["Region"];
                int rowData = 1;
                foreach (DC_Location_Region data in listRegion)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.RegionID + ":" + data.RegionName;
                }

                //data sheet Master.
                var master = new List<DC_Telesales_QuestionList>();
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    master = dbConn.Select<DC_Telesales_QuestionList>("SELECT * FROM DC_Telesales_QuestionList");
                }
                ExcelWorksheet dataSheetMaster = excelPkg.Workbook.Worksheets["Master data"];
                int rowData1 = 1;
                foreach (DC_Telesales_QuestionList data in master)
                {
                    int i = 1;
                    rowData1++;
                    foreach (var item in listRegion)
                    {
                        if (item.RegionID == data.RegionID)
                        {
                            dataSheetMaster.Cells[rowData1, i++].Value = item.RegionID + ": " + item.RegionName;
                            break;
                        }
                    }
                    dataSheetMaster.Cells[rowData1, i++].Value = data.Question;
                    dataSheetMaster.Cells[rowData1, i++].Value = data.AnswerType;
                    dataSheetMaster.Cells[rowData1, i++].Value = data.RowCreatedUser;
                    dataSheetMaster.Cells[rowData1, i++].Value = data.RowCreatedTime.ToString();
                    dataSheetMaster.Cells[rowData1, i++].Value = data.RowLastUpdatedUser;
                    dataSheetMaster.Cells[rowData1, i++].Value = data.RowLastUpdatedTime.ToString();
                }
                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_Telesales_QuestionList" + where + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                string fileName = "DC_Telesales_QuestionList" + where + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentType, fileName);
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

                    if (fileExtension == ".xlsx")
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["fileUpload"].FileName);
                        string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["fileUpload"].FileName);

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);

                        Request.Files["fileUpload"].SaveAs(fileLocation);
                        //Request.Files["fileUpload"].SaveAs(errorFileLocation);

                        var rownumber = 2;
                        var total = 0;

                        FileInfo fileInfo = new FileInfo(fileLocation);
                        var excelPkg = new ExcelPackage(fileInfo);

                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Telesales_QuestionList.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);

                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["Master data"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["Master data"];

                        //remove row
                        int totalRows = oSheet.Dimension.End.Row;
                        //eSheet.DeleteRow(2, totalRows, true);
                        //var realdata = WorksheetToDataTable(oSheet);
                        double dTemp;
                        DateTime tTemp;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            string region = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            string question = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            string answerType = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                            string regionID = region.Substring(0, region.IndexOf(':'));

                            using (var dbConn = Helpers.OrmliteConnection.openConn())
                            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                            {
                                try
                                {
                                    if (string.IsNullOrEmpty(region) || string.IsNullOrEmpty(question) || string.IsNullOrEmpty(answerType))
                                    {
                                        eSheet.Cells[rownumber, 1].Value = region;
                                        eSheet.Cells[rownumber, 2].Value = question;
                                        eSheet.Cells[rownumber, 3].Value = answerType;
                                        eSheet.Cells[rownumber, 8].Value = "Please input Region , Question , Answer Type.";
                                        rownumber++;
                                    }

                                    var trackKey = dbConn.Where<DC_Telesales_QuestionList>(new { RegionID = regionID, AnswerType = answerType, Question = question });
                                    if (trackKey.Count > 0)
                                    {
                                        eSheet.Cells[rownumber, 1].Value = region;
                                        eSheet.Cells[rownumber, 2].Value = question;
                                        eSheet.Cells[rownumber, 3].Value = answerType;
                                        eSheet.Cells[rownumber, 8].Value = "The question in this region already exist";
                                        rownumber++;
                                    }
                                    else
                                    {
                                        DC_Telesales_QuestionList item = new DC_Telesales_QuestionList();
                                        item.RegionID = region.Substring(0, region.IndexOf(':'));
                                        item.Question = question;
                                        item.AnswerType = answerType;
                                        item.RowCreatedTime = DateTime.Now;
                                        item.RowCreatedUser = currentUser.UserName;
                                        item.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                                        item.RowLastUpdatedUser = "";
                                        dbConn.Save(item);
                                        dbTrans.Commit();

                                        total++;
                                    }
                                }
                                catch (Exception e)
                                {
                                    dbTrans.Rollback();
                                    eSheet.Cells[rownumber, 1].Value = region;
                                    eSheet.Cells[rownumber, 2].Value = question;
                                    eSheet.Cells[rownumber, 3].Value = answerType;

                                    eSheet.Cells[rownumber, 8].Value = e.Message;
                                    rownumber++;
                                    continue;
                                }
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
