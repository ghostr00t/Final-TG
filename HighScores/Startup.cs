using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HighScore.Startup))]
namespace HighScore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
