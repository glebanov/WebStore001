using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Controllers
{
    public class ErrorController : Controller
    {
        //public IActionResult Error()
        //{
        //    return View();
        //}
        public IActionResult Error() => View();

        public IActionResult ErrorStatus(string code) => code switch
        {
            "404" => RedirectToAction(nameof(Error)),
            _ => Content($"Error code: {code}")
        };
    }
}
