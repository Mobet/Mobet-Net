namespace Mobet.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Url = c.String(maxLength: 50),
                        Sort = c.Int(nullable: false),
                        ParentId = c.Int(),
                        Icon = c.String(maxLength: 50),
                        LabelCss = c.String(maxLength: 50),
                        LabelText = c.String(maxLength: 50),
                        Description = c.String(maxLength: 250),
                        Version = c.Binary(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Code = c.String(maxLength: 50),
                        Description = c.String(maxLength: 250),
                        Version = c.Binary(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OpenId = c.String(maxLength: 50),
                        Name = c.String(maxLength: 50),
                        NickName = c.String(maxLength: 50),
                        Birthday = c.DateTime(),
                        Mobilephone = c.String(maxLength: 50),
                        Telphone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Street = c.String(maxLength: 250),
                        City = c.String(maxLength: 50),
                        Province = c.String(maxLength: 50),
                        Country = c.String(maxLength: 50),
                        Sex = c.Byte(),
                        Headimageurl = c.String(maxLength: 2000),
                        IdentityNo = c.String(maxLength: 50),
                        Version = c.Binary(),
                        CreateAccount = c.String(maxLength: 50),
                        CreateTime = c.DateTime(),
                        ChangeAccount = c.String(maxLength: 50),
                        ChangeTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleteAccount = c.String(),
                        DeleteTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleMenus",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.MenuId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Menus", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.RoleMenus", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.RoleMenus", "RoleId", "dbo.Roles");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.RoleMenus", new[] { "MenuId" });
            DropIndex("dbo.RoleMenus", new[] { "RoleId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.RoleMenus");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Menus");
        }
    }
}
