namespace Jam3iyati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notifications", "content", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notifications", "content", c => c.Int(nullable: false));
        }
    }
}
