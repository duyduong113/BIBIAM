using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;
using System.IO;


namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class OCMCustomerController : CustomController
    {
        //
        // GET: /SMSMOManagement/
        public ActionResult Index()
        {
            if (asset.View)
            {
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string query = @"Select
                                               cu.CustomerID
                                               ,cu.FullName
                                               ,case when cu.Gender = 1 then N'Nam' when cu.Gender = 0 then N'Nữ' else '' end as Gender
                                               ,case when cu.Birthday ='0000-00-00' then '1900-01-01' else cu.Birthday end as Birthday
                                               ,case when cu.StandardMobilePhone = '' then cu.MobilePhone else cu.MobilePhone end as MobilePhone
                                               ,cu.Email
                                               ,cu.ContactAddress
                                               ,isnull(di.TerritoryName,'') as District
                                               ,isnull(pr.TerritoryName,'') as Province 
                                               ,cu.RegisteredDate
                                               ,case when cu.FisrtOrderDate ='0000-00-00 00:00:00' then '1900-01-01' else cu.FisrtOrderDate end as FisrtOrderDate
                                               ,isnull(ord.LastOrderDate,'1900-01-01') as LastOrderDate
                                               ,isnull(ord.OrderNumber,0) as OrderNumber
                                               ,isnull(sv.LastOfSurvey,'1900-01-01') as LastOfSuvey
                                               ,isnull(sv.SurveyTime,0) as SurveyTime
                                        from [dbo].[DC_OCM_Customer] cu
                                        left join DC_OCM_Territory pr on pr.TerritoryID = cu.FKProvince and pr.[Level] = 'Province'
                                        left join DC_OCM_Territory di on di.TerritoryID = cu.FKDistrict and di.[Level] = 'District'
                                        left join (
                                               Select 
                                                      CustomerID
                                                      ,max(CreatedAt) as LastOfSurvey
                                                      ,count(distinct SurveyManagementID) as SurveyTime
                                               from DC_Survey_Management_Proceeded
                                               where [Source] = 'ocmcustomer' and CustomerID IS NOT NULL
                                               group by CustomerID
                                        )sv on sv.CustomerID = cu.CustomerID
                                        left join(
                                               Select 
                                                      FKCustomerID as CustomerID
                                                      ,Max(CreatedDate) as LastOrderDate
                                                      ,count(distinct OrderID) as OrderNumber
                                               from DC_OCM_Order
                                               Group by FKCustomerID
                                        )ord on ord.CustomerID = cu.CustomerID
                                        ";
                    data = KendoApplyFilter.KendoDataByQuery<Deca_OCMCustomer>(request, query, "");
                }
                return Json(data);
            }
        }

        [HttpPost]
        public ActionResult Excel_Export(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

    }
}