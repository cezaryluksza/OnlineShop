namespace OnlineShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCartLine : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CartLines", name: "Order_OrderId", newName: "OrderId");
            RenameIndex(table: "dbo.CartLines", name: "IX_Order_OrderId", newName: "IX_OrderId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CartLines", name: "IX_OrderId", newName: "IX_Order_OrderId");
            RenameColumn(table: "dbo.CartLines", name: "OrderId", newName: "Order_OrderId");
        }
    }
}
