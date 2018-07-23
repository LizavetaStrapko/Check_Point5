using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project5.Startup))]
namespace Project5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
