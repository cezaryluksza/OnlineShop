namespace OnlineShop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryFix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Products", name: "Category_CategoryId", newName: "CategoryId");
            RenameIndex(table: "dbo.Products", name: "IX_Category_CategoryId", newName: "IX_CategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Products", name: "IX_CategoryId", newName: "IX_Category_CategoryId");
            RenameColumn(table: "dbo.Products", name: "CategoryId", newName: "Category_CategoryId");
        }
    }
}
