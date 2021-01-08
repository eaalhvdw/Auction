using System.Data.Entity.Migrations;

namespace AuctionLibrary.Migrations
{
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Number = c.String(nullable: false, maxLength: 15),
                        Email = c.String(nullable: false),
                        LoginNameErrorMsg = c.String(),
                        LoginNumberErrorMsg = c.String(),
                        LoginEmailErrorMsg = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BuyingOffers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PriceAmount = c.Double(nullable: false),
                        ContactInfo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactInfoes", t => t.ContactInfo_Id, cascadeDelete: true)
                .ForeignKey("dbo.SalesSupplies", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.ContactInfo_Id);
            
            CreateTable(
                "dbo.SalesSupplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MetalType = c.Int(nullable: false),
                        MetalAmount = c.Int(nullable: false),
                        HighestPrice = c.Double(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        ImgFolderPath = c.String(),
                        ImgPath = c.String(),
                        ContactInfo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactInfoes", t => t.ContactInfo_Id, cascadeDelete: true)
                .Index(t => t.ContactInfo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BuyingOffers", "Id", "dbo.SalesSupplies");
            DropForeignKey("dbo.SalesSupplies", "ContactInfo_Id", "dbo.ContactInfoes");
            DropForeignKey("dbo.BuyingOffers", "ContactInfo_Id", "dbo.ContactInfoes");
            DropIndex("dbo.SalesSupplies", new[] { "ContactInfo_Id" });
            DropIndex("dbo.BuyingOffers", new[] { "ContactInfo_Id" });
            DropIndex("dbo.BuyingOffers", new[] { "Id" });
            DropTable("dbo.SalesSupplies");
            DropTable("dbo.BuyingOffers");
            DropTable("dbo.ContactInfoes");
        }
    }
}
