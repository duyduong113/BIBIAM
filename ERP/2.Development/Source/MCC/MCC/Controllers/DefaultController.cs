using MCC.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MCC.Controllers
{
    public class DefaultController : BaseController
    {

        [AllowAnonymous]
        public ActionResult ChangeCulture(string culture, string returnUrl)
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
            //if (!String.IsNullOrEmpty(returnUrl))
            //{
            //    return RedirectToAction("Index", returnUrl);
            //}
            //else
            //{
            //    return RedirectToAction("Index");
            //}
            return Redirect(returnUrl);

        }
        public ActionResult Index()
        {
            return View();
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
                return RedirectToAction("Register", "Account");
            }

        }
    }
}