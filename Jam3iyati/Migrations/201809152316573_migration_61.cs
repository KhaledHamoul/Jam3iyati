namespace Jam3iyati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_61 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdministrationDemandes", "state", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdministrationDemandes", "state");
        }
    }
}
