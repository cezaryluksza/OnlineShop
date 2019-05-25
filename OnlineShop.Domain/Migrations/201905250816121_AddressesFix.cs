namespace OnlineShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressesFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address_AddressId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Address_AddressId");
            AddForeignKey("dbo.AspNetUsers", "Address_AddressId", "dbo.Addresses", "AddressId");
            DropColumn("dbo.AspNetUsers", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            DropForeignKey("dbo.AspNetUsers", "Address_AddressId", "dbo.Addresses");
            DropIndex("dbo.AspNetUsers", new[] { "Address_AddressId" });
            DropColumn("dbo.AspNetUsers", "Address_AddressId");
        }
    }
}
