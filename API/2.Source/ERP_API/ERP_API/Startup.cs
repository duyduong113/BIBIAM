using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ERP_API.Startup))]

namespace ERP_API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
