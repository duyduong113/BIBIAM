using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using ERPAPD.Models;
using ServiceStack.OrmLite;
using ERPAPD.Helpers;
using Kendo.Mvc.UI;



namespace ERPAPD.Controllers
{
    public class MnDetailEmployerController : CustomController
    {
        // GET: MnDetailEmployer
        public ActionResult Index()
        {
            ViewBag.Title = "Thông tin chi tiết khách hàng";
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                var CustomerID = Request.QueryString["CustomerID"];
                var data = dbConn.SingleOrDefault<ERPAPD_Customer>("CustomerID= {0}", CustomerID);
                ViewBag.detailCustomer = data;
                if (ViewBag.detailCustomer != null)
                {
                    ViewBag.listCustomerPosition = dbConn.Select<CRM_Position>();
                    ViewBag.listCountry = dbConn.Select<CRM_Location_Countries>("SELECT CountryID,CountryName FROM CRM_Location_Countries");
                    //ViewBag.listType = dbConn.Select<ERPAPD_MasterData_Customer>("Active={0} AND Type ={1}", 1, "CustomerType");
                    ViewBag.listType = dbConn.Select<ERPAPD_MasterData_Customer>("Active={0} AND Type ={1}", 1, "CustomerType");
                    ViewBag.listCompanyType = dbConn.Select<ERPAPD_MasterData_Customer>("Active={0} AND Type ={1}", 1, "TypeOfBusiness");
                    ViewBag.listSource = dbConn.Select<Parameters>("Type ={0}", "CustomerSource");
                    ViewBag.listEmployee = dbConn.Select<ERPAPD_Employee>("SELECT RefEmployeeID,Name FROM ERPAPD_Employee");
                    // ViewBag.extentions = dbConn.SingleOrDefault<ERPAPD_Customer_Extensions>("CustomerID= {0}", CustomerID);
                    //var data1 = dbConn.Select<CRM_CategoryHierarchy>(@"SELECT ID,[HierarchyID],Value,ParentID,[Level] FROM CRM_CategoryHierarchy WHERE Level=1 ");
                    var data1 = dbConn.Select<CRM_CategoryHierarchy>(@"SELECT ID,[HierarchyID],Value,ParentID,[Level] FROM CRM_CategoryHierarchy Where Status=1 AND ID <> 1");
                    ViewBag.listCategory = data1;
                    ViewBag.listSubCategory = dbConn.Select<CRM_CategoryHierarchy>(@"SELECT ID,[HierarchyID],Value,ParentID,[Level] FROM CRM_CategoryHierarchy WHERE Status=1 AND Level = 2");
                    ViewBag.listGender = dbConn.Select<Parameters>(s => s.Type == "Gender");
                    //ViewBag.listExtention = dbConn.Select<CRM_ExtsInfo>(s => s.Type == "customer");

                    ViewBag.extentions_meta = dbConn.Select<CRM_ExtsInfo_Meta>("CustomerID= {0}", CustomerID);
                    //BaoHV add
                    ViewBag.listAgencyType = dbConn.Select<ERPAPD_List>(@"SELECT PKList,Code,Name FROM ERPAPD_List
																			  Where  FKListtype=57 AND Status=1");
                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;

                    if (!string.IsNullOrEmpty(ViewBag.detailCustomer.Country))
                    {
                        string Sql = "SELECT CityID,CityName FROM CRM_Location_City c LEFT JOIN CRM_Location_Region r ON r.RegionID = c.RegionID LEFT JOIN CRM_Location_Countries co ON co.CountryID = r.CountryID ";
                        ViewBag.listCity = dbConn.Select<CRM_Location_City>(Sql);
                    }
                    if (!string.IsNullOrEmpty(ViewBag.detailCustomer.Province))
                    {
                        string Sql = "SELECT DistrictID,DistrictName FROM CRM_Location_District  where CityID = '" + ViewBag.detailCustomer.Province + "'";
                        ViewBag.listDistrict = dbConn.Select<CRM_Location_District>(Sql);
                    }
                    if (!string.IsNullOrEmpty(ViewBag.detailCustomer.District))
                    {
                        string Sql = "SELECT WardsID,WardsName FROM CRM_Location_Wards  where DistrictID = '" + ViewBag.detailCustomer.District + "'";
                        ViewBag.listWards = dbConn.Select<CRM_Location_Wards>(Sql);
                    }
                    return View();
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
            }         
        }
        public ActionResult changeArea(string country, string city, string district)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                ViewBag.listCountry = dbConn.Select<CRM_Location_Countries>("SELECT CountryID,CountryName FROM CRM_Location_Countries");
                ViewBag.country = country;
                ViewBag.city = city;
                ViewBag.district = district;

                string Sql;
                if (!string.IsNullOrEmpty(country) && string.IsNullOrEmpty(city) && string.IsNullOrEmpty(district))
                {
                    Sql = "SELECT CityID,CityName FROM CRM_Location_City c LEFT JOIN CRM_Location_Region r ON r.RegionID = c.RegionID LEFT JOIN CRM_Location_Countries co ON co.CountryID = r.CountryID WHERE co.CountryID = '" + country + "'";
                    ViewBag.listCity = dbConn.Select<CRM_Location_City>(Sql);
                    if (ViewBag.listCity.Count != 0)
                    {
                        var firstCity = ViewBag.listCity[0];
                        Sql = "SELECT DistrictID,DistrictName FROM CRM_Location_District  where CityID = '" + firstCity.CityID + "'";
                        ViewBag.listDistrict = dbConn.Select<CRM_Location_District>(Sql);
                    }
                    if (ViewBag.listDistrict.Count != 0)
                    {
                        var firstDistrict = ViewBag.listDistrict[0];
                        Sql = "SELECT WardsID,WardsName FROM CRM_Location_Wards  where DistrictID = '" + firstDistrict.DistrictID + "'";
                        ViewBag.listWards = dbConn.Select<CRM_Location_Wards>(Sql);
                    }
                }

                if (!string.IsNullOrEmpty(country) && !string.IsNullOrEmpty(city) && string.IsNullOrEmpty(district))
                {
                    Sql = "SELECT CityID,CityName FROM CRM_Location_City c LEFT JOIN CRM_Location_Region r ON r.RegionID = c.RegionID LEFT JOIN CRM_Location_Countries co ON co.CountryID = r.CountryID WHERE co.CountryID = '" + country + "'";
                    ViewBag.listCity = dbConn.Select<CRM_Location_City>(Sql);
                    Sql = "SELECT DistrictID,DistrictName FROM CRM_Location_District  where CityID = '" + city + "'";
                    ViewBag.listDistrict = dbConn.Select<CRM_Location_District>(Sql);
                    if (ViewBag.listDistrict.Count != 0)
                    {
                        var firstDistrict = ViewBag.listDistrict[0];
                        Sql = "SELECT WardsID,WardsName FROM CRM_Location_Wards  where DistrictID = '" + firstDistrict.DistrictID + "'";
                        ViewBag.listWards = dbConn.Select<CRM_Location_Wards>(Sql);
                    }
                }
                if (!string.IsNullOrEmpty(country) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(district))
                {
                    Sql = "SELECT CityID,CityName FROM CRM_Location_City c LEFT JOIN CRM_Location_Region r ON r.RegionID = c.RegionID LEFT JOIN CRM_Location_Countries co ON co.CountryID = r.CountryID WHERE co.CountryID = '" + country + "'";
                    ViewBag.listCity = dbConn.Select<CRM_Location_City>(Sql);
                    Sql = "SELECT DistrictID,DistrictName FROM CRM_Location_District  where CityID = '" + city + "'";
                    ViewBag.listDistrict = dbConn.Select<CRM_Location_District>(Sql);
                    Sql = "SELECT WardsID,WardsName FROM CRM_Location_Wards  where DistrictID = '" + district + "'";
                    ViewBag.listWards = dbConn.Select<CRM_Location_Wards>(Sql);
                }

            }
            return View("select_box");
            //return Json(new { data = view, success = true });
        }
        public ActionResult Customer_Update(ERPAPD_Customer request)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    if (!string.IsNullOrEmpty(request.CustomerID))
                    {

                        var customer = dbConn.FirstOrDefault<ERPAPD_Customer>("CustomerID={0}", request.CustomerID);

                        if (!string.IsNullOrEmpty(request.ShortName))
                        {
                            customer.ShortName = request.ShortName.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.CustomerName))
                        {
                            customer.CustomerName = request.CustomerName.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.TaxCode))
                        {
                            customer.TaxCode = request.TaxCode.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.Source))
                        {
                            customer.Source = request.Source.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.CompanyType))
                        {
                            customer.CompanyType = request.CompanyType.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.Category))
                        {
                            customer.Category = request.Category.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.CustomerType))
                        {
                            customer.CustomerType = request.CustomerType.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.Area))
                        {
                            customer.Area = request.Area.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.Country))
                        {
                            customer.Country = request.Country.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.District))
                        {
                            customer.District = request.District.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.Province))
                        {
                            customer.Province = request.Province.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.Wards))
                        {
                            customer.Wards = request.Wards.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.Address))
                        {
                            customer.Address = request.Address.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.Address2))
                        {
                            customer.Address2 = request.Address2.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.Phone))
                        {
                            customer.Phone = request.Phone.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.Phone2))
                        {
                            customer.Phone2 = request.Phone2.Trim();
                        }

                        if (!string.IsNullOrEmpty(request.StaffId.ToString())) 
                        {
                            customer.StaffId = int.Parse(request.StaffId.ToString());
                        }
                        if (!string.IsNullOrEmpty(request.Fax))
                        {
                            customer.Fax = request.Fax.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.BankCode))
                        {
                            customer.BankCode = request.BankCode.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.BankBranch))
                        {
                            customer.BankBranch = request.BankBranch.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.BankName))
                        {
                            customer.BankName = request.BankName.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.Sponsor))
                        {
                            customer.Sponsor = request.Sponsor.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.Position))
                        {
                            customer.Position = request.Position.Trim();
                        }
                        if (!string.IsNullOrEmpty(request.Website))
                        {
                            //Array arr = request.Website.Trim().Split(',');

                            customer.Website = request.Website;

                        }
                        //BaoHV add
                        customer.AgencyType = !string.IsNullOrEmpty(request.AgencyType)? request.AgencyType: customer.AgencyType;
                        customer.AgencyRule = request.AgencyRule ;
                        customer.RowUpdatedAt = DateTime.Now;
                        customer.RowUpdatedUser = currentUser.UserName;
                        dbConn.Update(customer);
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

                    return Json(new { success = false, message = e.Message });
                }
            }
        }
        public ActionResult CRMContactList_Read([DataSourceRequest]DataSourceRequest request, string CustomerID)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT * FROM  [ERPAPD_Contacts] WHERE CustomerID = '" + CustomerID + "'";

                    data = KendoApplyFilter.KendoDataByQuery<ERPAPD_Contacts>(request, strQuery, "");
                    //data = KendoApplyFilter.KendoData<ERPAPD_Contacts>(request);
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Create_Extention(ERPAPD_Customer_Extensions request)
        {
            var ERPAPD_Customer_Extensions = new ERPAPD_Customer_Extensions();
            var rs = ERPAPD_Customer_Extensions.Save(request, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult Save_Extention(IEnumerable<CRM_Ext_Meta_Value> data_arr)
        {
           var rs = CRM_ExtsInfo_Meta.update_all_value(data_arr, currentUser.UserName);
           return Json(rs);
        }
    }
}