using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesApiController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        [HttpGet] 
        public IEnumerable<Employee> Get() => _EmployeesData.Get();

        [HttpGet("{id}")] 
        public Employee Get(int id) => _EmployeesData.Get(id);

        [HttpGet("employee")] 
        public Employee GetByName(string LastName, string FirstName, string Patronymic) => _EmployeesData.GetByName(LastName, FirstName, Patronymic);

        [HttpPost]
        public int Add(Employee employee) => _EmployeesData.Add(employee);

        [HttpPost("employee")] 
        public Employee Add(string LastName, string FirstName, string Patronymic, int Age) => _EmployeesData.Add(LastName, FirstName, Patronymic, Age);

        
        [HttpPut]// put -> http://localhost:5001/api/employees/5
        public void Update(/*int id, */Employee employee) => _EmployeesData.Update(employee);

        [HttpDelete("{id}")]
        public bool Delete(int id) => _EmployeesData.Delete(id);
    }
}