using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.Entities;
using System.Data.Entity;

namespace OnlineShop.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base()
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
