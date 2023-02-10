using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lesson1.Startup))]
namespace Lesson1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
