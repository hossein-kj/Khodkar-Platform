namespace KS.DataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstTime : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ContentManagement.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        IsLeaf = c.Boolean(nullable: false),
                        Order = c.Int(),
                        Email = c.String(maxLength: 32),
                        DynamicEntityId = c.Int(nullable: false),
                        WebPageId = c.Int(nullable: false),
                        Name = c.String(maxLength: 32),
                        Content = c.String(maxLength: 2048),
                        Public = c.Boolean(nullable: false),
                        Like = c.Int(nullable: false),
                        DisLike = c.Int(nullable: false),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        ViewCount = c.Int(nullable: false),
                        ViewRoleId = c.Int(),
                        ModifyRoleId = c.Int(),
                        AccessRoleId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.Comments", t => t.ParentId)
                .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId, cascadeDelete: true)
                .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
                .ForeignKey("ContentManagement.WebPages", t => t.WebPageId, cascadeDelete: true)
                .Index(t => t.ParentId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.WebPageId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifieUserId);
            
            CreateTable(
                "ContentManagement.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AliasName = c.String(maxLength: 256),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        ViewCount = c.Int(nullable: false),
                        ViewRoleId = c.Int(),
                        ModifyRoleId = c.Int(),
                        AccessRoleId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
                .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifieUserId);
            
            CreateTable(
                "ContentManagement.EntityMasterDataKeyValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MasterDataKeyValueId = c.Int(nullable: false),
                        LinkId = c.Int(),
                        DynamicEntityId = c.Int(),
                        FileId = c.Int(),
                        FilePathId = c.Int(),
                        UserId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        EntityTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.Files", t => t.FileId, cascadeDelete: true)
                .ForeignKey("ContentManagement.FilePaths", t => t.FilePathId, cascadeDelete: true)
                .ForeignKey("ContentManagement.Links", t => t.LinkId, cascadeDelete: true)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId, cascadeDelete: true)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.MasterDataKeyValueId, cascadeDelete: true)
                .ForeignKey("ContentManagement.Users", t => t.UserId)
                .Index(t => t.MasterDataKeyValueId)
                .Index(t => t.LinkId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.FileId)
                .Index(t => t.FilePathId)
                .Index(t => t.UserId);
            
            CreateTable(
                "ContentManagement.DynamicEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityTypeId = c.Int(),
                        TemplateUrl = c.String(maxLength: 1024),
                        CommentOff = c.Boolean(nullable: false),
                        Url = c.String(maxLength: 1024),
                        Public = c.Boolean(nullable: false),
                        Like = c.Int(nullable: false),
                        DisLike = c.Int(nullable: false),
                        ParentId = c.Int(),
                        IsLeaf = c.Boolean(nullable: false),
                        Order = c.Int(),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        ViewCount = c.Int(nullable: false),
                        ViewRoleId = c.Int(),
                        ModifyRoleId = c.Int(),
                        AccessRoleId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.ParentId)
                .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
                .Index(t => t.EntityTypeId)
                .Index(t => t.ParentId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifieUserId);
            
            CreateTable(
                "ContentManagement.BigIntProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Long(nullable: false),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.MasterDataKeyValues",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ParentId = c.Int(),
                        Name = c.String(maxLength: 255),
                        Code = c.String(maxLength: 512),
                        Url = c.String(maxLength: 1024),
                        IsLeaf = c.Boolean(nullable: false),
                        Order = c.Int(),
                        EnableCache = c.Boolean(nullable: false),
                        SlidingExpirationTimeInMinutes = c.Int(nullable: false),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        ViewCount = c.Int(nullable: false),
                        ViewRoleId = c.Int(),
                        ModifyRoleId = c.Int(),
                        AccessRoleId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        TypeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.ParentId)
                .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
                .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
                .Index(t => t.ParentId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifieUserId);
            
            CreateTable(
                "ContentManagement.EntityGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        DynamicEntityTypeId = c.Int(),
                        LinkId = c.Int(),
                        CommentId = c.Int(),
                        DynamicEntityId = c.Int(),
                        MasterDataKeyValueId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        EntityTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.Links", t => t.LinkId, cascadeDelete: true)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.Comments", t => t.CommentId, cascadeDelete: true)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.DynamicEntityTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.MasterDataKeyValueId)
                .Index(t => t.GroupId)
                .Index(t => t.DynamicEntityTypeId)
                .Index(t => t.LinkId)
                .Index(t => t.CommentId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.MasterDataKeyValueId);
            
            CreateTable(
                "ContentManagement.MasterDataLocalKeyValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MasterDataKeyValueId = c.Int(nullable: false),
                        Name = c.String(maxLength: 255),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.MasterDataKeyValueId, cascadeDelete: true)
                .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
                .Index(t => t.MasterDataKeyValueId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifieUserId);
            
            CreateTable(
                "ContentManagement.Links",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(maxLength: 255),
                        Html = c.String(maxLength: 512),
                        Action = c.String(),
                        TypeId = c.Int(nullable: false),
                        IconPath = c.String(maxLength: 255),
                        IsLeaf = c.Boolean(nullable: false),
                        TransactionCode = c.String(maxLength: 4),
                        Url = c.String(maxLength: 1024),
                        Order = c.Int(),
                        ParentId = c.Int(),
                        ShowToSearchEngine = c.Boolean(nullable: false),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        ViewCount = c.Int(nullable: false),
                        ViewRoleId = c.Int(),
                        ModifyRoleId = c.Int(),
                        AccessRoleId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.Links", t => t.ParentId)
                .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
                .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
                .Index(t => t.ParentId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifieUserId);
            
            CreateTable(
                "ContentManagement.EntityFilePaths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilePathId = c.Int(nullable: false),
                        EntityId = c.Int(nullable: false),
                        IsDefaults = c.Boolean(nullable: false),
                        DynamicEntityTypeId = c.Int(),
                        LinkId = c.Int(),
                        WebPageId = c.Int(),
                        DynamicEntityId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        EntityTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.WebPages", t => t.WebPageId, cascadeDelete: true)
                .ForeignKey("ContentManagement.Links", t => t.LinkId, cascadeDelete: true)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId, cascadeDelete: true)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.DynamicEntityTypeId)
                .ForeignKey("ContentManagement.FilePaths", t => t.FilePathId, cascadeDelete: true)
                .Index(t => t.FilePathId)
                .Index(t => t.DynamicEntityTypeId)
                .Index(t => t.LinkId)
                .Index(t => t.WebPageId)
                .Index(t => t.DynamicEntityId);
            
            CreateTable(
                "ContentManagement.FilePaths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        Description = c.String(maxLength: 128),
                        Type = c.Int(nullable: false),
                        Url = c.String(maxLength: 1024),
                        ThumbnailId = c.Int(),
                        Public = c.Boolean(nullable: false),
                        Guid = c.Guid(nullable: false),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        ViewCount = c.Int(nullable: false),
                        ViewRoleId = c.Int(),
                        ModifyRoleId = c.Int(),
                        AccessRoleId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
                .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
                .ForeignKey("ContentManagement.FilePaths", t => t.ThumbnailId)
                .Index(t => t.ThumbnailId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifieUserId);
            
            CreateTable(
                "ContentManagement.LocalFilePaths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilePathId = c.Int(nullable: false),
                        Name = c.String(maxLength: 255),
                        Description = c.String(maxLength: 128),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
                .ForeignKey("ContentManagement.FilePaths", t => t.FilePathId, cascadeDelete: true)
                .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
                .Index(t => t.FilePathId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifieUserId);
            
            CreateTable(
                "ContentManagement.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Description = c.String(maxLength: 128),
                        Content = c.Binary(),
                        Type = c.Int(nullable: false),
                        Url = c.String(maxLength: 1024),
                        ThumbnailId = c.Int(),
                        Public = c.Boolean(nullable: false),
                        Guid = c.Guid(nullable: false),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        ViewCount = c.Int(nullable: false),
                        ViewRoleId = c.Int(),
                        ModifyRoleId = c.Int(),
                        AccessRoleId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
                .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
                .ForeignKey("ContentManagement.FilePaths", t => t.ThumbnailId)
                .Index(t => t.ThumbnailId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifieUserId);
            
            CreateTable(
                "ContentManagement.LocalFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileId = c.Int(nullable: false),
                        Name = c.String(maxLength: 255),
                        Description = c.String(maxLength: 128),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
                .ForeignKey("ContentManagement.Files", t => t.FileId, cascadeDelete: true)
                .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
                .Index(t => t.FileId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifieUserId);
            
            CreateTable(
                "ContentManagement.WebPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Url = c.String(maxLength: 1024),
                        CategoryId = c.Int(),
                        TemplatePatternUrl = c.String(maxLength: 1024),
                        FrameWorkId = c.Int(),
                        CommentOff = c.Boolean(nullable: false),
                        Html = c.String(),
                        JavaScript = c.String(),
                        Style = c.String(),
                        Tools = c.String(maxLength: 512),
                        Services = c.String(),
                        EditMode = c.Boolean(nullable: false),
                        EnableCache = c.Boolean(nullable: false),
                        IsMobileVersion = c.Boolean(nullable: false),
                        TypeId = c.Int(nullable: false),
                        Like = c.Int(nullable: false),
                        DisLike = c.Int(nullable: false),
                        CacheSlidingExpirationTimeInMinutes = c.Int(nullable: false),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        ViewCount = c.Int(nullable: false),
                        ViewRoleId = c.Int(),
                        ModifyRoleId = c.Int(),
                        AccessRoleId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
                .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifieUserId);
            
            CreateTable(
                "ContentManagement.EntityFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileId = c.Int(nullable: false),
                        EntityId = c.Int(nullable: false),
                        IsDefaults = c.Boolean(nullable: false),
                        DynamicEntityTypeId = c.Int(),
                        LinkId = c.Int(),
                        WebPageId = c.Int(),
                        DynamicEntityId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        EntityTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.WebPages", t => t.WebPageId, cascadeDelete: true)
                .ForeignKey("ContentManagement.Links", t => t.LinkId, cascadeDelete: true)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId, cascadeDelete: true)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.DynamicEntityTypeId)
                .ForeignKey("ContentManagement.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.FileId)
                .Index(t => t.DynamicEntityTypeId)
                .Index(t => t.LinkId)
                .Index(t => t.WebPageId)
                .Index(t => t.DynamicEntityId);
            
            CreateTable(
                "ContentManagement.BitProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Boolean(nullable: false),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.DateTimeProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.DateTime(nullable: false),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.EntityDynamicEntites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MasterEntityTypeId = c.Int(nullable: false),
                        DetailEntityTypeId = c.Int(nullable: false),
                        MasterEntityId = c.Int(nullable: false),
                        DetailEntityId = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DetailEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.DetailEntityTypeId)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.MasterEntityId, cascadeDelete: true)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.MasterEntityTypeId, cascadeDelete: true)
                .Index(t => t.MasterEntityTypeId)
                .Index(t => t.DetailEntityTypeId)
                .Index(t => t.MasterEntityId)
                .Index(t => t.DetailEntityId);
            
            CreateTable(
                "ContentManagement.DynamicEntityProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubDynamicEntityId = c.Int(nullable: false),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.SubDynamicEntityId, cascadeDelete: true)
                .Index(t => t.SubDynamicEntityId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.FloatProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.GuidProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Guid(nullable: false),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.IntProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.Nvarchar1024Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 8),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.Nvarchar128Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 8),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.Nvarchar16Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 8),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.Nvarchar1Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 1),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.Nvarchar256Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 8),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.Nvarchar2Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 2),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.Nvarchar32Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 8),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.Nvarchar4Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 4),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.Nvarchar512Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 8),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.Nvarchar64Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 8),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.Nvarchar8Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 8),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.NvarcharMaxProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.TinyIntProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Byte(nullable: false),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.DynamicEntityId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.DynamicEntityId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.UserProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        EntityPropertyTypeId = c.Int(),
                        DynamicEntityId = c.Int(),
                        EntityTypeId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.DynamicEntities", t => t.UserId, cascadeDelete: true)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityPropertyTypeId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.EntityTypeId)
                .ForeignKey("ContentManagement.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.EntityPropertyTypeId)
                .Index(t => t.EntityTypeId);
            
            CreateTable(
                "ContentManagement.LanguageAndCultures",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Culture = c.String(maxLength: 8),
                        Country = c.String(),
                        IsRightToLeft = c.Boolean(nullable: false),
                        IsDefaults = c.Boolean(nullable: false),
                        FlagId = c.Int(),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        ViewCount = c.Int(nullable: false),
                        ViewRoleId = c.Int(),
                        ModifyRoleId = c.Int(),
                        AccessRoleId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
                .ForeignKey("ContentManagement.FilePaths", t => t.FlagId)
                .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
                .Index(t => t.FlagId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifieUserId);
            
            CreateTable(
                "ContentManagement.Codes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        TypeId = c.Int(nullable: false),
                        Value = c.String(),
                        Language = c.String(maxLength: 8),
                        CreateUserId = c.Int(),
                        ModifieUserId = c.Int(),
                        ViewCount = c.Int(nullable: false),
                        ViewRoleId = c.Int(),
                        ModifyRoleId = c.Int(),
                        AccessRoleId = c.Int(),
                        CreateLocalDateTime = c.String(maxLength: 22),
                        ModifieLocalDateTime = c.String(maxLength: 22),
                        AccessLocalDateTime = c.String(maxLength: 22),
                        CreateDateTime = c.DateTime(nullable: false),
                        ModifieDateTime = c.DateTime(nullable: false),
                        AccessDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
                .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
                .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.TypeId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifieUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ContentManagement.Codes", "TypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Codes", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Codes", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityGroups", "MasterDataKeyValueId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.EntityGroups", "DynamicEntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.EntityFiles", "FileId", "ContentManagement.Files");
            DropForeignKey("ContentManagement.EntityFiles", "DynamicEntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.EntityFilePaths", "FilePathId", "ContentManagement.FilePaths");
            DropForeignKey("ContentManagement.EntityFilePaths", "DynamicEntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.LanguageAndCultures", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LanguageAndCultures", "FlagId", "ContentManagement.FilePaths");
            DropForeignKey("ContentManagement.LanguageAndCultures", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Comments", "WebPageId", "ContentManagement.WebPages");
            DropForeignKey("ContentManagement.Comments", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityGroups", "CommentId", "ContentManagement.Comments");
            DropForeignKey("ContentManagement.Comments", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.Comments", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Users", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityMasterDataKeyValues", "UserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityMasterDataKeyValues", "MasterDataKeyValueId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.UserProperties", "UserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.UserProperties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.UserProperties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.UserProperties", "UserId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.TinyIntProperties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.TinyIntProperties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.TinyIntProperties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.EntityMasterDataKeyValues", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.NvarcharMaxProperties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.NvarcharMaxProperties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.NvarcharMaxProperties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.Nvarchar8Properties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar8Properties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar8Properties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.Nvarchar64Properties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar64Properties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar64Properties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.Nvarchar512Properties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar512Properties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar512Properties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.Nvarchar4Properties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar4Properties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar4Properties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.Nvarchar32Properties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar32Properties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar32Properties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.Nvarchar2Properties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar2Properties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar2Properties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.Nvarchar256Properties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar256Properties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar256Properties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.Nvarchar1Properties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar1Properties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar1Properties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.Nvarchar16Properties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar16Properties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar16Properties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.Nvarchar128Properties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar128Properties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar128Properties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.Nvarchar1024Properties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar1024Properties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Nvarchar1024Properties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.DynamicEntities", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.IntProperties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.IntProperties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.IntProperties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.GuidProperties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.GuidProperties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.GuidProperties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.EntityGroups", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.FloatProperties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.FloatProperties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.FloatProperties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.EntityFiles", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.EntityFilePaths", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.DynamicEntities", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.DynamicEntityProperties", "SubDynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.DynamicEntityProperties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.DynamicEntityProperties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.DynamicEntityProperties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.EntityDynamicEntites", "MasterEntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.EntityDynamicEntites", "MasterEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.EntityDynamicEntites", "DetailEntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.EntityDynamicEntites", "DetailEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.DateTimeProperties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.DateTimeProperties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.DateTimeProperties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.DynamicEntities", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.DynamicEntities", "ParentId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.BitProperties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.BitProperties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.BitProperties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.BigIntProperties", "EntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.BigIntProperties", "EntityPropertyTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.MasterDataKeyValues", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityGroups", "GroupId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.EntityMasterDataKeyValues", "LinkId", "ContentManagement.Links");
            DropForeignKey("ContentManagement.Links", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityGroups", "LinkId", "ContentManagement.Links");
            DropForeignKey("ContentManagement.EntityFiles", "LinkId", "ContentManagement.Links");
            DropForeignKey("ContentManagement.EntityFilePaths", "LinkId", "ContentManagement.Links");
            DropForeignKey("ContentManagement.WebPages", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityFiles", "WebPageId", "ContentManagement.WebPages");
            DropForeignKey("ContentManagement.EntityFilePaths", "WebPageId", "ContentManagement.WebPages");
            DropForeignKey("ContentManagement.WebPages", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.FilePaths", "ThumbnailId", "ContentManagement.FilePaths");
            DropForeignKey("ContentManagement.EntityMasterDataKeyValues", "FilePathId", "ContentManagement.FilePaths");
            DropForeignKey("ContentManagement.Files", "ThumbnailId", "ContentManagement.FilePaths");
            DropForeignKey("ContentManagement.EntityMasterDataKeyValues", "FileId", "ContentManagement.Files");
            DropForeignKey("ContentManagement.Files", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFiles", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFiles", "FileId", "ContentManagement.Files");
            DropForeignKey("ContentManagement.LocalFiles", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Files", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.FilePaths", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFilePaths", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFilePaths", "FilePathId", "ContentManagement.FilePaths");
            DropForeignKey("ContentManagement.LocalFilePaths", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.FilePaths", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Links", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Links", "ParentId", "ContentManagement.Links");
            DropForeignKey("ContentManagement.MasterDataLocalKeyValues", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.MasterDataLocalKeyValues", "MasterDataKeyValueId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.MasterDataLocalKeyValues", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.MasterDataKeyValues", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.MasterDataKeyValues", "ParentId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.BigIntProperties", "DynamicEntityId", "ContentManagement.DynamicEntities");
            DropForeignKey("ContentManagement.Users", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Comments", "ParentId", "ContentManagement.Comments");
            DropIndex("ContentManagement.Codes", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.Codes", new[] { "CreateUserId" });
            DropIndex("ContentManagement.Codes", new[] { "TypeId" });
            DropIndex("ContentManagement.LanguageAndCultures", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.LanguageAndCultures", new[] { "CreateUserId" });
            DropIndex("ContentManagement.LanguageAndCultures", new[] { "FlagId" });
            DropIndex("ContentManagement.UserProperties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.UserProperties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.UserProperties", new[] { "UserId" });
            DropIndex("ContentManagement.TinyIntProperties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.TinyIntProperties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.TinyIntProperties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.NvarcharMaxProperties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.NvarcharMaxProperties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.NvarcharMaxProperties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.Nvarchar8Properties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.Nvarchar8Properties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.Nvarchar8Properties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.Nvarchar64Properties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.Nvarchar64Properties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.Nvarchar64Properties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.Nvarchar512Properties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.Nvarchar512Properties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.Nvarchar512Properties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.Nvarchar4Properties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.Nvarchar4Properties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.Nvarchar4Properties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.Nvarchar32Properties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.Nvarchar32Properties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.Nvarchar32Properties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.Nvarchar2Properties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.Nvarchar2Properties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.Nvarchar2Properties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.Nvarchar256Properties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.Nvarchar256Properties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.Nvarchar256Properties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.Nvarchar1Properties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.Nvarchar1Properties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.Nvarchar1Properties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.Nvarchar16Properties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.Nvarchar16Properties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.Nvarchar16Properties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.Nvarchar128Properties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.Nvarchar128Properties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.Nvarchar128Properties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.Nvarchar1024Properties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.Nvarchar1024Properties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.Nvarchar1024Properties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.IntProperties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.IntProperties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.IntProperties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.GuidProperties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.GuidProperties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.GuidProperties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.FloatProperties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.FloatProperties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.FloatProperties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.DynamicEntityProperties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.DynamicEntityProperties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.DynamicEntityProperties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.DynamicEntityProperties", new[] { "SubDynamicEntityId" });
            DropIndex("ContentManagement.EntityDynamicEntites", new[] { "DetailEntityId" });
            DropIndex("ContentManagement.EntityDynamicEntites", new[] { "MasterEntityId" });
            DropIndex("ContentManagement.EntityDynamicEntites", new[] { "DetailEntityTypeId" });
            DropIndex("ContentManagement.EntityDynamicEntites", new[] { "MasterEntityTypeId" });
            DropIndex("ContentManagement.DateTimeProperties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.DateTimeProperties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.DateTimeProperties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.BitProperties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.BitProperties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.BitProperties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.EntityFiles", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.EntityFiles", new[] { "WebPageId" });
            DropIndex("ContentManagement.EntityFiles", new[] { "LinkId" });
            DropIndex("ContentManagement.EntityFiles", new[] { "DynamicEntityTypeId" });
            DropIndex("ContentManagement.EntityFiles", new[] { "FileId" });
            DropIndex("ContentManagement.WebPages", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.WebPages", new[] { "CreateUserId" });
            DropIndex("ContentManagement.LocalFiles", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.LocalFiles", new[] { "CreateUserId" });
            DropIndex("ContentManagement.LocalFiles", new[] { "FileId" });
            DropIndex("ContentManagement.Files", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.Files", new[] { "CreateUserId" });
            DropIndex("ContentManagement.Files", new[] { "ThumbnailId" });
            DropIndex("ContentManagement.LocalFilePaths", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.LocalFilePaths", new[] { "CreateUserId" });
            DropIndex("ContentManagement.LocalFilePaths", new[] { "FilePathId" });
            DropIndex("ContentManagement.FilePaths", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.FilePaths", new[] { "CreateUserId" });
            DropIndex("ContentManagement.FilePaths", new[] { "ThumbnailId" });
            DropIndex("ContentManagement.EntityFilePaths", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.EntityFilePaths", new[] { "WebPageId" });
            DropIndex("ContentManagement.EntityFilePaths", new[] { "LinkId" });
            DropIndex("ContentManagement.EntityFilePaths", new[] { "DynamicEntityTypeId" });
            DropIndex("ContentManagement.EntityFilePaths", new[] { "FilePathId" });
            DropIndex("ContentManagement.Links", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.Links", new[] { "CreateUserId" });
            DropIndex("ContentManagement.Links", new[] { "ParentId" });
            DropIndex("ContentManagement.MasterDataLocalKeyValues", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.MasterDataLocalKeyValues", new[] { "CreateUserId" });
            DropIndex("ContentManagement.MasterDataLocalKeyValues", new[] { "MasterDataKeyValueId" });
            DropIndex("ContentManagement.EntityGroups", new[] { "MasterDataKeyValueId" });
            DropIndex("ContentManagement.EntityGroups", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.EntityGroups", new[] { "CommentId" });
            DropIndex("ContentManagement.EntityGroups", new[] { "LinkId" });
            DropIndex("ContentManagement.EntityGroups", new[] { "DynamicEntityTypeId" });
            DropIndex("ContentManagement.EntityGroups", new[] { "GroupId" });
            DropIndex("ContentManagement.MasterDataKeyValues", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.MasterDataKeyValues", new[] { "CreateUserId" });
            DropIndex("ContentManagement.MasterDataKeyValues", new[] { "ParentId" });
            DropIndex("ContentManagement.BigIntProperties", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.BigIntProperties", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.BigIntProperties", new[] { "EntityPropertyTypeId" });
            DropIndex("ContentManagement.DynamicEntities", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.DynamicEntities", new[] { "CreateUserId" });
            DropIndex("ContentManagement.DynamicEntities", new[] { "ParentId" });
            DropIndex("ContentManagement.DynamicEntities", new[] { "EntityTypeId" });
            DropIndex("ContentManagement.EntityMasterDataKeyValues", new[] { "UserId" });
            DropIndex("ContentManagement.EntityMasterDataKeyValues", new[] { "FilePathId" });
            DropIndex("ContentManagement.EntityMasterDataKeyValues", new[] { "FileId" });
            DropIndex("ContentManagement.EntityMasterDataKeyValues", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.EntityMasterDataKeyValues", new[] { "LinkId" });
            DropIndex("ContentManagement.EntityMasterDataKeyValues", new[] { "MasterDataKeyValueId" });
            DropIndex("ContentManagement.Users", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.Users", new[] { "CreateUserId" });
            DropIndex("ContentManagement.Comments", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.Comments", new[] { "CreateUserId" });
            DropIndex("ContentManagement.Comments", new[] { "WebPageId" });
            DropIndex("ContentManagement.Comments", new[] { "DynamicEntityId" });
            DropIndex("ContentManagement.Comments", new[] { "ParentId" });
            DropTable("ContentManagement.Codes");
            DropTable("ContentManagement.LanguageAndCultures");
            DropTable("ContentManagement.UserProperties");
            DropTable("ContentManagement.TinyIntProperties");
            DropTable("ContentManagement.NvarcharMaxProperties");
            DropTable("ContentManagement.Nvarchar8Properties");
            DropTable("ContentManagement.Nvarchar64Properties");
            DropTable("ContentManagement.Nvarchar512Properties");
            DropTable("ContentManagement.Nvarchar4Properties");
            DropTable("ContentManagement.Nvarchar32Properties");
            DropTable("ContentManagement.Nvarchar2Properties");
            DropTable("ContentManagement.Nvarchar256Properties");
            DropTable("ContentManagement.Nvarchar1Properties");
            DropTable("ContentManagement.Nvarchar16Properties");
            DropTable("ContentManagement.Nvarchar128Properties");
            DropTable("ContentManagement.Nvarchar1024Properties");
            DropTable("ContentManagement.IntProperties");
            DropTable("ContentManagement.GuidProperties");
            DropTable("ContentManagement.FloatProperties");
            DropTable("ContentManagement.DynamicEntityProperties");
            DropTable("ContentManagement.EntityDynamicEntites");
            DropTable("ContentManagement.DateTimeProperties");
            DropTable("ContentManagement.BitProperties");
            DropTable("ContentManagement.EntityFiles");
            DropTable("ContentManagement.WebPages");
            DropTable("ContentManagement.LocalFiles");
            DropTable("ContentManagement.Files");
            DropTable("ContentManagement.LocalFilePaths");
            DropTable("ContentManagement.FilePaths");
            DropTable("ContentManagement.EntityFilePaths");
            DropTable("ContentManagement.Links");
            DropTable("ContentManagement.MasterDataLocalKeyValues");
            DropTable("ContentManagement.EntityGroups");
            DropTable("ContentManagement.MasterDataKeyValues");
            DropTable("ContentManagement.BigIntProperties");
            DropTable("ContentManagement.DynamicEntities");
            DropTable("ContentManagement.EntityMasterDataKeyValues");
            DropTable("ContentManagement.Users");
            DropTable("ContentManagement.Comments");
        }
    }
}
