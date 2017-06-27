using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using ERPAPD.Models;

namespace ERPAPD.Controllers
{
    public class PortalMessageController : Controller
    {
        //
        // GET: /PortalMessage/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            if (this.User.Identity.IsAuthenticated)
            {

                var data = Portal_Message.GetData(User.Identity.Name, "All");
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccess", "Error");
            }
        }
        [HttpPost]
        public ActionResult GetCountMessage()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                int count = Portal_Message.CountMessage(User.Identity.Name);
                return Json(new { data = count });
            }
            return RedirectToAction("NoAccess", "Error");
        }
        
        [HttpPost]
        public ActionResult GetMessage([DataSourceRequest]DataSourceRequest request)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var data = Portal_Message.GetData(User.Identity.Name, "Lastest");
                Portal_Message.UpdateIsRead(User.Identity.Name);
                return Json(data.ToDataSourceResult(request));
            }
            return RedirectToAction("NoAccess", "Error");
        }

        [HttpPost]
        public ActionResult UpdateClick(int id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                Portal_Message.UpdateIsClick(User.Identity.Name, id);
                return null;
            }
            return RedirectToAction("NoAccess", "Error");
        }
	}
}