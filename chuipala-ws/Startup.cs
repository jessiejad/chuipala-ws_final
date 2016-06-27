using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(chuipala_ws.Startup))]
namespace chuipala_ws
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
