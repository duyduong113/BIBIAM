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
    [Authorize]
    public class TelesalesAgentController : CustomController
    {


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

                    ViewBag.AllowView = true;
                    ViewBag.canEdit = asset.Update;

                    ViewBag.listUserName = dbConn.Select<EmployeeInfo>();

                    ViewBag.listTeam = dbConn.Select<Deca_Code_Master>("CodeType = {0}", "TelesalesAgentTeam");
                    ViewBag.listTeamDistinc = dbConn.Select<Deca_Code_Master>("CodeType = {0}", "TelesalesAgentTeam");
                    ViewBag.listRegion = DC_Location_Region.GetAllDC_Location_Regions().ToList();
                    //ViewBag.listMOPRegion = DC_MOP_Region.GetListRegion();

                    return View();
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult TelesalesAgent_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {

                List<DC_Telesales_Agent> data = new List<DC_Telesales_Agent>();
                data = DC_Telesales_Agent.GetDC_Telesales_Agent();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult Read_Agent_Branch_Region([DataSourceRequest] DataSourceRequest request)
        {
            var data = DC_Location_Region.GetAllDC_Location_Regions();
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult Read_Agent_Branch_Region2([DataSourceRequest] DataSourceRequest request, string RegionID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<DC_TeleSale_Agent_Region>("RegionID = {0}", RegionID);
                return Json(data.ToDataSourceResult(request));
            }
        }


        public ActionResult Branch_Region_Read([DataSourceRequest] DataSourceRequest request, string UserName)
        {
            List<DC_TeleSale_Agent_Region> data = DC_TeleSale_Agent_Region.GetReadData(UserName);
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult GetBranch()
        {
            var data = DC_Location_Region.GetAllDC_Location_Regions();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUser()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Users>("Groups like '%telesales%'");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RemoveUserFromTelesales(string UserName)
        {
            try
            {
                using (var DbConn = OrmliteConnection.openConn())
                {
                    var user = DbConn.SingleOrDefault<Users>("UserName = {0}", UserName);
                    var itemToRemove = user.Groups.SingleOrDefault(r => r.Name == "telesales");
                    if (itemToRemove != null)
                        user.Groups.Remove(itemToRemove);
                    user.UpdatedAt = DateTime.Now;
                    user.UpdatedBy = currentUser.UserName;
                    DbConn.Update(user);
                    return Json(new { success = true });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        [HttpPost]
        public ActionResult SaveBranch(string listBranch, string UserName)
        {
            int dataCount = 0;
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    listBranch = listBranch.Remove(listBranch.Length - 1);
                    DC_TeleSale_Agent_Region newrecords = new DC_TeleSale_Agent_Region();
                    string[] separators = { "," };
                    var data = listBranch.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < data.Length; i++)
                    {
                        //check exist DC_TeleSale_Agent_Branch_Region with UserName AND BranchID AND RegionID
                        var exist = dbConn.Select<DC_TeleSale_Agent_Region>("UserName ='" + UserName + "' AND RegionID = '" + data[i] + "'").ToList();
                        if (exist.Count == 0)
                        {
                            var dataA = new DC_TeleSale_Agent_Region();

                            dataA.RegionID = data[i];
                            dataA.RowCreatedTime = DateTime.Now;
                            dataA.RowCreatedUser = currentUser.UserName;
                            dataA.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                            dataA.RowLastUpdatedUser = "";
                            dataA.UserName = UserName;
                            dbConn.Insert(dataA);
                            dataCount++;
                        }
                    }
                    return Json(new { success = true, dataCount = dataCount });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }
            }
        }

        [HttpPost]
        public ActionResult SaveBranchIDRegionID(string listUser, string RegionID)
        {
            int dataCount = 0;
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    listUser = listUser.Remove(listUser.Length - 1);
                    DC_TeleSale_Agent_Region newrecords = new DC_TeleSale_Agent_Region();
                    string[] separators = { "," };
                    var data = listUser.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < data.Length; i++)
                    {
                        //check exist DC_TeleSale_Agent_Branch_Region with UserName AND BranchID AND RegionID
                        var exist = dbConn.Select<DC_TeleSale_Agent_Region>("UserName ='" + data[i] + "' AND RegionID = '" + RegionID + "'").ToList();
                        if (exist.Count == 0)
                        {
                            var dataA = new DC_TeleSale_Agent_Region();

                            dataA.RegionID = RegionID;
                            dataA.RowCreatedTime = DateTime.Now;
                            dataA.RowCreatedUser = currentUser.UserName;
                            dataA.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                            dataA.RowLastUpdatedUser = "";
                            dataA.UserName = data[i];
                            dbConn.Insert(dataA);
                            dataCount++;
                        }
                    }
                    return Json(new { success = true, dataCount = dataCount });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Branch_Remove([DataSourceRequest] DataSourceRequest request, string UserName)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                string[] separators = { "@@" };
                var data = Request.Form["data"].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    var data1 = new DC_TeleSale_Agent_Region();
                    for (int i = 0; i < data.Length; i++)
                    {
                        data1.Id = int.Parse(data[i]);
                        dbConn.Delete(data1);
                    }
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, alert = e.Message });
                }
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult deleteBranchIDRegionID([DataSourceRequest] DataSourceRequest request, string RegionID)
        {
            string[] separators = { "@@" };
            var data = Request.Form["data"].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data1 = new DC_TeleSale_Agent_Region();
                    for (int i = 0; i < data.Length; i++)
                    {
                        data1.Id = int.Parse(data[i]);
                        dbConn.Delete(data1);
                    }
                    return Json(new { success = true });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, alert = e.Message });
            }
        }

        public ActionResult TelesalesAgent_Save([DataSourceRequest]
                                            DataSourceRequest request, [Bind(Prefix = "models")]
                                            IEnumerable<EmployeeInfo> us)
        {
            using(var dbConn = OrmliteConnection.openConn())
            { 
            int count = 0;

            foreach (var item in us)
            {
                DC_Telesales_Agent ta = dbConn.FirstOrDefault<DC_Telesales_Agent>("UserName={0}", item.UserName);
                try
                {
                    if (ta == null)
                    {
                        ta = new DC_Telesales_Agent();
                        ta.XLiteID = item.XLiteID == null ? "" : item.XLiteID;
                        ta.UserName = item.UserName == null ? "" : item.UserName;
                        ta.Region = item.Region == null ? "" : item.Region;
                        ta.Team = item.Team == null ? "" : item.Team;
                        ta.CreatedAt = DateTime.Now;
                        ta.CreatedBy = currentUser.UserName;
                        dbConn.Insert(ta);
                    }
                    else
                    {
                        ta.XLiteID = item.XLiteID == null ? "" : item.XLiteID;
                        ta.Region = item.Region == null ? "" : item.Region;
                        ta.Team = item.Team == null ? "" : item.Team;
                        ta.CreatedAt = DateTime.Now;
                        ta.CreatedBy = currentUser.UserName;
                        dbConn.Update(ta);
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Can not update this data");
                }

                count++;

            }
            if (count > 0)
            {
                return Json("success");
            }
            else
            {
                ModelState.AddModelError("", "Can not update this data");
                return Json(us.ToDataSourceResult(request, ModelState));
            }
            }
        }

        [HttpPost]
        public ActionResult Create_Agent_Branch_Region([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<DC_TeleSale_Agent_Region> listResult)
        {
            if (asset.Update && asset.Update)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listResult != null && ModelState.IsValid)
                        {
                            foreach (var item in listResult)
                            {
                                if (string.IsNullOrEmpty(item.RegionID))
                                {
                                    ModelState.AddModelError("", "Please input RegionID");
                                    return Json(listResult.ToDataSourceResult(request, ModelState));
                                }

                                var dataBranch = dbConn.Select<DC_Location_Region>().ToList();
                                if (dataBranch.Count > 0)
                                {
                                    foreach (var item1 in dataBranch)
                                    {
                                        var exist = dbConn.Select<DC_TeleSale_Agent_Region>("Select * from DC_TeleSale_Agent_Branch_Region  Where UserName ='" + item.UserName + "' AND RegionID = '" + item1.RegionID + "'").ToList();
                                        if (exist.Count == 0)
                                        {
                                            var dataA = new DC_TeleSale_Agent_Region();
                                            dataA.RegionID = item1.RegionID;
                                            dataA.RowCreatedTime = DateTime.Now;
                                            dataA.RowCreatedUser = currentUser.UserName;
                                            dataA.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                                            dataA.RowLastUpdatedUser = "";
                                            dataA.UserName = item.UserName;
                                            dbConn.Insert(dataA);
                                        }
                                    }
                                }
                            }
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

        public FileResult TelesalesAgent_Export([DataSourceRequest]
                                 DataSourceRequest request)
        {
            if (asset.Export)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var UserName = currentUser.UserName;
                    List<DC_Telesales_Agent> d = DC_Telesales_Agent.GetDC_Telesales_Agent();

                    var listRequest = d;
                    //Get the data representing the current grid state - page, sort and filter
                    IEnumerable datas = listRequest.ToDataSourceResult(request).Data;
                    //Create new Excel workbook
                    FileStream fs = new FileStream(Server.MapPath(@"~\ExportExcelFile\DC_TelesaleAgent.xls"), FileMode.Open, FileAccess.Read);
                    var workbook = new HSSFWorkbook(fs, true);

                    //Create new Excel sheet
                    var sheetTeam = workbook.GetSheet("List");

                    int rowNumber1 = 1;

                    //Populate the sheet with values from the grid data
                    var list = dbConn.Select<DC_Telesales_Agent>().Select(s => s.Team.Distinct());

                    foreach (string data in list)
                    {
                        //Create a new row
                        var row = sheetTeam.CreateRow(rowNumber1++);
                        //Set values for the cells
                        row.CreateCell(0).SetCellValue(data);

                    }


                    //Create new Excel sheet
                    var sheetRegion = workbook.GetSheet("ListRegion");

                    int rowNumberRegion = 1;

                    //Populate the sheet with values from the grid data
                    var listRegion = DC_Location_Region.GetAllDC_Location_Regions().ToList();

                    foreach (var data in listRegion)
                    {
                        //Create a new row
                        var row = sheetRegion.CreateRow(rowNumberRegion++);
                        //Set values for the cells
                        row.CreateCell(0).SetCellValue(data.RegionName);
                    }

                    //Create new Excel sheet
                    var sheet = workbook.GetSheet("TelesaleAgent");

                    int rowNumber = 1;

                    //Populate the sheet with values from the grid data
                    foreach (DC_Telesales_Agent data in datas)
                    {
                        //Create a new row
                        var row = sheet.CreateRow(rowNumber++);
                        //Set values for the cells
                        row.CreateCell(0).SetCellValue(data.UserName);
                        row.CreateCell(1).SetCellValue(data.Team);
                        row.CreateCell(2).SetCellValue(data.Region);
                        row.CreateCell(3).SetCellValue(data.XLiteID);
                    }

                    //Write the workbook to a memory stream
                    MemoryStream output = new MemoryStream();
                    workbook.Write(output);

                    //Return the result to the end user
                    return File(output.ToArray(), //The binary data of the XLS file
                        "application/vnd.ms-excel", //MIME type of Excel files
                        "DC_TelesaleAgent" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to export data");
                return File("", //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_TelesaleAgent" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }

        }

        public ActionResult ImportFromExcel()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var dbConn = OrmliteConnection.openConn())
                    {


                        List<DC_Telesales_Agent_Meta> listData = new List<DC_Telesales_Agent_Meta>();
                        string filename = @"DC_TelesaleAgent Master Error_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                        int total = 0;
                        var listrow = new List<NPOI.SS.UserModel.IRow>();
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

                                //khoi tao save file error
                                int rowNumber = 1;
                                string currentUserName = currentUser.UserName;
                                FileStream fs = new FileStream(Server.MapPath(@"~\ExportExcelFile\DC_TelesaleAgent.xls"), FileMode.Open, FileAccess.Read);
                                string ErrorfileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), filename);
                                var workbook = new HSSFWorkbook(fs, true);
                                FileStream fileStream = new FileStream(ErrorfileLocation, FileMode.Create, FileAccess.Write);
                                var sheet = workbook.GetSheet("TelesaleAgent");
                                //for (int k = 0; k <= ds.Tables[0].Rows.Count; k++)
                                //{
                                //    var row = sheet.GetRow(k);
                                //    listrow.Add(row);
                                //    sheet.RemoveRow(row);
                                //}

                                //chay vong lap qua row
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    string importnote = "";
                                    string UserName = ds.Tables[0].Rows[i]["UserName"].ToString();
                                    string Team = ds.Tables[0].Rows[i]["Team"].ToString();
                                    string Region = ds.Tables[0].Rows[i]["Region"].ToString();
                                    string XLiteID = ds.Tables[0].Rows[i]["XLiteID"].ToString();
                                    try
                                    {
                                        if (string.IsNullOrWhiteSpace(UserName))
                                        {
                                            importnote += "Null error in required field(s)! ";
                                        }

                                        if (importnote != "")
                                        {
                                            throw new Exception(importnote);
                                        }

                                        EmployeeInfo us = new Models.EmployeeInfo();
                                        us.UserName = UserName;

                                        //var requestID = DC_Customer_lvCredit_Request.GetDC_Customer_lvCredit_Requests("[OrganizationID]='" + request.OrganizationID + "' AND [CustomerID] = '" + request.CustomerID + "'", "").FirstOrDefault();

                                        DC_Telesales_Agent ta = dbConn.FirstOrDefault<DC_Telesales_Agent>("UserName={0}", UserName);
                                        try
                                        {
                                            if (ta == null)
                                            {
                                                importnote += "Không tìm thấy agent này trong hệ thống!";
                                                throw new Exception(importnote);
                                            }
                                            else
                                            {
                                                ta.XLiteID = XLiteID;
                                                ta.Region = Region;
                                                ta.Team = Team;
                                                ta.CreatedAt = DateTime.Now;
                                                ta.CreatedBy = currentUser.UserName;
                                                dbConn.Update(ta);
                                            }
                                        }
                                        catch
                                        {
                                            importnote += "Không tìm thấy agent này trong hệ thống!";
                                            throw new Exception(importnote);
                                        }
                                    }
                                    catch (Exception e)
                                    {


                                        DC_Telesales_Agent_Meta org = new DC_Telesales_Agent_Meta();

                                        org.AgentID = UserName;
                                        org.Team = Team;
                                        org.ImportNote = e.Message;
                                        listData.Add(org);
                                        //Create a new row
                                        var row = sheet.CreateRow(rowNumber++);

                                        //Set values for the cells
                                        row.CreateCell(0).SetCellValue(UserName);
                                        row.CreateCell(1).SetCellValue(Team);
                                        row.CreateCell(2).SetCellValue(Region);
                                        row.CreateCell(2).SetCellValue(XLiteID);
                                        row.CreateCell(4).SetCellValue(importnote);
                                        //Write the workbook to a memory stream
                                        importnote = "";
                                        continue;
                                    }
                                }
                                workbook.Write(fileStream);
                                fileStream.Close();
                                excelConnection.Close();
                                excelConnection1.Close();
                            }
                            else
                            {
                                ModelState.AddModelError("", "Plese select Excel File.");
                            }
                        }
                        return Json(new { success = true, data = listData, total = total, link = filename });
                    }
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

        public FileResult Agent_Branch_Region_Export([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.Export)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var listdata = DC_TeleSale_Agent_Region.GetAll();
                    IEnumerable datas = listdata.ToDataSourceResult(request).Data;
                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_TeleSale_Agent_Branch_Region.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);
                    ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["Agent Master"];

                    ExcelWorksheet dataSheet3 = excelPkg.Workbook.Worksheets["ListUser"];
                    var listdata3 = dbConn.Select<EmployeeInfo>();
                    int rowData3 = 1;
                    foreach (var data in listdata3)
                    {
                        rowData3++;
                        dataSheet3.Cells[rowData3, 1].Value = data.UserName;
                    }

                    ExcelWorksheet dataSheet4 = excelPkg.Workbook.Worksheets["listBranch"];
                    var listdata4 = dbConn.Select<DC_Location_Region>();
                    int rowData4 = 1;
                    foreach (var data in listdata4)
                    {
                        rowData4++;
                        dataSheet4.Cells[rowData4, 1].Value = data.RegionName;
                    }

                    int rowData = 1;
                    foreach (DC_TeleSale_Agent_Region data in datas)
                    {
                        int i = 1;
                        rowData++;
                        dataSheet.Cells[rowData, i++].Value = data.UserName;
                        dataSheet.Cells[rowData, i++].Value = data.BranchName;
                        dataSheet.Cells[rowData, i++].Value = data.RegionName;

                        dataSheet.Cells[rowData, i++].Value = data.RowCreatedTime;
                        dataSheet.Cells[rowData, i++].Value = data.RowCreatedUser;
                        if (data.RowLastUpdatedTime.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedTime;
                        }
                        else
                        {
                            dataSheet.Cells[rowData, i++].Value = "";
                        }
                        dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedUser;
                    }

                    //Write the workbook to a memory stream
                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    //Return the result to the end user
                    return File(output.ToArray(), //The binary data of the XLS file
                        "application/vnd.ms-excel", //MIME type of Excel files
                        "DC_TeleSale_Agent_Branch_Region_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
                }
            }
            else
            {
                return File("", //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_TeleSale_Agent_Branch_Region_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
        }

        [HttpPost]
        public ActionResult ImportFromExcelAgent_Branch_Region()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<DC_TeleSale_Agent_Region> listData = new List<DC_TeleSale_Agent_Region>();
                    int total = 0;

                    /////create excel error file
                    string filename = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    int rowData = 1;

                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_TeleSale_Agent_Branch_Region.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);
                    ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["Agent Master"];

                    string ErrorfileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "DC_TeleSale_Agent_Branch_Region Error_" + filename);
                    FileStream fileStream = new FileStream(ErrorfileLocation, FileMode.Create, FileAccess.Write);

                    if (Request.Files["FileUpload"] != null && Request.Files["fileUpload"].ContentLength > 0)
                    {
                        string fileExtension =
                            System.IO.Path.GetExtension(Request.Files["fileUpload"].FileName);

                        if (fileExtension == ".xls" || fileExtension == ".xlsx")
                        {
                            // Create a folder in App_Data named ExcelFiles because you need to save the file temporarily location and getting data from there. 
                            string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["fileUpload"].FileName);
                            if (System.IO.File.Exists(fileLocation))
                                System.IO.File.Delete(fileLocation);
                            Request.Files["fileUpload"].SaveAs(fileLocation);
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

                            OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                            DataSet ds = new DataSet();
                            string query = "Select * from ['Agent Master$']";
                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                            {
                                dataAdapter.Fill(ds);
                            }
                            var exist_UserName = false;
                            var exist_BranchID = false;

                            foreach (var item in ds.Tables[0].Columns)
                            {
                                if (item.ToString() == "UserName")
                                {
                                    exist_UserName = true;
                                }
                                if (item.ToString() == "BranchID")
                                {
                                    exist_BranchID = true;
                                }
                            }
                            if (exist_UserName)
                            {
                                using (var dbConn = Helpers.OrmliteConnection.openConn())
                                {
                                    var listBranch = dbConn.Select<DC_Location_Region>();

                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        int err1 = 0;
                                        DC_TeleSale_Agent_Region icare = new DC_TeleSale_Agent_Region();
                                        DC_TeleSale_Agent_Region icareerr = new DC_TeleSale_Agent_Region();

                                        icare.UserName = icareerr.UserName = ds.Tables[0].Rows[i]["UserName"].ToString();
                                        string BranchID = ds.Tables[0].Rows[i]["BranchID"].ToString();

                                        if (string.IsNullOrEmpty(icare.UserName) || string.IsNullOrEmpty(BranchID))
                                        {
                                            icareerr.ImportNote = "Vui lòng nhập đủ các thông tin bắt buộc: UserName và Branch";
                                            listData.Add(icareerr);
                                            err1 = 1;
                                        }

                                        if (err1 == 0)
                                        {
                                            if (exist_UserName)
                                            {
                                                icare.UserName = ds.Tables[0].Rows[i]["UserName"].ToString();
                                            }


                                            var dataBranch = dbConn.Select<DC_Location_Region>().ToList();
                                            if (dataBranch.Count > 0)
                                            {
                                                foreach (var item1 in dataBranch)
                                                {
                                                    var exist = dbConn.Select<DC_TeleSale_Agent_Region>("Select * from DC_TeleSale_Agent_Branch_Region  Where UserName ='" + icare.UserName + "' AND RegionID = '" + item1.RegionID + "'").ToList();
                                                    if (exist.Count == 0)
                                                    {
                                                        icare.UserName = icare.UserName;
                                                        icare.RegionID = item1.RegionID;
                                                        icare.RowCreatedTime = DateTime.Now;
                                                        icare.RowCreatedUser = currentUser.UserName;
                                                        icare.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                                                        icare.RowLastUpdatedUser = "";
                                                        dbConn.Insert(icare);
                                                    }
                                                }
                                            }
                                        }
                                        if (err1 == 1)
                                        {
                                            rowData++;
                                            dataSheet.Cells[rowData, 1].Value = icareerr.UserName;
                                            if (exist_UserName)
                                            {
                                                dataSheet.Cells[rowData, 2].Value = ds.Tables[0].Rows[i]["UserName"].ToString();
                                            }
                                            if (exist_BranchID)
                                            {
                                                dataSheet.Cells[rowData, 3].Value = ds.Tables[0].Rows[i]["BranchID"].ToString();
                                            }
                                        }
                                        else
                                        {
                                            total++;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                return Json(new { success = false, message = "Need Field CampaignID and CampaignName" });
                            }

                            excelPkg.SaveAs(fileStream);
                            fileStream.Close();
                            excelConnection.Close();
                            excelConnection1.Close();
                        }
                        else
                        {
                            return Json(new { success = false, message = "Plese select Excel File." });
                        }
                    }
                    return Json(new { success = true, data = listData, total = total, link = filename });
                }
                else
                {
                    return Json(new { success = false, message = "You don't havve permission import" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // import 
        [HttpGet]
        public virtual ActionResult Download2(string file)
        {
            if (asset.Export)
            {
                string fullPath = Path.Combine(Server.MapPath("~/Excel"), "DC_TeleSale_Agent_Branch_Region Error_" + file);
                return File(fullPath, "application/vnd.ms-excel", "DC_TeleSale_Agent_Branch_Region Error_" + file);
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        [HttpPost]
        public ActionResult RemoveAgent1(string data)
        {
            if (asset.Delete)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    int count = 0;
                    int total = 0;
                    string message = "";
                    string[] separators = { ";;" };
                    var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    var check = new DC_TeleSale_Agent_Region();
                    foreach (var id in listdata)
                    {
                        try
                        {
                            check.Id = int.Parse(id);
                            var datacheck = dbConn.Select<DC_TeleSale_Agent_Region>("Id={0}", id).FirstOrDefault();
                            dbConn.Delete(check);
                            total++;
                        }
                        catch (Exception ex)
                        {
                            message = ex.Message;
                            count = 1;
                        }
                    }
                    return Json(new { error = count, success = total, message = message });
                }
            }
            else
            {
                return Json(new { error = 1, success = 0, message = "You don't have permisson to Delete" });
            }
        }
    }
}
