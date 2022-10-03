using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Services;
using Models;
using Services.Exceptions;

namespace ServiceTests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void AddEmployeeLimit18YearsExceptionTest()
        {
            var employeeService = new EmployeeService(new EmployeeStorage());
            var ivan = new Employee
            {
                Name = "Ivan",
                BirtDate = new DateTime(2006, 01, 01),
                PasportNum = 324763
            };
            try
            {
                employeeService.AddEmployee(ivan);
            }
            catch (Under18Exception e)
            {
                Assert.Equal("Работнику меньше 18", e.Message);
                Assert.Equal(typeof(Under18Exception), e.GetType());
            }
            catch (Exception e)
            {
                Assert.True(false);
            }
        }
        [Fact]
        public void AddEmployeeNoPasportDataExceptionTest()
        {
            var employeeService = new EmployeeService(new EmployeeStorage());
            var ivan = new Employee
            {
                Name = "Ivan",
                BirtDate = new DateTime(2000, 01, 01)
            };
            try
            {
                employeeService.AddEmployee(ivan);
            }
            catch (NoPasportData e)
            {
                Assert.Equal("У работника нет паспортных данных", e.Message);
                Assert.Equal(typeof(NoPasportData), e.GetType());
            }
            catch (Exception e)
            {
                Assert.True(false);
            }
        }
    }
}