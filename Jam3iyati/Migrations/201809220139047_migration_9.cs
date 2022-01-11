namespace Jam3iyati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Demandes", "read", c => c.Boolean(nullable: false , defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Demandes", "read");
        }
    }
}
