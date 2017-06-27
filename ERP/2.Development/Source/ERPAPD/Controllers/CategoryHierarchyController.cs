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
    public class CategoryHierarchyController : CustomController
    {
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    OrmLiteConfig.DialectProvider.UseUnicode = true;
            //    dbConn.DropTables(typeof(CRM_CategoryHierarchy));
            //    const bool overwrite = true;
            //    dbConn.CreateTables(overwrite, typeof(CRM_CategoryHierarchy));
            //}
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
                    ViewBag.listParent = dbConn.Select<CRM_CategoryHierarchy>(@"SELECT ID,HierarchyID, CAST(HierarchyID AS varchar)+' - '+ Value AS Value  FROM CRM_CategoryHierarchy");
                    ViewBag.listType = dbConn.Select<Parameters>(s => s.Type == "CategoryHierarchyType");
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

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var dbConn = Helpers.OrmliteConnection.openConn();
                var data = new List<CRM_CategoryHierarchy>();
                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                    data = dbConn.Select<CRM_CategoryHierarchy>(where+" Order by ID DESC");
                }
                data = dbConn.Select<CRM_CategoryHierarchy>();
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
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
        [Bind(Prefix = "models")] IEnumerable<CRM_CategoryHierarchy> list)
        {
            IEnumerable<CRM_CategoryHierarchy> u = new CRM_CategoryHierarchy[] { };
            if (asset.Create)
            {
                if (list != null && ModelState.IsValid)
                {
                    var dbConn = Helpers.OrmliteConnection.openConn();
                    using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                    {
                        foreach (var value in list)
                        {
                            var check = dbConn.SingleOrDefault<CRM_CategoryHierarchy>("HierarchyID={0}",value.HierarchyID);
                            if (check == null)
                            {
                                var item = new CRM_CategoryHierarchy();
                                item.HierarchyID = value.HierarchyID;
                                item.HierarchyType = value.HierarchyType;
                                item.Value = value.Value;
                                item.ParentID = value.ParentID;
                                item.Level = value.Level;
                                item.CreatedBy = currentUser.UserName;
                                item.CreatedAt = DateTime.Now;
                                item.UpdatedAt = DateTime.Parse("1900-01-01");
                                item.UpdatedBy = "";
                                dbConn.Insert(item);
                            }
                            else
                            {
                                ModelState.AddModelError("", "Đã tồn tại mã:" + value.HierarchyID);
                                return Json(u.ToDataSourceResult(request, ModelState));
                            }
                        }
                        dbTrans.Commit();
                        return Json(new { success = true });
                    }
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
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
        [Bind(Prefix = "models")] IEnumerable<CRM_CategoryHierarchy> list)
        {
            IEnumerable<CRM_CategoryHierarchy> u = new CRM_CategoryHierarchy[] { };
            if (asset.Update)
            {
                if (list != null)
                {
                    var dbConn = Helpers.OrmliteConnection.openConn();
                    using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                    {
                        foreach (var value in list)
                        {
                            var itemExit = dbConn.SingleOrDefault<CRM_CategoryHierarchy>("ID={0}",value.ID);
                            if (itemExit != null)
                            {   
                                if(itemExit.ID==1)
                                {
                                  itemExit.Value = value.Value.Trim();
                                }
                                else
                                {
                                    itemExit.HierarchyID = value.HierarchyID.Trim();
                                    itemExit.HierarchyType = value.HierarchyType.Trim();
                                    itemExit.Value = value.Value.Trim();
                                    itemExit.ParentID = value.ParentID.Trim();
                                    itemExit.Level = value.Level;    
                                }
                                itemExit.Status = value.Status;
                                itemExit.UpdatedAt = DateTime.Now;
                                itemExit.UpdatedBy = currentUser.UserName;
                                dbConn.Update(itemExit);
                            }
                        }
                        dbTrans.Commit();
                        return Json(new { success = true });
                    }
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

     

        public ActionResult ImportFromExcel_Team_read([DataSourceRequest] DataSourceRequest request, string data)
        {
            dynamic newdata = Newtonsoft.Json.JsonConvert.DeserializeObject(data, typeof(ExpandoObject));
            List<CRM_Team> list = new List<CRM_Team>();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
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
                        dbConn.Delete<CRM_CategoryHierarchy>(s => s.ID == int.Parse(item));
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
    }
}