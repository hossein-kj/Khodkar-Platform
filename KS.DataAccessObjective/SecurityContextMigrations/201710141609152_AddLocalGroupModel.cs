namespace KS.DataAccess.SecurityContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocalGroupModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Security.AspNetLocalGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        Name = c.String(maxLength: 256),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 19),
                        ModifieLocalDateTime = c.String(maxLength: 19),
                        AccessLocalDateTime = c.String(maxLength: 19),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Security.AspNetGroups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Security.AspNetLocalGroup", "GroupId", "Security.AspNetGroups");
            DropIndex("Security.AspNetLocalGroup", new[] { "GroupId" });
            DropTable("Security.AspNetLocalGroup");
        }
    }
}
