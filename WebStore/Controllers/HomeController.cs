using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View("SecondView");

        public IActionResult SecondAction()
        {
            return Content("Second controller action");
        }
    }
}
