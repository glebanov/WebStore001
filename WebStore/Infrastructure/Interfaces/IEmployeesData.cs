﻿using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> Get(); // Должно выдавать сотрудников

        Employee Get(int id); //Должно выдавать одного сотрудника по индификатору

        int Add(Employee employee); //Должно уметь добавлять сотрудника

        void Update(Employee employee); //Должно уметь обновлять сотрудника 

        bool Delete(int id); //Должно уметь удалять сотрудника по индификатору
    }
}