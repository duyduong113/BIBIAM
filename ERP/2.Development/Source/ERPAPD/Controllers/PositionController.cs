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
    public class PositionController : CustomController
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
     
        public ActionResult Position_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = new DataSourceResult();
            if (asset.View)
            {
                string strQuery = @"SELECT  * FROM CRM_Position";
                data = KendoApplyFilter.KendoDataByQuery<CRM_Position>(request, strQuery, "");
                return Json(data);
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        #endregion

        #region Create_Action
        [AcceptVerbs(HttpVerbs.Post)]      
        public ActionResult Position_Create([DataSourceRequest] DataSourceRequest request,
        [Bind(Prefix = "models")] IEnumerable<CRM_Position> listPosition)
        {
            IEnumerable<CRM_Position> u = new CRM_Position[] { };
            var dbConn = Helpers.OrmliteConnection.openConn();
            if (asset.Create)
            {
                if (listPosition != null)
                {
                    var w = new CRM_Position();
                    foreach (var team in listPosition)
                    {
                        var a=dbConn.Select<CRM_Position>(p => p.PositionID==team.PositionID).SingleOrDefault();
                        if(a!=null)
                        {
                            ModelState.AddModelError("", "Đã tồn tại mã chức vụ");
                            return Json(u.ToDataSourceResult(request, ModelState));
                        }
                        var b = dbConn.Select<CRM_Position>(p => p.PositionName == team.PositionName).SingleOrDefault();
                        if (b != null)
                        {
                            ModelState.AddModelError("", "Đã tồn tại tên chức vụ");
                            return Json(u.ToDataSourceResult(request, ModelState));
                        }
                        else
                        {
                            w.PositionID = team.PositionID;
                            w.PositionName = team.PositionName;
                            w.Active = true;
                            w.CreatedDatetime = DateTime.Now;
                            w.CreatedUser = currentUser.UserName;
                            w.LastUpdatedUser = "";
                            w.LastUpdatedDateTime = DateTime.Parse("1900-01-01");
                            dbConn.Insert(w);
                        }
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
        [Bind(Prefix = "models")] IEnumerable<CRM_Position> listPosition)
        {
            IEnumerable<CRM_Position> u = new CRM_Position[] { };
            var dbConn = Helpers.OrmliteConnection.openConn();
            if (asset.Create)
            {
                if (listPosition != null)
                {
                    foreach (var team in listPosition)
                    {
                        var list = dbConn.Select<CRM_Position>(p => p.Id == team.Id).FirstOrDefault();
                        list.PositionID = team.PositionID;
                        list.PositionName = team.PositionName;
                        list.LastUpdatedUser = currentUser.UserName;
                        list.LastUpdatedDateTime = DateTime.Now;
                        list.Active = team.Active;
                        dbConn.Update(list);
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
                                        //        _list.PositionID = positionid;
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
                        dbConn.Delete<CRM_Position>(s => s.Id == int.Parse(item));
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