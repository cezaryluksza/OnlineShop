namespace OnlineShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderTime");
        }
    }
}
