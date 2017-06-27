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
using System.Collections;
using System.Data.OleDb;
using NPOI.HSSF.UserModel;
using System.Dynamic;

namespace ERPAPD.Controllers
{
    public class EmployeeController : CustomController
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
                    ViewBag.listRegion= dbConn.Select<CRM_Hierarchy>(@"select a.ID ,a.HierarchyID, b.Value
                                                                    from CRM_Hierarchy a
                                                                    Inner join CRM_Hierarchy b On a.ParentID =  b.HierarchyID
                                                                    Where b.Level = 2");
                    ViewData["UserGroups"] = dbConn.Select<Groups>();
                }
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Create()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
                if (asset.Create)
                {
                    ViewBag.listUnit = dbConn.Select<CRM_Hierarchy>(@"  SELECT [ID]
                                                                        ,[HierarchyID]
                                                                        ,[Value]
	                                                                     FROM CRM_Hierarchy
                                                                      WHERE Level=3");

                    ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>(@" SELECT [ID]
                                                          ,[HierarchyID]
                                                          ,[Value]
	                                                       FROM CRM_Hierarchy
                                                        WHERE Level=2");


                    ViewBag.listEmployee = dbConn.Select<ERPAPD_Employee>();
                    return View();
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
        }
        public ActionResult Edit(string id)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Update)
                {
                    var isOld = dbConn.Select<ERPAPD_Employee>(@"SELECT em.*,hi.ParentID as Region  FROM ERPAPD_Employee em
                                                                LEFT JOIN CRM_Hierarchy hi 
                                                                ON em.FKUnit=hi.[HierarchyID]
                                                                Where PKEmployeeID='" + id + "'").FirstOrDefault();
                    
                    if (isOld != null)
                    {
                        ViewBag.listUnit = dbConn.Select<CRM_Hierarchy>(@"SELECT [ID]
                                                                        ,[HierarchyID]
                                                                        ,[Value]
	                                                                     FROM CRM_Hierarchy
                                                                      WHERE Level=3");
                        ViewBag.listRegion = dbConn.Select<CRM_Hierarchy>(@" SELECT [ID]
                                                          ,[HierarchyID]
                                                          ,[Value]
	                                                       FROM CRM_Hierarchy
                                                        WHERE Level=2");
                        ViewBag.listEmployee = dbConn.Select<ERPAPD_Employee>();
                        return View(isOld);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Edit(ERPAPD_Employee item)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Update && ModelState.IsValid)
                {
                    try
                    {
                        var existEm = dbConn.SingleOrDefault<ERPAPD_Employee>("SELECT * FROM ERPAPD_Employee WHERE PKEmployeeID='"+item.PKEmployeeID+"'");
                        if (!item.IsActive){ existEm.Status = 0;}
                        else{ existEm.Status = 1;}
                        if(!item.isDisplay){existEm.Display =0;}
                        else{existEm.Display = 1;}
                        if (item.isStatusOff ){
                            existEm.StatusOff = 1;
                            existEm.DateOff = DateTime.Now;
                        }
                        existEm.TelePhone = item.TelePhone;
                        existEm.TelHome = item.TelHome;
                        existEm.TelLocal = item.TelLocal;
                        existEm.TelMobile = item.TelMobile;
                        existEm.Address = item.Address;
                        existEm.BirthDay = item.BirthDay;
                        existEm.Email = item.Email;
                        existEm.EmployeeLink = item.EmployeeLink;
                        existEm.Fax = item.Fax;
                        existEm.FKPosition = item.FKPosition;
                        existEm.Name = item.Name;
                        existEm.UserName = item.UserName;
                        existEm.RowUpdatedAt = DateTime.Now;
                        existEm.RowUpdatedUser = currentUser.UserName;
                        existEm.FKUnit = item.FKUnit;
                        dbConn.Update(existEm);
                        dbTrans.Commit();
                        return Json(new { error = false, message = Resources.Multi.Success });
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { error = true, message = ex.Message });
                    }
                }
                else
                {
                    if (!asset.Create) return Json(new { error = true, message = "Không có quyền tạo mới." });
                    string message = "";
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            message += error.ErrorMessage + " ";
                        }
                    }
                    return Json(new { error = true, message = message });
                }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Create(ERPAPD_Employee item)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Create && ModelState.IsValid)
                {
                    try
                    {
                        string datetimeEmp = DateTime.Now.ToString("yyyyMMdd");
                        var EmployID = "";
                        var existEm = dbConn.SingleOrDefault<ERPAPD_Employee>("SELECT RowID, PKEmployeeID FROM ERPAPD_Employee ORDER BY RowID DESC");
                        if (existEm != null)
                        {
                            var nextNo = Int32.Parse(existEm.PKEmployeeID.Substring(12, 4)) + 1;
                            EmployID = "NVKD" + datetimeEmp + String.Format("{0:0000}", nextNo);
                        }
                        else
                        {
                            EmployID = "NVKD" + datetimeEmp + "0001";
                        }
                        if(!item.IsActive){
                            item.Status = 0;
                        }
                        else
                        {
                            item.Status = 1;
                        }
                        item.PKEmployeeID = EmployID;
                        item.Display = 1;
                        item.RowCreatedAt = DateTime.Now;
                        item.RowCreatedUser = currentUser.UserName;
                        item.RowUpdatedAt = DateTime.Parse("1900-01-01");
                        item.DateOff = DateTime.Parse("1900-01-01");
                        item.FKUnit = item.FKUnit;
                        item.RowUpdatedUser = "";
                        dbConn.Insert(item);
                        dbTrans.Commit();
                        return Json(new { error = false, message = Resources.Multi.Success });
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { error = true, message = ex.Message });
                    }
                }
                else
                {
                    if (!asset.Create) return Json(new { error = true, message = "Không có quyền tạo mới." });
                    string message = "";
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            message += error.ErrorMessage + " ";
                        }
                    }
                    return Json(new { error = true, message = message });
                }
        }
        #region Read_Action
        public ActionResult GetUnitByRegion(string id)
        {
           var dbConn = Helpers.OrmliteConnection.openConn();
           var data = dbConn.Select<CRM_Hierarchy>(@"  SELECT [ID]
                                               ,[HierarchyID]
                                               ,[Value]
                                              FROM CRM_Hierarchy
                                              WHERE Level=3 AND ParentID='" + id+"'");
           return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {   
                var dbConn = Helpers.OrmliteConnection.openConn();
                var data = new DataSourceResult();
                string strQuery = @"SELECT * FROM [ERPAPD_Employee]";
                data = KendoApplyFilter.KendoDataByQuery<ERPAPD_Employee>(request, strQuery, "");
                //data = dbConn.Select<ERPAPD_Employee>("SELECT * FROM ERPAPD_Employee ORDER BY RowID DESC");
                return Json(data,JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        #endregion

        #region Create_Action
        [AcceptVerbs(HttpVerbs.Post)]      
        public ActionResult Create22([DataSourceRequest] DataSourceRequest request,
        [Bind(Prefix = "models")] IEnumerable<ERPAPD_Employee> listEmplyee)
        {
            IEnumerable<ERPAPD_Employee> u = new ERPAPD_Employee[] { };
            var dbConn = Helpers.OrmliteConnection.openConn();
            if (asset.Create)
            {
                if (listEmplyee != null && ModelState.IsValid)
                {
                    var w = new ERPAPD_Employee();
                    if (w.Name == null)
                    {
                        w.Name = "";
                    }
                    foreach (var team in listEmplyee)
                    {
                        //w.RowCreatedAt = DateTime.Now;
                        //w.RowCreatedUser = currentUser.UserName;
                        //w.RowUpdatedUser = "";
                        //w.RowUpdatedAt = DateTime.Parse("1900-01-01");
                        //dbConn.Insert(w);
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
        public ActionResult Position_Update([DataSourceRequest] DataSourceRequest request,
        [Bind(Prefix = "models")] IEnumerable<ERPAPD_Employee> listEmplyee)
        {
            IEnumerable<ERPAPD_Employee> u = new ERPAPD_Employee[] { };
            if (asset.Create)
            {
                if (listEmplyee != null && ModelState.IsValid)
                {
                    var w = new CRM_Position();
                    foreach (var team in listEmplyee)
                    {
                        //var list = CRM_Position.GetCRM_Position(team.PositionID);
                        //list.LastUpdatedUser = currentUser.UserName;
                        //list.LastUpdatedDateTime = DateTime.Now;
                        //list.PositionName = team.PositionName;
                        //list.Active = team.Active;
                        //list.Update();
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
        public FileResult Export_Position([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.Export)
            {
                //Get the data representing the current grid state - page, sort and filter
                IEnumerable datas = CRM_Position.GetAllCRM_Positions().ToDataSourceResult(request).Data;
                //Create new Excel workbook
                FileStream fs = new FileStream(Server.MapPath(@"~\ExportExcelFile\CRM_Position.xls"), FileMode.Open, FileAccess.Read);
                var workbook = new HSSFWorkbook(fs, true);

                //Create new Excel sheet
                var sheet = workbook.GetSheet("Position");

                int rowNumber = 1;

                //Populate the sheet with values from the grid data
                foreach (CRM_Position data in datas)
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
                    "CRM_Position_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to export data");
                return File("", //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "CRM_Position_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
        }
        #endregion

        #region ImportFromExcel_Action
      

        [HttpPost]
        public ActionResult ImportFromExcel_Position()
        {
            if (asset.Export)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        List<CRM_Position> listData = new List<CRM_Position>();
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
                                        //var _list = CRM_Position.GetCRM_Position(int.Parse(positionid)); // kiểm tra id đã có trong db chưa

                                        //if (_list == null) // Chưa có thì insert
                                        //{
                                        //    try
                                        //    {
                                        //        CRM_Position meta = new CRM_Position();

                                        //        meta.PositionName = positionname;
                                        //        meta.Active = bool.Parse(active);
                                        //        meta.CreatedDatetime = DateTime.Now;
                                        //        meta.CreatedUser = currentUser.UserName;
                                        //        meta.LastUpdatedDateTime = DateTime.Now;
                                        //        meta.LastUpdatedUser = currentUser.UserName;
                                        //        meta.Save();
                                        //    }
                                        //    catch (Exception e)
                                        //    {

                                        //    }
                                        //}
                                        //else // Có rồi thì update
                                        //{
                                        //    try
                                        //    {
                                        //        _list.PositionID = int.Parse(positionid);
                                        //        _list.PositionName = positionname;
                                        //        _list.Active = bool.Parse(active);
                                        //        _list.LastUpdatedUser = currentUser.UserName;
                                        //        _list.LastUpdatedDateTime = DateTime.Now;
                                        //        _list.Update();
                                        //    }
                                        //    catch (Exception e)
                                        //    { }

                                        //}
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
            List<CRM_Position> list = new List<CRM_Position>();
            return Json(list.ToDataSourceResult(request));
        }
        #endregion    
        public ActionResult Delete(string data, string BusinessID)
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
                        dbConn.Delete<ERPAPD_Employee>(s => s.PKEmployeeID == item);
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
        public ActionResult SuggestEmployer(string text)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<EmployeeInfo>(
                    @"SELECT Top 10 e.*, t.TeamName as TeamName,h.Value as RegionName
		                FROM EmployeeInfo e 
		                left join CRM_Team t ON e.Team = t.TeamID
		                left join CRM_Hierarchy h ON e.Region = h.HierarchyID 
                        WHERE e.FullName COLLATE Latin1_General_CI_AI  LIKE N'%" + text + "%'"
                      );
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}