namespace OnlineShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addresses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        Region = c.String(),
                        PostalCode = c.String(),
                        City = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            DropColumn("dbo.AspNetUsers", "UserRole");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserRole", c => c.String());
            DropColumn("dbo.AspNetUsers", "Address");
            DropTable("dbo.Addresses");
        }
    }
}
