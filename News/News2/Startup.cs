using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(News2.Startup))]
namespace News2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
