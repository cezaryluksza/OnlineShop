using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.Entities;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineShop.Domain.Concrete
{
    public partial class EFDbContext : IdentityDbContext<ApplicationUser>
    {
        public EFDbContext() : base("EFDbContext") { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CartLine> Cartlines { get; set; }
        public DbSet<OrderByCartLines> OrderByCartLines { get; set; }
        public DbSet<Order> Orders { get; set; }



        public static EFDbContext Create()
        {
            return new EFDbContext();
        }
    }
}
