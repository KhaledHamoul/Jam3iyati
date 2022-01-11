namespace Jam3iyati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "read", c => c.Boolean(nullable: false));
            DropColumn("dbo.Demandes", "read");
            DropColumn("dbo.Notifications", "link");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "link", c => c.String());
            AddColumn("dbo.Demandes", "read", c => c.Boolean(nullable: false));
            DropColumn("dbo.Notifications", "read");
        }
    }
}
