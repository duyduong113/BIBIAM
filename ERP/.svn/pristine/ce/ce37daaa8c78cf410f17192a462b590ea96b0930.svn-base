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
using System.Collections;
using System.Data.OleDb;
using NPOI.HSSF.UserModel;
using System.Dynamic;
using System.Text.RegularExpressions;

namespace ERPAPD.Controllers
{
    public class DepartmentsController : CustomController
    {
        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                ViewData["Asset"] = asset;
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.listOwner = dbConn.Select<Users>();
                    ViewData["UserGroups"] = dbConn.Select<Groups>();
                }
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        #region Read_Action
        public ActionResult Department_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = CRM_Department.GetAllCRM_Departments();
                if (request.Filters == null || request.Filters.Count == 0)
                {
                    //data = data.Where(s => s.Status == "True").ToList();
                }
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        //public ActionResult Team_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    if (asset.View)
        //    {
        //        var data = Deca_Team.GetAllDeca_Teams();
        //        if (request.Filters == null || request.Filters.Count == 0)
        //        {
        //            //data = data.Where(s => s.Status == "True").ToList();
        //        }
        //        return Json(data.ToDataSourceResult(request));
        //    }
        //    else
        //    {
        //        return RedirectToAction("NoAccessRights", "Error");
        //    }
        //}

        public ActionResult Position_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = Deca_Position.GetAllDeca_Positions();
                if (request.Filters == null || request.Filters.Count == 0)
                {
                    //data = data.Where(s => s.Status == "True").ToList();
                }
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        #endregion

        #region Create_Action
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Department_Create([DataSourceRequest] DataSourceRequest request,
        [Bind(Prefix = "models")] IEnumerable<CRM_Department> listdepartment)
        {
            IEnumerable<CRM_Department> u = new CRM_Department[] { };
            if (asset.Create)
            {

                if (listdepartment != null && ModelState.IsValid)
                {

                    var w = new CRM_Department();
                    if (w.Department == null)
                    {
                        w.Department = "";
                    }
                    foreach (var department in listdepartment)
                    {
                        w.Department = department.Department;
                        w.Active = department.Active;
                        w.Manager = department.Manager;
                        w.CreatedDatetime = DateTime.Now;
                        w.CreatedUser = currentUser.UserName;
                        w.LastUpdatedUser = "";
                        w.LastUpdatedDateTime = DateTime.Parse("1900-01-01");
                        w.Save();
                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "Model not valid");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Team_Create([DataSourceRequest] DataSourceRequest request,
        //[Bind(Prefix = "models")] IEnumerable<Deca_Team> listTeam)
        //{
        //    IEnumerable<Deca_Team> u = new Deca_Team[] { };
        //    if (asset.Create)
        //    {
        //        if (listTeam != null && ModelState.IsValid)
        //        {
        //            foreach (var team in listTeam)
        //            {
        //                var _team = Deca_Team.GetDeca_Team(team.TeamID);
        //                if (_team != null)
        //                {
        //                    ModelState.AddModelError("", "TeamID is already exist: " + team.TeamID);
        //                    return Json(listTeam.ToDataSourceResult(request, ModelState));
        //                }

        //                var w = new Deca_Team();
        //                if (w.TeamName == null)
        //                {
        //                    w.TeamName = "";
        //                }
        //                w.TeamName = team.TeamName;
        //                w.Active = team.Active;
        //                w.CreatedDatetime = DateTime.Now;
        //                w.CreatedUser = currentUser.UserName;
        //                w.LastUpdatedUser = "";
        //                w.LastUpdatedDateTime = DateTime.Parse("1900-01-01");
        //                w.Save();
        //            }
        //            return Json(new { success = true });
        //        }
        //        ModelState.AddModelError("", "Model not valid");
        //        return Json(u.ToDataSourceResult(request, ModelState));
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "You don't have permission to create record");
        //        return Json(u.ToDataSourceResult(request, ModelState));
        //    }
        //}

        public ActionResult Position_Create([DataSourceRequest] DataSourceRequest request,
        [Bind(Prefix = "models")] IEnumerable<Deca_Position> listPosition)
        {
            IEnumerable<CRM_Department> u = new CRM_Department[] { };
            if (asset.Create)
            {
                if (listPosition != null && ModelState.IsValid)
                {

                    var w = new Deca_Position();
                    if (w.PositionName == null)
                    {
                        w.PositionName = "";
                    }
                    foreach (var team in listPosition)
                    {
                        w.PositionName = team.PositionName;
                        w.Active = team.Active;
                        w.CreatedDatetime = DateTime.Now;
                        w.CreatedUser = currentUser.UserName;
                        w.LastUpdatedUser = "";
                        w.LastUpdatedDateTime = DateTime.Parse("1900-01-01");
                        w.Save();
                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "Model not valid");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
        }
        #endregion

        #region Update_Action
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Department_Update([DataSourceRequest] DataSourceRequest request,
        [Bind(Prefix = "models")] IEnumerable<CRM_Department> listdepartment)
        {
            IEnumerable<CRM_Department> u = new CRM_Department[] { };
            if (asset.Create)
            {
                if (listdepartment != null && ModelState.IsValid)
                {
                    var w = new CRM_Department();
                    foreach (var department in listdepartment)
                    {
                        var list = CRM_Department.GetCRM_Department(department.DepartmentID);
                        list.LastUpdatedUser = currentUser.UserName;
                        list.LastUpdatedDateTime = DateTime.Now;

                        list.Department = department.Department;
                        list.Active = department.Active;
                        list.Manager = department.Manager;
                        list.Update();
                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "Model not valid");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Team_Update([DataSourceRequest] DataSourceRequest request,
        //[Bind(Prefix = "models")] IEnumerable<Deca_Team> listTeam)
        //{
        //    IEnumerable<Deca_Team> u = new Deca_Team[] { };
        //    if (asset.Create)
        //    {
        //        if (listTeam != null && ModelState.IsValid)
        //        {
        //            var w = new Deca_Team();
        //            foreach (var team in listTeam)
        //            {
        //                var list = Deca_Team.GetDeca_Team(team.TeamID);

        //                list.LastUpdatedUser = currentUser.UserName;
        //                list.LastUpdatedDateTime = DateTime.Now;

        //                list.TeamName = team.TeamName;
        //                list.Active = team.Active;
        //                list.Update();
        //            }
        //            return Json(new { success = true });
        //        }
        //        ModelState.AddModelError("", "Model not valid");
        //        return Json(u.ToDataSourceResult(request, ModelState));
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "You don't have permission to create record");
        //        return Json(u.ToDataSourceResult(request, ModelState));
        //    }
        //}

      //  [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Position_Update([DataSourceRequest] DataSourceRequest request,
        [Bind(Prefix = "models")] IEnumerable<Deca_Position> listPosition)
        {
            IEnumerable<Deca_Position> u = new Deca_Position[] { };
            if (asset.Create)
            {
                if (listPosition != null && ModelState.IsValid)
                {
                    var w = new Deca_Position();
                    foreach (var team in listPosition)
                    {
                        var list = Deca_Position.GetDeca_Position(team.PositionID);
                        list.LastUpdatedUser = currentUser.UserName;
                        list.LastUpdatedDateTime = DateTime.Now;

                        list.PositionName = team.PositionName;
                        list.Active = team.Active;
                        list.Update();
                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "Model not valid");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
        }
        #endregion

        #region Export_Action
        public FileResult Export_Department([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.Export)
            {
                //Get the data representing the current grid state - page, sort and filter
                IEnumerable datas = CRM_Department.GetAllCRM_Departments().ToDataSourceResult(request).Data;
                //Create new Excel workbook
                FileStream fs = new FileStream(Server.MapPath(@"~\ExportExcelFile\CRM_Department.xls"), FileMode.Open, FileAccess.Read);
                var workbook = new HSSFWorkbook(fs, true);

                //Create new Excel sheet
                var sheet = workbook.GetSheet("Department");

                int rowNumber = 1;

                //Populate the sheet with values from the grid data
                foreach (CRM_Department data in datas)
                {
                    //Create a new row
                    var row = sheet.CreateRow(rowNumber++);
                    //Set values for the cells
                    row.CreateCell(0).SetCellValue(data.DepartmentID);
                    row.CreateCell(1).SetCellValue(data.Department);
                    row.CreateCell(2).SetCellValue(data.Active);
                }

                //Write the workbook to a memory stream
                MemoryStream output = new MemoryStream();
                workbook.Write(output);

                //Return the result to the end user
                return File(output.ToArray(), //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "CRM_Department_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to export data");
                return File("", //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "CRM_Department_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
        }

        //public FileResult Export_Team([DataSourceRequest] DataSourceRequest request)
        //{
        //    if (asset.Export)
        //    {
        //        //Get the data representing the current grid state - page, sort and filter
        //        IEnumerable datas = Deca_Team.GetAllDeca_Teams().ToDataSourceResult(request).Data;
        //        //Create new Excel workbook
        //        FileStream fs = new FileStream(Server.MapPath(@"~\ExportExcelFile\Deca_Team.xls"), FileMode.Open, FileAccess.Read);
        //        var workbook = new HSSFWorkbook(fs, true);

        //        //Create new Excel sheet
        //        var sheet = workbook.GetSheet("Team");

        //        int rowNumber = 1;

        //        //Populate the sheet with values from the grid data
        //        foreach (Deca_Team data in datas)
        //        {
        //            //Create a new row
        //            var row = sheet.CreateRow(rowNumber++);
        //            //Set values for the cells
        //            row.CreateCell(0).SetCellValue(data.TeamID);
        //            row.CreateCell(1).SetCellValue(data.TeamName);
        //            row.CreateCell(2).SetCellValue(data.Active);
        //        }

        //        //Write the workbook to a memory stream
        //        MemoryStream output = new MemoryStream();
        //        workbook.Write(output);

        //        //Return the result to the end user
        //        return File(output.ToArray(), //The binary data of the XLS file
        //            "application/vnd.ms-excel", //MIME type of Excel files
        //            "Deca_Team_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "You don't have permission to export data");
        //        return File("", //The binary data of the XLS file
        //            "application/vnd.ms-excel", //MIME type of Excel files
        //            "Deca_Team_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        //    }
        //}

        public FileResult Export_Position([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.Export)
            {
                //Get the data representing the current grid state - page, sort and filter
                IEnumerable datas = Deca_Position.GetAllDeca_Positions().ToDataSourceResult(request).Data;
                //Create new Excel workbook
                FileStream fs = new FileStream(Server.MapPath(@"~\ExportExcelFile\Deca_Position.xls"), FileMode.Open, FileAccess.Read);
                var workbook = new HSSFWorkbook(fs, true);

                //Create new Excel sheet
                var sheet = workbook.GetSheet("Position");

                int rowNumber = 1;

                //Populate the sheet with values from the grid data
                foreach (Deca_Position data in datas)
                {
                    //Create a new row
                    var row = sheet.CreateRow(rowNumber++);
                    //Set values for the cells
                    row.CreateCell(0).SetCellValue(data.PositionID);
                    row.CreateCell(1).SetCellValue(data.PositionName);
                    row.CreateCell(2).SetCellValue(data.Active);
                }

                //Write the workbook to a memory stream
                MemoryStream output = new MemoryStream();
                workbook.Write(output);

                //Return the result to the end user
                return File(output.ToArray(), //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "Deca_Position_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to export data");
                return File("", //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "Deca_Position_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
        }
        #endregion

        #region ImportFromExcel_Action
        [HttpPost]
        public ActionResult ImportFromExcel_Department()
        {
            if (asset.Export)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        List<CRM_Department> listData = new List<CRM_Department>();
                        int total = 0;
                        if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                        {
                            string fileExtension =
                                System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                            if (fileExtension == ".xls" || fileExtension == ".xlsx")
                            {
                                // Create a folder in App_Data named ExcelFiles because you need to save the file temporarily location and getting data from there. 
                                string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);

                                if (System.IO.File.Exists(fileLocation))
                                    System.IO.File.Delete(fileLocation);

                                Request.Files["FileUpload"].SaveAs(fileLocation);
                                string excelConnectionString = string.Empty;

                                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                                excelConnection.Open();
                                DataTable dt = new DataTable();

                                dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                if (dt == null)
                                {
                                    return null;
                                }

                                String[] excelSheets = new String[dt.Rows.Count];
                                int t = 0;
                                //excel data saves in temp file here.
                                foreach (DataRow row in dt.Rows)
                                {
                                    excelSheets[t] = row["TABLE_NAME"].ToString();
                                    t++;
                                }
                                OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                                DataSet ds = new DataSet();

                                string query = string.Format("Select * from [{0}]", excelSheets[0]);
                                using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                                {
                                    dataAdapter.Fill(ds);
                                }
                                List<string> err = new List<string>();

                                //Chạy vòng vòng để check dữ liệu
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    string departmentID = ds.Tables[0].Rows[i]["DepartmentID"].ToString();
                                    if (departmentID.ToString() == "")
                                    {
                                        departmentID = "0";
                                    }
                                    string department = ds.Tables[0].Rows[i]["Department"].ToString();
                                    if (department.ToString() == "")
                                    {
                                        department = "";
                                    }
                                    string active = ds.Tables[0].Rows[i]["Active"].ToString();
                                    if (active.ToString() == "")
                                    {
                                        active = false.ToString();
                                    }
                                    try
                                    {
                                        var listDepartmentID = CRM_Department.GetCRM_Department(int.Parse(departmentID)); // kiểm tra id đã có trong db chưa

                                        if (listDepartmentID == null) // Chưa có thì insert
                                        {
                                            try
                                            {
                                                CRM_Department meta = new CRM_Department();

                                                meta.Department = department;
                                                meta.Active = bool.Parse(active);
                                                meta.CreatedDatetime = DateTime.Now;
                                                meta.CreatedUser = currentUser.UserName;
                                                meta.LastUpdatedDateTime = DateTime.Now;
                                                meta.LastUpdatedUser = currentUser.UserName;
                                                meta.Save();
                                            }
                                            catch (Exception e)
                                            {

                                            }
                                        }
                                        else // Có rồi thì update
                                        {
                                            try
                                            {
                                                listDepartmentID.DepartmentID = int.Parse(departmentID);
                                                listDepartmentID.Department = department;
                                                listDepartmentID.Active = bool.Parse(active);
                                                listDepartmentID.LastUpdatedUser = currentUser.UserName;
                                                listDepartmentID.LastUpdatedDateTime = DateTime.Now;
                                                listDepartmentID.Update();
                                            }
                                            catch (Exception e)
                                            { }

                                        }
                                        total++;
                                    }
                                    catch (Exception e)
                                    {
                                        continue;
                                    }

                                }

                            }
                            else
                            {
                                ModelState.AddModelError("", "Please select Excel File.");
                            }
                        }
                        return Json(new { success = true, data = listData.Select(a => a.Department).ToList(), total = total });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult ImportFromExcel_Department_read([DataSourceRequest] DataSourceRequest request, string data)
        {
            dynamic newdata = Newtonsoft.Json.JsonConvert.DeserializeObject(data, typeof(ExpandoObject));
            List<CRM_Department> list = new List<CRM_Department>();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        //public ActionResult ImportFromExcel_Team()
        //{
        //    if (asset.Export)
        //    {
        //        try
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                List<Deca_Team> listData = new List<Deca_Team>();
        //                int total = 0;
        //                if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
        //                {
        //                    string fileExtension =
        //                        System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

        //                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
        //                    {
        //                        // Create a folder in App_Data named ExcelFiles because you need to save the file temporarily location and getting data from there. 
        //                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);

        //                        if (System.IO.File.Exists(fileLocation))
        //                            System.IO.File.Delete(fileLocation);

        //                        Request.Files["FileUpload"].SaveAs(fileLocation);
        //                        string excelConnectionString = string.Empty;

        //                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

        //                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
        //                        excelConnection.Open();
        //                        DataTable dt = new DataTable();

        //                        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //                        if (dt == null)
        //                        {
        //                            return null;
        //                        }

        //                        String[] excelSheets = new String[dt.Rows.Count];
        //                        int t = 0;
        //                        //excel data saves in temp file here.
        //                        foreach (DataRow row in dt.Rows)
        //                        {
        //                            excelSheets[t] = row["TABLE_NAME"].ToString();
        //                            t++;
        //                        }
        //                        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
        //                        DataSet ds = new DataSet();

        //                        string query = string.Format("Select * from [{0}]", excelSheets[0]);
        //                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
        //                        {
        //                            dataAdapter.Fill(ds);
        //                        }
        //                        List<string> err = new List<string>();

        //                        //Chạy vòng vòng để check dữ liệu
        //                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //                        {
        //                            string teamid = ds.Tables[0].Rows[i]["TeamID"].ToString();
        //                            if (teamid.ToString() == "")
        //                            {
        //                                teamid = "0";
        //                            }
        //                            string teamname = ds.Tables[0].Rows[i]["TeamName"].ToString();
        //                            if (teamname.ToString() == "")
        //                            {
        //                                teamname = "";
        //                            }
        //                            string active = ds.Tables[0].Rows[i]["Active"].ToString();
        //                            if (active.ToString() == "")
        //                            {
        //                                active = false.ToString();
        //                            }
        //                            try
        //                            {
        //                                var _list = Deca_Team.GetDeca_Team(int.Parse(teamid)); // kiểm tra id đã có trong db chưa

        //                                if (_list == null) // Chưa có thì insert
        //                                {
        //                                    try
        //                                    {
        //                                        Deca_Team meta = new Deca_Team();

        //                                        meta.TeamName = teamname;
        //                                        meta.Active = bool.Parse(active);
        //                                        meta.CreatedDatetime = DateTime.Now;
        //                                        meta.CreatedUser = currentUser.UserName;
        //                                        meta.LastUpdatedDateTime = DateTime.Now;
        //                                        meta.LastUpdatedUser = currentUser.UserName;
        //                                        meta.Save();
        //                                    }
        //                                    catch (Exception e)
        //                                    {

        //                                    }
        //                                }
        //                                else // Có rồi thì update
        //                                {
        //                                    try
        //                                    {
        //                                        _list.TeamID = int.Parse(teamid);
        //                                        _list.TeamName = teamname;
        //                                        _list.Active = bool.Parse(active);
        //                                        _list.LastUpdatedUser = currentUser.UserName;
        //                                        _list.LastUpdatedDateTime = DateTime.Now;
        //                                        _list.Update();
        //                                    }
        //                                    catch (Exception e)
        //                                    { }

        //                                }
        //                                total++;
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                continue;
        //                            }

        //                        }

        //                    }
        //                    else
        //                    {
        //                        ModelState.AddModelError("", "Please select Excel File.");
        //                    }
        //                }
        //                return Json(new { success = true, data = listData.Select(a => a.TeamName).ToList(), total = total });
        //            }
        //            else
        //            {
        //                return Json(new { success = false });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json(new { success = false });
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("NoAccessRights", "Error");
        //    }
        //}

        //public ActionResult ImportFromExcel_Team_read([DataSourceRequest] DataSourceRequest request, string data)
        //{
        //    dynamic newdata = Newtonsoft.Json.JsonConvert.DeserializeObject(data, typeof(ExpandoObject));
        //    List<Deca_Team> list = new List<Deca_Team>();
        //    return Json(list.ToDataSourceResult(request));
        //}

       // [HttpPost]
        public ActionResult ImportFromExcel_Position()
        {
            if (asset.Export)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        List<Deca_Position> listData = new List<Deca_Position>();
                        int total = 0;
                        if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                        {
                            string fileExtension =
                                System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                            if (fileExtension == ".xls" || fileExtension == ".xlsx")
                            {
                                // Create a folder in App_Data named ExcelFiles because you need to save the file temporarily location and getting data from there. 
                                string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);

                                if (System.IO.File.Exists(fileLocation))
                                    System.IO.File.Delete(fileLocation);

                                Request.Files["FileUpload"].SaveAs(fileLocation);
                                string excelConnectionString = string.Empty;

                                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                                excelConnection.Open();
                                DataTable dt = new DataTable();

                                dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                if (dt == null)
                                {
                                    return null;
                                }

                                String[] excelSheets = new String[dt.Rows.Count];
                                int t = 0;
                                //excel data saves in temp file here.
                                foreach (DataRow row in dt.Rows)
                                {
                                    excelSheets[t] = row["TABLE_NAME"].ToString();
                                    t++;
                                }
                                OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                                DataSet ds = new DataSet();

                                string query = string.Format("Select * from [{0}]", excelSheets[0]);
                                using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                                {
                                    dataAdapter.Fill(ds);
                                }
                                List<string> err = new List<string>();

                                //Chạy vòng vòng để check dữ liệu
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    string positionid = ds.Tables[0].Rows[i]["PositionID"].ToString();
                                    if (positionid.ToString() == "")
                                    {
                                        positionid = "0";
                                    }
                                    string positionname = ds.Tables[0].Rows[i]["PositionName"].ToString();
                                    if (positionname.ToString() == "")
                                    {
                                        positionname = "";
                                    }
                                    string active = ds.Tables[0].Rows[i]["Active"].ToString();
                                    if (active.ToString() == "")
                                    {
                                        active = "";
                                    }
                                    try
                                    {
                                        var _list = Deca_Position.GetDeca_Position(int.Parse(positionid)); // kiểm tra id đã có trong db chưa

                                        if (_list == null) // Chưa có thì insert
                                        {
                                            try
                                            {
                                                Deca_Position meta = new Deca_Position();

                                                meta.PositionName = positionname;
                                                meta.Active = bool.Parse(active);
                                                meta.CreatedDatetime = DateTime.Now;
                                                meta.CreatedUser = currentUser.UserName;
                                                meta.LastUpdatedDateTime = DateTime.Now;
                                                meta.LastUpdatedUser = currentUser.UserName;
                                                meta.Save();
                                            }
                                            catch (Exception e)
                                            {

                                            }
                                        }
                                        else // Có rồi thì update
                                        {
                                            try
                                            {
                                                _list.PositionID = int.Parse(positionid);
                                                _list.PositionName = positionname;
                                                _list.Active = bool.Parse(active);
                                                _list.LastUpdatedUser = currentUser.UserName;
                                                _list.LastUpdatedDateTime = DateTime.Now;
                                                _list.Update();
                                            }
                                            catch (Exception e)
                                            { }

                                        }
                                        total++;
                                    }
                                    catch (Exception e)
                                    {
                                        continue;
                                    }

                                }

                            }
                            else
                            {
                                ModelState.AddModelError("", "Please select Excel File.");
                            }
                        }
                        return Json(new { success = true, data = listData.Select(a => a.PositionName).ToList(), total = total });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult ImportFromExcel_Position_read([DataSourceRequest] DataSourceRequest request, string data)
        {
            dynamic newdata = Newtonsoft.Json.JsonConvert.DeserializeObject(data, typeof(ExpandoObject));
            List<Deca_Position> list = new List<Deca_Position>();
            return Json(list.ToDataSourceResult(request));
        }
        #endregion
        public ActionResult BusinessUnit_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = new List<CRM_Business_Unit>();
                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                    data = CRM_Business_Unit.GetCRM_Business_Units(where, "RowCreatedTime DESC");
                }
                else
                {
                    data = CRM_Business_Unit.GetCRM_Business_Units("1=1", "RowCreatedTime DESC");
                }
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "You don't have permission read.");
            }
        }
        public ActionResult BusinessUnit_Create([DataSourceRequest]
                                            DataSourceRequest request, [Bind(Prefix = "models")]
                                            IEnumerable<CRM_Business_Unit> listLogistic)
        {
            try
            {
                if (asset.Create)
                {
                    if (listLogistic != null && ModelState.IsValid)
                    {
                        foreach (var logistic in listLogistic)
                        {
                            var write = new CRM_Business_Unit();
                            if (logistic.Name == null || logistic.Name == "")
                            {
                                ModelState.AddModelError("", "Please input Name");
                                return Json(listLogistic.ToDataSourceResult(request, ModelState));
                            }
                            int num;
                            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                            if (logistic.Phone != null && logistic.Phone != "")
                            {
                                bool isNumeric = int.TryParse(logistic.Phone, out num);
                                if (!isNumeric)
                                {
                                    ModelState.AddModelError("", "Invalid Phone number.");
                                    return Json(listLogistic.ToDataSourceResult(request, ModelState));
                                }
                            }
                            if (logistic.Fax != null && logistic.Fax != "")
                            {
                                bool isNumeric = int.TryParse(logistic.Fax, out num);
                                if (!isNumeric)
                                {
                                    ModelState.AddModelError("", "Invalid Fax number.");
                                    return Json(listLogistic.ToDataSourceResult(request, ModelState));
                                }
                            }
                            if (logistic.Email != null && logistic.Email != "")
                            {
                                if (!Regex.IsMatch(logistic.Email, pattern))
                                {
                                    ModelState.AddModelError("", "Invalid address email.");
                                    return Json(listLogistic.ToDataSourceResult(request, ModelState));
                                }
                            }
                            var checkName = CRM_Business_Unit.GetCRM_Business_Units("[Name] = '" + logistic.Name + "'AND [Active] = '" + logistic.Active + "'", "");
                            var checkEmail = CRM_Business_Unit.GetCRM_Business_Units("[Email] = '" + logistic.Email + "' AND [Email]!=''", "");
                            if (checkName.Count() > 0 || checkEmail.Count() > 0)
                            {
                                ModelState.AddModelError("", "Name or Email exist in system");
                                return Json(listLogistic.ToDataSourceResult(request, ModelState));
                            }
                            string id = "";
                            var checkID = CRM_Business_Unit.GetAllCRM_Business_Units().OrderByDescending(m => m.BusinessID).FirstOrDefault();
                            if (checkID != null)
                            {
                                var nextNo = Int32.Parse(checkID.BusinessID.Substring(2, checkID.BusinessID.Length - 2)) + 1;
                                id = "BU" + String.Format("{0:000}", nextNo);
                            }
                            else
                            {
                                id = "BU001";
                            }
                            write.BusinessID = id;
                            write.Name = logistic.Name.Trim();
                            write.Phone = logistic.Phone != null ? logistic.Phone.Trim() : "";
                            write.Email = logistic.Email != null ? logistic.Email.Trim() : "";
                            write.Fax = logistic.Fax != null ? logistic.Fax.Trim() : "";
                            write.Description = logistic.Description != null ? logistic.Description.Trim() : "";
                            write.Active = logistic.Active;
                            write.RowCreatedTime = DateTime.Now;
                            write.RowCreatedUser = currentUser.UserName;
                            write.Owner = logistic.Owner != null ? logistic.Owner.Trim() : "";
                            write.Save();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to update record");
                    return Json(listLogistic.ToDataSourceResult(request, ModelState));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Json(listLogistic.ToDataSourceResult(request, ModelState));
            }

            return Json(listLogistic.ToDataSourceResult(request, ModelState));
        }
        public ActionResult BusinessUnit_Update([DataSourceRequest]
                                            DataSourceRequest request, [Bind(Prefix = "models")]
                                            IEnumerable<CRM_Business_Unit> listLogistic)
        {
            try
            {
                if (asset.Update)
                {
                    if (listLogistic != null && ModelState.IsValid)
                    {
                        foreach (var logistic in listLogistic)
                        {
                            if (logistic.Name == null || logistic.Name == "")
                            {
                                ModelState.AddModelError("", "Please input Name");
                                return Json(listLogistic.ToDataSourceResult(request, ModelState));
                            }
                            int num;
                            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                            if (logistic.Phone != null && logistic.Phone != "")
                            {
                                bool isNumeric = int.TryParse(logistic.Phone, out num);
                                if (!isNumeric)
                                {
                                    ModelState.AddModelError("", "Invalid Phone number.");
                                    return Json(listLogistic.ToDataSourceResult(request, ModelState));
                                }
                            }
                            if (logistic.Fax != null && logistic.Fax != "")
                            {
                                bool isNumeric = int.TryParse(logistic.Fax, out num);
                                if (!isNumeric)
                                {
                                    ModelState.AddModelError("", "Invalid Fax number.");
                                    return Json(listLogistic.ToDataSourceResult(request, ModelState));
                                }
                            }
                            if (logistic.Email != null && logistic.Email != "")
                            {
                                if (!Regex.IsMatch(logistic.Email, pattern))
                                {
                                    ModelState.AddModelError("", "Invalid address email.");
                                    return Json(listLogistic.ToDataSourceResult(request, ModelState));
                                }
                            }
                            var checkName = CRM_Business_Unit.GetCRM_Business_Units("[Name] = N'" + logistic.Name + "' AND [Description] = N'" + logistic.Description + "' AND [Active] = '" + logistic.Active + "' AND [Phone] = '" + logistic.Phone + "' AND [Email] ='" + logistic.Email + "' AND [Fax] = '" + logistic.Fax + "' AND [Owner] = '" + logistic.Owner + "' AND [Owner] != '' ", "").FirstOrDefault();
                            if (checkName != null)
                            {
                                ModelState.AddModelError("", "Name exist in system");
                                return Json(listLogistic.ToDataSourceResult(request, ModelState));
                            }
                            var write = new CRM_Business_Unit();
                            write.BusinessID = logistic.BusinessID;
                            write.Name = logistic.Name.Trim();
                            write.Phone = logistic.Phone != null ? logistic.Phone.Trim() : "";
                            write.Email = logistic.Email != null ? logistic.Email.Trim() : "";
                            write.Fax = logistic.Fax != null ? logistic.Fax.Trim() : "";
                            write.Description = logistic.Description != null ? logistic.Description.Trim() : "";
                            write.Active = logistic.Active;
                            write.RowLastUpdatedTime = DateTime.Now;
                            write.RowLastUpdatedUser = currentUser.UserName;
                            write.Owner = logistic.Owner != null ? logistic.Owner.Trim() : "";
                            write.Update();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to update record");
                    return Json(listLogistic.ToDataSourceResult(request, ModelState));
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return Json(listLogistic.ToDataSourceResult(request, ModelState));
            }

            return Json(listLogistic.ToDataSourceResult(request, ModelState));
        }
        public ActionResult DeleteDeBusiness(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        var checkManager = CRM_Business_Unit_Manager.GetCRM_Business_Unit_Managers("[BusinessID] = '" + item + "'", "");
                        var checkDepartment = CRM_Business_Unit_Department.GetCRM_Business_Unit_Departments("[BusinessID] = '" + item + "'", "");
                        if (checkManager.Count() > 0 || checkDepartment.Count() > 0)
                        {
                            return Json(new { success = false, alert = "Can not remove " + item });
                        }
                        var check = new CRM_Business_Unit();
                        check.BusinessID = item;
                        check.Delete();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to create record" });
            }
        }
        public ActionResult GetlistUser([DataSourceRequest] DataSourceRequest request, string BusinessID)
        {
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = dbConn.Select<EmployeeInfo>();
                    return Json(data.ToDataSourceResult(request));
                }

            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        public ActionResult GetlistDepartment([DataSourceRequest] DataSourceRequest request, string BusinessID)
        {
            if (asset.View)
            {
                var data = ""; 
                //var data = CRM_Business_Unit_Department.GetCRM_Department(BusinessID);
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        public ActionResult Manager_Read([DataSourceRequest] DataSourceRequest request, string BusinessID)
        {
            if (asset.View)
            {
                var data = ERPAPD.Models.CRM_Business_Unit_Manager.GetCRM_Business_Unit_Manager(BusinessID);
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "You don't have permission read.");
            }
        }
        public ActionResult SaveManager(string data, string BusinessID)
        {
            if (asset.Create)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        var check = new CRM_Business_Unit_Manager();
                        check.BusinessID = BusinessID;
                        check.UserID = item;
                        check.Active = true;
                        check.RowCreatedTime = DateTime.Now;
                        check.RowCreatedUser = currentUser.UserName;
                        check.Save();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to create record" });
            }
        }
        public ActionResult DeleteManager(string data, string BusinessID)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        var check = new CRM_Business_Unit_Manager();
                        check.UserID = item;
                        check.BusinessID = BusinessID;
                        check.Delete();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to create record" });
            }
        }
        public ActionResult SaveDepartment(string data, string BusinessID)
        {
            if (asset.Create)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        var check = new CRM_Business_Unit_Department();
                        check.BusinessID = BusinessID;
                        check.DepartmentID = item;
                        check.Active = true;
                        check.RowCreatedTime = DateTime.Now;
                        check.RowCreatedUser = currentUser.UserName;
                        check.Save();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to create record" });
            }
        }
        public ActionResult Department_Reads([DataSourceRequest] DataSourceRequest request, string BusinessID)
        {
            if (asset.View)
            {
                var data = ERPAPD.Models.CRM_Business_Unit_Department.GetCRM_Business_Unit_Department(BusinessID);
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "You don't have permission read.");
            }
        }
        public ActionResult DeleteDepartment(string data, string BusinessID)
        {
            if (asset.Delete)
            {
                try
                {
                    var dbConn = Helpers.OrmliteConnection.openConn();
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                       // var check = new CRM_Business_Unit_Department();
                       // check.DepartmentID = item;
                       //  check.BusinessID = BusinessID;
                       //  check.Delete();
                        //var dbConn = Helpers.OrmliteConnection.openConn();
                       // dbConn.Delete();
                        dbConn.Delete<CRM_Department>(s => s.DepartmentID == int.Parse(item));
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to create record" });
            }
        }
        public FileResult BusinessExportExcel([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                var Alias = CRM_Business_Unit.GetCRM_Business_Units("1=1", "RowCreatedTime DESC");

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\CRM_Business_Unit.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);

                //data sheet
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["CRM_Business_Unit"];

                IEnumerable listData = Alias.ToDataSourceResult(request).Data;

                int rowData = 1;
                foreach (CRM_Business_Unit data in listData)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.BusinessID;
                    dataSheet.Cells[rowData, i++].Value = data.Name;
                    dataSheet.Cells[rowData, i++].Value = data.Owner;
                    if (data.Phone != null && data.Phone != "")
                    {
                        dataSheet.Cells[rowData, i++].Value = int.Parse(data.Phone);
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = data.Email;
                    if (data.Fax != null && data.Fax != "")
                    {
                        dataSheet.Cells[rowData, i++].Value = int.Parse(data.Fax);
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = data.Description;
                    dataSheet.Cells[rowData, i++].Value = data.Active;
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedTime.ToString();
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedUser;
                    if (data.RowLastUpdatedTime.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedTime.ToString();
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedUser;
                }
                var listUser = new List<Users>();
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    listUser = dbConn.Select<Users>();
                }
                ExcelWorksheet dataSheetUserID = excelPkg.Workbook.Worksheets["List"];
                IEnumerable listDataUserID = listUser.ToDataSourceResult(request).Data;

                int rowDataAliasID = 1;
                foreach (Users data in listDataUserID)
                {
                    int i = 1;
                    rowDataAliasID++;
                    dataSheetUserID.Cells[rowDataAliasID, i++].Value = data.UserName;
                }
                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "CRM_Business_Unit_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                string fileName = "CRM_Business_Unit_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentType, fileName);
            }
        }
        public ActionResult BusinessImportFromExcel()
        {
            try
            {
                if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                {
                    string fileExtension =
                        System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                    if (fileExtension == ".xlsx")
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);
                        string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUpload"].FileName);

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);

                        Request.Files["FileUpload"].SaveAs(fileLocation);
                        //Request.Files["fileUpload"].SaveAs(errorFileLocation);

                        var rownumber = 2;
                        var total = 0;
                        FileInfo fileInfo = new FileInfo(fileLocation);
                        var excelPkg = new ExcelPackage(fileInfo);
                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\CRM_Business_Unit.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);
                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["_Business_Unit"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["CRM_Business_Unit"];
                        int totalRows = oSheet.Dimension.End.Row;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            string ID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            string Name = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            string Owner = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                            string Phone = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "";
                            string Email = oSheet.Cells[i, 5].Value != null ? oSheet.Cells[i, 5].Value.ToString() : "";
                            string Fax = oSheet.Cells[i, 6].Value != null ? oSheet.Cells[i, 6].Value.ToString() : "";
                            string Description = oSheet.Cells[i, 7].Value != null ? oSheet.Cells[i, 7].Value.ToString() : "";
                            string Active = oSheet.Cells[i, 8].Value != null ? oSheet.Cells[i, 8].Value.ToString() : "TRUE";
                            try
                            {
                                var write = new CRM_Business_Unit();
                                var checkAliasNameExist = CRM_Business_Unit.GetCRM_Business_Units("[Name]='" + Name.Trim() + "'", "");
                                var check = CRM_Business_Unit.GetCRM_Business_Units("[Name]='" + Name.Trim() + "' AND [Active] ='" + Active + "' AND [Email] ='" + Email + "' AND [Phone] ='" + Phone + "' AND [Fax] ='" + Fax + "' AND [Description]='" + Description + "' AND [Owner]='" + Owner + "' AND [Owner] != ''", "").FirstOrDefault();
                                if (string.IsNullOrEmpty(Name.ToString()))
                                {
                                    eSheet.Cells[rownumber, 1].Value = ID;
                                    eSheet.Cells[rownumber, 2].Value = Name;
                                    eSheet.Cells[rownumber, 8].Value = Active;
                                    eSheet.Cells[rownumber, 13].Value = "Name required";
                                    rownumber++;
                                }
                                if (check == null)
                                {
                                    if (checkAliasNameExist.Count() > 0)
                                    {
                                        write.BusinessID = ID;
                                        write.Name = Name;
                                        write.Active = Convert.ToBoolean(Active);
                                        write.Phone = Phone;
                                        write.Email = Email;
                                        write.Fax = Fax;
                                        write.Description = Description;
                                        write.RowLastUpdatedTime = DateTime.Now;
                                        write.RowLastUpdatedUser = currentUser.UserName;
                                        write.Owner = Owner;
                                        write.Update();
                                    }
                                    else
                                    {
                                        string id = "";
                                        var checkID = CRM_Business_Unit.GetCRM_Business_Units("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                                        if (checkID != null)
                                        {
                                            var nextNo = Int32.Parse(checkID.BusinessID.Substring(2, checkID.BusinessID.Length - 2)) + 1;
                                            id = "BU" + String.Format("{0:000}", nextNo);
                                        }
                                        else
                                        {
                                            id = "BU001";
                                        }
                                        write.BusinessID = id;
                                        write.Name = Name;
                                        write.Active = Convert.ToBoolean(Active);
                                        write.Phone = Phone;
                                        write.Email = Email;
                                        write.Fax = Fax;
                                        write.Description = Description;
                                        write.RowCreatedTime = DateTime.Now;
                                        write.RowCreatedUser = currentUser.UserName;
                                        write.Owner = Owner;
                                        write.Save();
                                    }
                                    total++;
                                }
                            }
                            catch (Exception e)
                            {
                                eSheet.Cells[rownumber, 1].Value = ID;
                                eSheet.Cells[rownumber, 2].Value = Name;
                                eSheet.Cells[rownumber, 8].Value = Active;
                                eSheet.Cells[rownumber, 13].Value = e.Message;
                                rownumber++;
                                continue;
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
        public ActionResult Level_Read([DataSourceRequest] DataSourceRequest request, string PositionID)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            var data = dbConn.Select<DC_Position_Level>(" SELECT * FROM DC_Position_Level WHERE PositionID= '" + PositionID + "' order by Id DESC ");
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult Level_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<DC_Position_Level> list, string PositionID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Create)
                {
                    try
                    {
                        if (list != null && ModelState.IsValid)
                        {
                            var data = new DC_Position_Level();
                            foreach (var item in list)
                            {
                                if (string.IsNullOrEmpty(item.Description))
                                {
                                    ModelState.AddModelError("error", "Please input description.");
                                    dbTrans.Rollback();
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                var checkDescription = dbConn.Select<DC_Position_Level>("SELECT Description FROM dbo.DC_Position_Level WHERE PositionID = '" + PositionID + "' AND Description = '" + item.Description + "'");
                                if (checkDescription.Count() > 0)
                                {
                                    ModelState.AddModelError("error", "Level exist.");
                                    dbTrans.Rollback();
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                string id = "";
                                var checkID = dbConn.Select<DC_Position_Level>("SELECT LevelID, Id FROM dbo.DC_Position_Level ORDER BY Id DESC").FirstOrDefault();
                                if (checkID != null)
                                {
                                    var nextNo = int.Parse(checkID.LevelID.Substring(2, checkID.LevelID.Length - 2)) + 1;
                                    id = "LV" + String.Format("{0:0000}", nextNo);
                                }
                                else
                                {
                                    id = "LV0001";
                                }

                                data.PositionID = PositionID;
                                data.LevelID = id;
                                data.Description = !string.IsNullOrEmpty(item.Description) ? item.Description : "";
                                data.RowCreatedTime = DateTime.Now;
                                data.RowCreatedUser = currentUser.UserName;
                                data.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                                data.RowLastUpdatedUser = "";
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
        public ActionResult Level_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<DC_Position_Level> list, string PositionID)
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
                                if (string.IsNullOrEmpty(item.Description))
                                {
                                    ModelState.AddModelError("error", "Please input description.");
                                    dbTrans.Rollback();
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                var checkDescription = dbConn.Select<DC_Position_Level>("SELECT Description FROM dbo.DC_Position_Level WHERE PositionID = '" + PositionID + "' AND Description = '" + item.Description + "'");
                                if (checkDescription.Count() > 0)
                                {
                                    ModelState.AddModelError("error", "Level exist.");
                                    dbTrans.Rollback();
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                item.Description = !string.IsNullOrEmpty(item.Description) ? item.Description : "";
                                item.RowLastUpdatedTime = DateTime.Now;
                                item.RowLastUpdatedUser = currentUser.UserName;
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
        public ActionResult DeleteLevel(string data)
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
                            var checkexist = dbConn.Select<Users>("SELECT LevelID FROM [EmployeeInfo] WHERE LevelID = '" + item + "'");
                            if (checkexist.Count() > 0)
                            {
                                dbTrans.Rollback();
                                return Json(new { success = false, alert = "You can't delete record" });
                            }
                            dbConn.Delete<DC_Position_Level>(where: "LevelID={0}".Params(item));
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
        public ActionResult GetLevelByPosition(string PositionID)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            var data = dbConn.Select<DC_Position_Level>(" SELECT * FROM DC_Position_Level WHERE PositionID= '" + PositionID + "' order by Id DESC ");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}