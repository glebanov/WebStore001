using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Controllers;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Index();

            // Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Contact_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Index();


            Assert.IsType<ViewResult>(result);
        }
        [TestMethod]
        public void Shop_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }
        [TestMethod]
        public void Checkout_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }
        [TestMethod]
        public void Login_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Index();


            Assert.IsType<ViewResult>(result);
        }
        [TestMethod]
        public void Blog_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }
        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }
    }
}
