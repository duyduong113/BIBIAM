using ERPAPD.Models;
using ERPAPD.Helpers;
using Kendo.Mvc.UI;
using System.Data;
using System.Web.Mvc;
using ServiceStack.OrmLite;

namespace ERPAPD.Controllers
{
    public class CRM_ProductController : CustomController
    {
        // GET: CRM_Product

        public ActionResult Index()
        {
            ViewData["AllowCreate"] = asset.Create;
            ViewData["AllowUpdate"] = asset.Update;
            ViewData["AllowDelete"] = asset.Delete;
            ViewData["AllowExport"] = asset.Export;
           
            return View();
        }


        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();

                if (asset.View)
                {
                    string strQuery = @"SELECT  product.*
                                                ,ISNULL(ProductType.Name,'') AS ProductTypeName
	                                            ,ISNULL(pageType.Name,'') AS PageTypeName
	                                            ,ISNULL(webSite.Name,'') AS WebsiteName
	                                            ,ISNULL(cate.Name,'') AS CategoryName
	                                            ,ISNULL(location.Name,'') AS LocationName
	                                            ,ISNULL(nature.Name,'') AS NatureName
                                        FROM [ERPAPD_Product] product
                                        LEFT JOIN 
                                        (
	                                        SELECT a.Code,a.Name
	                                        FROM [ERPAPDDev].[dbo].[ERPAPD_List] a
	                                        INNER JOIN [ERPAPDDev].[dbo].[ERPAPD_ListType] b on b.PKListType = a.FKListtype AND b.PKListType = 19 
                                        ) productType On productType.Code = product.ProductType
                                        LEFT JOIN 
                                        (
	                                        SELECT a.Code,a.Name
	                                        FROM [ERPAPDDev].[dbo].[ERPAPD_List] a
	                                        INNER JOIN [ERPAPDDev].[dbo].[ERPAPD_ListType] b on b.PKListType = a.FKListtype AND b.PKListType = 20 
                                        ) webSite On webSite.Code = product.Website
                                        LEFT JOIN 
                                        (
	                                        SELECT a.Code,a.Name
	                                        FROM [ERPAPDDev].[dbo].[ERPAPD_List] a
	                                        INNER JOIN [ERPAPDDev].[dbo].[ERPAPD_ListType] b on b.PKListType = a.FKListtype AND b.PKListType = 21 
                                        ) pageType On pageType.Code = product.PageType
                                        LEFT JOIN 
                                        (
	                                        SELECT a.Code,a.Name
	                                        FROM [ERPAPDDev].[dbo].[ERPAPD_List] a
	                                        INNER JOIN [ERPAPDDev].[dbo].[ERPAPD_ListType] b on b.PKListType = a.FKListtype AND b.PKListType = 22 
                                        ) cate On cate.Code = product.Category
                                        LEFT JOIN 
                                        (
	                                        SELECT a.Code,a.Name
	                                        FROM [ERPAPDDev].[dbo].[ERPAPD_List] a
	                                        INNER JOIN [ERPAPDDev].[dbo].[ERPAPD_ListType] b on b.PKListType = a.FKListtype AND b.PKListType = 23 
                                        ) location On location.Code = product.Location
                                        LEFT JOIN 
                                        (
	                                        SELECT a.Code,a.Name
	                                        FROM [ERPAPDDev].[dbo].[ERPAPD_List] a
	                                        INNER JOIN [ERPAPDDev].[dbo].[ERPAPD_ListType] b on b.PKListType = a.FKListtype AND b.PKListType = 25 
                                        ) nature On nature.Code = product.Nature
                                        ";
                    data = KendoApplyFilter.KendoDataByQuery<ERPAPD_Product>(request, strQuery, "");
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }


        public ActionResult ReadProduct(string text,string TYPE)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                string strQuery = @"SELECT TOP 10 product.*
                                                ,ISNULL(ProductType.Name,'') AS ProductTypeName
	                                            ,ISNULL(pageType.Name,'') AS PageTypeName
	                                            ,ISNULL(webSite.Name,'') AS WebsiteName
	                                            ,ISNULL(cate.Name,'') AS CategoryName
	                                            ,ISNULL(location.Name,'') AS LocationName
	                                            ,ISNULL(nature.Name,'') AS NatureName
                                        FROM [ERPAPD_Product] product
                                        LEFT JOIN 
                                        (
	                                        SELECT a.Code,a.Name
	                                        FROM [ERPAPDDev].[dbo].[ERPAPD_List] a
	                                        INNER JOIN [ERPAPDDev].[dbo].[ERPAPD_ListType] b on b.PKListType = a.FKListtype AND b.PKListType = 19 
                                        ) productType On productType.Code = product.ProductType
                                        LEFT JOIN 
                                        (
	                                        SELECT a.Code,a.Name
	                                        FROM [ERPAPDDev].[dbo].[ERPAPD_List] a
	                                        INNER JOIN [ERPAPDDev].[dbo].[ERPAPD_ListType] b on b.PKListType = a.FKListtype AND b.PKListType = 20 
                                        ) webSite On webSite.Code = product.Website
                                        LEFT JOIN 
                                        (
	                                        SELECT a.Code,a.Name
	                                        FROM [ERPAPDDev].[dbo].[ERPAPD_List] a
	                                        INNER JOIN [ERPAPDDev].[dbo].[ERPAPD_ListType] b on b.PKListType = a.FKListtype AND b.PKListType = 21 
                                        ) pageType On pageType.Code = product.PageType
                                        LEFT JOIN 
                                        (
	                                        SELECT a.Code,a.Name
	                                        FROM [ERPAPDDev].[dbo].[ERPAPD_List] a
	                                        INNER JOIN [ERPAPDDev].[dbo].[ERPAPD_ListType] b on b.PKListType = a.FKListtype AND b.PKListType = 22 
                                        ) cate On cate.Code = product.Category
                                        LEFT JOIN 
                                        (
	                                        SELECT a.Code,a.Name
	                                        FROM [ERPAPDDev].[dbo].[ERPAPD_List] a
	                                        INNER JOIN [ERPAPDDev].[dbo].[ERPAPD_ListType] b on b.PKListType = a.FKListtype AND b.PKListType = 23 
                                        ) location On location.Code = product.Location
                                        LEFT JOIN 
                                        (
	                                        SELECT a.Code,a.Name
	                                        FROM [ERPAPDDev].[dbo].[ERPAPD_List] a
	                                        INNER JOIN [ERPAPDDev].[dbo].[ERPAPD_ListType] b on b.PKListType = a.FKListtype AND b.PKListType = 25 
                                        ) nature On nature.Code = product.Nature
                                      WHERE
                                      product.Name COLLATE Latin1_General_CI_AI  LIKE N'%" + text + "%' AND ProductType='" + TYPE + "'";
          
                var data = dbConn.Select<ERPAPD_Product>(strQuery);
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }
    }
}