using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MD.Web.Startup))]
namespace MD.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
