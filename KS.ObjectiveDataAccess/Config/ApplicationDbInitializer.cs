using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.ObjectiveDataAccess.Contexts;

namespace KS.ObjectiveDataAccess.Config
{
    public class ApplicationDbInitializer : NullDatabaseInitializer<SecurityContext>
    {

        //protected override void Seed(ApplicationDbContext context)
        //{
        //    InitializeIdentityForEF(context);

        //    base.Seed(context);
        //}

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEF(SecurityContext db)
        {

            //var userManager = HttpContext.Current

            //    .GetOwinContext().GetUserManager<ApplicationUserManager>();

            //var roleManager = HttpContext.Current

            //    .GetOwinContext().Get<ApplicationRoleManager>();

            //const string name = "admin@example.com";

            //const string password = "Admin@123456";

            //const string roleName = "Admin";



            ////Create Role Admin if it does not exist

            //var role = roleManager.FindByName(roleName);

            //if (role == null)
            //{

            //    role = new ApplicationRole(roleName);

            //    var roleresult = roleManager.Create(role);

            //}



            //var user = userManager.FindByName(name);

            //if (user == null)
            //{

            //    user = new ApplicationUser

            //    {

            //        UserName = name,

            //        Email = name,

            //        EmailConfirmed = true

            //    };

            //    var result = userManager.Create(user, password);

            //    result = userManager.SetLockoutEnabled(user.Id, false);

            //}



            //var groupManager = new ApplicationGroupManager();

            //var newGroup = new ApplicationGroup("SuperAdmins", "Full Access to All");



            //groupManager.CreateGroup(newGroup);

            //groupManager.SetUserGroups(user.Id, new int[] { newGroup.Id });

            //groupManager.SetGroupRoles(newGroup.Id, new int[] { role.Id });

        }
    }

}
