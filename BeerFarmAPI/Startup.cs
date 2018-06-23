using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeerFarmAPI.Startup))]
namespace BeerFarmAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
