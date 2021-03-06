﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            ProductController controller = new ProductController(mock.Object, null)
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
            ProductController controller = new ProductController(mock.Object, null);
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

        //[TestMethod]
        //public void CanFilterProducts()
        //{
        //    //Arrange
        //    Mock<IProductRepository> mock = new Mock<IProductRepository>();
        //    mock.Setup(m => m.Products).Returns(new Product[]
        //        {
        //            new Product {ProductId = 1, Name = "P1", Category = new Category { CategoryName = "Cat1", CategoryId = 1}, CategoryId = 1},
        //            new Product {ProductId = 2, Name = "P2", Category = new Category { CategoryName = "Cat2", CategoryId = 2}, CategoryId = 2},
        //            new Product {ProductId = 3, Name = "P3", Category = new Category { CategoryName = "Cat1", CategoryId = 1}, CategoryId = 1},
        //            new Product {ProductId = 4, Name = "P4", Category = new Category { CategoryName = "Cat2", CategoryId = 2}, CategoryId = 2},
        //            new Product {ProductId = 5, Name = "P5", Category = new Category { CategoryName = "Cat3", CategoryId = 3}, CategoryId = 3}
        //        });

        //    Mock<ICategoryRepository> mock2 = new Mock<ICategoryRepository>();
        //    mock2.Setup(m => m.Categories).Returns(new Category[]
        //    {
        //        new Category { CategoryName = "Cat1", CategoryId = 1},
        //        new Category { CategoryName = "Cat2", CategoryId = 2},
        //        new Category { CategoryName = "Cat3", CategoryId = 3}
        //    });

        //    ProductController controller = new ProductController(mock.Object, mock2.Object);
        //    controller.PageSize = 3;
        //    Category category = new Category { CategoryName = "Cat2" };

        //    //Act
        //    Product[] result = ((ProductsListViewModel)controller.List(category.CategoryName, 1).Model).Products.ToArray();

        //    //Assert
        //    Assert.AreEqual(result.Length, 2);
        //    Assert.IsTrue(result[0].Name == "P2" && result[0].Category.CategoryName == "Cat2");
        //    Assert.IsTrue(result[1].Name == "P4" && result[1].Category.CategoryName == "Cat2");
        //}


        [TestMethod]
        public void CanCreateCategories()
        {
            //Arrange
            Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();
            mock.Setup(m => m.Categories).Returns(new Category[]
                {
                    new Category { CategoryName = "Apples" },
                    new Category { CategoryName = "Apples" },
                    new Category { CategoryName = "Plums" },
                    new Category { CategoryName = "Oranges" }
                });
            NavController target = new NavController(mock.Object);
            

            //Act
            Category[] results = ((MenuViewModel)target.Menu(null).Model).Categories.Distinct().ToArray();

            //Assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0].CategoryName, "Apples");
            Assert.AreEqual(results[2].CategoryName, "Oranges");
            Assert.AreEqual(results[1].CategoryName, "Plums");

        }

        //[TestMethod]
        //public void IndicatesSelectedCategory()
        //{
        //    //Arrange
        //    Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();
        //    mock.Setup(m => m.Categories).Returns(new Category[]
        //        {
        //            new Category { CategoryName = "Apples"},
        //            new Category { CategoryName = "Oranges"}
        //        });

        //    NavController target = new NavController(mock.Object);

        //    Category categoryToSelect = new Category { CategoryName = "Apples" };

        //    //Act
        //    Category result = target.Menu(categoryToSelect.CategoryName).ViewBag.SelectedCategory;

        //    //Assert
        //    Assert.AreEqual(categoryToSelect, result);
        //}

        //[TestMethod]
        //public void GenerateCategorySpecificProductCount()
        //{
        //    //Arrange
        //    Mock<IProductRepository> mock = new Mock<IProductRepository>();
        //    mock.Setup(m => m.Products).Returns(new Product[]
        //        {
        //            new Product {ProductId = 1, Name = "P1", Category = new Category { CategoryName = "Cat1"}},
        //            new Product {ProductId = 2, Name = "P2", Category = new Category { CategoryName = "Cat2"}},
        //            new Product {ProductId = 3, Name = "P3", Category = new Category { CategoryName = "Cat1"}},
        //            new Product {ProductId = 4, Name = "P4", Category = new Category { CategoryName = "Cat2"}},
        //            new Product {ProductId = 5, Name = "P5", Category = new Category { CategoryName = "Cat3"}}
        //        });

        //    ProductController target = new ProductController(mock.Object, null);
        //    target.PageSize = 3;

        //    //Act
        //    int res1 = ((ProductsListViewModel)target.List("Cat1").Model).PagingInfo.TotalItems;
        //    int res2 = ((ProductsListViewModel)target.List("Cat2").Model).PagingInfo.TotalItems;
        //    int res3 = ((ProductsListViewModel)target.List("Cat3").Model).PagingInfo.TotalItems;
        //    int resAll = ((ProductsListViewModel)target.List(null).Model).PagingInfo.TotalItems;

        //    //Assert
        //    Assert.AreEqual(res1, 2);
        //    Assert.AreEqual(res2, 2);
        //    Assert.AreEqual(res3, 1);
        //    Assert.AreEqual(resAll, 5);
        //}

        #region Sorting
        [TestMethod]
        public void CanSortHighestPrice()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[]
                {
                    new Product { ProductId = 1, Name = "Produkt1", Price = 100M },
                    new Product { ProductId = 2, Name = "Produkt2", Price = 90M },
                    new Product { ProductId = 3, Name = "Produkt3", Price = 130.50M },
                    new Product { ProductId = 4, Name = "Produkt4", Price = 160.50M },
                    new Product { ProductId = 5, Name = "Produkt5", Price = 1500M }
                });

            ProductController productController = new ProductController(mock.Object, null);

            //Act
            Product[] result = ((ProductsListViewModel)productController.List(null, 1, SortingType.HighestPrice).Model).Products.ToArray();

            //Assert
            Assert.AreEqual(1500M, result[0].Price);
            Assert.AreEqual(160.50M, result[1].Price);
            Assert.AreEqual(130.50M, result[2].Price);
            Assert.AreEqual(100M, result[3].Price);
            Assert.AreEqual(90M, result[4].Price);
        }

        [TestMethod]
        public void CanSortLowestPrice()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[]
                {
                    new Product { ProductId = 1, Name = "Produkt1", Price = 100M },
                    new Product { ProductId = 2, Name = "Produkt2", Price = 90M },
                    new Product { ProductId = 3, Name = "Produkt3", Price = 130.50M },
                    new Product { ProductId = 4, Name = "Produkt4", Price = 160.50M },
                    new Product { ProductId = 5, Name = "Produkt5", Price = 1500M }
                });

            ProductController productController = new ProductController(mock.Object, null);

            //Act
            Product[] result = ((ProductsListViewModel)productController.List(
                null,
                1,
                SortingType.LowestPrice).Model).Products.ToArray();

            //Assert
            Assert.AreEqual(2, result[0].ProductId);
            Assert.AreEqual(1, result[1].ProductId);
            Assert.AreEqual(3, result[2].ProductId);
            Assert.AreEqual(4, result[3].ProductId);
            Assert.AreEqual(5, result[4].ProductId);
        }

        [TestMethod]
        public void CanSortMostPopular()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[]
                {
                    new Product { ProductId = 1, Name = "Produkt1", Price = 100M, Visits = 1000, NumberOfBought = 700 },
                    new Product { ProductId = 2, Name = "Produkt2", Price = 90M, Visits = 5000, NumberOfBought = 1700 },
                    new Product { ProductId = 3, Name = "Produkt3", Price = 130.50M, Visits = 200, NumberOfBought = 100 },
                    new Product { ProductId = 4, Name = "Produkt4", Price = 160.50M, Visits = 3000, NumberOfBought = 90 },
                    new Product { ProductId = 5, Name = "Produkt5", Price = 1500M, Visits = 4000, NumberOfBought = 1700 },
                    new Product { ProductId = 6, Name = "Produkt6", Price = 1500M, Visits = 4000, NumberOfBought = 3600 }
                });

            ProductController productController = new ProductController(mock.Object, null);

            //Act
            Product[] result = ((ProductsListViewModel)productController.List(null, 1, SortingType.MostPopular).Model).Products.ToArray();

            //Assert
            Assert.AreEqual("Produkt6", result[0].Name);
            Assert.AreEqual("Produkt2", result[1].Name);
            Assert.AreEqual("Produkt5", result[2].Name);
            Assert.AreEqual("Produkt1", result[3].Name);
            Assert.AreEqual("Produkt3", result[4].Name);
            Assert.AreEqual("Produkt4", result[5].Name);
        }
        #endregion
    }
}
