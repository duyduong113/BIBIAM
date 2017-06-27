using System.Web.Mvc;

namespace ERPAPD.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NoAccessRights()
        { return View(); }
        public ActionResult PageNotFound()
        {
            return View();
        }
    }
}