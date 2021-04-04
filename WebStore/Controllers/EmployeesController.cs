using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Route("staff")] // Можно переименовать контроллер 
    public class EmployeesController : Controller
    {

        private readonly IEmployeesData _EmployeesData;
        public EmployeesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        //[Route("all")] //Можно переименовать Index
        public IActionResult Index() => View(_EmployeesData.Get());

        //[Route ("info(id-{id}")] //Можно переименовать Details
        public IActionResult Details(int id) // http://localhost:5000/employees/details/2
        {
            var employee = _EmployeesData.Get(id);
            if (employee is not null)
                return View(employee);
            return NotFound();
        }
    }
}
