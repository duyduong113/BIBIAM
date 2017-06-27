using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DecaPay.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using DecaPay.Helpers;
using System.Data;
using System.IO;
using OfficeOpenXml;
using Dapper;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace DecaPay.Controllers
{
    [Authorize]
    [RequireHttps]
    public class SalesOrderController : CustomController
    {
        //
        // GET: /SalesOrder/
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(Sales_Order_Customer), typeof(Sales_Order_Detail), typeof(Sales_Order_Header), typeof(Sales_Item));
            //    const bool overwrite = true;
            //    dbConn.CreateTables(overwrite, typeof(Sales_Item), typeof(Sales_Order_Header), typeof(Sales_Order_Detail), typeof(Sales_Order_Customer));
            //}
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(Sales_Item));
            //    const bool overwrite = true;
            //    dbConn.CreateTables(overwrite, typeof(Sales_Item));
            //}
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {

                }
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        //list don hang
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<Sales_Order_Header>(request, "1=1");
                }
                return Json(data);
            }
        }

        //list detail cua don hang
        public ActionResult Detail_Read([DataSourceRequest]DataSourceRequest request, string Id)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<Sales_Order_Detail>();
                if (asset.View)
                {

                    data = dbConn.Select<Sales_Order_Detail>("HeaderId={0}", Id);
                }
                return Json(data.ToDataSourceResult(request));
            }
        }
        //list order history
        public ActionResult OrderHistoryRead([DataSourceRequest]DataSourceRequest request, string CustomerId)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<Sales_Order_Header>();
                if (asset.View)
                {
                    data = dbConn.Select<Sales_Order_Header>("CustomerId={0}", CustomerId);
                }
                return Json(data.ToDataSourceResult(request));
            }
        }

        //list customer khi search
        public ActionResult CustomerSearchRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(KendoApplyFilter.KendoData<Sales_Order_Customer>(request));
        }

        //list customer khi search
        public ActionResult BarcodeSearchRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(KendoApplyFilter.KendoData<Sales_Item>(request));
        }

        //man hinh create don hang
        public ActionResult Create()
        {
            if (asset.Create)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    if (dbConn.SingleOrDefault<Sales_Item>("1=1") == null)
                    {
                        for (int i = 0; i < 1000; i++)
                        {
                            dbConn.Insert(new Sales_Item { MSIN = "MSIN" + i, SKU = "SKU" + i, Barcode = "B" + i, ItemName = "Sản phẩm " + i, Unit = "Cái", UnitPrice = 1000000, CreatedAt = DateTime.Now, CreatedBy = "administrator" });
                        }
                    }

                }


                var data = new Sales_Order_Header();
                return View(data);
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        //search customer by id
        public ActionResult LoadCustomer(int Id)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var data = dbConn.SingleOrDefault<Sales_Order_Customer>("Id={0}", Id);
                    return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message }, JsonRequestBehavior.AllowGet);
                }

            }
        }

        //get item
        public ActionResult GetItem(string barCode)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = dbConn.SingleOrDefault<Sales_Item>("Barcode={0}", barCode);
                    return Json(new { success = true, data = data });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        //save customer
        [HttpPost]
        public ActionResult SaveCustomer(Sales_Order_Customer customer)
        {
            try
            {
                if (String.IsNullOrEmpty(customer.FirstName) ||
                        String.IsNullOrEmpty(customer.LastName) ||
                        String.IsNullOrEmpty(customer.Birthday.ToString()) ||
                        String.IsNullOrEmpty(customer.Phone) ||
                        String.IsNullOrEmpty(customer.Address))
                {
                    return Json(new { success = false, error = "Vui lòng nhập đầy đủ thông tin bắt buộc" });
                }

                using (var dbConn = Helpers.OrmliteConnection.openConn())
                using (var dbTrans = dbConn.OpenTransaction())
                {
                    customer.MetaSearch = Helpers.convertToUnSign3.Init(customer.FirstName.ToLower()) + ";" + Helpers.convertToUnSign3.Init(customer.LastName.ToLower()) + ";" + Helpers.convertToUnSign3.Init(customer.Phone.ToLower()) + ";" + (!String.IsNullOrEmpty(customer.Email) ? Helpers.convertToUnSign3.Init(customer.Email.ToLower()) + ";" : "");
                    customer.CreatedAt = DateTime.Now;
                    customer.CreatedBy = currentUser.UserName;
                    dbConn.Insert(customer);

                    int Id = (int)dbConn.GetLastInsertId();
                    customer.Id = Id;
                    string datetime = DateTime.Now.ToString("yyMMdd");
                    customer.CustomerId = "CU" + datetime + Id;
                    dbConn.Update(customer);
                    dbTrans.Commit();

                    return Json(new { success = true, Id = Id });
                }


            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        public void Print()
        {

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "SalesOrder.rpt"));

            // send the resulting document to the user (as PDF).
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "PrintBBBGHH_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                rd.Close();
                rd.Dispose();
            }
        }

        [HttpPost]
        public ActionResult PrintSalesOrder(int customerId, string PromotionCode, double customerMoney, double Discount, List<Sales_Order_Detail> listDetails)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var customer = dbConn.SingleOrDefault<Sales_Order_Customer>("Id={0}", customerId);
                    ReportDocument rd = new ReportDocument();
                    rd.Load(Path.Combine(Server.MapPath("~/Reports"), "SalesOrder.rpt"));
                    listDetails.ForEach(s => { s.Amount = s.Quantity * s.UnitPrice; s.Note = ""; s.CreatedAt = DateTime.Now; s.UpdatedAt = DateTime.Now; s.UpdatedBy = currentUser.UserName; });
                    var amount = listDetails.Sum(s => s.Amount);
                    var newdata = listDetails.Select(s => new Sales_Order_Detail_Report
                    {
                        ItemName = s.ItemName,
                        Quantity = s.Quantity,
                        UnitPrice = s.UnitPrice,
                        Amount = s.Amount
                    });

                    double PromotionAmount = 0;
                    switch (PromotionCode.ToUpper())
                    {
                        case "MGG10":
                            PromotionAmount = 10000;
                            break;
                        case "MGG20":
                            PromotionAmount = 20000;
                            break;
                        case "MGG30":
                            PromotionAmount = 30000;
                            break;
                        default:
                            break;
                    }

                    rd.SetDataSource(newdata.ToList());
                    rd.SetParameterValue("SoHD", "");
                    rd.SetParameterValue("KH", customer.CustomerId + "-" + customer.FirstName + " " + customer.LastName);
                    rd.SetParameterValue("DC", customer.Address);
                    rd.SetParameterValue("DoanhSoTichLuy", customer.IncomePoint);
                    rd.SetParameterValue("user", currentUser.UserName);
                    rd.SetParameterValue("amount", amount);
                    rd.SetParameterValue("discount", amount * 10 / 100);
                    rd.SetParameterValue("total", amount * 90 / 100 - PromotionAmount - Discount * 1000);
                    rd.SetParameterValue("customermoney", customerMoney);
                    rd.SetParameterValue("cashback", customerMoney - (amount * 90 / 100 - PromotionAmount - Discount * 1000));

                    // send the resulting document to the user (as PDF).
                    Response.ClearContent();
                    Response.ClearHeaders();

                    try
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Content/SalesOrder"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + "_SalesOrder.pdf");
                        rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fileLocation);

                        return Json(new { success = true, fileLocation = "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + "_SalesOrder.pdf" });
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        rd.Close();
                        rd.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        public virtual FileResult Download(string file)
        {
            string fullPath = Path.Combine(Server.MapPath("~/UploadFile/SalesOrder"), file);
            return File(fullPath, "application/pdf", DateTime.Now.ToString("ddMMyyyyHHmmss") + "_SalesOrder.pdf");
        }

        [HttpPost]
        public ActionResult CreateOrder(int CustomerId, string PromotionCode, double Discount, string CustomerMoney, List<Sales_Order_Detail> Details)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                using (var dbTrans = dbConn.OpenTransaction())
                {
                    //find customer
                    var customer = dbConn.SingleOrDefault<Sales_Order_Customer>("Id={0}", CustomerId);
                    if (customer == null)
                    {
                        return Json(new { success = false, error = "Khách hàng không tồn tại trong hệ thống" }, JsonRequestBehavior.AllowGet);
                    }

                    //create order header
                    var orderHeader = new Sales_Order_Header();
                    orderHeader.CustomerId = customer.CustomerId;
                    orderHeader.FirstName = customer.FirstName;
                    orderHeader.LastName = customer.LastName;
                    orderHeader.OrderDate = DateTime.Now;
                    orderHeader.CreatedAt = DateTime.Now;
                    orderHeader.CreatedBy = currentUser.UserName;
                    orderHeader.PromotionCode = PromotionCode;
                    dbConn.Insert(orderHeader);

                    //update sales order
                    int Id = (int)dbConn.GetLastInsertId();
                    orderHeader.Id = Id;
                    string datetime = DateTime.Now.ToString("yyMMdd");
                    orderHeader.SalesOrderId = "SO" + datetime + Id;
                    dbConn.Update(orderHeader);

                    //insert order detail
                    double Amount = 0;
                    foreach (var item in Details)
                    {
                        var orderItem = dbConn.SingleOrDefault<Sales_Item>("MSIN={0}", item.MSIN);
                        item.HeaderId = Id;
                        item.UnitPrice = orderItem != null ? orderItem.UnitPrice : 0;
                        item.Amount = orderItem != null ? item.Quantity * orderItem.UnitPrice : 0;
                        item.CreatedAt = DateTime.Now;
                        item.CreatedBy = currentUser.UserName;
                        Amount += orderItem != null ? item.Quantity * orderItem.UnitPrice : 0;
                        dbConn.Insert(item);
                    }

                    var discount = Amount * 10 / 100;
                    Amount -= discount;
                    //discount tich luy
                    Amount -= Discount * 1000;
                    switch (PromotionCode.ToUpper())
                    {
                        case "MGG10":
                            Amount -= 10000;
                            orderHeader.PromotionAmount = 10000;
                            break;
                        case "MGG20":
                            Amount -= 20000;
                            orderHeader.PromotionAmount = 20000;
                            break;
                        case "MGG30":
                            Amount -= 30000;
                            orderHeader.PromotionAmount = 30000;
                            break;
                        default:
                            break;
                    }
                    //set amount for order header
                    orderHeader.Amount = Amount;

                    if (Amount < 0)
                    {
                        return Json(new { success = false, error = "Vui lòng thay đổi số tiền khách hàng" }, JsonRequestBehavior.AllowGet);
                    }
                    dbConn.Update(orderHeader);

                    customer.UsedPoint += Discount;
                    customer.IncomePoint += Amount;
                    dbConn.Update(customer);

                    dbTrans.Commit();

                }

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CreateOrderAndPrint(int CustomerId, string PromotionCode, double Discount, string CustomerMoney, List<Sales_Order_Detail> Details)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                using (var dbTrans = dbConn.OpenTransaction())
                {
                    //find customer
                    var customer = dbConn.SingleOrDefault<Sales_Order_Customer>("Id={0}", CustomerId);
                    if (customer == null)
                    {
                        return Json(new { success = false, error = "Khách hàng không tồn tại trong hệ thống" }, JsonRequestBehavior.AllowGet);
                    }

                    //create order header
                    var orderHeader = new Sales_Order_Header();
                    orderHeader.CustomerId = customer.CustomerId;
                    orderHeader.FirstName = customer.FirstName;
                    orderHeader.LastName = customer.LastName;
                    orderHeader.OrderDate = DateTime.Now;
                    orderHeader.CreatedAt = DateTime.Now;
                    orderHeader.CreatedBy = currentUser.UserName;
                    orderHeader.PromotionCode = PromotionCode;
                    dbConn.Insert(orderHeader);

                    //update sales order
                    int Id = (int)dbConn.GetLastInsertId();
                    orderHeader.Id = Id;
                    string datetime = DateTime.Now.ToString("yyMMdd");
                    orderHeader.SalesOrderId = "SO" + datetime + Id;
                    dbConn.Update(orderHeader);

                    //insert order detail
                    double Amount = 0;
                    foreach (var item in Details)
                    {
                        var orderItem = dbConn.SingleOrDefault<Sales_Item>("MSIN={0}", item.MSIN);
                        item.HeaderId = Id;
                        item.UnitPrice = orderItem != null ? orderItem.UnitPrice : 0;
                        item.Amount = orderItem != null ? item.Quantity * orderItem.UnitPrice : 0;
                        item.CreatedAt = DateTime.Now;
                        item.CreatedBy = currentUser.UserName;
                        Amount += orderItem != null ? item.Quantity * orderItem.UnitPrice : 0;
                        dbConn.Insert(item);
                    }

                    var discount = Amount * 10 / 100;
                    Amount -= discount;
                    //discount tich luy
                    Amount -= Discount * 1000;
                    switch (PromotionCode.ToUpper())
                    {
                        case "MGG10":
                            Amount -= 10000;
                            orderHeader.PromotionAmount = 10000;
                            break;
                        case "MGG20":
                            Amount -= 20000;
                            orderHeader.PromotionAmount = 20000;
                            break;
                        case "MGG30":
                            Amount -= 30000;
                            orderHeader.PromotionAmount = 30000;
                            break;
                        default:
                            break;
                    }
                    //set amount for order header
                    orderHeader.Amount = Amount;

                    if (Amount < 0)
                    {
                        return Json(new { success = false, error = "Vui lòng thay đổi số tiền khách hàng" }, JsonRequestBehavior.AllowGet);
                    }
                    dbConn.Update(orderHeader);

                    customer.UsedPoint += Discount;
                    customer.IncomePoint += Amount;
                    dbConn.Update(customer);

                    dbTrans.Commit();

                    ReportDocument rd = new ReportDocument();
                    rd.Load(Path.Combine(Server.MapPath("~/Reports"), "SalesOrder.rpt"));
                    Details.ForEach(s => { s.Amount = s.Quantity * s.UnitPrice; s.Note = ""; s.CreatedAt = DateTime.Now; s.UpdatedAt = DateTime.Now; s.UpdatedBy = currentUser.UserName; });
                    var amount = Details.Sum(s => s.Amount);
                    var newdata = Details.Select(s => new Sales_Order_Detail_Report
                    {
                        ItemName = s.ItemName,
                        Quantity = s.Quantity,
                        UnitPrice = s.UnitPrice,
                        Amount = s.Amount
                    });
                    rd.SetDataSource(newdata.ToList());
                    rd.SetParameterValue("SoHD", "SAL.CH40021140617002");
                    rd.SetParameterValue("KH", customer.CustomerId + "-" + customer.FirstName + " " + customer.LastName);
                    rd.SetParameterValue("DC", customer.Address);
                    rd.SetParameterValue("DoanhSoTichLuy", customer.IncomePoint);
                    rd.SetParameterValue("user", currentUser.UserName);
                    rd.SetParameterValue("amount", amount);
                    rd.SetParameterValue("discount", amount * 10 / 100);
                    rd.SetParameterValue("total", Amount);
                    rd.SetParameterValue("customermoney", CustomerMoney);
                    rd.SetParameterValue("cashback", Double.Parse(CustomerMoney.Replace(",", "")) - Amount);

                    // send the resulting document to the user (as PDF).
                    Response.ClearContent();
                    Response.ClearHeaders();

                    try
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Content/SalesOrder"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + "_SalesOrder.pdf");
                        rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fileLocation);

                        return Json(new { success = true, fileLocation = fileLocation });
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        rd.Close();
                        rd.Dispose();
                    }

                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message }, JsonRequestBehavior.AllowGet);
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