using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineShop.WebUI.Controllers;
using OnlineShop.WebUI.Infrastructure.Abstract;
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
    public class AdminSecurityTests
    {
        //[TestMethod]
        //public void CanLoginWithValidCredentials()
        //{
        //    //Arrange
        //    Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
        //    mock.Setup(m => m.Authenticate("admin", "123")).Returns(true);

        //    LoginViewModel model = new LoginViewModel
        //    {
        //        UserName = "admin",
        //        Password = "123"
        //    };

        //    AccountController target = new AccountController(mock.Object);

        //    //Act
        //    ActionResult result = target.Login(model, "/MyURL");

        //    //Assert
        //    Assert.IsInstanceOfType(result, typeof(RedirectResult));
        //    Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        //}

    //    [TestMethod]
    //    public void CannotLoginWithINvalidCredentials()
    //    {
    //        //Arrange
    //        Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
    //        mock.Setup(m => m.Authenticate("nieprawidłowyUżytkownik", "nieprawidłoweHasło")).Returns(false);

    //        LoginViewModel model = new LoginViewModel
    //        {
    //            UserName = "nieprawidłowyUżytkownik",
    //            Password = "nieprawidłoweHasło"
    //        };

    //        AccountController target = new AccountController(mock.Object);

    //        //Act
    //        ActionResult result = target.Login(model, "/MyURL");

    //        //Assert
    //        Assert.IsInstanceOfType(result, typeof(ViewResult));
    //        Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
    //    }
    }
}
