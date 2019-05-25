using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using OnlineShop.Domain.Concrete;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineShop.WebUI.Startup))]
namespace OnlineShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }


        public void CreateRoles()
        {
            using (EFDbContext context = new EFDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                if (!roleManager.RoleExists("User"))
                {
                    var role = new IdentityRole
                    {
                        Name = "User"
                    };
                    roleManager.Create(role);
                }

                if (!roleManager.RoleExists("Administrator"))
                {
                    var role = new IdentityRole
                    {
                        Name = "Administrator"
                    };
                    roleManager.Create(role);
                }
            }
        }
    }
}