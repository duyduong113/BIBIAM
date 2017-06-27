using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCC.Controllers
{
    public class ErrorController : BaseController
    {
        //
        // GET: /Error/
        public ActionResult NoAccess()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult ErrorPage()
        {
            return View();
        }
	}
}