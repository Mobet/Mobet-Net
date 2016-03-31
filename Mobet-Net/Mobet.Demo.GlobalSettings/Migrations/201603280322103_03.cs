namespace Mobet.Demo.GlobalSettings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GlobalSettings", "DisplayName", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.GlobalSettings", "Description", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GlobalSettings", "Description");
            DropColumn("dbo.GlobalSettings", "DisplayName");
        }
    }
}
