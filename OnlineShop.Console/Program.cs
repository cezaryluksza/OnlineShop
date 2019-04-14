using OnlineShop.Domain.Concrete;
using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (EFDbContext context = new EFDbContext())
                {
                    var product = new Product()
                    {
                        Name = "Testowy produkt10",
                        Description = "Testowy opis10 źćłąęż",
                        Price = 89.99M,
                        Category = "Testowa kategoria",

                    };
                    context.Products.Add(product);
                    context.SaveChanges();
                }
                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();

            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}
