
using System.Data.Entity.Migrations;

namespace KS.DataAccess.ContentManagementDbContextMigrations
{
  


    internal sealed class Configuration : DbMigrationsConfiguration<KS.DataAccess.Contexts.ContentManagementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"ContentManagementDbContextMigrations";
        }

        protected override void Seed(KS.DataAccess.Contexts.ContentManagementContext context)
        {
            //  This method will be called after migrating to the latest version.

            //var dynamicContext = new ContentManagementDynamicContext();

            //dynamicContext.EntityGroups.AddRange(new List<EntityGroup>()
            //{
            //    new EntityGroup() {GroupId = }
            //});


            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //var service = context.Services.FirstOrDefault(sr => sr.Id == 6);
            //service.LocalValues = new Collection<MasterDataLocalKeyValue>();
            //service.LocalValues.Add(new MasterDataLocalKeyValue()
            //{
            //    MasterDataKeyValueId = 6,
            //    AccessDateTime = DateTime.Now,
            //    CreateDateTime = DateTime.Now,
            //    Language = "fa",
            //    Name = "Get All Roles",
            //    CreateUserId = 1,
            //    ModifieUserId = 1,
            //    ViewCount = 0
            //});
            //context.SaveChanges();
        }
    }
}
