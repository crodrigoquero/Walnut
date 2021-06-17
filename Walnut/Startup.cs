using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Walnut.Startup))]
namespace Walnut
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
