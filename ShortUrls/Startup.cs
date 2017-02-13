using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShortUrls.Startup))]
namespace ShortUrls
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
