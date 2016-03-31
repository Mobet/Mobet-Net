namespace Mobet.Demo.GlobalSettings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _02 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GlobalSettingGroups", "GlobalSettingGroupId", "dbo.GlobalSettingGroups");
            DropForeignKey("dbo.GlobalSettings", "GlobalSettingGroupId", "dbo.GlobalSettingGroups");
            DropIndex("dbo.GlobalSettingGroups", new[] { "GlobalSettingGroupId" });
            DropIndex("dbo.GlobalSettings", new[] { "GlobalSettingGroupId" });
            AddColumn("dbo.GlobalSettings", "Scope", c => c.Int(nullable: false));
            AddColumn("dbo.GlobalSettings", "Group", c => c.String());
            DropColumn("dbo.GlobalSettings", "Scopes");
            DropColumn("dbo.GlobalSettings", "GlobalSettingGroupId");
            DropTable("dbo.GlobalSettingGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GlobalSettingGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        GlobalSettingGroupId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.GlobalSettings", "GlobalSettingGroupId", c => c.Guid(nullable: false));
            AddColumn("dbo.GlobalSettings", "Scopes", c => c.Int(nullable: false));
            DropColumn("dbo.GlobalSettings", "Group");
            DropColumn("dbo.GlobalSettings", "Scope");
            CreateIndex("dbo.GlobalSettings", "GlobalSettingGroupId");
            CreateIndex("dbo.GlobalSettingGroups", "GlobalSettingGroupId");
            AddForeignKey("dbo.GlobalSettings", "GlobalSettingGroupId", "dbo.GlobalSettingGroups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GlobalSettingGroups", "GlobalSettingGroupId", "dbo.GlobalSettingGroups", "Id");
        }
    }
}
