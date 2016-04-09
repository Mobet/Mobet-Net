namespace Mobet.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CreateUser", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "ChangeUser", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "DeleteUser", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "Version", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            DropColumn("dbo.Users", "CreateAccount");
            DropColumn("dbo.Users", "ChangeAccount");
            DropColumn("dbo.Users", "DeleteAccount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "DeleteAccount", c => c.String());
            AddColumn("dbo.Users", "ChangeAccount", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "CreateAccount", c => c.String(maxLength: 50));
            DropColumn("dbo.Users", "Version");
            DropColumn("dbo.Users", "DeleteUser");
            DropColumn("dbo.Users", "ChangeUser");
            DropColumn("dbo.Users", "CreateUser");
        }
    }
}
