namespace Jam3iyati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Administrations", "adminID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Administrations", "adminID");
        }
    }
}
