using Services;
using Models;
using Xunit;
using Services.Exceptions;
using Services.Storage;
using ModelsDb;
using Services.Filters;

namespace ServiceTests
{
    public class ClientServiceTests
    {
        [Fact]
        public void AddClientLimit18YearsExceptionTest()
        {
            ClientService clientService = new ClientService(new ClientStorage());
            ClientDb ivan = new ClientDb
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
            ClientService clientService = new ClientService(new ClientStorage());
            ClientDb ivan = new ClientDb
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
        public void AddNewAccount_NoExistsClient_And_AccountAlreadyExistsExceptionTest()
        {
            // Arrange
            var clientStorage = new ClientStorage();
            var clientService = new ClientService(clientStorage);

            ClientDb ivan = new ClientDb
            {
                Name = "Ivan",
                BirtDate = new DateTime(2006, 01, 01),
                PasportNum = 324763
            };
            ClientDb ivanI = new ClientDb
            {
                Name = "Ivan",
                BirtDate = new DateTime(2006, 01, 01),
                PasportNum = 324763
            };
            AccountDb newAccount = new AccountDb
            {
                Currency = new CurrencyDb
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
                clientService.AddAccount(ivan, newAccount);

                Assert.Throws<ExistsException>(() => clientService.AddAccount(ivanI, newAccount));
                Assert.Throws<ExistsException>(() => clientService.AddAccount(ivan, newAccount));
                Assert.Contains(newAccount, clientStorage[ivan]);
            }
            catch (Exception e)
            {
                Assert.True(false);
            }

        }

    }
}