using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AaluxWeb.Startup))]
namespace AaluxWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
