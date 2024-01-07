using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Threading.Tasks;





[assembly: OwinStartup(typeof(todoproject.Startup))]
namespace todoproject
{
    
    public class Startup
    {
            public void Configuration(IAppBuilder app)
            {
                ConfigureAuth(app);

            }

        private void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
    
}
