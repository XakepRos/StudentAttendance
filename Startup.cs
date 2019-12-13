using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentAttendance.Startup))]
namespace StudentAttendance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
