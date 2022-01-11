namespace Jam3iyati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdministrationDemandes",
                c => new
                    {
                        AdministrationDemandeID = c.Int(nullable: false, identity: true),
                        AdministrationID = c.Int(nullable: false),
                        DemandeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdministrationDemandeID)
                .ForeignKey("dbo.Administrations", t => t.AdministrationID, cascadeDelete: true)
                .ForeignKey("dbo.Demandes", t => t.DemandeID, cascadeDelete: true)
                .Index(t => t.AdministrationID)
                .Index(t => t.DemandeID);
            
            CreateTable(
                "dbo.Administrations",
                c => new
                    {
                        AdministrationID = c.Int(nullable: false, identity: true),
                        type = c.Int(nullable: false),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.AdministrationID);
            
            CreateTable(
                "dbo.Associations",
                c => new
                    {
                        AssociationID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        field = c.Int(nullable: false),
                        subject = c.String(),
                        date = c.DateTime(nullable: false),
                        AdministrationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssociationID)
                .ForeignKey("dbo.Administrations", t => t.AdministrationID, cascadeDelete: false)
                .Index(t => t.AdministrationID);
            
            CreateTable(
                "dbo.AssociationMemebers",
                c => new
                    {
                        AssociationMemeberID = c.Int(nullable: false, identity: true),
                        MemeberID = c.Int(nullable: false),
                        AssociationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssociationMemeberID)
                .ForeignKey("dbo.Associations", t => t.AssociationID, cascadeDelete: true)
                .ForeignKey("dbo.Memebers", t => t.MemeberID, cascadeDelete: true)
                .Index(t => t.MemeberID)
                .Index(t => t.AssociationID);
            
            CreateTable(
                "dbo.Memebers",
                c => new
                    {
                        MemeberID = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        birthday = c.DateTime(nullable: false),
                        birthplace = c.String(),
                        cardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MemeberID);

            CreateTable(
                "dbo.Demandes",
                c => new
                {
                    DemandeID = c.Int(nullable: false, identity: true),
                    date = c.DateTime(nullable: false),
                    type = c.Int(nullable: false),
                    AssociationID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.DemandeID)
                .ForeignKey("dbo.Associations", t => t.AssociationID, cascadeDelete: true)
                .Index(t => t.AssociationID);
            
            CreateTable(
                "dbo.Apcs",
                c => new
                    {
                        ApcID = c.Int(nullable: false, identity: true),
                        DairaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApcID)
                .ForeignKey("dbo.Dairas", t => t.DairaID, cascadeDelete: true)
                .Index(t => t.DairaID);
            
            CreateTable(
                "dbo.Dairas",
                c => new
                    {
                        DairaID = c.Int(nullable: false, identity: true),
                        WilayaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DairaID)
                .ForeignKey("dbo.Wilayas", t => t.WilayaID, cascadeDelete: true)
                .Index(t => t.WilayaID);
            
            CreateTable(
                "dbo.Wilayas",
                c => new
                    {
                        WilayaID = c.Int(nullable: false, identity: true),
                        code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WilayaID);
            
            
            
          
           
            
          
            
           
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Apcs", "DairaID", "dbo.Dairas");
            DropForeignKey("dbo.Dairas", "WilayaID", "dbo.Wilayas");
            DropForeignKey("dbo.Demandes", "AssociationID", "dbo.Associations");
            DropForeignKey("dbo.AdministrationDemandes", "DemandeID", "dbo.Demandes");
            DropForeignKey("dbo.AssociationMemebers", "MemeberID", "dbo.Memebers");
            DropForeignKey("dbo.AssociationMemebers", "AssociationID", "dbo.Associations");
            DropForeignKey("dbo.Associations", "AdministrationID", "dbo.Administrations");
            DropForeignKey("dbo.AdministrationDemandes", "AdministrationID", "dbo.Administrations");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Dairas", new[] { "WilayaID" });
            DropIndex("dbo.Apcs", new[] { "DairaID" });
            DropIndex("dbo.Demandes", new[] { "AssociationID" });
            DropIndex("dbo.AssociationMemebers", new[] { "AssociationID" });
            DropIndex("dbo.AssociationMemebers", new[] { "MemeberID" });
            DropIndex("dbo.Associations", new[] { "AdministrationID" });
            DropIndex("dbo.AdministrationDemandes", new[] { "DemandeID" });
            DropIndex("dbo.AdministrationDemandes", new[] { "AdministrationID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Wilayas");
            DropTable("dbo.Dairas");
            DropTable("dbo.Apcs");
            DropTable("dbo.Demandes");
            DropTable("dbo.Memebers");
            DropTable("dbo.AssociationMemebers");
            DropTable("dbo.Associations");
            DropTable("dbo.Administrations");
            DropTable("dbo.AdministrationDemandes");
        }
    }
}
