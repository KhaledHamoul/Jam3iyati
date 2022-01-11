namespace Jam3iyati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Administrations", "userID", c => c.Int(nullable: false));
            AddColumn("dbo.Associations", "userID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Associations", "userID");
            DropColumn("dbo.Administrations", "userID");
        }
    }
}
