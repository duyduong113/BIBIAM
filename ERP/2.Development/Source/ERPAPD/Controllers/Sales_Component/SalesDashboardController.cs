using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DecaPay.Controllers.Sales_Component
{
    [Authorize]
    [RequireHttps]
    public class SalesDashboardController : CustomController
    {
        //
        // GET: /SalesDashboard/
        public ActionResult Index()
        {
            if (asset.View)
            {
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }
    }
}