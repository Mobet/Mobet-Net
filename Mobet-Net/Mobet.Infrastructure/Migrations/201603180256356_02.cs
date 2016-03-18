namespace Mobet.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Password", c => c.String(maxLength: 250));
            AddColumn("dbo.Users", "Salt", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "Subject", c => c.String());
            AlterColumn("dbo.Menus", "Url", c => c.String(maxLength: 250));
            DropColumn("dbo.Users", "Mobilephone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Mobilephone", c => c.String(maxLength: 50));
            AlterColumn("dbo.Menus", "Url", c => c.String(maxLength: 50));
            DropColumn("dbo.Users", "Subject");
            DropColumn("dbo.Users", "Salt");
            DropColumn("dbo.Users", "Password");
        }
    }
}
