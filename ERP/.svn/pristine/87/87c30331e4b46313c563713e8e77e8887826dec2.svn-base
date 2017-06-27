using ERPAPD.Helpers;
using ERPAPD.Models;
using Kendo.Mvc.UI;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Web.Mvc;

namespace ERPAPD.Controllers
{
    public class CustomerPresenterController : CustomController
    {
        // GET: CustomerPresenter
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
                        ViewBag.listGender = dbConn.Select<Parameters>(s => s.Type == "Gender");

                        ViewBag.listCustomer = dbConn.Select<ERPAPD_Customer>("SELECT TOP 100 CustomerID,CustomerName FROM ERPAPD_Customer");
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


        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {

            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT * FROM (Select 
                                       pk.*
                                       ,ISNULL(STUFF((
                                        SELECT 
                                            DISTINCT   ' - ' +cus.CustomerName +' <br> '           
	                                        FROM [dbo].[CRM_CustomerLead_Generation] fk  
		                                    LEFT JOIN ERPAPD_Customer cus ON cus.CustomerID=fk.CustomerID
			                                    where fk.PresenterId =pk.Id
			                                    FOR XML PATH(''), TYPE
                                        ).value('.', 'nvarchar(300)'), 1, 1, ''),'') listCustomer
	                                    ,ISNULL(STUFF((
                                        SELECT 
                                            DISTINCT   ' ; ' +cus.CustomerID           
	                                        FROM [dbo].[CRM_CustomerLead_Generation] fk  
		                                    LEFT JOIN ERPAPD_Customer cus ON cus.CustomerID=fk.CustomerID
			                                    where fk.PresenterId =pk.Id
			                                    FOR XML PATH(''), TYPE
                                        ).value('.', 'nvarchar(300)'), 1, 1, ''),'') CustomerID
	                                    ,B.Value AS PersonalName
                                    from CRM_Customer_Presenter pk
                                     LEFT JOIN [dbo].[Parameters] B
                                     ON pk.Personal=B.ParamID AND B.Type='Gender' ) data
                                        ";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_Customer_Presenter>(request, strQuery, "");
                    request.Filters = null;
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult ReadByCustomer([DataSourceRequest]DataSourceRequest request,string CustomerID)
        {

            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT * FROM (Select 
                                       pk.*
                                       ,ISNULL(STUFF((
                                        SELECT 
                                            DISTINCT   ' - ' +cus.CustomerName +' <br> '           
	                                        FROM [dbo].[CRM_CustomerLead_Generation] fk  
		                                    LEFT JOIN ERPAPD_Customer cus ON cus.CustomerID=fk.CustomerID
			                                    where fk.PresenterId =pk.Id
			                                    FOR XML PATH(''), TYPE
                                        ).value('.', 'nvarchar(300)'), 1, 1, ''),'') listCustomer
	                                    ,ISNULL(STUFF((
                                        SELECT 
                                            DISTINCT   ' ; ' +cus.CustomerID           
	                                        FROM [dbo].[CRM_CustomerLead_Generation] fk  
		                                    LEFT JOIN ERPAPD_Customer cus ON cus.CustomerID=fk.CustomerID
			                                    where fk.PresenterId =pk.Id
			                                    FOR XML PATH(''), TYPE
                                        ).value('.', 'nvarchar(300)'), 1, 1, ''),'') CustomerID
	                                    ,B.Value AS PersonalName
                                    from CRM_Customer_Presenter pk
                                     LEFT JOIN [dbo].[Parameters] B
                                     ON pk.Personal=B.ParamID AND B.Type='Gender' ) data
                                        ";
                    data = KendoApplyFilter.KendoDataByQuery<CRM_Customer_Presenter>(request, strQuery, "CustomerID like '%" + CustomerID + "%'");
                    request.Filters = null;
                    return Json(data);
                }
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult GetDetailCustomerPresenter(string Id)
        {
            using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (Id != null && Id != "")
                {
                    var data = dbConn.FirstOrDefault<CRM_Customer_Presenter>("Id ={0}", Id);
                    var listCustomer = dbConn.Select<CRM_CustomerLead_Generation>("PresenterId ={0}", data.Id);
                    return Json(new { data = data, listCustomer = listCustomer, success = true });
                }
                else
                {
                    var data = new CRM_Customer_Presenter();
                    return Json(new { data = data, success = true });
                }
            }

        }
        public ActionResult GetDetailCustomerByID(string Id)
        {
            using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (Id != null && Id != "")
                {
                    // var data = dbConn.FirstOrDefault<CRM_Customer_Presenter>("Id ={0}", Id);
                    var data = dbConn.Select<CRM_CustomerLead_Generation>("PresenterId ={0}", Id);
                    return Json(new { data = data, success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new CRM_Customer_Presenter();
                    return Json(new { data = data, success = true }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult CustomerPresenter_Create(CRM_Customer_Presenter item)
        {
            if (asset.View)
            {
                using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    if (item != null)
                    {
                        try
                        {
                            if (item.Id == 0)
                            {
                                CRM_Customer_Presenter insert = new CRM_Customer_Presenter();
                                insert.FirstName = !String.IsNullOrEmpty(item.FirstName) ? item.FirstName : "";
                                insert.MiddleName = !String.IsNullOrEmpty(item.MiddleName) ? item.MiddleName : "";
                                insert.LastName = !String.IsNullOrEmpty(item.LastName) ? item.LastName : "";
                                insert.Personal = !String.IsNullOrEmpty(item.Personal) ? item.Personal : "";
                                insert.DayOfBirth = !String.IsNullOrEmpty(item.DayOfBirth) ? item.DayOfBirth : "";
                                insert.MonthOfBirth = !String.IsNullOrEmpty(item.MonthOfBirth) ? item.MonthOfBirth : "";
                                insert.YearOfBirth = !String.IsNullOrEmpty(item.YearOfBirth) ? item.YearOfBirth : "";
                                insert.Phone = !String.IsNullOrEmpty(item.Phone) ? item.Phone : "";
                                insert.Email = !String.IsNullOrEmpty(item.Email) ? item.Email : "";
                                insert.Position = !String.IsNullOrEmpty(item.Position) ? item.Position : "";
                                insert.Company = !String.IsNullOrEmpty(item.Company) ? item.Company : "";
                                //insert.CustomerID = !String.IsNullOrEmpty(item.CustomerID) ? item.CustomerID : "";
                                insert.Note = !String.IsNullOrEmpty(item.Note) ? item.Note : "";
                                dbConn.Insert<CRM_Customer_Presenter>(insert);
                                long lastIdInsert = dbConn.GetLastInsertId();
                                if (item.customerArray != null && item.customerArray.Length > 0)
                                {
                                    foreach (string str in item.customerArray)
                                    {
                                        CRM_CustomerLead_Generation g = new CRM_CustomerLead_Generation();
                                        g.CustomerID = str;
                                        g.PresenterID = lastIdInsert;
                                        g.RowCreatedTime = DateTime.Now;
                                        g.RowCreatedUser = currentUser.UserName;
                                        //if (g != null)
                                        //{
                                        //    dbConn.Delete<CRM_CustomerLead_Generation>(g);
                                        //}
                                        dbConn.Insert<CRM_CustomerLead_Generation>(g);
                                    }
                                }
                            }
                            else
                            {
                                CRM_Customer_Presenter insert = new CRM_Customer_Presenter();
                                insert.Id = item.Id;
                                insert.FirstName = !String.IsNullOrEmpty(item.FirstName) ? item.FirstName : "";
                                insert.MiddleName = !String.IsNullOrEmpty(item.MiddleName) ? item.MiddleName : "";
                                insert.LastName = !String.IsNullOrEmpty(item.LastName) ? item.LastName : "";
                                insert.Personal = !String.IsNullOrEmpty(item.Personal) ? item.Personal : "";
                                insert.DayOfBirth = !String.IsNullOrEmpty(item.DayOfBirth) ? item.DayOfBirth : "";
                                insert.MonthOfBirth = !String.IsNullOrEmpty(item.MonthOfBirth) ? item.MonthOfBirth : "";
                                insert.YearOfBirth = !String.IsNullOrEmpty(item.YearOfBirth) ? item.YearOfBirth : "";
                                insert.Phone = !String.IsNullOrEmpty(item.Phone) ? item.Phone : "";
                                insert.Email = !String.IsNullOrEmpty(item.Email) ? item.Email : "";
                                insert.Position = !String.IsNullOrEmpty(item.Position) ? item.Position : "";
                                insert.Company = !String.IsNullOrEmpty(item.Company) ? item.Company : "";
                                //insert.CustomerID = !String.IsNullOrEmpty(item.CustomerID) ? item.CustomerID : "";
                                insert.Note = !String.IsNullOrEmpty(item.Note) ? item.Note : "";
                                dbConn.Update<CRM_Customer_Presenter>(insert);
                                if (item.customerArray != null && item.customerArray.Length > 0)
                                {
                                    dbConn.Delete<CRM_CustomerLead_Generation>(where: "PresenterID='" + insert.Id + "'");
                                    foreach (string s in item.customerArray)
                                    {
                                        CRM_CustomerLead_Generation g = new CRM_CustomerLead_Generation();
                                        g.CustomerID = s;
                                        g.PresenterID = insert.Id;
                                        g.RowCreatedTime = DateTime.Now;
                                        g.RowCreatedUser = currentUser.UserName;
                                        dbConn.Insert<CRM_CustomerLead_Generation>(g);
                                    }
                                }
                                else
                                {
                                    dbConn.Delete<CRM_CustomerLead_Generation>(where: "PresenterID='" + insert.Id + "'");
                                }
                            }
                            return Json(new { success = true });
                        }
                        catch (Exception ex)
                        {
                            return Json(new { success = false, error = ex.Message });
                        }
                        finally { dbConn.Close(); }

                    }
                    else
                    {
                        return Json(new { success = false, error = "Bạn không có quyền" });
                    }

                }
            }
            else
            {
                return Json(new { success = false, error = "Bạn không có quyền" });
            }

        }

        public ActionResult CustomerPresenter_Update(CRM_Customer_Presenter item)
        {
            if (asset.View)
            {
                using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    if (item != null)
                    {
                        try
                        {
                            CRM_Customer_Presenter insert = new CRM_Customer_Presenter();
                            insert.Id = item.Id;
                            insert.FirstName = !String.IsNullOrEmpty(item.FirstName) ? item.FirstName : "";
                            insert.MiddleName = !String.IsNullOrEmpty(item.MiddleName) ? item.MiddleName : "";
                            insert.LastName = !String.IsNullOrEmpty(item.LastName) ? item.LastName : "";
                            insert.Personal = !String.IsNullOrEmpty(item.Personal) ? item.Personal : "";
                            insert.DayOfBirth = !String.IsNullOrEmpty(item.DayOfBirth) ? item.DayOfBirth : "";
                            insert.MonthOfBirth = !String.IsNullOrEmpty(item.MonthOfBirth) ? item.MonthOfBirth : "";
                            insert.YearOfBirth = !String.IsNullOrEmpty(item.YearOfBirth) ? item.YearOfBirth : "";
                            insert.Phone = !String.IsNullOrEmpty(item.Phone) ? item.Phone : "";
                            insert.Email = !String.IsNullOrEmpty(item.Email) ? item.Email : "";
                            insert.Position = !String.IsNullOrEmpty(item.Position) ? item.Position : "";
                            insert.Company = !String.IsNullOrEmpty(item.Company) ? item.Company : "";
                            //insert.CustomerID = !String.IsNullOrEmpty(item.CustomerID) ? item.CustomerID : "";
                            insert.Note = !String.IsNullOrEmpty(item.Note) ? item.Note : "";
                            dbConn.Update<CRM_Customer_Presenter>(insert);
                            if (item.customerArray != null && item.customerArray.Length > 0)
                            {
                                foreach (string s in item.customerArray)
                                {
                                    CRM_CustomerLead_Generation g = new CRM_CustomerLead_Generation();
                                    g.CustomerID = s;
                                    g.PresenterID = insert.Id;
                                    g.RowCreatedTime = DateTime.Now;
                                    g.RowCreatedUser = currentUser.UserName;
                                    if (g != null)
                                    {
                                        dbConn.Delete<CRM_CustomerLead_Generation>(g);
                                    }
                                    dbConn.Insert<CRM_CustomerLead_Generation>(g);
                                }
                            }

                            return Json(new { success = true });
                        }
                        catch (Exception ex)
                        {
                            return Json(new { success = false, error = ex.Message });
                        }
                        finally { dbConn.Close(); }

                    }
                    else
                    {
                        return Json(new { success = false, error = "Bạn không có quyền" });
                    }

                }
            }
            else
            {
                return Json(new { success = false, error = "Bạn không có quyền" });
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    var check = dbConn.FirstOrDefault<CRM_Customer_Presenter>("Id={0}", id);
                    if (check != null)
                    {
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
        public ActionResult DeleteAll(string listdata)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    string[] separators = { "@@" };
                    var listRowID = listdata.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        var success = dbConn.Delete<CRM_Customer_Presenter>(where: "Id = '" + item + "'") >= 1;
                        if (!success)
                        {
                            return Json(new { success = false, message = "Không thể lưu" });
                        }
                    }
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, alert = ex.Message });
            }
        }
        public ActionResult DeletePresentInCustomer(string cus, int pre)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {
                    var check = dbConn.FirstOrDefault<CRM_CustomerLead_Generation>("CustomerId={0} AND PresenterId = {1}", cus, pre);
                    if (check != null)
                    {
                        dbConn.Delete(check);
                    }
                    Delete(pre);

                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, alert = ex.Message });
            }
            return Json(new { success = true });

        }
    }
}