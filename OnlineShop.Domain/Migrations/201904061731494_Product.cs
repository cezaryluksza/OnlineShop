namespace OnlineShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Visits", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "NumberOfBought", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "NumberOfComments", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "NumberOfComments");
            DropColumn("dbo.Products", "NumberOfBought");
            DropColumn("dbo.Products", "Visits");
        }
    }
}
