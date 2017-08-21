using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentityMgmt.Startup))]
namespace IdentityMgmt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
