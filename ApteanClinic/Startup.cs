using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ApteanClinic.Startup))]
namespace ApteanClinic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
