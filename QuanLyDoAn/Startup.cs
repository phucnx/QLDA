using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuanLyDoAn.Startup))]
namespace QuanLyDoAn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
