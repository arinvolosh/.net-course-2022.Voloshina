using Microsoft.EntityFrameworkCore;
using Models;
using ModelsDb;
using Services.Exceptions;
using Services.Filters;
using Services.Storage;

namespace Services
{
    public class EmployeeService
    {
        public DbBank _dbContext;

        public EmployeeService()
        {
            _dbContext = new DbBank();
        }
        public async Task<EmployeeDb> GetEmployee(Guid employeeId)
        {
            var employee = await _dbContext.employees.FirstOrDefaultAsync(c => c.Id == employeeId);

            if (employee == null)
            {
                throw new ExistsException("Этого работника не сущетсвует");
            }
            return employee;
        }
        public async Task<List<Employee>> GetEmployees(EmployeeFilters employeeFilter)
        {
            var selection = _dbContext.employees.Select(p => p);

            if (employeeFilter.Name != null)
                selection = selection.
                    Where(p => p.Name == employeeFilter.Name);

            if (employeeFilter.PasportNum != 0)
                selection = selection.
                   Where(p => p.PasportNum == employeeFilter.PasportNum);

            if (employeeFilter.StartDate != new DateTime())
                selection = selection.
                   Where(p => p.BirtDate == employeeFilter.StartDate);

            if (employeeFilter.EndDate != new DateTime())
                selection = selection.
                   Where(p => p.BirtDate == employeeFilter.EndDate);

            if (employeeFilter.Salary != 0)
                selection = selection.
                   Where(p => p.PasportNum == employeeFilter.PasportNum);

            return await selection.Select(employeeDb => new Employee()
            {
                Id = employeeDb.Id,
                Name = employeeDb.Name,
                PasportNum = employeeDb.PasportNum,
                BirtDate = employeeDb.BirtDate
            })
            .ToListAsync();
        }

        public async Task AddEmployee(Employee employee)
        {
            var employeeDb = new EmployeeDb()
            {
                Id = employee.Id,
                Name = employee.Name,
                PasportNum = employee.PasportNum,
                BirtDate = employee.BirtDate
            };
            if (employeeDb.PasportNum == 0)
            {
                throw new NoPasportData("У работника нет паспортных данных");
            }

            if (DateTime.Now.Year - employeeDb.BirtDate.Year < 18)
            {
                throw new Under18Exception("Работник меньше 18 лет");
            }
            await _dbContext.employees.AddAsync(employeeDb);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task UpdateEmployee(Employee employee)
        {
            var employeeDb = await _dbContext.employees.FirstOrDefaultAsync(c => c.Id == employee.Id);

            if (employeeDb == null)
            {
                throw new ExistsException("Этого работника не существует");
            }

            _dbContext.employees.Update(employeeDb);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEmployee(Employee employee)
        {
            var employeeDb = await _dbContext.employees.FirstOrDefaultAsync(c => c.Id == employee.Id);
            if (employeeDb == null)
            {
                throw new ExistsException("В базе нет такого клиента");
            }
            _dbContext.employees.Remove(employeeDb);
            await _dbContext.SaveChangesAsync();
        }
    }
}