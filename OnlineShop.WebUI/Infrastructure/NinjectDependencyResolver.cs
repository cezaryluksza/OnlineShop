using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Ninject;
using OnlineShop.Domain.Abstract;
using OnlineShop.Domain.Concrete;
using OnlineShop.WebUI.Infrastructure.Abstract;
using OnlineShop.WebUI.Infrastructure.Concrete;

namespace OnlineShop.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            _kernel.Bind<IProductRepository>().To<EFProductRepository>();

            var emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            _kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);

            //_kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();

            _kernel.Bind<ICategoryRepository>().To<EFCategoryRepository>();

        }
    }
}