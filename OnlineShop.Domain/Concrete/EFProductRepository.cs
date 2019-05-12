using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    if (product.ImageData != null)
                    {
                        dbEntry.ImageData = product.ImageData;
                        dbEntry.ImageDataThumbnail = product.ImageDataThumbnail;
                    }

                    if (product.ImageMimeType != null)
                        dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
        public Product DeleteProduct(int productId)
        {
            Product dbEntry = context.Products.Find(productId);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public IEnumerable<Product> MapProducts()
        {
            var products = context.Products;
            foreach (var product in products)
            {
                //product.Category = context.Categories.FirstOrDefault(x => x.CategoryId == product.CategoryId);
            }
            return products;
        }
    }
}
