using OnlineShop.Domain.Concrete;
using OnlineShop.Domain.Entities;
using OnlineShop.WebUI.Controllers;
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

                    var category = new Category()
                    {
                        CategoryName = "Elektronika"
                    };

                    //var category2 = new Category()
                    //{
                    //    CategoryName = "Elektronika"
                    //};

                    //List<Category> list = new List<Category>() {category, category2 };

                    //list = list.Distinct().ToList();

                    
                    //foreach (var item in list)
                    //{
                    //    System.Console.WriteLine(item.);
                    //}

                    context.Categories.Add(category);

                    var product = new Product()
                    {
                        Name = "Testowy produkt10",
                        Description = "Testowy opis10 źćłąęż",
                        Price = 89.99M,
                        Category = category

                    };
                    context.Products.Add(product);



                    context.SaveChanges();
                }
                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Main error" + e);
            }

        }
    }
}
