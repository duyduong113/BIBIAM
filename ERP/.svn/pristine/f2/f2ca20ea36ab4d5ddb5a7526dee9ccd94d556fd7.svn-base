using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using System.Collections;
using System.IO;
using OfficeOpenXml;
using ERPAPD.Helpers;
using ServiceStack.OrmLite;

namespace ERPAPD.Controllers
{
    [Authorize]
    public class CRMSurveyController : CustomController
    {
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    OrmLiteConfig.DialectProvider.UseUnicode = true;
            //    dbConn.DropTables(typeof(CRM_Survey_Category), typeof(CRM_Survey_Question), typeof(CRM_Survey_AnswerList), typeof(CRM_Survey_AnswerConfig));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(CRM_Survey_Category), typeof(CRM_Survey_Question), typeof(CRM_Survey_AnswerList), typeof(CRM_Survey_AnswerConfig));
            //}
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                if (asset.View)
                {
                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;
                    ViewData["Asset"] = asset;
                    ViewBag.Category = dbConn.Select<CRM_Survey_Category>().OrderBy(a => a.Id);
                    ViewBag.listUser = dbConn.Select<Users>("");
                   // ViewBag.listDepartment = dbConn.Select<Deca_Department>("Active = 1");
                    return View();
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
        }
        public ActionResult Questions()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    OrmLiteConfig.DialectProvider.UseUnicode = true;
            //    dbConn.DropTables(typeof(CRM_Survey_Category), typeof(CRM_Survey_Question), typeof(CRM_Survey_AnswerList), typeof(CRM_Survey_AnswerConfig));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(CRM_Survey_Category), typeof(CRM_Survey_Question), typeof(CRM_Survey_AnswerList), typeof(CRM_Survey_AnswerConfig));
            //}
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                if (asset.View)
                {
                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;
                    ViewData["Asset"] = asset;
                    ViewBag.Category = dbConn.Select<CRM_Survey_Category>().OrderBy(a => a.Id);
                    ViewBag.listUser = dbConn.Select<Users>("");
                  //  ViewBag.listDepartment = dbConn.Select<Deca_Department>("Active = 1");
                    return View();
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
        }
        public ActionResult SurveyCategory_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    return Json(KendoApplyFilter.KendoData<CRM_Survey_Category>(request));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult SurveyCategory_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Survey_Category> list)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Create)
                {
                    try
                    {
                        if (list != null && ModelState.IsValid)
                        {
                            var data = new CRM_Survey_Category();
                            foreach (var item in list)
                            {
                                string id = "";
                                if (String.IsNullOrEmpty(item.Name))
                                {
                                    ModelState.AddModelError("", "Please input name.");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                var checkID = dbConn.Select<CRM_Survey_Category>("SELECT CategoryID, Id FROM dbo.CRM_Survey_Category ORDER BY Id DESC").FirstOrDefault();
                                if (checkID != null)
                                {
                                    var nextNo = Int64.Parse(checkID.CategoryID.Substring(4, checkID.CategoryID.Length - 4)) + 1;
                                    id = "SCAT" + String.Format("{0:00000}", nextNo);
                                }
                                else
                                {
                                    id = "SCAT00001";
                                }
                                var checkColumName = dbConn.Select<CRM_Survey_Category>("SELECT Name FROM dbo.CRM_Survey_Category WHERE Name = '" + item.Name + "'").FirstOrDefault();
                                if (checkColumName != null)
                                {
                                    ModelState.AddModelError("", "Name is exists");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                data.CategoryID = id;
                                data.Description = !string.IsNullOrEmpty(item.Description) ? item.Description : "";
                                data.Name = item.Name.Trim();
                                data.Active = item.Active;
                                data.ParentCategoryID = !string.IsNullOrEmpty(item.ParentCategoryID) ? item.ParentCategoryID : "";

                                data.CreatedAt = DateTime.Now;
                                data.CreatedBy = currentUser.UserName;
                                data.UpdatedAt = DateTime.Parse("1900-01-01");
                                data.UpdatedBy = "";
                                dbConn.Save(data);
                            }
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                    return Json(new { sussess = true });
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to create record");
                    dbTrans.Rollback();
                    return Json(list.ToDataSourceResult(request, ModelState));
                }
        }
        public ActionResult SurveyCategory_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Survey_Category> list)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Update)
                {
                    try
                    {
                        if (list != null && ModelState.IsValid)
                        {
                            foreach (var item in list)
                            {
                                if (String.IsNullOrEmpty(item.Name))
                                {
                                    ModelState.AddModelError("", "Please input name");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                //var checkexist = dbConn.Select<DC_CRListColumns>("SELECT ColumnName, Note  FROM dbo.DC_CRListColumns WHERE ColumnName =  '" + item.Name + "' AND Note = '" + item.Description + "'").FirstOrDefault();
                                //if (checkexist != null)
                                //{
                                //    ModelState.AddModelError("", " Column name is exists.");
                                //    return Json(list.ToDataSourceResult(request, ModelState));
                                //}
                                item.Name = item.Name;
                                item.Description = !string.IsNullOrEmpty(item.Description) ? item.Description : "";
                                item.ParentCategoryID = !string.IsNullOrEmpty(item.ParentCategoryID) ? item.ParentCategoryID : "";
                                //item.Visibility = !string.IsNullOrEmpty(item.Visibility) ? item.Visibility : "Hidden";
                                item.Active = item.Active;
                                item.UpdatedAt = DateTime.Now;
                                item.UpdatedBy = currentUser.UserName;
                                dbConn.Update(item);
                            }
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                    return Json(new { sussess = true });
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to update record");
                    dbTrans.Rollback();
                    return Json(list.ToDataSourceResult(request, ModelState));
                }
        }
        public ActionResult DeleteCategory(string data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Delete)
                {
                    try
                    {
                        string[] separators = { "@@" };
                        var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listdata)
                        {
                            var check = dbConn.GetFirstColumn<string>("Select CategoryID FROM CRM_Survey_Question WHERE CategoryID='" + item +"'");
                            if (check.Count > 0)
                            {
                                dbTrans.Rollback();
                                return Json(new { success = false, alert = "Không thể xóa vì đã sử dụng trong câu hỏi." });

                            }
                            dbConn.Delete<CRM_Survey_Category>(where: "CategoryID={0}".Params(item));
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { success = false, alert = ex.Message });
                    }
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, alert = "Không có quyền xóa bỏ" });
                }
        }
        public ActionResult ExportCategory([DataSourceRequest] DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                if (asset.Export)
                {
                    var data = dbConn.Select<CRM_Survey_Category>();
                    IEnumerable datas = data.ToDataSourceResult(request).Data;

                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\CRM_SurveyCategory.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);
                    ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["DC_SurveyCategory"];
                    int rowData = 1;
                    foreach (CRM_Survey_Category item in datas)
                    {
                        int i = 1;
                        rowData++;
                        dataSheet.Cells[rowData, i++].Value = item.CategoryID;
                        dataSheet.Cells[rowData, i++].Value = item.Name;
                        dataSheet.Cells[rowData, i++].Value = item.Description;
                        dataSheet.Cells[rowData, i++].Value = item.ParentCategoryID;
                        dataSheet.Cells[rowData, i++].Value = item.Active;
                        //if (item.CreatedAt.ToString("dd/MM/yyyy") != "01/01/1900")
                        //{
                            dataSheet.Cells[rowData, i++].Value = item.CreatedAt.ToString();
                        //}
                        //else
                        //{
                            dataSheet.Cells[rowData, i++].Value = "";
                        //}
                        dataSheet.Cells[rowData, i++].Value = item.CreatedBy;
                        //if (item.UpdatedAt.ToString("dd/MM/yyyy") != "01/01/1900")
                        //{
                            dataSheet.Cells[rowData, i++].Value = item.UpdatedAt.ToString();
                        //}
                        //else
                        //{
                            dataSheet.Cells[rowData, i++].Value = "";
                        //}
                        dataSheet.Cells[rowData, i++].Value = item.UpdatedBy;
                    }
                    var dataCategory = dbConn.Select<CRM_Survey_Category>("SELECT * FROM CRM_Survey_Category");
                    //IEnumerable dataCategorys = dataCategory.ToDataSourceResult(request).Data;
                    ExcelWorksheet SheetParentCategory = excelPkg.Workbook.Worksheets["ParentCategory"];
                    int rowParentCategory = 1;
                    foreach (CRM_Survey_Category item1 in dataCategory)
                    {
                        int i = 1;
                        rowParentCategory++;
                        SheetParentCategory.Cells[rowParentCategory, i++].Value = item1.Name;
                    }
                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    string fileName = "CRM_SurveyCategory_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    output.Position = 0;
                    return File(output.ToArray(), contentType, fileName);

                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to export data");
                    return File("", //The binary data of the XLS file
                        "application/vnd.ms-excel", //MIME type of Excel files
                        "CRM_SurveyCategory_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
                }
        }
        public ActionResult ImportCategory()
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
                            FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\CRM_SurveyCategory.xlsx"));
                            template.CopyTo(errorFileLocation);
                            FileInfo _fileInfo = new FileInfo(errorFileLocation);
                            var _excelPkg = new ExcelPackage(_fileInfo);
                            ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["CRM_SurveyCategory"];
                            ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["CRM_SurveyCategory"];
                            int totalRows = oSheet.Dimension.End.Row;
                            for (int i = 2; i <= totalRows; i++)
                            {
                                string Id = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                                string Name = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                                string Description = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                                string Parent = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "";
                                string Active = oSheet.Cells[i, 5].Value != null ? oSheet.Cells[i, 5].Value.ToString() : "TRUE";
                                try
                                {
                                    var data = new CRM_Survey_Category();
                                    var check = dbConn.Select<CRM_Survey_Category>("SELECT * FROM CRM_Survey_Category WHERE CategoryID = '" + Id + "'").FirstOrDefault();
                                    var parentId = dbConn.Select<CRM_Survey_Category>("SELECT * FROM CRM_Survey_Category WHERE Name = N'" + Parent + "'").FirstOrDefault();
                                    if (String.IsNullOrEmpty(Name))
                                    {
                                        eSheet.Cells[rownumber, 1].Value = Id;
                                        eSheet.Cells[rownumber, 2].Value = Name;
                                        eSheet.Cells[rownumber, 3].Value = Description;
                                        eSheet.Cells[rownumber, 8].Value = "Name not null.";
                                        rownumber++;
                                    }
                                    else
                                    {
                                        if (check != null)
                                        {
                                            check.CategoryID = Id;
                                            check.Name = Name;
                                            check.Description = Description;
                                            check.ParentCategoryID = parentId.CategoryID;
                                            check.UpdatedAt = DateTime.Now;
                                            check.UpdatedBy = currentUser.UserName;
                                            check.Active = Boolean.Parse(Active);
                                            dbConn.Update(check);
                                        }
                                        else
                                        {
                                            string id = "";
                                            var checkID = dbConn.Select<CRM_Survey_Category>("SELECT CategoryID, Id FROM dbo.CRM_Survey_Category ORDER BY Id DESC").FirstOrDefault();
                                            if (checkID != null)
                                            {
                                                var nextNo = Int64.Parse(checkID.CategoryID.Substring(4, checkID.CategoryID.Length - 4)) + 1;
                                                id = "SCAT" + String.Format("{0:00000}", nextNo);
                                            }
                                            else
                                            {
                                                id = "SCAT00001";
                                            }
                                            data.CategoryID = id;
                                            data.Description = Description;
                                            data.Name = Name.Trim();
                                            data.Active = Boolean.Parse(Active);
                                            data.ParentCategoryID = "";
                                            data.CreatedAt = DateTime.Now;
                                            data.CreatedBy = currentUser.UserName;
                                            data.UpdatedAt = DateTime.Parse("1900-01-01");
                                            data.UpdatedBy = "";
                                            dbConn.Save(data);
                                        }
                                        total++;
                                    }
                                }
                                catch (Exception e)
                                {
                                    eSheet.Cells[rownumber, 1].Value = Id;
                                    eSheet.Cells[rownumber, 2].Value = Name;
                                    eSheet.Cells[rownumber, 3].Value = Description;
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
        public ActionResult Question_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    return Json(KendoApplyFilter.KendoData<CRM_Survey_Question>(request));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Question_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Survey_Question> list)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Create)
                {
                    try
                    {
                        if (list != null && ModelState.IsValid)
                        {
                            var data = new CRM_Survey_Question();
                            foreach (var item in list)
                            {
                                string id = "";
                                if (String.IsNullOrEmpty(item.Description))
                                {
                                    ModelState.AddModelError("", "Please input question.");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                if (String.IsNullOrEmpty(item.Type))
                                {
                                    ModelState.AddModelError("", "Please input answer type.");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                if (String.IsNullOrEmpty(item.CategoryID))
                                {
                                    ModelState.AddModelError("", "Please input Category type.");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                var checkID = dbConn.Select<CRM_Survey_Question>("SELECT QuestionID, Id FROM dbo.CRM_Survey_Question ORDER BY Id DESC").FirstOrDefault();
                                if (checkID != null)
                                {
                                    var nextNo = Int64.Parse(checkID.QuestionID.Substring(3, checkID.QuestionID.Length - 3)) + 1;
                                    id = "QTL" + String.Format("{0:00000}", nextNo);
                                }
                                else
                                {
                                    id = "QTL00001";
                                }
                                var checkColumName = dbConn.Select<CRM_Survey_Question>("SELECT [Description], Id FROM dbo.CRM_Survey_Question WHERE [Description] = N'" + item.Description + "' AND [Type] = '" + item.Type + "' AND CategoryID ='" + item.CategoryID + "'").FirstOrDefault();
                                if (checkColumName != null)
                                {
                                    ModelState.AddModelError("", "Question is exists");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }

                                data.QuestionID = id;
                                data.Description = !string.IsNullOrEmpty(item.Description) ? item.Description : "";
                                data.Type = !string.IsNullOrEmpty(item.Type) ? item.Type : "";
                                data.CategoryID = !string.IsNullOrEmpty(item.CategoryID) ? item.CategoryID : "";
                                data.Active = item.Active;

                                data.CreatedAt = DateTime.Now;
                                data.CreatedBy = currentUser.UserName;
                                data.UpdatedAt = DateTime.Parse("1900-01-01");
                                data.UpdatedBy = "";
                                dbConn.Save(data);
                            }
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                    return Json(new { sussess = true });
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to create record");
                    dbTrans.Rollback();
                    return Json(list.ToDataSourceResult(request, ModelState));
                }
        }
        public ActionResult Question_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Survey_Question> list)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Update)
                {
                    try
                    {
                        if (list != null && ModelState.IsValid)
                        {
                            foreach (var item in list)
                            {
                                if (String.IsNullOrEmpty(item.Description))
                                {
                                    ModelState.AddModelError("", "Please input description");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }

                                item.Type = !string.IsNullOrEmpty(item.Type) ? item.Type : "";
                                item.Description = !string.IsNullOrEmpty(item.Description) ? item.Description : "";
                                item.CategoryID = !string.IsNullOrEmpty(item.CategoryID) ? item.CategoryID : "";
                                item.Active = item.Active;
                                item.UpdatedAt = DateTime.Now;
                                item.UpdatedBy = currentUser.UserName;
                                dbConn.Update(item);
                                var checkConfig = dbConn.Select<CRM_Survey_AnswerConfig>("select Id from CRM_Survey_AnswerConfig where QuestionID = '" + item.QuestionID + "'").FirstOrDefault();
                                if (checkConfig != null && item.Type != "Multi choice")
                                {
                                    dbConn.Delete<CRM_Survey_AnswerConfig>(where: "QuestionID={0}".Params(item.QuestionID));
                                }
                                else if (checkConfig == null && item.Type == "Multi choice")
                                {
                                    var data2 = new CRM_Survey_AnswerConfig();
                                    data2.QuestionID = item.QuestionID;
                                    //data2.AnswerID = id;
                                    data2.Description = "Limit Choices Quantity";
                                    data2.Detail = 0;
                                    data2.Active = true;
                                    data2.CreatedAt = DateTime.Now;
                                    data2.CreatedBy = currentUser.UserName;
                                    data2.UpdatedAt = DateTime.Parse("1900-01-01");
                                    data2.UpdatedBy = "";
                                    dbConn.Save(data2);
                                }
                            }
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                    return Json(new { sussess = true });
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to update record");
                    dbTrans.Rollback();
                    return Json(list.ToDataSourceResult(request, ModelState));
                }
        }
        public ActionResult DeleteQuestion(string data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Delete)
                {
                    try
                    {
                        string[] separators = { "@@" };
                        var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listdata)
                        {
                            var checkexits = dbConn.GetFirstColumn<string>("SELECT QuestionID FROM CRM_Survey_Management_Question WHERE QuestionID= '" + item + "'");
                            if (checkexits.Count > 0)
                            {
                                dbTrans.Rollback();
                                return Json(new { success = false, alert = "Câu hỏi này không thể xóa vì đã sử dụng trong survey." });
                            }
                            dbConn.Delete<CRM_Survey_AnswerList>(where: "QuestionID={0}".Params(item));
                            dbConn.Delete<CRM_Survey_AnswerConfig>(where: "QuestionID={0}".Params(item));
                            dbConn.Delete<CRM_Survey_Question>(where: "QuestionID={0}".Params(item));
                        }
                        dbTrans.Commit();
                        return Json(new { success = true });
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { success = false, alert = ex.Message });
                    }
                }
                else
                {
                    return Json(new { success = false, alert = "You don't have permission to delete record" });
                }
        }
        public ActionResult ExportQuestion([DataSourceRequest] DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                if (asset.Export)
                {
                    var data = dbConn.Select<CRM_Survey_Question>("SELECT q.*, c.Name FROM CRM_Survey_Question q left join CRM_Survey_Category c on q.CategoryID = c.CategoryID");
                    IEnumerable datas = data.ToDataSourceResult(request).Data;

                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\CRM_SurveyQuestion.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);
                    ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["CRM_SurveyQuestion"];
                    int rowData = 1;
                    foreach (CRM_Survey_Question item in datas)
                    {
                        int i = 1;
                        rowData++;
                        dataSheet.Cells[rowData, i++].Value = item.QuestionID;
                        dataSheet.Cells[rowData, i++].Value = item.Description;
                        dataSheet.Cells[rowData, i++].Value = item.Type;
                        if (!String.IsNullOrEmpty(item.CategoryID))
                        {
                            dataSheet.Cells[rowData, i++].Value = item.Name + "-" + item.CategoryID;
                        }
                        else
                        {
                            dataSheet.Cells[rowData, i++].Value = "";
                        }
                        dataSheet.Cells[rowData, i++].Value = item.Active;
                        //if (item.CreatedAt.ToString("dd/MM/yyyy") != "01/01/1900")
                        //{
                            dataSheet.Cells[rowData, i++].Value = item.CreatedAt.ToString();
                        //}
                        //else
                        //{
                            dataSheet.Cells[rowData, i++].Value = "";
                        //}
                        dataSheet.Cells[rowData, i++].Value = item.CreatedBy;
                        //if (item.UpdatedAt.ToString("dd/MM/yyyy") != "01/01/1900")
                        //{
                            dataSheet.Cells[rowData, i++].Value = item.UpdatedAt.ToString();
                        //}
                        //else
                        //{
                            dataSheet.Cells[rowData, i++].Value = "";
                        //}
                        dataSheet.Cells[rowData, i++].Value = item.UpdatedBy;
                    }
                    var dataCategory = dbConn.Select<CRM_Survey_Category>("SELECT * FROM CRM_Survey_Category");
                    //IEnumerable dataCategorys = dataCategory.ToDataSourceResult(request).Data;
                    ExcelWorksheet SheetParentCategory = excelPkg.Workbook.Worksheets["Category"];
                    int rowParentCategory = 1;
                    foreach (CRM_Survey_Category item1 in dataCategory)
                    {
                        int i = 1;
                        rowParentCategory++;
                        SheetParentCategory.Cells[rowParentCategory, i++].Value = item1.Name + "-" + item1.CategoryID;
                    }
                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    string fileName = "CRM_SurveyQuestion_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    output.Position = 0;
                    return File(output.ToArray(), contentType, fileName);

                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to export data");
                    return File("", //The binary data of the XLS file
                        "application/vnd.ms-excel", //MIME type of Excel files
                        "CRM_SurveyQuestion_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
                }
        }
        public ActionResult ImportQuestion()
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
                            FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\CRM_SurveyQuestion.xlsx"));
                            template.CopyTo(errorFileLocation);
                            FileInfo _fileInfo = new FileInfo(errorFileLocation);
                            var _excelPkg = new ExcelPackage(_fileInfo);
                            ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["CRM_SurveyQuestion"];
                            ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["CRM_SurveyQuestion"];
                            int totalRows = oSheet.Dimension.End.Row;
                            for (int i = 2; i <= totalRows; i++)
                            {
                                string Id = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                                string Question = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                                string Type = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                                string Category = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "";
                                string[] CategoryID = Category.Split('-');
                                string Active = oSheet.Cells[i, 5].Value != null ? oSheet.Cells[i, 5].Value.ToString() : "TRUE";
                                try
                                {
                                    var data = new CRM_Survey_Question();
                                    var check = dbConn.Select<CRM_Survey_Question>("SELECT * FROM CRM_Survey_Question WHERE QuestionID = '" + Id + "'").FirstOrDefault();
                                    //var parentId = dbConn.Select<CRM_Survey_Category>("SELECT * FROM CRM_Survey_Category WHERE Name = N'" + Parent + "'").FirstOrDefault();
                                    if (String.IsNullOrEmpty(Question))
                                    {
                                        eSheet.Cells[rownumber, 1].Value = Id;
                                        eSheet.Cells[rownumber, 2].Value = Question;
                                        eSheet.Cells[rownumber, 3].Value = Type;
                                        eSheet.Cells[rownumber, 4].Value = Category;
                                        eSheet.Cells[rownumber, 10].Value = "Name not null.";
                                        rownumber++;
                                    }
                                    else
                                    {
                                        if (check != null)
                                        {
                                            check.CategoryID = Id;
                                            check.Description = Question;
                                            check.Type = Type;
                                            check.CategoryID = CategoryID[CategoryID.Count() - 1] != "" ? CategoryID[CategoryID.Count() - 1].Trim() : "";
                                            check.UpdatedAt = DateTime.Now;
                                            check.UpdatedBy = currentUser.UserName;
                                            check.Active = Boolean.Parse(Active);
                                            dbConn.Update(check);
                                        }
                                        else
                                        {
                                            string id = "";
                                            var checkID = dbConn.Select<CRM_Survey_Question>("SELECT QuestionID, Id FROM dbo.CRM_Survey_Question ORDER BY Id DESC").FirstOrDefault();
                                            if (checkID != null)
                                            {
                                                var nextNo = Int64.Parse(checkID.QuestionID.Substring(3, checkID.QuestionID.Length - 3)) + 1;
                                                id = "QTL" + String.Format("{0:00000}", nextNo);
                                            }
                                            else
                                            {
                                                id = "QTL00001";
                                            }
                                            data.CategoryID = id;
                                            data.Description = Question;
                                            data.Type = Type;
                                            data.Active = Boolean.Parse(Active);
                                            data.CategoryID = CategoryID[CategoryID.Count() - 1] != "" ? CategoryID[CategoryID.Count() - 1].Trim() : "";
                                            data.CreatedAt = DateTime.Now;
                                            data.CreatedBy = currentUser.UserName;
                                            data.UpdatedAt = DateTime.Parse("1900-01-01");
                                            data.UpdatedBy = "";
                                            dbConn.Save(data);
                                        }
                                        total++;
                                    }
                                }
                                catch (Exception e)
                                {
                                    eSheet.Cells[rownumber, 1].Value = Id;
                                    eSheet.Cells[rownumber, 2].Value = Question;
                                    eSheet.Cells[rownumber, 3].Value = Type;
                                    eSheet.Cells[rownumber, 4].Value = Category;
                                    eSheet.Cells[rownumber, 10].Value = e.Message;
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
        public ActionResult AnswerList_Read([DataSourceRequest] DataSourceRequest request, string QuestionID)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            return Json(KendoApplyFilter.KendoData<CRM_Survey_AnswerList>(request, "QuestionID='" + QuestionID + "'"));
        }
        public ActionResult AnswerList_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<CRM_Survey_AnswerList> list, string QuestionID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Create)
                {
                    try
                    {
                        if (list != null && ModelState.IsValid)
                        {
                            var data2 = new CRM_Survey_AnswerConfig();
                            var checkType = dbConn.Select<CRM_Survey_Question>("select [Type] from CRM_Survey_Question where QuestionID ='" + QuestionID + "'").FirstOrDefault();
                            var checkConfig = dbConn.Select<CRM_Survey_AnswerConfig>("select * from CRM_Survey_AnswerConfig where QuestionID = '" + QuestionID + "'").FirstOrDefault();
                            var data = new CRM_Survey_AnswerList();
                            foreach (var item in list)
                            {
                                string id = "";
                                if (String.IsNullOrEmpty(item.AnswerDescription))
                                {
                                    ModelState.AddModelError("", "Please input Answer description.");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }

                                var checkID = dbConn.Select<CRM_Survey_AnswerList>("SELECT AnswerID, Id FROM dbo.CRM_Survey_AnswerList ORDER BY Id DESC").FirstOrDefault();
                                if (checkID != null)
                                {
                                    var nextNo = Int64.Parse(checkID.AnswerID.Substring(2, checkID.AnswerID.Length - 2)) + 1;
                                    id = "AS" + String.Format("{0:00000}", nextNo);
                                }
                                else
                                {
                                    id = "AS00001";
                                }
                                //var checkColumName = dbConn.Select<CRM_Survey_Question>("SELECT [Description] FROM dbo.CRM_Survey_Question WHERE [Description] = '" + item.Description + "'").FirstOrDefault();
                                //if (checkColumName != null)
                                //{
                                //    ModelState.AddModelError("", "Question is exists");
                                //    return Json(list.ToDataSourceResult(request, ModelState));
                                //}

                                data.QuestionID = QuestionID;
                                data.AnswerID = id;
                                data.Content = !string.IsNullOrEmpty(item.Content) ? item.Content : "";
                                data.AnswerDescription = !string.IsNullOrEmpty(item.AnswerDescription) ? item.AnswerDescription : "";
                                data.Answer = !string.IsNullOrEmpty(item.Answer) ? item.Answer : "No";
                                data.Active = item.Active;
                                data.CreatedAt = DateTime.Now;
                                data.CreatedBy = currentUser.UserName;
                                data.UpdatedAt = DateTime.Parse("1900-01-01");
                                data.UpdatedBy = "";
                                dbConn.Save(data);
                                if (checkConfig == null && checkType.Type == "Multi choice")
                                {
                                    data2.QuestionID = QuestionID;
                                    data2.AnswerID = id;
                                    data2.Description = "Limit Choices Quantity";
                                    data2.Detail = 0;
                                    data2.Active = true;
                                    data2.CreatedAt = DateTime.Now;
                                    data2.CreatedBy = currentUser.UserName;
                                    data2.UpdatedAt = DateTime.Parse("1900-01-01");
                                    data2.UpdatedBy = "";
                                    dbConn.Save(data2);
                                }
                            }
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                    return Json(new { sussess = true });
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to create record");
                    dbTrans.Rollback();
                    return Json(list.ToDataSourceResult(request, ModelState));
                }
        }
        public ActionResult AnswerList_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Survey_AnswerList> list, string QuestionID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Update)
                {
                    try
                    {
                        if (list != null && ModelState.IsValid)
                        {
                            foreach (var item in list)
                            {
                                if (String.IsNullOrEmpty(item.Answer))
                                {
                                    ModelState.AddModelError("", "Please input answer");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                item.Answer = !string.IsNullOrEmpty(item.Answer) ? item.Answer : "";
                                item.QuestionID = QuestionID;
                                item.Content = !string.IsNullOrEmpty(item.Content) ? item.Content : "";
                                item.Active = item.Active;
                                item.UpdatedAt = DateTime.Now;
                                item.UpdatedBy = currentUser.UserName;
                                dbConn.Update(item);
                            }
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                    return Json(new { sussess = true });
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to update record");
                    dbTrans.Rollback();
                    return Json(list.ToDataSourceResult(request, ModelState));
                }
        }
        public ActionResult DeleteAnswer(string data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Delete)
                {
                    try
                    {
                        string[] separators = { "@@" };
                        var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        //check
                        var currentQuestionID = dbConn.Scalar<string>("SELECT QuestionID FROM CRM_Survey_AnswerList WHERE AnswerID= {0}", listdata[0]);
                        var checkexits = dbConn.GetFirstColumn<string>("SELECT QuestionID FROM CRM_Survey_Management_Question WHERE QuestionID= '" + currentQuestionID + "'");
                        if (checkexits.Count > 0)
                        {
                            dbTrans.Rollback();
                            return Json(new { success = false, alert = "Câu trả lời này không thể xóa vì đã sử dụng trong survey." });
                        }
                        foreach (var item in listdata)
                        {
                            dbConn.Delete<CRM_Survey_AnswerList>(where: "AnswerID={0}".Params(item));
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { success = false, alert = ex.Message });
                    }
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, alert = "You don't have permission to delete record" });
                }
        }
        public ActionResult Configuration_Read([DataSourceRequest] DataSourceRequest request, string QuestionID)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            return Json(KendoApplyFilter.KendoData<CRM_Survey_AnswerConfig>(request, "QuestionID='" + QuestionID + "'"));
        }
        public ActionResult Configuration_Update([DataSourceRequest] DataSourceRequest request, CRM_Survey_AnswerConfig config, string QuestionID)
        {
            IEnumerable<CRM_Survey_AnswerConfig> u = new CRM_Survey_AnswerConfig[] { };
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Update)
                {
                    try
                    {
                        var check = dbConn.Select<CRM_Survey_AnswerList>("select * from CRM_Survey_AnswerList where QuestionID='" + QuestionID + "'");
                        if (config.Detail > check.Count())
                        {
                            ModelState.AddModelError("", "Input detail error.");
                            dbTrans.Rollback();
                            return Json(u.ToDataSourceResult(request, ModelState));
                        }
                        dbConn.Update<CRM_Survey_AnswerConfig>(set: "Detail={0}, Active ={1}, UpdatedBy={2}, UpdatedAt={3}".Params(config.Detail, config.Active, currentUser.UserName, DateTime.Now), where: "AnswerID={0}".Params(config.AnswerID));
                        dbTrans.Commit();
                    }

                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(u.ToDataSourceResult(request, ModelState));
                    }
                    return Json(new { sussess = true });
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to update record");
                    dbTrans.Rollback();
                    return Json(u.ToDataSourceResult(request, ModelState));
                }
        }
        public ActionResult GetCategory()
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            var data = dbConn.Select<CRM_Survey_Category>("select * from CRM_Survey_Category");
            return Json(data);
        }
    }
}
