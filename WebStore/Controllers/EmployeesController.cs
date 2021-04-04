using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Route("staff")] // Можно переименовать контроллер 
    public class EmployeesController : Controller
    {
        private List<Employee> _Employees;

        public EmployeesController()
        {
            _Employees = TestData.Employees;

        }

        //[Route("all")] //Можно переименовать Index
        public IActionResult Index() => View(_Employees);

        //[Route ("info(id-{id}")] //Можно переименовать Details
        public IActionResult Details(int id) // http://localhost:5000/employees/details/2
        {
            var employee = _Employees.FirstOrDefault(e => e.Id == id);
            if (employee is not null)
                return View(employee);
            return NotFound();
        }
    }
}
