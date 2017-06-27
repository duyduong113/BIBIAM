using CMS.Filters;
using CMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using System.Data;
using CMS.Models;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace CMS.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all")]
    public class HomeController : CustomController
    {
        public ActionResult Index()
       {
           //if (currentUser != null)
           //    if (String.IsNullOrEmpty(currentUser.ma_gian_hang))
           //        return RedirectToAction("Register", "Merchant");
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                data = KendoApplyFilter.KendoData<Article>(request, "active = 1");
                return Json(data);
            }
        }

        [AllowAnonymous]
        public ActionResult SetCulture(string culture, string returnUrl)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", returnUrl);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
    }
}