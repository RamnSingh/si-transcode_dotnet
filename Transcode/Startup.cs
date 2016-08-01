using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Transcode.Startup))]
namespace Transcode
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //TranscodeModule.
        }
    }
}
