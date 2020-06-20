using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TraigoApp.Startup))]
namespace TraigoApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
