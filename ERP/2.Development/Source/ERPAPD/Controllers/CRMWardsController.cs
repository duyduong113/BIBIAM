using ERPAPD.Helpers;
using ERPAPD.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using Kendo.Mvc.Extensions;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class CRMWardsController : CustomController
    {
        // GET: Wards
        public ActionResult Index()
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        ViewData["AllowCreate"] = asset.Create;
                        ViewData["AllowUpdate"] = asset.Update;
                        ViewData["AllowDelete"] = asset.Delete;
                        ViewData["AllowExport"] = asset.Export;
                   
                        ViewBag.listDistrict = dbConn.Select<CRM_Location_District>("Active={0}",1);
                        ViewBag.listCity = dbConn.Select<CRM_Location_City>("Active={0}", 1).OrderByDescending(o => o.RegionName);
                        ViewBag.listRegion = dbConn.Select<CRM_Location_Region>("Active={0}", 1);
                        ViewBag.listCountry = dbConn.Select<CRM_Location_Countries>("SELECT CountryID,CountryName FROM CRM_Location_Countries");
                    }
                    catch (Exception) { }
                    finally { dbConn.Close(); }
                 }

                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult Wards_Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT   w.*
		                                        ,ISNULL(c.CityID,'') AS CityID
		                                        ,ISNULL(r.RegionID,'') AS RegionID
		                                        ,ISNULL(co.CountryID,'') AS CountryID
                                        FROM [CRM_Location_Wards] w
	                                    LEFT JOIN CRM_Location_District d ON d.DistrictID = w.DistrictID
	                                    LEFT JOIN CRM_Location_City c ON c.CityID = d.CityID
	                                    LEFT JOIN CRM_Location_Region r ON r.RegionID = c.RegionID
	                                    LEFT JOIN CRM_Location_Countries co ON co.CountryID = r.CountryID
                                         ";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_Location_Wards>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult Wards_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Location_Wards> listEx)
        {
            if (asset.Create)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listEx != null)
                        {
                            foreach (var regis in listEx)
                            {
                                if (String.IsNullOrEmpty(regis.WardsName))
                                {
                                    ModelState.AddModelError("", "Vui nhập tên Phường/Xã");
                                    return Json(listEx.ToDataSourceResult(request, ModelState));
                                }
                                if (String.IsNullOrEmpty(regis.DistrictID))
                                {
                                    ModelState.AddModelError("", "Vui lòng chọn Quận/Huyện");
                                    return Json(listEx.ToDataSourceResult(request, ModelState));
                                }

                                string id = "";
                                var wards = new CRM_Location_Wards();

                                var checkID = dbConn.Select<CRM_Location_Wards>().OrderByDescending(m => m.RowCreatedTime).FirstOrDefault();
                                if (checkID != null)
                                {
                                    var nextNo = Int32.Parse(checkID.WardsID.Substring(1, checkID.WardsID.Length - 1)) + 1;
                                    id = "W" + String.Format("{0:0000}", nextNo);
                                }
                                else
                                {
                                    id = "W0001";
                                }

                                var check = dbConn.FirstOrDefault<CRM_Location_Wards>("WardsName COLLATE Latin1_General_CI_AI LIKE {0} AND DistrictID = {1}", regis.WardsName, regis.DistrictID);
                                if (check != null)
                                {
                                    ModelState.AddModelError("", " Phường/Xã đã tồn tại trong Quận/Huyện này");
                                    return Json(listEx.ToDataSourceResult(request, ModelState));
                                }

                                wards.WardsID = id;
                                wards.WardsName = regis.WardsName.Trim();
                                wards.DistrictID = regis.DistrictID != null ? regis.DistrictID : "";
                                wards.Active = regis.Active;
                                wards.RowLastUpdatedTime = DateTime.Parse("1900-01-01");
                                wards.RowLastUpdatedUser = "";
                                wards.RowCreatedTime = DateTime.Now;
                                wards.RowCreatedUser = currentUser.UserName;

                                dbConn.Insert(wards);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        return Json(listEx.ToDataSourceResult(request, ModelState));
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listEx.ToDataSourceResult(request, ModelState));
            }
            return Json(listEx.ToDataSourceResult(request));
        }
        public ActionResult Wards_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Location_Wards> listEx)
        {
            if (asset.Create)
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    try
                    {
                        if (listEx != null)
                        {
                            foreach (var regis in listEx)
                            {
                                if (String.IsNullOrEmpty(regis.WardsName))
                                {
                                    ModelState.AddModelError("", "Vui nhập tên Phường/Xã");
                                    return Json(listEx.ToDataSourceResult(request, ModelState));
                                }

                                var item = dbConn.FirstOrDefault<CRM_Location_Wards>("WardsID={0}", regis.WardsID);
                                //var check = dbConn.FirstOrDefault<CRM_Location_Wards>("WardsName COLLATE Latin1_General_CI_AI LIKE {0} AND DistrictID = {1}", regis.WardsName, regis.DistrictID);
                                //if (check != null)
                                //{
                                //    ModelState.AddModelError("", " Phường/Xã đã tồn tại trong Quận/Huyện này");
                                //    return Json(listEx.ToDataSourceResult(request, ModelState));
                                //}
                                item.WardsID = regis.WardsID;
                                item.WardsName = regis.WardsName.Trim();
                                item.Active = regis.Active;
                                
                                item.RowLastUpdatedTime = DateTime.Now;
                                item.RowLastUpdatedUser = currentUser.UserName;
                                item.DistrictID = regis.DistrictID != null ? regis.DistrictID : "";
                                dbConn.Update(item);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        return Json(listEx.ToDataSourceResult(request, ModelState));
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listEx.ToDataSourceResult(request, ModelState));
            }
            return Json(listEx.ToDataSourceResult(request));
        }
        public ActionResult Delete(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                    {
                        string[] separators = { "@@" };
                        var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listRowID)
                        {
                            var check = dbConn.FirstOrDefault<CRM_Location_Wards>("WardsID={0}", item);
                            dbConn.Delete(check);
                        }
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
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }
    }
}