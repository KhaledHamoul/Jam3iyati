namespace Jam3iyati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apcs", "apcCode", c => c.Int(nullable: false));
            AddColumn("dbo.Dairas", "dairaCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dairas", "dairaCode");
            DropColumn("dbo.Apcs", "apcCode");
        }
    }
}
