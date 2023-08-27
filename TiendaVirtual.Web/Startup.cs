using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(TiendaVirtual.Web.Startup))]
namespace TiendaVirtual.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
