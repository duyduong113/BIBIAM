using System.Web.Mvc;
using System.Web.Routing;

namespace ERPAPD
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "signin-vietin", url: "signin-vietin", defaults: new { controller = "PaymentVT", action = "Callback" });

            routes.MapRoute(name: "signin-google", url: "signin-google", defaults: new { controller = "Account", action = "ExternalLoginCallback" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
