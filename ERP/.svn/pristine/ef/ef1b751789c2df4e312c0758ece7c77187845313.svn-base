using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using BIBIAM.Core.Entities;
using BIBIAM.Core.Data.DataObject;
using Hangfire;
using MCC.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using MCC.Filters;
using BIBIAM.Core.Data;
using BIBIAM.Core;

namespace MCC.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,create")]
    public class MerchantController : CustomController
    {
        public ActionResult Register()
        {
            if (currentUser != null)
                if (!String.IsNullOrEmpty(currentUser.ma_gian_hang))
                    return RedirectToAction("Index", "Home");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]

        public ActionResult Register(Merchant_Info model)
        {
            ModelState.Clear();
            if (currentUser != null)
                if (!String.IsNullOrEmpty(currentUser.ma_gian_hang))
                    return Json(new { success = true, JsonRequestBehavior.AllowGet });

            using (var dbConn = Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
            {
                //dbConn.ChangeDatabase(AllConstant.ERPDBName);
                var data = dbConn.SingleOrDefault<Merchant_Info>("email={0}", model.email);
                if (data == null)
                {
                    List<Merchant_Info> listMerchant = new List<Merchant_Info>();
                    listMerchant.Add(model);
                    var result = new Merchant_Info_DAO().UpSert(listMerchant, currentUser.name, AppConfigs.MCCConnectionString);
                    if (result == "true")
                    {
                        data = dbConn.SingleOrDefault<Merchant_Info>("email={0}", model.email);
                        currentUser.ma_gian_hang = data.ma_gian_hang;
                        //dbConn.ChangeDatabase(AllConstant.MCCDBName);
                        dbConn.UpdateOnly(currentUser,
                                onlyFields: p =>
                                    new
                                    {
                                        p.ma_gian_hang
                                    },
                            where: p => p.id == currentUser.id);
                        return Json(new { success = true, JsonRequestBehavior.AllowGet });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Tạo gian hàng thất bại", data, JsonRequestBehavior.AllowGet });
                    }
                }
                else
                {
                    return Json(new { success = false, email = Resources.Global._email_exist, data, JsonRequestBehavior.AllowGet });
                }
            }
        }
    }
}
