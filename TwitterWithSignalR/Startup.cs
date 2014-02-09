using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TwitterWithSignalR.Startup))]
namespace TwitterWithSignalR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
