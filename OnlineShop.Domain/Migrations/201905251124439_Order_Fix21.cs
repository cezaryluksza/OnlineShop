namespace OnlineShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_Fix21 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "OrderByCartLines_CartLinesByOrderId", "dbo.OrderByCartLines");
            DropIndex("dbo.Orders", new[] { "OrderByCartLines_CartLinesByOrderId" });
            DropColumn("dbo.Orders", "OrderByCartLines_CartLinesByOrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderByCartLines_CartLinesByOrderId", c => c.Int());
            CreateIndex("dbo.Orders", "OrderByCartLines_CartLinesByOrderId");
            AddForeignKey("dbo.Orders", "OrderByCartLines_CartLinesByOrderId", "dbo.OrderByCartLines", "CartLinesByOrderId");
        }
    }
}
