using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vstore.Startup))]
namespace Vstore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
