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
using OfficeOpenXml;
using System.IO;
using Kendo.Mvc;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Dynamic;
using Newtonsoft.Json;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class CustomerSupportController : CustomController
    {
        //
        // GET: /CustomerSupport/
        public ActionResult Index(string Phone)
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    OrmLiteConfig.DialectProvider.UseUnicode = true;
            //    dbConn.DropTables(typeof(Deca_AnonymousCustomer));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(Deca_AnonymousCustomer));
            //}
            if (asset.View)
            {
                using (var dbConn = OrmliteConnection.openConn())
                {
                    ViewData["ListStatus"] = dbConn.Select<Deca_Order_Status>();
                    ViewData["ListMerchant"] = dbConn.Select<DC_OCM_MerchantModelView>("SELECT PKMerchantID,MerchantName FROM DC_OCM_Merchant");
                    ViewData["TicketType"] = Deca_RT_Type.GetAllDeca_RT_Types_Active();
                    ViewBag.Phone = "";
                    if (Phone != null)
                    {
                        ViewBag.Phone = Phone;
                    }
                }
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }
        public ActionResult CustomerSearchRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(KendoApplyFilter.KendoDataCustomerSupport<Deca_Customer_Index>(request));
        }
        public ActionResult OrderHistoryRead([DataSourceRequest] DataSourceRequest request, string CustomerID, string DataSource)
        {
            if (DataSource == "ocmcustomer")
                return Json(KendoApplyFilter.KendoData<DC_OCM_Order>(request, "FKCustomerID=" + CustomerID));
            else
                return Json(new List<DC_OCM_Order>());
        }
        public ActionResult TicketHistoryRead([DataSourceRequest] DataSourceRequest request, string CustomerID, string CustomerSource)
        {
            string query = @"SELECT * FROM Deca_RT_Ticket";
            return Json(KendoApplyFilter.KendoDataByQuery<Deca_RT_Ticket>(request, query, "CustomerID = '" + CustomerID + "' AND CustomerSource = '" + CustomerSource + "'"));
        }
        public ActionResult OrderSearchRead(string OrderID)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = dbConn.FirstOrDefault<DC_OCM_Order>("OrderID={0}", OrderID);
                if (data == null)
                {
                    var dataPay = dbConn.FirstOrDefault<Deca_Order_Header>("OrderID={0}", OrderID);
                    if (dataPay == null)
                        return Json(new { success = false });
                    else
                    {
                        if (string.IsNullOrEmpty(dataPay.CustomerID))
                        {
                            //select source từ potential
                            var customer = dbConn.FirstOrDefault<Deca_Customer_Index>("PhysicalID={0} AND DataSource='potentialCustomer'", dataPay.PhysicalID);
                            if (customer == null)
                                return Json(new { success = false });
                            else
                                return Json(new { success = true, data = customer });
                        }
                        else
                        {
                            //select source từ customer
                            var customer = dbConn.FirstOrDefault<Deca_Customer_Index>("PhysicalID={0} AND DataSource='customer'", dataPay.PhysicalID);
                            if (customer == null)
                                return Json(new { success = false });
                            else
                                return Json(new { success = true, data = customer });
                        }
                    }
                }
                else
                {
                    //select customer nguôn ocm
                    var customer = dbConn.FirstOrDefault<Deca_Customer_Index>("CustomerID='" + data.FKCustomerID + "' AND DataSource='ocmcustomer'");
                    if (customer == null)
                        return Json(new { success = false });
                    else
                        return Json(new { success = true, data = customer });
                }
            }
        }
        public ActionResult AppointmentRead([DataSourceRequest] DataSourceRequest request)
        {
            string query = @"SELECT * FROM Deca_RT_Ticket";
            return Json(KendoApplyFilter.KendoDataByQuery<Deca_RT_Ticket>(request, query, "IsDone=0 AND RecallTime >'2000-01-01' AND RowCreatedUser = '" + currentUser.UserName + "'"));
        }
        public ActionResult AnnoucementRead([DataSourceRequest] DataSourceRequest request, string Type)
        {
            string value = Type == "Annoucement" ? "Type='" + Type + "'" : "Type<>'Annoucement'";
            return Json(KendoApplyFilter.KendoData<DC_CS_ManageInfo>(request, value + " AND IsActive=1 AND ExpiredDate > GETDATE()"));
        }
        public ActionResult SendSMSRead([DataSourceRequest] DataSourceRequest request, string CustomerID, string CustomerSource)
        {
            return Json(KendoApplyFilter.KendoData<Deca_SMS_SO>(request, "CustomerID ='" + CustomerID + "' AND CustomerSource = '" + CustomerSource + "'"));
        }

        public ActionResult SurveyForCS_Read([DataSourceRequest] DataSourceRequest request, string CustomerID, string DataSource)
        {
            //get list string survey customer nay da lam
            using (var dbConn = OrmliteConnection.openConn())
            {
                List<int> listSurvey = dbConn.GetFirstColumn<int>("SELECT Distinct SurveyManagementID FROM DC_Survey_Management_Proceeded WHERE CustomerID={0} AND [Source]={1}", CustomerID, DataSource);
                string notIn = "";
                string query = "";
                foreach (int id in listSurvey)
                {
                    notIn += "" + id + ",";
                }
                if (string.IsNullOrEmpty(notIn))
                {
                    query = @"SELECT 
                            m.Id, m.Title, m.Detail, m.StartDate, m.EndDate, m.QuestionCount
                            FROM DC_Survey_Management m 
                            JOIN DC_Survey_Management_User u
                            ON m.Id = u.SurveyManagementID
                            WHERE u.UserName = '" + currentUser.UserName + @"'
                                   AND m.StartDate< GETDATE() AND m.EndDate> GETDATE() AND m.[Status] <> N'Kết thúc'";
                }
                else
                {
                    notIn = notIn.Remove(notIn.Length - 1);
                    query = @"SELECT 
                            m.Id, m.Title, m.Detail, m.StartDate, m.EndDate, m.QuestionCount
                            FROM DC_Survey_Management m 
                            JOIN DC_Survey_Management_User u
                            ON m.Id = u.SurveyManagementID
                            WHERE u.UserName = '" + currentUser.UserName + @"'
                                   AND m.StartDate< GETDATE() AND m.EndDate> GETDATE() AND m.[Status] <> N'Kết thúc'
                                   AND m.Id not in (" + notIn + ")";
                }

                return Json(KendoApplyFilter.KendoDataByQuery<DC_Survey_Management>(request, query, ""));
            }
        }
        private string getCustomerRanking(string email)
        {
            try
            {
                int timeout = 15000;

                HttpWebRequest request = null;

                string url = "https://api.deca.vn/point/partner/get-rank-by-email/" + email;

                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = timeout;
                request.Headers.Add("partner", "twin");
                request.Headers.Add("token", "57BBOs8Bn9578BX2Cml5ppO5iT0H7Y1agG189ZYMSa7HrU44hVx4!H6u7X2");
                string result = string.Empty;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader data = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    result = data.ReadToEnd();
                    data.Close();
                }

                response.Close();
                dynamic objResult = JsonConvert.DeserializeObject(result);
                if (objResult.result == 0)
                {
                    return objResult.data.rank_name;
                }
                else
                    return "";
            }
            catch
            {
                //throw ex;
                return "";
            }
        }

        public ActionResult listViewOrderDetailRead([DataSourceRequest] DataSourceRequest request, string OrderID)
        {
            return Json(KendoApplyFilter.KendoData<DC_OCM_OrderDetail>(request, "OrderID='" + OrderID + "'"));
        }
        public ActionResult LoadCustomerOrderPopup(string OrderID)
        {
            using (var dbConn = OrmliteConnection.openConn())
                return Json(new { success = true, data = dbConn.FirstOrDefault<DC_OCM_Order>("OrderID={0}", OrderID) });
        }
        public ActionResult LoadCustomer(string CustomerID, string PhysicalID, string DataSource)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                if (DataSource.Trim() == "ocmcustomer")
                {
                    string selectQuery = @"SELECT 
                                                CONVERT(varchar(20), c.CustomerID) AS CustomerID, 
                                                'Email' = CASE WHEN ISNULL(m.Email,'')='' THEN c.Email ELSE ISNULL(m.Email,'') END,
                                                'Fullname' = CASE WHEN ISNULL(m.Fullname,'')='' THEN c.FullName ELSE ISNULL(m.Fullname,'') END,
                                                'Phone' = CASE WHEN ISNULL(m.Phone,'')='' THEN c.MobilePhone ELSE ISNULL(m.Phone,'') END,
                                                'Gender' = CASE WHEN ISNULL(m.Gender,'')='' THEN CASE WHEN c.Gender = 1 THEN 'Nam' ELSE N'Nữ' END ELSE CASE WHEN ISNULL(m.Gender,'') = 'GENDER01' THEN 'Nam' ELSE N'Nữ' END END,
                                                'Birthday'= CASE WHEN ISNULL(m.Birthday,'')='' THEN CASE WHEN c.Birthday = '0000-00-00' THEN '1900-01-01' ELSE DATEADD(mi,1,c.Birthday) END ELSE ISNULL(m.Birthday,'1900-01-01') END,
                                                'Address' = CASE WHEN ISNULL(m.[Address],'')='' THEN 
		                                            CASE WHEN c.ContactAddress = '' THEN '' ELSE c.ContactAddress+', ' END + 
		                                            CASE WHEN ISNULL(t1.TerritoryName,'') = '' THEN '' ELSE t1.TerritoryName+', ' END + 
		                                            CASE WHEN ISNULL(t.TerritoryName,'') = '' THEN '' ELSE t.TerritoryName END
	                                            ELSE ISNULL(m.[Address],'') END,	                                    
                                                ISNULL(m.CompanyName,'') AS CompanyName,
                                                ISNULL(m.CustomerType,'') AS CustomerType,
                                                ISNULL(m.Department,'') AS Department,
                                                ISNULL(m.Position,'') AS Position,
                                                ISNULL(m.PhysicalID,'') AS PhysicalID,
                                                ISNULL(m.Address1,'') AS Address1,
                                                ISNULL(m.Address2,'') AS Address2,
                                                ISNULL(m.OfficePhone,'') AS OfficePhone,
                                                ISNULL(m.HomePhone,'') AS HomePhone,
                                                'MobilePhone' = CASE WHEN ISNULL(m.MobilePhone,'')='' THEN CASE WHEN ISNULL(m.Phone,'')='' THEN c.MobilePhone ELSE ISNULL(m.Phone,'') END ELSE ISNULL(m.MobilePhone,'') END,
                                                ISNULL(m.Representative,'') AS Representative,
                                                ISNULL(m.SourceType,'') AS SourceType,
                                                ISNULL(m.CustomerRanking,'') AS CustomerRanking,
                                                ISNULL(m.TaxID,'') AS TaxID,
                                                ISNULL(m.Marital,'') AS Marital
                                            FROM DC_OCM_Customer c
                                            LEFT JOIN 
                                            (
                                                SELECT CustomerID,
                                                    MAX(CASE WHEN Factor = 'Address1' THEN Value ELSE '' END) AS 'Address1',
                                                    MAX(CASE WHEN Factor = 'Address2' THEN Value ELSE '' END) AS 'Address2',
                                                    MAX(CASE WHEN Factor = 'Marital' THEN Value ELSE '' END) AS 'Marital',
                                                    MAX(CASE WHEN Factor = 'OfficePhone' THEN Value ELSE '' END) AS 'OfficePhone',
                                                    MAX(CASE WHEN Factor = 'MobilePhone' THEN Value ELSE '' END) AS 'MobilePhone',
                                                    MAX(CASE WHEN Factor = 'HomePhone' THEN Value ELSE '' END) AS 'HomePhone',
                                                    MAX(CASE WHEN Factor = 'Representative' THEN Value ELSE '' END) AS 'Representative',
                                                    MAX(CASE WHEN Factor = 'TaxID' THEN Value ELSE '' END) AS 'TaxID',
                                                    MAX(CASE WHEN Factor = 'CompanyName' THEN Value ELSE '' END) AS 'CompanyName',
                                                    MAX(CASE WHEN Factor = 'Department' THEN Value ELSE '' END) AS 'Department',
                                                    MAX(CASE WHEN Factor = 'Position' THEN Value ELSE '' END) AS 'Position',
                                                    MAX(CASE WHEN Factor = 'SourceType' THEN Value ELSE '' END) AS 'SourceType',
                                                    MAX(CASE WHEN Factor = 'CustomerRanking' THEN Value ELSE '' END) AS 'CustomerRanking',
                                                    MAX(CASE WHEN Factor = 'PhysicalID' THEN Value ELSE '' END) AS 'PhysicalID',
                                                    MAX(CASE WHEN Factor = 'Email' THEN Value ELSE '' END) AS 'Email',
                                                    MAX(CASE WHEN Factor = 'Fullname' THEN Value ELSE '' END) AS 'Fullname',
                                                    MAX(CASE WHEN Factor = 'Phone' THEN Value ELSE '' END) AS 'Phone',                                 
                                                    MAX(CASE WHEN Factor = 'Gender' THEN Value ELSE '' END) AS 'Gender',
                                                    MAX(CASE WHEN Factor = 'Birthday' THEN Value ELSE '' END) AS 'Birthday',
                                                    MAX(CASE WHEN Factor = 'CustomerType' THEN Value ELSE '' END) AS 'CustomerType',
                                                    MAX(CASE WHEN Factor = 'Address' THEN Value ELSE '' END) AS 'Address'
                                                FROM Deca_Customer_Meta
                                                WHERE DataSource = {0} AND CustomerID = {1}
                                                GROUP BY CustomerID
                                                ) m ON c.CustomerID = m.CustomerID
	                                            left join DC_OCM_Territory t on t.TerritoryID = c.FKProvince and t.[Level] = 'Province'
	                                            left join DC_OCM_Territory t1 on t1.TerritoryID = c.FKDistrict and t.[Level] = 'District'
                                            WHERE c.CustomerID = {1}";
                    var data = dbConn.FirstOrDefault<Deca_Customer_ViewModel>(selectQuery, DataSource.Trim(), CustomerID);
                    if (data == null)
                    {
                        return Json(new { success = false, message = "Không tìm thấy khách hàng." });
                    }
                    data.DataSource = "ocmcustomer";
                    data.CustomerRanking = getCustomerRanking(data.Email);
                    return Json(new { success = true, data = data, });
                }
                else if (DataSource.Trim() == "potentialCustomer")
                {
                    string selectQuery = @"SELECT 
                                            c.*,
                                            'Gender' = CASE WHEN c.Sex = 'GENDER01' THEN 'Nam' ELSE N'Nữ' END, 
                                             ISNULL(m.Address1,'') AS Address1,
                                            ISNULL(m.Address2,'') AS Address2,
                                            ISNULL(m.HomePhone,'') AS HomePhone,
                                            'MobilePhone' = CASE WHEN ISNULL(m.MobilePhone,'')='' THEN c.Phone ELSE ISNULL(m.MobilePhone,'') END,
                                            ISNULL(m.OfficePhone,'') AS OfficePhone,
                                            ISNULL(m.Representative,'') AS Representative,
                                            ISNULL(m.CustomerRanking,'') AS CustomerRanking,
                                            ISNULL(m.TaxID,'') AS TaxID,
                                            ISNULL(m.CustomerType,'') AS CustomerType,
                                            ISNULL(m.Marital,'') AS Marital
                                            FROM Deca_Potential_Customer c
                                            LEFT JOIN 
                                            (
                                               SELECT CustomerID,
                                                  MAX(CASE WHEN Factor = 'Address1' THEN Value ELSE '' END) AS 'Address1',
                                                 MAX(CASE WHEN Factor = 'Address2' THEN Value ELSE '' END) AS 'Address2',
                                                 MAX(CASE WHEN Factor = 'MobilePhone' THEN Value ELSE '' END) AS 'MobilePhone',
                                                 MAX(CASE WHEN Factor = 'HomePhone' THEN Value ELSE '' END) AS 'HomePhone',
                                                 MAX(CASE WHEN Factor = 'CustomerRanking' THEN Value ELSE '' END) AS 'CustomerRanking',
                                                 MAX(CASE WHEN Factor = 'OfficePhone' THEN Value ELSE '' END) AS 'OfficePhone',
                                                 MAX(CASE WHEN Factor = 'Representative' THEN Value ELSE '' END) AS 'Representative',
                                                 MAX(CASE WHEN Factor = 'Marital' THEN Value ELSE '' END) AS 'Marital',
                                                 MAX(CASE WHEN Factor = 'CustomerType' THEN Value ELSE '' END) AS 'CustomerType',
                                                 MAX(CASE WHEN Factor = 'TaxID' THEN Value ELSE '' END) AS 'TaxID'
                                               FROM Deca_Customer_Meta
                                               WHERE DataSource = {0} AND CustomerID = {1}
                                               GROUP BY CustomerID
                                              ) m ON c.PhysicalID = m.CustomerID
                                              WHERE c.PhysicalID = {1}";
                    var data = dbConn.FirstOrDefault<Deca_Customer_ViewModel>(selectQuery, DataSource.Trim(), PhysicalID);
                    if (data == null)
                    {
                        return Json(new { success = false, message = "Không tìm thấy khách hàng." });
                    }
                    data.DataSource = "potentialCustomer";
                    data.CustomerID = data.PhysicalID;
                    data.CustomerRanking = "Chưa có tài khoản portal";
                    return Json(new { success = true, data = data, });
                }
                else if (DataSource.Trim() == "customer")
                {
                    string selectQuery = @"SELECT 
                                            c.*,
                                            ISNULL(m.Marital,'') AS Marital,
                                            'Gender' = CASE WHEN c.Sex = 'GENDER01' THEN 'Nam' ELSE N'Nữ' END, 
                                            ISNULL(m.Address1,'') AS Address1,
                                            ISNULL(m.Address2,'') AS Address2,
                                            ISNULL(m.HomePhone,'') AS HomePhone,
                                            'MobilePhone' = CASE WHEN ISNULL(m.MobilePhone,'')='' THEN c.Phone ELSE ISNULL(m.MobilePhone,'') END,
                                            ISNULL(m.OfficePhone,'') AS OfficePhone,
                                            ISNULL(m.Representative,'') AS Representative,
                                            ISNULL(m.CustomerRanking,'') AS CustomerRanking,
                                            ISNULL(m.TaxID,'') AS TaxID,
                                            ISNULL(m.CustomerType,'') AS CustomerType,
                                            ISNULL(m.Marital,'') AS Marital
                                            FROM Deca_Customer c
                                            LEFT JOIN 
                                            (
                                               SELECT CustomerID,
                                                 MAX(CASE WHEN Factor = 'Address1' THEN Value ELSE '' END) AS 'Address1',
                                                 MAX(CASE WHEN Factor = 'Address2' THEN Value ELSE '' END) AS 'Address2',
                                                 MAX(CASE WHEN Factor = 'MobilePhone' THEN Value ELSE '' END) AS 'MobilePhone',
                                                 MAX(CASE WHEN Factor = 'HomePhone' THEN Value ELSE '' END) AS 'HomePhone',
                                                 MAX(CASE WHEN Factor = 'CustomerRanking' THEN Value ELSE '' END) AS 'CustomerRanking',
                                                 MAX(CASE WHEN Factor = 'OfficePhone' THEN Value ELSE '' END) AS 'OfficePhone',
                                                 MAX(CASE WHEN Factor = 'Representative' THEN Value ELSE '' END) AS 'Representative',
                                                 MAX(CASE WHEN Factor = 'Marital' THEN Value ELSE '' END) AS 'Marital',
                                                 MAX(CASE WHEN Factor = 'CustomerType' THEN Value ELSE '' END) AS 'CustomerType',
                                                 MAX(CASE WHEN Factor = 'TaxID' THEN Value ELSE '' END) AS 'TaxID'
                                               FROM Deca_Customer_Meta
                                               WHERE DataSource = {0} AND CustomerID = {1}
                                               GROUP BY CustomerID
                                              ) m ON c.CustomerID = m.CustomerID
                                              WHERE c.CustomerID = {1}";
                    var data = dbConn.FirstOrDefault<Deca_Customer_ViewModel>(selectQuery, DataSource, CustomerID);
                    if (data == null)
                    {
                        return Json(new { success = false, message = "Không tìm thấy khách hàng." });
                    }
                    data.DataSource = "customer";
                    data.CustomerRanking = "Chưa có tài khoản portal";
                    return Json(new { success = true, data = data, });
                }
                else if (DataSource.Trim() == "anonymousCustomer")
                {
                    string selectQuery = @"SELECT 
                                            c.*,
                                            CAST(c.Id AS nvarchar(50)) AS CustomerID,
                                            'Gender' = CASE WHEN c.Gender = 'GENDER01' THEN 'Nam' ELSE N'Nữ' END, 
                                            'Birthday'= CASE WHEN ISNULL(c.Birthday,'')='' THEN '1900-01-01' ELSE DATEADD(mi,1,c.Birthday) END,
                                            'MobilePhone' = CASE WHEN ISNULL(c.MobilePhone,'')='' THEN c.Phone ELSE ISNULL(c.MobilePhone,'') END
                                            FROM Deca_AnonymousCustomer c
                                             WHERE c.Id = {0}";
                    var data = dbConn.FirstOrDefault<Deca_Customer_ViewModel>(selectQuery, int.Parse(CustomerID));
                    if (data == null)
                    {
                        return Json(new { success = false, message = "Không tìm thấy khách hàng." });
                    }
                    data.DataSource = "anonymousCustomer";
                    data.CustomerRanking = "Chưa có tài khoản portal";

                    return Json(new { success = true, data = data, });
                }
                else
                {
                    return Json(new { success = false, message = "Không tìm thấy nguồn khách hàng" });
                }
            }
        }
        [HttpPost]
        public ActionResult SaveCustomerInfo(Deca_Customer_ViewModel model)
        {

            if (asset.Update && ModelState.IsValid)
            {
                if (model.DataSource == "potentialCustomer")
                {
                    //update potential customer.
                    string message = UpdatePotentialCustomer(model);
                    if (message == "success")
                        return Json(new { success = true, message = "Cập nhật thông tin khách hàng thành công." });
                    else
                        return Json(new { success = false, message = message });
                }
                else if (model.DataSource == "customer")
                {
                    //update customer.
                    string message = UpdateCustomer(model);
                    if (message == "success")
                        return Json(new { success = true, message = "Cập nhật thông tin khách hàng thành công." });
                    else
                        return Json(new { success = false, message = message });
                }
                else if (model.DataSource == "ocmcustomer")
                {
                    string message = UpdateOCMCustomer(model);
                    if (message == "success")
                        return Json(new { success = true, message = "Cập nhật thông tin khách hàng thành công." });
                    else
                        return Json(new { success = false, message = message });
                }
                else
                {
                    string message = UpdateAnonymousCustomer(model);
                    if (message == "success")
                        return Json(new { success = true, message = "Cập nhật thông tin khách hàng thành công." });
                    else
                        return Json(new { success = false, message = message });
                }
            }
            else
            {
                if (!asset.Update) return Json(new { error = true, message = "Không có quyền cập nhật." });
                string message = "";
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        message += error.ErrorMessage + " ";
                    }
                }
                return Json(new { success = false, message = message });
            }

        }
        public bool UpdateCustomerIndex(Deca_Customer_ViewModel model, IDbConnection dbConn)
        {
            try
            {
                Deca_Customer_Index customerIndex = new Deca_Customer_Index();
                if (model.DataSource == "potentialCustomer")
                {
                    customerIndex = dbConn.FirstOrDefault<Deca_Customer_Index>("PhysicalID = {0} AND DataSource = {1}", model.PhysicalID, model.DataSource);
                }
                else
                {
                    customerIndex = dbConn.FirstOrDefault<Deca_Customer_Index>("CustomerID = {0} AND DataSource = {1}", model.CustomerID, model.DataSource);
                }
                string metaName = "";
                if (string.IsNullOrWhiteSpace(model.Fullname))
                    customerIndex.Fullname = "";
                else
                {
                    customerIndex.Fullname = model.Fullname;
                    metaName = convertToUnSign3.Init(customerIndex.Fullname);
                }
                customerIndex.Phone = model.Phone;
                customerIndex.Email = model.Email;
                customerIndex.Meta = model.CustomerID + "; " + metaName + "; " + model.MobilePhone + "; " + model.HomePhone + "; " + model.OfficePhone + "; " + model.PhysicalID + "; " + model.Email;
                customerIndex.UpdatedAt = DateTime.Now;
                customerIndex.UpdatedBy = currentUser.UserName;
                dbConn.Update(customerIndex);
                return true;
            }
            catch { return false; }
        }
        private string UpdateAnonymousCustomer(Deca_Customer_ViewModel model)
        {
            using (var dbConn = OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    bool bMeta = true;
                    //get current edit Customer in potential
                    var data = dbConn.FirstOrDefault<Deca_AnonymousCustomer>("Id =" + model.CustomerID);
                    //maping data từ model vào customer hiện tại.
                    data.Address = model.Address;
                    data.Address1 = model.Address1;
                    data.Address2 = model.Address2;
                    data.Birthday = model.Birthday;
                    data.CompanyName = model.CompanyName;
                    data.Department = model.Department;
                    data.Email = model.Email;
                    if (!string.IsNullOrEmpty(model.Gender))
                    {
                        data.Gender = model.Gender == "Nam" ? data.Gender = "GENDER01" : "GENDER02";
                    }
                    data.Fullname = model.Fullname;
                    data.Phone = model.Phone;
                    data.PhysicalID = model.PhysicalID;
                    data.Position = model.Position;
                    data.UpdatedAt = DateTime.Now;
                    data.UpdatedBy = currentUser.UserName;
                    data.CustomerType = model.CustomerType;
                    data.HomePhone = model.HomePhone;
                    data.Marital = model.Marital;
                    data.OfficePhone = model.OfficePhone;
                    data.MobilePhone = model.MobilePhone;
                    data.Representative = model.Representative;
                    data.TaxID = model.TaxID;
                    dbConn.Update(data);

                    if (!UpdateCustomerIndex(model, dbConn))
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin vào index";
                    }
                    dbTrans.Commit();
                    return "success";
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    return ex.Message;
                }
            }
        }
        private string UpdatePotentialCustomer(Deca_Customer_ViewModel model)
        {
            using (var dbConn = OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                try
                {
                    bool bMeta = true;
                    //get current edit Customer in potential
                    var data = dbConn.FirstOrDefault<Deca_Potential_Customer>("PhysicalID = {0}", model.PhysicalID);
                    //maping data từ model vào customer hiện tại.
                    data.Address = model.Address;
                    data.Birthday = model.Birthday;
                    data.CompanyID = model.CompanyID;
                    data.CompanyName = model.CompanyName;
                    data.Department = model.Department;
                    if (!string.IsNullOrEmpty(model.Gender))
                    {
                        data.Sex = model.Gender == "Nam" ? data.Sex = "GENDER01" : "GENDER02";
                    }
                    data.Email = model.Email;
                    data.Fullname = model.Fullname;
                    data.Phone = model.Phone;
                    data.PhysicalID = model.PhysicalID;
                    data.Position = model.Position;
                    data.UpdatedAt = DateTime.Now;
                    data.UpdatedBy = currentUser.UserName;
                    data.IsNew = true;
                    dbConn.Update(data);

                    //insert - update potential vao meta

                    Deca_Customer_Meta meta = new Deca_Customer_Meta();
                    meta.CreatedAt = DateTime.Now;
                    meta.CreatedBy = currentUser.UserName;
                    meta.CustomerID = model.PhysicalID;
                    meta.DataSource = model.DataSource;
                    meta.Factor = "Address1";
                    meta.UpdatedAt = DateTime.Now;
                    meta.UpdatedBy = currentUser.UserName;
                    meta.Value = model.Address1;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Address1";
                    }
                    //next meta
                    meta.Factor = "CustomerRanking";
                    meta.Value = model.CustomerRanking;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin CustomerRanking";
                    }
                    //next meta
                    meta.Factor = "CustomerType";
                    meta.Value = model.CustomerType;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin CustomerType";
                    }
                    //next meta
                    meta.Factor = "Address2";
                    meta.Value = model.Address2;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Address2";
                    }
                    //next meta
                    meta.Factor = "HomePhone";
                    meta.Value = model.HomePhone;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin HomePhone";
                    }
                    //next meta
                    meta.Factor = "Marital";
                    meta.Value = model.Marital;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Marital";
                    }
                    //next meta
                    meta.Factor = "OfficePhone";
                    meta.Value = model.OfficePhone;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin OfficePhone";
                    }
                    //next meta
                    meta.Factor = "MobilePhone";
                    meta.Value = model.MobilePhone;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin MobilePhone";
                    }
                    //next meta
                    meta.Factor = "Representative";
                    meta.Value = model.Representative;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Representative";
                    }
                    //next meta
                    meta.Factor = "TaxID";
                    meta.Value = model.TaxID;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin TaxID";
                    }
                    if (!UpdateCustomerIndex(model, dbConn))
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin vào index";
                    }
                    dbTrans.Commit();
                    return "success";
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    return ex.Message;
                }
        }

        private string UpdateCustomer(Deca_Customer_ViewModel model)
        {
            using (var dbConn = OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                try
                {
                    bool bMeta = true;
                    //get current edit Customer 
                    var data = dbConn.FirstOrDefault<Deca_Customer>("CustomerID = {0}", model.CustomerID);
                    //maping data từ model vào customer hiện tại.
                    data.Address = model.Address;
                    data.Birthday = model.Birthday;
                    data.MobilePhone = model.Phone;
                    data.CompanyID = model.CompanyID;
                    data.CompanyName = model.CompanyName;
                    data.Department = model.Department;
                    if (!string.IsNullOrEmpty(model.Gender))
                    {
                        data.Sex = model.Gender == "Nam" ? data.Sex = "GENDER01" : "GENDER02";
                    }
                    data.Email = model.Email;
                    data.Fullname = model.Fullname;
                    data.Phone = model.Phone;
                    data.PhysicalID = model.PhysicalID;
                    data.Position = model.Position;
                    data.UpdatedAt = DateTime.Now;
                    data.UpdatedBy = currentUser.UserName;
                    data.IsNew = true;
                    dbConn.Update(data);

                    //insert - update vao meta

                    Deca_Customer_Meta meta = new Deca_Customer_Meta();
                    meta.CreatedAt = DateTime.Now;
                    meta.CreatedBy = currentUser.UserName;
                    meta.CustomerID = model.CustomerID;
                    meta.DataSource = model.DataSource;
                    meta.Factor = "Address1";
                    meta.UpdatedAt = DateTime.Now;
                    meta.UpdatedBy = currentUser.UserName;
                    meta.Value = model.Address1;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Address1";
                    }
                    //next meta
                    meta.Factor = "CustomerType";
                    meta.Value = model.CustomerType;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin CustomerType";
                    }
                    //next meta

                    meta.Factor = "Address2";
                    meta.Value = model.Address2;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Address2";
                    }
                    //next meta
                    meta.Factor = "CustomerRanking";
                    meta.Value = model.CustomerRanking;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin CustomerRanking";
                    }
                    //next meta
                    meta.Factor = "HomePhone";
                    meta.Value = model.HomePhone;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin HomePhone";
                    }
                    //next meta
                    meta.Factor = "MobilePhone";
                    meta.Value = model.MobilePhone;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin MobilePhone";
                    }
                    //next meta
                    meta.Factor = "Marital";
                    meta.Value = model.Marital;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Marital";
                    }
                    //next meta
                    meta.Factor = "OfficePhone";
                    meta.Value = model.OfficePhone;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin OfficePhone";
                    }
                    //next meta
                    meta.Factor = "Representative";
                    meta.Value = model.Representative;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Representative";
                    }
                    //next meta
                    meta.Factor = "TaxID";
                    meta.Value = model.TaxID;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin TaxID";
                    }
                    if (!UpdateCustomerIndex(model, dbConn))
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin vào index";
                    }
                    dbTrans.Commit();
                    return "success";
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    return ex.Message;
                }
        }

        private string UpdateOCMCustomer(Deca_Customer_ViewModel model)
        {
            using (var dbConn = OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                try
                {
                    bool bMeta = true;
                    //get current edit Customer 
                    // var data = dbConn.FirstOrDefault<DC_OCM_Customer>("CustomerID = {0}", model.CustomerID);
                    //maping data từ model vào customer hiện tại.
                    //insert - update vao meta

                    Deca_Customer_Meta meta = new Deca_Customer_Meta();
                    meta.CreatedAt = DateTime.Now;
                    meta.CreatedBy = currentUser.UserName;
                    meta.CustomerID = model.CustomerID;
                    meta.DataSource = model.DataSource;
                    meta.Factor = "Address1";
                    meta.UpdatedAt = DateTime.Now;
                    meta.UpdatedBy = currentUser.UserName;
                    meta.Value = model.Address1;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Address1";
                    }
                    //next meta
                    meta.Factor = "Birthday";
                    meta.Value = model.Birthday.ToString("yyyy-MM-dd");
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Birthday";
                    }
                    //next meta
                    meta.Factor = "Phone";
                    meta.Value = model.Phone;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Phone";
                    }
                    //next meta
                    meta.Factor = "Address";
                    meta.Value = model.Address;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Address";
                    }
                    //next meta
                    meta.Factor = "CustomerType";
                    meta.Value = model.CustomerType;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin CustomerType";
                    }
                    //next meta
                    meta.Factor = "Email";
                    meta.Value = model.Email;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Email";
                    }
                    //next meta
                    meta.Factor = "Fullname";
                    meta.Value = model.Fullname;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Fullname";
                    }
                    //next meta
                    meta.Factor = "Gender";
                    meta.Value = model.Gender == "Nam" ? "GENDER01" : "GENDER02";
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Gender";
                    }
                    //next meta
                    meta.Factor = "Address2";
                    meta.Value = model.Address2;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Address2";
                    }
                    //next meta
                    meta.Factor = "CustomerRanking";
                    meta.Value = model.CustomerRanking;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin CustomerRanking";
                    }
                    //next meta
                    meta.Factor = "CompanyName";
                    meta.Value = model.CompanyName;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin CompanyName";
                    }
                    //next meta
                    meta.Factor = "Department";
                    meta.Value = model.Department;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Department";
                    }
                    //next meta
                    meta.Factor = "PhysicalID";
                    meta.Value = model.PhysicalID;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin PhysicalID";
                    }
                    //next meta
                    meta.Factor = "Position";
                    meta.Value = model.Position;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Position";
                    }

                    //next meta
                    meta.Factor = "HomePhone";
                    meta.Value = model.HomePhone;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin HomePhone";
                    }
                    //next meta
                    meta.Factor = "Marital";
                    meta.Value = model.Marital;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Marital";
                    }
                    //next meta
                    meta.Factor = "OfficePhone";
                    meta.Value = model.OfficePhone;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin OfficePhone";
                    }
                    //next meta
                    meta.Factor = "HomePhone";
                    meta.Value = model.HomePhone;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin HomePhone";
                    }
                    //next meta
                    meta.Factor = "MobilePhone";
                    meta.Value = model.MobilePhone;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin MobilePhone";
                    }
                    //next meta
                    meta.Factor = "Representative";
                    meta.Value = model.Representative;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin Representative";
                    }
                    //next meta
                    meta.Factor = "TaxID";
                    meta.Value = model.TaxID;
                    bMeta = meta.InsertUpdate(dbConn);
                    if (!bMeta)
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin TaxID";
                    }
                    if (!UpdateCustomerIndex(model, dbConn))
                    {
                        dbTrans.Rollback();
                        return "Không thể lưu thông tin vào index";
                    }
                    dbTrans.Commit();
                    return "success";
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    return ex.Message;
                }
        }
        public ActionResult GetListCompany()
        {
            using (var dbConn = OrmliteConnection.openConn())
                return Json(new { success = true, data = dbConn.Select<Deca_Company>() });
        }
        public ActionResult DoneAppointment(string TicketID)
        {
            try
            {
                using (var dbConn = OrmliteConnection.openConn())
                {
                    dbConn.Update("Deca_RT_Ticket", "IsDone=1", "TicketID='" + TicketID + "'");
                    return Json(new { success = true });
                }
            }
            catch
            {
                return Json(new { success = false, message = "Xảy ra lỗi khi update ticket" });
            }
        }

        public ActionResult LoadOCMOrderSuggestForSendTicket(string OrderID, string CustomerID)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(OrderID) && OrderID.Count() > 1)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        List<Deca_OCMOrder_Suggest> order = dbConn.Select<Deca_OCMOrder_Suggest>("SELECT TOP 5 OrderID, CreatedDate AS OrderDate, TotalAmt FROM DC_OCM_Order WHERE FKCustomerID=" + CustomerID + " AND [OrderID] LIKE N'" + OrderID + "%' ORDER BY CreatedDate DESC");
                        return Json(order, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new List<Deca_Order_TelesalesSuggest>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new List<Deca_Order_TelesalesSuggest>(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SendSMSFromCustomerSupport(Deca_SMS_SO smsSO)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                if (ModelState.IsValid)
                {
                    //save message.
                    smsSO.CreatedAt = DateTime.Now;
                    smsSO.CreatedBy = currentUser.UserName;
                    smsSO.UpdatedAt = DateTime.Now;
                    smsSO.UpdatedBy = currentUser.UserName;
                    smsSO.Status = Helpers.SendSMS.Send(smsSO.Phone.Trim(), smsSO.Content);
                    dbConn.Insert(smsSO);
                    return Json(new { error = false, message = "true" });
                }
                else
                {
                    string message = "";
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            message += error.ErrorMessage + " ";
                        }
                    }
                    return Json(new { error = true, message = message });
                }
            }
        }
        [HttpPost]
        public ActionResult SaveAnonymousCustomer(Deca_AnonymousCustomer model)
        {
            if (asset.Update && ModelState.IsValid)
            {
                using (var dbConn = OrmliteConnection.openConn())
                using (var dbTrans = dbConn.OpenTransaction())
                {
                    try
                    {
                        //check so phone nay co chua
                        var checkPhone = dbConn.GetFirstColumn<string>("SELECT CAST(Id AS NVARCHAR(16)) AS ID FROM Deca_Customer_Index WHERE CONTAINS((Meta,MetaOCM),'" + model.Phone + "')");
                        if (checkPhone.Count > 0)
                        {
                            return Json(new { success = false, message = "Số điện thoại này đã tồn tại trong khách hàng hệ thống." });
                        }
                        model.CreatedAt = DateTime.Now;
                        model.CreatedBy = currentUser.UserName;
                        dbConn.Insert(model);
                        model.Id = (int)dbConn.GetLastInsertId();
                        //them customer index
                        var currentIndex = dbConn.FirstOrDefault<Deca_Customer_Index>("CustomerID='" + model.Id + "' AND DataSource='anonymousCustomer'");
                        string meta = model.Id + "," + convertToUnSign3.Init(model.Fullname) + "," + model.Phone + (string.IsNullOrEmpty(model.MobilePhone) ? "" : "," + model.MobilePhone) + (string.IsNullOrEmpty(model.HomePhone) ? "" : "," + model.HomePhone) + (string.IsNullOrEmpty(model.OfficePhone) ? "" : "," + model.OfficePhone) + (string.IsNullOrEmpty(model.Email) ? "" : "," + model.Email);
                        if (currentIndex == null)
                        {
                            currentIndex = new Deca_Customer_Index();
                            currentIndex.CustomerID = model.Id.ToString();
                            currentIndex.CreatedAt = DateTime.Now;
                            currentIndex.CreatedBy = currentUser.UserName;
                            currentIndex.DataSource = "anonymousCustomer";
                            currentIndex.Email = model.Email;
                            currentIndex.Fullname = model.Fullname;
                            currentIndex.isDelete = false;
                            currentIndex.Meta = meta;
                            currentIndex.MetaOCM = "";
                            currentIndex.Phone = model.Phone;
                            currentIndex.PhysicalID = model.PhysicalID;
                            dbConn.Insert(currentIndex);
                        }
                        else
                        {
                            currentIndex.Email = model.Email;
                            currentIndex.Fullname = model.Fullname;
                            currentIndex.isDelete = false;
                            currentIndex.Meta = meta;
                            currentIndex.Phone = model.Phone;
                            currentIndex.PhysicalID = model.PhysicalID;
                            currentIndex.UpdatedAt = DateTime.Now;
                            currentIndex.UpdatedBy = currentUser.UserName;
                            dbConn.Update(currentIndex);
                        }
                        dbTrans.Commit();
                        return Json(new { success = true, message = "Success", CustomerID = model.Id });
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { success = false, message = ex.Message });
                    }
                }
            }
            else
            {
                if (!asset.Update) return Json(new { error = true, message = "Không có quyền thêm khách hàng." });
                string message = "";
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        message += error.ErrorMessage + " ";
                    }
                }
                return Json(new { success = false, message = message });
            }
        }
    }

}