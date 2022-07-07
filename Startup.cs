using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ServiciosPediG.Models;

[assembly: OwinStartupAttribute(typeof(ServiciosPediG.Startup))]
namespace ServiciosPediG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            PediGModelContainer bd = new PediGModelContainer();
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.UserName = "gmoraga820@gmail.com";
                user.Email = "gmoraga820@gmail.com";

                string userPWD = "12345678";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                    User U = new User();
                    U.Nombre = "Abigail Moraga";
                    U.NombreUsuario = "gmoraga820@gmail.com";
                    U.Email = "gmoraga820@gmail.com";
                    U.Contrasenia = userPWD;
                    bd.Users.Add(U);
                    bd.SaveChanges();
                }
            }
            // creating Creating Employee role     
            if (!roleManager.RoleExists("User"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.UserName = "yefre.castellon@gmail.com";
                user.Email = "yefre.castellon@gmail.com";

                string userPWD = "12345678";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role User    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "User");

                    User U = new User();
                    U.Nombre = "Yefre Castellon";
                    U.NombreUsuario = "yefre.castellon@gmail.com";
                    U.Email = "yefre.castellon@gmail.com";
                    U.Contrasenia = userPWD;
                    bd.Users.Add(U);
                    bd.SaveChanges();
                }
            }

        }
    }
}