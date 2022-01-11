using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Jam3iyati.Startup))]
namespace Jam3iyati
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
