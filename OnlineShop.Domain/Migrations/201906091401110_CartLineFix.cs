namespace OnlineShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CartLineFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartLines", "Order_OrderId", c => c.Int());
            CreateIndex("dbo.CartLines", "Order_OrderId");
            AddForeignKey("dbo.CartLines", "Order_OrderId", "dbo.Orders", "OrderId");
            DropColumn("dbo.CartLines", "OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CartLines", "OrderId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CartLines", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.CartLines", new[] { "Order_OrderId" });
            DropColumn("dbo.CartLines", "Order_OrderId");
        }
    }
}
