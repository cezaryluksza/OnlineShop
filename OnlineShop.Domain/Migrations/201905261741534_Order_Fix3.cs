namespace OnlineShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_Fix3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderByCartLines", "CartLine_CartLineId", "dbo.CartLines");
            DropForeignKey("dbo.AspNetUsers", "Address_AddressId", "dbo.Addresses");
            DropIndex("dbo.OrderByCartLines", new[] { "CartLine_CartLineId" });
            DropIndex("dbo.AspNetUsers", new[] { "Address_AddressId" });
            RenameColumn(table: "dbo.AspNetUsers", name: "Address_AddressId", newName: "AddressId");
            AddColumn("dbo.CartLines", "OrderId", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "AddressId");
            AddForeignKey("dbo.AspNetUsers", "AddressId", "dbo.Addresses", "AddressId", cascadeDelete: true);
            DropTable("dbo.OrderByCartLines");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderByCartLines",
                c => new
                    {
                        CartLinesByOrderId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        CartLine_CartLineId = c.Int(),
                    })
                .PrimaryKey(t => t.CartLinesByOrderId);
            
            DropForeignKey("dbo.AspNetUsers", "AddressId", "dbo.Addresses");
            DropIndex("dbo.AspNetUsers", new[] { "AddressId" });
            AlterColumn("dbo.AspNetUsers", "AddressId", c => c.Int());
            DropColumn("dbo.CartLines", "OrderId");
            RenameColumn(table: "dbo.AspNetUsers", name: "AddressId", newName: "Address_AddressId");
            CreateIndex("dbo.AspNetUsers", "Address_AddressId");
            CreateIndex("dbo.OrderByCartLines", "CartLine_CartLineId");
            AddForeignKey("dbo.AspNetUsers", "Address_AddressId", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.OrderByCartLines", "CartLine_CartLineId", "dbo.CartLines", "CartLineId");
        }
    }
}
