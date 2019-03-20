using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Entities;
using OnlineShop.WebUI.Controllers;
using OnlineShop.WebUI.Models;
using OnlineShop.WebUI.HtmlHelpers;

namespace OnlineShop.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanPaginate()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { ProductId = 1, Name = "P1"},
                new Product { ProductId = 2, Name = "P2"},
                new Product { ProductId = 3, Name = "P3"},
                new Product { ProductId = 4, Name = "P4"},
                new Product { ProductId = 5, Name = "P5"}
            });

            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            //Act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void CanGeneratePageLinks()
        {
            //Arrange - define an HTML gelper - we need to do this
            //in order to apply the extension method
            HtmlHelper htmlHelper = null;

            //Arrange - create PagingInfo data
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            //Arrange - set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Strona" + i;

            //Act
            MvcHtmlString result = htmlHelper.PageLinks(pagingInfo, pageUrlDelegate);

            //Assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Strona1"">1</a>" + @"<a class=""btn btn-default btn-primary selected"" href=""Strona2"">2</a>" + @"<a class=""btn btn-default"" href=""Strona3"">3</a>", result.ToString());
        }

        [TestMethod]
        public void CanSendPaginationViewModel()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { ProductId = 1, Name = "P1"},
                new Product { ProductId = 2, Name = "P2"},
                new Product { ProductId = 3, Name = "P3"},
                new Product { ProductId = 4, Name = "P4"},
                new Product { ProductId = 5, Name = "P5"}
            });

            //Arrange
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void CanFilterProducts()
        {
            //Arrange
            //create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                    new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
                    new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
                    new Product {ProductId = 4, Name = "P4", Category = "Cat2"},
                    new Product {ProductId = 5, Name = "P5", Category = "Cat3"}
                });

            //Arrange - create a controller and mage the page size 3 items
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            Product[] result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            //Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
        }


        [TestMethod]
        public void CanCreateCategories()
        {
            //Arrange
            // - create te mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product {ProductId = 1, Name = "P1", Category = "Apples"},
                    new Product {ProductId = 2, Name = "P2", Category = "Apples"},
                    new Product {ProductId = 3, Name = "P3", Category = "Plums"},
                    new Product {ProductId = 4, Name = "P4", Category = "Oranges"}
                });
            NavController target = new NavController(mock.Object);


            //Act - get the set of category
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();

            //Assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Apples");
            Assert.AreEqual(results[1], "Oranges");
            Assert.AreEqual(results[2], "Plums");

        }

        [TestMethod]
        public void IndicatesSelectedCategory()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product {ProductId = 1, Name = "P1", Category = "Apples"},
                    new Product {ProductId = 2, Name = "P2", Category = "Oranges"},
                });

            NavController target = new NavController(mock.Object);

            string categoryToSelect = "Apples";

            //Act
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            //Assert
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void GenerateCategorySpecificProductCount()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                    new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
                    new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
                    new Product {ProductId = 4, Name = "P4", Category = "Cat2"},
                    new Product {ProductId = 5, Name = "P5", Category = "Cat3"}
                });

            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            //Act
            int res1 = ((ProductsListViewModel)target.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductsListViewModel)target.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductsListViewModel)target.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((ProductsListViewModel)target.List(null).Model).PagingInfo.TotalItems;

            //Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
