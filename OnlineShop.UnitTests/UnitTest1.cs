using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Entities;
using OnlineShop.WebUI.Controllers;

namespace OnlineShop.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
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
            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;

            //Assert
            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }
    }
}
