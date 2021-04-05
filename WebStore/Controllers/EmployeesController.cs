using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;

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

        #region Edit

        public IActionResult Edit(int id)
        {
            if (id <= 0) return BadRequest();

            var employee = _EmployeesData.Get(id); //Получаем сотрудника из сервиса по id

            if (employee is null) //Проверям, чтобы сотрудник был найден
                return NotFound(); // Если не был найден, возвращаем NotFound

            return View(new EmployeeViewModel //Создаем объект и заполняем его параметры
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                MiddleName = employee.Patronymic,
                Age = employee.Age
            });
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model) //Второй метод получает данные из ViewModel и может вызван только Post запросом
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            var employee = new Employee //Появляется новый объект сотрудника
            {
                Id = model.Id,
                LastName = model.LastName,
                FirstName = model.Name,
                Patronymic = model.MiddleName,
                Age = model.Age
            };

            _EmployeesData.Update(employee);

            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();

            var employee = _EmployeesData.Get(id);

            if (employee is null)
                return NotFound();

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                MiddleName = employee.Patronymic,
                Age = employee.Age
            });
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _EmployeesData.Delete(id);

            return RedirectToAction("Index");
        }

        #endregion


    }
}
