namespace Jam3iyati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Administrations", "userID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Administrations", "userID", c => c.Int(nullable: false));
        }
    }
}
