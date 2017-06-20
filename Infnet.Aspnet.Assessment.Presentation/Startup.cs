using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Infnet.Aspnet.Assessment.Presentation.Startup))]
namespace Infnet.Aspnet.Assessment.Presentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
