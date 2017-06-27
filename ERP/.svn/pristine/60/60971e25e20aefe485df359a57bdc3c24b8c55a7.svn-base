using Hangfire.Dashboard;
using Microsoft.Owin;
using Owin;
using System.Collections.Generic;

[assembly: OwinStartupAttribute(typeof(ERPAPD.Startup))]
namespace ERPAPD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //GlobalConfiguration.Configuration.UseSqlServerStorage(ConfigurationManager.AppSettings["HangfireConnection"].ToString().Trim());
            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    AuthorizationFilters = new[] { new MyRestrictiveAuthorizationFilter() }
            //});
            //var options = new BackgroundJobServerOptions
            //{
            //    Queues = new[] { "critital", "default" }
            //};
            app.MapSignalR();
            //app.UseHangfireServer(options);
        }
    }
    public class MyRestrictiveAuthorizationFilter : IAuthorizationFilter
    {
        public bool Authorize(IDictionary<string, object> owinEnvironment)
        {
            // In case you need an OWIN context, use the next line,
            // `OwinContext` class is the part of the `Microsoft.Owin` package.
            var context = new OwinContext(owinEnvironment);
            if (context.Authentication.User.Identity.Name == "administrator")
            {
                return true;
            }

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return false;
        }
    }
}
