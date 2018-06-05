using KS.DataAccess.Contexts;
using KS.Model.Security;

namespace KS.DataAccess.SecurityContextMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    internal sealed class Configuration : DbMigrationsConfiguration<SecurityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"SecurityContextMigrations";
        }

        protected override void Seed(SecurityContext context)
        {
            //context.ApplicationLocalRoles.AddOrUpdate(new ApplicationLocalRole()
            //{
            //    AccessDateTime = DateTime.Now,
            //    CreateDateTime = DateTime.Now,
            //    ModifieDateTime = DateTime.Now,
            //    CreateUserId = 1,
            //    ModifieUserId = 1,
            //    RoleGroupId = 1,
            //    Language = "en",

            //});

            //JArray rss = JArray.Parse(File.ReadAllText(@"C:\Users\hossein\Documents\json.txt").Trim());




            //JArray level2Array = JArray.Parse(rss[4]["children"].ToString());
            //JArray level3Array = JArray.Parse(level2Array[1]["children"].ToString());

            //foreach (var menu in level3Array)
            //{

            //var tree = new ApplicationTree()
            //{
            //    Url = rss[5]["href"].ToString(),
            //    Order = Convert.ToInt32(rss[5]["order"].ToString()),
            //    Text = rss[5]["text"].ToString()
            //    //ParentId = 51
            //};
            //context.ApplicationTrees.Add(tree);

            //File.WriteAllText(@"C:\Users\hossein\Documents\temp.txt", menu["text"].ToString());
            //}

            //context.SaveChanges();


            //var userManager = new ApplicationUserManager(new ApplicationUserStore(context), new DpapiDataProtectionProvider(), new EmailService(), new SmsService(), null,new ApplicationPasswordHasher());
            //userManager.UserValidator = new UserValidator<ApplicationUser, int>(userManager);
            //userManager.PasswordValidator = new PasswordValidator();
            //var roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));

            //const string name = "admin@khodkar.com";

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



            //var groupManager = new ApplicationGroupManager(context, userManager, roleManager, new ApplicationGroupStore(context, new GroupStoreBase(context)));

            //var newGroup = new ApplicationGroup("SuperAdmins", "Full Access to All");



            //groupManager.CreateGroup(newGroup);

            //groupManager.SetUserGroups(user.Id, new int[] { newGroup.Id });

            //groupManager.SetGroupRoles(newGroup.Id, new int[] { role.Id });



            //var parent = context.ApplicationTrees.Where(ap => ap.Id == 75).SingleOrDefault();
            //var child = new ApplicationTree()
            //{
            //    Url = "test_2",
            //    Order = 1,
            //    Text = "تست 2"
            //    ,
            //    AccessDateTime = DateTime.Now
            //    ,
            //    CreateDateTime = DateTime.Now
            //    ,
            //    ModifieDateTime = DateTime.Now
            //    ,
            //    ParentId = 75
            //};
            //parent.Childrens = new Collection<ApplicationTree>() { child };

            ////context.ApplicationTrees.Add(tree);



            //context.SaveChanges();

            //parent.Offspring = new Collection<ApplicationTreeNode>()
            //{
            //    new ApplicationTreeNode()
            //    {
            //        Ancestor = parent,
            //        AncestorId = parent.Id,
            //        Offspring = child,
            //        OffspringId = child.Id

            //    }
            //};
            //context.SaveChanges();


        }
    }
}
