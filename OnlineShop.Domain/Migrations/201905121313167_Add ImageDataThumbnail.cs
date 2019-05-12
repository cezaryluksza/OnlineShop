namespace OnlineShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageDataThumbnail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageDataThumbnail", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageDataThumbnail");
        }
    }
}
