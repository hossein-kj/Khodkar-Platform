namespace KS.ObjectiveDataAccess.ContentManagementDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstTime : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "ContentManagement.Comments",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            ParentId = c.Int(),
            //            IsLeaf = c.Boolean(nullable: false),
            //            Order = c.Int(),
            //            Email = c.String(maxLength: 32),
            //            WebPageId = c.Int(nullable: false),
            //            Name = c.String(maxLength: 32),
            //            Content = c.String(maxLength: 2048),
            //            Public = c.Boolean(nullable: false),
            //            Like = c.Int(nullable: false),
            //            DisLike = c.Int(nullable: false),
            //            Language = c.String(maxLength: 8),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            ViewCount = c.Int(nullable: false),
            //            ViewRoleId = c.Int(),
            //            ModifyRoleId = c.Int(),
            //            AccessRoleId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Status = c.Int(nullable: false),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("ContentManagement.Comments", t => t.ParentId)
            //    .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
            //    .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
            //    .ForeignKey("ContentManagement.WebPages", t => t.WebPageId, cascadeDelete: true)
            //    .Index(t => t.ParentId)
            //    .Index(t => t.WebPageId)
            //    .Index(t => t.CreateUserId)
            //    .Index(t => t.ModifieUserId);
            
            //CreateTable(
            //    "ContentManagement.Users",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            AliasName = c.String(maxLength: 256),
            //            Language = c.String(maxLength: 8),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            ViewCount = c.Int(nullable: false),
            //            ViewRoleId = c.Int(),
            //            ModifyRoleId = c.Int(),
            //            AccessRoleId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Status = c.Int(nullable: false),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
            //    .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
            //    .Index(t => t.CreateUserId)
            //    .Index(t => t.ModifieUserId);
            
            //CreateTable(
            //    "ContentManagement.EntityMasterDataKeyValues",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            MasterDataKeyValueId = c.Int(nullable: false),
            //            LinkId = c.Int(),
            //            FileId = c.Int(),
            //            FilePathId = c.Int(),
            //            UserId = c.Int(),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //            EntityTypeId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("ContentManagement.Links", t => t.LinkId, cascadeDelete: true)
            //    .ForeignKey("ContentManagement.FilePaths", t => t.FilePathId, cascadeDelete: true)
            //    .ForeignKey("ContentManagement.Files", t => t.FileId, cascadeDelete: true)
            //    .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.MasterDataKeyValueId, cascadeDelete: true)
            //    .ForeignKey("ContentManagement.Users", t => t.UserId)
            //    .Index(t => t.MasterDataKeyValueId)
            //    .Index(t => t.LinkId)
            //    .Index(t => t.FileId)
            //    .Index(t => t.FilePathId)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "ContentManagement.Files",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(maxLength: 255),
            //            ContentType = c.String(maxLength: 100),
            //            Description = c.String(maxLength: 128),
            //            Content = c.Binary(),
            //            TypeCode = c.Int(nullable: false),
            //            Size = c.Single(nullable: false),
            //            Url = c.String(maxLength: 1024),
            //            Guid = c.Guid(nullable: false),
            //            Language = c.String(maxLength: 8),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            ViewCount = c.Int(nullable: false),
            //            ViewRoleId = c.Int(),
            //            ModifyRoleId = c.Int(),
            //            AccessRoleId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Status = c.Int(nullable: false),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
            //    .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
            //    .Index(t => t.CreateUserId)
            //    .Index(t => t.ModifieUserId);
            
            //CreateTable(
            //    "ContentManagement.LocalFiles",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FileId = c.Int(nullable: false),
            //            Name = c.String(maxLength: 255),
            //            Description = c.String(maxLength: 128),
            //            Language = c.String(maxLength: 8),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Status = c.Int(nullable: false),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
            //    .ForeignKey("ContentManagement.Files", t => t.FileId, cascadeDelete: true)
            //    .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
            //    .Index(t => t.FileId)
            //    .Index(t => t.CreateUserId)
            //    .Index(t => t.ModifieUserId);
            
            //CreateTable(
            //    "ContentManagement.FilePaths",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(maxLength: 128),
            //            Description = c.String(maxLength: 128),
            //            TypeCode = c.Int(nullable: false),
            //            Url = c.String(maxLength: 1024),
            //            Size = c.Single(nullable: false),
            //            Guid = c.Guid(nullable: false),
            //            Language = c.String(maxLength: 8),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            ViewCount = c.Int(nullable: false),
            //            ViewRoleId = c.Int(),
            //            ModifyRoleId = c.Int(),
            //            AccessRoleId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Status = c.Int(nullable: false),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
            //    .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
            //    .Index(t => t.CreateUserId)
            //    .Index(t => t.ModifieUserId);
            
            //CreateTable(
            //    "ContentManagement.LocalFilePaths",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FilePathId = c.Int(nullable: false),
            //            Name = c.String(maxLength: 255),
            //            Description = c.String(maxLength: 128),
            //            Language = c.String(maxLength: 8),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Status = c.Int(nullable: false),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
            //    .ForeignKey("ContentManagement.FilePaths", t => t.FilePathId, cascadeDelete: true)
            //    .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
            //    .Index(t => t.FilePathId)
            //    .Index(t => t.CreateUserId)
            //    .Index(t => t.ModifieUserId);
            
            //CreateTable(
            //    "ContentManagement.Links",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Text = c.String(maxLength: 255),
            //            Html = c.String(maxLength: 512),
            //            Action = c.String(maxLength: 255),
            //            TypeId = c.Int(nullable: false),
            //            IconPath = c.String(maxLength: 255),
            //            IsLeaf = c.Boolean(nullable: false),
            //            TransactionCode = c.String(maxLength: 4),
            //            Url = c.String(maxLength: 1024),
            //            IsMobile = c.Boolean(nullable: false),
            //            Order = c.Int(),
            //            ParentId = c.Int(),
            //            ShowToSearchEngine = c.Boolean(nullable: false),
            //            Language = c.String(maxLength: 8),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            ViewCount = c.Int(nullable: false),
            //            ViewRoleId = c.Int(),
            //            ModifyRoleId = c.Int(),
            //            AccessRoleId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Status = c.Int(nullable: false),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("ContentManagement.Links", t => t.ParentId)
            //    .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
            //    .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
            //    .Index(t => t.ParentId)
            //    .Index(t => t.CreateUserId)
            //    .Index(t => t.ModifieUserId);
            
            //CreateTable(
            //    "ContentManagement.EntityFilePaths",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FilePathId = c.Int(nullable: false),
            //            EntityId = c.Int(nullable: false),
            //            IsDefaults = c.Boolean(nullable: false),
            //            DynamicEntityTypeId = c.Int(),
            //            LinkId = c.Int(),
            //            WebPageId = c.Int(),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //            EntityTypeId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("ContentManagement.WebPages", t => t.WebPageId, cascadeDelete: true)
            //    .ForeignKey("ContentManagement.Links", t => t.LinkId, cascadeDelete: true)
            //    .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.DynamicEntityTypeId)
            //    .ForeignKey("ContentManagement.FilePaths", t => t.FilePathId, cascadeDelete: true)
            //    .Index(t => t.FilePathId)
            //    .Index(t => t.DynamicEntityTypeId)
            //    .Index(t => t.LinkId)
            //    .Index(t => t.WebPageId);
            
            //CreateTable(
            //    "ContentManagement.MasterDataKeyValues",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false),
            //            ParentId = c.Int(),
            //            Name = c.String(maxLength: 255),
            //            Code = c.String(maxLength: 512),
            //            SecondCode = c.String(maxLength: 512),
            //            Data = c.String(maxLength: 512),
            //            Key = c.Int(),
            //            Value = c.Int(),
            //            ParentTypeId = c.Int(),
            //            PathOrUrl = c.String(maxLength: 1024),
            //            SecondPathOrUrl = c.String(maxLength: 1024),
            //            IsLeaf = c.Boolean(nullable: false),
            //            IsType = c.Boolean(nullable: false),
            //            Order = c.Int(),
            //            ForeignKey1 = c.Int(),
            //            ForeignKey2 = c.Int(),
            //            ForeignKey3 = c.Int(),
            //            EnableCache = c.Boolean(nullable: false),
            //            SlidingExpirationTimeInMinutes = c.Int(nullable: false),
            //            Description = c.String(maxLength: 256),
            //            Guid = c.String(maxLength: 32),
            //            Version = c.Int(nullable: false),
            //            EditMode = c.Boolean(nullable: false),
            //            Language = c.String(maxLength: 8),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            ViewCount = c.Int(nullable: false),
            //            ViewRoleId = c.Int(),
            //            ModifyRoleId = c.Int(),
            //            AccessRoleId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Status = c.Int(nullable: false),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //            TypeId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.ParentId)
            //    .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
            //    .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
            //    .Index(t => t.ParentId)
            //    .Index(t => t.Guid, unique: true)
            //    .Index(t => t.CreateUserId)
            //    .Index(t => t.ModifieUserId);
            
            //CreateTable(
            //    "ContentManagement.EntityGroups",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            GroupId = c.Int(nullable: false),
            //            DynamicEntityTypeId = c.Int(),
            //            LinkId = c.Int(),
            //            CommentId = c.Int(),
            //            DynamicEntityId = c.Int(),
            //            MasterDataKeyValueId = c.Int(),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //            EntityTypeId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.GroupId, cascadeDelete: true)
            //    .ForeignKey("ContentManagement.Links", t => t.LinkId, cascadeDelete: true)
            //    .ForeignKey("ContentManagement.Comments", t => t.CommentId, cascadeDelete: true)
            //    .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.DynamicEntityTypeId)
            //    .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.MasterDataKeyValueId)
            //    .Index(t => t.GroupId)
            //    .Index(t => t.DynamicEntityTypeId)
            //    .Index(t => t.LinkId)
            //    .Index(t => t.CommentId)
            //    .Index(t => t.MasterDataKeyValueId);
            
            //CreateTable(
            //    "ContentManagement.MasterDataLocalKeyValues",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            MasterDataKeyValueId = c.Int(nullable: false),
            //            Name = c.String(maxLength: 255),
            //            Description = c.String(maxLength: 256),
            //            Language = c.String(maxLength: 8),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Status = c.Int(nullable: false),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
            //    .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.MasterDataKeyValueId, cascadeDelete: true)
            //    .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
            //    .Index(t => t.MasterDataKeyValueId)
            //    .Index(t => t.CreateUserId)
            //    .Index(t => t.ModifieUserId);
            
            //CreateTable(
            //    "ContentManagement.WebPages",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Title = c.String(maxLength: 255),
            //            Url = c.String(maxLength: 1024),
            //            CategoryId = c.Int(),
            //            TemplatePatternUrl = c.String(maxLength: 1024),
            //            FrameWorkId = c.Int(),
            //            FrameWorkUrl = c.String(maxLength: 1024),
            //            DependentModules = c.String(maxLength: 2048),
            //            CommentOff = c.Boolean(nullable: false),
            //            Html = c.String(),
            //            HaveScript = c.Boolean(nullable: false),
            //            HaveStyle = c.Boolean(nullable: false),
            //            Tools = c.String(maxLength: 512),
            //            Services = c.String(),
            //            Params = c.String(maxLength: 1024),
            //            EditMode = c.Boolean(nullable: false),
            //            EnableCache = c.Boolean(nullable: false),
            //            IsMobileVersion = c.Boolean(nullable: false),
            //            TypeId = c.Int(nullable: false),
            //            Like = c.Int(nullable: false),
            //            DisLike = c.Int(nullable: false),
            //            CacheSlidingExpirationTimeInMinutes = c.Int(nullable: false),
            //            Guid = c.String(maxLength: 32),
            //            Version = c.Int(nullable: false),
            //            Language = c.String(maxLength: 8),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            ViewCount = c.Int(nullable: false),
            //            ViewRoleId = c.Int(),
            //            ModifyRoleId = c.Int(),
            //            AccessRoleId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Status = c.Int(nullable: false),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
            //    .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
            //    .Index(t => t.Guid, unique: true)
            //    .Index(t => t.CreateUserId)
            //    .Index(t => t.ModifieUserId);
            
            //CreateTable(
            //    "ContentManagement.EntityFiles",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FileId = c.Int(nullable: false),
            //            EntityId = c.Int(nullable: false),
            //            IsDefaults = c.Boolean(nullable: false),
            //            DynamicEntityTypeId = c.Int(),
            //            LinkId = c.Int(),
            //            WebPageId = c.Int(),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //            EntityTypeId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("ContentManagement.WebPages", t => t.WebPageId, cascadeDelete: true)
            //    .ForeignKey("ContentManagement.Links", t => t.LinkId, cascadeDelete: true)
            //    .ForeignKey("ContentManagement.MasterDataKeyValues", t => t.DynamicEntityTypeId)
            //    .ForeignKey("ContentManagement.Files", t => t.FileId, cascadeDelete: true)
            //    .Index(t => t.FileId)
            //    .Index(t => t.DynamicEntityTypeId)
            //    .Index(t => t.LinkId)
            //    .Index(t => t.WebPageId);
            
            //CreateTable(
            //    "ContentManagement.LanguageAndCultures",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false),
            //            Culture = c.String(maxLength: 8),
            //            Country = c.String(),
            //            IsRightToLeft = c.Boolean(nullable: false),
            //            IsDefaults = c.Boolean(nullable: false),
            //            FlagId = c.Int(),
            //            Version = c.Int(nullable: false),
            //            Language = c.String(maxLength: 8),
            //            CreateUserId = c.Int(),
            //            ModifieUserId = c.Int(),
            //            ViewCount = c.Int(nullable: false),
            //            ViewRoleId = c.Int(),
            //            ModifyRoleId = c.Int(),
            //            AccessRoleId = c.Int(),
            //            CreateLocalDateTime = c.String(maxLength: 19),
            //            ModifieLocalDateTime = c.String(maxLength: 19),
            //            AccessLocalDateTime = c.String(maxLength: 19),
            //            CreateDateTime = c.DateTime(nullable: false),
            //            ModifieDateTime = c.DateTime(nullable: false),
            //            AccessDateTime = c.DateTime(nullable: false),
            //            Status = c.Int(nullable: false),
            //            RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("ContentManagement.Users", t => t.CreateUserId)
            //    .ForeignKey("ContentManagement.FilePaths", t => t.FlagId)
            //    .ForeignKey("ContentManagement.Users", t => t.ModifieUserId)
            //    .Index(t => t.FlagId)
            //    .Index(t => t.CreateUserId)
            //    .Index(t => t.ModifieUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ContentManagement.EntityFiles", "FileId", "ContentManagement.Files");
            DropForeignKey("ContentManagement.EntityFiles", "DynamicEntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.EntityFilePaths", "FilePathId", "ContentManagement.FilePaths");
            DropForeignKey("ContentManagement.EntityFilePaths", "DynamicEntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.LanguageAndCultures", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LanguageAndCultures", "FlagId", "ContentManagement.FilePaths");
            DropForeignKey("ContentManagement.LanguageAndCultures", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityGroups", "MasterDataKeyValueId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.EntityGroups", "DynamicEntityTypeId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Comments", "WebPageId", "ContentManagement.WebPages");
            DropForeignKey("ContentManagement.Comments", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityGroups", "CommentId", "ContentManagement.Comments");
            DropForeignKey("ContentManagement.Comments", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Users", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityMasterDataKeyValues", "UserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityMasterDataKeyValues", "MasterDataKeyValueId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.EntityMasterDataKeyValues", "FileId", "ContentManagement.Files");
            DropForeignKey("ContentManagement.EntityMasterDataKeyValues", "FilePathId", "ContentManagement.FilePaths");
            DropForeignKey("ContentManagement.EntityMasterDataKeyValues", "LinkId", "ContentManagement.Links");
            DropForeignKey("ContentManagement.Links", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityGroups", "LinkId", "ContentManagement.Links");
            DropForeignKey("ContentManagement.EntityFiles", "LinkId", "ContentManagement.Links");
            DropForeignKey("ContentManagement.EntityFilePaths", "LinkId", "ContentManagement.Links");
            DropForeignKey("ContentManagement.WebPages", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityFiles", "WebPageId", "ContentManagement.WebPages");
            DropForeignKey("ContentManagement.EntityFilePaths", "WebPageId", "ContentManagement.WebPages");
            DropForeignKey("ContentManagement.WebPages", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.MasterDataKeyValues", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.EntityGroups", "GroupId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.MasterDataLocalKeyValues", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.MasterDataLocalKeyValues", "MasterDataKeyValueId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.MasterDataLocalKeyValues", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.MasterDataKeyValues", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.MasterDataKeyValues", "ParentId", "ContentManagement.MasterDataKeyValues");
            DropForeignKey("ContentManagement.Links", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Links", "ParentId", "ContentManagement.Links");
            DropForeignKey("ContentManagement.FilePaths", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFilePaths", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFilePaths", "FilePathId", "ContentManagement.FilePaths");
            DropForeignKey("ContentManagement.LocalFilePaths", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.FilePaths", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Files", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFiles", "ModifieUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.LocalFiles", "FileId", "ContentManagement.Files");
            DropForeignKey("ContentManagement.LocalFiles", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Files", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Users", "CreateUserId", "ContentManagement.Users");
            DropForeignKey("ContentManagement.Comments", "ParentId", "ContentManagement.Comments");
            DropIndex("ContentManagement.LanguageAndCultures", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.LanguageAndCultures", new[] { "CreateUserId" });
            DropIndex("ContentManagement.LanguageAndCultures", new[] { "FlagId" });
            DropIndex("ContentManagement.EntityFiles", new[] { "WebPageId" });
            DropIndex("ContentManagement.EntityFiles", new[] { "LinkId" });
            DropIndex("ContentManagement.EntityFiles", new[] { "DynamicEntityTypeId" });
            DropIndex("ContentManagement.EntityFiles", new[] { "FileId" });
            DropIndex("ContentManagement.WebPages", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.WebPages", new[] { "CreateUserId" });
            DropIndex("ContentManagement.WebPages", new[] { "Guid" });
            DropIndex("ContentManagement.MasterDataLocalKeyValues", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.MasterDataLocalKeyValues", new[] { "CreateUserId" });
            DropIndex("ContentManagement.MasterDataLocalKeyValues", new[] { "MasterDataKeyValueId" });
            DropIndex("ContentManagement.EntityGroups", new[] { "MasterDataKeyValueId" });
            DropIndex("ContentManagement.EntityGroups", new[] { "CommentId" });
            DropIndex("ContentManagement.EntityGroups", new[] { "LinkId" });
            DropIndex("ContentManagement.EntityGroups", new[] { "DynamicEntityTypeId" });
            DropIndex("ContentManagement.EntityGroups", new[] { "GroupId" });
            DropIndex("ContentManagement.MasterDataKeyValues", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.MasterDataKeyValues", new[] { "CreateUserId" });
            DropIndex("ContentManagement.MasterDataKeyValues", new[] { "Guid" });
            DropIndex("ContentManagement.MasterDataKeyValues", new[] { "ParentId" });
            DropIndex("ContentManagement.EntityFilePaths", new[] { "WebPageId" });
            DropIndex("ContentManagement.EntityFilePaths", new[] { "LinkId" });
            DropIndex("ContentManagement.EntityFilePaths", new[] { "DynamicEntityTypeId" });
            DropIndex("ContentManagement.EntityFilePaths", new[] { "FilePathId" });
            DropIndex("ContentManagement.Links", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.Links", new[] { "CreateUserId" });
            DropIndex("ContentManagement.Links", new[] { "ParentId" });
            DropIndex("ContentManagement.LocalFilePaths", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.LocalFilePaths", new[] { "CreateUserId" });
            DropIndex("ContentManagement.LocalFilePaths", new[] { "FilePathId" });
            DropIndex("ContentManagement.FilePaths", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.FilePaths", new[] { "CreateUserId" });
            DropIndex("ContentManagement.LocalFiles", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.LocalFiles", new[] { "CreateUserId" });
            DropIndex("ContentManagement.LocalFiles", new[] { "FileId" });
            DropIndex("ContentManagement.Files", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.Files", new[] { "CreateUserId" });
            DropIndex("ContentManagement.EntityMasterDataKeyValues", new[] { "UserId" });
            DropIndex("ContentManagement.EntityMasterDataKeyValues", new[] { "FilePathId" });
            DropIndex("ContentManagement.EntityMasterDataKeyValues", new[] { "FileId" });
            DropIndex("ContentManagement.EntityMasterDataKeyValues", new[] { "LinkId" });
            DropIndex("ContentManagement.EntityMasterDataKeyValues", new[] { "MasterDataKeyValueId" });
            DropIndex("ContentManagement.Users", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.Users", new[] { "CreateUserId" });
            DropIndex("ContentManagement.Comments", new[] { "ModifieUserId" });
            DropIndex("ContentManagement.Comments", new[] { "CreateUserId" });
            DropIndex("ContentManagement.Comments", new[] { "WebPageId" });
            DropIndex("ContentManagement.Comments", new[] { "ParentId" });
            DropTable("ContentManagement.LanguageAndCultures");
            DropTable("ContentManagement.EntityFiles");
            DropTable("ContentManagement.WebPages");
            DropTable("ContentManagement.MasterDataLocalKeyValues");
            DropTable("ContentManagement.EntityGroups");
            DropTable("ContentManagement.MasterDataKeyValues");
            DropTable("ContentManagement.EntityFilePaths");
            DropTable("ContentManagement.Links");
            DropTable("ContentManagement.LocalFilePaths");
            DropTable("ContentManagement.FilePaths");
            DropTable("ContentManagement.LocalFiles");
            DropTable("ContentManagement.Files");
            DropTable("ContentManagement.EntityMasterDataKeyValues");
            DropTable("ContentManagement.Users");
            DropTable("ContentManagement.Comments");
        }
    }
}
