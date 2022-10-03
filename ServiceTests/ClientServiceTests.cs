using Services;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Services.Exceptions;

namespace ServiceTests
{
    public class ClientServiceTests
    {
        [Fact]
        public void AddClientLimit18YearsExceptionTest()
        {
            var clientService = new ClientService(new ClientStorage());
            var ivan = new Client
            {
                Name = "Ivan",
                BirtDate = new DateTime(2006, 01, 01),
                PasportNum = 324763
            };
            try
            {
                clientService.AddClient(ivan);
            }
            catch (Under18Exception e)
            {
                Assert.Equal("Клиенту меньше 18", e.Message);
                Assert.Equal(typeof(Under18Exception), e.GetType());
            }
            catch (Exception e)
            {
                Assert.True(false);
            }
        }
        [Fact]
        public void AddClientNoPasportDataExceptionTest()
        {
            var clientService = new ClientService(new ClientStorage());
            var ivan = new Client
            {
                Name = "Ivan",
                BirtDate = new DateTime(2006, 01, 01),
                PasportNum = 324763
            };
            try
            {
                clientService.AddClient(ivan);
            }
            catch (NoPasportData e)
            {
                Assert.Equal("У клиента нет паспортных данных", e.Message);
                Assert.Equal(typeof(NoPasportData), e.GetType());
            }
            catch (Exception e)
            {
                Assert.True(false);
            }
        }
        [Fact]
        public void AddClientExistsException()
        {
            var clientService = new ClientService(new ClientStorage());
            var ivan = new Client
            {
                Name = "Ivan",
                BirtDate = new DateTime(2006, 01, 01),
                PasportNum = 324763
            };
            var ivanI = new Client
            {
                Name = "Ivan",
                BirtDate = new DateTime(2006, 01, 01),
                PasportNum = 324763
            };
            try
            {
                clientService.AddClient(ivan);
                clientService.AddClient(ivanI);

            }
            catch (ExistsException e)
            {
                Assert.Equal("Такой клиент существует", e.Message);
                Assert.Equal(typeof(ExistsException), e.GetType());
            }
            catch (Exception e)
            {
                Assert.True(false);
            }
        }
        public void AddNewAccount_NoExistsClient_And_AccountAlreadyExistsExceptionTest()
        {
            // Arrange
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);

            var ivan = new Client
            {
                Name = "Ivan",
                BirtDate = new DateTime(2006, 01, 01),
                PasportNum = 324763
            };
            var ivanI = new Client
            {
                Name = "Ivan",
                BirtDate = new DateTime(2006, 01, 01),
                PasportNum = 324763
            };
            Account newAccount = new Account
            {
                Currency = new Currency
                {
                    Code = 5,
                    Name = "RUB",
                },
                Amount = 0
            };

            // Act/Assert
            try
            {
                clientService.AddClient(ivan);
                //clientService.AddAccount(ivan, newAccount);

                //Assert.Throws<ExistsException>(() => clientService.AddAccount(ivanI, newAccount));
                //Assert.Throws<ExistsException>(() => clientService.AddAccount(ivan, newAccount));
                //Assert.Contains(newAccount, clientStorage.Data[ivan]);
            }
            catch (Exception e)
            {
                Assert.True(false);
            }

        }

    }
}