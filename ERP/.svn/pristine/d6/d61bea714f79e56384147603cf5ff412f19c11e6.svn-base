using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Data;
using OfficeOpenXml;

namespace ERPAPD.Controllers
{
    public class MappingLocationController : CustomController
    {
        //


        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                ViewBag.Alias = DC_Location_Alias.GetDC_Location_Alias("1=1", "").ToList();
                ViewBag.Country = DC_Location_Countries.GetDC_Location_Countries("1=1", "").ToList();
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult MappingLocation_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = new List<DC_Location_MappingLocation>();
            data = DC_Location_MappingLocation.GetAllDC_Location_Regions();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult Mapping(string id)
        {
            var hOld = DC_Location_MappingLocation.GetAllDC_Location_Regions().Where(s => s.RegionID == id).FirstOrDefault();
            if (hOld != null)
            {
                ViewBag.hOld = id;
                ViewBag.Region = DC_Location_MappingLocation.GetAllDC_Location_Regions();
                return View(hOld);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult getInfoByRegion(string RegionID)
        {
            var data = new DC_Location_MappingLocation();
            data = DC_Location_MappingLocation.GetAllDC_Location_Regions().Where(s => s.RegionID == RegionID).FirstOrDefault();
            return Json(data);
            //return Json(data.ToDataSourceResult(request));
        }
        public ActionResult ListShippingCityAll_Read([DataSourceRequest] DataSourceRequest request, string RegionID)
        {
            var data = new List<DC_Location_MappingLocation>();
            data = DC_Location_MappingLocation.GetAllDC_Location_Mapping_City(RegionID).ToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult ListRegionMappingedAll_Read([DataSourceRequest] DataSourceRequest request, string RegionID)
        {
            var data = new List<DC_Location_City>();
            data = DC_Location_City.GetDC_Location_Citys("1=1", "").Where(s => s.RegionID == RegionID).ToList();
            return Json(data.ToDataSourceResult(request));
        }
        //public ActionResult ListPortalCityAll_Read([DataSourceRequest] DataSourceRequest request, string RegionID)
        //{
        //    var data = new List<DC_Location_MappingLocation>();
        //    data = DC_Location_MappingLocation.GetAllDC_Location_Portal_City(RegionID).ToList();
        //    return Json(data.ToDataSourceResult(request));
        //}
        public ActionResult SaveMappingCity(string data, string RegionID)
        {
            if (asset.Create)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    string id = "";
                    //var check = new DC_Location_MappingLocation();
                    var write = new DC_Location_City();
                    foreach (var item in listRowID)
                    {
                        //var checkClass = DC_Location_MappingLocation.GetAllDC_Location_Mappinged_City().Where(s => s.RegionID == RegionID && s.CityName == item.Trim()).FirstOrDefault();
                        //if (checkClass != null)
                        //{
                        //    ModelState.AddModelError("Error", "City exists");
                        //    return Json(new { success = false, alert = item + " exists" });
                        //}
                        //var checkID = DC_Location_MappingLocation.GetAllDC_Location_Mapping_Citys().OrderByDescending(m => m.RowID).FirstOrDefault();
                        //if (checkID != null )
                        //{
                        //    var nextNo = Int32.Parse(checkID.CityID.Substring(1, checkID.CityID.Length - 1)) + 1;
                        //    id = "A" + String.Format("{0:0000}", nextNo);
                        //}
                        //else
                        //{
                        //    id = "A0001";
                        //}
                        //check.CityID = id;
                        //check.CityName = item.Trim();
                        //check.RowCreatedUser = currentUser.UserName;
                        //check.RowCreatedTime = DateTime.Now;
                        //check.RegionID = RegionID;
                        //check.Active = true;
                        //check.Save();
                        var checkClass = DC_Location_City.GetDC_Location_Citys("1=1", "").Where(s => s.RegionID == RegionID && s.CityName == item.Trim()).FirstOrDefault();
                        if (checkClass != null)
                        {
                            ModelState.AddModelError("Error", "City exists");
                            return Json(new { success = false, alert = item + " exists City" });
                        }
                        var checkID = DC_Location_City.GetDC_Location_Citys("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                        if (checkID != null)
                        {
                            var nextNo = Int32.Parse(checkID.CityID.Substring(1, checkID.CityID.Length - 1)) + 1;
                            id = "A" + String.Format("{0:0000}", nextNo);
                        }
                        else
                        {
                            id = "A0001";
                        }
                        write.CityID = id;
                        write.CityName = item.Trim();
                        write.Active = true;
                        write.RowCreatedTime = DateTime.Now;
                        write.RowCreatedUser = currentUser.UserName;
                        write.RegionID = RegionID != null ? RegionID : "";
                        write.Save();
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return Json(new { success = false });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }
        public ActionResult DeleteMappingCity(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        var check = new DC_Location_City();
                        check.CityID = item;
                        check.Delete();
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Error", e.Message);
                    return Json(new { success = false });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }
        public FileResult ExportExcelMapping([DataSourceRequest]DataSourceRequest request, string RegionID)
        {
            if (asset.Export)
            {
                var Alias = DC_Location_MappingLocation.GetAllDC_Location_Mapping_City(RegionID).ToList();
                var portal = DC_Location_MappingLocation.GetAllDC_Location_Portal_City(RegionID).ToList();
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Location_Mapping_City.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["MappingCity"];

                IEnumerable listData = Alias.ToDataSourceResult(request).Data;
                IEnumerable listData_portal = portal.ToDataSourceResult(request).Data;
                int rowData = 1;
                int rowNumber = 1;
                int index_tmp = 0;
                var rowtemp = 1;
                foreach (DC_Location_MappingLocation data in listData)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.ShippingCity;
                    dataSheet.Cells[rowData, i++].Value = " MCA Shipping City";
                    rowtemp = rowData;
                }
                //foreach (DC_Location_MappingLocation data_portal in listData_portal)
                //{
                //    int i = 1;
                //    rowtemp++;
                //    dataSheet.Cells[rowtemp, i++].Value = data_portal.CityName;
                //    dataSheet.Cells[rowtemp, i++].Value = "Portal City";
                //}
                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_Location_Mapping_City_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                string fileName = "DC_Location_Mapping_City_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentType, fileName);
            }
        }
        public FileResult ExportExcelAllMappinged([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                var ShippingCity = DC_Location_MappingLocation.GetAllListCityMappingFrom_ShippingCity().ToList();
                var PortalCity = DC_Location_MappingLocation.GetAllListCityMappingFrom_PortalCity().ToList();
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Location_MappingRegionCity.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);
                //ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["DC_Location_Mapping_City"];
                //IEnumerable listData = ShippingCity.ToDataSourceResult(request).Data;
                //int rowData = 1;
                //foreach (DC_Location_MappingLocation data in listData)
                //{
                //    int i = 1;
                //    rowData++;
                //    dataSheet.Cells[rowData, i++].Value = data.RegionName;
                //    dataSheet.Cells[rowData, i++].Value = data.ShippingCity;
                //    dataSheet.Cells[rowData, i++].Value = "MCA Shipping City";
                //}
                ExcelWorksheet dataSheetPortal = excelPkg.Workbook.Worksheets["DC_Location_Mapping_City"];
                IEnumerable listPortalCity = PortalCity.ToDataSourceResult(request).Data;
                int rowDataMCA = 1;
                foreach (DC_Location_MappingLocation data in listPortalCity)
                {
                    int i = 1;
                    rowDataMCA++;
                    //dataSheetPortal.Cells[rowDataMCA, i++].Value = data.RegionName;
                    dataSheetPortal.Cells[rowDataMCA, i++].Value = data.RegionName;
                    dataSheetPortal.Cells[rowDataMCA, i++].Value = data.CityName;
                    //dataSheetPortal.Cells[rowDataMCA, i++].Value = "Portal City";
                }
                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_Location_MappingRegionCity_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                string fileName = "DC_Location_MappingRegionCity_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentType, fileName);
            }
        }
        public FileResult ExportAllListWaitingMapping([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                var ShippingCity = DC_Location_MappingLocation.GetAllListWaitingMapping_ShippingCity().ToList();
                var PortalCity = DC_Location_MappingLocation.GetAllListWaitingMapping_ShippingPortal().ToList();
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Location_MappingRegionCity.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["DC_Location_Mapping_City"];
                IEnumerable listData = ShippingCity.ToDataSourceResult(request).Data;
                int rowData = 1;
                foreach (DC_Location_MappingLocation data in listData)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.RegionName;
                    dataSheet.Cells[rowData, i++].Value = data.ShippingCity;
                    dataSheet.Cells[rowData, i++].Value = "MCA Shipping City";
                }
                ExcelWorksheet dataSheetPortal = excelPkg.Workbook.Worksheets["DC_Location_Portal_City"];
                IEnumerable listPortalCity = PortalCity.ToDataSourceResult(request).Data;
                int rowDataMCA = 1;
                foreach (DC_Location_MappingLocation data in listPortalCity)
                {
                    int i = 1;
                    rowDataMCA++;
                    dataSheetPortal.Cells[rowDataMCA, i++].Value = data.RegionName;
                    dataSheetPortal.Cells[rowDataMCA, i++].Value = data.CityName;
                    dataSheetPortal.Cells[rowDataMCA, i++].Value = "Portal City";
                }
                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_Location_MappingRegionCity_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                string fileName = "DC_Location_MappingRegionCity_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentType, fileName);
            }
        }

        public FileResult ExportExcel([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                var list = DC_Location_MappingLocation.GetAllDC_Location_Mappinged_City().ToList();

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Location_MappingLocation.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);

                //data sheet
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["DC_Location_MappingLocation"];

                IEnumerable listData = list.ToDataSourceResult(request).Data;

                int rowData = 1;
                foreach (DC_Location_MappingLocation data in listData)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.CityID;
                    dataSheet.Cells[rowData, i++].Value = data.CityName;
                    dataSheet.Cells[rowData, i++].Value = data.RegionID + " - " + data.RegionName;

                }


                //data sheet
                var region = DC_Location_Region.GetAllDC_Location_Regions();
                ExcelWorksheet dataSheetList = excelPkg.Workbook.Worksheets["List"];

                IEnumerable listDataRegion = region.ToDataSourceResult(request).Data;

                int rowDataRegion = 1;
                foreach (DC_Location_Region data in listDataRegion)
                {
                    int i = 1;
                    rowDataRegion++;
                    dataSheetList.Cells[rowDataRegion, i++].Value = data.RegionID + " - " + data.RegionName;
                }

                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_Location_MappingLocation_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                string fileName = "DC_Location_MappingLocation_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentType, fileName);
            }

        }

        //public ActionResult ImportFromExcel()
        //{
        //    try
        //    {
        //        if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
        //        {
        //            string fileExtension =
        //                System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

        //            if (fileExtension == ".xlsx")
        //            {
        //                string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);
        //                string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUpload"].FileName);

        //                if (System.IO.File.Exists(fileLocation))
        //                    System.IO.File.Delete(fileLocation);

        //                Request.Files["FileUpload"].SaveAs(fileLocation);
        //                //Request.Files["fileUpload"].SaveAs(errorFileLocation);

        //                var rownumber = 2;
        //                var total = 0;

        //                FileInfo fileInfo = new FileInfo(fileLocation);
        //                var excelPkg = new ExcelPackage(fileInfo);

        //                FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Location_MappingLocation.xlsx"));
        //                template.CopyTo(errorFileLocation);
        //                FileInfo _fileInfo = new FileInfo(errorFileLocation);
        //                var _excelPkg = new ExcelPackage(_fileInfo);

        //                ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["DC_Location_MappingLocation"];
        //                ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["DC_Location_MappingLocation"];

        //                //remove row
        //                int totalRows = oSheet.Dimension.End.Row;
        //                //eSheet.DeleteRow(2, totalRows, true);

        //                //var realdata = WorksheetToDataTable(oSheet);


        //                //for (int i = 2; i <= totalRows; i++)

        //                var listPo = new List<DC_Location_ICare_Center_Staff>();

        //                for (int i = 2; i <= totalRows; i++)
        //                {

        //                    // get data sheet Staff
        //                    string cityID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
        //                    string cityName = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
        //                    string regionID = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
        //                    string active = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "TRUE";

        //                    string id = !String.IsNullOrEmpty(regionID) ? regionID.Substring(0, regionID.LastIndexOf("-")).Trim() : "";


        //                    try
        //                    {

        //                        var checkExist = DC_Location_MappingLocation.checkRegionANDCityName(id, cityName).FirstOrDefault();
        //                        if (string.IsNullOrEmpty(cityName.ToString()) || string.IsNullOrEmpty(regionID.ToString()))
        //                        {
        //                            eSheet.Cells[rownumber, 2].Value = cityName;
        //                            eSheet.Cells[rownumber, 3].Value = regionID;
        //                            eSheet.Cells[rownumber, 9].Value = "cityName, regionID required";
        //                            rownumber++;
        //                        }
        //                        else if (checkExist != null)
        //                        {
        //                            eSheet.Cells[rownumber, 2].Value = cityName;
        //                            eSheet.Cells[rownumber, 3].Value = regionID;
        //                            eSheet.Cells[rownumber, 9].Value = "cityName, regionID exist in sytem";
        //                            rownumber++;
        //                        }
        //                        else
        //                        {
        //                            string id2 = "";
        //                            var check = new DC_Location_MappingLocation();

        //                            var checkID = DC_Location_MappingLocation.GetAllDC_Location_Mapping_Citys().OrderByDescending(m => m.RowID).FirstOrDefault();
        //                            if (checkID != null)
        //                            {
        //                                var nextNo = Int32.Parse(checkID.CityID.Substring(1, checkID.CityID.Length - 1)) + 1;
        //                                id2 = "A" + String.Format("{0:0000}", nextNo);
        //                            }
        //                            else
        //                            {
        //                                id2 = "A0001";
        //                            }
        //                            check.CityID = id2;
        //                            check.CityName = cityName;
        //                            check.RowCreatedUser = currentUser.UserName;
        //                            check.RowCreatedTime = DateTime.Now;
        //                            check.RegionID = regionID;
        //                            check.Active = true;
        //                            check.Save();
        //                            total++;
        //                        }
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        eSheet.Cells[rownumber, 2].Value = cityName;
        //                        eSheet.Cells[rownumber, 3].Value = regionID;
        //                        eSheet.Cells[rownumber, 9].Value = e.Message;
        //                        rownumber++;
        //                        continue;
        //                    }
        //                }
        //                _excelPkg.Save();

        //                return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
        //            }
        //            else
        //            {
        //                return Json(new { success = false, error = "File extension is not valid. *.xlsx please." });
        //            }
        //        }
        //        else
        //        {
        //            return Json(new { success = false, error = "File upload null" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, error = ex.Message });
        //    }
        //}
    }
}
