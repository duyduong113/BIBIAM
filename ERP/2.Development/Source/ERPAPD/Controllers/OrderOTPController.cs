using ServiceStack.OrmLite;
using System;
using System.Linq;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;

namespace ERPAPD.Controllers
{
    [Authorize]
    public class OrderOTPController : CustomController
    {
        //
        // GET: /OrderOTP/
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(OrderOTP_Log));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(OrderOTP_Log));
            //}
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
                    data = KendoApplyFilter.KendoData<OrderOTP>(request);
                }
                return Json(data);
            }
        }

        public ActionResult DecryptOTP(int ID)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = dbConn.FirstOrDefault<OrderOTP>("ID={0}", ID);
                    string result = convertToUnSign3.Decrypt(data.OTP);
                    //ghi log
                    OrderOTP_Log log = new OrderOTP_Log();
                    log.OrderID = data.OrderID;
                    log.OTP = data.OTP;
                    log.PhoneNumber = data.PhoneNumber;
                    log.ActionType = "View";
                    log.CreatedAt = DateTime.Now;
                    log.CreatedBy = currentUser.UserName;
                    dbConn.Insert(log);
                    return Json(new { success = true, data = result });
                }
            }
            else
            {
                return Json(new { success = false, message = "Không có quyền xem OTP." });
            }
        }
        [HttpPost]
        public ActionResult ResendOTP(int ID)
        {
            if (asset.Update)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    OrderOTP oo = dbConn.FirstOrDefault<OrderOTP>("ID={0}", ID);
                    if (oo == null)
                    {
                        return Json(new { success = false, message = "Không tìm thấy." });
                    }
                    else
                    {
                        if (oo.Status != "active")
                            return Json(new { success = false, message = "Lỗi 503: Nhập sai OTP quá số lần quy định." });

                        string result = Helpers.SendSMS.Send(oo.PhoneNumber, "Ma xac nhan giao dich cho don hang " + oo.OrderID + " la: " + convertToUnSign3.Decrypt((oo.OTP)) + ". Hieu luc trong 5 phut.");
                        if (result != "@Resources.Multi.Success")
                        {
                            return Json(new { success = false, message = "Không gửi được tin nhắn. Thử lại" });
                        }
                        oo.SendTime += 1;
                        oo.CreatedTime = DateTime.Now;
                        dbConn.Update(oo);
                        //ghi log
                        OrderOTP_Log log = new OrderOTP_Log();
                        log.OrderID = oo.OrderID;
                        log.OTP = oo.OTP;
                        log.PhoneNumber = oo.PhoneNumber;
                        log.ActionType = "Resend";
                        log.CreatedAt = DateTime.Now;
                        log.CreatedBy = currentUser.UserName;
                        dbConn.Insert(log);
                        return Json(new { success = true, message = "" });

                    }
                }
            }
            else
            {
                return Json(new { success = false, message = "Không có quyền gửi lại OTP." });
            }
        }
    }
}