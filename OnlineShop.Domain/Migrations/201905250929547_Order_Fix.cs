namespace OnlineShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_Fix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Cart_CartId", "dbo.Carts");
            DropIndex("dbo.Orders", new[] { "Cart_CartId" });
            CreateTable(
                "dbo.CartLines",
                c => new
                    {
                        CartLineId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.CartLineId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.CartLinesByOrders",
                c => new
                    {
                        CartLinesByOrderId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        CartLine_CartLineId = c.Int(),
                    })
                .PrimaryKey(t => t.CartLinesByOrderId)
                .ForeignKey("dbo.CartLines", t => t.CartLine_CartLineId)
                .Index(t => t.CartLine_CartLineId);
            
            AddColumn("dbo.Orders", "CartLinesByOrder_CartLinesByOrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "CartLinesByOrder_CartLinesByOrderId");
            AddForeignKey("dbo.Orders", "CartLinesByOrder_CartLinesByOrderId", "dbo.CartLinesByOrders", "CartLinesByOrderId", cascadeDelete: true);
            DropColumn("dbo.Orders", "Cart_CartId");
            DropTable("dbo.Carts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.CartId);
            
            AddColumn("dbo.Orders", "Cart_CartId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Orders", "CartLinesByOrder_CartLinesByOrderId", "dbo.CartLinesByOrders");
            DropForeignKey("dbo.CartLinesByOrders", "CartLine_CartLineId", "dbo.CartLines");
            DropForeignKey("dbo.CartLines", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "CartLinesByOrder_CartLinesByOrderId" });
            DropIndex("dbo.CartLinesByOrders", new[] { "CartLine_CartLineId" });
            DropIndex("dbo.CartLines", new[] { "Product_ProductId" });
            DropColumn("dbo.Orders", "CartLinesByOrder_CartLinesByOrderId");
            DropTable("dbo.CartLinesByOrders");
            DropTable("dbo.CartLines");
            CreateIndex("dbo.Orders", "Cart_CartId");
            AddForeignKey("dbo.Orders", "Cart_CartId", "dbo.Carts", "CartId", cascadeDelete: true);
        }
    }
}
