using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using Ninject;
using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Entities;

namespace OnlineShop.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product { Name = "Piłka nożna", Price = 25},
                new Product { Name = "Deska surfingowa", Price = 179 },
                new Product { Name = "Buty do biegania", Price = 95 }
            });

            kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}