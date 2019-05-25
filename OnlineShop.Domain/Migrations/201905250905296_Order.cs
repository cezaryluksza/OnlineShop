namespace OnlineShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Cart_CartId = c.Int(nullable: false),
                        ShippingDetails_ShippingDetailsId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Carts", t => t.Cart_CartId, cascadeDelete: true)
                .ForeignKey("dbo.ShippingDetails", t => t.ShippingDetails_ShippingDetailsId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Cart_CartId)
                .Index(t => t.ShippingDetails_ShippingDetailsId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateTable(
                "dbo.ShippingDetails",
                c => new
                    {
                        ShippingDetailsId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Line1 = c.String(nullable: false),
                        Line2 = c.String(),
                        Line3 = c.String(),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Zip = c.String(),
                        Country = c.String(nullable: false),
                        GiftWrap = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ShippingDetailsId);
            
            AddColumn("dbo.Addresses", "Line1", c => c.String(nullable: false));
            AddColumn("dbo.Addresses", "Line2", c => c.String());
            AddColumn("dbo.Addresses", "Line3", c => c.String());
            AddColumn("dbo.Addresses", "State", c => c.String(nullable: false));
            AddColumn("dbo.Addresses", "Zip", c => c.String());
            AlterColumn("dbo.Addresses", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "Country", c => c.String(nullable: false));
            DropColumn("dbo.Addresses", "AddressLine1");
            DropColumn("dbo.Addresses", "AddressLine2");
            DropColumn("dbo.Addresses", "Region");
            DropColumn("dbo.Addresses", "PostalCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "PostalCode", c => c.String());
            AddColumn("dbo.Addresses", "Region", c => c.String());
            AddColumn("dbo.Addresses", "AddressLine2", c => c.String());
            AddColumn("dbo.Addresses", "AddressLine1", c => c.String());
            DropForeignKey("dbo.Orders", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "ShippingDetails_ShippingDetailsId", "dbo.ShippingDetails");
            DropForeignKey("dbo.Orders", "Cart_CartId", "dbo.Carts");
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.Orders", new[] { "ShippingDetails_ShippingDetailsId" });
            DropIndex("dbo.Orders", new[] { "Cart_CartId" });
            AlterColumn("dbo.Addresses", "Country", c => c.String());
            AlterColumn("dbo.Addresses", "City", c => c.String());
            DropColumn("dbo.Addresses", "Zip");
            DropColumn("dbo.Addresses", "State");
            DropColumn("dbo.Addresses", "Line3");
            DropColumn("dbo.Addresses", "Line2");
            DropColumn("dbo.Addresses", "Line1");
            DropTable("dbo.ShippingDetails");
            DropTable("dbo.Carts");
            DropTable("dbo.Orders");
        }
    }
}
