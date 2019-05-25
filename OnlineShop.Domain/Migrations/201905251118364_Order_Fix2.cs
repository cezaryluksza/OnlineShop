namespace OnlineShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_Fix2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CartLinesByOrders", newName: "OrderByCartLines");
            DropForeignKey("dbo.Orders", "CartLinesByOrder_CartLinesByOrderId", "dbo.CartLinesByOrders");
            DropIndex("dbo.Orders", new[] { "CartLinesByOrder_CartLinesByOrderId" });
            RenameColumn(table: "dbo.Orders", name: "CartLinesByOrder_CartLinesByOrderId", newName: "OrderByCartLines_CartLinesByOrderId");
            AlterColumn("dbo.Orders", "OrderByCartLines_CartLinesByOrderId", c => c.Int());
            CreateIndex("dbo.Orders", "OrderByCartLines_CartLinesByOrderId");
            AddForeignKey("dbo.Orders", "OrderByCartLines_CartLinesByOrderId", "dbo.OrderByCartLines", "CartLinesByOrderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderByCartLines_CartLinesByOrderId", "dbo.OrderByCartLines");
            DropIndex("dbo.Orders", new[] { "OrderByCartLines_CartLinesByOrderId" });
            AlterColumn("dbo.Orders", "OrderByCartLines_CartLinesByOrderId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Orders", name: "OrderByCartLines_CartLinesByOrderId", newName: "CartLinesByOrder_CartLinesByOrderId");
            CreateIndex("dbo.Orders", "CartLinesByOrder_CartLinesByOrderId");
            AddForeignKey("dbo.Orders", "CartLinesByOrder_CartLinesByOrderId", "dbo.CartLinesByOrders", "CartLinesByOrderId", cascadeDelete: true);
            RenameTable(name: "dbo.OrderByCartLines", newName: "CartLinesByOrders");
        }
    }
}
