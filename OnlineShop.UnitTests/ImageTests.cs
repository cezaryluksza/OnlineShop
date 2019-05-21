using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Entities;
using OnlineShop.WebUI.Controllers;
//using OnlineShop.WebUI.Infrastructure.Abstract;
using OnlineShop.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineShop.UnitTests
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void CanRetrieveImageData()
        {
            //Arrange
            Product prod = new Product()
            {
                ProductId = 2,
                Name = "Test",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { ProductId = 1, Name = "P1"},
                prod,
                new Product { ProductId = 3, Name = "P3"}
            }.AsQueryable());

            ProductController target = new ProductController(mock.Object, null);
            //Act
            ActionResult result = target.GetImage(2);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(prod.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void CannotRetrieveImageDataForInvalidId()
        {
            //Arrange

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { ProductId = 1, Name = "P1"},
                new Product { ProductId = 2, Name = "P2"}
            }.AsQueryable());

            ProductController target = new ProductController(mock.Object, null);
            //Act
            ActionResult result = target.GetImage(100);

            //Assert
            Assert.IsNull(result);
        }


    }
}
