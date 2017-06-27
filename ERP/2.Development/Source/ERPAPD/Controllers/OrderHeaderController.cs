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
using Dapper;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class OrderHeaderController : CustomController
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
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    string strQuery = @"SELECT oh.*,  
                                        ISNULL(ct.ReasonID,'') AS ReasonID, 
                                        ISNULL(ct.[Description],'') AS CancelReason, 
                                        ISNULL(bt.CreatedAt,'1900-01-01') AS PaymentDate 
                                        FROM Deca_Order_Header oh 
                                        LEFT JOIN Deca_Order_CancelTracking ct 
                                        ON oh.OrderID = ct.OrderID 
                                        LEFT JOIN Deca_Bank_Transactions bt 
                                        ON oh.OrderID = bt.OrderID 
                                         ";
                    data = KendoApplyFilter.KendoDataByQuery<Deca_Order_Header>(request, strQuery, "");
                }
                return Json(data);
            }
        }
        public ActionResult Detail_Read([DataSourceRequest]DataSourceRequest request, string HeaderId)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<Deca_Order_Detail>();
                if (asset.View)
                {

                    data = dbConn.Select<Deca_Order_Detail>("HeaderId={0}", HeaderId);
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
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_Order.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "Deca_Order_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string query = "";
                    var data = new List<Deca_Order_ExcelModel>();
                    if (request.Filters.Any())
                    {
                        var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                        query += " select TOP 10000 oh.*,od.ProductID, od.ProductName, od.Quantity, od.UnitPrice,od.LineAmount,od.PriceID,od.SKUID, ISNULL(ct.[ReasonID],'') AS CancelReason, ISNULL(ct.[Description],'') AS CancelNote  ";
                        query += " FROM ";
                        query += " (SELECT * FROM Deca_Order_Header";
                        query += " WHERE " + where + ") oh ";
                        query += " LEFT JOIN Deca_Order_Detail od ";
                        query += " ON oh.OrderID = od.OrderID ";
                        query += " LEFT JOIN Deca_Order_CancelTracking ct";
                        query += " ON oh.OrderID = ct.OrderID ";

                        data = dbConn.Select<Deca_Order_ExcelModel>(query).ToList();
                    }
                    else
                    {
                        query += " select TOP 10000 oh.*,od.ProductID, od.ProductName, od.Quantity, od.UnitPrice,od.LineAmount,od.PriceID,od.SKUID, ISNULL(ct.[ReasonID],'') AS CancelReason,ISNULL(ct.[Description],'') AS CancelNote  ";
                        query += " FROM ";
                        query += " Deca_Order_Header oh ";
                        query += " LEFT JOIN Deca_Order_Detail od ";
                        query += " ON oh.OrderID = od.OrderID ";
                        query += " LEFT JOIN Deca_Order_CancelTracking ct";
                        query += " ON oh.OrderID = ct.OrderID ";
                        data = dbConn.Select<Deca_Order_ExcelModel>(query).ToList();
                    }

                    ExcelWorksheet expenseSheet = excelPkg.Workbook.Worksheets["Orders"];
                    var dataCity = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "Province").OrderBy(a => a.TerritoryName);
                    var dataDistrict = dbConn.Select<DC_OCM_Territory>("[Level] = {0}", "District").OrderBy(a => a.TerritoryName);
                    var dataBank = dbConn.Select<DC_Bank_Definition>().OrderBy(a => a.BankName);
                    var dataReason = dbConn.Select<Deca_Code_Master>("CodeType='CancelOrderReason'");
                    var dataStatus = dbConn.Select<Deca_Order_Status>();
                    int rowData = 1;
                    foreach (var item in data)
                    {
                        string paymentStatus = "";
                        if (item.PaymentStatus == "1")
                            paymentStatus = "Đã thanh toán";
                        else if (item.PaymentStatus == "0")
                            paymentStatus = "Chưa thanh toán";
                        else if (item.PaymentStatus == "-1")
                            paymentStatus = "Thanh toán thất bại";
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
                        expenseSheet.Cells[rowData, i++].Value = dataReason.FirstOrDefault(a => a.CodeID == item.CancelReason) == null ? "" : dataReason.FirstOrDefault(a => a.CodeID == item.CancelReason).Description;
                        expenseSheet.Cells[rowData, i++].Value = item.CancelNote;
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
                        expenseSheet.Cells[rowData, i++].Value = dataCity.FirstOrDefault(a => a.ID == item.ShippingCity) == null ? "" : dataCity.FirstOrDefault(a => a.ID == item.ShippingCity).TerritoryName;
                        expenseSheet.Cells[rowData, i++].Value = dataDistrict.FirstOrDefault(a => a.ID == item.ShippingDistrict) == null ? "" : dataDistrict.FirstOrDefault(a => a.ID == item.ShippingDistrict).TerritoryName;
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

        //requestbank
        public ActionResult RequestBank(string listOrderID, string Description, string BankID)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                if (asset.Update && ModelState.IsValid)
                {
                    if (!String.IsNullOrEmpty(BankID))
                    {
                        try
                        {
                            int s = 0, f = 0;
                            string[] separators = { "@@" };
                            var listid = listOrderID.Split(separators, StringSplitOptions.RemoveEmptyEntries).Distinct();
                            var listPotential = dbConn.Select<Deca_Potential_Customer>("PhysicalID IN (" + string.Join(",", listid.Select(p => "'" + p + "'")) + ") AND CreditCardStatus = 'CCS001' AND HaveCard = 0");
                            if (listPotential.Count > 0)
                            {
                                foreach (var potential in listPotential)
                                {
                                    if (potential.IsForm)
                                    {
                                        potential.CreditCardStatus = "CCS004";  // Gửi NH
                                        potential.SubmitedAt = DateTime.Now;
                                    }
                                    else
                                    {
                                        potential.CreditCardStatus = "CCS002"; // Gửi Deca
                                        potential.DecaRequested = DateTime.Now;
                                    }
                                    potential.RequestCreditBank = BankID;
                                    potential.DecaNote = Description;

                                    potential.UpdatedAt = DateTime.Now;
                                    potential.UpdatedBy = currentUser.UserName;
                                    dbConn.Update(potential);
                                    s++;
                                }
                            }
                            else
                            {
                                return Json(new { error = true, message = "Các Khách hàng này đang được xử lý." });
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
                        return Json(new { error = true, message = "Vui lòng chọn ngân hàng" });
                    }
                }
                else
                {
                    if (!asset.Update) return Json(new { error = true, message = "Không có quyền." });
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

        //createOCMOrder
        public ActionResult CreateOCMOrder(string data)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    if (asset.Update)
                    {
                        string[] separators = { "@@" };
                        var listid = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        var listData = dbConn.Select<Deca_Order_Header>("ID IN (" + string.Join(",", listid.Select(s => s)) + ") AND Status <> 10");
                        foreach (var order in listData)
                        {
                            //da tao order tren ocm
                            if (!String.IsNullOrEmpty(order.RefID))
                            {
                                if (order.PaymentStatus == "1")
                                {
                                    var bankTrans = dbConn.SingleOrDefault<Deca_Bank_Transactions>("OrderID={0}", order.OrderID);

                                    //thong in update truyen di
                                    var updateOrderAPI = new DC_OCM_Update();
                                    updateOrderAPI.ma_don_hang = order.RefID;
                                    updateOrderAPI.code_token = order.OrderID;
                                    updateOrderAPI.ma_giao_dich = bankTrans.BillTransRef;
                                    updateOrderAPI.ngay_giao_dich = order.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                                    updateOrderAPI.p_tai_khoan_thanh_toan = "liemnm@twin.vn";
                                    updateOrderAPI.ma_ngan_hang = !string.IsNullOrEmpty(order.Bank) ? order.Bank : "BA00001";
                                    updateOrderAPI.goi_tra_gop = 12;
                                    updateOrderAPI.p_id_cong_thanh_toan = 5;
                                    updateOrderAPI.p_loai_thanh_toan = 9;
                                    updateOrderAPI.api_key = "8b8a689ce420";
                                    updateOrderAPI.api_secret = "0124ccc29bc3c7e287a714d7eb99b2ce";
                                    updateOrderAPI.trang_thai_don_hang = 1;

                                    //goi api update
                                    var decaOrderApi = Helpers.DecaAPI.updateOCMOrder(updateOrderAPI);

                                    if (decaOrderApi.response_code != "0")
                                    {
                                        return Json(new { success = false, message = order.OrderID + ": " + decaOrderApi.message });
                                    }
                                }
                                else
                                {
                                    return Json(new { success = false, message = order.OrderID + ": Đã được tạo trên OCM" });
                                }
                            }
                            else
                            {
                                //da thanh toan va chua tao order tren ocm
                                if (order.PaymentStatus == "1")
                                {
                                    var orderitem = dbConn.SingleOrDefault<Deca_Order_Detail>("OrderID={0}", order.OrderID);
                                    //lay thong tin transaction
                                    var bankTrans = dbConn.SingleOrDefault<Deca_Bank_Transactions>("OrderID={0}", order.OrderID);

                                    var item = new List<KeyValuePair<string, DC_OCM_Item>>();
                                    var element = new KeyValuePair<string, DC_OCM_Item>(orderitem.ProductID, new DC_OCM_Item() { price = orderitem.UnitPrice, number = orderitem.Quantity, code_price = orderitem.PriceID.ToString() });
                                    item.Add(element);

                                    //tao request 
                                    DC_OCM_Create_Order orderApi = new DC_OCM_Create_Order();
                                    orderApi.p_id_nguoi_mua_hang = "67995";//config
                                    orderApi.p_id_gian_hang = int.Parse(order.ShopID);
                                    orderApi.san_pham = item.ToDictionary(s => s.Key, s => s.Value);
                                    orderApi.p_id_tinh = order.ShippingCity;
                                    orderApi.p_id_quan_huyen = order.ShippingDistrict;
                                    orderApi.p_so_luong_san_pham = 1;
                                    orderApi.p_loai_nt = "VND";//config
                                    orderApi.p_ten_nguoi_nhan_hang = order.CustomerName;
                                    orderApi.p_dia_chi_nhan_hang = order.ShippingAddress;
                                    orderApi.p_dien_thoai_nhan_hang = order.MobilePhone;
                                    orderApi.p_ten_nguoi_nhan_hang_khong_dau = Helpers.convertToUnSign3.Init(order.CustomerName);
                                    orderApi.p_dia_chi_nhan_hang_khong_dau = Helpers.convertToUnSign3.Init(order.ShippingAddress);
                                    orderApi.p_dien_thoai_di_dong_chuan = order.MobilePhone;
                                    orderApi.p_id_cong_thanh_toan = 5;//config
                                    orderApi.p_tai_khoan_thanh_toan = "liemnm@twin.vn";//config
                                    orderApi.ma_ngan_hang = !String.IsNullOrEmpty(order.Bank) ? order.Bank : "ShinhanBank";
                                    orderApi.p_loai_thanh_toan = 9;//config
                                    orderApi.gia_tri_thanh_toan = orderitem.UnitPrice * orderitem.Quantity;
                                    orderApi.code_token = orderitem.OrderID;
                                    orderApi.ma_giao_dich = bankTrans.BillTransRef;//giao dich cong thanh toan
                                    orderApi.ngay_giao_dich = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    orderApi.api_key = "8b8a689ce420";
                                    orderApi.api_secret = "0124ccc29bc3c7e287a714d7eb99b2ce";
                                    orderApi.goi_tra_gop = 12;
                                    orderApi.trang_thai_thanh_toan = 1;
                                    orderApi.ghi_chu = !string.IsNullOrEmpty(order.Note) ? order.Note : "không có thông tin";
                                    //call helper create order OCM
                                    var decaOrderApi = Helpers.DecaAPI.createOCMOrder(orderApi);

                                    if (decaOrderApi.response_code == "0")
                                    {
                                        dbConn.Execute("UPDATE Deca_Order_Header SET RefID = '" + decaOrderApi.code_order + "', UpdatedAt = GETDATE(),UpdatedBy = '" + currentUser.UserName + "' WHERE OrderID = '" + order.OrderID + "'");
                                    }
                                    else
                                    {
                                        return Json(new { success = false, message = order.OrderID + ": " + decaOrderApi.error_message });
                                    }
                                }
                                else // chua thanh toan va tao order tren ocm
                                {
                                    var orderitem = dbConn.SingleOrDefault<Deca_Order_Detail>("OrderID={0}", order.OrderID);

                                    var item = new List<KeyValuePair<string, DC_OCM_Item>>();
                                    var element = new KeyValuePair<string, DC_OCM_Item>(orderitem.ProductID, new DC_OCM_Item() { price = orderitem.UnitPrice, number = orderitem.Quantity, code_price = orderitem.PriceID.ToString() });
                                    item.Add(element);

                                    DC_OCM_Create_Order orderApi = new DC_OCM_Create_Order();
                                    orderApi.p_id_nguoi_mua_hang = "67995";//config
                                    orderApi.p_id_gian_hang = int.Parse(order.ShopID);
                                    orderApi.san_pham = item.ToDictionary(s => s.Key, s => s.Value);
                                    orderApi.p_id_tinh = order.ShippingCity;
                                    orderApi.p_id_quan_huyen = order.ShippingDistrict;
                                    orderApi.p_so_luong_san_pham = 1;
                                    orderApi.p_loai_nt = "VND";//config
                                    orderApi.p_ten_nguoi_nhan_hang = order.CustomerName;
                                    orderApi.p_dia_chi_nhan_hang = order.ShippingAddress;
                                    orderApi.p_dien_thoai_nhan_hang = order.MobilePhone;
                                    orderApi.p_ten_nguoi_nhan_hang_khong_dau = Helpers.convertToUnSign3.Init(order.CustomerName);
                                    orderApi.p_dia_chi_nhan_hang_khong_dau = Helpers.convertToUnSign3.Init(order.ShippingAddress);
                                    orderApi.p_dien_thoai_di_dong_chuan = order.MobilePhone;
                                    orderApi.p_id_cong_thanh_toan = 5;//config
                                    orderApi.p_tai_khoan_thanh_toan = "liemnm@twin.vn";//config
                                    orderApi.ma_ngan_hang = !String.IsNullOrEmpty(order.Bank) ? order.Bank : "ShinhanBank";
                                    orderApi.p_loai_thanh_toan = 9;//config
                                    orderApi.gia_tri_thanh_toan = orderitem.UnitPrice * orderitem.Quantity;
                                    orderApi.code_token = orderitem.OrderID;
                                    orderApi.ma_giao_dich = orderitem.OrderID;//giao dich cong thanh toan
                                    orderApi.ngay_giao_dich = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    orderApi.api_key = "8b8a689ce420";
                                    orderApi.api_secret = "0124ccc29bc3c7e287a714d7eb99b2ce";
                                    orderApi.goi_tra_gop = 12;
                                    orderApi.trang_thai_thanh_toan = 0;
                                    orderApi.ghi_chu = !string.IsNullOrEmpty(order.Note) ? order.Note : "không có thông tin";
                                    //call helper create order OCM
                                    var decaOrderApi = Helpers.DecaAPI.createOCMOrder(orderApi);

                                    if (decaOrderApi.response_code == "0")
                                    {
                                        order.RefID = decaOrderApi.code_order;
                                        order.UpdatedAt = DateTime.Now;
                                        order.UpdatedBy = currentUser.UserName;
                                        dbConn.Update(order);
                                    }
                                    else
                                    {
                                        return Json(new { success = false, message = order.OrderID + ": " + decaOrderApi.error_message });
                                    }
                                }


                            }


                        }
                    }
                    else
                    {
                        return Json(new { success = false, message = "Không có quyền gửi" });
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
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