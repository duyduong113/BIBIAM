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
    public class TelesalesOrganizationController : CustomController
    {
     

        //
        // GET: /TelesalePluginCode/
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    OrmLiteConfig.DialectProvider.UseUnicode = true;
            //    dbConn.DropTables(typeof(DC_Org_AvoidCallingTime));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(DC_Org_AvoidCallingTime));
            //}
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                ViewBag.AVoidCallTime = dbConn.Select<DC_AvoidCallingTimeFrame>("SELECT * FROM DC_AvoidCallingTimeFrame");
            }
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                ViewData["Asset"] = asset;
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewData["UserGroups"] = dbConn.Select<Groups>();
                }
                ViewBag.listAvoidCallTime = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='AvoidCallTime'", "").OrderBy(s => s.CodeID);
                ViewBag.listAllowedServices = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='AllowedServices'", "").OrderBy(s => s.CodeID);
                ViewBag.listCollectionType = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='CollectionType'", "").OrderBy(s => s.CodeID);
                ViewBag.listRegion = DC_Location_Region.GetList_Regions();
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult Read_OrganizationForTeleSale([DataSourceRequest] DataSourceRequest request)
        {
            var data = new List<DW_DC_Organization>();
            //if (request.Filters.Any())
            //{
            //var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
            data = DW_DC_Organization.GetListOrganizationForTeleSale().ToList();
            //}
            //else
            //{
            //    data = DW_DC_Organization.GetListOrganizationForTeleSale().ToList();
            //}
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult Read_Org_AvoidCallingTime([DataSourceRequest] DataSourceRequest request, string OrganizationID)
        {
            if (asset.View)
            {
                var data = new List<DC_Org_AvoidCallingTime>();
                data = DC_Org_AvoidCallingTime.GetDC_Org_AvoidCallingTime(OrganizationID);
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult GetTimeFrameDetail([DataSourceRequest] DataSourceRequest request, string Avoidcalltime)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<DC_DetailAvoidCallingTimeFrame>("SELECT *, CONVERT(NVARCHAR, FromHour) AS FromHourCV, CONVERT(NVARCHAR, ToHour) AS ToHourCV FROM DC_DetailAvoidCallingTimeFrame  WHERE HeaderID = '" + Avoidcalltime + "'");
                return Json(data.ToDataSourceResult(request));
            }
        }

        public ActionResult Save_Org_AVoidCallingTime(string AvoidCallTime, string TimeFrameDetail, string OrganizationID)
        {
            if (asset.Create)
            {
                if (!string.IsNullOrEmpty(AvoidCallTime) && !string.IsNullOrEmpty(TimeFrameDetail))
                {
                    try
                    {
                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                        {
                            var checkexist = dbConn.Select<DC_Org_AvoidCallingTime>("SELECT * FROM DC_Org_AvoidCallingTime WHERE HeaderID = " + AvoidCallTime + " AND DetailHeaderID = " + TimeFrameDetail + " AND OrgID = '" + OrganizationID + "'");
                            if (checkexist.Count <= 0)
                            {
                                var data = new DC_Org_AvoidCallingTime();
                                data.OrgID = OrganizationID;
                                data.HeaderID = AvoidCallTime;
                                data.DetailHeaderID = TimeFrameDetail;
                                data.RowCreatedTime = DateTime.Now;
                                data.RowCreatedUser = currentUser.UserName;
                                data.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                                data.RowLastUpdatedUser = "";
                                dbConn.Save(data);
                                dbTrans.Commit();
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, error = "Avoid Call Time is exists." });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, error = ex });
                    }
                }
                else
                {
                    return Json(new { success = false, error = "Please input field (*)" });
                }
            }
            else
            {
                return Json(new { success = false, error = "You don't have permission to add record" });
            }
        }

        public ActionResult Delete_Org_AvoidCallingTime(string data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Delete)
                {
                    int success = 0;
                    int error = 0;
                    try
                    {
                        string[] separators = { "@@" };
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        var data1 = new DC_Org_AvoidCallingTime();
                        foreach (var item in listRowID)
                        {
                            data1.Id = Int32.Parse(item);
                            dbConn.Delete(data1);
                        }
                        dbTrans.Commit();
                        success++;
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, alert = ex.Message });
                    }
                    return Json(new { success = true, totalSuccess = success, totalError = error });
                }
                else
                {
                    return Json(new { success = false, alert = "You don't have permission to delete record" });
                }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Organization_Update([DataSourceRequest]
                                            DataSourceRequest request, [Bind(Prefix = "models")]
                                            IEnumerable<DW_DC_Organization> listOrg)
        {
            if (asset.Update)
            {
                if (listOrg != null && ModelState.IsValid)
                {
                    foreach (var organization in listOrg)
                    {
                        if (!string.IsNullOrEmpty(organization.KeyPerson))
                        {
                            var d = DC_Organization_Meta.GetDC_Organization_Meta(organization.OrganizationID, "KeyPerson");
                            if (d != null)
                            {
                                d.Delete();
                            }

                            DC_Organization_Meta orgMeta = new DC_Organization_Meta();
                            orgMeta.OrganizationID = organization.OrganizationID;
                            orgMeta.Factor = "KeyPerson";
                            orgMeta.Value = organization.KeyPerson;
                            orgMeta.RowCreatedTime = DateTime.Now;
                            orgMeta.RowCreatedUser = currentUser.UserName;
                            orgMeta.Save();
                        }

                        if (!string.IsNullOrEmpty(organization.Branch))
                        {
                            var d = DC_Organization_Meta.GetDC_Organization_Meta(organization.OrganizationID, "Branch");
                            if (d != null)
                            {
                                d.Delete();
                            }

                            DC_Organization_Meta orgMeta = new DC_Organization_Meta();
                            orgMeta.OrganizationID = organization.OrganizationID;
                            orgMeta.Factor = "Branch";
                            orgMeta.Value = organization.Branch;
                            orgMeta.RowCreatedTime = DateTime.Now;
                            orgMeta.RowCreatedUser = currentUser.UserName;
                            orgMeta.Save();
                        }

                        if (!string.IsNullOrEmpty(organization.OnsiteInfo))
                        {
                            var d = DC_Organization_Meta.GetDC_Organization_Meta(organization.OrganizationID, "OnsiteInfo");
                            if (d != null)
                            {
                                d.Delete();
                            }

                            DC_Organization_Meta orgMeta = new DC_Organization_Meta();
                            orgMeta.OrganizationID = organization.OrganizationID;
                            orgMeta.Factor = "OnsiteInfo";
                            orgMeta.Value = organization.OnsiteInfo;
                            orgMeta.RowCreatedTime = DateTime.Now;
                            orgMeta.RowCreatedUser = currentUser.UserName;
                            orgMeta.Save();
                        }

                        if (!string.IsNullOrEmpty(organization.CollectionType))
                        {
                            var d = DC_Organization_Meta.GetDC_Organization_Meta(organization.OrganizationID, "CollectionType");
                            if (d != null)
                            {
                                d.Delete();
                            }

                            DC_Organization_Meta orgMeta = new DC_Organization_Meta();
                            orgMeta.OrganizationID = organization.OrganizationID;
                            orgMeta.Factor = "CollectionType";
                            orgMeta.Value = organization.CollectionType;
                            orgMeta.RowCreatedTime = DateTime.Now;
                            orgMeta.RowCreatedUser = currentUser.UserName;
                            orgMeta.Save();
                        }

                        if (!string.IsNullOrEmpty(organization.AllowService))
                        {
                            var d = DC_Organization_Meta.GetDC_Organization_Meta(organization.OrganizationID, "AllowService");
                            if (d != null)
                            {
                                d.Delete();
                            }

                            DC_Organization_Meta orgMeta = new DC_Organization_Meta();
                            orgMeta.OrganizationID = organization.OrganizationID;
                            orgMeta.Factor = "AllowService";
                            orgMeta.Value = organization.AllowService;
                            orgMeta.RowCreatedTime = DateTime.Now;
                            orgMeta.RowCreatedUser = currentUser.UserName;
                            orgMeta.Save();
                        }

                        if (!string.IsNullOrEmpty(organization.AvoidCallTime))
                        {
                            var d = DC_Organization_Meta.GetDC_Organization_Meta(organization.OrganizationID, "AvoidCallTime");
                            if (d != null)
                            {
                                d.Delete();
                            }

                            DC_Organization_Meta orgMeta = new DC_Organization_Meta();
                            orgMeta.OrganizationID = organization.OrganizationID;
                            orgMeta.Factor = "AvoidCallTime";
                            orgMeta.Value = organization.AvoidCallTime;
                            orgMeta.RowCreatedTime = DateTime.Now;
                            orgMeta.RowCreatedUser = currentUser.UserName;
                            orgMeta.Save();
                        }

                        //Allow Services
                        if (!string.IsNullOrEmpty(organization.AllowAirTime.ToString()))
                        {
                            var d = DC_Organization_Meta.GetDC_Organization_Meta(organization.OrganizationID, "AllowAirTime");
                            if (d != null)
                            {
                                d.Delete();
                            }

                            DC_Organization_Meta orgMeta = new DC_Organization_Meta();
                            orgMeta.OrganizationID = organization.OrganizationID;
                            orgMeta.Factor = "AllowAirTime";
                            orgMeta.Value = organization.AllowAirTime.ToString();
                            orgMeta.RowCreatedTime = DateTime.Now;
                            orgMeta.RowCreatedUser = currentUser.UserName;
                            orgMeta.Save();
                        }

                        if (!string.IsNullOrEmpty(organization.AllowCash2Home.ToString()))
                        {
                            var d = DC_Organization_Meta.GetDC_Organization_Meta(organization.OrganizationID, "AllowCash2Home");
                            if (d != null)
                            {
                                d.Delete();
                            }

                            DC_Organization_Meta orgMeta = new DC_Organization_Meta();
                            orgMeta.OrganizationID = organization.OrganizationID;
                            orgMeta.Factor = "AllowCash2Home";
                            orgMeta.Value = organization.AllowCash2Home.ToString();
                            orgMeta.RowCreatedTime = DateTime.Now;
                            orgMeta.RowCreatedUser = currentUser.UserName;
                            orgMeta.Save();
                        }

                        if (!string.IsNullOrEmpty(organization.AllowServices.ToString()))
                        {
                            var d = DC_Organization_Meta.GetDC_Organization_Meta(organization.OrganizationID, "AllowServices");
                            if (d != null)
                            {
                                d.Delete();
                            }

                            DC_Organization_Meta orgMeta = new DC_Organization_Meta();
                            orgMeta.OrganizationID = organization.OrganizationID;
                            orgMeta.Factor = "AllowServices";
                            orgMeta.Value = organization.AllowServices.ToString();
                            orgMeta.RowCreatedTime = DateTime.Now;
                            orgMeta.RowCreatedUser = currentUser.UserName;
                            orgMeta.Save();
                        }

                        if (!string.IsNullOrEmpty(organization.AllowPhysicalGoods.ToString()))
                        {
                            var d = DC_Organization_Meta.GetDC_Organization_Meta(organization.OrganizationID, "AllowPhysicalGoods");
                            if (d != null)
                            {
                                d.Delete();
                            }

                            DC_Organization_Meta orgMeta = new DC_Organization_Meta();
                            orgMeta.OrganizationID = organization.OrganizationID;
                            orgMeta.Factor = "AllowPhysicalGoods";
                            orgMeta.Value = organization.AllowPhysicalGoods.ToString();
                            orgMeta.RowCreatedTime = DateTime.Now;
                            orgMeta.RowCreatedUser = currentUser.UserName;
                            orgMeta.Save();
                        }


                        if (!string.IsNullOrEmpty(organization.TeleSaleNote))
                        {
                            var d = DC_Organization_Meta.GetDC_Organization_Meta(organization.OrganizationID, "TeleSaleNote");
                            if (d != null)
                            {
                                d.Delete();
                            }

                            DC_Organization_Meta orgMeta = new DC_Organization_Meta();
                            orgMeta.OrganizationID = organization.OrganizationID;
                            orgMeta.Factor = "TeleSaleNote";
                            orgMeta.Value = organization.TeleSaleNote;
                            orgMeta.RowCreatedTime = DateTime.Now;
                            orgMeta.RowCreatedUser = currentUser.UserName;
                            orgMeta.Save();
                        }

                        //if (!string.IsNullOrEmpty(organization.DeliveryNote))
                        //{
                        //    var d = DC_Organization_Meta.GetDC_Organization_Meta(organization.OrganizationID, "deliverynote");
                        //    if (d != null)
                        //    {
                        //        d.Delete();
                        //    }

                        //    DC_Organization_Meta orgMeta = new DC_Organization_Meta();
                        //    orgMeta.OrganizationID = organization.OrganizationID;
                        //    orgMeta.Factor = "deliverynote";
                        //    orgMeta.Value = organization.DeliveryNote;
                        //    orgMeta.RowCreatedTime = DateTime.Now;
                        //    orgMeta.RowCreatedUser = currentUser.UserName;
                        //    orgMeta.Save();
                        //}

                        if (!string.IsNullOrEmpty(organization.SalesPriority.ToString()))
                        {
                            var d = DC_Organization_Meta.GetDC_Organization_Meta(organization.OrganizationID, "SalesPriority");
                            if (d != null)
                            {
                                d.Delete();
                            }

                            DC_Organization_Meta orgMeta = new DC_Organization_Meta();
                            orgMeta.OrganizationID = organization.OrganizationID;
                            orgMeta.Factor = "SalesPriority";
                            orgMeta.Value = organization.SalesPriority.ToString();
                            orgMeta.RowCreatedTime = DateTime.Now;
                            orgMeta.RowCreatedUser = currentUser.UserName;
                            orgMeta.Save();
                        }

                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(new[] { listOrg }.ToDataSourceResult(new DataSourceRequest(), ModelState));
            }
            return Json(new[] { listOrg }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }


        public ActionResult Export([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.Export)
            {

                var data = DW_DC_Organization.GetListOrganizationForTeleSale().ToList();
                IEnumerable datas = data.ToDataSourceResult(request).Data;

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_OrganizationForTeleSale.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["DC_OrganizationForTeleSale"];
                int rowData = 1;
                foreach (DW_DC_Organization item in datas)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = item.OrganizationID;
                    dataSheet.Cells[rowData, i++].Value = item.LongName;
                    dataSheet.Cells[rowData, i++].Value = item.Address;

                    if (string.IsNullOrEmpty(item.CheckAllowServices))
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    else
                    {
                        if (item.CheckAllowServices == "1")
                        {
                            dataSheet.Cells[rowData, i++].Value = "Airtime";
                        }
                        else if (item.CheckAllowServices == "2")
                        {
                            dataSheet.Cells[rowData, i++].Value = "Airtime, Cashdvance, Physical goods, Services";
                        }
                        else
                        {
                            dataSheet.Cells[rowData, i++].Value = "Airtime, Physical goods, Services";
                        }
                    }



                    dataSheet.Cells[rowData, i++].Value = item.SalesPriority;
                    if (!string.IsNullOrEmpty(item.CollectionType))
                    {
                        var data1 = Deca_Code_Master.GetDeca_Code_Masters("[CodeID] = '" + item.CollectionType + "'", "").FirstOrDefault();
                        dataSheet.Cells[rowData, i++].Value = data1.Description;
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = item.TeleSaleNote;
                    dataSheet.Cells[rowData, i++].Value = item.KeyPerson;
                    dataSheet.Cells[rowData, i++].Value = item.OnsiteInfo;
                    dataSheet.Cells[rowData, i++].Value = item.DeliveryNote;
                    dataSheet.Cells[rowData, i++].Value = item.CreditLimitRules;
                    dataSheet.Cells[rowData, i++].Value = item.SettlementDate;
                    dataSheet.Cells[rowData, i++].Value = item.DeliveryNote;
                    dataSheet.Cells[rowData, i++].Value = item.Note;
                    dataSheet.Cells[rowData, i++].Value = item.RegionalBD;
                    dataSheet.Cells[rowData, i++].Value = item.DueDate;
                }

                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_OrganizationForTeleSale_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to export data");
                return File("", //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "DC_TelesaleAgent" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
        }
    }
}
