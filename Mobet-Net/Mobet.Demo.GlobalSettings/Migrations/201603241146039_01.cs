namespace Mobet.Demo.GlobalSettings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GlobalSettingGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        GlobalSettingGroupId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GlobalSettingGroups", t => t.GlobalSettingGroupId)
                .Index(t => t.GlobalSettingGroupId);
            
            CreateTable(
                "dbo.GlobalSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Value = c.String(maxLength: 2000),
                        Scopes = c.Int(nullable: false),
                        GlobalSettingGroupId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GlobalSettingGroups", t => t.GlobalSettingGroupId, cascadeDelete: true)
                .Index(t => t.GlobalSettingGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GlobalSettings", "GlobalSettingGroupId", "dbo.GlobalSettingGroups");
            DropForeignKey("dbo.GlobalSettingGroups", "GlobalSettingGroupId", "dbo.GlobalSettingGroups");
            DropIndex("dbo.GlobalSettings", new[] { "GlobalSettingGroupId" });
            DropIndex("dbo.GlobalSettingGroups", new[] { "GlobalSettingGroupId" });
            DropTable("dbo.GlobalSettings");
            DropTable("dbo.GlobalSettingGroups");
        }
    }
}
