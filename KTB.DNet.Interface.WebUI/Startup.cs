using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KTB.DNet.Interface.WebUI.Startup))]
namespace KTB.DNet.Interface.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
