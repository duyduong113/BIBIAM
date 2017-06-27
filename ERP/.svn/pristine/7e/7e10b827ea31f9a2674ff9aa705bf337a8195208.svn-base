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
using OfficeOpenXml;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class PreOrderChangeInfoController : CustomController
    {
        //
        // GET: /Organization/
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(DC_OCM_Update_Log));
            //    const bool overwrite = true;
            //    dbConn.CreateTables(overwrite, typeof(DC_OCM_Update_Log));
            //}
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.City = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName);
                    ViewBag.District = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "District").OrderBy(a => a.TerritoryName);
                    ViewBag.Bank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                    ViewBag.SaleMan = dbConn.Select<Users>().OrderBy(a => a.UserName);
                    ViewBag.Status = dbConn.Select<Deca_Order_Status>().OrderBy(a => a.ID);
                    ViewBag.ListReason = dbConn.Select<Deca_Code_Master>("CodeType={0}", "CancelOrderReason");
                }
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }
        public ActionResult Read1([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult data;
            data = PreOrderViewModels.GetAll_PreOrder_ProductChange_Dynamic(request, "");
            return Json(data);
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT	oh.*
                                        FROM Deca_Order_Header oh 
                                        LEFT JOIN Deca_Order_CancelTracking ct 
                                        ON oh.OrderID = ct.OrderID 
                                        LEFT JOIN Deca_Bank_Transactions bt 
                                        ON oh.OrderID = bt.OrderID
                                        lEFT JOIN Deca_Order_Detail od
                                        ON oh.OrderID = od.OrderID
                                        LEFT JOIN DC_SKU_Property b 
                                        On od.ProductID = b.productId And od.PriceID = b.productPriceId
                                        Where oh.PaymentStatus = 0
                                        AND (oh.Status = 0 OR oh.Status = 1 OR oh.Status = 2 OR oh.Status = 9)
                                        AND ((b.ISACTIVE = 0) 
                                        OR (b.IsActive = 1 AND od.UnitPrice <> b.sellingprice)
                                        OR (od.PriceID not in(Select productPriceId from DC_SKU_Property ))
                                        OR (od.PriceID IN (Select Distinct c.PKproductpriceID from DecaInsight..DC_OCM_Product_Price c where c.StockOnHand <= 0)))
                                         ";
                    data = KendoApplyFilter.KendoDataByQuery<Deca_Order_Header>(request, strQuery, "");
                }
                return Json(data);
            }
        }
        public ActionResult Detail_Read([DataSourceRequest]DataSourceRequest request, string OrderID)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<Deca_Order_Detail>();
                if (asset.View)
                {

                    data = dbConn.Select<Deca_Order_Detail>("OrderID={0}", OrderID);
                }
                return Json(data.ToDataSourceResult(request));
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_Order_Header> items)
        {
            if (asset.Update)
            {
                if (items != null && ModelState.IsValid)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        foreach (var item in items)
                        {
                            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                            {
                                //item.IsNew = true;
                                //item.UpdatedAt = DateTime.Now;
                                //item.UpdatedBy = User.Identity.Name;
                                //dbConn.Update(item);

                                //var log = new Deca_Order_Header_Log();
                                //log.CompanyID = item.CompanyID;
                                //log.Log = item;
                                //log.CreatedAt = DateTime.Now;
                                //log.CreatedBy = currentUser.UserName;
                                //dbConn.Insert(log);
                                //dbTrans.Commit();
                            }
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Don't have permission");
            }

            return Json(items.ToDataSourceResult(request, ModelState));
        }

        public ActionResult GetListReason()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                return Json(dbConn.Select<Deca_Code_Master>("CodeType={0}", "CancelOrderReason"), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetListBank()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ExportData([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_PreOrderChangeInfo.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_PreOrderChangeInfo_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string query = "";
                    var data = new List<Deca_Order_ExcelModel>();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        query += " SELECT TOP 10000 oh.*,od.ProductID, od.ProductName, od.Quantity, od.UnitPrice,od.LineAmount,od.PriceID,od.SKUID, ISNULL(ct.[Description],'') AS CancelReason  ";
                        query += " FROM (SELECT * FROM Deca_Order_Header";
                        query += " WHERE " + where + ") oh";
                        query += " LEFT JOIN Deca_Order_CancelTracking ct  ";
                        query += " ON oh.OrderID = ct.OrderID  ";
                        query += " LEFT JOIN Deca_Bank_Transactions bt  ";
                        query += " ON oh.OrderID = bt.OrderID ";
                        query += " lEFT JOIN Deca_Order_Detail od ";
                        query += " ON oh.OrderID = od.OrderID ";
                        query += " LEFT JOIN DC_SKU_Property b  ";
                        query += " On od.ProductID = b.productId And od.PriceID = b.productPriceId ";
                        query += " Where oh.PaymentStatus = 0 ";
                        query += " AND (oh.Status = 0 OR oh.Status = 1 OR oh.Status = 2 OR oh.Status = 9) ";
                        query += " AND ((b.ISACTIVE = 0)  ";
                        query += " OR (b.IsActive = 1 AND od.UnitPrice <> b.sellingprice) ";
                        query += " OR (od.PriceID not in(Select productPriceId from DC_SKU_Property )) ";
                        query += " OR (od.PriceID IN (Select Distinct c.PKproductpriceID from DecaInsight..DC_OCM_Product_Price c where c.StockOnHand <= 0))) ";

                        data = dbConn.Select<Deca_Order_ExcelModel>(query).ToList();
                    }
                    else
                    {
                        query += " SELECT TOP 10000 oh.*,od.ProductID, od.ProductName, od.Quantity, od.UnitPrice,od.LineAmount,od.PriceID,od.SKUID, ISNULL(ct.[Description],'') AS CancelReason  ";
                        query += " FROM Deca_Order_Header oh";
                        query += " LEFT JOIN Deca_Order_CancelTracking ct  ";
                        query += " ON oh.OrderID = ct.OrderID  ";
                        query += " LEFT JOIN Deca_Bank_Transactions bt  ";
                        query += " ON oh.OrderID = bt.OrderID ";
                        query += " lEFT JOIN Deca_Order_Detail od ";
                        query += " ON oh.OrderID = od.OrderID ";
                        query += " LEFT JOIN DC_SKU_Property b  ";
                        query += " On od.ProductID = b.productId And od.PriceID = b.productPriceId ";
                        query += " Where oh.PaymentStatus = 0 ";
                        query += " AND (oh.Status = 0 OR oh.Status = 1 OR oh.Status = 2 OR oh.Status = 9) ";
                        query += " AND ((b.ISACTIVE = 0)  ";
                        query += " OR (b.IsActive = 1 AND od.UnitPrice <> b.sellingprice) ";
                        query += " OR (od.PriceID not in(Select productPriceId from DC_SKU_Property )) ";
                        query += " OR (od.PriceID IN (Select Distinct c.PKproductpriceID from DecaInsight..DC_OCM_Product_Price c where c.StockOnHand <= 0))) ";

                        data = dbConn.Select<Deca_Order_ExcelModel>(query).ToList();
                    }

                    ExcelWorksheet expenseSheet = excelPkg.Workbook.Worksheets["Deca_PreOrderChangeInfo"];
                    var dataCity = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName);
                    var dataDistrict = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "District").OrderBy(a => a.TerritoryName);
                    var dataBank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                    var dataStatus = dbConn.Select<Deca_Order_Status>();
                    int rowData = 1;
                    foreach (var item in data)
                    {
                        string paymentStatus = "";
                        if (item.PaymentStatus == "0")
                            paymentStatus = "Chưa thanh toán";
                        int i = 1;
                        rowData++;
                        expenseSheet.Cells[rowData, i++].Value = item.OrderID;
                        expenseSheet.Cells[rowData, i++].Value = item.RefID;
                        expenseSheet.Cells[rowData, i++].Value = item.ProductID;
                        expenseSheet.Cells[rowData, i++].Value = item.PriceID;
                        expenseSheet.Cells[rowData, i++].Value = item.SKUID;
                        expenseSheet.Cells[rowData, i++].Value = item.ProductName;
                        expenseSheet.Cells[rowData, i++].Value = item.OrderDate;
                        expenseSheet.Cells[rowData, i++].Value = item.Quantity;
                        expenseSheet.Cells[rowData, i++].Value = item.UnitPrice;
                        expenseSheet.Cells[rowData, i++].Value = item.LineAmount;
                        expenseSheet.Cells[rowData, i++].Value = item.ShopName;
                        expenseSheet.Cells[rowData, i++].Value = item.Amount;
                        expenseSheet.Cells[rowData, i++].Value = dataStatus.FirstOrDefault(a => a.RefID.ToString() == item.Status) == null ? "" : dataStatus.FirstOrDefault(a => a.RefID.ToString() == item.Status).Name;
                        expenseSheet.Cells[rowData, i++].Value = item.CompletedDate;
                        expenseSheet.Cells[rowData, i++].Value = item.CustomerID;
                        expenseSheet.Cells[rowData, i++].Value = item.CustomerName;
                        expenseSheet.Cells[rowData, i++].Value = item.CompanyID;
                        expenseSheet.Cells[rowData, i++].Value = item.EmployeeID;
                        expenseSheet.Cells[rowData, i++].Value = item.PhysicalID;
                        expenseSheet.Cells[rowData, i++].Value = item.MobilePhone;
                        expenseSheet.Cells[rowData, i++].Value = item.Email;
                        expenseSheet.Cells[rowData, i++].Value = item.Note;
                        expenseSheet.Cells[rowData, i++].Value = item.ShippingAddress;
                        expenseSheet.Cells[rowData, i++].Value = dataCity.FirstOrDefault(a => a.TerritoryID == item.ShippingCity.ToString()) == null ? "" : dataCity.FirstOrDefault(a => a.TerritoryID == item.ShippingCity.ToString()).TerritoryName;
                        expenseSheet.Cells[rowData, i++].Value = dataDistrict.FirstOrDefault(a => a.TerritoryID == item.ShippingDistrict.ToString()) == null ? "" : dataDistrict.FirstOrDefault(a => a.TerritoryID == item.ShippingDistrict.ToString()).TerritoryName;
                        expenseSheet.Cells[rowData, i++].Value = item.Installment;
                        expenseSheet.Cells[rowData, i++].Value = item.PayPerMonth;
                        expenseSheet.Cells[rowData, i++].Value = item.BankFee;
                        expenseSheet.Cells[rowData, i++].Value = item.TotalAmount;
                        expenseSheet.Cells[rowData, i++].Value = dataBank.FirstOrDefault(a => a.BankID == item.Bank) == null ? "" : dataBank.FirstOrDefault(a => a.BankID == item.Bank).BankName;
                        expenseSheet.Cells[rowData, i++].Value = paymentStatus;
                        expenseSheet.Cells[rowData, i++].Value = item.IsLocked.ToString();
                        expenseSheet.Cells[rowData, i++].Value = item.CreatedAt;
                        expenseSheet.Cells[rowData, i++].Value = item.CreatedBy;
                        expenseSheet.Cells[rowData, i++].Value = item.UpdatedAt;
                        expenseSheet.Cells[rowData, i++].Value = item.UpdatedBy;
                    }
                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    output.Position = 0;
                    return File(output.ToArray(), contentType, fileName);
                }
            }
            else
            {
                return Json(new { success = false });
            }

        }

        public ActionResult Cancel(Deca_Order_CancelTracking model)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                if (asset.Update && ModelState.IsValid)
                {
                    try
                    {
                        int s = 0, f = 0;
                        string[] separators = { "@@" };
                        var listid = model.listOrderID.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var id in listid)
                        {
                            var item = dbConn.FirstOrDefault<Deca_Order_Header>("ID={0}", id);
                            if (int.Parse(item.Status) < 5 || int.Parse(item.Status) == 9)
                            {

                                Deca_Order_CancelTracking cancelTracking = new Deca_Order_CancelTracking();
                                cancelTracking.CreatedAt = DateTime.Now;
                                cancelTracking.CreatedBy = currentUser.UserName;
                                cancelTracking.Description = model.Description;
                                cancelTracking.OrderID = item.OrderID;
                                cancelTracking.ReasonID = model.ReasonID;
                                cancelTracking.UpdatedAt = DateTime.Now;
                                cancelTracking.UpdatedBy = currentUser.UserName;
                                dbConn.Insert(cancelTracking);
                                item.UpdatedAt = DateTime.Now;
                                item.UpdatedBy = currentUser.UserName;
                                item.Status = "10";
                                dbConn.Update(item);

                                if (!string.IsNullOrEmpty(item.RefID))
                                {
                                    if (item.PaymentStatus == "1")
                                    {
                                        var bankTrans = dbConn.SingleOrDefault<Deca_Bank_Transactions>("OrderID={0}", item.OrderID);

                                        var cancelOrderAPI = new DC_OCM_Update();
                                        cancelOrderAPI.ma_don_hang = item.RefID;
                                        cancelOrderAPI.code_token = item.OrderID;
                                        cancelOrderAPI.ma_giao_dich = bankTrans.BillTransRef;
                                        cancelOrderAPI.ngay_giao_dich = item.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                                        cancelOrderAPI.p_tai_khoan_thanh_toan = "liemnm@twin.vn";
                                        cancelOrderAPI.ma_ngan_hang = !string.IsNullOrEmpty(item.Bank) ? item.Bank : "BA00001";
                                        cancelOrderAPI.goi_tra_gop = 12;
                                        cancelOrderAPI.p_id_cong_thanh_toan = 5;
                                        cancelOrderAPI.p_loai_thanh_toan = 9;
                                        cancelOrderAPI.api_key = "8b8a689ce420";
                                        cancelOrderAPI.api_secret = "0124ccc29bc3c7e287a714d7eb99b2ce";
                                        cancelOrderAPI.trang_thai_don_hang = 2;
                                        cancelOrderAPI.id_ly_do_huy = 1;

                                        //goi api update
                                        var decaOrderApi = Helpers.DecaAPI.updateOCMOrder(cancelOrderAPI);
                                        if (decaOrderApi.response_code != "0")
                                        {

                                            return Json(new { success = false, message = item.OrderID + ": " + decaOrderApi.message });
                                        }
                                    }
                                    else
                                    {
                                        var cancelOrderAPI = new DC_OCM_Update();
                                        cancelOrderAPI.ma_don_hang = item.RefID;
                                        cancelOrderAPI.code_token = item.OrderID;
                                        cancelOrderAPI.ma_giao_dich = item.OrderID;
                                        cancelOrderAPI.ngay_giao_dich = item.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                                        cancelOrderAPI.p_tai_khoan_thanh_toan = "liemnm@twin.vn";
                                        cancelOrderAPI.ma_ngan_hang = !string.IsNullOrEmpty(item.Bank) ? item.Bank : "BA00001";
                                        cancelOrderAPI.goi_tra_gop = 12;
                                        cancelOrderAPI.p_id_cong_thanh_toan = 5;
                                        cancelOrderAPI.p_loai_thanh_toan = 9;
                                        cancelOrderAPI.api_key = "8b8a689ce420";
                                        cancelOrderAPI.api_secret = "0124ccc29bc3c7e287a714d7eb99b2ce";
                                        cancelOrderAPI.trang_thai_don_hang = 2;
                                        cancelOrderAPI.id_ly_do_huy = 1;

                                        //goi api update
                                        var decaOrderApi = Helpers.DecaAPI.updateOCMOrder(cancelOrderAPI);
                                        if (decaOrderApi.response_code != "0")
                                        {

                                            return Json(new { success = false, message = item.OrderID + ": " + decaOrderApi.message });
                                        }
                                    }



                                }
                                s++;
                            }
                            else
                            {
                                f++;
                            }
                        }
                        dbTrans.Commit();
                        return Json(new { success = s, fail = f, error = false });
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return Json(new { error = true, message = e.Message });
                    }
                }
                else
                {
                    if (!asset.Update) return Json(new { error = true, message = "Không có quyền hủy đơn hàng." });
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

        public ActionResult getBuyInfo(string physicalId)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new Dictionary<string, dynamic>();
                var customer = dbConn.SingleOrDefault<Deca_Customer>("PhysicalID={0}", physicalId);
                if (customer != null)
                {
                    var company = dbConn.SingleOrDefault<Deca_Company>("Id={0}", customer.CompanyID);
                    if (string.IsNullOrEmpty(customer.BranchID))
                    {
                        var city = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", company.City);
                        var district = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", company.District);
                        data.Add("customer", customer);
                        data.Add("company", company);
                        data.Add("city", city);
                        data.Add("district", district);
                    }
                    else
                    {
                        //get branch
                        DC_Company_Branch branch = dbConn.FirstOrDefault<DC_Company_Branch>("CompanyID={0} AND BranchID={1}", company.Id, customer.BranchID);
                        var city = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", branch.CityID);
                        customer.BranchName = branch.BranchName;
                        company.Address = branch.Address;
                        var district = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", branch.DistrictID);
                        data.Add("customer", customer);
                        data.Add("company", company);
                        data.Add("city", city);
                        data.Add("district", district);
                    }
                }
                else
                {
                    var potentialCustomer = dbConn.SingleOrDefault<Deca_Potential_Customer>("PhysicalID={0}", physicalId);
                    var company = dbConn.SingleOrDefault<Deca_Company>("Id={0}", potentialCustomer.CompanyID);
                    //
                    if (string.IsNullOrEmpty(potentialCustomer.BranchID))
                    {
                        var city = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", company.City);
                        var district = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", company.District);

                        data.Add("customer", potentialCustomer);
                        data.Add("company", company);
                        data.Add("city", city);
                        data.Add("district", district);
                    }
                    else
                    {
                        //get branch
                        DC_Company_Branch branch = dbConn.FirstOrDefault<DC_Company_Branch>("CompanyID={0} AND BranchID={1}", company.Id, potentialCustomer.BranchID);
                        var city = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", branch.CityID);
                        var district = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", branch.DistrictID);
                        potentialCustomer.BranchName = branch.BranchName;
                        company.Address = branch.Address;
                        data.Add("customer", potentialCustomer);
                        data.Add("company", company);
                        data.Add("city", city);
                        data.Add("district", district);
                    }

                }
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CreateOrder(string OrderID_Old, List<Deca_BuyInfo> listdata, string note, int checkchangeAddress)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var Info_OrderID_Old = dbConn.SingleOrDefault<Deca_Order_Header>("OrderID={0}", OrderID_Old);
                    
                    var orderId = String.Empty;
                    double Amount = 0;
                    string ocmMessage = string.Empty;
                    if (listdata.Select(s => s.merchantID).Distinct().Count() > 1)
                    {
                        return Json(new { success = false, error = "Vui lòng chọn sản phẩm cùng nhà cung cấp" });
                    }

                    //create header

                    using (var dbTran = dbConn.OpenTransaction())
                    {

                        //generate orderId
                        var maxOrderId = dbConn.Scalar<string>("SELECT RIGHT(MAX(OrderID),11) FROM Deca_Order_Header");
                        if (!String.IsNullOrEmpty(maxOrderId) && maxOrderId.Substring(0, 6) == DateTime.Now.ToString("yyMMdd"))
                        {
                            double nextNo = double.Parse(maxOrderId.Substring(6, 5)) + 1;
                            orderId = "TG" + DateTime.Now.ToString("yyMMdd") + string.Format("{0:00000}", nextNo);
                        }
                        else
                        {
                            orderId = "TG" + DateTime.Now.ToString("yyMMdd") + "00001";
                        }

                        //create header
                        var header = new Deca_Order_Header();
                        header.OrderDate = Info_OrderID_Old.OrderDate;

                        header.CompletedDate = Info_OrderID_Old.CompletedDate;
                        header.OrderID = orderId;
                        // Chuyển thành Trạng thái đơn hàng mới
                        header.Status = "0";
                        //
                        header.CustomerID = Info_OrderID_Old.CustomerID;
                        header.CustomerName = Info_OrderID_Old.CustomerName;
                        header.CompanyID = Info_OrderID_Old.CompanyID;
                        header.EmployeeID = Info_OrderID_Old.EmployeeID;
                        header.PhysicalID = Info_OrderID_Old.PhysicalID;
                        header.MobilePhone = Info_OrderID_Old.MobilePhone;
                        header.Email = Info_OrderID_Old.Email;
                        header.Source = Info_OrderID_Old.Source;
                        if (Info_OrderID_Old.Source == "SMSMO")
                        {
                            header.Note = "[Đơn hàng SMS]-[TG 12 tháng]" + " - " + "[CMND: " + Info_OrderID_Old.PhysicalID + "] " + " - " + "[Kiểm tra ảnh trên CMND] " + note;
                        }
                        else
                        {
                            header.Note = "[TG 12 tháng]" + " - " + "[CMND: " + Info_OrderID_Old.PhysicalID + "] " + " - " + "[Kiểm tra ảnh trên CMND] " + note;
                        }

                        //change address
                        if (checkchangeAddress == 0)
                        {
                            header.ShippingAddress = Info_OrderID_Old.ShippingAddress;
                            header.ShippingCity = Info_OrderID_Old.ShippingCity;
                            header.ShippingDistrict = Info_OrderID_Old.ShippingDistrict;
                        }
                        else
                        {
                            var lastshippingaddress = dbConn.FirstOrDefault<Deca_Customer_LastShippingAddress>("PhysicalID={0}", Info_OrderID_Old.PhysicalID);
                            header.ShippingAddress = lastshippingaddress.Address;
                            header.ShippingCity = int.Parse(lastshippingaddress.CityID);
                            header.ShippingDistrict = int.Parse(lastshippingaddress.DistrictID);
                        }


                        header.Installment = Info_OrderID_Old.Installment;
                        header.PaymentStatus = Info_OrderID_Old.PaymentStatus;
                        header.SaleMan = Info_OrderID_Old.SaleMan;
                        header.Bank = Info_OrderID_Old.Bank;
                        header.CreatedAt = DateTime.Now;
                        header.CreatedBy = currentUser.UserName;
                        header.ShopID = Info_OrderID_Old.ShopID;
                        header.ShopName = Info_OrderID_Old.ShopName;
                        dbConn.Insert(header);
                        int Id = (int)dbConn.GetLastInsertId();
                        header.ID = Id;

                        var item = new List<KeyValuePair<string, DC_OCM_Item>>();
                        foreach (var data in listdata)
                        {
                            var sku = dbConn.SingleOrDefault<DC_SKU_Property>("productId={0} AND colorId={1} AND merchantID={2}", data.productId, data.colorId, data.merchantID);
                            //create detail
                            var detail = new Deca_Order_Detail();
                            detail.SKUID = sku.sku;
                            detail.PriceID = sku.productPriceId;
                            detail.OrderID = header.OrderID;
                            detail.ProductID = data.productId.ToString();
                            detail.ProductName = data.name;
                            detail.Quantity = data.quantity;
                            detail.UnitPrice = data.sellingPrice;
                            detail.LineAmount = data.sellingPrice * data.quantity;
                            detail.Status = "0";
                            detail.CreatedAt = DateTime.Now;
                            detail.CreatedBy = currentUser.UserName;
                            dbConn.Insert(detail);

                            Amount += data.sellingPrice * data.quantity;
                            //add item
                            var element = new KeyValuePair<string, DC_OCM_Item>(data.productId.ToString(), new DC_OCM_Item() { price = data.sellingPrice, number = data.quantity, code_price = sku.productPriceId.ToString() });
                            item.Add(element);
                        }
                        header.Amount = Amount;
                        header.BankFee = 0.0199 * header.Amount;
                        header.TotalAmount = header.Amount + header.BankFee;
                        header.PayPerMonth = Math.Round(header.TotalAmount / 12, 0);

                        dbConn.Update(header);
                        //commit transaction
                        dbTran.Commit();
                    }
                    return Json(new { success = true, orderId = orderId, Amount = Amount, ocmMessage = ocmMessage });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }
        [HttpPost]
        public ActionResult Excel_Export(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
        [HttpPost]
        public ActionResult GetShippingAddress(string PhysicalID)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = new Dictionary<string, dynamic>();
                var customer = dbConn.SingleOrDefault<Deca_Customer>("PhysicalID={0}", PhysicalID);
                if (customer != null)
                {
                    var company = dbConn.SingleOrDefault<Deca_Company>("Id={0}", customer.CompanyID);
                    //get branch
                    var branch = dbConn.Select<DC_Company_Branch>("CompanyID={0}", company.Id);
                    if (branch.Count < 1)
                        branch.Insert(0, new DC_Company_Branch
                        {
                            BranchID = "",
                            Address = company.Address,
                            DistrictID = company.District,
                            CityID = company.City,
                            DistrictName = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", company.District).TerritoryName,
                            CityName = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", company.City).TerritoryName
                        });
                    else
                    {
                        foreach (var b in branch)
                        {
                            b.DistrictName = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", b.DistrictID).TerritoryName;
                            b.CityName = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", b.CityID).TerritoryName;
                        }
                    }
                    data.Add("shipping", branch);
                }
                else
                {
                    var potentialCustomer = dbConn.SingleOrDefault<Deca_Potential_Customer>("PhysicalID={0}", PhysicalID);
                    var company = dbConn.SingleOrDefault<Deca_Company>("Id={0}", potentialCustomer.CompanyID);
                    //
                    //get branch
                    var branch = dbConn.Select<DC_Company_Branch>("CompanyID={0}", company.Id);
                    if (branch.Count < 1)
                        branch.Insert(0, new DC_Company_Branch
                        {
                            BranchID = "",
                            Address = company.Address,
                            DistrictID = company.District,
                            CityID = company.City,
                            DistrictName = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", company.District).TerritoryName,
                            CityName = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", company.City).TerritoryName
                        });
                    else
                    {
                        foreach (var b in branch)
                        {
                            b.DistrictName = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", b.DistrictID).TerritoryName;
                            b.CityName = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", b.CityID).TerritoryName;
                        }
                    }
                    data.Add("shipping", branch);
                }
                return Json(data);
            }
        }
        [HttpPost]
        public ActionResult GetAddressByBranchID(string BranchID)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = new Dictionary<string, dynamic>();
                var currentBranch = dbConn.FirstOrDefault<DC_Company_Branch>("BranchID={0}", BranchID);
                currentBranch.DistrictName = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", currentBranch.DistrictID).TerritoryName;
                currentBranch.CityName = dbConn.SingleOrDefault<DC_OCM_Territory>("TerritoryID={0}", currentBranch.CityID).TerritoryName;
                var currentCompany = dbConn.FirstOrDefault<Deca_Company>("Id=" + currentBranch.CompanyID);
                data.Add("CompanyName", currentCompany.ShortName);
                data.Add("BranchName", currentBranch.BranchName);
                data.Add("Address", currentBranch.Address);
                data.Add("DistrictName", currentBranch.DistrictName);
                data.Add("CityName", currentBranch.CityName);
                data.Add("CityID", currentBranch.CityID);
                data.Add("DistrictID", currentBranch.DistrictID);
                return Json(data);
            }
        }
        [HttpPost]
        public ActionResult SaveCustomDeliveryAddress(string PhysicalID, string Address, string CityID, string DistrictID)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                try
                {
                    if (string.IsNullOrEmpty(PhysicalID) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(CityID) || string.IsNullOrEmpty(DistrictID))
                    {
                        throw new Exception("Vui lòng nhập đầy đủ thông tin");
                    }
                    else
                    {
                        Deca_Customer_LastShippingAddress shipping = dbConn.FirstOrDefault<Deca_Customer_LastShippingAddress>("PhysicalID={0}", PhysicalID);
                        if (shipping == null)
                        {
                            shipping = new Models.Deca_Customer_LastShippingAddress();
                            shipping.Address = Address;
                            shipping.CityID = CityID;
                            shipping.CreatedAt = DateTime.Now;
                            shipping.CreatedBy = currentUser.UserName;
                            shipping.DistrictID = DistrictID;
                            shipping.CustomerID = "";
                            shipping.PhysicalID = PhysicalID;
                            shipping.UpdatedAt = DateTime.Now;
                            shipping.UpdatedBy = currentUser.UserName;
                            dbConn.Insert(shipping);
                        }
                        else
                        {
                            shipping.Address = Address;
                            shipping.CityID = CityID;
                            shipping.DistrictID = DistrictID;
                            shipping.CustomerID = "";
                            shipping.PhysicalID = PhysicalID;
                            shipping.UpdatedAt = DateTime.Now;
                            shipping.UpdatedBy = currentUser.UserName;
                            dbConn.Update(shipping);
                        }
                        return Json(new { success = true, message = "success" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }
        [HttpPost]
        public ActionResult GetCurrentCustomDeliveryAddress(string PhysicalID)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var current = dbConn.FirstOrDefault<Deca_Customer_LastShippingAddress>("PhysicalID={0}", PhysicalID);
                if (current == null)
                    return Json(new { hasValue = false });
                else return Json(new { hasValue = true, data = current });
            }
        }
    }
}