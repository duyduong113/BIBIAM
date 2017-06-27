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
    public class FAQTopicController : CustomController
    {
        //
        // GET: /FAQTopic/
        public ActionResult Index()
        {
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
                    data = KendoApplyFilter.KendoData<Deca_Topic>(request);
                }
                return Json(data);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_Topic> items)
        {
            if (asset.Create)
            {
                if (items.Count() > 0)
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
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_Topic> items)
        {
            if (asset.Update)
            {
                if (items.Count() > 0)
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
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Topic.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_Topic" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    var data = new List<Deca_Topic>();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        data = dbConn.Select<Deca_Topic>(where).ToList();
                    }
                    else
                    {
                        data = dbConn.Select<Deca_Topic>().ToList();
                    }


                    ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["FAQTopic"];

                    int rowData = 1;

                    foreach (var item in data)
                    {
                        int i = 1;
                        rowData++;
                        dataSheet.Cells[rowData, i++].Value = item.Name;
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

                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Topic.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);

                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["FAQTopic"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["FAQTopic"];

                        var data = WorksheetToDataTables(oSheet);

                        var listData = data.AsEnumerable().Select(s => new Deca_Topic
                        {
                            Name = s["Name"] != null ? s["Name"].ToString() : "",
                            Description = s["Description"] != null ? s["Description"].ToString() : "",
                        }).ToList();

                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {
                            if (n == 1)
                            {
                                var required = listData.Where(s =>
                                        String.IsNullOrEmpty(s.Name) ||
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
                                    var newData = new Deca_Topic();
                                    newData.Name = item.Name;
                                    newData.Description = item.Description;
                                    newData.CreatedAt = DateTime.Now;
                                    newData.CreatedBy = currentUser.UserName;
                                    dbConn.Insert(newData);
                                    total++;
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

        public void writeErrorToFile(ExcelWorksheet eSheet, int rownumber, Deca_Topic item, string error)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {

                int i = 1;
                eSheet.Cells[rownumber, i++].Value = item.Name;
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
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Topic.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_Topic" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["FAQTopic"];

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