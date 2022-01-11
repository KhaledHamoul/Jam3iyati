namespace Jam3iyati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        notificationID = c.Int(nullable: false, identity: true),
                        subject = c.Int(nullable: false),
                        content = c.Int(nullable: false),
                        link = c.String(),
                        date = c.DateTime(nullable: false),
                        AssociationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.notificationID)
                .ForeignKey("dbo.Associations", t => t.AssociationID, cascadeDelete: true)
                .Index(t => t.AssociationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "AssociationID", "dbo.Associations");
            DropIndex("dbo.Notifications", new[] { "AssociationID" });
            DropTable("dbo.Notifications");
        }
    }
}
