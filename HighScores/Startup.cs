using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HighScores.Startup))]
namespace HighScores
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
