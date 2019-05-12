using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Concrete
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Category> Categories
        {
            get { return context.Categories; }
        }

        public void SaveCategory(Category category)
        {
            if (category.CategoryId == 0)
            {
                context.Categories.Add(category);
            }
            else
            {
                Category dbEntry = context.Categories.Find(category.CategoryId);
                if (dbEntry != null)
                {
                    dbEntry.CategoryName = category.CategoryName;
                }
            }
            context.SaveChanges();
        }
        public Category DeleteCategory(int categoryId)
        {
            Category dbEntry = context.Categories.Find(categoryId);
            if (dbEntry != null)
            {
                context.Categories.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public int GetCategoryId(string categoryName)
        {
            Category category = context.Categories.FirstOrDefault(x => x.CategoryName == categoryName);
            if (category != null)
            {
                return category.CategoryId;
            }
            return 0;
        }
    }
}
