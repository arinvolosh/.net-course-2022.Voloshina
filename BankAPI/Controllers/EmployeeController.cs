﻿using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController
    {
        private EmployeeService _employeeService;
        public EmployeeController()
        {
            _employeeService = new EmployeeService();
        }

        [HttpGet]
        public async Task<Employee> GetEmployee(Guid id)
        {
            return await _employeeService.GetEmployee(id);
        }

        [HttpPost]
        public async Task AddEmployee(Employee employee)
        {
            await _employeeService.AddEmployee(employee);
        }

        [HttpDelete]
        public async Task DeleteEmployee(Employee employee)
        {
            await _employeeService.DeleteEmployee(employee);
        }

        [HttpPut]
        public async Task UpdateEmployee(Employee employee)
        {
            await _employeeService.UpdateEmployee(employee);
        }
    }
}
