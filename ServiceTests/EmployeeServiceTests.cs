using Xunit;
using Services;
using Models;
using Services.Exceptions;
using Services.Filters;
namespace ServiceTests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void AddEmployeeLimit18YearsExceptionTest()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var ivan = new Employee
            {
                Name = "Ivan",
                BirtDate = new DateTime(2006, 01, 01),
                PasportNum = 324763
            };
            // Act&Assert
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
            // Arrange
            var employeeService = new EmployeeService();
            var ivan = new Employee
            {
                Name = "Ivan",
                BirtDate = new DateTime(2000, 01, 01)
            };
            // Act&Assert
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
        [Fact]
        public void AddEmployeeExistingClientTest()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var testDataGenerator = new TestDataGenerator();
            var oldEmployee = testDataGenerator.GetFakeDataEmployee().Generate();
            var newEmployee = new Employee()
            {
                Name = oldEmployee.Name,
                PasportNum = oldEmployee.PasportNum,
                BirtDate = oldEmployee.BirtDate
            };
            // Act&Assert
            try
            {
                employeeService.AddEmployee(oldEmployee);
                employeeService.AddEmployee(newEmployee);

            }
            catch (ExistsException e)
            {
                Assert.Equal("Этот работник уже существует", e.Message);
                Assert.Equal(typeof(ExistsException), e.GetType());
            }
            catch (Exception e)
            {
                Assert.True(false);
            }
        }
        [Fact]
        public void GetEmployeesFilterTest()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var testDataGenerator = new TestDataGenerator();
            var employeeFilter = new EmployeeFilters();
            var employee = new Employee();

            for (int i = 0; i < 10; i++)
            {
                employee = testDataGenerator.GetFakeDataEmployee().Generate();
                employeeService.AddEmployee(employee);
            };

            // Act&Assert
            employeeFilter.Name = employee.Name;
            employeeFilter.PasportNum = employee.PasportNum;

            Assert.NotNull(employeeService.GetEmployees(employeeFilter));

        }

        [Fact]
        public void DeleteEmployeeKeyNotFoundExceptionTest()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var testDataGenerator = new TestDataGenerator();
            var existsEmployee = testDataGenerator.GetFakeDataEmployee().Generate();
            var noExistsEmployee = testDataGenerator.GetFakeDataEmployee().Generate();

            // Act&Assert
            try
            {
                employeeService.AddEmployee(existsEmployee);
                employeeService.DeleteEmployee(existsEmployee);
                Assert.Null(employeeService._dbContext.employees.FirstOrDefault(e => e.Id == existsEmployee.Id));
            }
            catch (ExistsException e)
            {
                Assert.Throws<ExistsException>(() => employeeService.DeleteEmployee(noExistsEmployee));
                Assert.Equal(typeof(ExistsException), e.GetType());
            }
            catch (Exception e)
            {
                Assert.Null(employeeService._dbContext.employees.FirstOrDefault(e => e.Id == existsEmployee.Id));
                Assert.True(false);
            }
        }
        [Fact]
        public void UpdateEmployeeKeyNotFoundExceptionTest()
        {
            // Arrange
            var employeeService = new EmployeeService();
            var testDataGenerator = new TestDataGenerator();
            var existsEmployee = testDataGenerator.GetFakeDataEmployee().Generate();
            var noExistsEmployee = testDataGenerator.GetFakeDataEmployee().Generate();

            // Act&Assert
            try
            {
                employeeService.AddEmployee(existsEmployee);
                employeeService.UpdateEmployee(existsEmployee);
            }
            catch (ExistsException e)
            {
                Assert.Throws<ExistsException>(() => employeeService.UpdateEmployee(noExistsEmployee));
                Assert.Equal(typeof(ExistsException), e.GetType());
            }
            catch (Exception e)
            {
                Assert.True(false);
            }

        }
    }
}