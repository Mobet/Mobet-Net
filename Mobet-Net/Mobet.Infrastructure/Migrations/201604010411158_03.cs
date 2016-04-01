namespace Mobet.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GlobalSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        DisplayName = c.String(nullable: false, maxLength: 256),
                        Value = c.String(maxLength: 2000),
                        Description = c.String(maxLength: 2000),
                        Scope = c.Int(nullable: false),
                        Group = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GlobalSettings");
        }
    }
}
