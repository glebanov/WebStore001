using System;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers.API
{
    public class ConsoleController : ControllerBase
    {
        public IActionResult Clear()
        {
            Console.Clear();
            return Ok();
        }

        public IActionResult WriteLine(string Message)
        {
            Console.WriteLine(Message);
            return Ok();
        }
    }
}