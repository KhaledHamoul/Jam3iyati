namespace Jam3iyati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Associations", "userId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Associations", "userId", c => c.Int(nullable: false));
        }
    }
}
