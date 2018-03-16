namespace EagleEye2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Department = c.String(),
                        AccessLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        StoreId = c.Int(nullable: false),
                        CardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cards", t => t.CardId, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId)
                .Index(t => t.CardId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Transactions", "CardId", "dbo.Cards");
            DropForeignKey("dbo.Cards", "UserId", "dbo.Users");
            DropIndex("dbo.Transactions", new[] { "CardId" });
            DropIndex("dbo.Transactions", new[] { "StoreId" });
            DropIndex("dbo.Cards", new[] { "UserId" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Stores");
            DropTable("dbo.Users");
            DropTable("dbo.Cards");
        }
    }
}
