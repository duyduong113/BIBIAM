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
using System.IO;
using OfficeOpenXml;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class Utilities_ParametersController : CustomController
    {
        //
        // GET: /Organization/
        public ActionResult Index()
        {   
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;
                    ViewData["AllowImport"] = asset.Export;
                    ViewData["Asset"] = asset;
                }
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            string whereCondition = "";
            if (request.Filters.Count > 0)
            {
                whereCondition =  KendoApplyFilter.ApplyFilter(request.Filters[0],"");
            }
            var data = dbConn.Select<Parameters>(whereCondition);
            return Json(data.ToDataSourceResult(request));
        }
        //
        // GET: /DeliveryManage/Create
        public ActionResult Create(Parameters item)
        {
            IDbConnection db = Helpers.OrmliteConnection.openConn();
            try
            {
                if (!string.IsNullOrEmpty(item.Type) && !string.IsNullOrEmpty(item.Value) && !string.IsNullOrEmpty(item.ParamID))
                {
                    var isExistName = db.SingleOrDefault<Parameters>("Select * from Parameters Where ParamID='" + item.ParamID + "'");
                    if (isExistName == null)
                    {
                        if (asset.Create && item.CreatedAt == null && item.CreatedBy == null)
                        {
                            //if (isExist != null)
                            //    return Json(new { success = false, message = "Mã lý do đã tồn tại!" });
                            item.ParamID = !string.IsNullOrEmpty(item.ParamID) ? item.ParamID : "";
                            item.Value = !string.IsNullOrEmpty(item.Value) ? item.Value : "";
                            item.Type = !string.IsNullOrEmpty(item.Type) ? item.Type : "";
                            item.Descr = !string.IsNullOrEmpty(item.Descr) ? item.Descr : "";
                            item.CreatedAt = DateTime.Now;
                            item.Status = (item.Status != false) ? true : false;
                            item.CreatedBy = currentUser.UserName;
                            item.UpdatedAt = DateTime.Parse("1900-01-01");
                            item.UpdatedBy = "";
                            db.Insert<Parameters>(item);
                            long lastInsertId = db.GetLastInsertId();
                            return Json(new { success = true, ID = lastInsertId, CreatedBy = item.CreatedBy, CreatedAt = item.CreatedAt });
                        }
                        else if (asset.Update)
                        {
                            item.ParamID = !string.IsNullOrEmpty(item.ParamID) ? item.ParamID : "";
                            item.Value = !string.IsNullOrEmpty(item.Value) ? item.Value : "";
                            item.Type = !string.IsNullOrEmpty(item.Type) ? item.Type : "";
                            item.Descr = !string.IsNullOrEmpty(item.Descr) ? item.Descr : "";
                            item.UpdatedAt = DateTime.Now;
                            item.Status = (item.Status != false) ? true : false;
                            item.UpdatedBy = currentUser.UserName;
                            db.Update<Parameters>(item);
                            return Json(new { success = true });
                        }
                        else
                            return Json(new { success = false, message = "Bạn không có quyền" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Mã tham số đã tồn tại!" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Chưa nhập giá trị" });
                }
            }
            catch (Exception e)
            {
                //log.Error("Parameter - Create - " + e.Message);
                return Json(new { success = false, message = e.Message });
            }
            finally { db.Close(); }
        }
      
        public ActionResult UpdateDetail([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<Parameters> list)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            if (asset.Update)
            {
                if (list != null)
                {
                    try
                    {
                        foreach (var item in list)
                        {

                            if (dbConn.Select<Parameters>(s => s.ID == item.ID).Count() > 0)
                            {
                                dbConn.Update<Parameters>(set: "ParamID = {0}, Type = {1}, Value = {2}, Descr = {3}, UpdatedBy = {4}, UpdatedAt = {5}, Status = {6} "
                                    .Params(item.ParamID,item.Type,item.Value,item.Descr,item.UpdatedBy,currentUser.UserName,item.Status)
                                    , where: "ID = {0}".Params(item.ID));
                            }
                            else
                            {
                                var data = new Parameters();
                                data.ParamID = item.ParamID;
                                data.Value = item.Value;
                                data.Type = item.Type;
                                data.Descr = item.Descr;
                                data.Status = (item.Status != false) ? true : false;
                                data.CreatedAt = DateTime.Now;
                                data.CreatedBy = currentUser.UserName;
                                data.UpdatedAt = DateTime.Parse("1900-01-01");
                                data.UpdatedBy = "";
                                dbConn.Insert<Parameters>(data);
                            }
                        }
                        ModelState.AddModelError("Thành công!", "Lưu thành công.");
                        return Json(new { sussess = true });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("error", ex.Message);
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                }
                return Json(new { sussess = true });
            }
            else if (asset.Create)
            {
                foreach (var item in list)
                {
                    var isExist = dbConn.GetByIdOrDefault<Parameters>(item.ID);
                    if (isExist == null)
                    {
                        var data = new Parameters();
                        data.ParamID = item.ParamID;
                        data.Value = item.Value;
                        data.Type = item.Type;
                        data.Status = (item.Status != false) ? true : false;
                        data.Descr = item.Descr;
                        data.CreatedAt = DateTime.Now;
                        data.CreatedBy = currentUser.UserName;
                        data.UpdatedAt = DateTime.Parse("1900-01-01");
                        data.UpdatedBy = "";
                        dbConn.Insert<Parameters>(data);
                    }
                }
                ModelState.AddModelError("Thành công!", "Lưu thành công.");
                return Json(new { sussess = true });
            }
            else
            {
                ModelState.AddModelError("error", "Bạn không có quyền cập nhật.");
                return Json(list.ToDataSourceResult(request, ModelState));
            }
        }
        //Xuanph - add - 24/12/2015
        public ActionResult ImportData()
        {
            IDbConnection dbConn = Helpers.OrmliteConnection.openConn();
            try
            {
                if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                {
                    string fileExtension =
                        System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                    if (fileExtension == ".xlsx" || fileExtension == ".xls")
                    {


                        string datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/ExcelImport"), "[" + currentUser.UserName + "-" + datetime + Request.Files["FileUpload"].FileName);
                        string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/ExcelImport"), "[" + currentUser.UserName + "-" + datetime + "-Error]" + Request.Files["FileUpload"].FileName);
                        string linkerror = "[" + currentUser.UserName + "-" + datetime + "-Error]" + Request.Files["FileUpload"].FileName;

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);

                        Request.Files["FileUpload"].SaveAs(fileLocation);

                        var rownumber = 2;
                        var total = 0;
                        FileInfo fileInfo = new FileInfo(fileLocation);
                        var excelPkg = new ExcelPackage(fileInfo);
                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportTemplate\CauHinhThamSo.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);
                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["Data"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["Data"];
                        int totalRows = oSheet.Dimension.End.Row;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            String Paramid = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            String Type = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            String Value = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                            String Descr = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "";
                            try
                            {
                                if (string.IsNullOrEmpty(Paramid) || string.IsNullOrEmpty(Type) || string.IsNullOrEmpty(Value))
                                {
                                    eSheet.Cells[rownumber, 1].Value = Paramid;
                                    eSheet.Cells[rownumber, 2].Value = Type;
                                    eSheet.Cells[rownumber, 3].Value = Value;
                                    eSheet.Cells[rownumber, 4].Value = Descr;
                                    eSheet.Cells[rownumber, 5].Value = "Vui lòng nhập (*).";
                                    rownumber++;
                                }
                                else
                                {
                                    var checkID = dbConn.SingleOrDefault<Parameters>("SELECT id,ParamID,CreatedAt,CreatedBy FROM dbo.Parameters where ParamID = '" + Paramid + "' ORDER BY ParamID DESC");
                                    var item = new Parameters();

                                    item.ParamID = Paramid;
                                    item.Type = Type;
                                    item.Value = Value;
                                    item.Descr = Descr;

                                    if (checkID == null)
                                    {
                                        item.CreatedAt = DateTime.Now;
                                        item.CreatedBy = currentUser.UserName;
                                        item.UpdatedAt = DateTime.Parse("1900-01-01");
                                        item.UpdatedBy = "";
                                        dbConn.Insert<Parameters>(item);
                                    }
                                    else
                                    {
                                        item.ID = checkID.ID;
                                        item.UpdatedAt = DateTime.Now;
                                        item.UpdatedBy = currentUser.UserName;
                                        item.CreatedBy = checkID.CreatedBy;
                                        item.CreatedAt = checkID.CreatedAt;
                                        dbConn.Update<Parameters>(item);
                                    }
                                    total++;
                                }
                            }
                            catch (Exception e)
                            {
                                eSheet.Cells[rownumber, 1].Value = Paramid;
                                eSheet.Cells[rownumber, 2].Value = Type;
                                eSheet.Cells[rownumber, 3].Value = Value;
                                eSheet.Cells[rownumber, 4].Value = Descr;
                                eSheet.Cells[rownumber, 5].Value = e.Message;
                                rownumber++;
                                continue;
                            }
                        }
                        _excelPkg.Save();
                        return Json(new { success = true, total = total, totalError = rownumber - 2, link = linkerror });
                    }

                    else
                    {
                        return Json(new { success = false, message = "Không phải là file Excel. *.xlsx" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Không có file hoặc file không phải là Excel" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }
        // xuanph - add - 24/12/2015
        public ActionResult ExportTemplate([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportTemplate\CauHinhThamSo.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);
                    string fileName = "CauHinhThamSo_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
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
        public ActionResult DeleteItem(string data)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    var detail = new Parameters();
                    foreach (var item in listdata)
                    {
                        if (dbConn.Select<Parameters>(s => s.ID == int.Parse(item)).Count() > 0)
                        {
                            var success = dbConn.Delete<Parameters>(where: "ID = '" + item + "'") >= 1;

                            if (!success)
                            {
                                return Json(new { success = false, message = "Không thể lưu" });
                            }
                        }
                    }
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
            else
            {
                return Json(new { success = false, message = "Không có quyền xóa." });
            }
        }
        public ActionResult GetByTypeRead(string Type)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Parameters>("Type='"+Type+"'").OrderBy(s => s.ParamID);
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }
    }
}