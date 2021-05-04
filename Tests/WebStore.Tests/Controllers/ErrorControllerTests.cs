using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Controllers;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
   public class ErrorControllerTests
    {
        //[TestMethod]
        //public void Error_Returns_View()
        //{
        //    var controller = new ErrorController();

        //    var result = controller.Error();

        //    Assert.IsType<ViewResult>(result);
        //}

        [TestMethod]
        public void Error_Returns_View()
        {
            var controller = new ErrorController();

            var result = controller.Error();

            Assert.IsType<ViewResult>(result);
        }
        [TestMethod]
        public void ErrorStatus_RedirectTo_Error()
        {
            var controller = new ErrorController();

            const string error_status_code = "404";
            const string expected_action_name = nameof(ErrorController.Error);

            var result = controller.ErrorStatus(error_status_code);

            var redirect_to_action = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(expected_action_name, redirect_to_action.ActionName);
            Assert.Null(redirect_to_action.ControllerName);
        }
    }
}
